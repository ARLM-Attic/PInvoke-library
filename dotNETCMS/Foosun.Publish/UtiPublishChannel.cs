using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Foosun.Common;
using Foosun.Control;
using Foosun.Config;
using Foosun.Model;

namespace Foosun.Publish
{
    public partial class UltiPublish
    {
        /// <summary>
        /// 是否发布主页、新闻、栏目、专题，bool标志
        /// </summary>
        private bool fs_isCHPubindex;
        public bool IsCHPublishIndex
        {
            set { fs_isCHPubindex = value; }
        }

        /// <summary>
        /// 是否发布新闻，bool标志
        /// </summary>
        private bool fs_isCHPubNews;
        public bool IsCHPubNews
        {
            set { fs_isCHPubNews = value; }
        }

        /// <summary>
        /// 是否发布栏目，bool标志
        /// </summary>
        private bool fs_isCHPubClass;
        public bool IsCHPubClass
        {
            set { fs_isCHPubClass = value; }
        }
        /// <summary>
        /// 是否发布专题，bool标志
        /// </summary>
        private bool fs_isCHPubSpecial;
        public bool IsCHPubSpecial
        {
            set { fs_isCHPubSpecial = value; }
        }

        //-----------------------------------------------------------------
        /// <summary>
        /// 发布频道新闻方式标志
        /// </summary>
        private int fs_CHnewsFlag;
        public int CHnewsFlag
        {
            set { fs_CHnewsFlag = value; }
        }
        /// <summary>
        /// 发布频道新闻的字符串参数
        /// </summary>
        private string fs_strNewsCHParams = string.Empty;
        public string strNewsCHParams
        {
            set { fs_strNewsCHParams = value; }
        }
        /// <summary>
        /// 发布频道栏目方式标志
        /// </summary>
        private int fs_ClassCHFlag;
        public int ClassCHFlag
        {
            set { fs_ClassCHFlag = value; }
        }
        /// <summary>
        /// 发布频道栏目的字符串参数
        /// </summary>
        private string fs_strClassCHParams = string.Empty;
        public string strClassCHParams
        {
            set { fs_strClassCHParams = value; }
        }
        /// <summary>
        /// 发布频道专题方式标志
        /// </summary>
        private int fs_CHspecialFlag;
        public int CHspecialFlag
        {
            set { fs_CHspecialFlag = value; }
        }
        /// <summary>
        /// 发布频道专题的字符串参数
        /// </summary>
        private string fs_strCHSpecialParams = string.Empty;
        public string strCHSpecialParams
        {
            set { fs_strCHSpecialParams = value; }
        }

        /// <summary>
        /// 取得频道ID
        /// </summary>
        private int fs_ChID;
        public int intCHID
        {
            set { fs_ChID = value; }
            get { return fs_ChID; }
        }

        /// <summary>
        /// 取得频道表
        /// </summary>
        private string fs_Table;
        public string strCHTalbe
        {
            set { fs_Table = value; }
            get { return fs_Table; }
        }

        /// <summary>
        /// 要发布频道的新闻条数
        /// </summary>
        private int nCHNewsCount = 0;
        /// <summary>
        /// 要发布频道的栏目数
        /// </summary>
        private int nCHClassCount = 0;
        /// <summary>
        /// 要发布频道的专题数
        /// </summary>
        private int nCHSpecialCount = 0;
        /// <summary>
        /// 生成每一条频道新闻的路径
        /// </summary>
        private string saveinfoPath = string.Empty;

        private string saveCHClassPath = string.Empty;

        //-----------------------------------------------------------------
        /// <summary>
        /// 开始发布频道
        /// </summary>
        public void StartCHPublish()
        {
            CommonData.Initialize();
            HProgressBar.Start();
            if (fs_isCHPubindex) { ultiPublishCHIndex(); }
            if (fs_isCHPubNews) { ultiPublishCHNews(); }
            if (fs_isCHPubClass) { ultiPublishCHClass(); }
            if (fs_isCHPubSpecial) { ultiPublishCHSpecial(); }
        }

