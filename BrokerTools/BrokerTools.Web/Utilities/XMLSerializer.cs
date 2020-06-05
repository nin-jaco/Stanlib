using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace BrokerTools.Web.Utilities
{
    public class XMLSerializer
    {
        XMLSerializer() { }

        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        public static String SerializeObjectToXML<T>(T objectToSerialize) where T : new()
        {
            try
            {
                String XmlizedString = null;
                MemoryStream memoryStream = new MemoryStream();
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                xs.Serialize(xmlTextWriter, objectToSerialize);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
                if (!XmlizedString[0].Equals('<'))
                    XmlizedString = XmlizedString.Substring(1);
                return XmlizedString;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return null;
            }
        }

        public static T DeserializeObjectFromXML<T>(string xml) where T : new()
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer;
            try
            {
                xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(new StringReader(xml));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
