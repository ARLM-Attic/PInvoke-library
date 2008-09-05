using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Foosun.Config
{
    public class ConfigFileManage
    {
        
        private static string filepath;
        private static IConfigInfo configinfo = null;
        private static object lockHelper = new object();

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="configfilepath">配置文件路径</param>
        /// <param name="configtype">配置类型</param>
        /// <returns>返回配置实例</returns>
        public static IConfigInfo DeserializeInfo(string configfilepath, Type configtype)
        {
            IConfigInfo info;
            FileStream stream = null;
            try
            {
                stream = new FileStream(configfilepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(configtype);
                info = (IConfigInfo)serializer.Deserialize(stream);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return info;
        }

        public static IConfigInfo LoadConfig(ref string fileoldchange, string configFilePath, Type type)
        {
            return LoadConfig(ref fileoldchange, configFilePath, type, true);
        }

        public static IConfigInfo LoadConfig(ref string fileoldchange, string configFilePath, Type type, bool checkTime)
        {
            filepath = configFilePath;
            
            if (checkTime)
            {
                string lastWriteTime = File.GetLastWriteTime(configFilePath).ToString();
                if (fileoldchange == lastWriteTime)
                {
                    return configinfo;
                }
                fileoldchange = lastWriteTime;
                lock (lockHelper)
                {
                    configinfo = DeserializeInfo(configFilePath, type);
                    return configinfo;
                }
            }
            lock (lockHelper)
            {
                configinfo = DeserializeInfo(configFilePath, type);
            }
        
            return configinfo;
        }


       

       

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="configFilePath">保存配置信息的物理路径</param>
        /// <param name="configinfo">配置信息实例</param>
        /// <returns>成功或失败</returns>
        public static bool SaveConfig(string configFilePath, IConfigInfo configinfo)
        {
            bool flag = false;
            FileStream stream = null;
            try
            {
                stream = new FileStream(configFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                new XmlSerializer(configinfo.GetType()).Serialize((Stream)stream, configinfo);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return flag;
        }
 


        public static string ConfigFilePath
        {
            get
            {
                return filepath;
            }
            set
            {
                filepath = value;
            }
        }

        public static IConfigInfo ConfigInfo
        {
            get
            {
                return configinfo;
            }
            set
            {
                configinfo = value;
            }
        }


       
    }
}