        /// <summary>
        /// 生成频道主页
        /// </summary>
        private void ultiPublishCHIndex()
        {
            try
            {
                HProgressBar.Roll("正在频道发布主页", 0);
                string indexname = "index.html";
                TempletPath = "/" + Foosun.Config.UIConfig.dirTemplet + "/" + Foosun.Common.Public.readCHparamConfig("channeltemplet", fs_ChID);
                TempletPath = TempletPath.Replace("//", "/");
                TempletPath = TempletPath.Replace("/", "\\");
                TempletPath = TempletPath.ToLower().Replace("{@dirtemplet}", strTempletDir);
                indexname = Foosun.Common.Public.readCHparamConfig("channelindexname", fs_ChID);
                Template indexTemp = new Template(SiteRootPath.Trim('\\') + TempletPath, TempType.ChIndex);
                indexTemp.ChID = fs_ChID;
                indexTemp.GetHTML();
                indexTemp.ReplaceLabels();
                string SaveIndexPath = Foosun.Common.Public.readCHparamConfig("channelindexpath", fs_ChID);
                SaveIndexPath = SaveIndexPath.Replace("//", "/").Replace("{@dirHTML}", Foosun.Config.UIConfig.dirHtml);
                SaveIndexPath = SaveIndexPath.Replace("/", "\\");
                General.WriteHtml(indexTemp.FinallyContent, SiteRootPath.TrimEnd('\\') + "\\" + SaveIndexPath);
                string dimm = Foosun.Config.UIConfig.dirDumm;
                if (dimm.Trim() != string.Empty){dimm = "/" + dimm;}
                HProgressBar.Roll("发布频道主页成功。&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a> &nbsp; <a class=\"list_link\" href=\"" + (dimm + "/" + SaveIndexPath.Replace("\\", "/")).Replace("//", "/") + "\" target=\"_blank\">浏览首页</a>", 100);
            }
            catch (Exception ex)
            {
                Foosun.Common.Public.savePublicLogFiles("□□□发布主页", "【错误描述：】\r\n" + ex.ToString(),"");
                HProgressBar.Roll("发布频道主页失败。<a href=\"../publish/error/geterror.aspx?\">查看日志</a>", 0);
            }
        }

        /// <summary>
        /// 生成频道所有信息
        /// </summary>
        private void ultiPublishCHNews()
        {
            using (IDataReader rd = getALLCHNews(fs_Table))
            {
                int num = 0;
                int succeedNum = 0;
                int failedNum = 0;
                bool flag = false;
                if (nCHNewsCount > 0)
                {
                    while (rd.Read())
                    {
                        flag = true;
                        TempletPath = rd["Templet"].ToString();
                        TempletPath = TempletPath.Replace("/", "\\");
                        TempletPath = TempletPath.ToLower().Replace("{@dirtemplet}", strTempletDir);
                        setSaveinfoPath(int.Parse(rd["classID"].ToString()), rd["SavePath"].ToString(), rd["FileName"].ToString());
                        bool state = publishSingleCHNews(int.Parse(rd["id"].ToString()), fs_Table, int.Parse(rd["classID"].ToString()));//发布一条新闻，返回成功与否                                              
                        num += 1;
                        if (state)
                        {
                            succeedNum++;
                        }
                        else
                        {
                            failedNum++;
                        }
                        string msg = string.Format(MsgFormat, "信息", nCHNewsCount.ToString(), ((int)num).ToString(), succeedNum.ToString(), failedNum.ToString(), "发布频道信息成功。&nbsp;&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a>");
                        HProgressBar.Roll(msg, (num * 100 / nCHNewsCount));
                    }
                }
                else
                {
                    HProgressBar.Roll("频道没有信息", 0);
                }
                if (!flag)
                {
                    string msg = string.Format(MsgFormat, "信息", nCHNewsCount.ToString(), ((int)num).ToString(), succeedNum.ToString(), failedNum.ToString(), "发布频道信息成功。&nbsp;&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a>");
                    if (nCHNewsCount > 0)
                    {
                        HProgressBar.Roll(msg, (num * 100 / nCHNewsCount));
                    }
                    else
                    {
                        HProgressBar.Roll("频道没有信息", 0);
                    }
                }
            }
            if (templateList.Count != 0)
            {
                templateList.Clear();
            }
            if (failedList.Count != 0)
            {
                for (int i = 0; i < failedList.Count; i++)
                {
                    Foosun.Common.Public.savePublicLogFiles("□□□发布频道信息", "【ID】:" + failedList[i].Split('$')[0] + "\r\n【错误描述：】\r\n" + failedList[i].Split('$')[1],"");
                }
                failedList.Clear();
            }
            if (succeedList.Count != 0)
            {
                //这里执行存储过程，暂时屏蔽
                updateCHNewsIsHtml(fs_Table, "isHtml", "id");
            }
        }

