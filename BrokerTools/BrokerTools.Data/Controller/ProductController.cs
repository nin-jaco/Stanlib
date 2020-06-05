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
using BrokerTools.Data.Model;
using System.Collections.Generic;
using System.Linq;
using BrokerTools.Data.FundProxy;

namespace BrokerTools.Data.Controller
{
    public class ProductController
    {
        /// Get All Products
        /// </summary>
        public static bool HasInitialized()
        {
            using (BuildDBRepository db = new BuildDBRepository())
            {
                return db.HasInitialized;
            }
        }

        /// Check DB Initialized
        /// </summary>
        public static IEnumerable<Product> GetAllProduct()
        {
            using (ProductRepository db = new ProductRepository())
            {
                return db.GetAllProducts();
            }
        }

        /// Get Product By Code
        /// </summary>
        public static Product GetProductByCode(ProductCode productCode)
        {
            using (ProductRepository db = new ProductRepository())
            {
                return db.GetFundsByProductOID(productCode);
            }
        }
    }
}
