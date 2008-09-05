using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Config.API
{

    /// <summary>
    /// 整合应用信息类
    /// </summary>
    [Serializable]
    public class ApplicationInfo
    {
        public ApplicationInfo()
        {
        }
        string appID = string.Empty;

        /// <summary>
        /// 应用标识，不同的应用标识ID应不能重复
        /// </summary>
        public string AppID
        {
            get { return appID; }
            set { appID = value; }
        }
         
 
        string appUrl = string.Empty;
        /// <summary>
        /// 应用的接口URL
        /// </summary>
        public string AppUrl
        {
            get { return appUrl; }
            set { appUrl = value; }
        }

    }
}