        /// <summary>
        /// 设置已发布新闻、栏目的IsHtml
        /// </summary>
        private void updateCHNewsIsHtml(string tableName, string isHtml, string idField)
        {
            try
            {
                CommonData.DalPublish.UpdateCHNewsIsHtml(tableName, isHtml, idField, succeedList);
            }
            catch
            {
                //写日志
            }
            finally
            {
                succeedList.Clear();
            }
        }

        /// <summary>
        /// 发布一条频道信息
        /// </summary>
        /// <returns></returns>
        public bool publishSingleCHNews(int ID,string DTable,int ClassID)
        {
            try
            {
                TempletPath = SiteRootPath.Trim('\\') + TempletPath;
                bool existFlag = false;
                if (templateList.Count != 0)
                {
                    foreach (Template temp in templateList)
                    {
                        if (TempletPath == temp.TempFilePath)
                        {
                            existFlag = true;
                            temp.CHNewsID = ID;
                            temp.ChID = fs_ChID;
                            makeHtmlFile_channel(temp, existFlag, ClassID, ID);
                            break;
                        }
                    }
                    if (!existFlag)
                    {
                        Template newsTemplate = new Template(TempletPath, TempType.ChNews);
                        newsTemplate.CHNewsID = ID;
                        newsTemplate.ChID = fs_ChID;
                        newsTemplate.GetHTML();
                        makeHtmlFile_channel(newsTemplate, existFlag, ClassID, ID);
                    }
                }
                else
                {
                    Template newsTemplate = new Template(TempletPath, TempType.ChNews);
                    newsTemplate.CHNewsID = ID;
                    newsTemplate.ChID = fs_ChID;
                    newsTemplate.GetHTML();
                    makeHtmlFile_channel(newsTemplate, existFlag, ClassID, ID);
                }
                succeedList.Add(ID.ToString());
                return true;
            }
            catch (Exception e)
            {
                //failedList.Add(newsID + "$" + e.Message);
                Foosun.Common.Public.savePublicLogFiles("□□□发布新闻", "【ID】:" + ClassID + "\r\n【错误描述：】\r\n" + e.ToString(),"");
                return false;
            }
        }

        /// <summary>
        /// 生成频道所有栏目
        /// </summary>
        public void ultiPublishCHClass()
        {
            using (IDataReader rd = getAllCHClass())
            {
                int num = 0;
                int succeedNum = 0;
                int failedNum = 0;
                bool HasRows = false;
                if (nCHClassCount > 0)
                {
                    while (rd.Read())
                    {
                        HasRows = true;
                        TempletPath = rd["Templet"].ToString();
                        TempletPath = TempletPath.Replace("/", "\\");
                        TempletPath = TempletPath.ToLower().Replace("{@dirtemplet}", strTempletDir);
                        string dirHTML = Foosun.Common.Public.readCHparamConfig("htmldir", fs_ChID);
                        dirHTML = dirHTML.Replace("{@dirHTML}", Foosun.Config.UIConfig.dirHtml);
                        string TmpsaveClassPath = "\\" + dirHTML.Trim('\\').Trim('/') + "\\" + rd["savePath"].ToString().Trim('\\').Trim('/')+"\\" + rd["FileName"].ToString().Trim('\\').Trim('/');
                        saveClassPath = TmpsaveClassPath.Replace("/", "\\");
                        int intClassId = int.Parse(rd["ID"].ToString());
                        bool state = publishSingleCHClass(intClassId);
                        num += 1;
                        if (state)
                        {
                            succeedNum++;
                        }
                        else
                        {
                            failedNum++;
                        }
                        string msg = string.Format(MsgFormat, "栏目", nCHClassCount.ToString(), ((int)num).ToString(), succeedNum.ToString(), failedNum.ToString(), "发布栏目成功&nbsp;&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a>");
                        HProgressBar.Roll(msg, (num * 100 / nCHClassCount));
                    }
                }
                else
                {
                    HProgressBar.Roll("没有栏目", 0);
                }
                if (!HasRows)
                {
                    string msg = string.Format(MsgFormat, "栏目", nCHClassCount.ToString(), ((int)num).ToString(), succeedNum.ToString(), failedNum.ToString(), "发布栏目成功&nbsp;&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a>&nbsp;&nbsp;<a class=\"list_link\" href=\"../../index.html\" target=\"_blank\">浏览首页</a>");
                    if (nClassCount > 0)
                    {
                        HProgressBar.Roll(msg, (num * 100 / nCHClassCount));
                    }
                    else
                    {
                        HProgressBar.Roll("没有栏目", 0);
                    }
                }
            }
            if (templateList.Count != 0)
            {
                templateList.Clear();
            }
            if (failedList.Count != 0)
            {
                for (int i = 0; i < failedList.Count; i++)
                {
                    Foosun.Common.Public.savePublicLogFiles("□□□发布栏目", "【ID】:" + failedList[i].Split('$')[0] + "\r\n【错误描述：】\r\n" + failedList[i].Split('$')[1],"");
                }
                failedList.Clear();
            }
        }

