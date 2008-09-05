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
public partial class user_friend_friend : Foosun.Web.UI.UserPage
{
    public DataTable dt_class;
    Info fl = new Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!IsPostBack)                                               //判断页面是否重载
        {
            StartUser();                                                   //初始化
            StartPram();                                                   //初始化
            FriendLinkManage(1);//初始分页数据
        }
        string _type = Request.QueryString["type"];
        if (_type != null && _type != string.Empty)
        { 
            if(_type.ToString()=="del")
            {
                fl.delf(Request.QueryString["id"].ToString());
                PageRight("删除成功", "friend.aspx");
            }
        }
        Selectclass.Items.Clear();
        getClassInfo();
        #region 判断链接申请是否开放
        DataTable dt_pram = fl.IsOpen();
        string isopen = dt_pram.Rows[0]["IsOpen"].ToString();
        if (isopen == "0")
        {
            PageError("抱歉。管理员没有开放此功能，暂时不能使用！", "");
        }
        #endregion
    }
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        FriendLinkManage(PageIndex);//管理页面分页查询
    }
    protected void FriendLinkManage(int PageIndex)//显示链接管理页面
    {
        int i, j; 
        DataTable dt = Foosun.CMS.Pagination.GetPage("user_friend_friend_aspx", PageIndex, PAGESIZE, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        try
        {
            if (dt != null)//判断如果dt里面没有内容，将不会显示
            {
                if (dt.Rows.Count > 0)
                {
                    //添加列
                    dt.Columns.Add("class", typeof(String));//类别
                    dt.Columns.Add("type", typeof(String));//类型
                    dt.Columns.Add("lock", typeof(String));//锁定

                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        int id = int.Parse(dt.Rows[k]["id"].ToString());
                        string LinkUrl = dt.Rows[k]["Url"].ToString();
                        string siteName = dt.Rows[k]["Name"].ToString();
                        string ClassID = dt.Rows[k]["ClassID"].ToString();
                        int Type = int.Parse(dt.Rows[k]["Type"].ToString());
                        int llock = int.Parse(dt.Rows[k]["Lock"].ToString());
                        dt.Rows[k]["Name"] = "<a href='" + LinkUrl + "' class=\"list_link\" title='" + siteName + "' target=\"_bank\">" + dt.Rows[k]["Name"].ToString() + "</a>";
                        #region 取类别的名称
                        string className = "";
                        try
                        {
                            DataTable dtcs = fl.ClassName_Click(ClassID);
                            className = dtcs.Rows[0]["ClassCName"].ToString();
                            dt.Rows[k]["class"] = className;
                        }
                        catch { }
                        #endregion

                        #region 取类型值 1,文字，0,图片
                        switch (Type)
                        {
                            case 1:
                                dt.Rows[k]["type"] = "文字";
                                break;
                            case 0:
                                dt.Rows[k]["type"] = "图片";
                                break;
                            default:
                                dt.Rows[k]["type"] = "文字";
                                break;
                        }
                        #endregion

                        #region 状态 1,锁定 0,正常
                        switch (llock)
                        {
                            case 0:
                                dt.Rows[k]["lock"] = "<img src=\"../../sysImages/folder/yes.gif\" border=\"0\">&nbsp;┊&nbsp;<span style=\"color:#999999\" title=\"审核通过了的不能删除\">删除</a>";
                                break;
                            case 1:
                                dt.Rows[k]["lock"] = "<img src=\"../../sysImages/folder/no.gif\" border=\"0\">&nbsp;┊&nbsp;<a href=\"friend.aspx?type=del&id=" + dt.Rows[0]["id"].ToString() + "\" onclick=\"{if(confirm('确定要删除吗？')){return true;}return false;}\" class=\"list_link\">删除</a>";
                                break;
                            default:
                                dt.Rows[k]["lock"] = "<img src=\"../../sysImages/folder/yes.gif\" border=\"0\">&nbsp;┊&nbsp;<span style=\"color:#999999\" title=\"审核通过了的不能删除\">删除</a>";
                                break;
                        }
                        #endregion
                    }
                }
                else
                {
                    NoContent.InnerHtml = Show_NoContent();
                    this.PageNavigator1.Visible = false;
                }
            }
            else
            {
                NoContent.InnerHtml = Show_NoContent();
                this.PageNavigator1.Visible = false;
            }
        }
        catch { }
        DataList1.DataSource = dt;
        DataList1.DataBind();
    }
    #region 取分类信息
    protected void getClassInfo()
    {
        dt_class = fl.ClassInfo();
        if (dt_class != null)
        {
            ClassRender("0", 0);
        }
        dt_class.Clear();
        dt_class.Dispose();
    }
    #endregion

    /// <summary>
    /// 递归
    /// </summary>
    /// <param name="PID"></param>
    /// <param name="Layer"></param> 
    ///code by chenzhaohui 
    #region
    private void ClassRender(string PID, int Layer)
    {
        DataRow[] row = dt_class.Select("ParentID='" + PID + "'");
        if (row.Length < 1)
            return;
        else
        {
            foreach (DataRow r in row)
            {
                ListItem it = new ListItem();
                it.Value = r["ClassID"].ToString();
                string stxt = "├";
                for (int i = 0; i < Layer; i++)
                {
                    stxt += "─";
                }
                it.Text = stxt + r["ClassCName"].ToString();
                this.Selectclass.Items.Add(it);
                ClassRender(r["ClassID"].ToString(), Layer + 1);
            }
        }
    }
    #endregion

    /// <summary>
    /// 初始信息
    /// </summary>
    /// code by chenzhaohui

    protected void StartUser()
    {
        DataTable dt = fl.StartUserC();
        if (dt.Rows.Count > 0)
        {
            this.Author.Text = dt.Rows[0]["UserNum"].ToString().Trim();
            this.Mail.Text = dt.Rows[0]["Email"].ToString().Trim();
        }
        else
        {
            PageError("意外错误：未知错误<br>", "");
        }
    }

    /// <summary>
    /// 参数表取值
    /// </summary>
    ///code by chenzhaohui 

    protected void StartPram()
    {
        #region 参数表取值
        DataTable dt_pram = fl.PramValue();
        if (dt_pram.Rows.Count > 0)
        {
            string Str_Content = Foosun.Common.Input.ToTxt(dt_pram.Rows[0]["Content"].ToString());
            Know.InnerHtml = Str_Content;
        }
        else
        {
            PageError("意外错误：未知错误<br>", "");
        }
        #endregion
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    #region save
    protected void addFriend_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            //取得设置添加中的表单信息
            string Str_Class = Request.Form["Selectclass"];//选择类别
            string Str_Name = this.Name.Text.Trim();//站点名称
            string Str_Type = this.LinkType.Text.Trim();//链接类型
            string Str_Url = this.Url.Text.Trim();//链接地址
            string Str_Content = this.ContentFri.Value.Trim();//站点说明
            string Str_PicUrl = this.PicUrll.Text;//图片地址
            string Str_Author = this.Author.Text.Trim();//申请人作者
            string Str_Mail = this.Mail.Text.Trim();//邮件
            string Str_ContentFor = this.ContentFor.Value.Trim();//申请理由
            if (isread.Checked==false)
            {
                PageError("请先同意申请协议", "friend.aspx");
            }
            #region 类型
            string tip = null;
            switch (Str_Type)
            {
                case "1":
                    tip = "文字链接申请";
                    break;
                case "0":
                    tip = "图片链接申请";
                    break;
            }
            #endregion
            //检查是否有已经存在的链接名称
            if (fl.ISExitNamee(Str_Name)!=0)
            {
                PageError("对不起，该链接已经存在", "friend.aspx");
            }

            #region //向数据库中写入添加的链接信息
            int sav = fl.SaveLink(Str_Class, Str_Name, Str_Type, Str_Url, Str_Content, Str_PicUrl, Str_Author, Str_Mail, Str_ContentFor);
            #endregion
            //载入数据-刷新页面
            if (sav!=0)
            {
                PageRight("" + tip + "成功", "friend.aspx");
            }
            else
            {
                PageError("意外错误：未知错误<br />", "shortcut_list.aspx");
            }

        }
    }
    #endregion

    string Show_NoContent()
    {
            string nos = "";
            nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
            nos = nos + "<tr class='TR_BG_list'>";
            nos = nos + "<td class='navi_link'>当前没有记录!</td>";
            nos = nos + "</tr>";
            nos = nos + "</table>";
            return nos;
    }
}
