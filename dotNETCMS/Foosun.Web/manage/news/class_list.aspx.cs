using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using Foosun.CMS;
using Foosun.CMS.Common;
using Foosun.Model;

using System.Configuration;
using System.Data.SqlClient;

public partial class manage_news_class_list : Foosun.Web.UI.ManagePage
{
    public manage_news_class_list()
    {
        Authority_Code = "C019";
    }
    ContentManage rd = new ContentManage();
    rootPublic pd = new rootPublic();
    Foosun.CMS.UserMisc ud = new Foosun.CMS.UserMisc();
    DataTable newsClassTable = null;
    //加载函数
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";
        string ReadType = Foosun.Common.Public.readparamConfig("ReviewType");
        if (ReadType == "1")
        {
            makeHTML2.Enabled = false;
            makeHTML2.Text = "";
        }
        else
        {
            this.makeHTML2.Attributes.Add("onclick", "javascript:return checkTF('生成静态文件');");
        }
        this.ClassIndex.Attributes.Add("onclick", "javascript:return checkTF('生成索引页');");
        this.makeXML2.Attributes.Add("onclick", "javascript:return checkTF('生成XML');");
        string stat = Request.QueryString["Stat"];
        if (!IsPostBack)
        {
            if (Foosun.Config.verConfig.PublicType != "1")
            {
                ClassIndex.Enabled = false;
            }
            if (stat != "" && stat != null)
            {
                this.Authority_Code = "C029";
                this.CheckAdminAuthority();
                string Classid = Foosun.Common.Input.Filter(Request.QueryString["id"]);
                StaticChange(Classid);
                pd.SaveUserAdminLogs(0, 1, UserNum, "锁定/解锁操作", "锁定/解锁操作栏目.ClassID:" + Request.Form["Checkbox1"] + "");
                PageRight("锁定/解锁操作栏目成功!", "class_list.aspx");
            }
            string getSiteID = Request.QueryString["SiteID"];
            if (SiteID == "0")
            {
                if (getSiteID != null && getSiteID != "")
                {
                    channelList.InnerHtml = "&nbsp;&nbsp;" + SiteList(getSiteID.ToString());
                }
                else
                {
                    channelList.InnerHtml = "&nbsp;&nbsp;" + SiteList(SiteID);
                }
            }
            //分页
            StartLoad(1);
        }
        if (Request.QueryString["Type"] == "orderAction")
        {
            string ClassId = Foosun.Common.Input.Filter(Request.QueryString["ClassId"].ToString());
            int orderId = int.Parse(Request.QueryString["OrderId"].ToString());
            updateOrder(ClassId, orderId);
        }