        /// <summary>
        /// 发布一个栏目(频道)
        /// </summary>
        public bool publishSingleCHClass(int classID)
        {
            try
            {
                TempletPath = SiteRootPath.Trim('\\') + TempletPath;
                bool existFlag = false;
                if (templateList.Count != 0)
                {
                    foreach (Template temp in templateList)
                    {
                        if (TempletPath == temp.TempFilePath)
                        {
                            existFlag = true;
                            temp.CHClassID = classID;
                            temp.ChID = fs_ChID;
                            replaceTemp(temp, existFlag, saveClassPath, classID.ToString(), "class");
                            break;
                        }
                    }
                    if (!existFlag)
                    {
                        Template classTemplate = new Template(TempletPath, TempType.ChClass);
                        classTemplate.CHClassID = classID;
                        classTemplate.ChID = fs_ChID;
                        classTemplate.GetHTML();
                        replaceTemp(classTemplate, existFlag, saveClassPath, classID.ToString(), "class");
                    }
                }
                else
                {
                    Template classTemplate = new Template(TempletPath, TempType.ChClass);
                    classTemplate.CHClassID = classID;
                    classTemplate.ChID = fs_ChID;
                    classTemplate.GetHTML();
                    replaceTemp(classTemplate, existFlag, saveClassPath, classID.ToString(), "class");
                }
                succeedList.Add(classID.ToString());
                return true;
            }
            catch (Exception e)
            {
                failedList.Add(classID + "$" + e.Message);
                Foosun.Common.Public.savePublicLogFiles("□□□发布栏目", "【ID】:" + classID + "\r\n【错误描述：】\r\n" + e.ToString(),"");
                return false;
            }
        }

        /// <summary>
        /// 得到频道要发布的所有栏目
        /// </summary>
        protected IDataReader getAllCHClass()
        {
            switch (fs_ClassFlag)
            {
                case 0: return getCHClassesAll();
                case 1: return getCHClassesSelect();
                default: return null;
            }
        }

        /// <summary>
        ///  选择发布所有栏目时，取得所有栏目
        /// </summary>
        /// <param name="strClassParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有栏目的查询结果</returns>
        protected IDataReader getCHClassesAll()
        {
            return CommonData.DalPublish.GetPublishCHClass("", fs_ChID, out nCHClassCount);
        }
        /// <summary>
        /// 选择发选择栏目时，取得所有栏目
        /// </summary>        
        /// <returns>取得符合条件的所有栏目的查询结果</returns>
        protected IDataReader getCHClassesSelect()
        {
            if (fs_strClassParams != null)
            {
                string classid = "";
                string[] classparm = fs_strClassCHParams.Split('$');
                for (int i = 0; i < classparm.Length; i++)
                {
                    if (i > 0)
                    {
                        classid += ",";

                    }
                    classid += "" + classparm[i] + "";
                }
                return CommonData.DalPublish.GetPublishCHClass(classid, fs_ChID, out nCHClassCount);
            }
            else
            {
                HProgressBar.Roll("请选择栏目", 0);
                return null;
            }
        }

