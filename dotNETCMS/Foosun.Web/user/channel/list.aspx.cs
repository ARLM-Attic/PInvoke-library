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

public partial class user_channel_list : Foosun.Web.UI.UserPage
{
    Channel rd = new Channel();
    public static string itemname = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!IsPostBack)
        {

            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            string ClassID = Request.QueryString["ClassID"];
            int sClassID = 0;
            if (ClassID != null && ClassID != string.Empty)
            {
                sClassID = int.Parse(ClassID.ToString());
            }
            string ConstrTF = Request.QueryString["ConstrTF"];
            string ChID = Request.QueryString["ChID"];
            if (ChID == null && ChID == string.Empty)
            {
                PageError("错误的参数", "javascript:history.back();", true);
            }
            IDataReader cr = rd.getModelinfo(int.Parse(ChID.ToString()));
            if (cr.Read())
            {
                if (cr["isConstr"].ToString() == "0")
                {
                    PageError("此频道不允许发布信息", "javascript:history.back();", true);
                }
                itemname = cr["channelItem"].ToString();
            }
            cr.Close();
            if (ConstrTF != null && ConstrTF != string.Empty)
            {
                string id = Request.QueryString["id"];
                if (id == null && id == string.Empty)
                {
                    PageError("错误的参数", "javascript:history.back();", true);
                }
                string OpStr = string.Empty;
                IDataReader dr = rd.GetTopicInfo(int.Parse(id.ToString()), int.Parse(ChID.ToString()));
                if (dr.Read())
                {
                    switch (ConstrTF.ToString())
                    {
                        case "1":
                            if (dr["islock"].ToString() == "0")
                            {
                                PageError("管理员已经审核通过,不能锁定", "javascript:history.back();", true);
                            }
                            else
                            {
                                rd.updateUserInfo(int.Parse(id.ToString()), int.Parse(ChID.ToString()), 1, Foosun.Global.Current.UserName);
                                OpStr = "锁定成功";
                            }
                            break;
                        case "0":
                            rd.updateUserInfo(int.Parse(id.ToString()), int.Parse(ChID.ToString()), 0, Foosun.Global.Current.UserName);
                            OpStr = "解锁成功";
                            break;
                        case "2":
                            if (dr["islock"].ToString() == "0")
                            {
                                PageError("管理员已经审核通过,不能删除.如果要删除,请与管理员联系", "javascript:history.back();", true);
                            }
                            else
                            {
                                rd.updateUserInfo(int.Parse(id.ToString()), int.Parse(ChID.ToString()), 2, Foosun.Global.Current.UserName);
                                OpStr = "删除成功";
                            }
                            break;
                    }
                }
                dr.Close();
                PageRight(OpStr, "list.aspx?ChID=" + ChID + "", true);
            }
            GetClassList((this.gClassID), 0, 0, sClassID);
            string keywords = Request.QueryString["keywords"];
            string gkeywords = string.Empty;
            if (keywords != null && keywords != string.Empty)
            {
                gkeywords = keywords.ToString();
            }
            if (ClassID != null && ClassID != string.Empty)
            {
                StartLoad(1, gkeywords, ClassID.ToString());
            }
            else
            {
                StartLoad(1, gkeywords, "0");
            }
        }
    }

    /// <summary>
    /// 获取栏目列表
    /// </summary>
    /// <param name="lst"></param>
    /// <param name="ParentID"></param>
    /// <param name="Layer"></param>
    /// <param name="sClassID"></param>
    protected void GetClassList(DropDownList lst, int ParentID, int Layer, int sClassID)
    {
        IDataReader dr = rd.getClassList(ParentID, int.Parse(Request.QueryString["ChID"].ToString()));
        while (dr.Read())
        {
            ListItem it = new ListItem();
            string stxt = "";
            it.Value = dr["ID"].ToString();
            if (Layer > 0)
                stxt = "┝";
            for (int i = 1; i < Layer; i++)
            {
                stxt += " ┉ ";
            }
            it.Text = stxt + dr["ClassCName"].ToString();
            if (sClassID == int.Parse(dr["id"].ToString()))
            {
                it.Selected = true;
            }
            lst.Items.Add(it);
            GetClassList(lst, int.Parse(dr["ID"].ToString()), (Layer + 1), sClassID);
        }
        dr.Close();
    }

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        string keywords = Request.QueryString["keywords"];
        string ClassID = Request.QueryString["ClassID"];
        if (ClassID == string.Empty && ClassID == null)
        {
            ClassID = "0";
        }
        if (keywords == null && keywords == string.Empty)
        {
            StartLoad(PageIndex, "", ClassID.ToString());
        }
        else
        {
            StartLoad(PageIndex, keywords.ToString(), ClassID.ToString());
        }
    }

    protected void StartLoad(int PageIndex,string Keywords,string ClassID)
    {
        int i, j;
        string Author = Foosun.Global.Current.UserName;
        DataTable dt = rd.GetUserChannelPage(Author,Keywords, ClassID, int.Parse(Request.QueryString["ChID"].ToString()), PageIndex, 20, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null&&dt.Rows.Count>0)
        {
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("titles", typeof(string));
                dt.Columns.Add("isLocks", typeof(string));
                dt.Columns.Add("ConstrTFs", typeof(string));
                dt.Columns.Add("op", typeof(string));
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    dt.Rows[k]["titles"] = "<a href=\"Content.aspx?ChID=" + dt.Rows[k]["ChID"].ToString() + "&id=" + dt.Rows[k]["id"].ToString() + "\" class=\"list_link\">" + dt.Rows[k]["title"].ToString() + "</a>";
                    if (dt.Rows[k]["islock"].ToString() == "1")
                    {
                        dt.Rows[k]["isLocks"] = "<span class=\"reshow\">未审核</span>";
                    }
                    else
                    {
                        dt.Rows[k]["isLocks"] = "<span title=\"通过审核后不能锁定\">已审核</span>";
                    }
                    if (dt.Rows[k]["islock"].ToString() == "0")
                    {
                        dt.Rows[k]["op"] = "<a href=\"content.aspx?ChID=" + dt.Rows[k]["ChID"].ToString() + "&id=" + dt.Rows[k]["id"].ToString() + "\" class=\"list_link\">修改</a>&nbsp;┊&nbsp;<a href=\"list.aspx?ChID=" + dt.Rows[k]["ChID"].ToString() + "&id=" + dt.Rows[k]["id"].ToString() + "&ConstrTF=2\"  OnClick=\"{if(confirm('管理员已经审核,不能删除')){return false;}return false;}\" class=\"list_link\">删除</a>";
                    }
                    else
                    {
                        dt.Rows[k]["op"] = "<a href=\"content.aspx?ChID=" + dt.Rows[k]["ChID"].ToString() + "&id=" + dt.Rows[k]["id"].ToString() + "\" class=\"list_link\">修改</a>&nbsp;┊&nbsp;<a href=\"list.aspx?ChID=" + dt.Rows[k]["ChID"].ToString() + "&id=" + dt.Rows[k]["id"].ToString() + "&ConstrTF=2\"  OnClick=\"{if(confirm('确定要删除吗?')){return true;}return false;}\" class=\"list_link\">删除</a>";
                    }
                    if (dt.Rows[k]["ConstrTF"].ToString() == "0")
                    {
                        if (dt.Rows[k]["islock"].ToString() == "0")
                        {
                            dt.Rows[k]["ConstrTFs"] = "<a href=\"list.aspx?ChID=" + dt.Rows[k]["ChID"].ToString() + "&id=" + dt.Rows[k]["id"].ToString() + "&ConstrTF=1\"  OnClick=\"{if(confirm('管理员已经审核通过,不能锁定')){return false;}return false;}\" class=\"list_link\">正常</a>";
                        }
                        else
                        {
                            dt.Rows[k]["ConstrTFs"] = "<a href=\"list.aspx?ChID=" + dt.Rows[k]["ChID"].ToString() + "&id=" + dt.Rows[k]["id"].ToString() + "&ConstrTF=1\"  OnClick=\"{if(confirm('确定要锁定吗？')){return true;}return false;}\" class=\"list_link\">正常</a>";
                        }
                    }
                    else
                    {
                        dt.Rows[k]["ConstrTFs"] = "<a href=\"list.aspx?ChID=" + dt.Rows[k]["ChID"].ToString() + "&id=" + dt.Rows[k]["id"].ToString() + "&ConstrTF=0\"  OnClick=\"{if(confirm('确定要解锁吗？')){return true;}return false;}\" class=\"reshow\">锁定</a>";
                    }

                }
            }
            DataList1.DataSource = dt;                         
            DataList1.DataBind();
            DataList1.Dispose();
        }
    }

    protected void del_info(object sender, EventArgs e)
    {
        string Str = Request.Form["infoID"];
        string ChID = Request.QueryString["ChID"];
        if (Str == null || Str == String.Empty)
        {
            PageError("请至少选择一项!", "");
        }
        else
        {
            string[] gIDARR = Str.Split(',');
            Str = null;
            for (int i = 0; i < gIDARR.Length; i++)
            {
                IDataReader dr = rd.GetTopicInfo(int.Parse(gIDARR[i].ToString()), int.Parse(ChID.ToString()));
                if (dr.Read())
                {
                    if (dr["islock"].ToString() == "1")
                    {
                        rd.updateUserInfo(int.Parse(gIDARR[i].ToString()), int.Parse(ChID.ToString()), 2, Foosun.Global.Current.UserName);
                    }
                }
                dr.Close();
            }
            PageRight("删除成功。", "javascript:history.back();", true);
        }
    }
}
