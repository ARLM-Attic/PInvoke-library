///************************************************************************************************************
///**********广告公共页面Code By DengXi************************************************************************
///************************************************************************************************************
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

public partial class manage_advertisement_list : Foosun.Web.UI.ManagePage
{
    public manage_advertisement_list()
    {
        Authority_Code = "S006";
    }
    public string str_dirMana =Foosun.Config.UIConfig.dirDumm;//获取用户虚拟路径
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";                        //设置页面无缓存

        if (str_dirMana != "" && str_dirMana != null && str_dirMana != string.Empty)
            str_dirMana = "//" + str_dirMana;

        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;        //获取版权信息
        }
        string str_Type = Request.QueryString["Type"];
        if (str_Type != "" && str_Type != null)
            GetList(str_Type);
    }

    /// <summary>
    /// 获得功能菜单
    /// </summary>
    /// <param name="type">要显示的类型,分别为Ads,Class,Stat</param>
    /// <returns>获得功能菜单</returns>
    /// Code By DengXi

    protected string GetMenu(string type)
    {
        string temp_Str = "";

        switch (type)
        {
            case "Ads":
                temp_Str += "<table border=\"0\" width=\"98%\"><tr><td width=\"75%\" align=\"left\">" + GetShowType() + "</td>"+
                            "<td align=\"right\"><a href=\"ads_add.aspx\" class=\"list_link\">添加广告</a>&nbsp;┊&nbsp;" +
                            "<a href=\"javascript:DelAll('" + type + "');\" class=\"list_link\">全部删除</a>&nbsp;┊&nbsp;" +
                            "<a href=\"javascript:Del('" + type + "');\" class=\"list_link\">批量删除</a></td></tr></table>";
                break;
            case "Class":
                temp_Str += getSiteList();
                temp_Str += "  <a href=\"adsclass_add.aspx\" class=\"list_link\">添加分类</a>&nbsp;┊&nbsp;" +
                            "<a href=\"javascript:DelAll('" + type + "');\" class=\"list_link\">全部删除</a>&nbsp;┊&nbsp; " +
                            "<a href=\"javascript:Del('" + type + "');\" class=\"list_link\">批量删除</a>";
                break;
            case "Stat":
                temp_Str += getSiteList();
                temp_Str += "  <a href=\"javascript:DelAll('" + type + "');\" class=\"list_link\">全部重置</a>&nbsp;┊&nbsp;" +
                            "<a href=\"javascript:Del('" + type + "');\" class=\"list_link\">批量重置</a>";
                temp_Str += "";
                break;
        }
        string str_TempStr = "<table width=\"100%\" border=\"0\" cellpadding=\"3\" cellspacing=\"1\" align=\"center\">\r";
        str_TempStr += "<tr>\r";
        str_TempStr += "<td style=\"padding-right:15px;\" align=\"right\">\r";
        str_TempStr += temp_Str + "\r";
        str_TempStr += "</td>\r</tr>\r";
        str_TempStr += "</table>\r";
        return str_TempStr;
    }


    /// <summary>
    /// 判断ID是否合法
    /// </summary>
    /// <param name="idlist">要检测的编号</param>
    /// <returns>判断ID是否合法</returns>
    /// Code By DengXi

    protected string CheckID(string idlist)
    {
        idlist = Foosun.Common.Input.Losestr(idlist);
        if(idlist=="IsNull")
            PageError("请选择要批量操作的内容!", "");
        idlist = Foosun.Common.Input.CutComma(idlist);
        return idlist;
    }


    /// <summary>
    /// 获得列表
    /// </summary>
    /// <param name="type">要显示列表的类型</param>
    /// <returns>获得列表</returns>
    /// Code By DengXi

    protected void GetList(string type)
    {
        string str_OpType = Request.QueryString["OpType"];
        if (str_OpType != "" && str_OpType != null && str_OpType != string.Empty)
        {
            string str_ID = Request.QueryString["ID"];
            switch (str_OpType)
            { 
                case "adsLock":
                    adsLock(str_ID);
                    break;
                case "adsUnLock":
                    adsUnLock(str_ID);
                    break;
                case "adsDel":
                    adsDel(CheckID(str_ID));
                    break;
                case "adsDelAll":
                    adsDelAll();
                    break;
                case "classDel":
                    classDel(CheckID(str_ID));
                    break;
                case "classDelAll":
                    classDelAll();
                    break;
                case "statDel":
                    statDel(CheckID(str_ID));
                    break;
                case "statDelAll":
                    statDelAll();
                    break;
            }
        }

        string curPage = Request.QueryString["page"];    //当前页码
        string str_TempStr = GetMenu(type);
        int pageSize = 15, page = 0;                     //每页显示数

        if (curPage == "" || curPage == null || curPage == string.Empty) { page = 1; }
        else
        {
            try { page = int.Parse(curPage); }
            catch (Exception e)
            {
                PageError("参数错误！"+e.ToString()+"", "");
            }
        }
        DataTable dt = GetSql(type);
        if (dt != null)
        {
            int Cnt = dt.Rows.Count;

            //获得当前分页数
            int pageCount = Cnt / pageSize;
            if (Cnt % pageSize != 0) { pageCount++; }

            if (page > pageCount) { page = pageCount; }
            if (page < 1) { page = 1; }

            switch (type)
            {
                case "Ads":
                    str_TempStr = str_TempStr + GetPageInfo(dt, page, pageSize, Cnt, pageCount, type);
                    break;
                case "Class":
                    str_TempStr = str_TempStr + GetPageInfo(dt, page, pageSize, Cnt, pageCount, type);
                    break;
                case "Stat":
                    str_TempStr = str_TempStr + GetPageInfo(dt, page, pageSize, Cnt, pageCount, type);
                    break;
            }
            dt.Clear();
            dt.Dispose();
        }
        Response.Write(str_TempStr);
        Response.End();
    }

    /// <summary>
    /// 构造SQL语句
    /// </summary>
    /// <param name="type">要构造SQL语句的类型</param>
    /// <returns>返回SQL语句</returns>
    /// Code By DengXi

    protected DataTable GetSql(string type)
    {
        Foosun.Model.AdsListInfo ali = new Foosun.Model.AdsListInfo();

        ali.type = type;
        if (Request.QueryString["SiteID"] != null && Request.QueryString["SiteID"]!=string.Empty)
        {
            ali.showSiteID = Foosun.Common.Input.Filter(Request.QueryString["SiteID"]);
        }
        ali.showAdsType = Request.QueryString["showadstype"];
        if (Request.QueryString["adsType"] != null && Request.QueryString["adsType"] != string.Empty)
        {
            ali.adsType = Foosun.Common.Input.Filter(Request.QueryString["adsType"]);
        }

        ali.searchType = Request.QueryString["searchType"];
        if (Request.QueryString["SearchKey"] != null && Request.QueryString["SearchKey"] != string.Empty)
        {
            ali.SearchKey = Foosun.Common.Input.Filter(Request.QueryString["SearchKey"]);
        }

        if (Request.QueryString["searchType"] != null && Request.QueryString["searchType"] == "className")
        {
            ali.SearchKey = Foosun.Common.Input.Filter(Request.QueryString["SearchKey"]);
            ali.type = "ClassName";
        }
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        DataTable dt = ac.list(ali);
        return dt;
    }


    /// <summary>
    /// 取得分页
    /// </summary>
    /// <param name="page">当前页数</param>
    /// <param name="pageSize">每页显示多少条</param>
    /// <param name="Cnt">总记录数</param>
    /// <param name="url">链接地址</param>
    /// <param name="pageCount">分页总数</param>
    /// <param name="type">要显示的类型</param>
    /// <returns>返回分页</returns>
    /// Code By DengXi

    protected string ShowPage(int page, int pageSize, int Cnt, string url, int pageCount, string type)
    {
        string urlstr = "共" + Cnt.ToString() + "条记录,共" + pageCount.ToString() + "页,当前第" + page.ToString() + "页   ";
        urlstr = urlstr + "<a href=\"javascript:GetList('" + type + "',1)\" title=\"首页\" class=\"list_link\">首页</a> ";
        if ((page - 1) < 1)
            urlstr = urlstr + " <a href=\"javascript:GetList('" + type + "',1)\" title=\"上一页\" class=\"list_link\">上一页</a> ";
        else
            urlstr = urlstr + " <a href=\"javascript:GetList('" + type + "'," + (page - 1) + ")\" title=\"上一页\" class=\"list_link\">上一页</a> ";
        if ((page + 1) < pageCount)
            urlstr = urlstr + " <a href=\"javascript:GetList('" + type + "'," + (page + 1) + ")\" title=\"下一页\" class=\"list_link\">下一页</a> ";
        else
            urlstr = urlstr + " <a href=\"javascript:GetList('" + type + "'," + pageCount + ")\" title=\"下一页\" class=\"list_link\">下一页</a> ";
        urlstr = urlstr + " <a href=\"javascript:GetList('" + type + "'," + pageCount + ")\" title=\"尾页\" class=\"list_link\">尾页</a> ";
        return urlstr;
    }

    /// <summary>
    /// 取得分页
    /// </summary>
    /// <param name="dt">要用的分页的数据表</param>
    /// <param name="page">当前页数</param>
    /// <param name="pageSize">每页显示多少条</param>
    /// <param name="Cnt">总记录数</param>
    /// <param name="pageCount">分页总数</param>
    /// <param name="type">要显示的类型</param>
    /// <returns>返回分页</returns>
    /// Code By DengXi

    protected string GetPageInfo(DataTable dt, int page, int pageSize, int Cnt, int pageCount, string type)
    {
        string str_TempStr = "";
        string colnum = "";
        int i = 0;
        int j = 0;

        str_TempStr += "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"4\" cellspacing=\"1\" bgcolor=\"#FFFFFF\" class=\"table\">";
        str_TempStr += "<tr class=\"TR_BG\">";

        switch (type)
        {
            case "Ads":
                //str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">编号</td>";
                str_TempStr += "<td style=\"width:200px;\" align=\"left\" valign=\"middle\" class=\"sys_topBg\">名称</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">类型</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">类别</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">添加时间</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">所属用户</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">状态</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">操作 <input type=\"checkbox\" " +
                               "value=\"'-1'\" name=\"ID\" id=\"ID\" onclick=\"javascript:selectAll(this.form,this.checked)\" /> </td>";
                str_TempStr += "</tr>";
                for (i = (page - 1) * pageSize, j = 1; i < Cnt && j <= pageSize; i++, j++)
                {
                    string adsID = dt.Rows[i]["AdID"].ToString();
                    string adsName = dt.Rows[i]["adName"].ToString();
                    string adsType = dt.Rows[i]["adType"].ToString();
                    string adsAddTime = dt.Rows[i]["creatTime"].ToString();
                    string adsClassName = dt.Rows[i]["Cname"].ToString();

                    Foosun.CMS.Common.rootPublic rd = new Foosun.CMS.Common.rootPublic();
                    string uname  = rd.getUserName(dt.Rows[i]["CusID"].ToString());

                    string adsCusID = "<a target=\"_blank\" href=\"../../" + Foosun.Config.UIConfig.dirUser + "/"+
                                      "showuser.aspx?uid=" + rd.getUserName(dt.Rows[i]["CusID"].ToString()) + "\" class=\"list_link\">" + uname + "</a>";
                    string adsMode = dt.Rows[i]["isLock"].ToString();

                    string str_adsTempMode = "";
                    if (adsMode == "1") { str_adsTempMode = "<font color=\"red\">锁定</font>"; } else { str_adsTempMode = "正常"; }
                    string Op = " <a href=\"javascript:EditAds('" + type + "','" + adsID + "');\" class=\"list_link\">" +
                                "<img src=\"../../sysImages/"+Foosun.Config.UIConfig.CssPath()+"/sysico/edit.gif\" border=\"0\" alt=\"修改\" /></a>" +
                                "<a href=\"javascript:Lock('" + type + "','" + adsID + "');\" class=\"list_link\">" +
                                "<img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/lock.gif\" border=\"0\" alt=\"锁定\" /></a>" +
                                "<a href=\"javascript:UnLock('" + type + "','" + adsID + "');\" class=\"list_link\">" +
                                "<img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/unlock.gif\" border=\"0\" alt=\"解锁\" /></a>" +
                                " <a href=\"javascript:getCode('" + adsID + "');\" class=\"list_link\">代码</a> <input type=\"checkbox\" value=\"'" + adsID + "'\" id=\"ID\" name=\"ID\" />";
                    str_TempStr += "<tr class=\"TR_BG_list\" onmouseover=\"javascript:overColor(this);\" onmouseout=\"javascript:outColor(this);\">";
                    //str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + adsID + "</td>";
                    str_TempStr += "<td style=\"width:200px;\" align=\"left\" valign=\"middle\" height=\"20\">" + adsName + "</td>";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + GetAdsType(adsType) + "</td>";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + adsClassName + "</td>";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + adsAddTime + "</td>";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + adsCusID + "</td>";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + str_adsTempMode + "</td>";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + Op + "</td>";
                    str_TempStr += "</tr>";
                }
                colnum = "7";
                break;
            case "Class":
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">编号</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">名称</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">价格</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">添加时间</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">操作 <input type=\"checkbox\" "+
                               " value=\"'-1'\" name=\"ID\" id=\"ID\" onclick=\"javascript:selectAll(this.form,this.checked)\" /> </td>";
                str_TempStr += "</tr>";
                for (i = (page - 1) * pageSize, j = 1; i < Cnt && j <= pageSize; i++, j++)
                {
                    string adsClassID = dt.Rows[i]["AcID"].ToString();
                    string adsClassName = dt.Rows[i]["Cname"].ToString();
                    string adsClassMoney = dt.Rows[i]["Adprice"].ToString();
                    string adsClassAddTime = dt.Rows[i]["creatTime"].ToString();
                    string Op = " <a href=\"javascript:EditAds('" + type + "','" + adsClassID + "');\" class=\"list_link\">" +
                                "<img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改\" /></a>" +
                                "<a href=\"javascript:AddAdsClass('" + adsClassID + "');\" class=\"list_link\">" +
                                "<img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addclass.gif\" border=\"0\" alt=\"添加子类\" /></a><" +
                                "input type=\"checkbox\" value=\"'" + adsClassID + "'\" id=\"ID\" name=\"ID\" />";
                    str_TempStr += "<tr class=\"TR_BG_list\" onmouseover=\"javascript:overColor(this);\" onmouseout=\"javascript:outColor(this);\">";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + adsClassID + "</td>";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + adsClassName + "</td>";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + adsClassMoney + "</td>";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + adsClassAddTime + "</td>";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + Op + "</td>";
                    str_TempStr += "</tr>";

                    str_TempStr += ChildList(adsClassID, "┝", type); ;
                }
                colnum = "5";
                break;
            case "Stat":
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">编号</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">广告名称</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">点击数</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">显示次数</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">查看统计信息</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">操作 <input type=\"checkbox\" "+
                               " value=\"'-1'\" name=\"ID\" id=\"ID\" onclick=\"javascript:selectAll(this.form,this.checked)\" /> </td>";
                str_TempStr += "</tr>";
                for (i = (page - 1) * pageSize, j = 1; i < Cnt && j <= pageSize; i++, j++)
                {
                    string adsID = dt.Rows[i]["AdID"].ToString();
                    string adName = dt.Rows[i]["adName"].ToString();
                    string clicknum = dt.Rows[i]["ClickNum"].ToString();
                    string shownum = dt.Rows[i]["ShowNum"].ToString();
                    string op = "<input type=\"checkbox\" value=\"'" + adsID + "'\" id=\"ID\" name=\"ID\" />";
                    string LookInfo = "<a href=\"javascript:LookInfo('" + adsID + "');\" class=\"list_link\">"+
                                      "<img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/review.gif\" alt=\"查看此广告统计信息\" border=\"0\"></a>";
                    str_TempStr += "<tr class=\"TR_BG_list\" onmouseover=\"javascript:overColor(this);\" onmouseout=\"javascript:outColor(this);\">";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + adsID + "</td>";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + adName + "</td>";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + clicknum + "</td>";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + shownum + "</td>";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + LookInfo + "</td>";
                    str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + op + "</td>";
                    str_TempStr += "</tr>";
                } 
                colnum = "6";
                break;
        }
        string url = "Recyle.aspx?Type=" + type + "&page=";
        str_TempStr += "<tr class=\"TR_BG_list\" align=\"right\"><td colspan=\"" + colnum + "\">" + ShowPage(page, pageSize, Cnt, url, pageCount, type) + "</td></tr>";
        str_TempStr += "</table>";
        return str_TempStr;
    }

    /// <summary>
    /// 获得广告类型
    /// </summary>
    /// <param name="type">需要返回的广告类型</param>
    /// <returns>返回广告类型</returns>
    /// Code By DengXi

    protected string GetAdsType(string type)
    {
        string str_Type = "";
        switch (type)
        { 
            case "0":
                str_Type = "显示广告";
                break;
            case "1":
                str_Type = "弹出新窗口";
                break;
            case "2":
                str_Type = "打开新窗口";
                break;
            case "3":
                str_Type = "渐隐消失";
                break;
            case "4":
                str_Type = "网页对话框";
                break;
            case "5":
                str_Type = "透明对话框";
                break;
            case "6":
                str_Type = "满屏浮动";
                break;
            case "7":
                str_Type = "左下底端";
                break;
            case "8":
                str_Type = "右下底端";
                break;
            case "9":
                str_Type = "对联广告(顶端)";
                break;
            case "10":
                str_Type = "循环广告";
                break;
            case "11":
                str_Type = "文字广告";
                break;
            case "12":
                str_Type = "对联广告(底端)";
                break;
        }
        return str_Type;
    }


    /// <summary>
    /// 取得子类
    /// </summary>
    /// <param name="Classid">父类编号</param>
    /// <param name="sign">层次</param>
    /// <param name="type">操作的类型</param>
    /// <returns>返回子类编号</returns>
    /// Code By DengXi


    string ChildList(string Classid, string sign,string type)
    {
        string str_TempStr = "";
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();

        DataTable dv = ac.childlist(Classid);
        sign += "─";
        if (dv != null)
        {
            for (int i = 0; i < dv.Rows.Count; i++)
            {
                string adsClassID = dv.Rows[i]["AcID"].ToString();
                string adsClassName = dv.Rows[i]["Cname"].ToString();
                string adsClassMoney = dv.Rows[i]["Adprice"].ToString();
                string adsClassAddTime = dv.Rows[i]["creatTime"].ToString();
                string Op = " <a href=\"javascript:EditAds('" + type + "','" + adsClassID + "');\" class=\"list_link\">" +
                            "<img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改\" /></a>" +
                            "<a href=\"javascript:AddAdsClass('" + adsClassID + "');\" class=\"list_link\">" +
                            "<img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addclass.gif\" border=\"0\" alt=\"添加子类\" /></a>" +
                            "<input type=\"checkbox\" value=\"'" + adsClassID + "'\" id=\"ID\" name=\"ID\" />";

                str_TempStr += "<tr class=\"TR_BG_list\" onmouseover=\"javascript:overColor(this);\" onmouseout=\"javascript:outColor(this);\">";
                str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + adsClassID + "</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + sign + adsClassName + "</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + adsClassMoney + "</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + adsClassAddTime + "</td>";
                str_TempStr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + Op + "</td>";
                str_TempStr += "</tr>";
                str_TempStr += ChildList(adsClassID, sign, type);
            }
            dv.Clear();
            dv.Dispose();
        }
        return str_TempStr;
    }

    /// <summary>
    /// 锁定广告
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <returns>锁定广告</returns>
    /// Code By DengXi

    protected void adsLock(string adsID)
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        ac.Lock(adsID);
        PageRight("锁定广告成功!", "list.aspx");
    }

    /// <summary>
    /// 解锁广告
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <returns>解锁广告</returns>
    /// Code By DengXi

    protected void adsUnLock(string adsID)
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        ac.UnLock(adsID);
        PageRight("解锁广告成功!", "list.aspx");
    }


    /// <summary>
    /// 删除全部广告
    /// </summary>
    /// <returns>删除全部广告</returns>
    /// Code By DengXi

    protected void adsDelAll()
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        ac.DelAllAds();
        PageRight("删除全部广告成功!", "list.aspx");
    }

    /// <summary>
    /// 批量删除广告
    /// </summary>
    /// <param name="idStr">广告编号</param>
    /// <returns>批量删除广告</returns>
    /// Code By DengXi

    protected void adsDel(string idStr)
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        ac.DelPAds(idStr);
        PageRight("批量删除广告成功!", "list.aspx");
    }


    /// <summary>
    /// 删除全部栏目
    /// </summary>
    /// <returns>删除全部栏目</returns>
    /// Code By DengXi

    protected void classDelAll()
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        ac.DelAllAdsClass();
        PageRight("删除全部栏目成功!", "list.aspx");
    }

    /// <summary>
    /// 批量删除栏目
    /// </summary>
    /// <param name="idStr">广告编号</param>
    /// <returns>批量删除栏目</returns>
    /// Code By DengXi

    protected void classDel(string idStr)
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        ac.DelPAdsClass(idStr);
        PageRight("批量删除栏目成功!", "list.aspx");
    }

    /// <summary>
    /// 重置全部统计信息
    /// </summary>
    /// <returns>重置全部统计信息</returns>
    /// Code By DengXi

    protected void statDelAll()
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        ac.statDelAll();
        PageRight("重置全部统计信息成功!", "list.aspx");
    }

    /// <summary>
    /// 批量重置统计信息
    /// </summary>
    /// <param name="idStr">广告编号</param>
    /// <returns>批量重置统计信息</returns>
    /// Code By DengXi

    protected void statDel(string idStr)
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        ac.statDel(idStr);
        PageRight("批量重置统计信息成功!", "list.aspx");
    }

    /// <summary>
    /// 显示广告类型下拉列表框
    /// </summary>
    /// <param name="type">广告类型</param>
    /// Code By DengXi
   
    protected string GetShowType()
    { 
        string adsType = Request.QueryString["adsType"];
        string selected = "";
        string temp_Str = "";
        temp_Str="<select name=\"adType\" id=\"adType\" onchange=\"javascript:ShowType(this.value);\" style=\"width:100px;\">";
        if (adsType == "-1") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"-1\" " + selected + ">所有广告</option>";
        if (adsType == "0") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"0\" " + selected + ">显示广告</option>";
        if (adsType == "1") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"1\" " + selected + ">弹出新窗口</option>";
        if (adsType == "2") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"2\" " + selected + ">打开新窗口</option>";
        if (adsType == "3") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"3\" " + selected + ">渐隐消失</option>";
        if (adsType == "4") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"4\" " + selected + ">网页对话框</option>";
        if (adsType == "5") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"5\" " + selected + ">透明对话框</option>";
        if (adsType == "6") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"6\" " + selected + ">满屏浮动</option>";
        if (adsType == "7") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"7\" " + selected + ">左下底端</option>";
        if (adsType == "8") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"8\" " + selected + ">右下底端</option>";
        if (adsType == "9") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"9\" " + selected + ">对联广告(顶端)</option>";
        if (adsType == "10") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"10\" " + selected + ">循环广告</option>";
        if (adsType == "11") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"11\" " + selected + ">文字广告</option>";
        if (adsType == "12") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"12\" " + selected + ">对联广告(底端)</option>";
        temp_Str += "</select>&nbsp;";
        temp_Str += getSiteList();

        temp_Str += " 搜索广告 <select name=\"SearchType\" id=\"SearchType\" style=\"width:80px;\">";
        if (Request.QueryString["SearchType"] == "adsname") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"adsname\" " + selected + ">广告名称</option>";
        if (Request.QueryString["SearchType"] == "user") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"user\" " + selected + ">所属用户</option>";
        if (Request.QueryString["SearchType"] == "className") { selected = "selected"; } else { selected = ""; }
            temp_Str += "<option value=\"className\" " + selected + ">所属类别</option>";
        temp_Str += "</select> ";
        temp_Str += "<input type=\"text\" style=\"width:80px;\" name=\"SearchKey\" class=\"form\" value=\"" + Request.QueryString["SearchKey"] + "\" /> ";
        temp_Str += "<input type=\"button\" value=\"搜 索\" class=\"form\" onclick=\"SearchGo();\" /> ";
        return temp_Str;
    }

    protected string getSiteList()
    {
        string tempStr = "";
        if (SiteID == "0")
        {
            UserMisc uc = new UserMisc();
            DataTable dt = uc.getSiteList();
            if (dt != null)
            {
                tempStr = "<select name=\"Site\" id=\"Site\" width=\"200px\" onChange=\"changeSite(this.value);\">";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string tempsiteid = dt.Rows[i]["ChannelID"].ToString();
                    string tempchnnelid = dt.Rows[i]["ChannelID"].ToString();
                    string tempcname = dt.Rows[i]["CName"].ToString();
                    if ((Request.QueryString["SiteID"] == "" || Request.QueryString["SiteID"] == null) && tempchnnelid == "0")
                        tempStr += "<option value=\"" + tempchnnelid + "\" selected>" + tempcname + "</option>\r";
                    else
                    {
                        if (Request.QueryString["SiteID"] == tempsiteid)
                            tempStr += "<option value=\"" + tempchnnelid + "\" selected>" + tempcname + "</option>\r";
                        else
                            tempStr += "<option value=\"" + tempchnnelid + "\">" + tempcname + "</option>\r";
                    }
                }
                tempStr += "</select>";
                dt.Clear(); dt.Dispose();
            }
        }
        return tempStr;
    }
}
