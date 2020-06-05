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
    public class ProductRepository : Repository
    {
        /// <summary>
        /// Check if DB has been setup
        /// </summary>
        /// <returns></returns>
        public int GetProductCount()
        {
            return siaqodb.LoadAll<Product>().Count();
        }

        /// <summary>
        /// Return all Products
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetAllProducts()
        {
            return siaqodb.LoadAll<Product>();
        }

        /// <summary>
        /// Get Product By Code
        /// </summary>
        /// <returns></returns>
        public Product GetFundsByProductOID(ProductCode productCode)
        {
            return siaqodb.Cast<Product>().SingleOrDefault(t => t.Code == productCode);
        }
    }
}