        /// <summary>
        /// 生成频道所有专题
        /// </summary>
        public void ultiPublishCHSpecial()
        {
            float num = 0;
            int succeedNum = 0;
            int failedNum = 0;
            bool HasRows = false;
            IDataReader rd = getAllCHSpecials();
            {
                if (nCHSpecialCount > 0)
                {
                    while (rd.Read())
                    {
                        HasRows = true;
                        TempletPath = rd["Templet"].ToString();
                        TempletPath = TempletPath.Replace("/", "\\");
                        TempletPath = TempletPath.ToLower().Replace("{@dirtemplet}", strTempletDir);
                        string dirHTML = Foosun.Common.Public.readCHparamConfig("htmldir", fs_ChID);
                        dirHTML = dirHTML.Replace("{@dirHTML}", Foosun.Config.UIConfig.dirHtml);
                        string TmpsaveSpecialPath = "\\" + dirHTML.Trim('\\').Trim('/') + "\\" + rd["SavePath"].ToString().Trim('\\').Trim('/') + '\\' + rd["FileName"].ToString().Trim('\\').Trim('/');
                        saveSpecialPath = (TmpsaveSpecialPath.Replace("/", "\\"));
                        bool state = publishSingleCHSpecial(int.Parse(rd["ID"].ToString()));
                        if (state)
                        {
                            num += 1;
                            succeedNum++;
                        }
                        else
                        {
                            failedNum++;
                        }
                        string msg = string.Format(MsgFormat, "专题", nCHSpecialCount.ToString(), ((int)num).ToString(), succeedNum.ToString(), failedNum.ToString(), "发布栏目成功。&nbsp;&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a> &nbsp;&nbsp;&nbsp; <a class=\"list_link\" href=\"../../index.html\" target=\"_blank\">浏览首页</a>");
                        HProgressBar.Roll(msg, (int)(num / nCHSpecialCount) * 100);
                    }
                    rd.Close();
                }
                else
                {
                    HProgressBar.Roll("没有专题", 0);
                }
                rd.Close();
                if (!HasRows)
                {
                    string msg = string.Format(MsgFormat, "专题", nCHSpecialCount.ToString(), ((int)num).ToString(), succeedNum.ToString(), failedNum.ToString(), "发布栏目成功。&nbsp;&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a> &nbsp;&nbsp;&nbsp; <a class=\"list_link\" href=\"../../index.html\" target=\"_blank\">浏览首页</a>");
                    HProgressBar.Roll(msg, (int)(num / nCHSpecialCount) * 100);
                }
            }
            if (templateList.Count != 0)
            {
                templateList.Clear();
            }
            if (failedList.Count != 0)
            {
                for (int i = 0; i < failedList.Count; i++)
                {
                    Foosun.Common.Public.savePublicLogFiles("□□□发布专题", "【ID】:" + failedList[i].Split('$')[0] + "\r\n【错误描述：】\r\n" + failedList[i].Split('$')[1],"");
                }
                failedList.Clear();
            }
        }

        public bool publishSingleCHSpecial(int specialID)
        {
            try
            {
                TempletPath = SiteRootPath.Trim('\\') + TempletPath;
                bool existFlag = false;
                if (templateList.Count != 0)
                {
                    foreach (Template temp in templateList)
                    {
                        if (TempletPath == temp.TempFilePath)
                        {
                            existFlag = true;
                            temp.CHSpecialID = specialID;
                            temp.ChID = fs_ChID;
                            replaceTemp(temp, existFlag, saveSpecialPath, specialID.ToString(), "special");
                            break;
                        }
                    }
                    if (!existFlag)
                    {
                        Template specialTemplate = new Template(TempletPath, TempType.Chspecial);
                        specialTemplate.CHSpecialID = specialID;
                        specialTemplate.ChID = fs_ChID;
                        specialTemplate.GetHTML();
                        replaceTemp(specialTemplate, existFlag, saveSpecialPath, specialID.ToString(), "special");
                    }
                }
                else
                {
                    Template specialTemplate = new Template(TempletPath, TempType.Chspecial);
                    specialTemplate.CHSpecialID = specialID;
                    specialTemplate.ChID = fs_ChID;
                    specialTemplate.GetHTML();
                    replaceTemp(specialTemplate, existFlag, saveSpecialPath, specialID.ToString(), "special");
                }
                succeedList.Add(specialID.ToString());
                return true;
            }
            catch (Exception e)
            {
                failedList.Add(specialID.ToString() + "$" + e.Message);
                return false;
            }
        }

        /// <summary>
        /// 得到要发布的所有专题(频道)
        /// </summary>
        protected IDataReader getAllCHSpecials()
        {
            string spid = string.Empty;
            switch (fs_CHspecialFlag)
            {
                case 0:
                    break;
                case 1:
                    spid = getCHSpecialsSelect();
                    break;
                default:
                    return null;
            }
            return CommonData.DalPublish.GetPublishCHSpecial(fs_ChID, spid, out nCHSpecialCount);
        }

