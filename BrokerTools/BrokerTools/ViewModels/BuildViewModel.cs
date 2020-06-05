using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using BrokerTools.Base;
using BrokerTools.Data.Model;
using BrokerTools.Data;
using BrokerTools.Data.Adapter;
using BrokerTools.Data.Controller;
using BrokerTools.Data.FundProxy;
using System.Windows.Threading;
using System.Windows;
using BrokerTools.Controllers;

namespace BrokerTools.ViewModels
{


    public class Task
    {
        public string TaskDescription { get; set; }
        public Product product { get; set; }
        public int Total { get; set; }
        public int Processed { get; set; }
        public bool Completed { get; set; }
    }

    /// <summary>
    ///     View model to initialize and update the database
    /// </summary>
    public class BuildViewModel : BaseViewModel
    {

        ServiceAdapter serviceadapter;

        DelegateCommand _SyncDatabaseCommand;

        ObservableCollection<Task> Tasks = new ObservableCollection<Task>();

        public ObservableCollection<string> TasksComplete { get; private set; }

        public BuildViewModel()
        {
            serviceadapter = new ServiceAdapter();
            serviceadapter.OnFundProcessed += new ServiceAdapter.FundProcessedHandler(serviceadapter_OnFundProcessed);
            serviceadapter.OnFundProcessError += new ServiceAdapter.FundProcessErrorHandler(serviceadapter_OnFundProcessError);

            TasksComplete = new ObservableCollection<string>();

            StepType = true;

            //TasksComplete = new ObservableCollection<string>();
            if (DesignerProperties.IsInDesignTool)
            {
                Step = "Sample step...";
                StepType = false;
                ProgressPct = 50;
                Total = 50;
                TasksComplete.Add("Task 1");
                TasksComplete.Add("Task 2");
                return;
            }

            ShellViewService.Current.RebuildRequested +=new EventHandler(BeginBuild);
        }

        private string _Title;

        /// <summary>
        ///     Title
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set
            {
                _step = value;
                if (Deployment.Current.Dispatcher.CheckAccess())
                    RaisePropertyChanged(() => Title);
                else
                    Deployment.Current.Dispatcher.BeginInvoke(() => RaisePropertyChanged(() => Title));

            }
        }

        ///// <summary>
        /////     Completed tasks
        ///// </summary>
        //public ObservableCollection<string> TasksComplete { get; private set; }
        private string _step;

        /// <summary>
        ///     Current step
        /// </summary>
        public string Step
        {
            get { return _step; }
            set
            {
                _step = value;
                if (Deployment.Current.Dispatcher.CheckAccess())
                    RaisePropertyChanged(() => Step);
                else
                    Deployment.Current.Dispatcher.BeginInvoke(() => RaisePropertyChanged(() => Step));

            }
        }

        private bool _stepType;

        /// <summary>
        ///     True if the progress of this step is indeterminate
        /// </summary>
        public bool StepType
        {
            get { return _stepType; }
            set
            {
                _stepType = value;
                if (Deployment.Current.Dispatcher.CheckAccess())
                    RaisePropertyChanged(() => StepType);
                else
                    Deployment.Current.Dispatcher.BeginInvoke(() => RaisePropertyChanged(() => StepType));
            }
        }

        private int _progressPct;

        /// <summary>
        ///     Percentage completed
        /// </summary>
        public int ProgressPct
        {
            get { return _progressPct; }
            set
            {
                _progressPct = value;
                
                if (Deployment.Current.Dispatcher.CheckAccess())
                    RaisePropertyChanged(() => ProgressPct);
                 else
                    Deployment.Current.Dispatcher.BeginInvoke(() => RaisePropertyChanged(() => ProgressPct));
          
            }
        }

        private int _total;

        /// <summary>
        ///     Total items processed in this step
        /// </summary>
        public int Total
        {
            get { return _total; }
            set
            {
                _total = value;

                if (Deployment.Current.Dispatcher.CheckAccess())
                   RaisePropertyChanged(() => Total);
                else
                    Deployment.Current.Dispatcher.BeginInvoke(() => RaisePropertyChanged(() => Total));
            }
        }

