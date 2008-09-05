//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By JiangDong                       ==
//===========================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Foosun.CMS.Collect
{
     public class Page
     {
        protected string _Url = "";
         protected string _Encode = "utf-8";
        protected string _Doc = "";
        protected string _Error = "";
        public Page(string url)
        {
            _Url = url;
        }
        public Page(string url,string encode)
        {
            _Url = url;
            _Encode = encode;
        }
        public bool Fetch()
        {
            bool flag = false;
            try
            {
                Uri url = new Uri(_Url);
                _Doc = Utility.GetPageContent(url, _Encode);
                flag = true;
            }
            catch(UriFormatException e)
            {
                _Error = e.ToString();
            }
            catch (System.Net.WebException e)
            {
                _Error = e.ToString();
            }
            catch (Exception e)
            {
                _Error = e.ToString();
            }
            return flag;
        }
        public string LastError
        {
            get { return _Error; }
        }
    }
}
