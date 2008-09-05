using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Foosun.Common;
using Foosun.Control;
using Foosun.Config;
using Foosun.Model;
using Foosun.DALProfile;
using System.Web;
using System.Threading;
using System.Collections;

namespace Foosun.Publish
{
    public partial class UltiPublish
    {
        private const string MsgFormat = "正在发布{0}，共需要发布{1}条，正在发布第{2}条，成功{3}条， <a href=\"error/GetError.aspx?d=now\" title=\"点击查看错误信息\"><span style=\"color:red;\">失败{4}条</span></a>{5}";

        private static Hashtable _userPublishInfo = new Hashtable();
        private bool _publisStates = true;
        private string _publisStateStr = null;
        /// <summary>
        /// 是否发布主页、新闻、栏目、专题，bool标志
        /// </summary>
        private bool fs_isPubindex;
        public bool IsPublishIndex
        {
            set { fs_isPubindex = value; }
        }
        /// <summary>
        /// 是否发布新闻，bool标志
        /// </summary>
        private bool fs_isPubNews;
        public bool IsPubNews
        {
            set { fs_isPubNews = value; }
        }
        /// <summary>
        /// 是否发布栏目索引文件，bool标志
        /// </summary>
        private bool fs_isClassIndex;
        /// <summary>
        /// 是否发布栏目索引文件，bool标志
        /// </summary>
        public bool isClassIndex
        {
            set { fs_isClassIndex = value; }
        }
        /// <summary>
        /// 是否发布栏目，bool标志
        /// </summary>
        private bool fs_isPubClass;
        public bool IsPubClass
        {
            set { fs_isPubClass = value; }
        }
        /// <summary>
        /// 是否发布专题，bool标志
        /// </summary>
        private bool fs_isPubSpecial;
        public bool IsPubSpecial
        {
            set { fs_isPubSpecial = value; }
        }

