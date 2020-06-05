using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Sqo;
using System.ComponentModel;

namespace BrokerTools.Data
{
    public class SiaqodbFactory
    {
        public static string siaoqodbPath = "BrokerTools";
        private static Siaqodb instance;

        public static void SetDBName(string path)
        {
            siaoqodbPath = path;
        }
        public static Siaqodb GetInstance()
        {
            //if (DesignerProperties.IsInDesignTool)
            //    return new Siaqodb();

            if (instance == null)
            {
                instance = new Siaqodb(siaoqodbPath);
            }
            return instance;
        }
        public static void CloseDatabase()
        {
            if (instance != null)
            {
                instance.Close();
                instance = null;
            }
        }
#if SILVERLIGHT && !WINDOWS_PHONE
        public static void CreateSiaqodbWithSpecialFolder(string path, Environment.SpecialFolder specialFolder)
        {
            instance = new Siaqodb(path, specialFolder);
        }
#endif
    }
}
