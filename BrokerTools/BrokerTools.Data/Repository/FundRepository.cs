using System;
using System.Linq;
using Sqo;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BrokerTools.Model;
using System.Threading;
using BrokerTools.Data.Infrastructure;
using BrokerTools.Data.FundProxy;
using System.ServiceModel;
using BrokerTools.Data.Model;

namespace BrokerTools.Data
{
    public class FundRepository : Repository
    {
       

        //public List<Fund> GetFundsByProductCode(string code)
        //{
        //    return siaqodb.Cast<Fund>().Where(t => t.ProductCode == code).ToList();
        //}

        public IEnumerable<Fund> GetFundsByProductOID(int ProductOID)
        {
            return siaqodb.Cast<Fund>().Where(t => t.ProductOID == ProductOID).ToList();
        }

        public IEnumerable<Fund> GetFundsByProductCode(ProductCode productCode)
        {
            return siaqodb.Cast<Fund>().Where(t => t.ProductCode == productCode).ToList();
        }

        /// <summary>
        /// Save a Fund with performance values
        /// </summary>
        /// <param name="fund"></param>
        public void CreateFundWithPerformance(Fund fund)
        {
            try
            {
                transaction = siaqodb.BeginTransaction();

                Save<Fund>(fund, transaction);

                foreach (var performance in fund.PerformanceList)
                {
                    Save<FundPerformance>(performance, transaction);
                }

                transaction.Commit();
            }
            catch (Exception Ex)
            {
                transaction.Rollback();
                throw Ex;
            }
            finally
            {
               
            }
        }

        public void ClearFundData()
        {
            try
            {
                transaction = siaqodb.BeginTransaction();

                siaqodb.DropType<Fund>();
                siaqodb.DropType<FundPerformance>();

                transaction.Commit();
            }
            catch (Exception Ex)
            {
                transaction.Rollback();
                throw Ex;
            }
            finally
            {

            }
        }
    }
}
