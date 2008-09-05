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
using Foosun.Model;


namespace Foosun.Web.manage.news
{
    public partial class Class_list_ajax : Foosun.Web.UI.ManagePage
    {
        public Class_list_ajax()
        {
            Authority_Code = "C019";
        }
        ContentManage rd = new ContentManage();
        rootPublic pd = new rootPublic();
        Foosun.CMS.UserMisc ud = new Foosun.CMS.UserMisc();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.CacheControl = "no-cache";
                Response.Expires = 0;
                string ParentId = Request.QueryString["ParentId"];
                string ShowFlag = Request.QueryString["ShowFlag"];

                if (ParentId == "" || ParentId == null)
                {
                    return;
                }
                setParentIDList(ParentId, bool.Parse(ShowFlag));
                if (string.IsNullOrEmpty(ShowFlag) || bool.Parse(ShowFlag) == true)
                {
                    string text = getchildClassList(ParentId, "");
                    text += "|||" + ParentId;
                    Response.Write(text);
                }
            }
        }

        /// <summary>
        /// 设置节点ID
        /// </summary>
        /// <param name="parentID">节点的ID</param>
        /// <param name="ShowFlag">是否展开</param>
        private void setParentIDList(string parentID, bool ShowFlag)
        {
            ArrayList arr = null;
            //如果为空,则创建一个内容
            if (Session["__ParentIDList"] == null)
            {
                arr = new ArrayList();
                Session["__ParentIDList"] = arr;
            }
            //取出内容
            arr = (ArrayList)Session["__ParentIDList"];
            //展开节点
            if (ShowFlag)
            {
                //判断是否有此节点ID
                bool isHave = true;
                for (int i = 0; i < arr.Count; i++)
                {
                    if (arr[i].ToString().Equals(parentID))
                        isHave = false;
                }
                if (isHave)
                    arr.Add(parentID);
            }
            else//移出节点
            {
                ArrayList aList = new ArrayList();
                for (int i = 0; i < arr.Count; i++)
                {
                    if (!arr[i].ToString().Equals(parentID))
                    {
                        aList.Add(arr[i]);
                    }
                }
                arr = aList;
            }
            Session["__ParentIDList"] = arr;
        }

        string getchildClassList(string Classid, string sign)
        {
            string strchar = "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"4\" cellspacing=\"1\" class=\"table\">";
            DataTable dt = rd.getChildList(Classid);
            sign += " ┉ ";
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("st", typeof(string));
                    dt.Columns.Add("pop", typeof(string));
                    dt.Columns.Add("Colum", typeof(string));
                    dt.Columns.Add("ClassCNames", typeof(string));
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        this.ClassID = dt.Rows[j]["classid"].ToString();
                        if (dt.Rows[j]["isPage"].ToString() == "1")
                        {
                            dt.Rows[j]["ClassCNames"] = "<a href=\"news_Page.aspx?ClassID=" + dt.Rows[j]["ClassID"] + "&Action=Edit\" class=\"list_link\" title=\"点击修改单页面\">" + dt.Rows[j]["ClassCName"] + "</a>";
                        }
                        else
                        {
                            string classTemple = dt.Rows[j]["ClassTemplet"] + "";
                            string ReadNewsTemplet = dt.Rows[j]["ReadNewsTemplet"] + "";
                            classTemple = string.IsNullOrEmpty(classTemple) == true ? "无" : classTemple;
                            ReadNewsTemplet = string.IsNullOrEmpty(ReadNewsTemplet) == true ? "无" : ReadNewsTemplet;
                            //显示模板
                            dt.Rows[j]["ClassCNames"] = "<a onclick=\"var cname=escape('" + dt.Rows[j]["ClassCName"] + "');window.location.href='Class_Add.aspx?Cname=cname&Acation=Add," + dt.Rows[j]["ClassID"] + "&SiteID=" + Request.QueryString["SiteID"] + "'\" href='#' class=\"list_link\" title=\"点击修改栏目&#13;栏目模板：" + classTemple + "&#13;内容模板：" + ReadNewsTemplet + "\">" + dt.Rows[j]["ClassCName"] + "[" + dt.Rows[j]["ClassEname"] + "]</a>";
                        }
                        if (dt.Rows[j]["IsURL"].ToString() == "1")
                        {
                            dt.Rows[j]["st"] = "<font color=\"blue\">外部</font>&nbsp;&nbsp;";
                        }
                        else
                        {
                            dt.Rows[j]["st"] = "<font color=\"red\">系统</font>&nbsp;&nbsp;";
                        }
                        if (dt.Rows[j]["isPage"].ToString() == "1")
                        {
                            dt.Rows[j]["st"] += "<font color=\"#FF9900\">单页</font>&nbsp;&nbsp;";
                        }
                        else
                        {
                            dt.Rows[j]["st"] += "普通&nbsp;&nbsp;";
                        }
                        if (dt.Rows[j]["IsLock"].ToString() == "1")
                        {
                            dt.Rows[j]["st"] += "<a href=\"?Stat=Change&id=" + dt.Rows[j]["ClassID"] + "\" title=\"点击正常\" class=\"list_link\">锁定</a> ";
                        }
                        else
                        {
                            dt.Rows[j]["st"] += "<a href=\"?Stat=Change&id=" + dt.Rows[j]["ClassID"] + "\" title=\"点击锁定\" class=\"list_link\"><font color=\"green\">正常</font></a> ";
                        }
                        if (dt.Rows[j]["Domain"].ToString().Length > 5)
                        {
                            dt.Rows[j]["st"] += "<font color=\"blue\">域</font>&nbsp;&nbsp;";
                        }
                        else
                        {
                            dt.Rows[j]["st"] += "<font color=\"#999999\">域</font>&nbsp;&nbsp;";
                        }
                        if (dt.Rows[j]["NaviShowtf"].ToString() == "1")
                        {
                            dt.Rows[j]["st"] += "<font color=\"red\">显示</font>&nbsp;&nbsp;";
                        }
                        else
                        {
                            dt.Rows[j]["st"] += "隐藏&nbsp;&nbsp;";
                        }

                        string _TempStr = "";
                        if (dt.Rows[j]["IsURL"].ToString() == "0")
                        {
                            if (dt.Rows[j]["isPage"].ToString() == "0")
                            {
                                _TempStr = "<a title=\"添加新闻\" href=\"News_add.aspx?ClassID=" + dt.Rows[j]["ClassID"].ToString() + "&EditAction=add\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addnews.gif\" border=\"0\" /></a><a onclick=\"var cname='" + dt.Rows[j]["ClassCName"] + "';window.location.href='Class_add.aspx?Cname=cname&Number=" + dt.Rows[j]["ClassID"] + "&SiteID=" + Request.QueryString["SiteID"] + "'\" href=\"#\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addclass.gif\" border=\"0\" title=\"添加子类\" /></a><a href=\"news_Page.aspx?Number=" + dt.Rows[j]["ClassID"] + "\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addpage.gif\" border=\"0\" title=\"添加单页面\" /></a>";
                            }
                        }

                        //操作
                        if (dt.Rows[j]["isPage"].ToString() == "1")
                        {
                            dt.Rows[j]["pop"] = "<input name=\"Checkbox1\" type=\"checkbox\" value=" + dt.Rows[j]["ClassID"] + " />&nbsp;&nbsp;<a href=\"news_Page.aspx?ClassID=" + dt.Rows[j]["ClassID"] + "&Action=Edit\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/edit.gif\" border=\"0\" title=\"修改\"></a><a href=\"news_review.aspx?ID=" + dt.Rows[j]["ClassID"] + "&type=class\" class=\"list_link\" target=\"_blank\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/review.gif\" border=\"0\" title=\"浏览\" /></a>" + _TempStr + "";
                        }
                        else
                        {
                            dt.Rows[j]["pop"] = "<input name=\"Checkbox1\" type=\"checkbox\" value=" + dt.Rows[j]["ClassID"] + " />&nbsp;&nbsp;<a onclick=\"var cnames=escape('" + dt.Rows[j]["ClassCName"] + "');window.location.href='Class_Add.aspx?Cname=cnames&Acation=Add," + dt.Rows[j]["ClassID"] + "&SiteID=" + Request.QueryString["SiteID"] + ";'\" href=\"#\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/edit.gif\" border=\"0\" title=\"修改\"></a><a href=\"news_review.aspx?ID=" + dt.Rows[j]["ClassID"] + "&type=class\" class=\"list_link\" target=\"_blank\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysIco/review.gif\" border=\"0\" title=\"浏览\" /></a>" + _TempStr + "";
                        }

                        if (!this.CheckAuthority())
                        {
                            //strchar += "<tr class=\"TR_BG_list\"  onmouseover=\"overColor(this)\" onmouseout=\"outColor(this)\">";
                            //strchar += "<td  align=\"center\" valign=\"middle\" height=20>" + dt.Rows[j]["id"] + "</td>";
                            //strchar += "<td  align=\"left\" valign=\"middle\" colspan=\"4\"><img src=\"../../sysImages/folder/yess.gif\" border=\"0\">没有权限操作此栏目</span></td>";
                            //strchar += "</tr>";
                        }
                        else
                        {
                            strchar += "<tr class=\"TR_BG_list\"  onmouseover=\"overColor(this)\" onmouseout=\"outColor(this)\">";
                            strchar += "<td width=\"7%\" align=\"center\" valign=\"middle\" height=20>" + dt.Rows[j]["id"] + "</td>";
                            DataTable dc = rd.checkHasSub(dt.Rows[j]["ClassID"].ToString());
                            if (int.Parse(dc.Rows[0][0].ToString()) > 0)

                                strchar += "<td width=\"36%\" align=\"left\" valign=\"middle\" ><img id=\"img_parentid_" + dt.Rows[j]["ClassID"] + "\" src=\"../../sysImages/normal/b.gif\" style=\"cursor:hand\" alt=\"点击展开子栏目\"  onClick=\"javascript:SwitchImg(this,'" + dt.Rows[j]["ClassID"] + "');\" border=\"0\">" + sign + dt.Rows[j]["ClassCNames"] + "<span style=\"font-size:10px;color:red;\" title=\"新闻数\">(" + rd.getClassNewsCount(dt.Rows[j]["ClassID"].ToString()) + ")</span></td>";

                            else

                                strchar += "<td width=\"36%\" align=\"left\" valign=\"middle\" ><img src=\"../../sysImages/normal/s.gif\" style=\"cursor:pointer;\" alt=\"没有子栏目\"  border=\"0\" class=\"LableItem\" />" + sign + dt.Rows[j]["ClassCNames"] + "<span style=\"font-size:10px;color:red;\" title=\"新闻数\">(" + rd.getClassNewsCount(dt.Rows[j]["ClassID"].ToString()) + ")</span></td>";

                            strchar += "<td   width=\"5%\" align=\"center\" valign=\"middle\" ><a class=\"list_link\" href=\"javascript:orderAction(" + dt.Rows[j]["ClassID"] + "," + dt.Rows[j]["OrderID"] + ");\" title=\"点击排序\"><strong>" + dt.Rows[j]["OrderID"] + "</strong></a></td>";
                            strchar += "<td width=\"24%\" align=\"center\" valign=\"middle\" >" + dt.Rows[j]["st"] + "</td>";
                            strchar += "<td  valign=\"middle\" >" + dt.Rows[j]["pop"] + "</td>";
                            strchar += "</tr>";
                        }
                        strchar += "<tr class=\"TR_BG_list\"  ><td colspan=\"5\"><div id=\"Parent" + dt.Rows[j]["ClassID"].ToString() + "\" class=\"SubItem\" HasSub=\"True\" style=\"height:100%; display:none;\"></div></td></tr>";
                        //strchar += getchildClassList(dt.Rows[j]["ClassID"].ToString(), sign);
                        dt.Rows[j]["Colum"] = strchar;
                    }
                    dt.Clear(); dt.Dispose();
                }
            }
            strchar += "</table>";
            return strchar;
        }

        //protected bool checkHasSub(string columnID)
        //{
        //    Dao dao = new Dao();
        //    DataSet ds = dao.ExecSqlDataSet("select * from dbo.fs_news_Class where ParentID = " + columnID);
        //    if (ds.Tables[0].Rows.Count > 0)
        //        return true;
        //    else
        //        return false;
        //}
    }
}