        /// <summary>
        /// 是否发布单页面
        /// </summary>
        private bool fs_isPubIsPage;
        public bool IsPubIsPage
        {
            set { fs_isPubIsPage = value; }
        }
        private string fs_strClassIsPageParam;
        public string StrClassIsPageParam
        {
            set { fs_strClassIsPageParam = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// 发布新闻方式标志
        /// </summary>
        private int fs_newsFlag;
        public int newsFlag
        {
            set { fs_newsFlag = value; }
        }
        /// <summary>
        /// 发布新闻的字符串参数
        /// </summary>
        private string fs_strNewsParams = string.Empty;
        public string strNewsParams
        {
            set { fs_strNewsParams = value; }
        }
        /// <summary>
        /// 发布栏目方式标志
        /// </summary>
        private int fs_ClassFlag;
        public int ClassFlag
        {
            set { fs_ClassFlag = value; }
        }
        /// <summary>
        /// 发布栏目的字符串参数
        /// </summary>
        private string fs_strClassParams = string.Empty;
        public string strClassParams
        {
            set { fs_strClassParams = value; }
        }
        /// <summary>
        /// 发布专题方式标志
        /// </summary>
        private int fs_specialFlag;
        public int specialFlag
        {
            set { fs_specialFlag = value; }
        }
        /// <summary>
        /// 发布专题的字符串参数
        /// </summary>
        private string fs_strSpecialParams = string.Empty;
        public string strSpecialParams
        {
            set { fs_strSpecialParams = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// 模板路径
        /// </summary>
        private string TempletPath;
        /// <summary>
        /// 模板实例列表
        /// </summary>
        private IList<Template> templateList = new List<Template>();
        /// <summary>
        /// 所发布成功的新闻、栏目、专题的ID列表
        /// </summary>
        private IList<string> succeedList = new List<string>();
        /// <summary>
        /// 发布失败的新闻、栏目、专题的ID、异常信息
        /// </summary>
        private IList<string> failedList = new List<string>();
        //-----------------------------------------------------------------
        /// <summary>
        /// 是否显示生成进度条标志
        /// </summary>
        private bool fs_isProgressBar;
        /// <summary>
        /// 要发布的新闻条数
        /// </summary>
        private int nNewsCount = 0;
        /// <summary>
        /// 要发布的栏目数
        /// </summary>
        private int nClassCount = 0;
        /// <summary>
        /// 要发布的专题数
        /// </summary>
        private int nSpecialCount = 0;
        /// <summary>
        /// 当前的用户名
        /// </summary>
        private string userName = string.Empty;
        /// <summary>
        /// 网站根目录物理路径
        /// </summary>
        public string SiteRootPath = string.Empty;
        /// <summary>
        /// 生成每一条新闻的路径
        /// </summary>
        private string saveNewsPath = string.Empty;
        /// <summary>
        /// 生成每一个栏目的路径
        /// </summary>
        private string saveClassPath = string.Empty;
        /// <summary>
        /// 生成每一个专题的路径
        /// </summary>
        private string saveSpecialPath = string.Empty;
        /// <summary>
        /// 获取模板目录
        /// </summary>
        private string strTempletDir = Foosun.Config.UIConfig.dirTemplet;
        /// <summary>
        /// 构造函数,传入标志和页面信息字符串
        /// </summary>
        /// <param name="fs_tempPath"></param>        
        public UltiPublish(bool isProgressBar)
        {
            fs_isProgressBar = isProgressBar;
            // SiteRootPath = Foosun.Common.ServerInfo.GetRootPath() + "\\";
            SiteRootPath = Foosun.Common.ServerInfo.GetRootPath();
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// 关闭所有数据库连接
        /// </summary>
        public void CloseAllConnection()
        {
            DbHelper.CloseConn();
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// 开始发布
        /// </summary>
        public void StartPublish()
        {
            CommonData.Initialize();
            HProgressBar.Start();
            if (fs_isPubindex) { ultiPublishIndex(); }
            if (fs_isPubClass) { ultiPublishClass(); }
            if (fs_isPubSpecial) { ultiPublishSpecial(); }
            if (fs_isPubIsPage) { ultiPublishIsPage(); }
            if (fs_isPubNews) { ultiPublishNews(); }
            //bug修改 如果没有进行任何操作,提示出来 周峻平 2008-6-23
            if (fs_isPubindex == false && fs_isPubNews == false && fs_isPubClass == false && fs_isPubSpecial == false)
            {
                HProgressBar.Roll("发布主页完成。&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a> &nbsp; <a class=\"list_link\" href=\"../../index.html\" target=\"_blank\">浏览首页</a>", 100);
            }
        }
        /// <summary>
        /// 生成主页
        /// </summary>
        private void ultiPublishIndex()
        {
           // try
            //{
                HProgressBar.Roll("正在发布主页", 0);
                string indexname = "index.html";
                TempletPath = Foosun.Common.Public.readparamConfig("IndexTemplet"); //rd["IndexTemplet"].ToString();
                TempletPath = TempletPath.Replace("/", "\\");
                TempletPath = TempletPath.ToLower().Replace("{@dirtemplet}", strTempletDir);
                indexname = Foosun.Common.Public.readparamConfig("IndexFileName");//rd["IndexFileName"].ToString();
                Template indexTemp = new Template(SiteRootPath.Trim('\\') + TempletPath, TempType.Index);
                indexTemp.GetHTML();
                indexTemp.ReplaceLabels();
                General.WriteHtml(indexTemp.FinallyContent, SiteRootPath.TrimEnd('\\') + "\\" + indexname);
                General.publishXML("0");
                //发布今日历史文档
                General.publishHistryIndex(0);
                HProgressBar.Roll("发布主页成功。&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a> &nbsp; <a class=\"list_link\" href=\"../../" + indexname + "\" target=\"_blank\">浏览首页</a>", 100);
          //  }
           // catch (Exception ex)
           // {
            //    Foosun.Common.Public.savePublicLogFiles("□□□发布主页", "【错误描述：】\r\n" + ex.ToString(), "");
               // HProgressBar.Roll("发布主页失败。<a href=\"error/geterror.aspx?\">查看日志</a>", 0);
           // }
        }

        //随机用户编号
        private int userPublishID = 0;
        
        /// <summary>
        /// 生成所有新闻
        /// </summary>
        private void ultiPublishNews()
        {
            showMessage showObj = new showMessage();

            IDataReader rd = getAllNews();
            showObj.Indexname = Foosun.Common.Public.readparamConfig("IndexFileName");
            int _countTime = 0;
            if (nNewsCount > 0)
            {
                DataTable dts = new DataTable();
                DataRow dr = null;
                dts.Columns.Add("newsID");
                dts.Columns.Add("datalib");
                dts.Columns.Add("classID");
                dts.Columns.Add("SavePath");
                dts.Columns.Add("FileName");
                dts.Columns.Add("FileEXName");
                dts.Columns.Add("templet");

                while (rd.Read())
                {
                    dr = dts.NewRow();
                    dr["newsID"] = rd["newsID"];
                    dr["datalib"] = rd["datalib"];
                    dr["classID"] = rd["classID"];
                    dr["SavePath"] = rd["SavePath"];
                    dr["FileName"] = rd["FileName"];
                    dr["FileEXName"] = rd["FileEXName"];
                    dr["templet"] = rd["templet"];
                    dts.Rows.Add(dr);
                }
                //设置实体
                showObj.ThreadRt = dts;


                StringBuilder sbScript = new StringBuilder();
                //得到一个随机用户编号
                Random userPublishRandom = new Random();
                userPublishID = userPublishRandom.Next(1000, 9999);


                _userPublishInfo.Add(userPublishID, showObj);


                sbScript.Append("<script language=\"JavaScript\" type=\"text/javascript\" src=\"../../configuration/js/Public.js\"></script>");
                sbScript.Append("<script language=\"JavaScript\" type=\"text/javascript\" src=\"../../configuration/js/Prototype.js\"></script>");
                sbScript.Append("<script language=\"javascript\">");
                sbScript.Append("var userPublishID=" + userPublishID + ";");
                sbScript.Append("showMessageRequest('" + Foosun.Config.UIConfig.dirMana + "');");
                sbScript.Append("</script>");
                HttpContext.Current.Response.Write(sbScript.ToString());
                HttpContext.Current.Response.Flush();


                if (PublisStates)//快速发布
                {
                    thread();
                }
                else//节省CPU
                {
                    Thread ths = new Thread(new ThreadStart(thread));
                    ths.Priority = ThreadPriority.Lowest;
                    ths.Name = Foosun.Common.ServerInfo.ServerPort;
                    ths.Start();
                }
                
            }
            else
            {
                HProgressBar.Roll("没有新闻", 0);
            }
        }

        private void thread()
        {
            //lock (this)
            //{
                showMessage showObjs = (showMessage)_userPublishInfo[userPublishID];
                try
                {
                    DataRow d = null;
                    for (int ii = 0; ii < showObjs.ThreadRt.Rows.Count; ii++)
                    {
                        d = showObjs.ThreadRt.Rows[ii];
                        showObjs.ThreadFlag = true;
                        TempletPath = d["templet"].ToString();
                        TempletPath = TempletPath.Replace("/", "\\");
                        TempletPath = TempletPath.ToLower().Replace("{@dirtemplet}", strTempletDir);
                        setSaveNewsPath(d["classID"].ToString(), d["SavePath"].ToString(), d["FileName"].ToString(), d["FileEXName"].ToString());
                        bool state = publishSingleNews(d["newsID"].ToString(), d["datalib"].ToString(), d["classID"].ToString());//发布一条新闻，返回成功与否                                              
                        showObjs.ThisPublisCount += 1;
                        if (state)
                        {
                            showObjs.Success = showObjs.Success + 1;
                        }
                        else
                        {
                            showObjs.Error = showObjs.Error + 1;
                        }

                        showObjs.BarNum = showObjs.ThisPublisCount * 100 / nNewsCount;
                        showObjs.MaxPublishNumber = nNewsCount;
                        //将实体设置到集合中
                        _userPublishInfo[userPublishID] = showObjs;

                        //
                        string _showMSG = string.Format(MsgFormat, "新闻", nNewsCount.ToString(), showObjs.ThisPublisCount, showObjs.Success, showObjs.Error, "发布新闻成功。&nbsp;&nbsp;<a class=\"list_link\" href=\"../../" + showObjs.Indexname + "\" target=\"_blank\">浏览首页</a> &nbsp;&nbsp;&nbsp; ");
                        showMSGNotThread(_showMSG, showObjs.BarNum);
                    }
                    if (!showObjs.ThreadFlag)
                    {
                        string _showMSG = string.Format(MsgFormat, "新闻", nNewsCount.ToString(), showObjs.ThisPublisCount, showObjs.Success, showObjs.Error, "发布新闻成功。&nbsp;&nbsp;<a class=\"list_link\" href=\"../../" + showObjs.Indexname + "\" target=\"_blank\">浏览首页</a> &nbsp;&nbsp;&nbsp; ");
                        if (nNewsCount > 0)
                        {
                            showMSGNotThread(_showMSG, showObjs.BarNum);
                        }
                        else
                        {
                            showMSGNotThread("没有新闻", 0);
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
                            Foosun.Common.Public.savePublicLogFiles("□□□发布新闻", "【ID】:" + failedList[i].Split('$')[0] + "\r\n【错误描述：】\r\n" + failedList[i].Split('$')[1], "");
                        }
                        failedList.Clear();
                    }
                    if (succeedList.Count != 0)
                    {
                        updateNewsIsHtml(DBConfig.TableNamePrefix + "news", "isHtml", "newsID");
                    }
                    //将实体设置到集合中
                    _userPublishInfo[userPublishID] = showObjs;
                }
                catch
                {
                    Thread.CurrentThread.Abort();
                    throw;
                }
                finally
                {
                    string threadNames = Thread.CurrentThread.Name;
                    if (!string.IsNullOrEmpty(threadNames))
                    {
                        int resultThread = 0;
                        if (int.TryParse(threadNames,out resultThread))
                        {
                            Thread.CurrentThread.Abort();
                        }
                    }
                    _userPublishInfo.Remove(userPublishID);
                } 
           // }
        }

        private void showMSGNotThread(string msg,int count)
        {
            string threadNames = Thread.CurrentThread.Name;
            if (string.IsNullOrEmpty(threadNames))
            {
                HProgressBar.Roll(msg, count);
            }
            
        }

        //设置显示参数
        public static string showMessages(int ajaxUserPublishID)
        {
            string message = null;
            showMessage showObjs = (showMessage)_userPublishInfo[ajaxUserPublishID];
            if (showObjs == null)
            {
                showObjs = new showMessage();
            }

            message = "<indexname>" + showObjs.Indexname + "</indexname><thisPublish>" + showObjs.ThisPublisCount + "</thisPublish><maxPublishNumber>" + showObjs.MaxPublishNumber + "</maxPublishNumber><thisPublisCount>" + showObjs.ThisPublisCount + "</thisPublisCount><success>" + showObjs.Success + "</success><error>" + showObjs.Error + "</error><barNum>" + showObjs.BarNum + "</barNum>";

            return message;
        }


        /// <summary>
        /// 为saveNewsPath赋值
        /// </summary>
        /// <param name="classID">新闻所属栏目的ID</param>
        /// <param name="SavePath">新闻表中SavePath字段的值</param>
        /// <param name="FileName">新闻表中FileName字段的值</param>
        /// <param name="FileName">新闻表中FileName字段的值</param>
        private void setSaveNewsPath(string classID, string SavePath, string FileName, string FileEXName)
        {
            PubClassInfo info = CommonData.GetClassById(classID);
            if (info != null)
            {
                saveNewsPath = "\\" + info.SavePath.Trim('\\').Trim('/') + "\\" + info.SaveClassframe.Trim('\\').Trim('/') + "\\" + SavePath.Trim('\\').Trim('/') + "\\" + FileName.Trim('\\').Trim('/') + FileEXName.Trim('\\').Trim('/');
            }
        }
        /// <summary>
        /// 设置已发布新闻、栏目的IsHtml
        /// </summary>
        private void updateNewsIsHtml(string tableName, string isHtml, string idField)
        {
            try
            {
                CommonData.DalPublish.UpdateNewsIsHtml(tableName, isHtml, idField, succeedList);
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
        /// 生成所有栏目
        /// </summary>
        public void ultiPublishClass()
        {
            using (IDataReader rd = getAllClass())
            {
                int num = 0;
                int succeedNum = 0;
                int failedNum = 0;
                bool HasRows = false;
                string indexname = "index.html";
                indexname = Foosun.Common.Public.readparamConfig("IndexFileName");
                if (nClassCount > 0)
                {
                    while (rd.Read())
                    {
                        HasRows = true;
                        TempletPath = rd["classtemplet"].ToString();
                        TempletPath = TempletPath.Replace("/", "\\");
                        TempletPath = TempletPath.ToLower().Replace("{@dirtemplet}", strTempletDir);
                        string TmpsaveClassPath = "\\" + rd["savePath"].ToString().Trim('\\').Trim('/') + "\\" + rd["SaveClassframe"].ToString().Trim('\\').Trim('/') + '\\' + rd["ClassSaveRule"].ToString().Trim('\\').Trim('/');
                        saveClassPath = TmpsaveClassPath.Replace("/", "\\");
                        string strClassId = rd["classid"].ToString();
                        bool state = publishSingleClass(strClassId, rd["Datalib"].ToString());
                        if (fs_isClassIndex)
                        {
                            General.publishClassIndex(strClassId);
                        }
                        num += 1;
                        if (state)
                        {
                            succeedNum++;
                        }
                        else
                        {
                            failedNum++;
                        }
                        string msg = string.Format(MsgFormat, "栏目", nClassCount.ToString(), ((int)num).ToString(), succeedNum.ToString(), failedNum.ToString(), "发布栏目成功&nbsp;&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a>&nbsp;&nbsp;<a class=\"list_link\" href=\"../../" + indexname + "\" target=\"_blank\">浏览首页</a>");
                        HProgressBar.Roll(msg, (num * 100 / nClassCount));
                    }
                }
                else
                {
                    HProgressBar.Roll("没有栏目", 0);
                }
                if (!HasRows)
                {
                    string msg = string.Format(MsgFormat, "栏目", nClassCount.ToString(), ((int)num).ToString(), succeedNum.ToString(), failedNum.ToString(), "发布栏目成功&nbsp;&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a>&nbsp;&nbsp;<a class=\"list_link\" href=\"../../" + indexname + "\" target=\"_blank\">浏览首页</a>");
                    if (nClassCount > 0)
                    {
                        HProgressBar.Roll(msg, (num * 100 / nClassCount));
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
                    Foosun.Common.Public.savePublicLogFiles("□□□发布栏目", "【ID】:" + failedList[i].Split('$')[0] + "\r\n【错误描述：】\r\n" + failedList[i].Split('$')[1], "");
                }
                failedList.Clear();
            }
            if (succeedList.Count != 0)
            {
                updateNewsIsHtml(DBConfig.TableNamePrefix + "news_class", "isunHtml", "cLassID");
            }

        }
        /// <summary>
        /// 生成所有专题
        /// </summary>
        public void ultiPublishSpecial()
        {
            float num = 0;
            int succeedNum = 0;
            int failedNum = 0;
            bool HasRows = false;
            string indexname = "index.html";
            indexname = Foosun.Common.Public.readparamConfig("IndexFileName");
            IDataReader rd = getAllSpecials();
            {
                if (nSpecialCount > 0)
                {
                    while (rd.Read())
                    {
                        HasRows = true;
                        TempletPath = rd["Templet"].ToString();
                        TempletPath = TempletPath.Replace("/", "\\");
                        TempletPath = TempletPath.ToLower().Replace("{@dirtemplet}", strTempletDir);
                        string TmpsaveSpecialPath = "\\" + rd["SavePath"].ToString().Trim('\\').Trim('/') + "\\" + rd["saveDirPath"].ToString().Trim('\\').Trim('/') + '\\' + rd["FileName"].ToString().Trim('\\').Trim('/') + rd["FileEXName"].ToString().Trim('\\').Trim('/');
                        saveSpecialPath = TmpsaveSpecialPath.Replace("{@dirHtml}", Foosun.Config.UIConfig.dirHtml).Replace("/", "\\"); ;
                        bool state = publishSingleSpecial(rd["specialID"].ToString());
                        if (state)
                        {
                            num += 1;
                            succeedNum++;
                        }
                        else
                        {
                            failedNum++;
                        }
                        string msg = string.Format(MsgFormat, "专题", nSpecialCount.ToString(), ((int)num).ToString(), succeedNum.ToString(), failedNum.ToString(), "发布栏目成功。&nbsp;&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a> &nbsp;&nbsp;&nbsp;<a class=\"list_link\" href=\"../../" + indexname + "\" target=\"_blank\">浏览首页</a>");
                        HProgressBar.Roll(msg, (int)(num / nSpecialCount) * 100);
                    }
                }
                else
                {
                    HProgressBar.Roll("没有专题", 0);
                }
                rd.Close();
                if (!HasRows)
                {
                    string msg = string.Format(MsgFormat, "专题", nSpecialCount.ToString(), ((int)num).ToString(), succeedNum.ToString(), failedNum.ToString(), "发布栏目成功。&nbsp;&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a> &nbsp;&nbsp;&nbsp; <a class=\"list_link\" href=\"../../" + indexname + "\" target=\"_blank\">浏览首页</a>");
                    HProgressBar.Roll(msg, (int)(num / nSpecialCount) * 100);
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
                    Foosun.Common.Public.savePublicLogFiles("□□□发布专题", "【ID】:" + failedList[i].Split('$')[0] + "\r\n【错误描述：】\r\n" + failedList[i].Split('$')[1], "");
                }
                failedList.Clear();
            }
        }
        /// <summary>
        /// 发布一条新闻
        /// </summary>
        /// <param name="newsID">单条新闻的ID</param>
        /// <param name="datalib">该条新闻所在的表</param>
        /// <returns>成功与否标志</returns>
        public bool publishSingleNews(string newsID, string datalib, string classID)
        {
            return General.publishSingleNews(newsID, classID);
            /*
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
                            temp.NewsID = newsID;
                            makeHtmlFile(temp, existFlag, classID, newsID);
                            break;
                        }
                    }
                    if (!existFlag)
                    {
                        Template newsTemplate = new Template(TempletPath, TempType.News);
                        newsTemplate.NewsID = newsID;
                        newsTemplate.GetHTML();
                        makeHtmlFile(newsTemplate, existFlag, classID, newsID);
                    }
                }
                else
                {
                    Template newsTemplate = new Template(TempletPath, TempType.News);
                    newsTemplate.NewsID = newsID;
                    newsTemplate.GetHTML();
                    makeHtmlFile(newsTemplate, existFlag, classID, newsID);
                }
                succeedList.Add(newsID);
                return true;
            }
            catch (Exception e)
            {
                failedList.Add(newsID + "$" + e.Message);
                Foosun.Common.Public.savePublicLogFiles("□□□发布新闻", "【ID】:" + classID + "\r\n【错误描述：】\r\n" + e.ToString(), "");
                return false;
            }

            */

        }
        /// <summary>
        /// 发布一个栏目
        /// </summary>
        /// <param name="classID">单个栏目的ID</param>
        /// <param name="datalib">该栏目所在的表</param>
        /// <returns>成功与否标志</returns>
        public bool publishSingleClass(string classID, string datalib)
        {
            try
            {
                TempletPath = SiteRootPath.Trim('\\') + TempletPath;
                bool existFlag = false;
                string tmpPath = HttpContext.Current.Server.MapPath(saveClassPath);
                if (templateList.Count != 0)
                {
                    foreach (Template temp in templateList)
                    {
                        if (TempletPath == temp.TempFilePath)
                        {
                            existFlag = true;
                            temp.ClassID = classID;
                            
                            replaceTemp(temp, existFlag, tmpPath, classID, "class");
                            break;
                        }
                    }
                    if (!existFlag)
                    {
                        //Template classTemplate = new Template(TempletPath, TempType.Class);
                        //classTemplate.ClassID = classID;
                        //classTemplate.GetHTML();
                        //string TmpPath = SiteRootPath + saveClassPath;
                        //replaceTempg(classTemplate, TmpPath, classID, "class");

                        Template classTemplate = new Template(TempletPath, TempType.Class);
                        classTemplate.ClassID = classID;
                        classTemplate.GetHTML();
                        replaceTemp(classTemplate, existFlag, tmpPath, classID, "class");
                    }
                }
                else
                {
                    Template classTemplate = new Template(TempletPath, TempType.Class);
                    classTemplate.ClassID = classID;
                    classTemplate.GetHTML();
                    replaceTemp(classTemplate, existFlag, tmpPath, classID, "class");
                }
                succeedList.Add(classID);
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
        /// 发布一个专题
        /// </summary>
        /// <param name="specialID">专题ID</param>
        /// <returns>成功与否标志</returns>
        public bool publishSingleSpecial(string specialID)
        {
            try
            {
                TempletPath = SiteRootPath.Trim('\\') + TempletPath;
                string specialSavePath = HttpContext.Current.Server.MapPath(saveSpecialPath);
                bool existFlag = false;
                if (templateList.Count != 0)
                {
                    foreach (Template temp in templateList)
                    {
                        if (TempletPath == temp.TempFilePath)
                        {
                            existFlag = true;
                            temp.SpecialID = specialID;
                            replaceTemp(temp, existFlag, specialSavePath, specialID, "special");
                            break;
                        }
                    }
                    if (!existFlag)
                    {
                        Template specialTemplate = new Template(TempletPath, TempType.Special);
                        specialTemplate.SpecialID = specialID;
                        specialTemplate.GetHTML();
                        replaceTemp(specialTemplate, existFlag, specialSavePath, specialID, "special");
                    }
                }
                else
                {
                    Template specialTemplate = new Template(TempletPath, TempType.Special);
                    specialTemplate.SpecialID = specialID;
                    specialTemplate.GetHTML();
                    replaceTemp(specialTemplate, existFlag, specialSavePath, specialID, "special");
                }
                succeedList.Add(specialID);
                return true;
            }
            catch (Exception e)
            {
                failedList.Add(specialID + "$" + e.Message);
                return false;
            }
        }
        /// <summary>
        /// 处理模板
        /// </summary>
        /// <param name="tempRe">模板实例</param>
        /// <param name="existFlag">该模板是否存在于模板列表</param>
        protected void replaceTemp(Template tempRe, bool existFlag, string savePath,string id,string ContentType)
        {
            tempRe.ReplaceLabels();
            savePath = savePath.Replace("/", @"\\");
            savePath = savePath.Replace(@"\\\\", @"\\");
            if (tempRe.MyTempType == TempType.Class || tempRe.MyTempType == TempType.Special || tempRe.MyTempType == TempType.ChClass || tempRe.MyTempType == TempType.Chspecial)
            {
                string FinlContent = tempRe.FinallyContent;
                int pos1 = FinlContent.IndexOf("{Foosun:NewsLIST}");
                int pos2 = FinlContent.IndexOf("{/Foosun:NewsLIST}");
                string filepath = savePath;
                string TmpPath = savePath;
                if (pos2 > pos1 && pos1 > -1)
                {
                    int getFiledot = savePath.LastIndexOf(".");
                    int getFileg = savePath.LastIndexOf("\\");
                    string getFileName = savePath.Substring((getFileg + 1), ((getFiledot - getFileg) - 1));
                    string getFileEXName = savePath.Substring(getFiledot);
                    #region 处理分页
                    string PageHead = FinlContent.Substring(0, pos1);
                    string PageEnd = FinlContent.Substring(pos2 + 18);
                    string PageMid = FinlContent.Substring(pos1 + 17, pos2 - pos1 - 17);
                    string pattern = @"\{\$FS\:P[01]\}\{Page\:\d\$[^\$]{0,6}\$[^\$]{0,20}\}";
                    Regex reg = new Regex(pattern, RegexOptions.Compiled);
                    Match match = reg.Match(PageMid);
                    if (match.Success)
                    {
                        if (Foosun.Config.verConfig.PublicType == "0" || tempRe.MyTempType == TempType.ChClass || tempRe.MyTempType == TempType.Chspecial)
                        {
                            string PageStr = match.Value;
                            int posPage = PageStr.IndexOf("}{Page:");

                            string postResult = PageStr.Substring(posPage + 7);
                            postResult = postResult.TrimEnd('}');
                            string[] postResultARR = postResult.Split('$');
                            string postResult_style = postResultARR[0];
                            string postResult_color = postResultARR[1];
                            string postResult_css = postResultARR[2];
                            string postResult_css1 = "";
                            if (postResult_css.Trim() != string.Empty)
                            {
                                postResult_css1 = " class=\"" + postResult_css + "\"";
                            }
                            string[] ArrayCon = reg.Split(PageMid);
                            int n = ArrayCon.Length;
                            if (ArrayCon[n - 1] == null || ArrayCon[n - 1].Trim() == string.Empty)
                                n--;
                            for (int i = 0; i < n; i++)
                            {
                                string getPageStr = "";
                                if (i > 0)
                                {
                                    int laspot = TmpPath.LastIndexOf('.');
                                    filepath = TmpPath.Substring(0, laspot) + "_" + (i + 1) + TmpPath.Substring(laspot);
                                }
                                UltiPublish gpl = new UltiPublish(true);
                                getPageStr = gpl.getPagelist(postResult_style, i, getFileName, getFileEXName, postResult_color, postResult_css1, n, id, ContentType, 0);
                                string PageContent = PageHead + ArrayCon[i] + getPageStr + PageEnd;

                                General.WriteHtml(PageContent, filepath);
                            }
                            if (n > 0)
                            {
                                if (!existFlag)
                                {
                                    templateList.Add(tempRe);
                                }
                                return;
                            }
                        }
                    }
                    #endregion
                }
            }
            string p1js = "<span style=\"text-align:center;\" id=\"gPtypenowdiv" + DateTime.Now.ToShortDateString() + "\">加载中...</span>";
            p1js += "<script language=\"javascript\" type=\"text/javascript\">";
            p1js += "pubajax('" + CommonData.SiteDomain + "/configuration/system/public.aspx','NowStr=" + DateTime.Now.ToShortDateString() + "&ruleStr=1','gPtypenowdiv" + DateTime.Now.ToShortDateString() + "');";
            p1js += "</script>";
            General.WriteHtml(tempRe.FinallyContent.Replace("{Foosun:NewsLIST}", "").Replace("{/Foosun:NewsLIST}", "").Replace("{$FS:P1}", p1js), savePath);
            if (!existFlag)
            {
                templateList.Add(tempRe);
            }
            return;
        }

        /// <summary>
        /// 得到分页样式
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="sColor"></param>
        /// <param name="Css"></param>
        /// <returns></returns>
        public static string getPageStyle(string Input, string sColor)
        {
            string colorstr = "";
            if (sColor.Trim() != string.Empty)
            {
                colorstr = " style=\"color:" + sColor + "\"";
            }
            return "<span" + colorstr + ">" + Input + "</span>";
        }

        public string getPageresult(string ID, string ReadType, string ContentType, int ConstStr, string getFileName, string getFileEXName, int isPop)
        {
            
            string getURL = string.Empty;
            string CHSTR = string.Empty;
            if (fs_ChID != 0)
            {
                CHSTR = "&ChID=" + fs_ChID.ToString() + ""; 
            }
            if (ReadType == "1")
            {
                if (ContentType == "class")
                {
                    if (ConstStr == 0)
                    {
                        getURL = "list.aspx?id=" + ID + CHSTR;
                    }
                    else
                    {
                        getURL = "list.aspx?id=" + ID + "&Page=" + ConstStr + CHSTR;
                    }
                }
                else
                {
                    if (ConstStr == 0)
                    {
                        getURL = "Special.aspx?id=" + ID + CHSTR;
                    }
                    else
                    {
                        getURL = "Special.aspx?id=" + ID + "&Page=" + ConstStr + CHSTR;
                    }
                }
            }
            else
            {
                if (isPop == 0)
                {
                    if (ConstStr == 0)
                    {
                        getURL = getFileName + getFileEXName;
                    }
                    else
                    {
                        getURL = getFileName + "_" + ConstStr + getFileEXName;
                    }
                }
                else
                {
                    if (ContentType == "class")
                    {
                        if (ConstStr == 0)
                        {
                            getURL = "list.aspx?id=" + ID + CHSTR;
                        }
                        else
                        {
                            getURL = "list.aspx?id=" + ID + "&Page=" + ConstStr + CHSTR;
                        }
                    }
                    else
                    {
                        if (ConstStr == 0)
                        {
                            getURL = "Special.aspx?id=" + ID + CHSTR;
                        }
                        else
                        {
                            getURL = "Special.aspx?id=" + ID + "&Page=" + ConstStr + CHSTR;
                        }
                    }
                }
            }
            return getURL;
        }

        /// <summary>
        /// 得到分页样式
        /// </summary>
        /// <param name="Numstr"></param>
        /// <param name="i"></param>
        /// <param name="getFileName"></param>
        /// <param name="getFileEXName"></param>
        /// <param name="postResult_color"></param>
        /// <param name="postResult_css"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public string getPagelist(string Numstr, int i, string getFileName, string getFileEXName, string postResult_color, string postResult_css, int n,string ID,string ContentType,int isPop)
        {
            string Pagestr = string.Empty;
            string ReadType = Foosun.Common.Public.readparamConfig("ReviewType");
            switch (Numstr)
            {
                case "0":
                    Pagestr += "<div style=\"padding-top:15px;" + postResult_css + ">共" + n + "页,&nbsp;当前第" + (i + 1) + "页,&nbsp;";
                    if (i == 0)
                    {
                        Pagestr += getPageStyle("首页", postResult_color) + "&nbsp;";
                        Pagestr += getPageStyle("上一页", postResult_color) + "&nbsp;";
                    }
                    else
                    {
                        Pagestr += "<a " +  " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\" title=\"首页\">" + getPageStyle("首页", postResult_color) + "</a>&nbsp;";
                        if (i == 1)
                        {
                            Pagestr += "<a " +  " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\" title=\"上一页\">" + getPageStyle("上一页", postResult_color) + "</a>&nbsp;";
                        }
                        else
                        {
                            Pagestr += "<a " +  " href=\"" + getPageresult(ID, ReadType, ContentType, i, getFileName, getFileEXName, isPop) + "\" title=\"上一页\">" + getPageStyle("上一页", postResult_color) + "</a>&nbsp;";
                        }
                    }
                    if (n < 10)
                    {
                        Pagestr += "<a " +  " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\" title=\"上十页\">" + getPageStyle("上十页", postResult_color) + "</a>&nbsp;";
                        for (int m = i; m < n; m++)
                        {
                            if (m == i)
                            {
                                Pagestr += "<strong>" + getPageStyle("" + (m + 1), postResult_color) + "</strong>&nbsp;";
                            }
                            else
                            {
                                if (m == 0)
                                {
                                    Pagestr += "<a " +  " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\">" + getPageStyle("" + (m + 1), postResult_color) + "</a>&nbsp;";
                                }
                                else
                                {
                                    Pagestr += "<a " +  " href=\"" + getPageresult(ID, ReadType, ContentType, (m + 1), getFileName, getFileEXName, isPop) + "\">" + getPageStyle("" + (m + 1), postResult_color) + "</a>&nbsp;";
                                }
                            }
                        }
                        Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, n, getFileName, getFileEXName, isPop) + "\" title=\"下十页\">" + getPageStyle("下十页", postResult_color) + "</a>&nbsp;";
                    }
                    else if (n > 10)
                    {
                        if (i < 11)
                        {
                            Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\" title=\"上十页\">" + getPageStyle("上十页", postResult_color) + "</a>&nbsp;";
                        }
                        else
                        {
                            Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, ((i + 1) - 10), getFileName, getFileEXName, isPop) + "\" title=\"上十页\">" + getPageStyle("上十页", postResult_color) + "</a>&nbsp;";
                        }
                        int mjs = (i + 10);
                        if ((n - i) < 10)
                        {
                            mjs = n;
                        }
                        for (int m = i; m < (mjs); m++)
                        {
                            if (m == i)
                            {
                                Pagestr += "<strong>" + getPageStyle("" + (m + 1), postResult_color) + "</strong>&nbsp;";
                            }
                            else
                            {
                                if (m == 0)
                                {
                                    Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\">" + getPageStyle("" + (m + 1), postResult_color) + "</a>&nbsp;";
                                }
                                else
                                {
                                    Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, (m + 1), getFileName, getFileEXName, isPop) + "\">" + getPageStyle("" + (m + 1), postResult_color) + "</a>&nbsp;";
                                }
                            }
                        }
                        if ((i + 10) > n)
                        {
                            Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, n, getFileName, getFileEXName, isPop) + "\" title=\"下十页\">" + getPageStyle("下十页", postResult_color) + "</a>&nbsp;";
                        }
                        else
                        {
                            Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, ((i + 1) + 10), getFileName, getFileEXName, isPop) + "\" title=\"下十页\">" + getPageStyle("下十页", postResult_color) + "</a>&nbsp;";
                        }
                    }
                    if (i == (n - 1))
                    {
                        Pagestr += getPageStyle("下一页", postResult_color) + "&nbsp;";
                        Pagestr += getPageStyle("尾页", postResult_color) + "</div>";
                    }
                    else
                    {
                        Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, (i + 2), getFileName, getFileEXName, isPop) + "\" title=\"下一页\">" + getPageStyle("下一页", postResult_color) + "</a>&nbsp;";
                        Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, n, getFileName, getFileEXName, isPop) + "\" title=\"尾页\">" + getPageStyle("尾页", postResult_color) + "</a></div>";
                    }
                    break;
                case "1":
                    Pagestr += "<div style=\"padding-top:15px;\">共" + n + "页,&nbsp;当前第" + (i + 1) + "页,&nbsp;";
                    if ((i + 1) > 2)
                    {
                        Pagestr += "&nbsp;<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, i, getFileName, getFileEXName, isPop) + "\">" + getPageStyle("上一页", postResult_color) + "</a>&nbsp;&nbsp;";
                    }
                    else
                    {
                        if (i == 0)
                        {
                            Pagestr += "&nbsp;" + getPageStyle("上一页", postResult_color) + "&nbsp;&nbsp;";
                        }
                        else
                        {
                            Pagestr += "&nbsp;<a " + postResult_css + " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\">" + getPageStyle("上一页", postResult_color) + "</a>&nbsp;&nbsp;";
                        }
                    }
                    for (int j = 0; j < n; j++)
                    {
                        if (j == i)
                        {
                            Pagestr += "<strong>" + getPageStyle("第" + (j + 1) + "页", postResult_color) + "</strong>&nbsp;";
                        }
                        else
                        {
                            if (j == 0)
                            {
                                Pagestr += "<a " + postResult_css + " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\">" + getPageStyle("第" + (j + 1) + "页", postResult_color) + "</a>&nbsp;";
                            }
                            else
                            {
                                Pagestr += "<a " + postResult_css + " href=\"" + getPageresult(ID, ReadType, ContentType, (j + 1), getFileName, getFileEXName, isPop) + "\">" + getPageStyle("第" + (j + 1) + "页", postResult_color) + "</a>&nbsp;";
                            }
                        }
                    }
                    if ((i + 1) ==n)
                    {
                        Pagestr += "&nbsp;&nbsp;" + getPageStyle("下一页", postResult_color) + "&nbsp;";
                    }
                    else
                    {
                        Pagestr += "&nbsp;&nbsp;<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, (i + 2), getFileName, getFileEXName, isPop) + "\">" + getPageStyle("下一页", postResult_color) + "</a>&nbsp;";
                    }
                    Pagestr += "</div>";
                    break;
                case "2":
                    Pagestr += "<div style=\"padding-top:15px;\">共" + n + "页,&nbsp;当前第" + (i + 1) + "页,&nbsp;";
                    if ((i + 1) > 2)
                    {
                        Pagestr += "&nbsp;<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, i, getFileName, getFileEXName, isPop) + "\">" + getPageStyle("上一页", postResult_color) + "</a>&nbsp;&nbsp;";
                    }
                    else
                    {
                        if (i == 0)
                        {
                            Pagestr += "&nbsp;" + getPageStyle("上一页", postResult_color) + "&nbsp;&nbsp;";
                        }
                        else
                        {
                            Pagestr += "&nbsp;<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\">" + getPageStyle("上一页", postResult_color) + "</a>&nbsp;&nbsp;";
                        }
                    }
                    for (int j = 0; j < n; j++)
                    {
                        if (j == i)
                        {
                            Pagestr += "<strong>" + getPageStyle("" + (j + 1), postResult_color) + "</strong>&nbsp;";
                        }
                        else
                        {
                            if (j == 0)
                            {
                                Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\">" + getPageStyle("" + (j + 1), postResult_color) + "</a>&nbsp;";
                            }
                            else
                            {
                                Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, (j + 1), getFileName, getFileEXName, isPop) + "\">" + getPageStyle("" + (j + 1), postResult_color) + "</a>&nbsp;";
                            }
                        }
                    }
                    if ((i + 1) == n)
                    {
                        Pagestr += "&nbsp;&nbsp;" + getPageStyle("下一页", postResult_color) + "&nbsp;";
                    }
                    else
                    {
                        Pagestr += "&nbsp;&nbsp;<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, (i + 2), getFileName, getFileEXName, isPop) + "\">" + getPageStyle("下一页", postResult_color) + "</a>&nbsp;";
                    }
                    Pagestr += "</div>";
                    break;
                default:
                    Pagestr += "<div style=\"padding-top:15px;\">共" + n + "页,&nbsp;当前第" + (i + 1) + "页,&nbsp;";
                    if (i == 0)
                    {
                        Pagestr += getPageStyle("<font face=webdings title=\"首页\">9</font>", postResult_color) + "&nbsp;";
                        Pagestr += getPageStyle("<font face=webdings title=\"上一页\">3</font>", postResult_color) + "&nbsp;";
                    }
                    else
                    {
                        Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\" title=\"首页\">" + getPageStyle("<font face=webdings>9</font>", postResult_color) + "</a>&nbsp;";
                        if (i == 1)
                        {
                            Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\" title=\"上一页\">" + getPageStyle("<font face=webdings>3</font>", postResult_color) + "</a>&nbsp;";
                        }
                        else
                        {
                            Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, i, getFileName, getFileEXName, isPop) + "\" title=\"上一页\">" + getPageStyle("<font face=webdings>3</font>", postResult_color) + "</a>&nbsp;";
                        }
                    }
                    if (n < 10)
                    {
                        Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\" title=\"上十页\">" + getPageStyle("<font face=webdings>7</font>", postResult_color) + "</a>&nbsp;";
                        for (int m = i; m < n; m++)
                        {
                            if (m == i)
                            {
                                Pagestr += "<strong>" + getPageStyle("" + (m + 1), postResult_color) + "</strong>&nbsp;";
                            }
                            else
                            {
                                if (m == 0)
                                {
                                    Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\">" + getPageStyle("" + (m + 1), postResult_color) + "</a>&nbsp;";
                                }
                                else
                                {
                                    Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, (m + 1), getFileName, getFileEXName, isPop) + "\">" + getPageStyle("" + (m + 1), postResult_color) + "</a>&nbsp;";
                                }
                            }
                        }
                        Pagestr += getPageStyle("<font face=webdings>8</font>", postResult_color) + "&nbsp;";
                    }
                    else if (n > 10)
                    {
                        if (i < 11)
                        {
                            Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\" title=\"上十页\">" + getPageStyle("<font face=webdings>7</font>", postResult_color) + "</a>&nbsp;";
                        }
                        else
                        {
                            Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, ((i + 1) - 10), getFileName, getFileEXName, isPop) + "\" title=\"上十页\">" + getPageStyle("<font face=webdings>7</font>", postResult_color) + "</a>&nbsp;";
                        }
                        int mjs = (i + 10);
                        if ((n - i) < 10)
                        {
                            mjs = n;
                        }
                        for (int m = i; m < (mjs); m++)
                        {
                            if (m == i)
                            {
                                Pagestr += "<strong>" + getPageStyle("" + (m + 1), postResult_color) + "</strong>&nbsp;";
                            }
                            else
                            {
                                if (m == 0)
                                {
                                    Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, 0, getFileName, getFileEXName, isPop) + "\">" + getPageStyle("" + (m + 1), postResult_color) + "</a>&nbsp;";
                                }
                                else
                                {
                                    Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, (m + 1), getFileName, getFileEXName, isPop) + "\">" + getPageStyle("" + (m + 1), postResult_color) + "</a>&nbsp;";
                                }
                            }
                        }
                        if ((i + 10) > n)
                        {
                            Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, n, getFileName, getFileEXName, isPop) + "\" title=\"下十页\">" + getPageStyle("<font face=webdings>7</font>", postResult_color) + "</a>&nbsp;";
                        }
                        else
                        {
                            Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, ((i + 1) + 10), getFileName, getFileEXName, isPop) + "\" title=\"下十页\">" + getPageStyle("<font face=webdings>7</font>", postResult_color) + "</a>&nbsp;";
                        }
                    }
                    if (i == (n - 1))
                    {
                        Pagestr += getPageStyle("<font face=webdings title=\"下一页\">4</font>", postResult_color) + "&nbsp;&nbsp;";
                        Pagestr += getPageStyle("<font face=webdings title=\"尾页\">:</font>", postResult_color) + "</div>";
                    }
                    else
                    {
                        Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, (i + 2), getFileName, getFileEXName, isPop) + "\" title=\"下一页\">" + getPageStyle("<font face=webdings>4</font>", postResult_color) + "</a>&nbsp;";
                        Pagestr += "<a " + " href=\"" + getPageresult(ID, ReadType, ContentType, n, getFileName, getFileEXName, isPop) + "\" title=\"尾页\">" + getPageStyle("<font face=webdings>:</font>", postResult_color) + "</a></div>";
                    }
                    break;

            }
            return Pagestr;
        }

        /// <summary>
        /// 处理模板
        /// </summary>
        /// <param name="tempRe">模板实例</param>
        /// <param name="existFlag">该模板是否存在于模板列表</param>
        protected void makeHtmlFile(Template tempRe, bool existFlag, string classID,string NewsID)
        {
            tempRe.ReplaceLabels();
            if (tempRe.MyTempType == TempType.News)
            {
                string FinlContent = tempRe.FinallyContent;
                int pos1 = FinlContent.IndexOf("<!-FS:STAR=");
                int pos2 = FinlContent.IndexOf("FS:END->");
                if (pos1 > -1)
                {
                    int getFiledot = saveNewsPath.LastIndexOf(".");
                    int getFileg = saveNewsPath.LastIndexOf("\\");
                    string getFileName = saveNewsPath.Substring((getFileg + 1), ((getFiledot - getFileg) - 1));
                    string getFileEXName = saveNewsPath.Substring(getFiledot);
                    string PageHead = FinlContent.Substring(0, pos1);
                    string PageEnd = FinlContent.Substring(pos2 + 8);
                    string PageMid = FinlContent.Substring(pos1 + 11, pos2 - pos1 - 11);
                    //string[] ArrayCon = PageMid.Split(new string[] { "[FS:PAGE]" }, StringSplitOptions.RemoveEmptyEntries);
                    //int n = ArrayCon.Length;

                    //if (ArrayCon[n - 1] == null || ArrayCon[n - 1].Trim() == string.Empty)
                    //    n--;
                    Regex re = new Regex(@"(\[FS:PAGE=(?<p>[\s\S]+?)\$\])|(\[FS:PAGE\])", RegexOptions.IgnoreCase);
                    PageMid = re.Replace(PageMid, @"[FS:PAGE]");
                    string[] ArrayCon = Regex.Split(PageMid, @"\[FS:PAGE\]", RegexOptions.IgnoreCase);
                    int n = ArrayCon.Length;
                    for (int i = 0; i < n; i++)
                    {
                        string filepath = SiteRootPath + saveNewsPath;
                        if (i > 0)
                        {
                            int laspot = filepath.LastIndexOf('.');
                            filepath = filepath.Substring(0, laspot) + "_" + (i + 1) + filepath.Substring(laspot);
                        }
                        string PageContent = PageHead + ArrayCon[i] + PageEnd;
                        PageContent=re.Replace(PageContent, "");
                        string getFileContent = General.ReplaceResultPage(NewsID, PageContent.Replace("FS:END->", "").Replace("<!-FS:STAR=", ""), getFileName, getFileEXName, n, (i + 1),0);
                        General.WriteHtml(getFileContent, filepath);
                    }
                    if (n > 0)
                    {
                        if (!existFlag)
                        {
                            templateList.Add(tempRe);
                        }
                        return;
                    }
                }
            }
            General.WriteHtml(tempRe.FinallyContent.Replace("FS:END->", "").Replace("<!-FS:STAR=", ""), SiteRootPath + saveNewsPath);
            if (!existFlag)
            {
                templateList.Add(tempRe);
            }
        }

        /// <summary>
        /// 得到要发布的所有新闻
        /// </summary>
        /// <param name="newsFlag">页面发布种类标志位</param>
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得所有新闻的查询结果</returns>
        protected IDataReader getAllNews()
        {
            switch (fs_newsFlag)
            {
                case 0: return getNewsAll();
                case 1: return getNewsLast();
                case 2: return getNewsUnhtml();
                case 3: return getNewsClasses();
                case 4: return getNewsDate();
                case 5: return getNewsId();
                default: return null;
            }
        }

        /// <summary>
        /// 选择发布所有新闻时，取得所有新闻
        /// </summary> 
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        protected IDataReader getNewsAll()
        {
            return CommonData.DalPublish.GetPublishNewsAll(out nNewsCount);
        }
        /// <summary>
        /// 选择发布最新时，取得所有新闻
        /// </summary>
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        protected IDataReader getNewsLast()
        {
            int ncount;
            IDataReader rd = CommonData.DalPublish.GetPublishNewsLast(Convert.ToInt32(fs_strNewsParams), false, out ncount);
            nNewsCount = Convert.ToInt32(fs_strNewsParams);
            if (ncount < nNewsCount)
            {
                nNewsCount = ncount;
            }
            return rd;
        }
        /// <summary>
        /// 选择只发布未生成时，取得所有新闻
        /// </summary>
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        protected IDataReader getNewsUnhtml()
        {
            int ncount;
            IDataReader rd = CommonData.DalPublish.GetPublishNewsLast(Convert.ToInt32(fs_strNewsParams), true, out ncount);
            nNewsCount = Convert.ToInt32(fs_strNewsParams);
            if (ncount < nNewsCount)
            {
                nNewsCount = ncount;
            }
            return rd;
        }
        /// <summary>
        /// 选择选择栏目时，取得所有新闻
        /// </summary>
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        protected IDataReader getNewsClasses()
        {
            bool IsHtml = false;//还没有起作用
            bool IsDesc = false;//还没有起作用
            if (fs_strNewsParams.IndexOf("#") >= 0)
            {
                //只发布未发布的
                IsHtml = true;
                fs_strNewsParams = fs_strNewsParams.Replace("#", "");
            }
            if (fs_strNewsParams.IndexOf("&") >= 0)
            {
                //倒序
                IsDesc = true;
                fs_strNewsParams = fs_strNewsParams.Replace("&", "");
            }
            string classid = "";
            string[] PublishParam = fs_strNewsParams.Split('$');
            int n = PublishParam.Length;
            for (int i = 0; i < n - 1; i++)
            {
                if (i > 0)
                    classid += ",";
                classid += "'" + PublishParam[i] + "'";
            }
            return CommonData.DalPublish.GetPublishNewsByClass(classid, IsHtml, IsDesc, PublishParam[n - 1], out nNewsCount);
        }
        /// <summary>
        /// 选择按照日期发布时，取得所有新闻
        /// </summary>
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        protected IDataReader getNewsDate()
        {
            DateTime StartTm = Convert.ToDateTime(fs_strNewsParams.Split('$')[0]);
            DateTime EndTm = Convert.ToDateTime(fs_strNewsParams.Split('$')[1]);
            return CommonData.DalPublish.GetPublishNewsByTime(StartTm, EndTm, out nNewsCount);
        }
        /// <summary>
        /// 选择按照ID发布时，取得所有新闻
        /// </summary>
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        protected IDataReader getNewsId()
        {
            int MinID = Convert.ToInt32(fs_strNewsParams.Split('$')[0]);
            int MaxID = Convert.ToInt32(fs_strNewsParams.Split('$')[1]);
            return CommonData.DalPublish.GetPublishNewsByID(MinID, MaxID, out nNewsCount);
        }
        /// <summary>
        /// 得到要发布的所有栏目
        /// </summary>
        /// <param name="ClassFlag">页面发布种类标志位</param>
        /// <returns>取得所有栏目的查询结果</returns>
        protected IDataReader getAllClass()
        {
            switch (fs_ClassFlag)
            {
                case 0: return getClassesAll();
                case 1: return getClassesSelect();
                default: return null;
            }
        }
        /// <summary>
        ///  选择发布所有栏目时，取得所有栏目
        /// </summary>
        /// <param name="strClassParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有栏目的查询结果</returns>
        protected IDataReader getClassesAll()
        {
            if (fs_strClassParams.IndexOf("#") >= 0)
            {
                return CommonData.DalPublish.GetPublishClass(Foosun.Global.Current.SiteID, "", true, out nClassCount);

            }
            else
            {
                return CommonData.DalPublish.GetPublishClass(Foosun.Global.Current.SiteID, "", false, out nClassCount);
            }
        }
        /// <summary>
        /// 选择发选择栏目时，取得所有栏目
        /// </summary>        
        /// <returns>取得符合条件的所有栏目的查询结果</returns>
        protected IDataReader getClassesSelect()
        {
            if (fs_strClassParams != null)
            {
                string classid = "";
                string[] classparm = fs_strClassParams.Split('$');
                for (int i = 0; i < classparm.Length; i++)
                {
                    if (i > 0)
                    {
                        classid += ",";

                    }
                    classid += "'" + classparm[i] + "'";
                }
                return CommonData.DalPublish.GetPublishClass("", classid, true, out nClassCount);
            }
            else
            {
                HProgressBar.Roll("请选择栏目", 0);
                return null;
            }
        }
        /// <summary>
        /// 得到要发布的所有专题
        /// </summary>
        /// <param name="specialFlag">页面发布种类标志位</param>        
        /// <returns>取得所有专题的查询结果</returns>
        protected IDataReader getAllSpecials()
        {
            string spid = string.Empty;
            switch (fs_specialFlag)
            {
                case 0:
                    break;
                case 1:
                    spid = getSpecialsSelect();
                    break;
                default:
                    return null;
            }
            return CommonData.DalPublish.GetPublishSpecial(spid, out nSpecialCount);
        }
        /// <summary>
        /// 选择选择专题时，取得所有专题
        /// </summary>        
        /// <returns>取得符合条件的所有专题的查询结果</returns>
        protected string getSpecialsSelect()
        {
            if (fs_strSpecialParams != null)
            {
                string reslt = "";
                string[] spparam = fs_strSpecialParams.Split('$');
                for (int i = 0; i < spparam.Length; i++)
                {
                    if (i > 0)
                        reslt += ",";
                    reslt += "'" + spparam[i] + "'";
                }
                return reslt;
            }
            else
            {
                HProgressBar.Roll("请选择专题", 0);
                return "";
            }
        }

        /// <summary>
        /// 判断是否是快速发布
        /// </summary>
        /// <returns></returns>
        private bool PublisStates
        {
            get 
            {
                if (_publisStateStr == null)
                {
                    _publisStateStr = Public.readparamConfig("publishState");
                }                
                try
                {
                    if (_publisStateStr == null || _publisStateStr.Equals(""))//默认为快速发布
                    {
                        return true;
                    }
                    if (_publisStateStr.Equals("True"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return true;
                }
            }
        }

        #region 发布单页
        private void ultiPublishIsPage()
        {
            string classid = "";
            string[] classparm = fs_strClassIsPageParam.Split('$');
            for (int i = 0; i < classparm.Length; i++)
            {
                if (i > 0)
                {
                    classid += ",";

                }
                classid += "'" + classparm[i] + "'";
            }
            IDataReader rd = CommonData.DalPublish.GetPublishClass("", classid, true, out nClassCount);


            int num = 0;
            int succeedNum = 0;
            int failedNum = 0;
            bool HasRows = false;
            string indexname = "index.html";
            indexname = Foosun.Common.Public.readparamConfig("IndexFileName");
            if (nClassCount > 0)
            {
                while (rd.Read())
                {
                    HasRows = true;
                    TempletPath = rd["classtemplet"].ToString();
                    TempletPath = TempletPath.Replace("/", "\\");
                    TempletPath = TempletPath.ToLower().Replace("{@dirtemplet}", strTempletDir);
                    string TmpsaveClassPath = "\\" + rd["savePath"].ToString().Trim('\\').Trim('/');
                    saveClassPath = TmpsaveClassPath.Replace("/", "\\");
                    string strClassId = rd["classid"].ToString();
                    bool state = Foosun.Publish.General.publishPage(strClassId);
                    num += 1;
                    if (state)
                    {
                        succeedNum++;
                    }
                    else
                    {
                        failedNum++;
                    }
                    string msg = string.Format(MsgFormat, "单页", nClassCount.ToString(), ((int)num).ToString(), succeedNum.ToString(), failedNum.ToString(), "发布单页成功&nbsp;&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a>&nbsp;&nbsp;<a class=\"list_link\" href=\"../../" + indexname + "\" target=\"_blank\">浏览首页</a>");
                    HProgressBar.Roll(msg, (num * 100 / nClassCount));
                }
            }
            else
            {
                HProgressBar.Roll("没有单页", 0);
            }
            if (!HasRows)
            {
                string msg = string.Format(MsgFormat, "单页", nClassCount.ToString(), ((int)num).ToString(), succeedNum.ToString(), failedNum.ToString(), "发布单页成功&nbsp;&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a>&nbsp;&nbsp;<a class=\"list_link\" href=\"../../" + indexname + "\" target=\"_blank\">浏览首页</a>");
                if (nClassCount > 0)
                {
                    HProgressBar.Roll(msg, (num * 100 / nClassCount));
                }
                else
                {
                    HProgressBar.Roll("没有单页", 0);
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
                    Foosun.Common.Public.savePublicLogFiles("□□□发布单页", "【ID】:" + failedList[i].Split('$')[0] + "\r\n【错误描述：】\r\n" + failedList[i].Split('$')[1], "");
                }
                failedList.Clear();
            }
        }
        #endregion

        #region 显示信息实体
        /// <summary>
        /// 显示信息实体
        /// </summary>
        private class showMessage
        {
            private string _indexname = null;
            private int _maxPublishNumber = 0;
            private int _thisPublisCount = 0;
            private int _success = 0;
            private int _error = 0;
            private int _barNum = 0;

            private DataTable _ThreadRt = null;
            private bool _ThreadFlag = false;

            public string Indexname
            {
                get { return _indexname; }
                set { this._indexname = value; }
            }
            public int MaxPublishNumber
            {
                get { return _maxPublishNumber; }
                set { this._maxPublishNumber = value; }
            }
            public int ThisPublisCount
            {
                get { return _thisPublisCount; }
                set { this._thisPublisCount = value; }
            }
            public int Success
            {
                get { return _success; }
                set { this._success = value; }
            }
            public int Error
            {
                get { return _error; }
                set { this._error = value; }
            }
            public int BarNum
            {
                get { return _barNum; }
                set { this._barNum = value; }
            }
            public DataTable ThreadRt
            {
                get { return _ThreadRt; }
                set { this._ThreadRt = value; }
            }
            public bool ThreadFlag
            {
                get { return _ThreadFlag; }
                set { this._ThreadFlag = value; }
            }
        }
        #endregion
    }   
}