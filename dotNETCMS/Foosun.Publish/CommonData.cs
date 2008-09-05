using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Foosun.Config;
using Foosun.DALFactory;
using Foosun.Model;

namespace Foosun.Publish
{
    public class CommonData
    {
        public static Foosun.DALFactory.IPublish DalPublish = DataAccess.CreatePublish();
        public static IList<PubClassInfo> NewsClass = new List<PubClassInfo>();
        public static IList<PubSpecialInfo> NewsSpecial = new List<PubSpecialInfo>();
        public static IList<PubCHClassInfo> CHClass = new List<PubCHClassInfo>();
        public static IList<PubCHSpecialInfo> CHSpecial = new List<PubCHSpecialInfo>();
        /// <summary>
        /// 取得网站域名的根目录(绝对路径及相对路径)
        /// </summary>
        public static string SiteDomain;

        public static string getUrl()
        {
            string flg = "";
            string dirdumm = Foosun.Config.UIConfig.dirDumm;

            if (dirdumm.Trim() != string.Empty)
            {
                dirdumm = "/" + dirdumm;
            }
            string linkType = Foosun.Common.Public.readparamConfig("linkTypeConfig");
            string sitedomain = Foosun.Common.Public.readparamConfig("siteDomain");
            if (linkType == "1")
            {
                if (sitedomain.IndexOf("http://") > -1) { flg = sitedomain + dirdumm; }
                else {
                    flg = "http://" + sitedomain;
                    if (Foosun.Common.ServerInfo.ServerPort != "80")
                    {
                        flg += ":" + Foosun.Common.ServerInfo.ServerPort;
                    }
                    flg += dirdumm;
                }
            }
            else { flg = dirdumm; }
            //rd.Close();
            return flg;
        }
        /// <summary>
        /// 初始化数据函数
        /// </summary>
        /// <param name="cn"></param>
        public static void Initialize()
        {
            SiteDomain = getUrl();
            NewsClass.Clear();
            NewsSpecial.Clear();
            NewsClass = CommonData.DalPublish.GetClassList();
            CHClass = CommonData.DalPublish.GetCHClassList();
            NewsSpecial = CommonData.DalPublish.GetSpecialList();
            CHSpecial = CommonData.DalPublish.GetCHSpecialList();
        }
        /// <summary>
        /// 取得栏目的相关信息,必须是没有放在回收站中的
        /// </summary>
        //public static PubClassInfo GetClassById(string classid)
        //{
        //    foreach (PubClassInfo cl in NewsClass)
        //    {
        //        if (cl.ClassID.Equals(classid))
        //            return cl;
        //    }
        //    return null;
        //}
        /// <summary>
        /// 获取栏目信息
        /// </summary>
        /// <param name="ClassId">栏目ID</param>
        /// <returns></returns>
        public static PubClassInfo GetClassById(string ClassId)
        {
            return CommonData.DalPublish.GetClassById(ClassId);
        }
        /// <summary>
        /// 取得频道栏目的相关信息
        /// </summary>
        public static PubCHClassInfo GetCHClassById(int ID)
        {
            foreach (PubCHClassInfo ccl in CHClass)
            {
                if (ccl.Id.Equals(ID))
                    return ccl;
            }
            return null;
        }

        /// <summary>
        /// 取得专题的相关信息,必须是没有放在回收站中的
        /// </summary>
        public static PubSpecialInfo GetSpecial(string specialid)
        {
            //foreach (PubSpecialInfo sp in NewsSpecial)
            //{
            //    if (sp.SpecialID.Equals(specialid))
            //        return sp;
            //}
            //return null;
            return CommonData.DalPublish.GetSpecial(specialid);
        }

        /// <summary>
        /// 取得专题的相关信息,必须是没有放在回收站中的
        /// </summary>
        public static PubCHSpecialInfo GetCHSpecial(int ID)
        {
            foreach (PubCHSpecialInfo sp in CHSpecial)
            {
                if (sp.Id.Equals(ID))
                    return sp;
            }
            return null;
        }