        /// <summary>
        /// 选择选择专题时，取得所有专题
        /// </summary>        
        protected string getCHSpecialsSelect()
        {
            if (fs_strCHSpecialParams != null)
            {
                string reslt = "";
                string[] spparam = fs_strCHSpecialParams.Split('$');
                for (int i = 0; i < spparam.Length; i++)
                {
                    if (i > 0)
                        reslt += ",";
                    reslt += "" + spparam[i] + "";
                }
                return reslt;
            }
            else
            {
                HProgressBar.Roll("请选择频道专题", 0);
                return "";
            }
        }

        /// <summary>
        /// 得到要发布的频道新闻
        /// </summary>
        /// <param name="DTable"></param>
        /// <returns></returns>
        protected IDataReader getALLCHNews(string DTable)
        {
            switch (fs_CHnewsFlag)
            {
                case 0: return getCHNewsAll(DTable);
                case 1: return getCHNewsLast(DTable);
                case 2: return getCHNewsUnhtml(DTable);
                case 3: return getCHNewsClasses(DTable);
                case 4: return getCHNewsDate(DTable);
                case 5: return getCHNewsId(DTable);
                default: return null;
            }
        }

        /// <summary>
        /// 选择发布频道所有新闻时，取得所有新闻
        /// </summary> 
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        protected IDataReader getCHNewsAll(string DTable)
        {
            return CommonData.DalPublish.GetPublishCHNewsAll(DTable, out nCHNewsCount);
        }

        /// <summary>
        /// 选择发布频道最新时，取得所有新闻
        /// </summary>
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        protected IDataReader getCHNewsLast(string DTable)
        {
            int ncount;
            IDataReader rd = CommonData.DalPublish.GetPublishCHNewsLast(DTable,Convert.ToInt32(fs_strNewsCHParams), false, out ncount);
            nCHNewsCount = Convert.ToInt32(fs_strNewsCHParams);
            if (ncount < nCHNewsCount)
            {
                nCHNewsCount = ncount;
            }
            return rd;
        }
        /// <summary>
        /// 选择只发布频道未生成时，取得所有新闻
        /// </summary>
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        protected IDataReader getCHNewsUnhtml(string DTable)
        {
            int ncount;
            IDataReader rd = CommonData.DalPublish.GetPublishCHNewsLast(DTable, Convert.ToInt32(fs_strNewsCHParams), true, out ncount);
            nCHNewsCount = Convert.ToInt32(fs_strNewsCHParams);
            if (ncount < nCHNewsCount)
            {
                nCHNewsCount = ncount;
            }
            return rd;
        }
        /// <summary>
        /// 选择选择频道栏目时，取得所有新闻
        /// </summary>
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        protected IDataReader getCHNewsClasses(string DTable)
        {
            bool IsHtml = false;
            if (fs_strNewsCHParams.IndexOf("#") >= 0)
            {
                //只发布未发布的
                IsHtml = true;
                fs_strNewsCHParams = fs_strNewsCHParams.Replace("#", "");
            }
            if (fs_strNewsCHParams.IndexOf("&") >= 0)
            {
                fs_strNewsCHParams = fs_strNewsCHParams.Replace("&", "");
            }
            string classid = "";
            string[] PublishParam = fs_strNewsCHParams.Split('$');
            int n = PublishParam.Length;
            for (int i = 0; i < n - 1; i++)
            {
                if (i > 0)
                    classid += ",";
                classid = "'" + PublishParam[i] + "'";
            }
            return CommonData.DalPublish.GetPublishCHNewsByClass(DTable,classid, IsHtml, false, PublishParam[n - 1], out nCHNewsCount);
        }
        /// <summary>
        /// 选择按照频道日期发布时，取得所有新闻
        /// </summary>
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        protected IDataReader getCHNewsDate(string DTable)
        {
            DateTime StartTm = Convert.ToDateTime(fs_strNewsCHParams.Split('$')[0]);
            DateTime EndTm = Convert.ToDateTime(fs_strNewsCHParams.Split('$')[1]);
            return CommonData.DalPublish.GetPublishCHNewsByTime(DTable,StartTm, EndTm, out nCHNewsCount);
        }
        /// <summary>
        /// 选择按照ID频道发布时，取得所有新闻
        /// </summary>
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        protected IDataReader getCHNewsId(string DTable)
        {
            int MinID = Convert.ToInt32(fs_strNewsCHParams.Split('$')[0]);
            int MaxID = Convert.ToInt32(fs_strNewsCHParams.Split('$')[1]);
            return CommonData.DalPublish.GetPublishCHNewsByID(DTable,MinID, MaxID, out nCHNewsCount);
        }



