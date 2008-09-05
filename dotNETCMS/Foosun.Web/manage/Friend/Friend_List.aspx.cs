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


public partial class manage_Friend_Friend_List : Foosun.Web.UI.ManagePage
{
    public manage_Friend_Friend_List()
    {
        Authority_Code = "S014";
    }
    FrindLink fl = new FrindLink();
    public DataTable dt_class;
    rootPublic log = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        this.PageNavigator2.OnPageChange += new PageChangeHandler(PageNavigator2_PageChange);
        Response.CacheControl = "no-cache"; //清除缓存
        if (!IsPostBack) //判断页面是否重载
        {
            //判断用户是否登录
            copyright.InnerHtml = CopyRight; //获取版权信息
            ParamStartLoad(); //载入初始参数设置页面数据
            FriendClassManage(1); //初始分页数据
            FriendLinkManage(1);//初始分页数据
            StartUser();

            #region 参数
            string type = Request.QueryString["type"];
            if (type != null && type != "")
            {
                switch (type.ToString())
                {
                    case "edit_class"://修改
                        this.Authority_Code = "S015";
                        this.CheckAdminAuthority();
                        EditClass_Start();
                        break;
                    case "edit_link":
                        this.Authority_Code = "S015";
                        this.CheckAdminAuthority();
                        Edit_Link_Start();
                        break;
                    case "delone_class"://删除单个
                        this.Authority_Code = "S015";
                        this.CheckAdminAuthority();
                        DelOne_Class();
                        break;
                    case "delone_link":
                        this.Authority_Code = "S015";
                        this.CheckAdminAuthority();
                        DelOne_Link();
                        break;
                    case "suo":
                        this.Authority_Code = "S015";
                        this.CheckAdminAuthority();
                        Suo();
                        break;
                    case "unsuo":
                        this.Authority_Code = "S015";
                        this.CheckAdminAuthority();
                        Unsuo();
                        break;
                }
            }
            #endregion
        }
        #region
        SelectClass.Items.Clear();
        getClassInfo();
        #endregion
        GetParentValue();  //父类编号
        ShowNavi.InnerHtml = ShowNaviFunc(); //显示功能导航菜单
    }

    /// <summary>
    /// 取栏目信息
    /// </summary>

    #region
    protected void getClassInfo()
    {
        dt_class = fl.GetClass();
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
                string stxt = "┝";
                for (int i = 0; i < Layer; i++)
                {
                    stxt += "┉";
                }
                it.Text = stxt + r["ClassCName"].ToString();
                this.SelectClass.Items.Add(it);
                ClassRender(r["ClassID"].ToString(), Layer + 1);
            }
        }
    }
    #endregion

    #region 分页
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        FriendClassManage(PageIndex);//管理页面分页查询
    }
    protected void PageNavigator2_PageChange(object sender, int PageIndex)
    {
        FriendLinkManage(PageIndex);//管理页面分页查询
    }
    #endregion

    /// <summary>
    /// 显示功能菜单
    /// </summary>
    /// <returns></returns>
    ///code by chenzhaohui 

    #region 显示功能菜单
    string ShowNaviFunc()
    {
        string strlist = "";
        string strlist1 = "";
        strlist += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"Navitable\">\n";
        strlist += "<tr class=\"list_link\"><td height=\"18\" style=\"width: 45%\" colspan=\"2\" style=\"padding-left:14px;\">\n";
        string _tmplet = "";
        if (SiteID == "0")
        {
            _tmplet = "<a href=\"Friend_List.aspx?type=pram\" class=\"list_link\"><a href=\"?type=pram\" class=\"list_link\">参数设置</a>&nbsp;┊&nbsp;";
        }
        strlist += _tmplet + "<a href=\"?type=class\" class=\"menulist\">分类管理</a>\n";
        strlist1 += "┆&nbsp;<a href=\"?type=link\" class=\"menulist\">连接管理</a>\n";
        strlist += strlist1;
        strlist += "</td></tr></table> \n";
        return strlist;

    }
    #endregion

    /// <summary>
    /// 载入初始参数设置数据
    /// </summary>
    /// code by chenzhaohui

    #region 载入初始参数设置数据
    void ParamStartLoad()
    {
        DataTable dt = fl.ParamStart();
        if (dt.Rows.Count > 0)
        {
            #region 参数设置
            if (dt.Rows[0]["IsOpen"].ToString() == "1")
            {
                IsOpen.Checked = true;
            }
            else
            {
                IsOpen.Checked = false;
            }
            if (dt.Rows[0]["IsRegister"].ToString() == "1")
            {
                IsRegister.Checked = true;
            }
            else
            {
                IsRegister.Checked = false;
            }
            if (Foosun.Config.UIConfig.Linktagert.Equals("_blank"))
            {
                isBlank.Checked = true;
            }
            else
            {
                isBlank.Checked = false;
            }
            if (Foosun.Config.UIConfig.Linktagertimg.Equals("_blank"))
            {
                isImgBlank.Checked = true;
            }
            else
            {
                isImgBlank.Checked = false;
            }
            ArrSize.Text = dt.Rows[0]["ArrSize"].ToString();
            Content.Value = Foosun.Common.Input.ToTxt(dt.Rows[0]["Content"].ToString());
            if (dt.Rows[0]["isLock"].ToString() == "1")
            {
                isLock.Checked = true;
            }
            else
            {
                isLock.Checked = false;
            }
            #endregion
        }
    }
    #endregion

    /// <summary>
    /// 保存参数设置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    #region 保存参数设置
    protected void SavePram_ServerClick(object sender, EventArgs e)
    {
        if (SiteID != "0")
        {
            PageError("你没有此项的操作权限。", "");
        }
        if (Page.IsValid)//判断页面是否通过验证
        {
            #region 取得投票参数设置添加中的表单信息
            int open = 0, IsReg = 0, isLok = 0;
            if (IsOpen.Checked == true)
            {
                open = 1;
            }
            if (IsRegister.Checked == true)
            {
                IsReg = 1;
            }
            if (isLock.Checked == true)
            {
                isLok = 1;
            }
            if (isBlank.Checked == true)
            {
                Foosun.Config.UIConfig.Linktagert = "_blank";
            }
            else
            {
                Foosun.Config.UIConfig.Linktagert = "";
            }
            if (isImgBlank.Checked == true)
            {
                Foosun.Config.UIConfig.Linktagertimg = "_blank";
            }
            else
            {
                Foosun.Config.UIConfig.Linktagertimg = "";
            }
            string Str_ArrSize = Foosun.Common.Input.Filter(this.ArrSize.Text.Trim());//图片尺寸
            string Str_Content = Foosun.Common.Input.Filter(this.Content.Value.Trim());//注册须知;
            #endregion

            #region 向数据库中写入添加的参数设置信息
            int up_pram = fl.Update_Pram(open, IsReg, isLok, Str_ArrSize, Str_Content);
            #endregion

            #region 载入数据-刷新页面
            if (up_pram != 0)
            {
                log.SaveUserAdminLogs(1, 1, UserNum, "友情连接参数设置", "友情连接参数设置成功！");
                PageRight("友情连接参数设置成功", "Friend_List.aspx?type=pram");
            }
            else
            {
                PageError("意外错误：未知错误<br />", "shortcut_list.aspx");
            }
            #endregion
        }
    }
    #endregion

    /// <summary>
    /// 分类管理页
    /// </summary>
    /// <param name="PageIndex"></param>
    /// code by chenzhaohui

    #region 分类管理页
    protected void FriendClassManage(int PageIndex)//显示类别管理页面
    {
        int i, j;
        DataTable dt = Foosun.CMS.Pagination.GetPage("manage_Friend_Friend_List_1_aspx", PageIndex, PAGESIZE, out i, out j, null);
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
                    dt.Columns.Add("oPerate", typeof(String));//操作
                    dt.Columns.Add("Colum", typeof(String));

                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        String strchar = null;
                        string id = dt.Rows[k]["ClassID"].ToString();
                        dt.Rows[k]["ClassCName"] = "<a href='Friend_List.aspx?type=edit_class&id=" + id + "' class='list_link' title='点击查看详情或修改'>" + dt.Rows[k]["ClassCName"].ToString() + "</a>";
                        dt.Rows[k]["oPerate"] = "<a href=\"Friend_List.aspx?type=edit_class&id=" + id + "\"  class=\"list_link\" title=\"修改此项\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改此项\" /></a><a href=\"Friend_List.aspx?type=delone_class&id=" + id + "\"  class=\"list_link\" title=\"删除此项\" onclick=\"{if(confirm('确认删除吗？其下的子类也将被删除!')){return true;}return false;}\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除此项\" /></a><a href=\"Friend_List.aspx?type=add_class&parentid=" + id + "\" class=\"list_link\" title=\"添加子类\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addclass.gif\" border=\"0\" alt=\"添加子类\" /></a><input type='checkbox' name='friend_checkbox' id='friend_checkbox' value=\"" + id + "\"/>";
                        strchar += "<tr class=\"TR_BG_list\">";
                        strchar += "<td  align=\"left\" valign=\"middle\" >" + dt.Rows[k]["ClassCName"] + "</td>";
                        strchar += "<td  align=\"center\" valign=\"middle\" >" + dt.Rows[k]["Content"] + "</td>";
                        strchar += "<td  align=\"center\" valign=\"middle\" >" + dt.Rows[k]["oPerate"] + "</td>";
                        strchar += "</tr>";
                        strchar += GetChildList(dt.Rows[k]["ClassID"].ToString(), "┝");
                        dt.Rows[k]["Colum"] = strchar;
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
    #endregion

    /// <summary>
    /// 递归
    /// </summary>
    /// <param name="Classid"></param>
    /// <param name="sign"></param>
    /// <returns></returns>

    #region 递归
    string GetChildList(string Classid, string sign)
    {
        String strchar = null;
        DataTable _dt = fl.GetChildClassList(Classid);
        sign += "┄";
        _dt.Columns.Add("oPerate", typeof(String));
        for (int pi = 0; pi < _dt.Rows.Count; pi++)
        {
            string id = _dt.Rows[pi]["ClassID"].ToString();
            _dt.Rows[pi]["ClassCName"] = "<a href='Friend_List.aspx?type=edit_class&id=" + id + "' class='list_link' title='点击查看详情或修改'>" + _dt.Rows[pi]["ClassCName"].ToString() + "</a>";
            //操作
            _dt.Rows[pi]["oPerate"] = "<a href=\"Friend_List.aspx?type=edit_class&id=" + id + "\"  class=\"list_link\" title=\"修改此项\"><img src=\"../../sysImages/folder/re.gif\" border=\"0\" alt=\"修改此项\" /></a><a href=\"Friend_List.aspx?type=delone_class&id=" + id + "\"  class=\"list_link\" title=\"删除此项\" onclick=\"{if(confirm('确认删除吗？')){return true;}return false;}\"><img src=\"../../sysImages/folder/del.gif\" border=\"0\" alt=\"删除此项\" /></a><a href=\"Friend_List.aspx?type=add_class&parentid=" + id + "\" class=\"list_link\" title=\"添加子类\"><img src=\"../../sysImages/folder/new.gif\" border=\"0\" alt=\"添加子类\" /></a><input type='checkbox' name='friend_checkbox' id='friend_checkbox' value=\"" + id + "\"/>";
            strchar += "<tr class=\"TR_BG_list\">";
            strchar += "<td  align=\"left\" valign=\"middle\" >" + sign + _dt.Rows[pi]["ClassCName"] + "</td>";
            strchar += "<td  align=\"center\" valign=\"middle\" >" + _dt.Rows[pi]["Content"] + "</td>";
            strchar += "<td  align=\"center\" valign=\"middle\" >" + _dt.Rows[pi]["oPerate"] + "</td>";
            strchar += "</tr>";
            strchar += GetChildList(_dt.Rows[pi]["ClassID"].ToString(), sign);
        }
        return strchar;

    }
    #endregion

    /// <summary>
    /// 连接管理页
    /// </summary>
    /// <param name="PageIndex"></param>
    /// code by chenzhaohui

    #region 连接管理页
    protected void FriendLinkManage(int PageIndex)//显示连接管理页面
    {
        int i, j;
        DataTable dt = Foosun.CMS.Pagination.GetPage("manage_Friend_Friend_List_2_aspx", PageIndex, PAGESIZE, out i, out j, null);
        this.PageNavigator2.PageCount = j;
        this.PageNavigator2.PageIndex = PageIndex;
        this.PageNavigator2.RecordCount = i;
        try
        {
            if (dt != null)//判断如果dt里面没有内容，将不会显示
            {
                if (dt.Rows.Count > 0)
                {
                    //添加列
                    dt.Columns.Add("class", typeof(String));//类别
                    dt.Columns.Add("type", typeof(String));//类型
                    dt.Columns.Add("author", typeof(String));//作者
                    dt.Columns.Add("lock", typeof(String));//锁定
                    dt.Columns.Add("operate", typeof(String));//操作

                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        int id = int.Parse(dt.Rows[k]["id"].ToString());
                        string LinkUrl = dt.Rows[k]["Url"].ToString();
                        string siteName = dt.Rows[k]["Name"].ToString();
                        string ClassID = dt.Rows[k]["ClassID"].ToString();
                        int Type = int.Parse(dt.Rows[k]["Type"].ToString());
                        int llock = int.Parse(dt.Rows[k]["Lock"].ToString());
                        string Authorr = dt.Rows[k]["Author"].ToString();
                        dt.Rows[k]["Name"] = "<a href='" + LinkUrl + "' class=\"list_link\" title='" + siteName + "' target=\"_bank\">" + dt.Rows[k]["Name"].ToString() + "</a>";
                        dt.Rows[k]["operate"] = "<input type=\"checkbox\" id=\"friend_checkbox_link\" value=\"" + dt.Rows[k]["id"].ToString() + "\" name=\"friend_checkbox_link\"  />";
                        #region 取类别的名称
                        string className = "";
                        try
                        {
                            DataTable dtcs = fl.CClas(ClassID);
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
                                dt.Rows[k]["lock"] = "<img src=\"../../sysImages/folder/yes.gif\" border=\"0\">";
                                break;
                            case 1:
                                dt.Rows[k]["lock"] = "<img src=\"../../sysImages/folder/no.gif\" border=\"0\">";
                                break;
                        }
                        #endregion

                        #region 取作者值
                        DataTable dt_user = fl.USerSess(Authorr);
                        string AdminName = (dt_user.Rows[0]["UserName"].ToString());
                        string isadmin = dt_user.Rows[0]["isAdmin"].ToString();
                        dt.Rows[k]["author"] = isadmin == "1" ? "管理员:<a href=\"../../" + Foosun.Config.UIConfig.dirUser + "/showUser.aspx?uid=" + AdminName + "\" target=\"_blank\" class=\"list_link\">" + AdminName + "</a>" : "会员:<a href=\"../../" + Foosun.Config.UIConfig.dirUser + "/showUser.aspx?uid=" + AdminName + "\" target=\"_blank\" class=\"list_link\">" + AdminName + "</a>";
                        #endregion
                        dt.Rows[k]["operate"] = "<a href=\"Friend_List.aspx?type=edit_link&id=" + id + "\"  class=\"list_link\" title=\"修改此项\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改此项\" /></a><input type='checkbox' name='friend_checkbox_link' id='friend_checkbox_link' value=\"" + id + "\"/>";
                    }
                }
                else
                {
                    NoContent_Link.InnerHtml = Show_NoContent_link();
                    this.PageNavigator2.Visible = false;
                }
            }
            else
            {
                NoContent_Link.InnerHtml = Show_NoContent_link();
                this.PageNavigator2.Visible = false;
            }
        }
        catch { }
        DataList2.DataSource = dt;
        DataList2.DataBind();
    }
    #endregion

    #region 提示无内容
    //当没有内容时显示没有内容提示
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
    string Show_NoContent_link()
    {
        string type = Request.QueryString["type"];
        string nos = "";
        if (type == "link")
        {
            nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
            nos = nos + "<tr class='TR_BG_list'>";
            nos = nos + "<td class='navi_link'>当前没有满足条件的连接！</td>";
            nos = nos + "</tr>";
            nos = nos + "</table>";
        }
        return nos;
    }
    #endregion

    /// <summary>
    /// 新增分类
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    #region 新增分类
    protected void SaveAddClass_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            //取得设置添加中的表单信息
            string parentid = this.ParentID.Text.Trim();//父类编号
            string Str_ClassCName = Foosun.Common.Input.Filter(this.ClassCName.Text.Trim());//类别名称
            string Str_ClassEName = Foosun.Common.Input.Filter(this.ClassEName.Text.Trim());//英文名称
            string Str_Description = Foosun.Common.Input.Filter(this.Content_Class.Value.Trim());//说明
            #region 检查重复数据
        check: string Str_ClassID = Foosun.Common.Rand.Number(12);
            if (fl.IsExitClassName(Str_ClassID) != 0)
                goto check;
            #endregion
            //检查是否有已经存在的类别名称
            if (fl.ISExitNam(Str_ClassCName) != 0)
            {
                PageError("对不起，该类别已经存在", "Friend_List.aspx?type=class");
            }
            //判断类别名称是否为空
            if (Str_ClassCName == null || Str_ClassCName == string.Empty)
            {
                PageError("对不起，类别名称不能为空，请返回继续添加", "Friend_List.aspx?type=class");
            }
            //向数据库中写入添加的类别信息

            int iclass = fl.Insert_Class(Str_ClassID, Str_ClassCName, Str_ClassEName, Str_Description, parentid);

            //载入数据-刷新页面
            if (iclass != 0)
            {
                log.SaveUserAdminLogs(1, 1, UserNum, "新增友情连接", "新增连接成功");
                PageRight("友情连接系统新增类别成功", "Friend_List.aspx?type=class");
            }
            else
            {
                PageError("意外错误：未知错误<br />", "shortcut_list.aspx");
            }

        }
    }
    #endregion

    /// <summary>
    ///  删除单个事件 类别
    /// </summary>
    ///code by chenzhaohui 

    #region 删除单个事件 类别
    protected void DelOne_Class()
    {
        string FID = Request.QueryString["ID"];
        string Parentid = Request.QueryString["parentid"];
        int del1 = 0, del2 = 0;
        if (FID == null)
        {
            PageError("错误的参数传递!", "");
        }
        else
        {
            try
            {
                del1 = fl.del_oneClass_1(FID);
                del2 = fl.del_oneClass_2(FID);
                PageRight("删除成功。", "Friend_List.aspx?type=class");
            }
            catch { }
        }
        PageError("意外错误：未知错误", "");
    }
    #endregion

    /// <summary>
    /// 删除单个事件 连接
    /// </summary>
    ///code by chenzhaohui 

    #region 删除单个事件 连接
    protected void DelOne_Link()
    {
        int FID = int.Parse(Request.QueryString["ID"].ToString());
        int dellink = 0;
        try
        {
            dellink = fl.del_onelink(FID);
            if (dellink == 0)
            {
                PageError("意外错误：未知错误", "");
            }
            else
            {
                PageRight("删除成功。", "Friend_List.aspx?type=link");
            }
        }
        catch { }
    }
    #endregion


    /// <summary>
    /// 锁定单个事件 连接
    /// </summary>
    /// code by chenzhaohui

    #region 锁定单个事件 连接
    protected void Suo()
    {
        int FID = int.Parse(Request.QueryString["ID"].ToString());
        int suoxone = 0;
        try
        {
            suoxone = fl.suo_onelink(FID);
            if (suoxone == 0)
            {
                PageError("意外错误：未知错误", "");
            }
            else
            {
                PageRight("锁定成功。", "Friend_List.aspx?type=link");
            }
        }
        catch { }
    }
    #endregion

    /// <summary>
    ///  解锁定单个事件 连接
    /// </summary>
    /// code by chenzhaohui

    #region 解锁定单个事件 连接
    protected void Unsuo()
    {
        int FID = int.Parse(Request.QueryString["id"].ToString());
        int unsuos = 0;
        try
        {
            unsuos = fl.unsuo_onelink(FID);
            if (unsuos == 0)
            {
                PageError("意外错误：未知错误", "");
            }
            else
            {
                PageRight("解锁成功!!", "Friend_List.aspx?type=link");
            }
        }
        catch { }
    }
    #endregion

    /// <summary>
    /// 修改初始页面值 分类
    /// </summary>
    /// code by chenzhaohui

    #region 修改初始页面值 分类
    protected void EditClass_Start()
    {
        string FID = Request.QueryString["ID"];
        DataTable dt = fl.EditClass(FID);
        if (dt.Rows.Count > 0)
        {
            this.ParentIDEdit.Text = dt.Rows[0]["ParentID"].ToString().Trim();
            this.ClassCNameEdit.Text = dt.Rows[0]["ClassCName"].ToString().Trim();
            this.ClassENameEdit.Text = dt.Rows[0]["ClassEName"].ToString().Trim();
            this.DescriptionE.Value = dt.Rows[0]["Content"].ToString().Trim();
        }
        else
        {
            PageError("未知错误,异常错误", "Friend_List.aspx?type=class");
        }
    }
    #endregion

    /// <summary>
    /// 修改保存事件 分类
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    ///code by chenzhaohui 

    #region 修改保存事件 分类
    protected void UpdateClass_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            string FID = Request.QueryString["ID"];
            //取得添加中的表单信息
            string Str_ClassNameE = Foosun.Common.Input.Filter(this.ClassCNameEdit.Text.Trim());//类别名称
            string Str_EnglishE = Foosun.Common.Input.Filter(this.ClassENameEdit.Text.Trim());//英文
            string Str_Descript = Foosun.Common.Input.Filter(this.DescriptionE.Value.Trim());//说明

            //判断类别名称是否为空
            if (Str_ClassNameE == null || Str_ClassNameE == string.Empty)
            {
                PageError("对不起，类别名称不能为空，请返回继续修改", "Friend_List.aspx?type=class");
            }
            int updat = fl.EditClick(FID, Str_ClassNameE, Str_EnglishE, Str_Descript);

            #region 刷新页面
            if (updat != 0)
            {
                PageRight("修改成功", "Friend_List.aspx?type=class");
            }
            else
            {
                PageError("意外错误：未知错误<br>", "Friend_List.aspx?type=class");
            }
            #endregion
        }
    }
    #endregion

    /// <summary>
    /// 批量删除分类
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    #region 批量删除分类
    protected void delP_class_Click(object sender, EventArgs e)
    {
        string friend_checkbox = Request.Form["friend_checkbox"];
        int box1 = 0;
        int box2 = 0;
        if (friend_checkbox == null || friend_checkbox == String.Empty)
        {
            PageError("请先选择批量操作的内容!", "");
        }
        else
        {
            String[] CheckboxArray = friend_checkbox.Split(',');
            friend_checkbox = null;
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                box1 = fl._DelPClass(CheckboxArray[i]);
                box2 = fl._DelPClass2(CheckboxArray[i]);
            }
            PageRight("删除数据成功,请返回继续操作!", "Friend_List.aspx?type=class");
        }
        PageError("删除数据失败,请与管理联系!", "");
    }
    #endregion

    /// <summary>
    /// 删除全部分类
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    #region 删除全部分类
    protected void delall_class_Click(object sender, EventArgs e)
    {
        int dall = fl._DelAllClass();
        if (dall != 0)
        {
            PageRight("删除全部成功。", "Friend_List.aspx?type=class");
        }
        else
        {
            PageError("删除数据失败,请与管理联系!", "");
        }
    }
    #endregion

    /// <summary>
    /// 批量锁定 连接
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    #region 批量锁定 连接
    protected void SuoP_Click(object sender, EventArgs e)
    {
        string friend_checkbox_link = Request.Form["friend_checkbox_link"];
        int link_lock = 0;
        if (friend_checkbox_link == null || friend_checkbox_link == String.Empty)
        {
            PageError("请先选择批量操作的内容!", "");
        }
        else
        {
            String[] CheckboxArray = friend_checkbox_link.Split(',');
            friend_checkbox_link = null;
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                link_lock = fl._LockP_Link(CheckboxArray[i]);
                if (link_lock == 0)
                {
                    PageError("锁定数据失败,请与管理联系!", "");
                    break;
                }
            }
            PageRight("锁定数据成功!", "Friend_List.aspx?type=link");
        }
    }
    #endregion

    /// <summary>
    /// 批量解锁 连接
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    #region 批量解锁 连接
    protected void UnsuoP_Click(object sender, EventArgs e)
    {
        string friend_checkbox_link = Request.Form["friend_checkbox_link"];
        int unlockk = 0;
        if (friend_checkbox_link == null || friend_checkbox_link == String.Empty)
        {
            PageError("请先选择批量操作的内容!", "");
        }
        else
        {
            String[] CheckboxArray = friend_checkbox_link.Split(',');
            friend_checkbox_link = null;
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                unlockk = fl._unLockP_Link(CheckboxArray[i]);
                if (unlockk == 0)
                {
                    PageError("锁定数据失败!", "");
                    break;
                }
            }
            PageRight("解锁数据成功!", "Friend_List.aspx?type=link");
        }
    }
    #endregion

    /// <summary>
    /// 批量删除 连接
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    #region 批量删除 连接
    protected void delP_link_Click(object sender, EventArgs e)
    {
        string friend_checkbox_link = Request.Form["friend_checkbox_link"];
        int delpl = 0;
        if (friend_checkbox_link == null || friend_checkbox_link == String.Empty)
        {
            PageError("请先选择批量操作的内容!", "");
        }
        else
        {
            String[] CheckboxArray = friend_checkbox_link.Split(',');
            friend_checkbox_link = null;
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                delpl = fl._delP_Link(CheckboxArray[i]);
                if (delpl == 0)
                {
                    PageError("删除数据失败,可能是没找到记录!", "");
                    break;
                }
            }
            PageRight("删除数据成功,请返回继续操作!", "Friend_List.aspx?type=link");
        }
    }
    #endregion

    /// <summary>
    /// 删除全部 连接
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    #region 删除全部 连接
    protected void delall_link_Click(object sender, EventArgs e)
    {
        int delao = fl._delAll_Link();
        if (delao == 0)
        {
            PageError("意外错误：未知错误", "");
        }
        else
        {
            PageRight("删除全部成功。", "Friend_List.aspx?type=link");
        }
    }
    #endregion

    /// <summary>
    /// 保存连接增加事件 link
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    #region 保存连接增加事件 link
    protected void SaveLink_ServerClick(object sender, EventArgs e)
    {
        if (this.SelectClass.Items.Count == 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "提示", "<script>if(confirm('您还没有任何友情连接分类!现在就添加吗?')==true){window.location.href='?type=add_class';}</script>");
        }
        else if (Page.IsValid)//判断页面是否通过验证
        {
            #region 取得设置添加中的表单信息

            string Str_Class = Request.Form["SelectClass"];//选择类别
            string Str_Name = Foosun.Common.Input.Filter(this.Name.Text.Trim());//站点名称
            string Str_Type = this.Type.Text.Trim();//连接类型
            string Str_Url = Foosun.Common.Input.Filter(this.Url.Text.Trim());//连接地址
            string Str_Content = Foosun.Common.Input.Filter(this.ContentFri.Value.Trim());//站点说明
            string Str_PicUrl = Foosun.Common.Input.Filter(this.PicUrl.Text);//图片地址
            string Str_Author = Foosun.Common.Input.Filter(this.Author.Text.Trim());//申请人作者
            string Str_Mail = Foosun.Common.Input.Filter(this.Mail.Text.Trim());//邮件
            string Str_ContentFor = Foosun.Common.Input.Filter(this.ContentFor.Value.Trim());//申请理由
            string Str_LinkContent = Foosun.Common.Input.Filter(this.LinkContent.Value.Trim());//其他联系方式
            string Str_Addtime = this.Addtime.Text.Trim();//添加时间

            #endregion

            int Isuser = 0, isLok = 0;
            if (Lock.Checked == true)
            {
                isLok = 1;
            }
            if (IsUser.Checked == true)
            {
                Isuser = 1;
            }
            #region 类型
            string tip = null;
            switch (Str_Type)
            {
                case "1":
                    tip = "文字连接添加";
                    break;
                case "0":
                    tip = "图片连接添加";
                    break;
            }
            #endregion
            #region 检查是否有已经存在的连接名称
            if (fl.ExistName_Link(Str_Name) != 0)
            {
                PageError("对不起，该连接已经存在", "Friend_List.aspx?type=link");
            }
            if (Str_Name == null || Str_Name == string.Empty)
            {
                PageError("对不起，连接名称不能为空", "Friend_List.aspx?type=link");
            }
            #endregion

            #region 向数据库中写入添加的连接信息
            int lsa = fl._LinkSave(Str_Class, Str_Name, Str_Type, Str_Url, Str_Content, Str_PicUrl, Str_Author, Str_Mail, Str_ContentFor, Str_LinkContent, Str_Addtime, Isuser, isLok);
            //载入数据-刷新页面
            if (lsa != 0)
            {
                log.SaveUserAdminLogs(1, 1, UserNum, "" + tip + "", "" + tip + "成功");
                PageRight("" + tip + "成功", "Friend_List.aspx?type=link");
            }
            else
            {
                PageError("意外错误：未知错误<br />", "shortcut_list.aspx");
            }
            #endregion
        }
    }
    #endregion

    /// <summary>
    /// 修改初始页面值 连接
    /// </summary>
    /// code by chenzhaohui

    #region 修改初始页面值 连接
    protected void Edit_Link_Start()
    {
        int FID = int.Parse(Request.QueryString["ID"].ToString());
        DataTable dt = fl.Start_Link(FID);
        if (dt.Rows.Count > 0)
        {
            //Response.Write("<script language=\"javascript\">SelectE('" + dt.Rows[0]["Type"].ToString() + "');</script>");

            getClassInfo_Edit(dt.Rows[0]["ClassID"].ToString());//取类别信息
            this.SiteName.Text = dt.Rows[0]["Name"].ToString().Trim();
            this.LinkType.Text = dt.Rows[0]["Type"].ToString().Trim();
            this.linkUrl.Text = dt.Rows[0]["Url"].ToString().Trim();
            this.siteDesc.Value = dt.Rows[0]["Content"].ToString().Trim();
            this.PicUrll.Text = dt.Rows[0]["PicUrl"].ToString().Trim();
            if (dt.Rows[0]["Lock"].ToString() == "1")
            {
                isSuo.Checked = true;
            }
            else
            {
                isSuo.Checked = false;
            }
            if (dt.Rows[0]["IsUser"].ToString() == "1")
            {
                isUserr.Checked = true;
            }
            else
            {
                isUserr.Checked = false;
            }
            this.Authorr.Text = dt.Rows[0]["Author"].ToString().Trim();
            this.Emaill.Text = dt.Rows[0]["Mail"].ToString().Trim();
            this.forfor.Value = dt.Rows[0]["ContentFor"].ToString().Trim();
            this.otherl.Value = dt.Rows[0]["LinkContent"].ToString().Trim();
            this.datetime.Text = dt.Rows[0]["Addtime"].ToString().Trim();
        }
        else
        {
            PageError("未知错误,异常错误", "Friend_List.aspx?type=link");
        }
    }
    //protected void showjst()
    //{
    //    int FID = int.Parse(Request.QueryString["ID"].ToString());
    //    DataTable dt = fl.Start_Link(FID);
    //    if (dt.Rows.Count > 0)
    //    {
    //        Response.Write("<script language=\"javascript\">SelectE('" + dt.Rows[0]["Type"].ToString() + "');</script>");
    //    }
    //}
    #endregion

    /// <summary>
    /// 递归
    /// </summary>
    /// <param name="classid"></param>

    #region
    protected void getClassInfo_Edit(string classid)
    {
        dt_class = fl.Edit_Link_Di();
        if (dt_class != null)
        {
            ClassRender_Edit("0", 0, classid);
        }
        dt_class.Clear();
        dt_class.Dispose();
    }
    private void ClassRender_Edit(string PID, int Layer, string classid)
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
                if (classid == it.Value)
                {
                    it.Selected = true;
                }
                string stxt = "├";
                for (int i = 0; i < Layer; i++)
                {
                    stxt += "─";
                }
                it.Text = stxt + r["ClassCName"].ToString();
                this.Sclass.Items.Add(it);
                ClassRender_Edit(r["ClassID"].ToString(), Layer + 1, classid);
            }
        }
    }
    #endregion

    /// <summary>
    /// 修改连接
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    #region 修改连接
    protected void EditFriend_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            int FID = int.Parse(Request.QueryString["ID"].ToString());
            //取得表单信息
            string Str_Class = Request.Form["Sclass"];//选择类别
            string Str_Name = Foosun.Common.Input.Filter(this.SiteName.Text.Trim());//站点名称
            string Str_Type = this.LinkType.Text;//连接类型
            string Str_Url = Foosun.Common.Input.Filter(this.linkUrl.Text.Trim())+"";//连接地址
            string Str_Content = this.siteDesc.Value+"";//站点说明
            string Str_PicUrl = Foosun.Common.Input.Filter(this.PicUrll.Text.Trim())+"";//图片地址
            int Isuser = 0, isLok = 0;
            if (isSuo.Checked == true)
            {
                isLok = 1;
            }
            if (isUserr.Checked == true)
            {
                Isuser = 1;
            }
            string Str_Author = Foosun.Common.Input.Filter(this.Authorr.Text.Trim())+"";//申请人作者
            string Str_Mail = Foosun.Common.Input.Filter(this.Emaill.Text.Trim())+"";//邮件
            string Str_ContentFor = Foosun.Common.Input.Filter(this.forfor.Value.Trim())+"";//申请理由
            string Str_LinkContent = Foosun.Common.Input.Filter(this.otherl.Value.Trim())+"";//其他联系方式
            string Str_Addtime = this.datetime.Text.Trim();//添加时间
            if (Str_Addtime == null || Str_Addtime == "")
            {
                Str_Addtime = DateTime.Now.ToString();
            }
            //判断名称是否为空
            if (Str_Name == null || Str_Name == string.Empty)
            {
                PageError("对不起，名称不能为空，请返回继续修改", "Friend_List.aspx?type=link");
            }
            int elink = fl.Update_Link(Str_Class, Str_Name, Str_Type, Str_Url, Str_Content, Str_PicUrl, Isuser, isLok, Str_Author, Str_Mail, Str_ContentFor, Str_LinkContent, Str_Addtime, FID);
            #region 刷新页面
            if (elink != 0)
            {
                PageRight("修改成功", "Friend_List.aspx?type=link");
            }
            else
            {
                PageError("意外错误：未知错误<br>", "");
            }
            #endregion
        }
    }
    #endregion

    /// <summary>
    /// 查询连接
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    #region 查询连接
    //protected void BtnSearch_Click(object sender, EventArgs e)
    //{
    //    FriendLinkManage(1);
    //}
    #endregion

    /// <summary>
    /// 获取父类ID开始
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    protected void GetParentValue()
    {
        string parentid = Request.QueryString["parentid"];//父类编号
        if (parentid == "" || parentid == null || parentid == string.Empty)
        {
            this.ParentID.Text = "0";
        }
        else
        {
            this.ParentID.Text = Foosun.Common.Input.Filter(parentid);
        }
    }

    /// <summary>
    /// 作者编号
    /// </summary>
    /// code by chenzhaohui

    protected void StartUser()
    {
        DataTable dt = fl.UserNumm();
        if (dt.Rows.Count > 0)
        {
            this.Author.Text = dt.Rows[0]["UserNum"].ToString().Trim();
        }
        else
        {
            this.Author.Text = "";
        }
    }
}