using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Config.API
{
    /// <summary>
    /// 整合配置信息类
    /// </summary>
    [Serializable]
    public class APIConfig : IConfigInfo
    {
        public APIConfig()
        {
        }
        ApplicaitonCollection applicationList;
        /// <summary>
        /// 应用列表
        /// </summary>
        public ApplicaitonCollection ApplicationList
        {
            get { return applicationList; }
            set { applicationList = value; }
        }

        bool enable;
        /// <summary>
        /// 是否启用整合
        /// </summary>
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        string appKey;
        /// <summary>
        /// 整合密钥
        /// </summary>
        public string AppKey
        {
            get { return appKey; }
            set { appKey = value; }
        }
        string appID;
        /// <summary>
        /// 当前应用的标识ID
        /// </summary>
        public string AppID
        {
            get { return appID; }
            set { appID = value; }
        }
 
    }
}
