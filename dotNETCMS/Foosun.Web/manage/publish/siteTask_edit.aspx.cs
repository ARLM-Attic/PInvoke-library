//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                     Forum:bbs.foosun.net                        ==
//==                     WebSite:www.foosun.net                      ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                   Tel:86-28-85098980/66026180                   ==
//==                   QQ:655071,MSN:ikoolls@gmail.com               ==
//==                   Email:Service@foosun.cn                       ==
//==                      Code By ChenZhaoHui                        ==
//=====================================================================
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.CMS;
using Foosun.CMS.Common;

public partial class manage_publish_siteTask_edit : Foosun.Web.UI.ManagePage
{
    #region
    rootPublic rd = new rootPublic();
    Psframe pd = new Psframe();
    public string classidd = "";//取classid的值
    public string newsidd = "";//取newsid的值
    public string specialidd = "";//取special的值
    public string timesett = "";//定时
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.CacheControl = "no-cache"; //设置页面无缓存
            
            //LoginInfo.CheckPop("权限代码", "0", "1", "9"); //权限代码
            copyright.InnerHtml = CopyRight;
            Start();//初始参数设置信息  
        }

        /// <summary>
        /// 获取新闻,栏目,专题列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        #region 获取新闻,栏目,专题列表
        divClassNews.InnerHtml = getClassNews("divclassNews");
        divClassClass.InnerHtml = getClassNews("divclassClass");
        DivSpecial.InnerHtml = getSpecial();
        #endregion
    }

    /// <summary>
    /// 初始参数设置信息
    /// </summary>
    /// <returns></returns>
    ///code by chenzhaohui 

    protected void Start()
    { 
        string taskid = Request.QueryString["taskID"];
        if (taskid == null || taskid == "" || taskid == string.Empty)
        {
            PageError("参数错误", "siteTask.aspx");
        }
        else
        {
            DataTable dt = pd.getTaskIDInfo(taskid);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.TaskName.Text = dt.Rows[0]["TaskName"].ToString();//任务名称

                #region 发布首页
                if (dt.Rows[0]["isIndex"].ToString() == "1")
                {
                    isIndex.Checked = true;
                }
                else
                {
                    isIndex.Checked = false;
                }
                #endregion

                this.CreatTime.Text = dt.Rows[0]["CreatTime"].ToString();//创建时间

                #region 取栏目数组的值
                string class_value = dt.Rows[0]["ClassID"].ToString();//取classid的值

                string all_class = "";//所有
                string every_class = "";//每天
                string today_class = "";//今天
                
                all_class = class_value.Split('|')[0];//所有
                every_class = class_value.Split('|')[1];//每天
                today_class = class_value.Split('|')[2];//今天
                                
                #region 程序版本
                string str_publicType = Foosun.Config.verConfig.PublicType;
                #endregion
                #region 版本1
                if (str_publicType == "1")
                {
                    if (all_class == "1")
                    {
                        AllClass1.Checked = true;
                    }
                    else
                    {
                        AllClass1.Checked = false;
                    }
                    if (every_class == "1")
                    {
                        EveryDayClass1.Checked = true;
                    }
                    else
                    {
                        EveryDayClass1.Checked = false;
                    }
                    if (today_class == "1")
                    {
                        TodayClass1.Checked = true;
                    }
                    else
                    {
                        TodayClass1.Checked = false;
                    }
                }
                #endregion
                #region 版本0
                else 
                {
                    if (all_class == "1")
                    {
                        AllClass0.Checked = true;
                    }
                    else
                    {
                        AllClass0.Checked = false;
                    }
                    if (today_class == "1")
                    {
                        TodayClass0.Checked = true;
                    }
                    else
                    {
                        TodayClass0.Checked = false;
                    }
                    every_class = "0";
                }
                #endregion
                
                #endregion              
            }
            else
            {
                PageError("参数错误","shortcut_list.aspx");
            }
        }
        Startt();
    }

    /// <summary>
    /// 显示栏目修改时选择的值(js回调)
    /// </summary>
    /// code by chenzhaohui

    protected void showjs()
    {
         string taskid = Request.QueryString["taskID"];
         if (taskid == null || taskid == "" || taskid == string.Empty)
         {
             PageError("参数错误", "siteTask.aspx");
         }
         else
         {
             DataTable dt = pd.getTaskIDInfo(taskid);
             if (dt != null && dt.Rows.Count > 0)
             {
                 string class_value = dt.Rows[0]["ClassID"].ToString();//取classid的值
                 string news = dt.Rows[0]["News"].ToString();//取news的值
                 string special = dt.Rows[0]["Special"].ToString();//取special的值
                 string time = dt.Rows[0]["TimeSet"].ToString();//取timeset的值
                 
                 #region classid数组
                 classidd = class_value.Split('|')[3];//classid数组
                 Response.Write("<script language=\"javascript\">PageClass('" + classidd + "');</script>\r");
                 #endregion

                 #region news数组
                 newsidd = news.Split('|')[4];//newsid数组
                 Response.Write("<script language=\"javascript\">PageNews('" + newsidd + "');</script>\r");
                 #endregion

                 #region special数组
                 specialidd = special;//special数组
                 Response.Write("<script language=\"javascript\">PageSpecial('" + specialidd + "');</script>\r");
                 #endregion

                 #region 定时
                 timesett = time;//timeset数组
                 Response.Write("<script language=\"javascript\">PageTimeSet('" + timesett + "');</script>\r");
                 #endregion
             }
         }
    }

    protected void Startt()
    { 
        
        string taskid = Request.QueryString["taskID"].ToString();
        if (taskid == null || taskid == "" || taskid == string.Empty)
        {
            PageError("参数错误", "siteTask.aspx");
        }
        else
        { 
             DataTable dt = pd.getTaskIDInfo(taskid);
             if (dt != null && dt.Rows.Count > 0)
             {
                 string news = dt.Rows[0]["News"].ToString();//取news的值
                 string newsidd = "";//按ID发布
                 string newstime = "";//按日期发布
                 string lastnews = "";//按最新数发布
                 string unhtml = "";
                 if (news.Split('|')[0] == "1")
                 {
                     Response.Write("<script language=\"javascript\">DispChange(9);</script>\r");
                     this.AllNews.Checked = true;
                 }
                 else
                 {
                     this.AllNews.Checked = false;
                 }

                 newsidd = news.Split('|')[1];
                 if (news.Split('|')[1] != "0")
                 {
                     //Response.Write("<script language=\"javascript\">DispChange(0);</script>\r");
                     this.NewsID1.Text = newsidd.Split(',')[0];
                     this.NewsID2.Text = newsidd.Split(',')[1];
                     this.NewsID.Checked = true;
                 }
                 else
                 {
                     this.NewsID1.Text = "";
                     this.NewsID2.Text = "";
                     this.NewsID.Checked = false;
                 }

                 newstime = news.Split('|')[2];
                 if (news.Split('|')[2] != "0")
                 {
                     //Response.Write("<script language=\"javascript\">DispChange(1);</script>\r");
                     this.Data1.Text = newstime.Split(',')[0];
                     this.Data2.Text = newstime.Split(',')[1];
                     this.Data.Checked = true;
                 }
                 else
                 {
                     this.Data1.Text = "";
                     this.Data2.Text = "";
                     this.Data.Checked = false;
                 }

                 lastnews = news.Split('|')[3];
                 if (news.Split('|')[3] != "0")
                 {
                     //Response.Write("<script language=\"javascript\">DispChange(2);</script>\r");
                     this.LastNewsNum.Text = lastnews;
                     this.LastNewsNum_checkbox.Checked = true;
                 }
                 else
                 {
                     this.LastNewsNum.Text = "";
                     this.LastNewsNum_checkbox.Checked = false;
                 }
                 unhtml = news.Split('|')[5];
                 if (news.Split('|')[5] != "0")
                 {
                     for (int k = 0; k < 2; k++)
                     {
                         if (this.unHTML.Items[k].Value == unhtml)
                         {
                             this.unHTML.Items[k].Selected = true;
                         }
                     }
                 }
             }
        }
    }

    /// <summary>
    /// 保存修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    protected void Savetask_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string taskid = Request.QueryString["taskid"];
            if (taskid == null || taskid == "" || taskid == string.Empty)
            {
                PageError("参数错误", "siteTask.aspx");
            }
            else
            {
                string Str_TaskName = this.TaskName.Text.Trim();//任务名

                #region 发布首页
                int isindexx = 0;
                if (isIndex.Checked)
                {
                    isindexx = 1;
                }
                else
                {
                    isindexx = 0;
                }
                #endregion

                #region 定时
                string Str_TimeSet = Request.Form["TimeSet"];
                if (Str_TimeSet == null || Str_TimeSet == "" || Str_TimeSet == string.Empty)
                {
                    Str_TimeSet = "0";
                }
                else
                {
                    Str_TimeSet = Request.Form["TimeSet"];
                }
                #endregion

                string Str_CreateTime = this.CreatTime.Text.Trim();//创建时间
                #region checkdata
                if (Foosun.Common.Input.ChkDate(Str_CreateTime) == false)
                {
                    PageError("日期格式不正确", "siteTask.aspx");
                }
                #endregion


                #region 栏目索引

                int all_class = 0, every_class = 0, today_class = 0;
                string str_publictype = Foosun.Config.verConfig.PublicType;
                if (str_publictype == "1")
                {
                    if (AllClass1.Checked)
                    {
                        all_class = 1;
                    }
                    else
                    {
                        all_class = 0;
                    }
                    if (EveryDayClass1.Checked)
                    {
                        every_class = 1;
                    }
                    else
                    {
                        every_class = 0;
                    }
                    if (TodayClass1.Checked)
                    {
                        today_class = 1;
                    }
                    else
                    {
                        today_class = 0;
                    }
                }
                else
                {
                    if (AllClass1.Checked)
                    {
                        all_class = 1;
                    }
                    else
                    {
                        all_class = 0;
                    }
                    if (TodayClass1.Checked)
                    {
                        today_class = 1;
                    }
                    else
                    {
                        today_class = 0;
                    }
                    every_class = 0;
                }
                #endregion

                #region 栏目数组
                string Str_ClassID = Request.Form["divclassNews"];
                if (Str_ClassID == null || Str_ClassID == "" || Str_ClassID == string.Empty)
                {
                    Str_ClassID = "0";
                }
                else
                {
                    Str_ClassID = Request.Form["divclassNews"];
                }
                #endregion

                #region 生成所有新闻
                int allnews = 0;
                if (AllNews.Checked)
                {
                    allnews = 1;
                }
                else
                {
                    allnews = 0;
                }
                #endregion

                #region 新闻ID
                string Str_NewsID = "0";
                if (NewsID.Checked)
                {
                    string Str_NewsID1 = Foosun.Common.Input.Filter(this.NewsID1.Text.Trim());
                    string Str_NewsID2 = Foosun.Common.Input.Filter(this.NewsID2.Text.Trim());
                    if (Str_NewsID1 == null || Str_NewsID1 == "" || Str_NewsID1 == string.Empty || Str_NewsID2 == null || Str_NewsID2 == "" || Str_NewsID2 == string.Empty)
                    {
                        Str_NewsID = "0";
                    }
                    else
                    {
                        if (!Foosun.Common.Input.IsInteger(Str_NewsID1) || !Foosun.Common.Input.IsInteger(Str_NewsID2))
                        {
                            PageError("抱歉，ID必须为数字", "siteTask.aspx");
                        }
                        else
                        {
                            if (int.Parse(Str_NewsID1) > int.Parse(Str_NewsID2))
                            {
                                PageError("抱歉，第一个ID必须比第二个小", "siteTask.aspx");
                            }
                            else
                            {
                                Str_NewsID = "" + Str_NewsID1 + "," + Str_NewsID2 + "";
                            }
                        }
                    }
                }
                #endregion

                #region 新闻日期
                string Str_Data = "0";
                if (Data.Checked)
                {
                    string Str_Data1 = Foosun.Common.Input.Filter(this.Data1.Text.Trim());
                    string Str_Data2 = Foosun.Common.Input.Filter(this.Data2.Text.Trim());
                    if (Str_Data1 == null || Str_Data1 == "" || Str_Data1 == string.Empty || Str_Data2 == null || Str_Data2 == "" || Str_Data2 == string.Empty)
                    {
                        Str_Data = "0";//不按照日期发布
                    }
                    else
                    {
                        #region check data
                        if (Foosun.Common.Input.ChkDate(Str_Data1) == false || Foosun.Common.Input.ChkDate(Str_Data2) == false)
                        {
                            PageError("日期格式不正确", "siteTask.aspx");
                        }
                        #endregion
                        else
                        {
                            Str_Data = "" + Str_Data1 + "," + Str_Data2 + "";
                        }
                    }
                }
                #endregion

                #region 最新数

                int Str_LastNewsNum = 0;
                if (LastNewsNum_checkbox.Checked)
                {
                    string lastnum = this.LastNewsNum.Text.Trim();
                    if (lastnum == null || lastnum == "" || lastnum == string.Empty)
                    {
                        Str_LastNewsNum = 0;
                    }
                    else
                    {
                        if (!Foosun.Common.Input.IsInteger(lastnum))
                        {
                            PageError("最新数应为数字型", "siteTask.aspx");
                        }
                        else
                        {
                            Str_LastNewsNum = int.Parse(lastnum);
                        }
                    }
                }
                #endregion

                #region 新闻数组
                string Str_ArrNewsID = Request.Form["divclassClass"];
                if (Str_ArrNewsID == null || Str_ArrNewsID == "" || Str_ArrNewsID == string.Empty)
                {
                    Str_ArrNewsID = "0";
                }
                else
                {
                    Str_ArrNewsID = Request.Form["divclassClass"];
                }
                #endregion

                #region 专题数组
                string Str_Special = Request.Form["SpecialID"];
                if (Str_Special == null || Str_Special == "" || Str_Special == string.Empty)
                {
                    Str_Special = "0";
                }
                else
                {
                    Str_Special = Request.Form["SpecialID"];
                }
                #endregion

                #region 修改数据

                Foosun.Model.Task uc = new Foosun.Model.Task();
                uc.taskID = taskid;
                uc.TaskName = Str_TaskName;
                uc.isIndex = isindexx;
                uc.ClassID = all_class + "|" + every_class + "|" + today_class + "|" + Str_ClassID;
                uc.News = allnews + "|" + Str_NewsID + "|" + Str_Data + "|" + Str_LastNewsNum + "|" + Str_ArrNewsID+"|"+this.unHTML.SelectedValue;
                uc.Special = Str_Special;
                uc.TimeSet = Str_TimeSet;
                uc.CreatTime = DateTime.Parse((DateTime.Now).ToString());
                uc.SiteID = SiteID;
                pd.UpdateTask(uc);
                PageRight("修改成功", "siteTask.aspx");
                #endregion

                #region 更新数据库
                #endregion
            }
        }
    }
    /// <summary>
    /// 得到栏目父类列表
    /// </summary>
    /// <param name="flgStr"></param>
    /// <returns></returns>
    protected string getClassNews(string flgStr)
    {
        string _Str = "\r<select name=\"" + flgStr + "\" size=\"10\" multiple=\"multiple\" style=\"width:100%\">\r";
        DataTable dt = rd.getClassListPublic("0");
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _Str += "<option value=\"" + dt.Rows[i]["ClassID"].ToString() + "\">┝" + dt.Rows[i]["ClassCName"].ToString() + "</option>\r";
                    _Str += getChildClassNews(dt.Rows[i]["ClassID"].ToString(), "┝┉┉");
                }
            }
            dt.Clear(); dt.Dispose();
        }
        _Str += "</select>\r";
        return _Str;
    }

    /// <summary>
    /// 得到子类列表
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="_tmp"></param>
    /// <returns></returns>
    protected string getChildClassNews(string ParentID, string _tmp)
    {
        string _Str = "";
        DataTable dt = rd.getClassListPublic(ParentID);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _Str += "<option value=\"" + dt.Rows[i]["ClassID"].ToString() + "\">" + _tmp + dt.Rows[i]["ClassCName"].ToString() + "</option>\r";
                    _Str += getChildClassNews(dt.Rows[i]["ClassID"].ToString(), "┝" + _tmp);
                }
            }
            dt.Clear(); dt.Dispose();
        }
        return _Str;
    }

    /// <summary>
    /// 得到专题父类列表
    /// </summary>
    /// <param name="flgStr"></param>
    /// <returns></returns>
    protected string getSpecial()
    {
        string _Str = "\r<select name=\"SpecialID\" size=\"10\" multiple=\"multiple\" style=\"width:100%\">\r";
        DataTable dt = rd.getSpecialListPublic("0");
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _Str += "<option value=\"" + dt.Rows[i]["SpecialID"].ToString() + "\">┝" + dt.Rows[i]["SpecialCName"].ToString() + "</option>\r";
                    _Str += getChildSpecial(dt.Rows[i]["SpecialID"].ToString(), "┝┉┉");
                }
            }
            dt.Clear(); dt.Dispose();
        }
        _Str += "</select>\r";
        return _Str;
    }

    /// <summary>
    /// 得到专题子类列表
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="_tmp"></param>
    /// <returns></returns>
    protected string getChildSpecial(string ParentID, string _tmp)
    {
        string _Str = "";
        DataTable dt = rd.getSpecialListPublic(ParentID);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _Str += "<option value=\"" + dt.Rows[i]["SpecialID"].ToString() + "\">" + _tmp + dt.Rows[i]["SpecialCName"].ToString() + "</option>\r";
                    _Str += getChildClassNews(dt.Rows[i]["SpecialID"].ToString(), "┝" + _tmp);
                }
            }
            dt.Clear(); dt.Dispose();
        }
        return _Str;
    }
}
