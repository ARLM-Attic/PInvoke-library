using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Config
{
    public class OfficialConfig
    {
        /// <summary>
        /// 官方文件对比地址
        /// </summary>
        static public string CompareFileUrl = BaseConfig.GetConfigValue("comparefileurl");
    }
}
