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
using System.IO;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Foosun.CMS
{
/// <summary>
/// Foosun 的摘要说明
/// </summary>
    public class FSSecurity
    {
        public FSSecurity()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="dit">1为加密码，其它数字为解密</param>
        /// <param name="strData">待加密/解密字符串</param>
        /// <param name="key">32位Key值</param>
        /// <returns>加密后的字符串</returns>
        public static string FDESEncrypt(string strData, int dit)
        {
            byte[] DESKey = new byte[] { 0x82, 0xBC, 0xA1, 0x6A, 0xF5, 0x87, 0x3B, 0xE6, 0x59, 0x6A, 0x32, 0x64, 0x7F, 0x3A, 0x2A, 0xBB, 0x2B, 0x68, 0xE2, 0x5F, 0x06, 0xFB, 0xB8, 0x2D, 0x67, 0xB3, 0x55, 0x19, 0x4E, 0xB8, 0xBF, 0xDD };
            if (dit == 1)
            {
                return DESEncrypt(strData, DESKey);
            }
            else
            {
                return DESDecrypt(strData, DESKey);
            }
        }


        /// <param name="strData">待加密字符串</param>
        /// <param name="key">32位Key值</param>
        public static string DESEncrypt(string strData, byte[] key)
        {
            SymmetricAlgorithm fs = Rijndael.Create();
            fs.Key = key;
            fs.Mode = CipherMode.ECB;
            fs.Padding = PaddingMode.ANSIX923;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, fs.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] byt = Encoding.Unicode.GetBytes(strData);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        //----------------DES解密方式-------------------------------
        /// <param name="key">32位Key值</param>
        /// <returns>解密后的字符串</returns>
        public static string DESDecrypt(string strData, byte[] key)
        {
            SymmetricAlgorithm fs = Rijndael.Create();
            fs.Key = key;
            fs.Mode = CipherMode.ECB;
            fs.Padding = PaddingMode.ANSIX923;
            ICryptoTransform ct = fs.CreateDecryptor();
            byte[] byt = Convert.FromBase64String(strData);
            MemoryStream ms = new MemoryStream(byt);
            CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs, Encoding.Unicode);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 序列号控制
        /// </summary>
        /// <param name="snStr"></param>
        /// <param name="userWebsite"></param>
        public static void userControls(string snStr, string userWebsite)
        {


        }

        /// <summary>
        /// 版权所有
        /// </summary>
        /// <param name="VersionNUm"></param>
        /// <returns></returns>
        public static string CopyRightInfo(int VersionNUm)
        {
            string CrI = "<span style=\"line-height:20px;font-size:11.5px;font-family:Arial;\">" + Foosun.Config.UIConfig.copyright + "</span>";
            if (VersionNUm != 999)
            {
                CrI += "<br />" + gCopyRightInfo();
            }
            return CrI;
        }

        public static string gCopyRightInfo()
        {
            string isTryversion = Foosun.Config.verConfig.isTryversion;
            string helpcenterStr =Foosun.Config.verConfig.helpcenterStr;
            string ForumStr = Foosun.Config.verConfig.ForumStr;
            string CrI = isTryversion + ",&nbsp;&nbsp;" + helpcenterStr + ",&nbsp;&nbsp;" + ForumStr + "";
            return CrI;
        }
    }
}
