using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

using BrokerTools.Data.FundProxy;
using System.Threading;
using BrokerTools.Data.Infrastructure;
using System.ServiceModel;
using BrokerTools.Data.Extentions;
using BrokerTools.Data.Model;
using BrokerTools.Data.Controller;

namespace BrokerTools.Data.Adapter
{

    public class ServiceAdapter
    {
        private SynchronizationContext synchronizationContext = System.Threading.SynchronizationContext.Current ?? new SynchronizationContext();

        private BackgroundWorker _Worker = new BackgroundWorker();
        private Queue<SourceLookup> _Queue = new Queue<SourceLookup>();
        private List<SourceLookup> _ActiveRequests = new List<SourceLookup>();

        FundServiceClient proxy;

        private const int _MaxRequests = 3;
        public ServiceAdapter()
        {
           proxy = new FundServiceClient();
           proxy.GetFundCompleted += new EventHandler<GetFundCompletedEventArgs>(proxy_GetFundCompleted);
           _Worker.DoWork += DoWork;
        }

        public void Start()
        {
            _Worker.RunWorkerAsync();
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            while (!_Worker.CancellationPending)
            {
                if (_Queue.Count > 0)
                {
                    int _NumRequests = 0;
                    lock (_ActiveRequests)
                    {
                        _NumRequests = _ActiveRequests.Count;
                    }

                    if (_NumRequests >= _MaxRequests)
                        continue;

                    // Check the queue for new request items 
                    SourceLookup lookup = null;
                    lock (_Queue)
                    {
                        lookup = _Queue.Dequeue();
                    }

                    if (lookup == null)
                        continue;

                    // We found a new request item!                 
                    lock (_ActiveRequests)
                    {
                        _ActiveRequests.Add(lookup);
                    }
                    // TBD: Initiate an async service request,                
                    //      something like the following:
                    try
                    {
                        proxy.GetFundAsync(lookup,lookup);
                    }
                    catch (Exception Ex)
                    {
                    }
                }
            }
        }

        void proxy_GetFundCompleted(object sender, GetFundCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null || e.Cancelled)
                {
                    if (OnFundProcessError != null)
                    {
                        OnFundProcessError(this, new SourceLookupEventArgs(e.UserState as SourceLookup));
                    }
                }

                OLife obj = e.Result;

                InvestmentProduct investmentproduct = (InvestmentProduct)obj.Items[0];

                string code = investmentproduct.InvestProductSysKey[0].Replace("Silica_FundCode{","").Replace("}","");

                SourceLookup lookup = e.UserState as SourceLookup; //_ActiveRequests.SingleOrDefault(t => t.SourceId == code);
                lock (_ActiveRequests)
                {
                    if(lookup != null)
                        _ActiveRequests.Remove(lookup);

                    Fund fund = new Fund(investmentproduct);

                    FundController.CreateFundWithPerformance(fund); 
                    
                    if (OnFundProcessed != null)
                    {
                        OnFundProcessed(this, new InvestProductEventArgs(investmentproduct));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddRequest(SourceLookup lookup)
        {
            lock (_Queue)
            {
                _Queue.Enqueue(lookup);
            }
        }

        public delegate void FundProcessedHandler(object sender, InvestProductEventArgs evt);
        // event declaration 
        public event FundProcessedHandler OnFundProcessed;

        public delegate void FundProcessErrorHandler(object sender, SourceLookupEventArgs evt);
        // event declaration 
        public event FundProcessErrorHandler OnFundProcessError;

        public void GetFundsByProductCode(ProductCode ProductCode, Action<IOperationResult<OLife>> callback)
        {

            ThreadPool.QueueUserWorkItem(
            o =>
            {
                var channelFactory = new ChannelFactory<IFundServiceChannel>("*");
                var simpleService = channelFactory.CreateChannel();

                simpleService.BeginGetFunds(ProductCode,
                (ar) =>
                {

                    OperationResult<OLife> operationResult = new OperationResult<OLife>();

                    try
                    {
                        operationResult.Result = simpleService.EndGetFunds(ar);
                    }
                    catch (Exception ex)
                    {
                        operationResult.Error = ex;
                    }

                    synchronizationContext.Post(
                        (state) =>
                        {
                            callback(operationResult);
                        },
                        null);
                },
                null);
            });
        }
    } 
}
