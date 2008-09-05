//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By Y.xiaoBin                       ==
//===========================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Foosun.CMS.Common
{
    public class SystemInfo
    {
        public static string GetRootURI()
        {
            string AppPath = "";
            string UrlAuthority = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            if (HttpContext.Current.Request.ApplicationPath == "/")
                //直接安装在   Web   站点   
                AppPath = UrlAuthority;
            else
                //安装在虚拟子目录下   
                AppPath = UrlAuthority + HttpContext.Current.Request.ApplicationPath;
            return AppPath;
        }
    }
}
