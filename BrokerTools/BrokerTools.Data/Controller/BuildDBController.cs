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

namespace BrokerTools.Data.Controller
{
    public class BuildDBController
    {
        /// Check DB Initialized
        /// </summary>
        public static bool HasInitialized()
        {
            using (BuildDBRepository db = new BuildDBRepository())
            {
                return db.HasInitialized;
            }
        }

        public static bool ShouldSync()
        {
            bool Result = false;

            using (BuildDBRepository db = new BuildDBRepository())
            {
                if (!db.HasInitialized)
                    Result = true;
                else
                {
                    DateTime lastSync = DateTime.MinValue;
                    var synclog = db.GetLastSync();

                    if (synclog == null)
                        Result = true;
                    else
                    {
                        if (DateTime.Now.Subtract(synclog.LastSync).TotalDays >= 120)
                            Result = true;
                    }
                }
            }

            return Result;
        }

        public static void BuildCache()
        {
            using (BuildDBRepository db = new BuildDBRepository())
            {
                db.BuildCache();
            }
        }

        public static void SetSyncNow()
        {
            using (BuildDBRepository db = new BuildDBRepository())
            {
                db.SetSyncNow();
            }
        }
    }
}
