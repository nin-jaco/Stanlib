using System;
using Sqo;
using System.Collections.Generic;
using BrokerTools.Model;
using System.Linq.Expressions;
using Sqo.Transactions;

namespace BrokerTools.Data
{
    public class Repository:IDisposable
    {
        protected Siaqodb siaqodb;

        protected Transaction transaction;

        public Repository()
        {
            siaqodb = SiaqodbFactory.GetInstance();
        }

        public void Save<T>(T obj)
        {
            siaqodb.StoreObject(obj);
        }


        public void Save<T>(T obj, Transaction transaction)
        {
            siaqodb.StoreObject(obj, transaction);
        }
       
        public void Delete<T>(T obj)
        {
            siaqodb.Delete(obj);
            siaqodb.Flush();
        }

        public void Delete<T>(T obj, Transaction transaction)
        {
            siaqodb.Delete(obj, transaction);
            siaqodb.Flush();
        }

        public IEnumerable<T> QueryList<T>(Expression<Func<T, bool>> predicate)
        {
            return siaqodb.Cast<T>().Where(predicate);
        }

        public void Flush()
        {
            siaqodb.Flush();
        }

        public void Dispose()
        {
            SiaqodbFactory.CloseDatabase();
        }
    }
}
