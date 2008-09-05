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
using System.ComponentModel;
using System.Drawing;
using System.Web.SessionState;
using Foosun.CMS;
using Foosun.CMS.Common;
using Foosun.Model;

public partial class Manage_Stat_View : Foosun.Web.UI.ManagePage
{
    public Manage_Stat_View()
    {
        Authority_Code = "S001";
    }
    Stat sta = new Stat();
    rootPublic rd = new rootPublic();
    #region 定义全局变量
    public static DataView dv;//公用视图dv
    public string strtheurl;//当前地址
    public string statid;//编号
    #endregion

    #region 取得服务器变量集合
    System.Collections.Specialized.NameValueCollection ServerVariables = System.Web.HttpContext.Current.Request.ServerVariables;
    #endregion

    public string isDataBase = "";//是否是独立的数据库
    public string OnlyBaseConn = "";//独立数据库连接路径

    protected void Page_Load(object sender, EventArgs e)
    {
        #region 在此处放置用户代码以初始化页面

        #region 分页调用函数
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        this.PageNavigator2.OnPageChange += new PageChangeHandler(PageNavigator2_PageChange);
        #endregion

        #region 查询方法返回视图
        DataTable tb = sta.sel();
        dv = tb.DefaultView;
        #endregion

        Response.CacheControl = "no-cache";//设置页面无缓存

        string act = Request.QueryString["act"];
        statmethod.InnerHtml = StatMehtodd();                                   //功能菜单
        if (!IsPostBack)
        {

            copyright.InnerHtml = CopyRight;
            ParamStartLoad();//载入初始参数设置页面数据
            StatList(1);    //分页初始化
            ClassList(1);    //分页初始化
            ZongHeStat();   //综合统计页面数据
            switch (act)
            {
                case "edit":
                    this.Authority_Code = "S003";
                    this.CheckAdminAuthority();
                    EditClass();//修改
                    break;
                case "delone":
                    this.Authority_Code = "S003";
                    this.CheckAdminAuthority();
                    DelOne();//单个删除
                    break;
            }
        }
        ShowNavi.InnerHtml = ShowNaviFunc();                            //功能菜单
        CodeUseTable.InnerHtml = GetCodeUse();                          //代码调用页面
        #endregion
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <returns>取得分页信息</returns>
    /// Code By ChenZhaohui

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        StatList(PageIndex);//管理页面分页查询
    }
    protected void PageNavigator2_PageChange(object sender, int PageIndex)
    {
        ClassList(PageIndex);//类别页面分页查询
    }

    /// <summary>
    /// 初始分类
    /// </summary>
    /// <returns>取得分类信息</returns>
    /// Code By ChenZhaohui

    void EditClass()
    {
        string id = Request.QueryString["id"].ToString();
        #region 从统计分类设置表中读出数据并初始化赋值
        classnameEdit.Text = sta.Str_ClassSql(id);
        #endregion
    }

    /// <summary>
    /// 显示功能菜单
    /// </summary>
    /// <returns>返回功能菜单</returns>
    ///  Code By ChenZhaohui


    #region 显示功能菜单
    string ShowNaviFunc()
    {
        string typ = Request.QueryString["type"];
        string strlist = "";
        if (typ != "zonghe" && typ != "all" && typ != "hour" && typ != "day" && typ != "week" && typ != "month" && typ != "page" && typ != "ip" && typ != "cs" && typ != "area" && typ != "come" && typ != "code")
        {
            strlist += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"Navitable\">\n";
            strlist += "<tr class=\"menulist\"><td height=\"18\" style=\"width: 45%\" colspan=\"2\" >\n";
            string _tmplet = "";
            if (Foosun.Global.Current.SiteID == "0")
            {
                _tmplet = "<a href=\"View.aspx?type=pram\" class=\"menulist\">参数设置</a>&nbsp;┊&nbsp; ";
            }
            strlist += _tmplet + "<a href=\"?type=class\" class=\"menulist\">分类管理</a>\n";
            strlist += "</td></tr></table> \n";
        }
        return strlist;

    }
    #endregion

    /// <summary>
    /// 删除单个分类数据
    /// </summary>
    ///code by chenzhaohui 

