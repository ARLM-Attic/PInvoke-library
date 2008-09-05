using System;
#if NET1
#else
using System.Collections.Generic;
#endif
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Collections;

namespace Discuz.Web.Services.API
{
    public class Util
    {
        private const string LINE = "\r\n";
#if NET1
        private static Hashtable serializer_dict = new Hashtable();
#else
		private static Dictionary<int, XmlSerializer> serializer_dict = new Dictionary<int, XmlSerializer>();
#endif

        private DNTParam VersionParam = DNTParam.Create("v", "1.0");
        private string api_key;
        private string secret;
        private bool use_json;

        private static XmlSerializer ErrorSerializer
        {
            get
            {
                return GetSerializer(typeof(Error));
            }
        }

        public Util(string api_key, string secret)
        {
            this.api_key = api_key;
            this.secret = secret;
        }

        /// <summary>
        /// 是否使用json格式返回数据
        /// </summary>
        public bool UseJson
        {
            get { return use_json; }
            set { use_json = value; }
        }

        /// <summary>
        /// 整合程序密钥
        /// </summary>
        internal string SharedSecret
        {
            get { return secret; }
            set { secret = value; }
        }

        /// <summary>
        /// 整合程序Key
        /// </summary>
        internal string ApiKey
        {
            get { return api_key; }
        }

        //public T GetResponse<T>(string method_name, params DNTParam[] parameters)
        //{
        //    string url = FormatGetUrl(method_name, parameters);
        //    byte[] response_bytes = GetResponseBytes(url);

        //    XmlSerializer response_serializer = GetSerializer(typeof(T));
        //    try
        //    {
        //        T response = (T)response_serializer.Deserialize(new MemoryStream(response_bytes));
        //        return response;
        //    }
        //    catch
        //    {
        //        Error error = (Error)ErrorSerializer.Deserialize(new MemoryStream(response_bytes));
        //        throw new DNTException(error.ErrorCode, error.ErrorMsg);
        //    }
        //}

        //public XmlDocument GetResponse(string method_name, params DNTParam[] parameters)
        //{
        //    string url = FormatGetUrl(method_name, parameters);
        //    byte[] response_bytes = GetResponseBytes(url);

        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(Encoding.Default.GetString(response_bytes));

        //    return doc;
        //}

        /// <summary>
        /// 获得远程页面内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static byte[] GetResponseBytes(string url)
        {
            WebRequest request = HttpWebRequest.Create(url);
            WebResponse response = null;

            try
            {
                response = request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return Encoding.UTF8.GetBytes(reader.ReadToEnd());
                }
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }

        /// <summary>
        /// 获得序列器
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static XmlSerializer GetSerializer(Type t)
        {
            int type_hash = t.GetHashCode();

            if (!serializer_dict.ContainsKey(type_hash))
                serializer_dict.Add(type_hash, new XmlSerializer(t));

            return serializer_dict[type_hash] as XmlSerializer;
        }

        /// <summary>
        /// 转换参数
        /// </summary>
        /// <param name="method_name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal DNTParam[] Sign(string method_name, DNTParam[] parameters)
        {
            ArrayList list = new ArrayList();
            list.Add(DNTParam.Create("method", method_name));
            list.Add(DNTParam.Create("api_key", api_key));
            list.Add(VersionParam);
            list.Sort();

            StringBuilder values = new StringBuilder();

            foreach (DNTParam param in list)
                values.Append(param.ToString());

            values.Append(secret);

            byte[] md5_result = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(values.ToString()));

            StringBuilder sig_builder = new StringBuilder();

            foreach (byte b in md5_result)
                sig_builder.Append(b.ToString("x2"));

            list.Add(DNTParam.Create("sig", sig_builder.ToString()));

            return (DNTParam[])list.ToArray();
        }

        /// <summary>
        /// 将字符型转为浮点型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static float GetFloatFromString(string input)
        {
            float returnValue;
#if NET1
            try
            {
                returnValue = Convert.ToSingle(input);
            }
            catch 
            {
                returnValue = -1;
            }
#else
            float.TryParse(input, out returnValue);
#endif
            return returnValue;
        }

    }
}