        //设置节点ID
        //取出内容
        System.Collections.ArrayList arr = (System.Collections.ArrayList)Session["__ParentIDList"];
        if (arr == null || arr.Count == 0)
        {
            this.HiddenField_ParentID.Value = "";
        }
        else
        {
            string parentIDList = string.Empty;
            for (int i = 0; i < arr.Count; i++)
            {
                parentIDList += arr[i].ToString() + "|";
            }
            this.HiddenField_ParentID.Value = parentIDList;
        }

    }

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        StartLoad(PageIndex);
    }

    /// <summary>
    /// 得到站点列表
    /// </summary>
    /// <param name="SessionSiteID">内存总的SiteID</param>
    /// <returns>返回列表</returns>
    protected string SiteList(string SessionSiteID)
    {
        string siteStr = "<select name=\"SiteID\" id=\"SiteID\" onChange=\"getchanelInfo(this)\">\r";
        DataTable crs = ud.getSiteList();
        if (crs != null)
        {
            for (int i = 0; i < crs.Rows.Count; i++)
            {
                string getSiteID = SessionSiteID;
                string SiteID1 = crs.Rows[i]["ChannelID"].ToString();
                if (getSiteID != SiteID1)
                {
                    siteStr += "<option value=\"" + crs.Rows[i]["ChannelID"] + "\">" + crs.Rows[i]["CName"] + "</option>\r";
                }
                else
                {
                    siteStr += "<option value=\"" + crs.Rows[i]["ChannelID"] + "\"  selected=\"selected\">" + crs.Rows[i]["CName"] + "</option>\r";
                }
            }
        }
        //}
        siteStr += "</select>\r";
        return siteStr;
    }

    //更新权重
    protected void updateOrder(string ClassID, int OrderID)
    {
        rd.updateOrderP(ClassID, OrderID);
        pd.SaveUserAdminLogs(0, 1, UserNum, "更新权重", "ClassID:" + ClassID + "");
        PageRight("更新权重成功!", "class_list.aspx");
    }

    //更改操作状态
    protected void StaticChange(string Classid)
    {
        // 获取id编号
        int intStr = 0;
        DataTable dt = rd.getLock(Classid.ToString());
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                intStr = int.Parse(dt.Rows[0]["isLock"].ToString());
            }
            dt.Clear(); dt.Dispose();
        }
        if (intStr == 0)
        {
            intStr = 1;
        }
        else
        {
            intStr = 0;
        }
        rd.ChangeLock(Classid, intStr);
    }

    //批量锁定/解锁数据
    protected void Lock_Click(object sender, EventArgs e)
    {
        this.Authority_Code = "C029";
        this.CheckAdminAuthority();
        string Str = Request.Form["Checkbox1"];
        if (Str == null || Str == string.Empty)
        {
            PageError("请至少选择一项!", "");
        }
        else
        {
            string[] Checkbox = (Str.ToString()).Split(',');
            for (int i = 0; i < Checkbox.Length; i++)
            {
                StaticChange(Checkbox[i]);
            }
            pd.SaveUserAdminLogs(0, 1, UserNum, "锁定/解锁操作", "锁定/解锁操作栏目.ClassID:" + Request.Form["Checkbox1"] + "");
            //此处进行静态文件的删除
            PageRight("锁定/解锁操作栏目成功,请返回继续操作!", "class_list.aspx");
        }
    }



    //数据初始化
    protected void StartLoad(int PageIndex)
    {
        int i, j;
        string _SiteID = Request.QueryString["SiteID"];
        DataTable dt = null;
        if (_SiteID != null && _SiteID != string.Empty)
        {
            if (SiteID == "0")
            {
                SQLConditionInfo st = new SQLConditionInfo("@SiteID", _SiteID.ToString());
                dt = Foosun.CMS.Pagination.GetPage("manage_news_class_list_1_aspx", PageIndex, 50, out i, out j, st);
            }
            else
            {
                dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 50, out i, out j, null);
            }
        }
        else
        {
            SQLConditionInfo st = new SQLConditionInfo("@SiteID", SiteID);
            dt = Foosun.CMS.Pagination.GetPage("manage_news_class_list_1_aspx", PageIndex, 50, out i, out j, st);
        }
        //else
        //{
        //     dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 50, out i, out j, null);
        //}

        if (dt != null)
        {
            dt.Columns.Add("st", typeof(string));
            dt.Columns.Add("pop", typeof(string));
            dt.Columns.Add("Colum", typeof(string));
            dt.Columns.Add("ClassCNames", typeof(string));

            this.PageNavigator1.PageCount = j;
            this.PageNavigator1.PageIndex = PageIndex;
            this.PageNavigator1.RecordCount = i;

            for (int k = 0; k < dt.Rows.Count; k++)
            {
                this.ClassID = dt.Rows[k]["classid"].ToString();

                string strchar = "";
                //取出子类
                if (dt.Rows[k]["isPage"].ToString() == "1")
                {
                    dt.Rows[k]["ClassCNames"] = "<a href=\"news_Page.aspx?ClassID=" + Server.UrlEncode(Convert.ToString(dt.Rows[k]["ClassID"])) + "&Action=Edit\" class=\"list_link\" title=\"点击修改单页面\">" + dt.Rows[k]["ClassCName"] + "</a>";
                }
                else
                {
                    //时间：2008-07-22 title显示模板 修改者：吴静岚
                    dt.Rows[k]["ClassCNames"] = "<a href=\"Class_Add.aspx?Cname=" + Server.UrlEncode(Convert.ToString(dt.Rows[k]["ClassCName"])) + "&Acation=Add," + dt.Rows[k]["ClassID"] + "&SiteID=" + Request.QueryString["SiteID"] + "\" class=\"list_link\" title=\"点击修改栏目&#13;栏目模板：" + dt.Rows[k]["ClassTemplet"].ToString() + "&#13;内容模板：" + dt.Rows[k]["ReadNewsTemplet"].ToString() + "\">" + dt.Rows[k]["ClassCName"] + "[" + dt.Rows[k]["ClassEname"] + "]</a>";
                    //--wjl>
                }
                if (dt.Rows[k]["IsURL"].ToString() == "1")
                {
                    dt.Rows[k]["st"] = "<font color=blue>外部</font>&nbsp;&nbsp;";
                }
                else
                {
                    dt.Rows[k]["st"] = "<font color=red>系统</font>&nbsp;&nbsp;";
                }
                if (dt.Rows[k]["isPage"].ToString() == "1")
                {
                    dt.Rows[k]["st"] += "<font color=\"#FF9900\">单页</font>&nbsp;&nbsp;";
                }
                else
                {
                    dt.Rows[k]["st"] += "普通&nbsp;&nbsp;";
                }

                if (dt.Rows[k]["IsLock"].ToString() == "1")
                {
                    dt.Rows[k]["st"] += "<a href=\"?Stat=Change&id=" + dt.Rows[k]["ClassID"] + "\" title=\"点击正常\" class=\"list_link\">锁定</a> ";
                }
                else
                {
                    dt.Rows[k]["st"] += "<a href=\"?Stat=Change&id=" + dt.Rows[k]["ClassID"] + "\" title=\"点击锁定\" class=\"list_link\"><font color=\"green\">正常</font></a> ";
                }

                if (dt.Rows[k]["Domain"].ToString().Length > 5)
                {
                    dt.Rows[k]["st"] += "<font color=\"blue\">域</font>&nbsp;&nbsp;";
                }
                else
                {
                    dt.Rows[k]["st"] += "<font color=\"#999999\">域</font>&nbsp;&nbsp;";
                }
                if (dt.Rows[k]["NaviShowtf"].ToString() == "1")
                {
                    dt.Rows[k]["st"] += "<font color=\"red\">显示</font>&nbsp;&nbsp;";
                }
                else
                {
                    dt.Rows[k]["st"] += "隐藏&nbsp;&nbsp;";
                }

                string _TempStr = "";
                if (dt.Rows[k]["IsURL"].ToString() == "0")
                {
                    if (dt.Rows[k]["isPage"].ToString() == "0")
                    {
                        _TempStr = "<a title=\"添加新闻\" href=\"News_add.aspx?ClassID=" + dt.Rows[k]["ClassID"].ToString() + "&EditAction=add\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addnews.gif\" border=\"0\" title=\"添加新闻\" /></a><a href=\"Class_add.aspx?Cname=" + Server.UrlEncode(Convert.ToString(dt.Rows[k]["ClassCName"])) + "&Number=" + dt.Rows[k]["ClassID"] + "&SiteID=" + Request.QueryString["SiteID"] + "\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addclass.gif\" border=\"0\" title=\"添加子类\" /></a><a href=\"news_Page.aspx?Number=" + dt.Rows[k]["ClassID"] + "\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addpage.gif\" border=\"0\" title=\"添加单页面\" /></a>";
                    }
                }
                if (dt.Rows[k]["isPage"].ToString() == "1")
                {
                    dt.Rows[k]["pop"] = "<input name=\"Checkbox1\" type=\"checkbox\" value=" + dt.Rows[k]["ClassID"] + " />&nbsp;&nbsp;<a href=\"news_Page.aspx?ClassID=" + dt.Rows[k]["ClassID"] + "&Action=Edit\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/edit.gif\" border=\"0\" title=\"修改\" /></a><a href=\"news_review.aspx?ID=" + dt.Rows[k]["ClassID"] + "&type=class\" class=\"list_link\" target=\"_blank\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/review.gif\" border=\"0\" title=\"浏览\" /></a>" + _TempStr + "";
                }
                else
                {
                    dt.Rows[k]["pop"] = "<input name=\"Checkbox1\" type=\"checkbox\" value=" + dt.Rows[k]["ClassID"] + " />&nbsp;&nbsp;<a href=\"Class_Add.aspx?Cname=" + Server.UrlEncode(Convert.ToString(dt.Rows[k]["ClassCName"])) + "&Acation=Add," + dt.Rows[k]["ClassID"] + "&SiteID=" + Request.QueryString["SiteID"] + "\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/edit.gif\" border=\"0\" title=\"修改\" /></a><a href=\"news_review.aspx?ID=" + dt.Rows[k]["ClassID"] + "&type=class\" class=\"list_link\" target=\"_blank\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/review.gif\" border=\"0\" title=\"浏览\" /></a>" + _TempStr + "";
                }

                if (!this.CheckAuthority())
                {
                    //dt.Rows[k]["ClassCNames"] = dt.Rows[k]["ClassCName"];
                    ////strchar += "<tr class=\"TR_BG_list\"  onmouseover=\"overColor(this)\" onmouseout=\"outColor(this)\">";
                    ////strchar += "<td  align=\"center\" valign=\"middle\" height=20>" + dt.Rows[k]["id"] + "</td>";
                    ////strchar += "<td  align=\"left\" valign=\"middle\" colspan=\"4\"><img src=\"../../sysImages/folder/yess.gif\" border=\"0\">没有权限操作此栏目</span></td>";
                    ////strchar += "</tr>";
                    //strchar += "<tr class=\"TR_BG_list\"  onmouseover=\"overColor(this)\" onmouseout=\"outColor(this)\">";
                    //strchar += "<td  align=\"center\" valign=\"middle\" height=20>" + dt.Rows[k]["id"] + "</td>";
                    //DataTable dc = rd.checkHasSub(dt.Rows[k]["ClassID"].ToString());
                    //if (int.Parse(dc.Rows[0][0].ToString()) > 0)

                    //    strchar += "<td  align=\"left\" valign=\"middle\" ><img src=\"../../sysImages/normal/b.gif\" style=\"cursor:hand\" onClick=\"javascript:SwitchImg(this,'" + dt.Rows[k]["ClassID"] + "');\"  border=\"0\">&nbsp;" + dt.Rows[k]["ClassCNames"] + "<span style=\"font-size:10px;color:red\" title=\"新闻数\">(" + rd.getClassNewsCount(dt.Rows[k]["ClassID"].ToString()) + ")</span></td>";

                    //else

                    //    strchar += "<td  align=\"left\" valign=\"middle\" ><img src=\"../../sysImages/normal/s.gif\" style=\"cursor:pointer;\" alt=\"没有子栏目\"  border=\"0\" class=\"LableItem\" />&nbsp;" + dt.Rows[k]["ClassCNames"] + "<span style=\"font-size:10px;color:red\" title=\"新闻数\">(" + rd.getClassNewsCount(dt.Rows[k]["ClassID"].ToString()) + ")</span></td>";

                    //strchar += "<td  align=\"center\" valign=\"middle\" ><strong>" + dt.Rows[k]["OrderID"] + "</strong></td>";
                    //strchar += "<td  align=\"center\" valign=\"middle\" >" + dt.Rows[k]["st"] + "</td>";
                    //strchar += "<td valign=\"middle\" >" + "无权限" + "</td>";
                    //strchar += "</tr>";
                }
                else
                {
                    strchar += "<tr class=\"TR_BG_list\"  onmouseover=\"overColor(this)\" onmouseout=\"outColor(this)\">";
                    strchar += "<td  align=\"center\" valign=\"middle\" height=20>" + dt.Rows[k]["id"] + "</td>";
                    DataTable dc = rd.checkHasSub(dt.Rows[k]["ClassID"].ToString());
                    if (int.Parse(dc.Rows[0][0].ToString()) > 0)

                        strchar += "<td  align=\"left\" valign=\"middle\" ><img id=\"img_parentid_" + dt.Rows[k]["ClassID"] + "\" src=\"../../sysImages/normal/b.gif\" style=\"cursor:hand\" onClick=\"javascript:SwitchImg(this,'" + dt.Rows[k]["ClassID"] + "');\"  border=\"0\">&nbsp;" + dt.Rows[k]["ClassCNames"] + "<span style=\"font-size:10px;color:red\" title=\"新闻数\">(" + rd.getClassNewsCount(dt.Rows[k]["ClassID"].ToString()) + ")</span></td>";

                    else

                        strchar += "<td  align=\"left\" valign=\"middle\" ><img src=\"../../sysImages/normal/s.gif\" style=\"cursor:pointer;\" alt=\"没有子栏目\"  border=\"0\" class=\"LableItem\" />&nbsp;" + dt.Rows[k]["ClassCNames"] + "<span style=\"font-size:10px;color:red\" title=\"新闻数\">(" + rd.getClassNewsCount(dt.Rows[k]["ClassID"].ToString()) + ")</span></td>";

                    strchar += "<td  align=\"center\" valign=\"middle\" ><a class=\"list_link\" href=\"javascript:orderAction(" + dt.Rows[k]["ClassID"] + "," + dt.Rows[k]["OrderID"] + ");\" title=\"点击排序\"><strong>" + dt.Rows[k]["OrderID"] + "</a></strong></td>";
                    strchar += "<td  align=\"center\" valign=\"middle\" >" + dt.Rows[k]["st"] + "</td>";
                    strchar += "<td valign=\"middle\" >" + dt.Rows[k]["pop"] + "</td>";
                    strchar += "</tr>";
                    strchar += "<tr class=\"TR_BG_list\"><td colspan=\"5\"><div id=\"Parent" + dt.Rows[k]["ClassID"].ToString() + "\" style=\" display:none;\"></div></td></tr>";
                }
                
                //strchar += getchildClassList(dt.Rows[k]["ClassID"].ToString(), "┝");
                dt.Rows[k]["Colum"] = strchar;
            }
        }
        DataList1.DataSource = dt;
        DataList1.DataBind();
    }

    //递归
    string getchildClassList(string Classid, string sign)
    {
        string strchar = "";

        sign += " ┉ ";
        if (newsClassTable != null)
        {
            if (newsClassTable.Rows.Count > 0)
            {

                DataRow[] rows = newsClassTable.Select(string.Format("ParentID='{0}'", Classid.Replace("'", "''")), "OrderID Desc,id desc");
                foreach (DataRow row in rows)
                {
                    this.ClassID = row["classid"].ToString();
                    if (row["isPage"].ToString() == "1")
                    {
                        row["ClassCNames"] = "<a href=\"news_Page.aspx?ClassID=" + row["ClassID"] + "&Action=Edit\" class=\"list_link\" title=\"点击修改单页面\">" + row["ClassCName"] + "</a>";
                    }
                    else
                    {
                        row["ClassCNames"] = "<a href=\"Class_Add.aspx?Cname=" + Server.UrlEncode(Convert.ToString(row["ClassCName"])) + "&Acation=Add," + row["ClassID"] + "&SiteID=" + Request.QueryString["SiteID"] + "\" class=\"list_link\" title=\"点击修改栏目\">" + row["ClassCName"] + "[" + row["ClassEname"] + "]</a>";
                    }
                    if (row["IsURL"].ToString() == "1")
                    {
                        row["st"] = "<font color=\"blue\">外部</font>&nbsp;&nbsp;";
                    }
                    else
                    {
                        row["st"] = "<font color=\"red\">系统</font>&nbsp;&nbsp;";
                    }
                    if (row["isPage"].ToString() == "1")
                    {
                        row["st"] += "<font color=\"#FF9900\">单页</font>&nbsp;&nbsp;";
                    }
                    else
                    {
                        row["st"] += "普通&nbsp;&nbsp;";
                    }
                    if (row["IsLock"].ToString() == "1")
                    {
                        row["st"] += "<a href=\"?Stat=Change&id=" + row["ClassID"] + "\" title=\"点击正常\" class=\"list_link\">锁定</a> ";
                    }
                    else
                    {
                        row["st"] += "<a href=\"?Stat=Change&id=" + row["ClassID"] + "\" title=\"点击锁定\" class=\"list_link\"><font color=\"green\">正常</font></a> ";
                    }
                    if (row["Domain"].ToString().Length > 5)
                    {
                        row["st"] += "<font color=\"blue\">域</font>&nbsp;&nbsp;";
                    }
                    else
                    {
                        row["st"] += "<font color=\"#999999\">域</font>&nbsp;&nbsp;";
                    }
                    if (row["NaviShowtf"].ToString() == "1")
                    {
                        row["st"] += "<font color=\"red\">显示</font>&nbsp;&nbsp;";
                    }
                    else
                    {
                        row["st"] += "隐藏&nbsp;&nbsp;";
                    }

                    string _TempStr = "";
                    if (row["IsURL"].ToString() == "0")
                    {
                        if (row["isPage"].ToString() == "0")
                        {
                            _TempStr = "<a title=\"添加新闻\" href=\"News_add.aspx?ClassID=" + row["ClassID"].ToString() + "&EditAction=add\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addnews.gif\" border=\"0\" /></a><a href=\"Class_add.aspx?Cname=" + Server.UrlEncode(Convert.ToString(row["ClassCName"])) + "&Number=" + row["ClassID"] + "&SiteID=" + Request.QueryString["SiteID"] + "\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addclass.gif\" border=\"0\" title=\"添加子类\" /></a><a href=\"news_Page.aspx?Number=" + row["ClassID"] + "\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addpage.gif\" border=\"0\" title=\"添加单页面\" /></a>";
                        }
                    }

                    //操作
                    if (row["isPage"].ToString() == "1")
                    {
                        row["pop"] = "<input name=\"Checkbox1\" type=\"checkbox\" value=" + row["ClassID"] + " />&nbsp;&nbsp;<a href=\"news_Page.aspx?ClassID=" + row["ClassID"] + "&Action=Edit\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/edit.gif\" border=\"0\" title=\"修改\"></a><a href=\"news_review.aspx?ID=" + row["ClassID"] + "&type=class\" class=\"list_link\" target=\"_blank\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/review.gif\" border=\"0\" title=\"浏览\" /></a>" + _TempStr + "";
                    }
                    else
                    {
                        row["pop"] = "<input name=\"Checkbox1\" type=\"checkbox\" value=" + row["ClassID"] + " />&nbsp;&nbsp;<a href=\"Class_Add.aspx?Cname=" + Server.UrlEncode(Convert.ToString(row["ClassCName"])) + "&Acation=Add," + row["ClassID"] + "&SiteID=" + Request.QueryString["SiteID"] + "\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/edit.gif\" border=\"0\" title=\"修改\"></a><a href=\"news_review.aspx?ID=" + row["ClassID"] + "&type=class\" class=\"list_link\" target=\"_blank\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/review.gif\" border=\"0\" title=\"浏览\" /></a>" + _TempStr + "";
                    }

                    if (!this.CheckAuthority())
                    {
                        //strchar += "<tr class=\"TR_BG_list\"  onmouseover=\"overColor(this)\" onmouseout=\"outColor(this)\">";
                        //strchar += "<td  align=\"center\" valign=\"middle\" height=20>" + row["id"] + "</td>";
                        //strchar += "<td  align=\"left\" valign=\"middle\" colspan=\"4\"><img src=\"../../sysImages/folder/yess.gif\" border=\"0\">没有权限操作此栏目</span></td>";
                        //strchar += "</tr>";
                    }
                    else
                    {
                        strchar += "<tr class=\"TR_BG_list\"  onmouseover=\"overColor(this)\" onmouseout=\"outColor(this)\">";
                        strchar += "<td align=\"center\" valign=\"middle\" height=20>" + row["id"] + "</td>";
                        strchar += "<td align=\"left\" valign=\"middle\" >" + sign + row["ClassCNames"] + "<span style=\"font-size:10px;color:red;\" title=\"新闻数\">(" + rd.getClassNewsCount(row["ClassID"].ToString()) + ")</span></td>";
                        strchar += "<td align=\"center\" valign=\"middle\" ><a class=\"list_link\" href=\"javascript:orderAction(" + row["ClassID"] + "," + row["OrderID"] + ");\" title=\"点击排序\"><strong>" + row["OrderID"] + "</strong></a></td>";
                        strchar += "<td align=\"center\" valign=\"middle\" >" + row["st"] + "</td>";
                        strchar += "<td valign=\"middle\" >" + row["pop"] + "</td>";
                        strchar += "</tr>";
                    }
                    strchar += getchildClassList(row["ClassID"].ToString(), sign);
                    row["Colum"] = strchar;
                }

            }
        }
        return strchar;
    }

    //彻底批量删除数据
    protected void Selected_del_Click(object sender, EventArgs e)
    {
        this.Authority_Code = "C030";
        this.CheckAdminAuthority();
        String Str = Request.Form["Checkbox1"];
        if (Str == null || Str == String.Empty)
        {
            PageError("请先选择删除项!", "");
        }
        else
        {
            String[] Checkbox = Str.Split(',');
            Str = null;
            for (int i = 0; i < Checkbox.Length; i++)
            {
                //如果此栏目下有新闻则不删除
                if (rd.getClassNewsCount(Checkbox[i].ToString()) == 0)
                {
                    DataTable dt = rd.getChildList(Checkbox[i]);
                    if (dt.Rows.Count == 0)
                    {
                        rd.del_Class(Checkbox[i]);
                        rd.GetChildClassdel(Checkbox[i]);
                    }
                    else
                    {
                        PageError("删除数据到回收站失败!此栏目下有子栏目,不能删除", "class_list.aspx");
                    }
                }
                else
                {
                    PageError("彻底删除栏目失败!原因:此栏目下有新闻.", "class_list.aspx");
                }
            }
            pd.SaveUserAdminLogs(1, 1, UserNum, "删除栏目", "彻底删除栏目.ClassID:" + Request.Form["Checkbox1"] + "");
            //此处进行静态文件的删除
            PageRight("彻底删除栏目成功!", "class_list.aspx");
        }
    }

    /// <summary>
    /// 初始化栏目
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void del_allClass(object sender, EventArgs e)
    {
        //权限管理
        this.Authority_Code = "C027";
        this.CheckAdminAuthority();
        rd.delClassAll();
        pd.SaveUserAdminLogs(1, 1, UserNum, "初始化栏", "删除了所有栏目及内容信息");
        //此处进行静态文件的删除
        PageRight("初始化栏成功!", "class_list.aspx");
    }

    /// <summary>
    /// 生成XML
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void makeXML(object sender, EventArgs e)
    {
        this.Authority_Code = "C031";
        this.CheckAdminAuthority();
        string Str = Request.Form["Checkbox1"];
        if (Str == null || Str == String.Empty)
        {
            PageError("请至少选择一项!", "");
        }
        else
        {
            string[] Checkbox = Str.Split(',');
            Str = null;
            int j = 0;
            for (int i = 0; i < Checkbox.Length; i++)
            {
                if (Foosun.Publish.General.publishXML(Checkbox[i]))
                {
                    j++;
                }
            }
            PageRight("生成" + j + "个XML成功!", "class_list.aspx");
        }
    }

    /// <summary>
    /// 生成HTML,生成静态文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void makeHTML(object sender, EventArgs e)
    {
        this.Authority_Code = "C032";
        this.CheckAdminAuthority();
        string Str = Request.Form["Checkbox1"];
        if (Str == null || Str == String.Empty)
        {
            PageError("请至少选择一项!", "");
        }
        else
        {
            Foosun.Control.HProgressBar.Start();
            Foosun.Publish.General PG = new Foosun.Publish.General();
            try
            {
                Foosun.Control.HProgressBar.Roll("正在发布栏目", 0);
                string[] Checkboxs = Str.Split(',');
                Str = null;
                int j = 0;
                int m = Checkboxs.Length;
                for (int i = 0; i < m; i++)
                {
                    if (rd.getclassPage(Checkboxs[i]) == 0)
                    {
                        if (PG.publishSingleClass(Checkboxs[i].ToString()))
                        {
                            j++;
                        }
                    }
                    else
                    {
                        if (Foosun.Publish.General.publishPage(Checkboxs[i].ToString()))
                        {
                            j++;
                        }
                    }
                    Foosun.Control.HProgressBar.Roll("共生成" + m + "个栏目，正在发布" + (i + 1) + "个。", ((i + 1) * 100 / m));
                }
                Foosun.Control.HProgressBar.Roll("发布栏目成功,成功" + j + "个,<a href=\"../Publish/error/geterror.aspx?\">失败" + (Checkboxs.Length - j) + "个(可能有栏目有浏览权限)</a>. &nbsp;<a href=\"class_list.aspx\">返回</a>", 100);
            }
            catch (Exception ex)
            {
                Foosun.Common.Public.savePublicLogFiles("□□□发布栏目", "【错误描述：】\r\n" + ex.ToString(), UserName);
                Foosun.Control.HProgressBar.Roll("发布栏目失败。<a href=\"../publish/error/geterror.aspx?\">查看日志</a>", 0);
            }
            Response.End();
        }
    }

    protected void makeClassIndex(object sender, EventArgs e)
    {
        string Str = Request.Form["Checkbox1"];
        if (Str == null || Str == String.Empty)
        {
            PageError("请至少选择一项!", "");
        }
        else
        {
            string[] Checkboxs = Str.Split(',');
            Str = null;
            int j = 0;
            int m = 0;
            for (int i = 0; i < Checkboxs.Length; i++)
            {
                if (rd.getclassPage(Checkboxs[i]) == 0)
                {
                    if (Foosun.Publish.General.publishClassIndex(Checkboxs[i]))
                    {
                        j++;
                    }
                    else
                    {
                        m++;
                    }
                }
            }
            PageRight("共生成" + j + "个栏目!失败" + m + "个栏目。<li>如果生成有差异，可能是您选择了单页面</li>", "class_list.aspx");
        }
    }

    /// <summary>
    /// 清除数据
    /// 清空栏目数据 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void clearNewsInfo(object sender, EventArgs e)
    {
        this.Authority_Code = "C033";
        this.CheckAdminAuthority();
        String Str = Request.Form["Checkbox1"];
        if (Str == null || Str == String.Empty)
        {
            PageError("请至少选择一项!", "");
        }
        else
        {
            String[] Checkbox = Str.Split(',');
            Str = null;
            for (int i = 0; i < Checkbox.Length; i++)
            {
                rd.clearNewsInfo(Checkbox[i]);
            }
            pd.SaveUserAdminLogs(1, 1, UserNum, "清除数据", "清除数据.ClassID:" + Request.Form["Checkbox1"] + "");
            PageRight("清除数据成功!", "class_list.aspx");
        }
    }


    //放入回收站
    protected void AllDel_Click(object sender, EventArgs e)
    {
        this.Authority_Code = "CE01";
        this.CheckAdminAuthority();
        String Str = Request.Form["Checkbox1"];
        if (Str == null || Str == String.Empty)
        {
            PageError("请先选择删除项!", "");
        }
        else
        {
            String[] Checkbox = Str.Split(',');
            Str = null;
            for (int i = 0; i < Checkbox.Length; i++)
            {
                //如果此栏目下有新闻则不删除
                if (rd.getClassNewsCount(Checkbox[i].ToString()) == 0)
                {
                    DataTable dt = rd.getChildList(Checkbox[i]);
                    if (dt.Rows.Count == 0)
                    {
                        rd.del_recyleClass(Checkbox[i]);
                        rd.GetChildClassdel_recyle(Checkbox[i]);
                    }
                    else
                    {
                        PageError("删除数据到回收站失败!此栏目下有子栏目,不能删除", "class_list.aspx");
                    }
                }
                else
                {
                    PageError("删除数据到回收站失败!栏目下的新闻不能删除", "class_list.aspx");
                }
            }
            pd.SaveUserAdminLogs(1, 1, UserNum, "删除栏目", "删除栏目到回收站.ClassID:" + Request.Form["Checkbox1"] + "");
            PageRight("删除数据到回收站成功,请返回继续操作!", "class_list.aspx");
        }
    }

    //复位操作
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //权限管理
        this.Authority_Code = "C024";
        this.CheckAdminAuthority();

        string str_ClassID = Request.Form["Checkbox1"];
        if (str_ClassID != null && str_ClassID != "")
            str_ClassID = "'" + str_ClassID.Replace(",", "','") + "'";
        else
            str_ClassID = null;
        rd.ClassReset(str_ClassID);
        PageRight("操作成功,此操作对锁定栏目无效!", "Class_list.aspx");
    }

    //一级排序操作
    protected void FirsSort_Click(object sender, EventArgs e)
    {
        Response.Redirect("SortPage.aspx?Acton=First");
    }

    protected void customShow_Click(object sender, EventArgs e)
    {
        StartLoadCustom(1);
    }

    //protected bool checkHasSub(string columnID)
    //{
    //    string connectionString = ConfigurationManager.ConnectionStrings["foosun"].ConnectionString;
    //    SqlConnection con = new SqlConnection(connectionString);
    //    SqlCommand command = new SqlCommand("select * from dbo.fs_news_Class where ParentID = " + columnID, con);
    //    SqlDataAdapter adapter = new SqlDataAdapter(command);
    //    DataSet ds = new DataSet();
    //    adapter.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //        return true;
    //    else
    //        return false;
    //}

    //数据初始化
    protected void StartLoadCustom(int PageIndex)
    {
        int i, j;
        string _SiteID = Request.QueryString["SiteID"];

        Foosun.CMS.AdminGroup ac = new Foosun.CMS.AdminGroup();
        if (_SiteID != null && _SiteID != string.Empty)
        {
            if (SiteID == "0")
            {
                newsClassTable = ac.getClassList("*", "news_Class", string.Format("Where  isRecyle<>1 and SiteID='{0}'", 0));
            }
            else
            {
                newsClassTable = ac.getClassList("*", "news_Class", string.Format("Where  isRecyle<>1 and SiteID='{0}'", SiteID.Replace("'", "''")));
            }
        }
        else
        {
            newsClassTable = ac.getClassList("*", "news_Class", string.Format("Where  isRecyle<>1 and SiteID='{0}'", 0));
        }

        if (newsClassTable != null)
        {
            newsClassTable.Columns.Add("st", typeof(string));
            newsClassTable.Columns.Add("pop", typeof(string));
            newsClassTable.Columns.Add("Colum", typeof(string));
            newsClassTable.Columns.Add("ClassCNames", typeof(string));
            for (int k = 0; k < newsClassTable.Rows.Count; k++)
            {
                this.ClassID = newsClassTable.Rows[k]["classid"].ToString();

                string strchar = "";
                //取出子类
                if (newsClassTable.Rows[k]["isPage"].ToString() == "1")
                {
                    newsClassTable.Rows[k]["ClassCNames"] = "<a href=\"news_Page.aspx?ClassID=" + newsClassTable.Rows[k]["ClassID"] + "&Action=Edit\" class=\"list_link\" title=\"点击修改单页面\">" + newsClassTable.Rows[k]["ClassCName"] + "</a>";
                }
                else
                {
                    //时间：2008-07-22 title显示模板 修改者：吴静岚
                    newsClassTable.Rows[k]["ClassCNames"] = "<a href=\"Class_Add.aspx?Cname=" + Server.UrlEncode(Convert.ToString(newsClassTable.Rows[k]["ClassCName"])) + "&Acation=Add," + newsClassTable.Rows[k]["ClassID"] + "&SiteID=" + Request.QueryString["SiteID"] + "\" class=\"list_link\" title=\"点击修改栏目&#13;栏目模板：" + newsClassTable.Rows[k]["ClassTemplet"].ToString() + "&#13;内容模板：" + newsClassTable.Rows[k]["ReadNewsTemplet"].ToString() + "\">" + newsClassTable.Rows[k]["ClassCName"] + "[" + newsClassTable.Rows[k]["ClassEname"] + "]</a>";
                    //--wjl>
                }
                if (newsClassTable.Rows[k]["IsURL"].ToString() == "1")
                {
                    newsClassTable.Rows[k]["st"] = "<font color=blue>外部</font>&nbsp;&nbsp;";
                }
                else
                {
                    newsClassTable.Rows[k]["st"] = "<font color=red>系统</font>&nbsp;&nbsp;";
                }
                if (newsClassTable.Rows[k]["isPage"].ToString() == "1")
                {
                    newsClassTable.Rows[k]["st"] += "<font color=\"#FF9900\">单页</font>&nbsp;&nbsp;";
                }
                else
                {
                    newsClassTable.Rows[k]["st"] += "普通&nbsp;&nbsp;";
                }

                if (newsClassTable.Rows[k]["IsLock"].ToString() == "1")
                {
                    newsClassTable.Rows[k]["st"] += "<a href=\"?Stat=Change&id=" + newsClassTable.Rows[k]["ClassID"] + "\" title=\"点击正常\" class=\"list_link\">锁定</a> ";
                }
                else
                {
                    newsClassTable.Rows[k]["st"] += "<a href=\"?Stat=Change&id=" + newsClassTable.Rows[k]["ClassID"] + "\" title=\"点击锁定\" class=\"list_link\"><font color=\"green\">正常</font></a> ";
                }

                if (newsClassTable.Rows[k]["Domain"].ToString().Length > 5)
                {
                    newsClassTable.Rows[k]["st"] += "<font color=\"blue\">域</font>&nbsp;&nbsp;";
                }
                else
                {
                    newsClassTable.Rows[k]["st"] += "<font color=\"#999999\">域</font>&nbsp;&nbsp;";
                }
                if (newsClassTable.Rows[k]["NaviShowtf"].ToString() == "1")
                {
                    newsClassTable.Rows[k]["st"] += "<font color=\"red\">显示</font>&nbsp;&nbsp;";
                }
                else
                {
                    newsClassTable.Rows[k]["st"] += "隐藏&nbsp;&nbsp;";
                }

                string _TempStr = "";
                if (newsClassTable.Rows[k]["IsURL"].ToString() == "0")
                {
                    if (newsClassTable.Rows[k]["isPage"].ToString() == "0")
                    {
                        _TempStr = "<a title=\"添加新闻\" href=\"News_add.aspx?ClassID=" + newsClassTable.Rows[k]["ClassID"].ToString() + "&EditAction=add\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addnews.gif\" border=\"0\" title=\"添加新闻\" /></a><a href=\"Class_add.aspx?Cname=" + Server.UrlEncode(Convert.ToString(newsClassTable.Rows[k]["ClassCName"])) + "&Number=" + newsClassTable.Rows[k]["ClassID"] + "&SiteID=" + Request.QueryString["SiteID"] + "\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addclass.gif\" border=\"0\" title=\"添加子类\" /></a><a href=\"news_Page.aspx?Number=" + newsClassTable.Rows[k]["ClassID"] + "\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addpage.gif\" border=\"0\" title=\"添加单页面\" /></a>";
                    }
                }
                if (newsClassTable.Rows[k]["isPage"].ToString() == "1")
                {
                    newsClassTable.Rows[k]["pop"] = "<input name=\"Checkbox1\" type=\"checkbox\" value=" + newsClassTable.Rows[k]["ClassID"] + " />&nbsp;&nbsp;<a href=\"news_Page.aspx?ClassID=" + newsClassTable.Rows[k]["ClassID"] + "&Action=Edit\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/edit.gif\" border=\"0\" title=\"修改\" /></a><a href=\"news_review.aspx?ID=" + newsClassTable.Rows[k]["ClassID"] + "&type=class\" class=\"list_link\" target=\"_blank\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/review.gif\" border=\"0\" title=\"浏览\" /></a>" + _TempStr + "";
                }
                else
                {
                    newsClassTable.Rows[k]["pop"] = "<input name=\"Checkbox1\" type=\"checkbox\" value=" + newsClassTable.Rows[k]["ClassID"] + " />&nbsp;&nbsp;<a href=\"Class_Add.aspx?Cname=" + Server.UrlEncode(Convert.ToString(newsClassTable.Rows[k]["ClassCName"])) + "&Acation=Add," + newsClassTable.Rows[k]["ClassID"] + "&SiteID=" + Request.QueryString["SiteID"] + "\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/edit.gif\" border=\"0\" title=\"修改\" /></a><a href=\"news_review.aspx?ID=" + newsClassTable.Rows[k]["ClassID"] + "&type=class\" class=\"list_link\" target=\"_blank\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/review.gif\" border=\"0\" title=\"浏览\" /></a>" + _TempStr + "";
                }

                if (!this.CheckAuthority())
                {
                    newsClassTable.Rows[k]["ClassCNames"] = newsClassTable.Rows[k]["ClassCName"];
                    //strchar += "<tr class=\"TR_BG_list\"  onmouseover=\"overColor(this)\" onmouseout=\"outColor(this)\">";
                    //strchar += "<td  align=\"center\" valign=\"middle\" height=20>" + dt.Rows[k]["id"] + "</td>";
                    //strchar += "<td  align=\"left\" valign=\"middle\" colspan=\"4\"><img src=\"../../sysImages/folder/yess.gif\" border=\"0\">没有权限操作此栏目</span></td>";
                    //strchar += "</tr>";
                    strchar += "<tr class=\"TR_BG_list\"  onmouseover=\"overColor(this)\" onmouseout=\"outColor(this)\">";
                    strchar += "<td  align=\"center\" valign=\"middle\" height=20>" + newsClassTable.Rows[k]["id"] + "</td>";
                    strchar += "<td  align=\"left\" valign=\"middle\" ><img src=\"../../sysImages/folder/yess.gif\" border=\"0\">&nbsp;" + newsClassTable.Rows[k]["ClassCNames"] + "<span style=\"font-size:10px;color:red\" title=\"新闻数\">(" + rd.getClassNewsCount(newsClassTable.Rows[k]["ClassID"].ToString()) + ")</span></td>";
                    strchar += "<td  align=\"center\" valign=\"middle\" ><strong>" + newsClassTable.Rows[k]["OrderID"] + "</strong></td>";
                    strchar += "<td  align=\"center\" valign=\"middle\" >" + newsClassTable.Rows[k]["st"] + "</td>";
                    strchar += "<td valign=\"middle\" >" + "无权限" + "</td>";
                    strchar += "</tr>";
                }
                else
                {
                    strchar += "<tr class=\"TR_BG_list\"  onmouseover=\"overColor(this)\" onmouseout=\"outColor(this)\">";
                    strchar += "<td  align=\"center\" valign=\"middle\" height=20>" + newsClassTable.Rows[k]["id"] + "</td>";
                    strchar += "<td  align=\"left\" valign=\"middle\" ><img src=\"../../sysImages/folder/yess.gif\" border=\"0\">&nbsp;" + newsClassTable.Rows[k]["ClassCNames"] + "<span style=\"font-size:10px;color:red\" title=\"新闻数\">(" + rd.getClassNewsCount(newsClassTable.Rows[k]["ClassID"].ToString()) + ")</span></td>";
                    strchar += "<td  align=\"center\" valign=\"middle\" ><a class=\"list_link\" href=\"javascript:orderAction(" + newsClassTable.Rows[k]["ClassID"] + "," + newsClassTable.Rows[k]["OrderID"] + ");\" title=\"点击排序\"><strong>" + newsClassTable.Rows[k]["OrderID"] + "</a></strong></td>";
                    strchar += "<td  align=\"center\" valign=\"middle\" >" + newsClassTable.Rows[k]["st"] + "</td>";
                    strchar += "<td valign=\"middle\" >" + newsClassTable.Rows[k]["pop"] + "</td>";
                    strchar += "</tr>";
                }
                strchar += getchildClassList(newsClassTable.Rows[k]["ClassID"].ToString(), "┝");
                newsClassTable.Rows[k]["Colum"] = strchar;
            }
        }
        DataView dv = new DataView(newsClassTable, "ParentID='0'", "OrderID Desc,id desc", DataViewRowState.CurrentRows);
        DataList1.DataSource = dv;
        DataList1.DataBind();
        newsClassTable.Clear();
        newsClassTable.Dispose();
        newsClassTable = null;
    }

    protected void treeShow_Click(object sender, EventArgs e)
    {
        this.HiddenField_ParentID.Value = "";
        Session["__ParentIDList"] = null;
        this.StartLoad(1);
    }

}