        /// <summary>
        /// Gets the "Build Database" Command.
        /// </summary>
        public DelegateCommand SyncDatabaseCommand
        {
            get
            {
                if (_SyncDatabaseCommand == null)
                    _SyncDatabaseCommand = new DelegateCommand(OnSyncDatabase);
                return _SyncDatabaseCommand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        void OnSyncDatabase(object parameter)
        {
            BeginBuild(null, null);
        }

        void ProcessTask(Task task)
        {
            Step = String.Concat("Retrieving Funds List for Product ", task.product.Name, " ...");

           
            StepType = false;
            ProgressPct = 0;

            serviceadapter.GetFundsByProductCode(task.product.Code,
                                               (result) =>
                                               {
                                                   OLife olife = result.Result;

                                                   Total = olife.Items.Count();
                                                   
                                                   task.Total = Total;

                                                   foreach (InvestmentProduct investproduct in olife.Items)
                                                   {
                                                       SourceLookup lookup = new SourceLookup()
                                                       {
                                                           SourceType = SourceTypes.Silica_FundCode,
                                                           SourceId = investproduct.InvestProductSysKey[0].Replace("Compass_FundCode{", "").Replace("}", ""),
                                                       };

                                                       //Only for dev purposes until the live feed is up
                                                       lookup.ProductCode = investproduct.ProductCode;

                                                       serviceadapter.AddRequest(lookup);
                                                   }
                                               });
        }

        /// <summary>
        ///     Entry point
        /// </summary>
        public void BeginBuild(object sender, EventArgs args)
        {

            ShellViewService.Current.RebuildRequested -= new EventHandler(BeginBuild);
            try
            {
                Step = "Intilialize Database ...";
                StepType = true;

                if (!BuildDBController.HasInitialized())
                    BuildDBController.BuildCache();

                IEnumerable<Product> productlist = ProductController.GetAllProduct();

                if (productlist.Count() > 0)
                {

                    Step = "Clear Cached funds ...";
                    StepType = true;
                    FundController.ClearFundData();

                    serviceadapter.Start();
                }

                Tasks = (from a in productlist.Where(t=>t.Code == ProductCode.IP)
                         select new Task
                         {
                             product = a,
                             TaskDescription = String.Concat("Retrieving Funds List for Product ", a.Name, " ...")
                         }).ToObservableCollection();


                Task task = Tasks[0];
                ProcessTask(task);

            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
       
        /// <summary>
        /// Fund Processed Callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="evt"></param>
        void serviceadapter_OnFundProcessed(object sender, Data.Extentions.InvestProductEventArgs evt)
        {
            Step = String.Concat("Fund Processed - ", evt.InvestmentProduct.FullName);

            ProgressPct++;

            Task task = Tasks.SingleOrDefault(t => t.product.Code.ToString("F").ToUpper() == evt.InvestmentProduct.ProductCode.ToUpper());

            String TaskDescription = task.TaskDescription;

            task.Processed++;

            if (task.Processed >= task.Total)
            {
                task.Completed = true;


                if (Deployment.Current.Dispatcher.CheckAccess())
                    this.TasksComplete.Add(task.TaskDescription);
                else
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() => this.TasksComplete.Add(TaskDescription));
                 
                }

                task = Tasks.FirstOrDefault(t => t.Completed == false);
                if (task != null)
                {
                    ProcessTask(task);
                }
                else
                {
                    ProgressPct = 0;
                    Deployment.Current.Dispatcher.BeginInvoke(() => ShellViewService.Current.RebuildRequested += new EventHandler(BeginBuild));
                    Deployment.Current.Dispatcher.BeginInvoke(() => ShellViewService.Current.Navigator.NavigateToShellView());

                    BuildDBController.SetSyncNow();

                }
            }
                
        }

        void serviceadapter_OnFundProcessError(object sender, Data.Extentions.SourceLookupEventArgs evt)
        {
            Task task = Tasks.SingleOrDefault(t => t.product.Code.ToString("F").ToUpper() == evt.SourceLookup.ProductCode.ToUpper());

            if (task != null)
            {
                task.Total--;
            }
        }
   }
}