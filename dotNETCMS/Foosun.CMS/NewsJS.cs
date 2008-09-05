//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By Jiang.Dong                      ==
//===========================================================
using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Foosun.Model;
using Foosun.DALFactory;

namespace Foosun.CMS
{
    public class NewsJS
    {
        private INewsJS dal;
        public NewsJS()
        {
            dal = DataAccess.CreateNewsJS();
        }
        public IList<NewsJSInfo> GetPage(int PageIndex, int PageSize, out int RecordCount, out int PageCount, int JsType)
        {
            return dal.GetPage(PageIndex, PageSize, out RecordCount, out PageCount, JsType);
        }
        public void Delete(string id)
        {
            dal.Delete(id);
        }
        public DataTable GetJSFilePage(int PageIndex, int PageSize, out int RecordCount, out int PageCount, int id)
        {
            return dal.GetJSFilePage(PageIndex, PageSize, out  RecordCount, out  PageCount, id);
        }
        public DataTable GetJSFiles(string jsid)
        {
            return dal.GetJSFiles(jsid);
        }
        //<--修改者：吴静岚 时间：2008-06-24 解决自由JS调用新闻条数不受限
        /// <summary>
        /// 获取JS调用新闻条数
        /// </summary>
        /// <param name="jsid">js编号</param>
        /// <returns>查询结果</returns>
        public DataTable GetJSNum(string jsid)
        {
            return dal.GetJSNum(jsid);
        }
        //wjl-->
        public void RemoveNews(int id)
        {
            dal.RemoveNews(id);
        }
        public NewsJSInfo GetSingle(int id)
        {
            return dal.GetSingle(id);
        }
        public NewsJSInfo GetSingle(string JsID)
        {
            return dal.GetSingle(JsID);
        }
        public void Update(NewsJSInfo info)
        {
            dal.Update(info);
            EstablishJsFile(info);
        }
        public void Add(NewsJSInfo info)
        {
            info.JsID = dal.Add(info);
            EstablishJsFile(info);
        }
        public void EstablishJsFile(NewsJSInfo info)
        {
            string JsContent = "";
            string TmpContent = GetJsTmpContent(info.JsTempletID);
            if (TmpContent.Trim() != "")
            {
                if (info.jsType == 0)
                {
                    #region 系统JS
                    Foosun.Publish.CommonData.Initialize();
                    Foosun.Publish.LabelMass JsLbl = new Foosun.Publish.LabelMass(TmpContent, "", "", "", 0, 0, 0, 0);
                    JsLbl.TemplateType = Foosun.Publish.TempType.News;
                    JsLbl.ParseContent();
                    JsContent = JsLbl.Analyse_List(null, null);
                    #endregion 系统JS
                }
                else
                {
                    #region 自由JS
                    DataTable dt = GetJSFiles(info.JsID);
                    //int NewsID=0;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Foosun.Publish.CommonData.Initialize();
                        //<--修改者：吴静岚 时间：2008-06-24 解决自由JS调用新闻条数不受限
                        DataTable dts = GetJSNum(info.JsID);
                        int newNum = int.Parse( dts.Rows[0][0].ToString());
                        int tempI = 0;
                        //wjl-->
                        foreach (DataRow r in dt.Rows)
                        {
                            Foosun.Publish.LabelMass JsLbl = new Foosun.Publish.LabelMass(TmpContent, "", "", r["NewsId"].ToString(), 0, 0, 0, 0);
                            JsLbl.TemplateType = Foosun.Publish.TempType.News;
                            //bug修改，自由JS处理不应该ParseContent,by arjun 2006-6-17
                            //bug修改,修改截取字符串长度参数,by 周峻平 2008-5-28
                            JsContent += JsLbl.Analyse_ReadNews(0, info.jsLenTitle, info.jsLenContent, info.jsLenNavi, TmpContent, "", 1, 1, 0);
                            //<--修改者：吴静岚 时间：2008-06-24 解决自由JS调用新闻条数不受限
                            tempI++;
                            if (tempI == newNum)
                            {
                                break;
                            }
                            //wjl-->
                        }
                    }
                    #endregion 自由JS
                }
            }
            string JsPath = info.jssavepath;
            if (JsPath.Substring(JsPath.Length - 1, 1) != "\\")
            {
                JsPath += "\\";
            }
            if (JsPath.Substring(0, 1) == "/")
            {
                JsPath = JsPath.Substring(1);
            }
            JsPath = Foosun.Common.ServerInfo.GetRootPath() + "\\" + JsPath;
            if (!Directory.Exists(JsPath))
            {
                Directory.CreateDirectory(JsPath);
            }
            string FileName = JsPath + info.jsfilename + ".js";
            using (StreamWriter sw = new StreamWriter(FileName, false))
            {
                string FileContent = "document.write('";
                FileContent += JsContent.Replace("'","\'").Replace("\n","");
                FileContent += "');";
                sw.Write(FileContent.Replace("\r", "").Replace("1 ", ""));
            }
        }
        private string GetJsTmpContent(string jstmpid)
        {
            return dal.GetJsTmpContent(jstmpid);
        }
    }
}