        public static string getNewsURLFormID(string NewsID, string DataLib)
        {
            string URLSTR = "";
            string ReadType = Foosun.Common.Public.readparamConfig("ReviewType");
            IDataReader rd = DalPublish.GetNewsInfoAndClassInfo(NewsID, DataLib);
            if (rd.Read())
            {
                if (rd["NewsType"].ToString() == "2")
                {
                    URLSTR = rd["URLaddress"].ToString();
                }
                else
                {
                    if (rd["isDelPoint"].ToString() != "0")
                    {
                        URLSTR = "/Content.aspx?id=" + NewsID;
                    }
                    else
                    {
                        if (ReadType == "1")
                        {
                            URLSTR = "/Content.aspx?id=" + NewsID;
                        }
                        else
                        {
                            URLSTR = rd["SavePath1"].ToString() + "/" + rd["SaveClassframe"] + "/" + rd["SavePath"].ToString() + "/" + rd["FileName"].ToString() + rd["FileEXName"].ToString();
                        }
                    }
                    URLSTR = URLSTR.Replace("//", "/");
                    URLSTR = getUrl() + URLSTR;
                }
            }
            rd.Close();
            return URLSTR;
        }

        /// <summary>
        /// 取得栏目访问地址
        /// </summary>
        /// <param name="SavePath">栏目保存路径</param>
        /// <param name="ClassSaveRule">栏目保存规则</param>
        /// <returns>返回访问地址</returns>
        public static string getClassURL(string Domain,int isDelPoint, string ClassID, string SavePath, string SaveClassframe, string ClassSaveRule)
        {
            CommonData.Initialize();
            string tmstr = "";
            if (Domain.Length > 5)
            {
                if (Foosun.Common.Public.readparamConfig("ReviewType") == "1")
                {
                    tmstr = "/list.aspx?id=" + ClassID;
                    return CommonData.SiteDomain + tmstr.Replace("//", "/");
                }
                else
                {
                    if (isDelPoint != 0)
                    {
                        tmstr = "/list.aspx?id=" + ClassID;
                        return CommonData.SiteDomain + tmstr.Replace("//", "/");
                    }
                    else
                    {
                        tmstr = "/" + ClassSaveRule;
                        return Domain + tmstr.Replace("//", "/");
                    }
                }
            }
            else
            {
                if (Foosun.Common.Public.readparamConfig("ReviewType") == "1")
                {
                    tmstr = "/list.aspx?id=" + ClassID;
                }
                else
                {
                    if (isDelPoint != 0)
                    {
                        tmstr = "/list.aspx?id=" + ClassID;
                    }
                    else
                    {
                        tmstr = "/" + SavePath + "/" + SaveClassframe + "/" + ClassSaveRule;
                    }
                }
                 return CommonData.SiteDomain + tmstr.Replace("//", "/");
            }
        }

        /// <summary>
        /// 取得新闻访问地址
        /// </summary>
        /// <param name="SavePath">新闻保存路径</param>
        /// <param name="FileName">新闻文件名</param>
        /// <param name="SaveClassframe">栏目存储新闻的规则</param>
        /// <param name="FileEXName">新闻文件名后缀</param>   
        /// <returns>返回新闻访问地址</returns>
        public static string getNewsURL(string isDelPoint, string NewsID, string SavePath, string SaveClassframe, string FileName, string FileEXName)
        {
            CommonData.Initialize();
            string str_temppath = "";
            if (Foosun.Common.Public.readparamConfig("ReviewType") == "1")
            {
                str_temppath = "/content.aspx?id=" + NewsID;
            }
            else
            {
                if (isDelPoint != "0")
                {
                    str_temppath = "/content.aspx?id=" + NewsID;
                }
                else
                {
                    str_temppath = "/" + SaveClassframe + "/" + SavePath + "/" + FileName + FileEXName;
                }
            }
            str_temppath = CommonData.SiteDomain + str_temppath.Replace("//", "/");
            return str_temppath;
        }

        /// <summary>
        /// 取得栏目访问地址
        /// </summary>
        /// <param name="ClassID">栏目编号</param>
        /// <returns>返回访问地址</returns>
        public static string getClassURL(string ClassID)
        {
            CommonData.Initialize();
            Foosun.Model.PubClassInfo pci = CommonData.GetClassById(ClassID);
            return CommonData.getClassURL(pci.Domain,pci.isDelPoint, pci.ClassID, pci.SavePath, pci.SaveClassframe, pci.ClassSaveRule);
        }
    }
}
