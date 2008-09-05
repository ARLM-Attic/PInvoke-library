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

public partial class manage_publish_siteTask_add : Foosun.Web.UI.ManagePage
{
    public string Str_CreatTime;
    rootPublic rd = new rootPublic();
    Psframe pd = new Psframe();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.CacheControl = "no-cache";                        //设置页面无缓存
            
            //LoginInfo.CheckPop("权限代码", "0", "1", "9");             //权限代码
            copyright.InnerHtml = CopyRight;
        }

        Str_CreatTime = DateTime.Now.ToString();//创建时间
        this.CreatTime.Text = Str_CreatTime;

        /// <summary>
        /// 获取新闻,栏目,专题列表
        /// </summary>

        #region 获取新闻,栏目,专题列表
        divClassNews.InnerHtml = getClassNews("divclassNews");
        divClassClass.InnerHtml = getClassNews("divclassClass");
        DivSpecial.InnerHtml = getSpecial();
        #endregion
    }

    /// <summary>
    /// 新增计划任务
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    protected void Savetask_ServerClick(object sender, EventArgs e)
    {
        #region 判断页面是否验证成功
        if (Page.IsValid)
        {
            #region 开始取表单里的值

            string Str_TaskName = Foosun.Common.Input.Filter(this.TaskName.Text.Trim());//任务名

            #region 是否生成首页
            int isindexx = 0;
            if (isIndex.Checked)
            {
                isindexx = 1;
            }
            else
            {
                PageError("生成首页 必须选择", "siteTask.aspx");
            }
            #endregion

            #region 定时发布状态
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

            #region checkdata
            if (Foosun.Common.Input.ChkDate(Str_CreatTime)==false)
            {
                PageError("日期格式不正确", "siteTask.aspx");
            }
            #endregion

            #region 开始取栏目设置里的值

            #region 程序版本,如果为1，则显示每天生成一页并取其值，否则不。
            string str_publicType = Foosun.Config.verConfig.PublicType;

            #region 栏目索引复选？
            int all_class = 0, every_class = 0, today_class = 0;
            #endregion

            #region 程序版本,按照的版本来取值

            #region 程序版本1取值
            if (str_publicType == "1")
            {
                if (AllClass1.Checked){all_class = 1;}
                else{all_class = 0;}
                if (EveryDayClass1.Checked){every_class = 1;}
                else{every_class = 0;}
                if (TodayClass1.Checked){today_class = 1;}
                else{today_class = 0;}
            }
            #endregion
            #region 程序版本0取值
            else
            {
                if (AllClass0.Checked){all_class = 1;}
                else{all_class = 0;}
                if (TodayClass0.Checked){today_class = 1;}
                else{today_class = 0;}
                every_class = 0;
            }
            #endregion
            #endregion
            #endregion

            #region 取classid值,从下拉列表取值
            string Str_ClassPublish = Request.Form["divclassNews"];
            if (Str_ClassPublish == null || Str_ClassPublish == "" || Str_ClassPublish == string.Empty)
            {
                Str_ClassPublish = "0";
            }
            else
            {
                Str_ClassPublish = Request.Form["divclassNews"];
            }
            #endregion

            #endregion

            #region 开始取新闻设置里的值

            #region 是否生成所有新闻
            int allnewss = 0;
            if (AllNews.Checked)
            {
                allnewss = 1;
            }
            else
            {
                allnewss = 0;
            }
            #endregion

            #region 按照新闻ID

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
            else
            {
                Str_NewsID = "0";//不按照ID发布
            }
            #endregion

            #region 按照日期生成

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
            else
            {
                Str_Data = "0";//不按照日期发布
            }
            #endregion

            #region 按照最新生成数量

            int Str_LastNewsNum = 0;
            if (LastNewsNum_checkbox.Checked)
            {
                string lastnum = Foosun.Common.Input.Filter(this.LastNewsNum.Text.Trim());
                if (lastnum == null || lastnum == "" || lastnum == string.Empty)
                {
                    Str_LastNewsNum = 0;
                }
                else
                {
                    if (!Foosun.Common.Input.IsInteger(lastnum))
                    {
                        PageError("抱歉，最新数必须为数字型", "siteTask.aspx");
                    }
                    else
                    {
                        Str_LastNewsNum = int.Parse(lastnum);
                    }
                }
            }
            else
            {
                Str_LastNewsNum = 0;//不按照此方式发布
            }
            #endregion

            #region 取classid值,从下拉列表取值
            string Str_ClassPublish_News = Request.Form["divclassClass"];
            if (Str_ClassPublish_News == null || Str_ClassPublish_News == "" || Str_ClassPublish_News == string.Empty)
            {
                Str_ClassPublish_News = "0";
            }
            else
            {
                Str_ClassPublish_News = Request.Form["divclassClass"];
            }
            #endregion

            #endregion

            #region 开始取专题设置里的值
            #region 取classid值,从下拉列表取值
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

            #endregion

            #endregion

            #region 12位随机数
            tcheck: string Str_taskID = Foosun.Common.Rand.Number(12);
                DataTable rdTF = pd.getTaskParam(Str_taskID);
                if(rdTF.Rows.Count>0)
                    goto tcheck;
                rdTF.Clear(); rdTF.Dispose();
            #endregion

            #region 检查是否有已经存在的名称
            DataTable nTF = pd.getTaskName(Str_TaskName);
            if (nTF!=null)
            {
                if (nTF.Rows.Count > 0)
                {
                    PageError("对不起，该任务名称已经存在", "siteTask.aspx");
                }
                nTF.Clear(); nTF.Dispose();
            }
            #endregion

            #region 插入数据

            Foosun.Model.Task uc = new Foosun.Model.Task();
            uc.taskID = Str_taskID;
            uc.TaskName = Str_TaskName;
            uc.isIndex = isindexx;
            uc.ClassID = all_class + "|" + every_class + "|" + today_class + "|" + Str_ClassPublish;
            uc.News = allnewss + "|" + Str_NewsID + "|" + Str_Data + "|" + Str_LastNewsNum + "|" + Str_ClassPublish_News + "|" + this.unHTML.SelectedValue;
            uc.Special = Str_Special;
            uc.TimeSet = Str_TimeSet;
            uc.CreatTime = DateTime.Parse((DateTime.Now).ToString());
            uc.SiteID = SiteID;
            pd.insertTask(uc);
            PageRight("新增成功", "siteTask.aspx");
            #endregion
        }
        #endregion
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
