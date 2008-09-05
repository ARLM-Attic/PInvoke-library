using System;
using System.Web;
using System.Text;
using Foosun.Model;
using System.Security.Cryptography;
using System.IO;

namespace Foosun.Global
{
    public class Current
    {
        private SymmetricAlgorithm mobjCryptoService;
        private string Key;
        public Current()
        {
            mobjCryptoService = new RijndaelManaged();
            Key = "Guz(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87jH7";
        }
        public static string SiteID
        {
            get
            {
                try
                {
                    return GetInfo().SiteID.Trim();
                }
                catch
                {
                    return "0";
                }
            }
        }
        public static string UserNum
        {
            get
            {
                try
                {
                    return GetInfo().UserNum;
                }
                catch
                {
                    string url = "../" + Foosun.Config.UIConfig.dirUser + "/index.aspx";
                    System.Web.HttpContext.Current.Response.Redirect(url, true);
                }
                return GetInfo().UserNum;
            }
        }

        public static string adminLogined
        {
            get
            {
                return GetInfo().adminLogined;
            }
        }

        public static string UserName
        {
            get
            {
                return GetInfo().UserName;
            }
        }

        public static string ClientIP
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
        }

        public static bool IsTimeout()
        {
            try
            {
                GlobalUserInfo info = GetInfo();
                if (info != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return true;
            }
        }

        public static void Set(GlobalUserInfo info)
        {
            //创建一个cookie对象实例 , 不设置cookie时间,表式为会话cookie
            HttpCookie cookie = new HttpCookie("SITEINFO");
            //设置值
            string userInfo = info.UserNum + "," + info.UserName + "," + info.SiteID + "," + info.adminLogined + "," + info.uncert;
            //将数据加密
            Current current = new Current();
            string strEncrypto = current.Encrypto(userInfo);
            //将加密后的数据设置给cookie
            cookie.Value = strEncrypto;
            //将cookie发送发服务器
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        private static GlobalUserInfo GetInfo()
        {
            //查看用户是否存在
            if (HttpContext.Current.Request.Cookies["SITEINFO"] == null)
            {
                throw new Exception("您没有登录系统或会话已过期,请重新登录");
            }
            else
            {
                //得到用户数据
                HttpCookie cook = HttpContext.Current.Request.Cookies["SITEINFO"];
                //取值
                string userInfo = cook.Value;
                //将数据解密
                Current current = new Current();
                string strDecrypto = current.Decrypto(userInfo);
                //截取
                string[] strInfo = strDecrypto.Split(',');
                //创建GlobalUserInfo对象并赋值
                GlobalUserInfo globalUserInfo = new GlobalUserInfo(strInfo[0], strInfo[1], strInfo[2], strInfo[3]);
                //返回GlobalUserInfo对象
                return globalUserInfo;
            }
        }

        #region 数据加密解密
        /// <summary>
        /// 获得密钥
        /// </summary>
        /// <returns>密钥</returns>
        private byte[] GetLegalKey()
        {
            string sTemp = Key;
            mobjCryptoService.GenerateKey();
            byte[] bytTemp = mobjCryptoService.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
                sTemp = sTemp.Substring(0, KeyLength);
            else if (sTemp.Length < KeyLength)
                sTemp = sTemp.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        /// <summary>
        /// 获得初始向量IV
        /// </summary>
        /// <returns>初试向量IV</returns>
        private byte[] GetLegalIV()
        {
            string sTemp = "E4ghj*Ghg7!rNIfb&95GUY86GfghUb#er57HBh(u%g6HJ($jhWk7&!hg4ui%$hjk";
            mobjCryptoService.GenerateIV();
            byte[] bytTemp = mobjCryptoService.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
                sTemp = sTemp.Substring(0, IVLength);
            else if (sTemp.Length < IVLength)
                sTemp = sTemp.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="Source">待加密的串</param>
        /// <returns>经过加密的串</returns>
        private string Encrypto(string Source)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
            MemoryStream ms = new MemoryStream();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            byte[] bytOut = ms.ToArray();
            return Convert.ToBase64String(bytOut);
        }
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="Source">待解密的串</param>
        /// <returns>经过解密的串</returns>
        private string Decrypto(string Source)
        {
            byte[] bytIn = Convert.FromBase64String(Source);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        #endregion 数据加密解密
    }
}