        /// <summary>
        /// 为saveNewsPath赋值
        /// </summary>
        /// <param name="classID">新闻所属栏目的ID</param>
        /// <param name="SavePath">新闻表中SavePath字段的值</param>
        /// <param name="FileName">新闻表中FileName字段的值</param>
        /// <param name="FileName">新闻表中FileName字段的值</param>
        private void setSaveinfoPath(int classID, string SavePath, string FileName)
        {
            PubCHClassInfo info = CommonData.GetCHClassById(classID);
            if (info != null)
            {
                string dirHTML = Foosun.Common.Public.readCHparamConfig("htmldir", fs_ChID);
                dirHTML = dirHTML.Replace("{@dirHTML}", Foosun.Config.UIConfig.dirHtml);
                saveinfoPath =  "\\" + dirHTML.Trim('\\').Trim('/') + "\\" + info.SavePath.Trim('\\').Trim('/') + "\\" + SavePath.Trim('\\').Trim('/') + "\\" + FileName.Trim('\\').Trim('/');
            }
        }


        /// <summary>
        /// 处理频道模板
        /// </summary>
        /// <param name="tempRe">模板实例</param>
        /// <param name="existFlag">该模板是否存在于模板列表</param>
        protected void makeHtmlFile_channel(Template tempRe, bool existFlag, int classID, int infoID)
        {
            tempRe.ReplaceLabels();
            if (tempRe.MyTempType == TempType.ChNews)
            {
                string FinlContent = tempRe.FinallyContent;
                int pos1 = FinlContent.IndexOf("<!-FS:STAR=");
                int pos2 = FinlContent.IndexOf("FS:END->");
                if (pos1 > -1)
                {
                    saveinfoPath = saveinfoPath.Replace("\\\\", "\\");
                    int getFiledot = saveinfoPath.LastIndexOf(".");
                    int getFileg = saveinfoPath.LastIndexOf("\\");
                    string getFileName = saveinfoPath.Substring((getFileg + 1), ((getFiledot - getFileg) - 1));
                    string getFileEXName = saveinfoPath.Substring(getFiledot);
                    string PageHead = FinlContent.Substring(0, pos1);
                    string PageEnd = FinlContent.Substring(pos2 + 8);
                    string PageMid = FinlContent.Substring(pos1 + 11, pos2 - pos1 - 11);
                    string[] ArrayCon = PageMid.Split(new string[] { "[FS:PAGE]" }, StringSplitOptions.RemoveEmptyEntries);
                    int n = ArrayCon.Length;
                    //if (ArrayCon[n - 1] == null || ArrayCon[n - 1].Trim() == string.Empty)
                    //    n--;
                    for (int i = 0; i < n; i++)
                    {
                        string filepath = SiteRootPath + saveinfoPath;
                        if (i > 0)
                        {
                            int laspot = filepath.LastIndexOf('.');
                            filepath = filepath.Substring(0, laspot) + "_" + (i + 1) + filepath.Substring(laspot);
                        }
                        string PageContent = PageHead + ArrayCon[i] + PageEnd;
                        string getFileContent = General.ReplaceResultPage(infoID.ToString(), PageContent.Replace("[FS:PAGE]", "").Replace("FS:END->", "").Replace("<!-FS:STAR=", ""), getFileName, getFileEXName, n, (i + 1), 0);
                        General.WriteHtml(getFileContent, filepath);
                    }
                    //if (n > 0)
                    //{
                    //    if (!existFlag)
                    //    {
                    //        templateList.Add(tempRe);
                    //    }
                    //    return;
                    //}
                }
            }
            General.WriteHtml(tempRe.FinallyContent.Replace("FS:END->", "").Replace("<!-FS:STAR=", ""), SiteRootPath + saveinfoPath);
            //if (!existFlag)
            //{
            //    templateList.Add(tempRe);
            //}
        }
    }
}