    #region 删除单个分类数据
    void DelOne()
    {
        string ID = Request.QueryString["ID"].ToString();
        if (ID == null)
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "统计分类删除单个类别", "参数错误");
            PageError("错误的参数传递!", "View.aspx?type=class");
        }
        else
        {

            bool s1 = sta.Str_classSql(ID);
            bool s2 = sta.Str_statInfo_Sql(ID);
            bool s3 = sta.Str_statContent_Sql(ID);
            //bug修改 周峻平 2005-5-28
            if (s1)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "统计分类删除单个类别", "删除成功");
                PageRight("删除成功。", "View.aspx?type=class");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "统计分类删除单个类别", "意外错误");
                PageError("意外错误：未知错误", "shortcut_list.aspx");
            }
        }
    }
    #endregion

    /// <summary>
    /// 初始化参数设置页面
    /// </summary>
    ///code by chenzhaohui 

    #region 初始化参数设置页面
    void ParamStartLoad()
    {
        #region 从统计参数设置表中读出数据并初始化赋值
        DataTable dt = sta.sel();
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                #region 统计参数设置
                SystemName.Text = dt.Rows[0]["SystemName"].ToString();
                SystemNameE.Text = dt.Rows[0]["SystemNameE"].ToString();
                ipCheck.Text = dt.Rows[0]["ipCheck"].ToString();
                ipTime.Text = dt.Rows[0]["ipTime"].ToString();
                isOnlinestat.Text = dt.Rows[0]["isOnlinestat"].ToString();
                pageNum.Text = dt.Rows[0]["pageNum"].ToString();
                cookies.Text = dt.Rows[0]["cookies"].ToString();
                pointNum.Text = dt.Rows[0]["pointNum"].ToString();
                #endregion
            }
        }
        #endregion
    }
    #endregion

    /// <summary>
    /// 保存参数设置事件函数
    /// </summary>
    /// <returns>保存参数设置事件函数</returns>
    /// Code By ChenZhaohui

    protected void savePram_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            if (SiteID != "0")
            {
                PageError("没有权限", "");
            }
            string Str_SystemNameE = Foosun.Common.Input.Filter(this.SystemNameE.Text);//统计系统英文名称
            if (Str_SystemNameE == null || Str_SystemNameE == string.Empty)
            {
                PageError("对不起，系统英文名称不能为空", "View.aspx?type=pram");
            }
            stat_Param sp = new stat_Param();
            sp.SystemName = this.SystemName.Text;
            sp.SystemNameE = Str_SystemNameE;
            sp.ipCheck = int.Parse(this.ipCheck.Text);
            sp.ipTime = int.Parse(this.ipTime.Text);
            sp.isOnlinestat = int.Parse(this.isOnlinestat.Text);
            sp.pageNum = int.Parse(this.pageNum.Text);
            sp.pointNum = int.Parse(this.pointNum.Text);
            sp.SiteID = SiteID;
            sp.cookies = this.cookies.Text;
            #region 向数据库中写入添加的统计参数设置信息
            #endregion

            #region 载入数据-刷新页面
            sta.Str_InSql(sp);
            PageRight("统计系统参数设置成功", "View.aspx?type=pram");
            #endregion
        }
    }

    /// <summary>
    /// 统计分类列表
    /// </summary>
    /// <returns>取得分类管理页</returns>
    /// Code By ChenZhaohui

    protected void ClassList(int PageIndex)//显示详细记录
    {
        int i, j;
        int num = sta.Stat_Sql(); ;//从参数设置里取得每页显示记录的条数
        if (num == 0)
        {
            num = 20;
        }
        DataTable dt = Foosun.CMS.Pagination.GetPage("Manage_Stat_View_1_aspx", PageIndex, num, out i, out j, null);

        this.PageNavigator2.PageCount = j;
        this.PageNavigator2.PageIndex = PageIndex;
        this.PageNavigator2.RecordCount = i;
        if (dt != null)//判断如果dt里面没有内容，将不会显示
        {
            if (dt.Rows.Count > 0)
            {
                //添加列
                dt.Columns.Add("oPerate", typeof(String));//操作
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    int id = int.Parse(dt.Rows[k]["id"].ToString());
                    string classid = dt.Rows[k]["id"].ToString();
                    string statid = dt.Rows[k]["statid"].ToString();

                    dt.Rows[k]["ClassName"] = "<a href=\"View.aspx?act=edit&id=" + statid + "\"  class=\"list_link\" title=\"点击查看详情或修改\">" + dt.Rows[k]["ClassName"].ToString() + "</a>";
                    dt.Rows[k]["oPerate"] = "<a href=\"View.aspx?act=edit&id=" + statid + "\"  class=\"list_link\" title=\"点击查看详情或修改\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改此类\" /></a><a href=\"View.aspx?act=delone&id=" + statid + "\"  class=\"list_link\" title=\"点击删除此项\" onclick=\"{if(confirm('确认删除吗？')){return true;}return false;}\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除此类\" /></a><a href=\"View.aspx?type=zonghe&Navi=view&id=" + statid + "\" class=\"list_link\" title=\"查看该分类下统计信息\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/review1.gif\" border=\"0\" alt=\"查看该分类下统计信息\" /></a><input type='checkbox' name='stat_checkbox' id='stat_checkbox' value=\"" + statid + "\"/>";

                }
            }
            else
            {
                NoContent.InnerHtml = Show_NoContent();
                this.PageNavigator2.Visible = false;
            }
        }
        else
        {
            NoContent.InnerHtml = Show_NoContent();
            this.PageNavigator2.Visible = false;
        }
        DataList2.DataSource = dt;
        DataList2.DataBind();
    }

    /// <summary>
    /// 当没有内容时显示没有内容提示
    /// </summary>
    /// <returns>当没有内容时显示没有内容提示</returns>
    /// Code By ChenZhaohui

    string Show_NoContent()
    {
        string type = Request.QueryString["type"];
        string nos = "";
        if (type == "class")
        {
            nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
            nos = nos + "<tr class='TR_BG_list'>";
            nos = nos + "<td class='navi_link'>当前没有满足条件的分类！</td>";
            nos = nos + "</tr>";
            nos = nos + "</table>";
        }
        return nos;
    }

    /// <summary>
    /// 功能菜单
    /// </summary>
    /// <returns>功能菜单</returns>
    /// Code By ChenZhaohui

    string StatMehtodd()
    {
        //取得传递的参数值
        string id = Request.QueryString["id"];
        //显示功能菜单
        string liststr = "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"Navitable\">";
        liststr = liststr + "<tr class=\"menulist\">\r";
        liststr = liststr + "<td height=\"18\" style=\"width: 45%\" colspan=\"2\"><div align=\"left\"> <a href=\"?type=class\" class=\"menulist\"><font color=\"#ff0000\">返回统计分类:</font></a>&nbsp;┊&nbsp;<a href=\"?type=zonghe&Navi=view&id=" + id + "\" class=\"menulist\">综合统计</a>&nbsp;┊&nbsp;<a href=\"?type=all&Navi=view&id=" + id + "\" class=\"menulist\">详细记录</a>&nbsp;┊&nbsp;<a href=\"?type=hour&Navi=view&id=" + id + "\" class=\"menulist\">24小时统计</a>&nbsp;┊&nbsp;<a href=\"?type=day&Navi=view&id=" + id + "\" class=\"menulist\">日统计</a>&nbsp;┊&nbsp;<a href=\"?type=week&Navi=view&id=" + id + "\" class=\"menulist\">周统计</a>&nbsp;┊&nbsp;<a href=\"?type=month&Navi=view&id=" + id + "\" class=\"menulist\">月统计</a>&nbsp;┊&nbsp;<a href=\"?type=page&Navi=view&id=" + id + "\" class=\"menulist\">被访页面</a>&nbsp;┊&nbsp;<a href=\"?type=ip&Navi=view&id=" + id + "\" class=\"menulist\">IP统计</a>&nbsp;┊&nbsp;<a href=\"?type=cs&Navi=view&id=" + id + "\" class=\"menulist\">客户端</a>&nbsp;┊&nbsp;<a href=\"?type=area&Navi=view&id=" + id + "\" class=\"menulist\">地区</a>&nbsp;┊&nbsp;<a href=\"?type=come&Navi=view&id=" + id + "\" class=\"menulist\"> 来路统计</a>&nbsp;┊&nbsp;<a href=\"?type=code&Navi=view&id=" + id + "\" class=\"menulist\"> 代码调用</a></div></td>\r";
        liststr = liststr + "</tr>\r";
        liststr = liststr + "</table>";
        return liststr;
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <returns>批量删除</returns>
    /// Code By ChenZhaohui

    protected void DelP_Click(object sender, EventArgs e)
    {
        this.Authority_Code = "S003";
        this.CheckAdminAuthority();
        string stat_checkbox = Request.Form["stat_checkbox"];
        if (stat_checkbox == null || stat_checkbox == String.Empty)
        {
            PageError("请先选择批量操作的内容!", "");
        }
        else
        {
            String[] CheckboxArray = stat_checkbox.Split(',');
            stat_checkbox = null;
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                #region 删除分类下的统计信息
                sta.Vote_Sql(CheckboxArray[i]);
                sta.Str_statInfo_Sql_1(CheckboxArray[i]);
                sta.Str_statContent_Sql_1(CheckboxArray[i]);
                #endregion
            }
            rd.SaveUserAdminLogs(1, 1, UserNum, "批量删除统计系统分类", "删除数据成功");
            PageRight("删除数据成功,请返回继续操作!", "View.aspx?type=class");
        }
        rd.SaveUserAdminLogs(1, 1, UserNum, "批量删除统计系统分类", "删除数据失败");
        PageError("删除数据失败,请与管理联系!", "");
    }

    /// <summary>
    /// 删除全部
    /// </summary>
    /// <returns>删除全部</returns>
    /// Code By ChenZhaohui

    protected void DelAll_Click(object sender, EventArgs e)
    {
        this.Authority_Code = "S003";
        this.CheckAdminAuthority();
        bool s1 = sta.Str_StatSql();
        bool s2 = sta.Str_DelAllinfo_Sql();
        bool s3 = sta.Str_DelContent_Sql();
        if (s1 && s2 && s3)
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "全部删除统计系统分类", "删除数据成功");
            PageRight("删除全部成功。", "View.aspx?type=class");
        }
        else
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "全部删除统计系统分类", "意外错误：未知错误");
            PageError("意外错误：未知错误", "View.aspx?type=class");
        }
    }

    /// <summary>
    /// 增加分类
    /// </summary>
    /// <returns>增加分类</returns>
    /// Code By ChenZhaohui

    protected void stataddclass_ServerClick(object sender, EventArgs e)
    {
        this.Authority_Code = "S003";
        this.CheckAdminAuthority();
        if (Page.IsValid)//判断页面是否通过验证
        {
            //取得设置添加中的表单信息
            string Str_Classname = Foosun.Common.Input.Filter(this.ClassName.Text.Trim());//类别
            #region 检查重复数据

        check: string Str_statid = Foosun.Common.Rand.Number(12);
            if (sta.sel_1(Str_statid) != 0)
                goto check;
            #endregion

            //检查是否有已经存在的类别名称
            if (sta.Str_CheckSql(Str_Classname) != 0)
            {
                PageError("对不起，该类别已经存在", "View.aspx?act=add");
            }
            //检查主题名是否为空
            if (Str_Classname == null || Str_Classname == string.Empty)
            {
                PageError("对不起，类别名称不能为空，请返回继续添加", "View.aspx?act=add");
            }
            //向数据库中写入添加的类别信息

            //载入数据-刷新页面
            if (sta.Str_InSql_1(Str_statid, Str_Classname, SiteID) != 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "新增统计类别", "新增类别成功");
                PageRight("新增类别成功", "View.aspx?type=class");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "新增统计类别", "意外错误");
                PageError("意外错误：未知错误", "shortcut_list.aspx");
            }

        }
    }

    /// <summary>
    /// 修改分类
    /// </summary>
    /// <returns>修改分类</returns>
    /// Code By ChenZhaohui

    protected void Editsave_ServerClick(object sender, EventArgs e)
    {
        this.Authority_Code = "S003";
        this.CheckAdminAuthority();
        if (Page.IsValid)//判断页面是否通过验证
        {
            string id = Request.QueryString["id"];
            //取得设置修改中的表单信息
            string Str_ClassnameE = Foosun.Common.Input.Filter(this.classnameEdit.Text.Trim());//类别
            //检查类别名是否为空
            if (Str_ClassnameE == null || Str_ClassnameE == string.Empty)
            {
                PageError("对不起，类别名称不能为空，请返回继续添加", "View.aspx?act=add");
            }
            //向数据库中写入添加的类别信息

            //载入数据-刷新页面
            if (sta.Str_UpdateSql(Str_ClassnameE, id) != 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "修改统计系统分类", "修改成功");
                PageRight("修改类别成功", "View.aspx?type=class");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "修改统计系统分类", "意外错误");
                PageError("意外错误：未知错误", "shortcut_list.aspx");
            }

        }
    }

    /// <summary>
    /// 清空所有统计信息
    /// </summary>
    /// <returns>清空所有统计信息</returns>
    /// Code By ChenZhaohui

    protected void ClearAll_Click(object sender, EventArgs e)
    {
        //更新数据
        if (sta.Str_StatSql_1() != 0 && sta.Str_StatSqlZ() != 0)
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "清空所有统计信息", "清空统计表中数据成功");
            PageRight("恭喜!清空统计表中数据成功。", "View.aspx?type=class");
        }
        else
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "清空所有统计信息", "意外错误");
            PageError("意外错误：未知错误", "");
        }
    }


    /// <summary>
    /// 综合统计 的摘要说明。
    /// </summary>
    /// Code By ChenZhaoHui

    #region 综合统计
    void ZongHeStat()//综合统计页面数据控制显示代码
    {
        int total;//总访问量
        string starttimee;//开始统计时间 
        int highest;//最高访问量
        string highesttime;//最高访问量时间
        int onlinePerson;//在线人数
        int inttoday;//今日访问量
        int intyesterday;//昨日访问量
        int intthisyear;//今年访问量
        int intthismonth;//本月访问量
        double viewdayavg;//平均日访问量
        double viewdays;//访问天数
        int inttodayguess;//预计今日
        int intuserviewN;//当前人员访问量
        string Str_sysNameE = dv[0].Row["SystemNameE"].ToString();
        #region 参数传递，得到相应类别下的统计
        string viewid = Request.QueryString["id"];
        #endregion

        /// <summary>
        /// 综合统计 开始处理统计数据。
        /// </summary>
        /// Code By ChenZhaoHui

        DataView dv1 = new DataView();
        DataTable dt = sta.sel_2(viewid, Foosun.Global.Current.SiteID);
        dv1 = dt.DefaultView; //按照指定ID查处相应数据
        dv1.Table.AcceptChanges();//提交更改的数据
        //判断有没有开始统计，有没有数据存在
        if (dv1.Count == 0) { }
        else
        {
            //总访问数、开始访问日期、最高访问数、最高访问数发生日期（从简数据库读取）
            total = int.Parse(dv1[0].Row["vtop"].ToString()); AllViewNum.Text = total.ToString();//赋值
            starttimee = dv1[0].Row["starttime"].ToString(); StatTimeStart.Text = starttimee.ToString();//赋值
            if (total == 0) { }

            #region 取得相应值并赋值
            highest = int.Parse(dv1[0].Row["vhigh"].ToString()); TheHightViewNum.Text = highest.ToString();//赋值
            highesttime = DateTime.Parse(dv1[0].Row["vhightime"].ToString()).Date.ToShortDateString();//赋值
            TheHightViewNumDay.Text = highesttime.ToString();//赋值
            dv1.Dispose();//释放资源
            #endregion

            //在线人数//从统计信息表中取得值，通过ip数来统计在线人数
            DateTime newtime = DateTime.Now.AddHours(0).AddMinutes(-20);
            DataTable dt1 = sta.sel_3(newtime, viewid, Foosun.Global.Current.SiteID);
            dv1 = dt1.DefaultView;
            dv1.Table.AcceptChanges();
            onlinePerson = dv1.Count; OnlinePeopleNum.Text = onlinePerson.ToString();//赋值
            dv1.Dispose();

            //今日访问量、昨日访问量//从统计综合内容表中取得值
            DataTable dt2 = sta.sel_New(viewid, Foosun.Global.Current.SiteID);
            dv1 = dt2.DefaultView;
            dv1.Table.AcceptChanges();
            if (dv1.Count != 0)
            {
                inttoday = int.Parse(dv1[0].Row["today"].ToString()); TodayViewNum.Text = inttoday.ToString();//赋值
                intyesterday = int.Parse(dv1[0].Row["yesterday"].ToString()); YesterDayViewNum.Text = intyesterday.ToString();//赋值
            }
            else
            {
                inttoday = 0;
                intyesterday = 0;
            }
            dv1.Dispose();//释放资源

            //今年访问量//从统计信息表中取得值
            int year = int.Parse(DateTime.Now.AddHours(0).Year.ToString());
            DataTable dt3 = sta.sel_Year(year, viewid, Foosun.Global.Current.SiteID);
            dv1 = dt3.DefaultView;
            dv1.Table.AcceptChanges();
            intthisyear = dv1.Count; ThisYearViewNum.Text = intthisyear.ToString();//赋值
            dv1.Dispose();

            //本月访问量
            int Month = int.Parse(DateTime.Now.AddHours(0).Month.ToString());
            DataTable dt4 = sta.sel_Month(Month, viewid, Foosun.Global.Current.SiteID);
            dv1 = dt4.DefaultView;
            dv1.Table.AcceptChanges();
            intthismonth = dv1.Count; ThisMonthViewNum.Text = intthismonth.ToString();//赋值
            dv1.Dispose();

            //访问天数、平均每天访问量,计算平均值
            //取得访问天数的值
            viewdays = DateTime.Now.AddHours(0).Subtract(DateTime.Parse(starttimee)).TotalDays;
            viewdayavg = total / viewdays;//计算平均值
            double dbcf = System.Math.Pow(10, int.Parse(dv[0].Row["pointNum"].ToString()));
            viewdays = (int)(viewdays * dbcf + 0.5) / dbcf;
            viewdayavg = (int)(viewdayavg * dbcf + 0.5) / dbcf; 
            AverageDayViewNum.Text = viewdayavg.ToString();//赋值(结果为在参数设置中相应小数位数的值)平均日访问量
            //天数四舍五入
            string strVeiwDays = viewdays.ToString();
            if (strVeiwDays != null || !strVeiwDays.Equals(""))
            {
                strVeiwDays = strVeiwDays.Substring(0, strVeiwDays.IndexOf('.'));
            }
            StatDaysNum.Text = strVeiwDays;//赋值(结果为在参数设置中相应小数位数的值)---访问天数-----
            //预计今日访问量
            double dblvdaylong = DateTime.Now.AddHours(0).Subtract(DateTime.Now.AddHours(0).Date).TotalDays;
            inttodayguess = (int)(((inttoday / dblvdaylong) + intyesterday) / 2 + 0.5);
            if (inttodayguess < inttoday) inttodayguess = (int)((inttoday / dblvdaylong) + 0.5); GuessTodayViewNum.Text = inttodayguess.ToString();//赋值
        }
    }
    #endregion

    /// <summary>
    /// 详细记录 的摘要说明。
    /// </summary>
    /// Code By ChenZhaoHui

    protected void StatList(int PageIndex)//显示详细记录
    {
        //参数传递，得到相应类别下的统计
        string viewid = "";
        if (Request.QueryString["id"] != null)
        {
            viewid = Request.QueryString["id"];
        }
        int i, j;
        int num = sta.Stat_Sql();//从参数设置里取得每页显示记录的条数
        SQLConditionInfo[] sts = new SQLConditionInfo[2];
        sts[0] = new SQLConditionInfo("@viewid", viewid);
        sts[1] = new SQLConditionInfo("@SiteID", SiteID);
        DataTable dt = Foosun.CMS.Pagination.GetPage("Manage_Stat_View_2_aspx", PageIndex, num, out i, out j, sts);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;

        DataList1.DataSource = dt;
        DataList1.DataBind();
    }

    /// <summary>
    /// 24小时统计 的摘要说明。
    /// </summary>
    /// Code By ChenZhaoHui

    //24小时统计显示相关函数HourStat
    public string HourStat(int show_flag)
    {
        //定义的参数控制显示提示用
        string strHourStat_msg1 = "";
        string strHourStat_msg2 = "";
        string strHourStat_msg3 = "";
        string strHourStat_msg4 = "";
        //定义数组
        int[] intvhour = new int[24];
        int intmaxhour = 0;
        int intsumhour = 0;
        int intthehour;
        int lsbf;
        //参数传递，得到相应类别下的统计
        string viewid = Request.QueryString["id"];
        for (int i = 0; i < 24; i++)
        {
            intvhour[i] = vhourcon(i);
            if (intvhour[i] > intmaxhour) intmaxhour = intvhour[i];
            intsumhour += intvhour[i];
        }

        //防止除数为0出错
        if (intmaxhour == 0) intmaxhour = 1;
        if (intsumhour == 0) intsumhour = 1;

        for (int i = 0; i < 24; i++)
        {
            intthehour = int.Parse(DateTime.Now.AddHours(0).Hour.ToString()) + i + 1;
            if (intthehour > 23) intthehour -= 24;

            strHourStat_msg1 += "<td width=15 valign=bottom background='../../sysImages/StatIcon/tu_back.gif' align=center><img style='BORDER-BOTTOM: #000000 1px solid;' src='../../sysImages/StatIcon/tu.gif'	height='";
            #region 计算统计次数等信息
            strHourStat_msg1 += (int)(float.Parse(intvhour[intthehour].ToString()) / intmaxhour * 100) + "' width='12' alt='" + intthehour + "时，访问" + intvhour[intthehour] + "次，";
            #endregion
            //计算访问量的百分数，精确到小数后1位，小于零的在前面加字母0
            lsbf = (int)(int.Parse(intvhour[intthehour].ToString()) * 1000 / intsumhour) / 10;
            strHourStat_msg1 += lsbf + "% " + CopyRight + "'></td>";

            strHourStat_msg2 += "<td width=15 align=center><a title='" + intthehour + "时，访问" + intvhour[intthehour] + "次，";
            strHourStat_msg2 += lsbf + "% " + CopyRight + "'><font face='Arial' style='letter-spacing: -1'>" + intthehour + "</font></a></td>";
        }

        DataView dv1 = new DataView(); //定义24小时统计视图
        //执行操作，返回数据更新数据库
        DataTable dt5 = sta.sel_return(viewid, SiteID);
        dv1 = dt5.DefaultView;
        dv1.Table.AcceptChanges();//提交上次调用AcceptChanges()后对表的所有更改

        int[] intvallhour = new int[24];
        int intmaxallhour = 0;
        int intsumallhour = 0;

        for (int i = 0; i < dv1.Count; i++)
        {
            intvallhour[int.Parse(dv1[i].Row["vhour"].ToString())] = int.Parse(dv1[i].Row["allhour"].ToString());
            if (intvallhour[int.Parse(dv1[i].Row["vhour"].ToString())] > intmaxallhour) intmaxallhour = intvallhour[int.Parse(dv1[i].Row["vhour"].ToString())];
            intsumallhour += intvallhour[int.Parse(dv1[i].Row["vhour"].ToString())];
        }

        dv1.Dispose();//释放资源

        //防止除数为0出错
        if (intmaxallhour == 0) intmaxallhour = 1;
        if (intsumallhour == 0) intsumallhour = 1;

        for (int i = 0; i < 24; i++)
        {
            strHourStat_msg3 += "<td width=\"15\" valign=bottom background='../../sysImages/StatIcon/tu_back.gif' align=center><img style='BORDER-BOTTOM: #000000 1px solid;' src='../../sysImages/StatIcon/tu.gif'	height='";
            //计算统计次数
            strHourStat_msg3 += (int)(float.Parse(intvallhour[i].ToString()) / intmaxallhour * 100) + "' width='12' alt='" + i + "时，访问" + intvallhour[i] + "次，";
            //计算访问量的百分数，精确到小数后1位，小于零的在前面加字母0
            lsbf = (int)(int.Parse(intvallhour[i].ToString()) * 1000 / intsumallhour) / 10;
            strHourStat_msg3 += lsbf + "% " + CopyRight + "'></td>";

            strHourStat_msg4 += "<td width=15 align=center><a title='" + i + "时，访问" + intvallhour[i] + "次，";
            strHourStat_msg4 += lsbf + "% " + CopyRight + "'><font face='Arial' style='letter-spacing: -1'>" + i + "</font></a></td>";
        }

        switch (show_flag)
        {
            //最近24小时访问统计
            case 0:
                hour_lbhigh1.Text = ((int)((float)(intmaxhour) * 10 + 0.5) / 10).ToString();
                hour_lbhigh2.Text = ((float)(int)((3 * (float)(intmaxhour) * 10 / 4) + 0.5) / 10).ToString();
                hour_lbhigh3.Text = ((float)(int)(((float)(intmaxhour) * 10 / 2) + 0.5) / 10).ToString();
                hour_lbhigh4.Text = ((float)(int)(((float)(intmaxhour) * 10 / 4) + 0.5) / 10).ToString();
                return "";
            case 1:
                return strHourStat_msg1 + "<td width=10><img src='../../sysImages/StatIcon/tu_back_right.gif'></td><td width=10></td></tr><tr><td align=right><p style='line-height: 100%; margin-right: 2; margin-top: 0; margin-bottom: 0'><font face='Arial'>0</font></td><td width=10></td>" + strHourStat_msg2;

            //所有24小时访问统计
            case 2:
                hour_lbhigh5.Text = ((int)((float)(intmaxallhour) * 10 + 0.5) / 10).ToString();
                hour_lbhigh6.Text = ((float)(int)((3 * (float)(intmaxallhour) * 10 / 4) + 0.5) / 10).ToString();
                hour_lbhigh7.Text = ((float)(int)(((float)(intmaxallhour) * 10 / 2) + 0.5) / 10).ToString();
                hour_lbhigh8.Text = ((float)(int)(((float)(intmaxallhour) * 10 / 4) + 0.5) / 10).ToString();
                return "";
            case 3:
                return strHourStat_msg3 + "<td width=10><img src='../../sysImages/StatIcon/tu_back_right.gif'></td><td width=10></td></tr><tr><td align=right><p style='line-height: 100%; margin-right: 2; margin-top: 0; margin-bottom: 0'><font face='Arial'>0</font></td><td width=10></td>" + strHourStat_msg4;

            default:
                return "";

        }

    }
    public int vhourcon(int thehour)
    {
        DataView dv2 = new DataView();//初始化
        //参数传递，得到相应类别下的统计
        string viewid = Request.QueryString["id"];
        if (thehour == int.Parse(DateTime.Now.AddHours(0).Hour.ToString()))
        {
            //执行操作，返回数据更新数据库
            int vhour = int.Parse(DateTime.Now.AddHours(0).Hour.ToString());
            int vday = int.Parse(DateTime.Now.AddHours(0).Day.ToString());
            int vmonth = int.Parse(DateTime.Now.AddHours(0).Month.ToString());
            int vyear = int.Parse(DateTime.Now.AddHours(0).Year.ToString());
            DataTable dt = sta.sel_5(vhour, vday, vmonth, vyear, viewid, SiteID);
            dv2 = dt.DefaultView;
            dv2.Table.AcceptChanges();//提交上次调用AcceptChanges()后对表的所有更改

            if (dv2.Count > 0)
            {
                return int.Parse(dv2[0].Row["vhourcon"].ToString());
            }
            else
            {
                dv2.Dispose();//释放资源
                return 0;
            }
        }
        else
        {
            string vtime = DateTime.Now.AddHours(0).AddDays(-1).ToString();
            DataTable dt1 = sta.sel_4(thehour, vtime, viewid, SiteID);
            //执行操作，返回数据更新数据库
            dv2 = dt1.DefaultView;
            dv2.Table.AcceptChanges();//提交上次调用AcceptChanges()后对表的所有更改

            if (dv2.Count > 0)
            {
                return int.Parse(dv2[0].Row["vhourcon"].ToString());
            }
            else
            {
                dv2.Dispose();
                return 0;
            }
        }

    }

    /// <summary>
    ///日统计的摘要说明。
    /// </summary>
    /// Code By ChenZhaoHui

    //日统计显示相关函数DayStat
    public string DayStat(int show_flag)
    {
        //找到开始统计天数，如果天数不足31天，则跳过前面的空间
        string strDayStat_msg1 = "";
        string strDayStat_msg2 = "";
        string strDayStat_msg3 = "";
        string strDayStat_msg4 = "";
        int intmaxday = 0;
        int intsumday = 0;
        string[] strweek = new string[] { "日", "一", "二", "三", "四", "五", "六", "" };
        string strvfirst;
        int intvdays;

        int lsbf;
        //参数传递，得到相应类别下的统计
        string viewid = Request.QueryString["id"];
        //日统计视图定义
        DataView dv1 = new DataView();
        DataTable dt = sta.sel_day(viewid, SiteID);
        dv1 = dt.DefaultView;
        dv1.Table.AcceptChanges();//提交更改的数据

        if (dv1.Count > 0)
        {
            strvfirst = dv1[0].Row["vtime"].ToString();
        }
        else
        {
            strvfirst = DateTime.Now.AddHours(0).ToString();
        }

        dv1.Dispose();
        intvdays = (int)(DateTime.Now.AddHours(0).Subtract(DateTime.Parse(strvfirst)).TotalDays);

        //声明二维数组，voutday(*,0)为访问量,voutday(*,1)为日期,voutday(*,2)为星期
        int[,] arvday = new int[31, 3];
        string[,] arvoutday = new string[31, 3];

        for (int i = 0; i < 31; i++)
        {
            arvday[i, 0] = vdaycon(DateTime.Now.AddHours(0).AddDays(i - 30).ToShortDateString());
            if (arvday[i, 0] > intmaxday) intmaxday = arvday[i, 0];
            intsumday += arvday[i, 0];
            arvday[i, 1] = (int)(DateTime.Now.AddHours(0).AddDays(i - 30).Day);
            arvday[i, 2] = (int)(DateTime.Now.AddHours(0).AddDays(i - 30).DayOfWeek);
        }

        //防止除数为0而出错
        if (intmaxday == 0) intmaxday = 1;
        if (intsumday == 0) intsumday = 1;

        //根据已统计天数将数值左移
        if (intvdays >= 31)
        {
            for (int i = 0; i < 31; i++)
            {
                arvoutday[i, 0] = arvday[i, 0].ToString();
                arvoutday[i, 1] = arvday[i, 1].ToString();
                arvoutday[i, 2] = arvday[i, 2].ToString();
            }
        }
        else
        {
            for (int i = 0; i < 31; i++)
            {
                if (i <= intvdays)
                {
                    arvoutday[i, 0] = arvday[i + 30 - intvdays, 0].ToString();
                    arvoutday[i, 1] = arvday[i + 30 - intvdays, 1].ToString();
                    arvoutday[i, 2] = arvday[i + 30 - intvdays, 2].ToString();
                }
                else
                {
                    arvoutday[i, 0] = "0";
                    arvoutday[i, 1] = "";
                    arvoutday[i, 2] = "7";
                }
            }
        }

        for (int i = 0; i < 31; i++)
        {
            strDayStat_msg1 += "<td width=15 valign=bottom background='../../sysImages/StatIcon/tu_back.gif' align=center><img style='BORDER-BOTTOM: #000000 1px solid' src='../../sysImages/StatIcon/tu.gif'";
            //计算统计次数
            strDayStat_msg1 += " height='" + (int)(float.Parse(arvoutday[i, 0].ToString()) / intmaxday * 100) + "' width='12' alt='" + arvoutday[i, 1] + "日，星期" + strweek[int.Parse(arvoutday[i, 2].ToString())];
            strDayStat_msg1 += "，访问" + arvoutday[i, 0] + "次，";
            //计算访问量的百分数，精确到小数后1位，小于零的在前面加字母0
            lsbf = (int)(int.Parse(arvoutday[i, 0].ToString()) * 1000 / intsumday) / 10;
            strDayStat_msg1 += lsbf + "% " + CopyRight + "'></td>";

            strDayStat_msg2 += "<td width=15 align=center><a title='" + arvoutday[i, 1] + "日，星期" + strweek[int.Parse(arvoutday[i, 2].ToString())];
            strDayStat_msg2 += "，访问" + arvoutday[i, 0] + "次，";
            strDayStat_msg2 += lsbf + "% " + CopyRight + "'>";
            switch (arvoutday[i, 2].ToString())
            {
                case "0":
                    strDayStat_msg2 += "<font face='Arial' style='letter-spacing: -1' color='red'>";
                    break;

                case "6":
                    strDayStat_msg2 += "<font face='Arial' style='letter-spacing: -1' color='red'>";
                    break;

                case "7":
                    strDayStat_msg2 += "<font face='Arial' style='letter-spacing: -1' class='fonts'>";
                    break;

                default:
                    strDayStat_msg2 += "<font face='Arial' style='letter-spacing: -1'>";
                    break;

            }
            strDayStat_msg2 += arvoutday[i, 1] + "</font></a></td>";
        }
        //执行数据操作，更新数据库
        DataTable dt1 = sta.sel_6(viewid, SiteID);
        dv1 = dt1.DefaultView;
        dv1.Table.AcceptChanges();//提交更改的数据

        int intmaxallday = 0;
        int intsumallday = 0;
        int[] intvallday = new int[31];

        for (int i = 0; i < dv1.Count; i++)
        {
            intvallday[int.Parse(dv1[i].Row["vday"].ToString()) - 1] = int.Parse(dv1[i].Row["allday"].ToString());
            if (intvallday[int.Parse(dv1[i].Row["vday"].ToString()) - 1] > intmaxallday) intmaxallday = intvallday[int.Parse(dv1[i].Row["vday"].ToString()) - 1];
            intsumallday += intvallday[int.Parse(dv1[i].Row["vday"].ToString()) - 1];
        }

        //防止除数为0而出错
        if (intmaxallday == 0) intmaxallday = 1;
        if (intsumallday == 0) intsumallday = 1;

        dv1.Dispose();

        for (int i = 0; i < 31; i++)
        {
            strDayStat_msg3 += "<td width=15 valign=bottom background='../../sysImages/StatIcon/tu_back.gif' align=center><img style='BORDER-BOTTOM: #000000 1px solid;' src='../../sysImages/StatIcon/tu.gif'";
            strDayStat_msg3 += " height='" + (int)(float.Parse(intvallday[i].ToString()) / intmaxallday * 100) + "' width='12' alt='";
            strDayStat_msg3 += i + 1 + "日，访问" + intvallday[i] + "次，";
            //计算访问量的百分数，精确到小数后1位，小于零的在前面加字母0
            lsbf = (int)(int.Parse(intvallday[i].ToString()) * 1000 / intsumallday) / 10;
            strDayStat_msg3 += lsbf + "% " + CopyRight + "'></td>";

            strDayStat_msg4 += "<td width=15 align=center><a title='" + i + 1 + "日，访问" + intvallday[i] + "次，";
            strDayStat_msg4 += lsbf + "% " + CopyRight + "'><font face='Arial' style='letter-spacing: -1'>";
            if ((i + 1) % 2 != 0) strDayStat_msg4 += i + 1;
            strDayStat_msg4 += "</font></a></td>";
        }

        switch (show_flag)
        {
            //最近31天访问量
            case 0:
                day_lbhigh1.Text = ((int)((float)(intmaxday) * 10 + 0.5) / 10).ToString();
                day_lbhigh2.Text = ((float)(int)((3 * (float)(intmaxday) * 10 / 4) + 0.5) / 10).ToString();
                day_lbhigh3.Text = ((float)(int)(((float)(intmaxday) * 10 / 2) + 0.5) / 10).ToString();
                day_lbhigh4.Text = ((float)(int)(((float)(intmaxday) * 10 / 4) + 0.5) / 10).ToString();
                return "";

            case 1:
                return strDayStat_msg1 + "<td width=10><img src='../../sysImages/StatIcon/tu_back_right.gif'></td><td width=10></td></tr><tr height='18'><td align=right><p style='line-height: 100%; margin-right: 2; margin-top: 0; margin-bottom: 0'><font face='Arial'>0</font></td><td width=10></td>" + strDayStat_msg2;
            //所有月分日访问量
            case 2:
                day_lbhigh5.Text = ((int)((float)(intmaxallday) * 10 + 0.5) / 10).ToString();
                day_lbhigh6.Text = ((float)(int)((3 * (float)(intmaxallday) * 10 / 4) + 0.5) / 10).ToString();
                day_lbhigh7.Text = ((float)(int)(((float)(intmaxallday) * 10 / 2) + 0.5) / 10).ToString();
                day_lbhigh8.Text = ((float)(int)(((float)(intmaxallday) * 10 / 4) + 0.5) / 10).ToString();
                return "";

            case 3:
                return strDayStat_msg3 + "<td width=10><img src='../../sysImages/StatIcon/tu_back_right.gif'></td><td width=10></td></tr><tr height='18'><td align=right><p style='line-height: 100%; margin-right: 2; margin-top: 0; margin-bottom: 0'><font face='Arial'>0</font></td><td width=10></td>" + strDayStat_msg4;

            default:
                return "";
        }
    }

    /// <summary>
    ///周统计的摘要说明。
    /// </summary>
    /// Code By ChenZhaoHui

    //周统计显示相关函数
    public string WeekStat(int show_flag)
    {

        //找到开始统计天数，如果天数不足7天，则跳过前面的空间
        string strWeekStat_msg1 = "";
        string strWeekStat_msg2 = "";
        string strWeekStat_msg3 = "";
        string strWeekStat_msg4 = "";

        string strvfirst;
        int intvdays;
        int intmaxday = 0;
        int intsumday = 0;
        string[] strweek = new string[] { "日", "一", "二", "三", "四", "五", "六", "" };
        int lsbf;
        //参数传递，得到相应类别下的统计
        string viewid = Request.QueryString["id"];
        //周统计视图定义
        DataView dv1 = new DataView();
        DataTable dt = sta.sel_7(viewid, SiteID);
        dv1 = dt.DefaultView;
        dv1.Table.AcceptChanges();

        if (dv1.Count > 0)
        {
            strvfirst = dv1[0].Row["vfirst"].ToString();
        }
        else
        {
            strvfirst = DateTime.Now.AddHours(0).ToString();
        }

        dv1.Dispose();
        intvdays = (int)(DateTime.Now.AddHours(0).Subtract(DateTime.Parse(strvfirst)).TotalDays);

        //声明二维数组，voutday(*,0)为访问量,voutday(*,1)为日期,voutday(*,2)为星期
        int[,] arvday = new int[7, 3];
        string[,] arvoutday = new string[7, 3];

        for (int i = 0; i < 7; i++)
        {
            arvday[i, 0] = vdaycon(DateTime.Now.AddHours(0).AddDays(i - 6).ToShortDateString());
            if (arvday[i, 0] > intmaxday) intmaxday = arvday[i, 0];
            intsumday += arvday[i, 0];
            arvday[i, 1] = (int)(DateTime.Now.AddHours(0).AddDays(i - 6).Day);
            arvday[i, 2] = (int)(DateTime.Now.AddHours(0).AddDays(i - 6).DayOfWeek);
        }

        //防止除数为0而出错
        if (intmaxday == 0) intmaxday = 1;
        if (intsumday == 0) intsumday = 1;

        //根据已统计天数将数值左移
        if (intvdays >= 7)
        {
            for (int i = 0; i < 7; i++)
            {
                arvoutday[i, 0] = arvday[i, 0].ToString();
                arvoutday[i, 1] = arvday[i, 1].ToString();
                arvoutday[i, 2] = arvday[i, 2].ToString();
            }
        }
        else
        {
            for (int i = 0; i < 7; i++)
            {
                if (i <= intvdays)
                {
                    arvoutday[i, 0] = arvday[i + 6 - intvdays, 0].ToString();
                    arvoutday[i, 1] = arvday[i + 6 - intvdays, 1].ToString();
                    arvoutday[i, 2] = arvday[i + 6 - intvdays, 2].ToString();
                }
                else
                {
                    arvoutday[i, 0] = "0";
                    arvoutday[i, 1] = "";
                    arvoutday[i, 2] = "7";
                }
            }
        }

        for (int i = 0; i < 7; i++)
        {
            strWeekStat_msg1 += "<td width=15 valign=bottom background='../../sysImages/StatIcon/tu_back.gif' align=center><img style='BORDER-BOTTOM: #000000 1px solid' src='../../sysImages/StatIcon/tu.gif'";
            strWeekStat_msg1 += " height='" + (int)(float.Parse(arvoutday[i, 0].ToString()) / intmaxday * 100) + "' width='12' alt='" + arvoutday[i, 1] + "日，星期" + strweek[int.Parse(arvoutday[i, 2].ToString())];
            strWeekStat_msg1 += "，访问" + arvoutday[i, 0] + "次，";
            //计算访问量的百分数，精确到小数后1位，小于零的在前面加字母0
            lsbf = (int)(int.Parse(arvoutday[i, 0].ToString()) * 1000 / intsumday) / 10;
            strWeekStat_msg1 += lsbf + "% " + CopyRight + "'></td>";

            strWeekStat_msg2 += "<td width=15 align=center><a title='" + arvoutday[i, 1] + "日，星期" + strweek[int.Parse(arvoutday[i, 2].ToString())];
            strWeekStat_msg2 += "，访问" + arvoutday[i, 0] + "次，";
            strWeekStat_msg2 += lsbf + "% " + CopyRight + "'>";
            switch (arvoutday[i, 2].ToString())
            {
                case "0":
                    strWeekStat_msg2 += "<font face='Arial' style='letter-spacing: -1' color='red'>";
                    break;

                case "6":
                    strWeekStat_msg2 += "<font face='Arial' style='letter-spacing: -1' color='red'>";
                    break;

                case "7":
                    strWeekStat_msg2 += "<font face='Arial' style='letter-spacing: -1' class='fonts'>";
                    break;

                default:
                    strWeekStat_msg2 += "<font face='Arial' style='letter-spacing: -1'>";
                    break;

            }
            strWeekStat_msg2 += strweek[int.Parse(arvoutday[i, 2].ToString())] + "</font></a></td>";
        }
        //执行数据操作，更新数据
        DataTable dt2 = sta.sel_8(viewid, SiteID);
        dv1 = dt2.DefaultView;
        dv1.Table.AcceptChanges();

        int intmaxallweek = 0;
        int intsumallweek = 0;
        int[] intvallweek = new int[7];

        for (int i = 0; i < dv1.Count; i++)
        {
            intvallweek[int.Parse(dv1[i].Row["vweek"].ToString())] = int.Parse(dv1[i].Row["allweek"].ToString());
            if (intvallweek[int.Parse(dv1[i].Row["vweek"].ToString())] > intmaxallweek) intmaxallweek = intvallweek[int.Parse(dv1[i].Row["vweek"].ToString())];
            intsumallweek += intvallweek[int.Parse(dv1[i].Row["vweek"].ToString())];
        }

        //防止除数为0而出错
        if (intmaxallweek == 0) intmaxallweek = 1;
        if (intsumallweek == 0) intsumallweek = 1;

        dv1.Dispose();

        for (int i = 0; i < 7; i++)
        {
            strWeekStat_msg3 += "<td width=15 valign=bottom background='../../sysImages/StatIcon/tu_back.gif' align=center><img style='BORDER-BOTTOM: #000000 1px solid;' src='../../sysImages/StatIcon/tu.gif'";
            strWeekStat_msg3 += " height='" + (int)(float.Parse(intvallweek[i].ToString()) / intmaxallweek * 100) + "' width='12' alt='星期";
            strWeekStat_msg3 += strweek[i] + "，访问" + intvallweek[i] + "次，";
            //计算访问量的百分数，精确到小数后1位，小于零的在前面加字母0
            lsbf = (int)(int.Parse(intvallweek[i].ToString()) * 1000 / intsumallweek) / 10;
            strWeekStat_msg3 += lsbf + "% " + CopyRight + "'></td>";

            strWeekStat_msg4 += "<td width=15 align=center><a title='星期" + strweek[i] + "，访问" + intvallweek[i] + "次，";
            strWeekStat_msg4 += lsbf + "% " + CopyRight + "'><font face='Arial' style='letter-spacing: -1'>" + strweek[i] + "</font></a></td>";
        }


        switch (show_flag)
        {
            case 0:
                week_lbhigh1.Text = ((int)((float)(intmaxday) * 10 + 0.5) / 10).ToString();
                week_lbhigh2.Text = ((float)(int)((3 * (float)(intmaxday) * 10 / 4) + 0.5) / 10).ToString();
                week_lbhigh3.Text = ((float)(int)(((float)(intmaxday) * 10 / 2) + 0.5) / 10).ToString();
                week_lbhigh4.Text = ((float)(int)(((float)(intmaxday) * 10 / 4) + 0.5) / 10).ToString();
                return "";

            case 1:
                return strWeekStat_msg1 + "<td width=10><img src='../../sysImages/StatIcon/tu_back_right.gif'></td><td width=10></td></tr><tr height='18'><td align=right><p style='line-height: 100%; margin-right: 2; margin-top: 0; margin-bottom: 0'><font face='Arial'>0</font></td><td width=10></td>" + strWeekStat_msg2;

            case 2:
                week_lbhigh5.Text = ((int)((float)(intmaxallweek) * 10 + 0.5) / 10).ToString();
                week_lbhigh6.Text = ((float)(int)((3 * (float)(intmaxallweek) * 10 / 4) + 0.5) / 10).ToString();
                week_lbhigh7.Text = ((float)(int)(((float)(intmaxallweek) * 10 / 2) + 0.5) / 10).ToString();
                week_lbhigh8.Text = ((float)(int)(((float)(intmaxallweek) * 10 / 4) + 0.5) / 10).ToString();
                return "";

            case 3:
                return strWeekStat_msg3 + "<td width=10><img src='../../sysImages/StatIcon/tu_back_right.gif'></td><td width=10></td></tr><tr height='18'><td align=right><p style='line-height: 100%; margin-right: 2; margin-top: 0; margin-bottom: 0'><font face='Arial'>0</font></td><td width=10></td>" + strWeekStat_msg4;

            default:
                return "";
        }

    }

    public int vdaycon(string theday)
    {
        string strtheday = DateTime.Parse(theday).ToString();
        string strthetday = DateTime.Parse(theday).AddDays(1).ToString();
        //参数传递，得到相应类别下的统计
        string viewid = Request.QueryString["id"];

        DataView dv2 = new DataView();
        DataTable dt4 = sta.sel_9(strtheday, strthetday, viewid, SiteID);
        dv2 = dt4.DefaultView;
        dv2.Table.AcceptChanges();
        if (dv2.Count > 0)
        {
            return int.Parse(dv2[0].Row["vdaycon"].ToString());
        }
        else
        {
            dv2.Dispose();
            return 0;
        }

    }

    /// <summary>
    ///月统计的摘要说明。
    /// </summary>
    /// Code By ChenZhaoHui

    //月统计显示相关函数
    public string MonthStat(int show_flag)
    {
        //参数传递，得到相应类别下的统计
        string viewid = Request.QueryString["id"];
        if (show_flag <= 1)
        {
            DateTime dtdatetwelve1 = DateTime.Now.AddHours(0).AddMonths(-11);
            string strdatetwelve = dtdatetwelve1.Year.ToString() + "-" + dtdatetwelve1.Month.ToString() + "-1 00:00:00";
            DataTable dt1 = sta.sel_10(strdatetwelve, viewid, SiteID);
            return do_month_data(dt1, show_flag);
        }
        else
        {
            if (show_flag != 4)
            {
                DataTable dt2 = sta.sel_vmonth(viewid, SiteID);
                return do_month_data(dt2, show_flag);
            }
            else
            {
                DataView dv1 = new DataView();
                DataTable dt3 = sta.sel_vyear(viewid, SiteID);
                dv1 = dt3.DefaultView;
                dv1.Table.AcceptChanges();

                string strshow_year_data_msg = "";
                int intmaxallyear = 0;
                int intsumallyear = 0;
                string strtheyear;
                string strvallyear;
                int lsbf;

                for (int i = 0; i < dv1.Count; i++)
                {
                    if (int.Parse(dv1[i].Row["allyear"].ToString()) > intmaxallyear) intmaxallyear = int.Parse(dv1[i].Row["allyear"].ToString());
                    intsumallyear += int.Parse(dv1[i].Row["allyear"].ToString());
                }

                //防止除数为零而出错
                if (intmaxallyear == 0) intmaxallyear = 1;
                if (intsumallyear == 0) intsumallyear = 1;

                for (int i = 0; i < dv1.Count; i++)
                {
                    strtheyear = dv1[i].Row["vyear"].ToString();
                    strvallyear = dv1[i].Row["allyear"].ToString();

                    strshow_year_data_msg += "<tr><td width='40' align=right><a title='" + strtheyear + "年，访问" + strvallyear + "次，";
                    //计算访问量的百分数，精确到小数后1位，小于零的在前面加字母0
                    lsbf = (int)(int.Parse(strvallyear.ToString()) * 1000 / intsumallyear) / 10;
                    strshow_year_data_msg += lsbf + "% " + CopyRight + "'>" + strtheyear + "</a>&nbsp;</td>";
                    strshow_year_data_msg += "<td width='230' background='../../sysImages/StatIcon/tu_back_2.gif' align=left><img style='BORDER-left: #000000 1px solid;' src='../../sysImages/StatIcon/tu.gif'";
                    strshow_year_data_msg += " width='" + (int)(float.Parse(strvallyear.ToString()) / intmaxallyear * 183) + "' height='12' alt='" + strtheyear + "年，访问" + strvallyear + "次，";
                    strshow_year_data_msg += lsbf + "% " + CopyRight + "'> " + strvallyear + "</td></tr>";

                }

                dv1.Dispose();

                return strshow_year_data_msg;
            }

        }


    }
    public string do_month_data(DataTable strSql, int show_flag)
    {
        string strMonthStat_msg1 = "";
        string strMonthStat_msg2 = "";
        int intmaxallmonth = 0;
        int intsumallmonth = 0;
        int intthemonth;
        int lsbf;

        DataView dv1 = new DataView();
        dv1 = strSql.DefaultView;//查询方法返回视图
        dv1.Table.AcceptChanges();

        int[] intvallmonth = new int[12];

        for (int i = 0; i < dv1.Count; i++)
        {
            intvallmonth[int.Parse(dv1[i].Row["vmonth"].ToString()) - 1] = int.Parse(dv1[i].Row["allmonth"].ToString());
            if (intvallmonth[int.Parse(dv1[i].Row["vmonth"].ToString()) - 1] > intmaxallmonth) intmaxallmonth = intvallmonth[int.Parse(dv1[i].Row["vmonth"].ToString()) - 1];
            intsumallmonth += intvallmonth[int.Parse(dv1[i].Row["vmonth"].ToString()) - 1];
        }

        dv1.Dispose();

        //防止除数为零而出错
        if (intmaxallmonth == 0) intmaxallmonth = 1;
        if (intsumallmonth == 0) intsumallmonth = 1;

        for (int i = 0; i < 12; i++)
        {
            if (show_flag <= 1)
            {
                intthemonth = int.Parse(DateTime.Now.AddHours(0).Month.ToString()) + i;
                if (intthemonth > 11) intthemonth -= 12;
            }
            else
            {
                intthemonth = i;
            }

            strMonthStat_msg1 += "<td width=15 valign=bottom background='../../sysImages/StatIcon/tu_back.gif' align=center><img style='BORDER-BOTTOM: #000000 1px solid' src='../../sysImages/StatIcon/tu.gif'";
            strMonthStat_msg1 += " height='" + (int)(float.Parse(intvallmonth[intthemonth].ToString()) / intmaxallmonth * 100) + "' width='12' alt='" + (int)(intthemonth + 1) + "月";
            strMonthStat_msg1 += "，访问" + intvallmonth[intthemonth] + "次，";
            //计算访问量的百分数，精确到小数后1位，小于零的在前面加字母0
            lsbf = (int)(int.Parse(intvallmonth[intthemonth].ToString()) * 1000 / intsumallmonth) / 10;
            strMonthStat_msg1 += lsbf + "% " + CopyRight + "'></td>";

            strMonthStat_msg2 += "<td width=20 align=center><a title='" + (int)(intthemonth + 1) + "月，访问" + intvallmonth[intthemonth] + "次，";
            strMonthStat_msg2 += lsbf + "% " + CopyRight + "'><font face='Arial' style='letter-spacing: -1'>" + (int)(intthemonth + 1) + "</font></a></td>";
        }

        switch (show_flag)
        {
            case 0:
                month_lbhigh1.Text = ((int)((float)(intmaxallmonth) * 10 + 0.5) / 10).ToString();
                month_lbhigh2.Text = ((float)(int)((3 * (float)(intmaxallmonth) * 10 / 4) + 0.5) / 10).ToString();
                month_lbhigh3.Text = ((float)(int)(((float)(intmaxallmonth) * 10 / 2) + 0.5) / 10).ToString();
                month_lbhigh4.Text = ((float)(int)(((float)(intmaxallmonth) * 10 / 4) + 0.5) / 10).ToString();
                return "";

            case 1:
                return strMonthStat_msg1 + "<td width=10><img src='../../sysImages/StatIcon/tu_back_right.gif'></td><td width=10></td></tr><tr height='18'><td align=right><p style='line-height: 100%; margin-right: 2; margin-top: 0; margin-bottom: 0'><font face='Arial'>0</font></td><td width=10></td>" + strMonthStat_msg2;

            case 2:
                month_lbhigh5.Text = ((int)((float)(intmaxallmonth) * 10 + 0.5) / 10).ToString();
                month_lbhigh6.Text = ((float)(int)((3 * (float)(intmaxallmonth) * 10 / 4) + 0.5) / 10).ToString();
                month_lbhigh7.Text = ((float)(int)(((float)(intmaxallmonth) * 10 / 2) + 0.5) / 10).ToString();
                month_lbhigh8.Text = ((float)(int)(((float)(intmaxallmonth) * 10 / 4) + 0.5) / 10).ToString();
                return "";

            case 3:
                return strMonthStat_msg1 + "<td width=10><img src='../../sysImages/StatIcon/tu_back_right.gif'></td><td width=10></td></tr><tr height='18'><td align=right><p style='line-height: 100%; margin-right: 2; margin-top: 0; margin-bottom: 0'><font face='Arial'>0</font></td><td width=10></td>" + strMonthStat_msg2;
            default:
                return "";

        }

    }

    /// <summary>
    ///被访问页面统计的摘要说明。
    /// </summary>
    /// Code By ChenZhaoHui

    //被访问页面统计相关函数
    public string PageStat()
    {
        string strPageStat_msg = "";
        string strthepage;
        string strvallpage;
        string strsvpage = "";
        int lsbf;
        //参数传递，得到相应类别下的统计
        string viewid = Request.QueryString["id"];
        DataView dv1 = new DataView();
        DataTable dts = sta.sel_vpage(viewid, SiteID);
        dv1 = dts.DefaultView; ;
        dv1.Table.AcceptChanges();

        int intmaxallpage = 0;
        int intsumallpage = 0;
        for (int i = 0; i < dv1.Count; i++)
        {
            if (int.Parse(dv1[i].Row["allpage"].ToString()) > intmaxallpage) intmaxallpage = int.Parse(dv1[i].Row["allpage"].ToString());
            intsumallpage += int.Parse(dv1[i].Row["allpage"].ToString());
        }

        //防止除数为0出错
        if (intmaxallpage == 0) intmaxallpage = 1;
        if (intsumallpage == 0) intsumallpage = 1;

        int j = 0;
        for (int i = 0; i < dv1.Count; i++)
        {
            strthepage = dv1[i].Row["vpage"].ToString();//当前访问页
            strvallpage = dv1[i].Row["allpage"].ToString();//记录总访问次数

            int intthelen = strthepage.Length;
            //根据strthepage.Length判断访问页是直接访问（连接到view.aspx）还是其他网页（连接到相应网页去）
            if (intthelen == 0)
            {
                strthepage = "View.aspx";
                strsvpage = "通过收藏或直接输入网址访问";
                strPageStat_msg += "<tr><td width='220' align=right><a href='" + strthepage + "' target='_blank' class='menulist'  title='" + strthepage + "，访问" + strvallpage + "次，";
            }

            if (intthelen > 0 && intthelen <= 33)
            {
                strsvpage = strthepage;
                strPageStat_msg += "<tr><td width='220' align=right><a href='" + strthepage + "' target='_blank' class='menulist'  title='" + strthepage + "，访问" + strvallpage + "次，";
            }
            //若长度>=34，截取其长度为前33
            if (intthelen >= 34)
            {
                strsvpage = strthepage.Substring(0, 33) + "...";
                strPageStat_msg += "<tr><td width='220' align=right><a href='" + strthepage + "' target='_blank' class='menulist'  title='" + strthepage + "，访问" + strvallpage + "次，";
            }


            //计算访问量的百分数，精确到小数后1位，小于零的在前面加字母0
            lsbf = (int)(int.Parse(strvallpage.ToString()) * 1000 / intsumallpage) / 10;

            strPageStat_msg += lsbf + "% " + CopyRight + "'>" + strsvpage + "</a>&nbsp;</td>";
            strPageStat_msg += "<td width='230' background='../../sysImages/StatIcon/tu_back_2.gif' align=left>";
            strPageStat_msg += "<img style='BORDER-left: #000000 1px solid;' src='../../sysImages/StatIcon/tu.gif'";
            strPageStat_msg += " width='" + (int)(float.Parse(strvallpage.ToString()) / intmaxallpage * 183) + "' height='12' alt='" + strthepage + "，访问" + strvallpage + "次，";
            strPageStat_msg += lsbf + "% " + CopyRight + "'> " + strvallpage + "</td></tr>";

            j++;
            if (j >= 40) break;

        }

        dv1.Dispose();

        return strPageStat_msg;
    }

    /// <summary>
    ///IP统计的摘要说明。
    /// </summary>
    /// Code By ChenZhaoHui

    //IP统计显示相关函数
    public string IpStat()
    {
        string strIpStat_msg = "";
        string strtheip;
        string strvallip;
        string strsvip = "";
        int lsbf;
        //参数传递，得到相应类别下的统计
        string viewid = Request.QueryString["id"];
        DataView dv1 = new DataView();
        DataTable dt1 = sta.sel_vip(viewid, SiteID);
        dv1 = dt1.DefaultView;
        dv1.Table.AcceptChanges();

        int intmaxallip = 0;
        int intsumallip = 0;
        for (int i = 0; i < dv1.Count; i++)
        {
            if (int.Parse(dv1[i].Row["allip"].ToString()) > intmaxallip) intmaxallip = int.Parse(dv1[i].Row["allip"].ToString());
            intsumallip += int.Parse(dv1[i].Row["allip"].ToString());
        }
        //防止除数为0出错
        if (intmaxallip == 0) intmaxallip = 1;
        if (intsumallip == 0) intsumallip = 1;

        int j = 0;
        for (int i = 0; i < dv1.Count; i++)
        {
            strtheip = dv1[i].Row["vip"].ToString();
            strvallip = dv1[i].Row["allip"].ToString();

            int intthelen = strtheip.Length;
            if (intthelen == 0)
            {
                strtheip = "View.aspx";
                strsvip = "通过收藏或直接输入网址访问";
            }
            if (intthelen > 0 && intthelen <= 33)
            {
                strsvip = strtheip;
            }
            if (intthelen >= 34)
            {
                strsvip = strtheip.Substring(0, 33) + "...";
            }

            strIpStat_msg += "<tr><td width='120' align=right><a title='" + strtheip + "，访问" + strvallip + "次，";

            //计算访问量的百分数，精确到小数后1位，小于零的在前面加字母0
            lsbf = (int)(int.Parse(strvallip.ToString()) * 1000 / intsumallip) / 10;

            strIpStat_msg += lsbf + "% " + CopyRight + "'>" + strsvip + "</a>&nbsp;</td>";
            strIpStat_msg += "<td width='230' background='../../sysImages/StatIcon/tu_back_2.gif' align=left>";
            strIpStat_msg += "<img style='BORDER-left: #000000 1px solid;' src='../../sysImages/StatIcon/tu.gif'";
            strIpStat_msg += " width='" + (int)(float.Parse(strvallip.ToString()) / intmaxallip * 183) + "' height='12' alt='" + strtheip + "，访问" + strvallip + "次，";
            strIpStat_msg += lsbf + "% " + CopyRight + "'> " + strvallip + "</td></tr>";

            j++;
            if (j >= 40) break;

        }
        dv1.Dispose();//释放资源

        return strIpStat_msg;
    }

    /// <summary>
    ///客户端统计的摘要说明。
    /// </summary>
    /// Code By ChenZhaoHui

    public string SoftStat(int show_flag)
    {
        string strSoftStat_msg1 = "";
        string strSoftStat_msg2 = "";
        int lsbf;

        DataView dv1 = new DataView();

        //浏览器使用情况
        string[,] arvsoft = new string[,] { { "NetCaptor", "" }, { "MSIE 6.x", "" }, { "MSIE 5.x", "" }, { "MSIE 4.x", "" }, { "Netscape", "" }, { "Opera", "" }, { "Other", "" } };
        for (int i = 0; i <= 6; i++) arvsoft[i, 1] = howsoft(arvsoft[i, 0]);

        int intmaxsoft = 0;
        int intsumsoft = 0;
        for (int i = 0; i <= 6; i++)
        {
            if (int.Parse(arvsoft[i, 1].ToString()) > intmaxsoft) intmaxsoft = int.Parse(arvsoft[i, 1].ToString());
            intsumsoft += int.Parse(arvsoft[i, 1].ToString());
        }

        //防止除数为0出错
        if (intmaxsoft == 0) intmaxsoft = 1;
        if (intsumsoft == 0) intsumsoft = 1;

        for (int i = 0; i <= 6; i++)
        {
            strSoftStat_msg1 += "<td width=45 valign=bottom background='../../sysImages/StatIcon/tu_back.gif' align=center><img style='BORDER-BOTTOM: #000000 1px solid' src='../../sysImages/StatIcon/tu.gif' height='" + (int)(float.Parse(arvsoft[i, 1].ToString()) / intmaxsoft * 100) + "' width='12' alt='" + arvsoft[i, 0] + "，访问" + arvsoft[i, 1] + "次，";
            //显示统计的详细情况，用户可以通过查看其记录了解统计情况
            strSoftStat_msg2 += "<td width=45 align=center><a title='" + arvsoft[i, 0] + "，访问" + arvsoft[i, 1] + "次，";

            //计算访问量的百分数，精确到小数后1位，小于零的在前面加字母0
            lsbf = (int)(int.Parse(arvsoft[i, 1].ToString()) * 1000 / intsumsoft) / 10;

            strSoftStat_msg1 += lsbf + "% " + CopyRight + "'>" + "</td>";
            strSoftStat_msg2 += lsbf + "% " + CopyRight + "'>";
            if (arvsoft[i, 0].Length > 6)
            {
                strSoftStat_msg2 += arvsoft[i, 0].Substring(0, 6);
            }
            else
            {
                strSoftStat_msg2 += arvsoft[i, 0];
            }
            strSoftStat_msg2 += "</font></a></td>";
        }

        switch (show_flag)
        {
            case 0:
                soft_lbhigh1.Text = ((int)((float)(intmaxsoft) * 10 + 0.5) / 10).ToString();
                soft_lbhigh2.Text = ((float)(int)((3 * (float)(intmaxsoft) * 10 / 4) + 0.5) / 10).ToString();
                soft_lbhigh3.Text = ((float)(int)(((float)(intmaxsoft) * 10 / 2) + 0.5) / 10).ToString();
                soft_lbhigh4.Text = ((float)(int)(((float)(intmaxsoft) * 10 / 4) + 0.5) / 10).ToString();
                return "";

            case 1:
                return strSoftStat_msg1;

            case 2:
                return strSoftStat_msg2;

            default:
                return "";

        }

    }


    //操作系统统计
    public string OsStat(int show_flag)
    {
        string strOsStat_msg1 = "";
        string strOsStat_msg2 = "";
        int lsbf;
        //参数传递，得到相应类别下的统计
        string viewid = Request.QueryString["id"];
        DataSet myds1 = new DataSet();
        DataView dv1 = new DataView();

        //操作系统使用情况
        string[,] arvos = new string[,] { { "Win2k", "" }, { "WinXP", "" }, { "Win2k3", "" }, { "WinNT", "" }, { "Win9x", "" }, { "类Unix", "" }, { "Mac", "" }, { "Other", "" } };
        for (int i = 0; i <= 7; i++) arvos[i, 1] = howOS(arvos[i, 0]);

        int intmaxos = 0;
        int intsumos = 0;
        for (int i = 0; i <= 7; i++)
        {
            if (int.Parse(arvos[i, 1].ToString()) > intmaxos) intmaxos = int.Parse(arvos[i, 1].ToString());
            intsumos += int.Parse(arvos[i, 1].ToString());
        }

        //防止除数为0出错
        if (intmaxos == 0) intmaxos = 1;
        if (intsumos == 0) intsumos = 1;

        for (int i = 0; i <= 7; i++)
        {
            strOsStat_msg1 += "<td width=45 valign=bottom background='../../sysImages/StatIcon/tu_back.gif' align=center><img style='BORDER-BOTTOM: #000000 1px solid' src='../../sysImages/StatIcon/tu.gif' height='" + (int)(float.Parse(arvos[i, 1].ToString()) / intmaxos * 100) + "' width='12' alt='" + arvos[i, 0] + "，访问" + arvos[i, 1] + "次，";
            strOsStat_msg2 += "<td width=45 align=center><a title='" + arvos[i, 1] + "，访问" + arvos[i, 1] + "次，";

            //计算访问量的百分数，精确到小数后1位，小于零的在前面加字母0
            lsbf = (int)(int.Parse(arvos[i, 1].ToString()) * 1000 / intsumos) / 10;

            strOsStat_msg1 += lsbf + "% " + CopyRight + "'>" + "</td>";
            strOsStat_msg2 += lsbf + "% " + CopyRight + "'>" + arvos[i, 0] + "</font></a></td>";
        }

        switch (show_flag)
        {
            case 0:
                soft_lbhigh5.Text = ((int)((float)(intmaxos) * 10 + 0.5) / 10).ToString();
                soft_lbhigh6.Text = ((float)(int)((3 * (float)(intmaxos) * 10 / 4) + 0.5) / 10).ToString();
                soft_lbhigh7.Text = ((float)(int)(((float)(intmaxos) * 10 / 2) + 0.5) / 10).ToString();
                soft_lbhigh8.Text = ((float)(int)(((float)(intmaxos) * 10 / 4) + 0.5) / 10).ToString();
                return "";

            case 1:
                return strOsStat_msg1;

            case 2:
                return strOsStat_msg2;

            default:
                return "";

        }

    }
    //屏幕宽度统计情况
    public string WidthStat()
    {
        string strWidthStat_msg = "";
        string strthewidth;
        string strvallwidth;
        int lsbf;
        //参数传递，得到相应类别下的统计
        string viewid = Request.QueryString["id"];
        DataView dv1 = new DataView();
        DataTable dt8 = sta.sel_vwidth(viewid, SiteID);
        dv1 = dt8.DefaultView;
        dv1.Table.AcceptChanges();

        int intmaxallwidth = 0;
        int intsumallwidth = 0;
        for (int i = 0; i < dv1.Count; i++)
        {
            if (int.Parse(dv1[i].Row["allwidth"].ToString()) > intmaxallwidth) intmaxallwidth = int.Parse(dv1[i].Row["allwidth"].ToString());
            intsumallwidth += int.Parse(dv1[i].Row["allwidth"].ToString());
        }

        //防止除数为0出错
        if (intmaxallwidth == 0) intmaxallwidth = 1;
        if (intsumallwidth == 0) intsumallwidth = 1;

        int j = 0;
        for (int i = 0; i < dv1.Count; i++)
        {
            strthewidth = dv1[i].Row["vwidth"].ToString();
            strvallwidth = dv1[i].Row["allwidth"].ToString();

            strWidthStat_msg += "<tr><td width='40' align=right><a title='" + strthewidth + "，访问" + strvallwidth + "次，";

            //计算访问量的百分数，精确到小数后1位，小于零的在前面加字母0
            lsbf = (int)(int.Parse(strvallwidth.ToString()) * 1000 / intsumallwidth) / 10;

            strWidthStat_msg += lsbf + "% " + CopyRight + "'>" + strthewidth + "</a>&nbsp;</td>";
            strWidthStat_msg += "<td width='230' background='../../sysImages/StatIcon/tu_back_2.gif' align=left>";
            strWidthStat_msg += "<img style='BORDER-left: #000000 1px solid;' src='../../sysImages/StatIcon/tu.gif'";
            strWidthStat_msg += " width='" + (int)(float.Parse(strvallwidth.ToString()) / intmaxallwidth * 183) + "' height='12' alt='" + strthewidth + "，访问" + strvallwidth + "次，";
            strWidthStat_msg += lsbf + "% " + CopyRight + "'> " + strvallwidth + "</td></tr>";

            j++;
            if (j >= 40) break;

        }

        dv1.Dispose();

        return strWidthStat_msg;

    }

    public string howsoft(string vsoft)
    {
        DataView dv2 = new DataView();
        //参数传递，得到相应类别下的统计
        string viewid = Request.QueryString["id"];
        DataTable dt9 = sta.sel_vsoft(vsoft, viewid, SiteID);
        dv2 = dt9.DefaultView;
        dv2.Table.AcceptChanges();
        if (dv2.Count > 0)
        {
            return dv2[0].Row["howsoft"].ToString();
        }
        else
        {
            dv2.Dispose();//释放资源
            return "0";
        }

    }

    public string howOS(string vOS)
    {
        DataView dv2 = new DataView();
        //参数传递，得到相应类别下的统计
        string viewid = Request.QueryString["id"];
        DataTable dtr = sta.sel_vOS(vOS, viewid, SiteID);
        dv2 = dtr.DefaultView;
        dv2.Table.AcceptChanges();
        if (dv2.Count > 0)
        {
            return dv2[0].Row["howOS"].ToString();
        }
        else
        {
            dv2.Dispose();
            return "0";
        }

    }

    /// <summary>
    ///地区统计的摘要说明。
    /// </summary>
    /// Code By ChenZhaoHui

    public string AreaStat()
    {
        string strAreaStat_msg = "";
        string strthewhere;
        string strvallwhere;
        string strsvwhere = "";
        int lsbf;
        //参数传递，得到相应类别下的统计
        string viewid = Request.QueryString["id"];

        DataView dv1 = new DataView();
        DataTable dtt = sta.sel_vwhere(viewid, SiteID);
        dv1 = dtt.DefaultView;
        dv1.Table.AcceptChanges();

        int intmaxallwhere = 0;
        int intsumallwhere = 0;

        for (int i = 0; i < dv1.Count; i++)
        {
            if (int.Parse(dv1[i].Row["allwhere"].ToString()) > intmaxallwhere) intmaxallwhere = int.Parse(dv1[i].Row["allwhere"].ToString());
            intsumallwhere += int.Parse(dv1[i].Row["allwhere"].ToString());
        }

        //防止除数为0出错
        if (intmaxallwhere == 0) intmaxallwhere = 1;
        if (intsumallwhere == 0) intsumallwhere = 1;

        int j = 0;
        for (int i = 0; i < dv1.Count; i++)
        {
            strthewhere = dv1[i].Row["vwhere"].ToString();
            strvallwhere = dv1[i].Row["allwhere"].ToString();

            int intthelen = strthewhere.Length;
            if (intthelen == 0)
            {
                strthewhere = "View.aspx";
                strsvwhere = "通过收藏或直接输入网址访问";
            }
            if (intthelen > 0 && intthelen <= 33)
            {
                strsvwhere = strthewhere;
            }
            if (intthelen >= 34)
            {
                strsvwhere = strthewhere.Substring(0, 33) + "...";
            }

            strAreaStat_msg += "<tr><td width='120' align=right><a title='" + strthewhere + "，访问" + strvallwhere + "次，";

            //计算访问量的百分数，精确到小数后1位，小于零的在前面加字母0
            lsbf = (int)(int.Parse(strvallwhere.ToString()) * 1000 / intsumallwhere) / 10;

            strAreaStat_msg += lsbf + "% " + CopyRight + "'>" + strsvwhere + "</a>&nbsp;</td>";
            strAreaStat_msg += "<td width='230' background='../../sysImages/StatIcon/tu_back_2.gif' align=left>";
            strAreaStat_msg += "<img style='BORDER-left: #000000 1px solid;' src='../../sysImages/StatIcon/tu.gif'";
            strAreaStat_msg += " width='" + (int)(float.Parse(strvallwhere.ToString()) / intmaxallwhere * 183) + "' height='12' alt='" + strthewhere + "，访问" + strvallwhere + "次，";
            strAreaStat_msg += lsbf + "% " + CopyRight + "'> " + strvallwhere + "</td></tr>";

            j++;
            if (j >= 40) break;

        }

        dv1.Dispose();

        return strAreaStat_msg;
    }

    /// <summary>
    ///来路统计的摘要说明。
    /// </summary>
    /// Code By ChenZhaoHui 

    public string ComeStat()
    {
        string strComeStat_msg = "";
        string strthecome;
        string strvallcome;
        string strsvcome = "";
        int lsbf;
        //参数传递，得到相应类别下的统计
        string viewid = Request.QueryString["id"];

        DataView dv1 = new DataView();
        DataTable dtss = sta.sel_vcome(viewid, SiteID);
        dv1 = dtss.DefaultView;
        dv1.Table.AcceptChanges();

        int intmaxallcome = 0;
        int intsumallcome = 0;
        for (int i = 0; i < dv1.Count; i++)
        {
            if (int.Parse(dv1[i].Row["allcome"].ToString()) > intmaxallcome) intmaxallcome = int.Parse(dv1[i].Row["allcome"].ToString());
            intsumallcome += int.Parse(dv1[i].Row["allcome"].ToString());
        }

        //防止除数为0出错
        if (intmaxallcome == 0) intmaxallcome = 1;
        if (intsumallcome == 0) intsumallcome = 1;

        int j = 0;
        for (int i = 0; i < dv1.Count; i++)
        {
            strthecome = dv1[i].Row["vcome"].ToString();
            strvallcome = dv1[i].Row["allcome"].ToString();

            int intthelen = strthecome.Length;

            if (intthelen == 0)
            {
                strthecome = "View.aspx";
                strsvcome = "通过收藏或直接输入网址访问";
                strComeStat_msg += "<tr><td width='220' align=right><a href='" + strthecome + "' target='_blank' class='menulist'  title='" + strthecome + "，访问" + strvallcome + "次，";

            }

            if (intthelen > 0 && intthelen <= 33)
            {
                strsvcome = strthecome;
                strComeStat_msg += "<tr><td width='220' align=right><a href='http://" + strthecome + "' target='_blank' class='menulist'  title='" + strthecome + "，访问" + strvallcome + "次，";
            }
            if (intthelen >= 34)
            {
                strsvcome = strthecome.Substring(0, 33) + "...";
                strComeStat_msg += "<tr><td width='220' align=right><a href='http://" + strthecome + "' target='_blank' class='menulist'  title='" + strthecome + "，访问" + strvallcome + "次，";
            }


            //计算访问量的百分数，精确到小数后1位，小于零的在前面加字母0
            lsbf = (int)(int.Parse(strvallcome.ToString()) * 1000 / intsumallcome) / 10;

            strComeStat_msg += lsbf + "% " + CopyRight + "'>" + strsvcome + "</a>&nbsp;</td>";
            strComeStat_msg += "<td width='230' background='../../sysImages/StatIcon/tu_back_2.gif' align=left>";
            strComeStat_msg += "<img style='BORDER-left: #000000 1px solid;' src='../../sysImages/StatIcon/tu.gif'";
            strComeStat_msg += " width='" + (int)(float.Parse(strvallcome.ToString()) / intmaxallcome * 183) + "' height='12' alt='" + strthecome + "，访问" + strvallcome + "次，";
            strComeStat_msg += lsbf + "% " + CopyRight + "'> " + strvallcome + "</td></tr>";

            j++;
            if (j >= 40) break;

        }

        dv1.Dispose();//释放资源

        return strComeStat_msg;

    }

    /// <summary>
    ///代码调用页的摘要说明。
    /// </summary>
    /// Code By ChenZhaoHui

    string GetCodeUse()//显示代码调用页面
    {

        //取得访问统计者的当前IP域名
        string statid = Request.QueryString["id"];//取得传递过来的参数值        
        string liststr = "<table width=\"98%\" border=\"0\" height=\"90\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"table\" id=\"CodeUseTable\">";
        liststr = liststr + "<tr class=\"TR_BG_list\">\r";
        liststr = liststr + "<td width='100%'class=\"list_link\" colspan=\"2\"><strong>流量统计代码调用</strong></td>\r";
        liststr = liststr + "</tr>\r";

        liststr = liststr + "<tr class=\"TR_BG_list\">\r";
        liststr = liststr + "<td width='12%' class=\"list_link\" valign=\"middle\" align=\"center\">滚动统计样式</td>\r";
        liststr = liststr + "<td width=\"88%\" valign=\"middle\" class=\"list_link\"><SPAN class=\"small2\">&lt;script language=&quot;JavaScript&quot; src=&quot;" + Foosun.Publish.CommonData.getUrl() + "/stat/mystat.aspx?code=1&id=" + statid + "&quot; type=&quot;text/JavaScript&quot;&gt;&lt;/script&gt;</SPAN></td>\r";
        liststr = liststr + "</tr>\r";

        liststr = liststr + "<tr class=\"TR_BG_list\">\r";
        liststr = liststr + "<td width='12%'class=\"list_link\" valign=\"middle\"><div align=\"center\">图标统计样式</div></td>\r";
        liststr = liststr + "<td width=\"89%\" valign=\"middle\" class=\"list_link\"><SPAN class=\"small2\">&lt;script language=&quot;JavaScript&quot; src=&quot;" + Foosun.Publish.CommonData.getUrl() + "/stat/mystat.aspx?code=2&id=" + statid + "&quot; type=&quot;text/JavaScript&quot;&gt;&lt;/script&gt;</SPAN></td>\r";
        liststr = liststr + "</tr>\r";

        liststr = liststr + "<tr class=\"TR_BG_list\">\r";
        liststr = liststr + "<td width='12%'class=\"list_link\" valign=\"middle\"><div align=\"center\">文字统计样式</div></td>\r";
        liststr = liststr + "<td width=\"88%\" valign=\"middle\" class=\"list_link\"><SPAN class=\"small2\">&lt;script language=&quot;JavaScript&quot; src=&quot;" + Foosun.Publish.CommonData.getUrl() + "/stat/mystat.aspx?code=0&id=" + statid + "&quot; type=&quot;text/JavaScript&quot;&gt;&lt;/script&gt;</SPAN></td>\r";
        liststr = liststr + "</tr>\r";
        liststr = liststr + "</table>";
        return liststr;
        //显示代码调用页结束
    }
}