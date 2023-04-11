using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ARP_Spoofing_Client.ObjectSerializer
{

    interface IObjectSerializable
    {
        string Serialize<T>(T obj) where T : class;
        T Deserialize<T>(string xmlString) where T : class;
    }

    class XmlObjectSerializer : IObjectSerializable
    {
        /// <summary>
        /// 將物件序列化成XML格式字串
        /// </summary>
        /// <typeparam name="T">物件型別</typeparam>
        /// <param name="obj">物件</param>
        /// <returns>XML格式字串</returns>
        public string Serialize<T>(T obj) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            var stringWriter = new StringWriter();
            using (var writer = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(writer, obj);
                return stringWriter.ToString();
            }
        }
        /// <summary>
        /// 將XML格式字串反序列化成物件
        /// </summary>
        /// <typeparam name="T">物件型別</typeparam>
        /// <param name="xmlString">XML格式字串</param>
        /// <returns>反序列化後的物件</returns>
        public T Deserialize<T>(string xmlString) where T : class
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(xmlString))
            {
                object deserializationObj = deserializer.Deserialize(reader);
                return deserializationObj as T;
            };
        }
    }

    class JsonObjectSerializer : IObjectSerializable
    {
        /// <summary>
        /// 將物件序列化成JSON格式字串
        /// </summary>
        /// <typeparam name="T">物件型別</typeparam>
        /// <param name="obj">物件</param>
        /// <returns>JSON格式字串</returns>
        public string Serialize<T>(T obj) where T : class
        {
            string jsonData = JsonConvert.SerializeObject(obj);
            //Console.WriteLine(jsonData);
            return jsonData;
        }
        /// <summary>
        /// 將JSON格式字串反序列化成物件
        /// </summary>
        /// <typeparam name="T">物件型別</typeparam>
        /// <param name="jsonString">XML格式字串</param>
        /// <returns>反序列化後的物件</returns>
        public T Deserialize<T>(string jsonString) where T : class
        {
            T deserializationObj = JsonConvert.DeserializeObject<T>(jsonString);
            return deserializationObj;
        }
    }

}
