///************************************************************************************************************
///**********回收站  Code By DengXi****************************************************************************
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

public partial class manage_news_Recyle : Foosun.Web.UI.ManagePage
{
    public manage_news_Recyle()
    {
        Authority_Code = "Q027";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";                        //设置页面无缓存
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;              //获取版权信息
        }
        string str_Type = Request.QueryString["Type"];
        if (str_Type != "" && str_Type != null)
        {
            GetList(str_Type);
        }
    }

    /// <summary>
    /// 取得列表
    /// </summary>
    /// <param name="type">当前显示的类型</param>
    /// <returns>取得列表</returns>
    /// Code By DengXi

    protected void GetList(string type)
    {
        string curPage = Request.QueryString["page"];    //当前页码
        string str_TempStr = GetMenu(type);
        ReOp(type);
        int pageSize = 20, page = 0;                     //每页显示数

        if (curPage == "" || curPage == null || curPage == string.Empty) { page = 1; }
        else
        {
            try { page = int.Parse(curPage); }
            catch (Exception e)
            {
                PageError("参数错误！<li>" + e.ToString()+ "</li>", "");
            }
        }
        if (type == "APIList")
        {
            Response.Write("<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"4\" cellspacing=\"1\" bgcolor=\"#FFFFFF\" class=\"table\"><tr class=\"TR_BG_list\"><td align=\"left\">API</td></tr></table>");
            Response.End();
        }
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        DataTable dt = rc.getList(type);
 
        if (dt != null)
        {
            int Cnt = dt.Rows.Count;

            //获得当前分页数-----------------------------------------------------
            int pageCount = Cnt / pageSize;
            if (Cnt % pageSize != 0) { pageCount++; }

            if (page > pageCount) { page = pageCount; }
            if (page < 1) { page = 1; }

            str_TempStr = str_TempStr + "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"4\" cellspacing=\"1\" bgcolor=\"#FFFFFF\" class=\"table\">";
            str_TempStr = str_TempStr + "<tr class=\"TR_BG\">";
            str_TempStr = str_TempStr + "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\" style=\"width:20%\">编号</td>";

            switch (type)
            {
                case "NCList":      //新闻栏目列表
                    str_TempStr = str_TempStr + GetPageInfo(dt, "栏目中文名", page, pageSize, Cnt, pageCount, type);
                    break;
                case "NList":       //新闻列表
                    str_TempStr = str_TempStr + GetPageInfo(dt, "新闻标题", page, pageSize, Cnt, pageCount, type);
                    break;
                case "CList":       //频道列表
                    str_TempStr = str_TempStr + GetPageInfo(dt, "频道中文名", page, pageSize, Cnt, pageCount, type);
                    break;
                case "SList":       //专题列表
                    str_TempStr = str_TempStr + GetPageInfo(dt, "专题中文名", page, pageSize, Cnt, pageCount, type);
                    break;
                case "LCList":      //标签栏目列表
                    str_TempStr = str_TempStr + GetPageInfo(dt, "标签栏目名称", page, pageSize, Cnt, pageCount, type);
                    break;
                case "LList":       //标签列表
                    str_TempStr = str_TempStr + GetPageInfo(dt, "标签名称", page, pageSize, Cnt, pageCount, type);
                    break;
                case "PSFList":     //PSF结点列表
                    str_TempStr = str_TempStr + GetPageInfo(dt, "结点名称", page, pageSize, Cnt, pageCount, type);
                    break;
                case "StCList":     //样式栏目列表
                    str_TempStr = str_TempStr + GetPageInfo(dt, "样式栏目名称", page, pageSize, Cnt, pageCount, type);
                    break;
                case "StList":     //样式列表
                    str_TempStr = str_TempStr + GetPageInfo(dt, "样式名称", page, pageSize, Cnt, pageCount, type);
                    break;
                case "APIList":     //APIList列表
                    str_TempStr = "APIList";
                    break;
                default:
                    str_TempStr = "参数错误!";
                    break;
            }
            dt.Clear();
            dt.Dispose();
        }
        Response.Write(str_TempStr);
        Response.End();
    }

    /// <summary>
    /// 获得前台功能菜单
    /// </summary>
    /// <param name="type">当前显示的类型</param>
    /// <returns>获得前台功能菜单</returns>
    /// Code By DengXi
    
    protected string GetMenu(string type)
    {
        string temp_Classstr = "";
        string str_TempStr = "<table width=\"100%\" border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\" align=\"center\">\r";
        str_TempStr += "<tr>\r";
        str_TempStr += "<td style=\"padding-right:15px;\" align=\"right\">\r";
        str_TempStr += "<a href=\"javascript:RAll('" + type + "');\" class=\"list_link\">全部恢复</a>&nbsp;┊&nbsp;\r";
        str_TempStr += "<a href=\"javascript:DAll('" + type + "');\" class=\"list_link\">全部删除</a>&nbsp;┊&nbsp;\r";
        str_TempStr += "<a href=\"javascript:PR('" + type + "');\" class=\"list_link\">批量恢复</a>&nbsp;┊&nbsp;\r";
        str_TempStr += "<a href=\"javascript:PD('" + type + "');\" class=\"list_link\">批量删除</a> \r";
        
        Foosun.CMS.AdminGroup ac = new Foosun.CMS.AdminGroup();
        if (type == "NList")
        {
            DataTable dt = ac.getClassList("ClassID,ClassCName,ParentID", "news_Class", "Where isRecyle=0 And SiteID='" + SiteID + "'");
            temp_Classstr = "请指定一个新闻栏目(<span class=\"helpstyle\" style=\"cursor:help;\" title=\"点击显示帮助\" onclick=\"Help('H_Recyle_001',this)\">帮助</span> ) <select name=\"className\" id=\"className\" style=\"width:200px;padding-bottom:0;padding-left:0;padding-right:0;padding-top:0;\" class=\"form\"><option value=\"\">请选择要恢复到那个栏目</option>" + listShow(dt, "0", 0) + "</select>\r";
        }
        if (type == "StList")
        {
            DataTable dt = ac.getClassList("ClassID,Sname", "sys_styleclass", "Where isRecyle=0 And SiteID='" + SiteID + "'");
            temp_Classstr = "请指定一个样式栏目(<span class=\"helpstyle\" style=\"cursor:help;\" title=\"点击显示帮助\" onclick=\"Help('H_Recyle_002',this)\">帮助</span> ) " + GetSClist(dt) + "\r";
        }
        if (type == "LList")
        {
            DataTable dt = ac.getClassList("ClassID,ClassName", "sys_LabelClass", "Where isRecyle=0 And SiteID='" + SiteID + "'");
            temp_Classstr = "请指定一个标签栏目(<span class=\"helpstyle\" style=\"cursor:help;\" title=\"点击显示帮助\" onclick=\"Help('H_Recyle_003',this)\">帮助</span> ) " + GetSClist(dt) + "\r";
        }
        str_TempStr += temp_Classstr;
        str_TempStr += "</td>\r</tr>\r";
        str_TempStr += "</table>\r";
        return str_TempStr;
    }


    /// <summary>
    /// 返回列表
    /// </summary>
    /// <param name="tempdt">DataTable</param>
    /// <param name="PID">父类编号</param>
    /// <param name="Layer">层次</param>

    protected string listShow(DataTable tempdt, string PID, int Layer)
    {
        string str_list = "";
        DataRow[] row = null;
        row = tempdt.Select("ParentID='" + PID + "'");
        if (row.Length < 1)
            return str_list;
        else
        {
            foreach (DataRow r in row)
            {
                string strText = "┝";
                for (int j = 0; j < Layer; j++)
                {
                    strText += "┉";
                }
                str_list += "<option value=\"" + r[0].ToString() + "\">" + strText + r[1].ToString() +"</option>";
                if (r[0].ToString() != "0")
                    str_list += listShow(tempdt, r[0].ToString(), Layer + 1);
            }
        }
        return str_list;
    }

    /// <summary>
    /// 显示分页
    /// </summary>
    /// <param name="page">当前页数</param>
    /// <param name="pageSize">每页显示多少条</param>
    /// <param name="Cnt">总记录数</param>
    /// <param name="url">链接地址</param>
    /// <param name="pageCount">分页总数</param>
    /// <param name="type">当前显示的类型</param>
    /// <returns>显示分页</returns>
    /// Code By DengXi

    protected string ShowPage(int page, int pageSize, int Cnt, string url, int pageCount, string type)
    {
        string urlstr = "共" + Cnt.ToString() + "条记录,共" + pageCount.ToString() + "页,当前第" + page.ToString() + "页   ";
        urlstr += "<a href=\"javascript:GetList('" + type + "',1)\" title=\"首页\" class=\"list_link\">首页</a> ";
        if ((page - 1) < 1)
            urlstr += " <a href=\"javascript:GetList('" + type + "',1)\" title=\"上一页\" class=\"list_link\">上一页</a> ";
        else
            urlstr += " <a href=\"javascript:GetList('" + type + "'," + (page - 1) + ")\" title=\"上一页\" class=\"list_link\">上一页</a> ";
        if ((page + 1) < pageCount)
            urlstr += " <a href=\"javascript:GetList('" + type + "'," + (page + 1) + ")\" title=\"下一页\" class=\"list_link\">下一页</a> ";
        else
            urlstr += " <a href=\"javascript:GetList('" + type + "'," + pageCount + ")\" title=\"下一页\" class=\"list_link\">下一页</a> ";
        urlstr += " <a href=\"javascript:GetList('" + type + "'," + pageCount + ")\" title=\"尾页\" class=\"list_link\">尾页</a> ";
        return urlstr;
    }

    /// <summary>
    /// 分页公共部份
    /// </summary>
    /// <param name="dt">要显示列表的数据表</param>
    /// <param name="Cm">列表要显示的中文名称</param>
    /// <param name="page">当前页数</param>
    /// <param name="pageSize">每页显示多少条</param>
    /// <param name="Cnt">总记录数</param>
    /// <param name="pageCount">分页总数</param>
    /// <param name="type">当前显示的类型</param>
    /// <returns>分页公共部份</returns>
    /// Code By DengXi
    
    protected string GetPageInfo(DataTable dt,string Cm, int page, int pageSize, int Cnt, int pageCount, string type)
    {
        string str_TempStr = "";
        int i = 0;
        int j = 0;
        str_TempStr = str_TempStr + "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\" style=\"width:50%\">" + Cm + "</td>";
        str_TempStr = str_TempStr + "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">操作 <input type=\"checkbox\" value=\"'-1'\" name=\"ID\" id=\"ID\" onclick=\"javascript:selectAll(this.form,this.checked)\" /></td>";
        str_TempStr = str_TempStr + "</tr>";
        for (i = (page - 1) * pageSize, j = 1; i < Cnt && j <= pageSize; i++, j++)
        {
            string ID = dt.Rows[i][1].ToString();
            string CName = dt.Rows[i][2].ToString();
            string Op = "<input type=\"checkbox\" value=\"'" + ID + "'\" id=\"ID\" name=\"ID\" />";

            str_TempStr = str_TempStr + "<tr class=\"TR_BG_list\" onmouseover=\"javascript:overColor(this);\" onmouseout=\"javascript:outColor(this);\">";
            str_TempStr = str_TempStr + "<td align=\"left\" valign=\"middle\" height=\"20\">" + ID + "</td>";
            str_TempStr = str_TempStr + "<td align=\"left\" valign=\"middle\" height=\"20\">" + CName + "</td>";
            str_TempStr = str_TempStr + "<td align=\"left\" valign=\"middle\" height=\"20\">" + Op + "</td>";
            str_TempStr = str_TempStr + "</tr>";
        }
        string url = "Recyle.aspx?Type=" + type + "&page=";
        str_TempStr = str_TempStr + "<tr class=\"TR_BG_list\" align=\"right\"><td colspan=\"3\">" + ShowPage(page, pageSize, Cnt, url, pageCount, type) + "</td></tr>";
        str_TempStr = str_TempStr + "</table>";
        return str_TempStr;
    }

    /// <summary>
    /// 操作
    /// </summary>
    /// <param name="type">要操作的类型</param>
    /// <returns>操作</returns>
    /// Code By DengXi

    protected void ReOp(string type)
    {
        string str_Optype = Request.QueryString["Op"];
        string str_IdList = Request.QueryString["idlist"];
        if (str_Optype != null && str_Optype!="")
        {
            switch (str_Optype)
            {
                case "RAll"://全部恢复
                    this.Authority_Code = "Q027";
                    this.CheckAdminAuthority();
                    RAll(type);
                    break;
                case "DAll"://全部删除
                    this.Authority_Code = "Q027";
                    this.CheckAdminAuthority();
                    DAll(type);
                    break;
                case "PR":  //批量恢复
                    this.Authority_Code = "Q027";
                    this.CheckAdminAuthority();
                    PR(type, Foosun.Common.Input.CutComma(CheckID(str_IdList)));
                    break;
                case "PD":  //批量删除
                    this.Authority_Code = "Q027";
                    this.CheckAdminAuthority();
                    PD(type, Foosun.Common.Input.CutComma(CheckID(str_IdList)));
                    break;           
            }
        }
    }

    /// <summary>
    /// 检测ID是否合法
    /// </summary>
    /// <param name="idlist">要检测的ID</param>
    /// <returns>检测ID是否合法</returns>
    /// Code By DengXi
    
    protected string CheckID(string idlist)
    {
        idlist = Foosun.Common.Input.Losestr(idlist);
        if (idlist == "IsNull")
            PageError("请选择要批量操作的内容!", "");
        return idlist;
    }

    /// <summary>
    /// 全部恢复
    /// </summary>
    /// <param name="type">要恢复的类型</param>
    /// <returns>全部恢复</returns>
    /// Code By DengXi

    protected void RAll(string type)
    {
        switch (type)
        {
            case "NCList":      //新闻栏目
                RallNCList();   
                break;
            case "NList":       //新闻
                RallNList();
                break;
            case "CList":       //频道
                RallCList();
                break;
            case "SList":       //专题
                RallSList();
                break;
            case "LCList":     //标签栏目
                RallLCList();
                break;
            case "LList":       //标签
                RallLList();
                break;
            case "StCList":     //样式栏目
                RallStCList();
                break;
            case "StList":     //样式
                RallStList();
                break;
            case "PSFList":     //PSF结点
                RallPSFList();
                break;
        }
    }

    /// <summary>
    /// 全部删除
    /// </summary>
    /// <param name="type">全部删除的类型</param>
    /// <returns>全部删除</returns>
    /// Code By DengXi

    protected void DAll(string type)
    {
        switch (type)
        {
            case "NCList":      //新闻栏目
                DallNCList();
                break;
            case "NList":       //新闻
                DallNList();
                break;
            case "CList":       //频道
                DallCList();
                break;
            case "SList":       //专题
                DallSList();
                break;
            case "StCList":     //样式栏目
                DallStCList();
                break;
            case "StList":     //样式
                DallStList();
                break;
            case "LCList":     //标签栏目
                DallLCList();
                break;
            case "LList":       //标签
                DallLList();
                break;
            case "PSFList":     //PSF结点
                DallPSFList();
                break;
        }
    }

    /// <summary>
    /// 批量恢复
    /// </summary>
    /// <param name="type">批量恢复的类型</param>
    /// <param name="idlist">批量恢复的ID</param>
    /// <returns>批量恢复</returns>
    /// Code By DengXi

    protected void PR(string type,string idlist)
    {
        switch (type)
        {
            case "NCList":      //新闻栏目
                PRNCList(idlist);
                break;
            case "NList":       //新闻
                PRNList(idlist);
                break;
            case "CList":       //频道
                PRCList(idlist);
                break;
            case "SList":       //专题
                PRSList(idlist);
                break;
            case "StCList":     //样式栏目
                PRStCList(idlist);
                break;
            case "StList":     //样式
                PRStList(idlist);
                break;
            case "LCList":     //标签栏目
                PRLCList(idlist);
                break;
            case "LList":       //标签
                PRLList(idlist);
                break;
            case "PSFList":     //PSF结点
                PRPSFList(idlist);
                break;
        }
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="type">批量删除的类型</param>
    /// <param name="idlist">批量删除的ID</param>
    /// <returns>批量删除</returns>
    /// Code By DengXi

    protected void PD(string type, string idlist)
    {
        switch (type)
        {
            case "NCList":      //新闻栏目
                PDNCList(idlist);
                break;
            case "NList":       //新闻
                PDNList(idlist);
                break;
            case "CList":       //频道
                PDCList(idlist);
                break;
            case "SList":       //专题
                PDSList(idlist);
                break;
            case "LCList":     //标签栏目
                PDLCList(idlist);
                break;
            case "LList":       //标签
                PDLList(idlist);
                break;
            case "StCList":     //样式栏目
                PDStCList(idlist);
                break;
            case "StList":     //样式
                PDStList(idlist);
                break;
            case "PSFList":     //PSF结点
                PDPSFList(idlist);
                break;
        }
    }


    /// <summary>
    /// 恢复全部新闻栏目
    /// </summary>
    /// <returns>恢复全部新闻栏目</returns>
    /// Code By DengXi

    protected void RallNCList()
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.RallNCList();
        PageRight("恢复全部新闻栏目成功!", "");
    }

    /// <summary>
    /// 恢复全部新闻
    /// </summary>
    /// <returns>恢复全部新闻</returns>
    /// Code By DengXi

    protected void RallNList()
    {
        string str_ClassID = Request.QueryString["className"];
        if (str_ClassID == "" || str_ClassID == null || str_ClassID == string.Empty)
            PageError("请指定一个栏目!", "");
        else
        {
            Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
            rc.RallNList(str_ClassID);
            PageRight("恢复全部新闻成功!", "");
        }
    }

    /// <summary>
    /// 恢复全部频道
    /// </summary>
    /// <returns>恢复全部频道</returns>
    /// Code By DengXi

    protected void RallCList()
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.RallCList();
        PageRight("恢复全部频道成功!", "");
    }

    /// <summary>
    /// 恢复全部专题
    /// </summary>
    /// <returns>恢复全部专题</returns>
    /// Code By DengXi

    protected void RallSList()
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.RallSList();
        PageRight("恢复全部专题成功!", "");
    }

    /// <summary>
    /// 恢复全部标签栏目
    /// </summary>
    /// <returns>恢复全部标签栏目</returns>
    /// Code By DengXi

    protected void RallLCList()
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.RallLCList();
        PageRight("恢复全部标签栏目成功!", "");
    }

    /// <summary>
    /// 恢复全部标签
    /// </summary>
    /// <returns>恢复全部标签</returns>
    /// Code By DengXi

    protected void RallLList()
    {
        string str_ClassID = Request.QueryString["className"];
        if (str_ClassID == "" || str_ClassID == null || str_ClassID == string.Empty)
            PageError("请选择要将标签恢复到那个栏目!", "");
        else
        {
            Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
            rc.RallLList(str_ClassID);
            PageRight("恢复全部标签成功!", "");
        }
    }

    /// <summary>
    /// 恢复全部样式栏目
    /// </summary>
    /// <returns>恢复全部样式栏目</returns>
    /// Code By DengXi

    protected void RallStCList()
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.RallStCList();
        PageRight("恢复全部样式栏目成功!", "");
    }

    /// <summary>
    /// 恢复全部样式
    /// </summary>
    /// <returns>恢复全部样式</returns>
    /// Code By DengXi

    protected void RallStList()
    {
        string str_ClassID = Request.QueryString["className"];
        if (str_ClassID == "" || str_ClassID == null || str_ClassID == string.Empty)
            PageError("请选择要将样式恢复到那个栏目!", "");
        else
        {
            Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
            rc.RallStList(str_ClassID);
            PageRight("恢复全部样式成功!", "");
        }
    }


    /// <summary>
    /// 恢复全部PSF结点
    /// </summary>
    /// <returns>恢复全部PSF结点</returns>
    /// Code By DengXi

    protected void RallPSFList()
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.RallPSFList();
        PageRight("恢复全部PSF(结点)成功!", "");
    }

    /// <summary>
    /// 删除全部新闻栏目
    /// </summary>
    /// <returns>删除全部新闻栏目</returns>
    /// Code By DengXi
        
    protected void DallNCList()
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.DallNCList();
        PageRight("删除全部新闻栏目成功!", "");
    }


    /// <summary>
    /// 删除全部新闻
    /// </summary>
    /// <returns>删除全部新闻</returns>
    /// Code By DengXi

    protected void DallNList()
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.DallNList();
        PageRight("删除全部新闻成功!", "");
    }

    /// <summary>
    /// 删除全部频道
    /// </summary>
    /// <returns>删除全部频道</returns>
    /// Code By DengXi

    protected void DallCList()
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.DallCList();
        PageRight("删除全部频道成功!", "");
    }

    /// <summary>
    /// 删除全部专题
    /// </summary>
    /// <returns>删除全部专题</returns>
    /// Code By DengXi

    protected void DallSList()
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.DallSList();
        PageRight("删除全部专题成功!", "");
    }

    /// <summary>
    /// 删除全部标签栏目
    /// </summary>
    /// <returns>删除全部标签栏目</returns>
    /// Code By DengXi

    protected void DallLCList()
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.DallLCList();
        PageRight("删除全部标签栏目成功!", "");
    }


    /// <summary>
    /// 删除全部标签
    /// </summary>
    /// <returns>删除全部标签</returns>
    /// Code By DengXi

    protected void DallLList()
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.DallLList();
        PageRight("删除全部标签成功!", "");
    }


    /// <summary>
    /// 删除全部样式栏目
    /// </summary>
    /// <returns>删除全部样式栏目</returns>
    /// Code By DengXi

    protected void DallStCList()
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.DallStCList();
        PageRight("删除全部样式栏目成功!", "");
    }


    /// <summary>
    /// 删除全部样式
    /// </summary>
    /// <returns>删除全部样式</returns>
    /// Code By DengXi

    protected void DallStList()
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.DallStList();
        PageRight("删除全部样式成功!", "");
    }

    /// <summary>
    /// 删除全部PSF结点
    /// </summary>
    /// <returns>删除全部PSF结点</returns>
    /// Code By DengXi

    protected void DallPSFList()
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.DallPSFList();
        PageRight("删除全部PSF结点成功!", "");
    }

    /// <summary>
    /// 批量恢复新闻栏目
    /// </summary>
    /// <param name="idlist">新闻栏目编号</param>
    /// <returns>批量恢复新闻栏目</returns>
    /// Code By DengXi

    protected void PRNCList(string idlist)
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.PRNCList(idlist);
        PageRight("批量恢复新闻栏目成功!", "");
    }

    /// <summary>
    /// 批量恢复新闻
    /// </summary>
    /// <param name="idlist">新闻编号</param>
    /// <returns>批量恢复新闻</returns>
    /// Code By DengXi

    protected void PRNList(string idlist)
    {
        string str_ClassID = Request.QueryString["className"];
        if (str_ClassID == "" || str_ClassID == null || str_ClassID == string.Empty)
            PageError("请选择要将新闻恢复到那个栏目!", "");
        else
        {
            Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
            rc.PRNList(str_ClassID,idlist);
            PageRight("批量恢复新闻成功!", "");
        }
    }

    /// <summary>
    /// 批量恢复频道
    /// </summary>
    /// <param name="idlist">频道编号</param>
    /// <returns>批量恢复频道</returns>
    /// Code By DengXi

    protected void PRCList(string idlist)
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.PRCList(idlist);
        PageRight("批量恢复频道成功!", "");
     }

    /// <summary>
    /// 批量恢复专题
    /// </summary>
    /// <param name="idlist">专题编号</param>
    /// <returns>批量恢复专题</returns>
    /// Code By DengXi

    protected void PRSList(string idlist)
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.PRSList(idlist);
        PageRight("批量恢复专题成功!", "");
    }

    /// <summary>
    /// 批量恢复样式栏目
    /// </summary>
    /// <param name="idlist">样式栏目编号</param>
    /// <returns>批量恢复样式栏目</returns>
    /// Code By DengXi

    protected void PRStCList(string idlist)
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.PRStCList(idlist);
        PageRight("批量恢复样式栏目成功!", "");
    }

    /// <summary>
    /// 批量恢复样式
    /// </summary>
    /// <param name="idlist">样式编号</param>
    /// <returns>批量恢复样式</returns>
    /// Code By DengXi

    protected void PRStList(string idlist)
    {
        string str_ClassID = Request.QueryString["className"];
        if (str_ClassID == "" || str_ClassID == null || str_ClassID == string.Empty)
            PageError("请选择要将样式恢复到那个栏目!", "");
        else
        {
            Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
            rc.PRStList(str_ClassID, idlist);
            PageRight("批量恢复样式成功!", "");
        }
    }

    /// <summary>
    /// 批量恢复标签栏目
    /// </summary>
    /// <param name="idlist">标签栏目编号</param>
    /// <returns>批量恢复标签栏目</returns>
    /// Code By DengXi

    protected void PRLCList(string idlist)
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.PRLCList(idlist);
        PageRight("批量恢复标签栏目成功!", "");
    }

    /// <summary>
    /// 批量恢复标签
    /// </summary>
    /// <param name="idlist">标签编号</param>
    /// <returns>批量恢复标签</returns>
    /// Code By DengXi


    protected void PRLList(string idlist)
    {
        string str_ClassID = Request.QueryString["className"];
        if (str_ClassID == "" || str_ClassID == null || str_ClassID == string.Empty)
            PageError("请选择要将标签恢复到那个栏目!", "");
        else
        {
            Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
            rc.PRLList(str_ClassID, idlist);
            PageRight("批量恢复标签成功!", "");
        }
    }

    /// <summary>
    /// 批量恢复PSF结点
    /// </summary>
    /// <param name="idlist">PSF结点编号</param>
    /// <returns>批量恢复PSF结点</returns>
    /// Code By DengXi

    protected void PRPSFList(string idlist)
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.PRPSFList(idlist);
        PageRight("批量恢复PSF(结点)成功!", "");
    }

    /// <summary>
    /// 批量删除新闻栏目
    /// </summary>
    /// <param name="idlist">新闻栏目编号</param>
    /// <returns>批量删除新闻栏目</returns>
    /// Code By DengXi

    protected void PDNCList(string idlist)
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.PDNCList(idlist);
        PageRight("批量删除新闻栏目成功!", "");
    }


    /// <summary>
    /// 批量删除新闻
    /// </summary>
    /// <param name="idlist">新闻编号</param>
    /// <returns>批量删除新闻</returns>
    /// Code By DengXi


    protected void PDNList(string idlist)
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.PDNList(idlist);
        PageRight("批量删除新闻成功!", "");
    }

    /// <summary>
    /// 批量删除频道
    /// </summary>
    /// <param name="idlist">频道编号</param>
    /// <returns>批量删除频道</returns>
    /// Code By DengXi

    protected void PDCList(string idlist)
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.PDCList(idlist);
        PageRight("批量删除频道成功!", "");
    }

    /// <summary>
    /// 批量删除专题
    /// </summary>
    /// <param name="idlist">专题编号</param>
    /// <returns>批量删除专题</returns>
    /// Code By DengXi

    protected void PDSList(string idlist)
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.PDSList(idlist);
        PageRight("批量删除专题成功!", "");
    }


    /// <summary>
    /// 批量删除样式栏目
    /// </summary>
    /// <param name="idlist">样式栏目编号</param>
    /// <returns>批量删除样式栏目</returns>
    /// Code By DengXi

    protected void PDStCList(string idlist)
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.PDStCList(idlist);
        PageRight("批量删除样式栏目成功!", "");
    }

    /// <summary>
    /// 批量删除样式
    /// </summary>
    /// <param name="idlist">样式编号</param>
    /// <returns>批量删除样式</returns>
    /// Code By DengXi

    protected void PDStList(string idlist)
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.PDStList(idlist);
        PageRight("批量删除样式成功!", "");
    }


    /// <summary>
    /// 批量删除标签栏目
    /// </summary>
    /// <param name="idlist">标签栏目编号</param>
    /// <returns>批量删除标签栏目</returns>
    /// Code By DengXi

    protected void PDLCList(string idlist)
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.PDLCList(idlist);
        PageRight("批量删除标签栏目成功!", "");
    }


    /// <summary>
    /// 批量删除标签
    /// </summary>
    /// <param name="idlist">标签编号</param>
    /// <returns>批量删除标签</returns>
    /// Code By DengXi

    protected void PDLList(string idlist)
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.PDLList(idlist);
        PageRight("批量删除标签成功!", "");
    }

    /// <summary>
    /// 批量删除PSF结点
    /// </summary>
    /// <param name="idlist">PSF结点编号</param>
    /// <returns>批量删除PSF结点</returns>
    /// Code By DengXi

    protected void PDPSFList(string idlist)
    {
        Foosun.CMS.Recyle rc = new Foosun.CMS.Recyle();
        rc.PDPSFList(idlist);
        PageRight("批量删除PSF(结点)成功!", "");
    }
    
    /// <summary>
    /// 取得样式栏目列表
    /// </summary>
    /// <returns>返回样式栏目下拉列表框</returns>
    /// Code By DengXi
   
    protected string GetSClist(DataTable dt)
    {
        string str_TempStr = "<select name=\"className\" id=\"className\" style=\"width:200px;padding-bottom:0;padding-left:0;padding-right:0;padding-top:0;\" class=\"form\">";
        str_TempStr = str_TempStr + "<option value=\"\">请选择要恢复到那个栏目</option>";
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() != "99999999")
                    str_TempStr = str_TempStr + "<option value=\"" + dt.Rows[i][0] + "\">" + dt.Rows[i][1] + "</option>";
            }
            dt.Clear();
            dt.Dispose();
        }
        str_TempStr = str_TempStr + "</select>";
        return str_TempStr;
    }
}
