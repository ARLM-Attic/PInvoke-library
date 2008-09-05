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

public partial class user_show_info : Foosun.Web.UI.BasePage
{
    UserMisc rd = new UserMisc();
    rootPublic pd = new rootPublic();
    Constr con = new Constr();
    Discuss dis = new Discuss();
    public string tmpDir = Foosun.Config.UIConfig.UserdirFile;
    public string tmpDir1 = Foosun.Config.UIConfig.dirFile;
    public string dirdumm = Foosun.Config.UIConfig.dirDumm;
    protected void Page_Load(object sender, EventArgs e)
    {
        copyright.InnerHtml = CopyRight;
        string _s = Request.QueryString["s"];
        string u_name = Request.QueryString["uid"];
        string uID = "";
        ePointName.InnerHtml = pd.getgPointName();
        if (dirdumm.Trim() != string.Empty)
        {
            tmpDir = dirdumm + "/" + Foosun.Config.UIConfig.UserdirFile;
            tmpDir1 = dirdumm + "/" + Foosun.Config.UIConfig.dirFile;
        }
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        this.PageNavigator2.OnPageChange += new PageChangeHandler(PageNavigator2_PageChange);
        this.PageNavigator3.OnPageChange += new PageChangeHandler(PageNavigator3_PageChange);
        if (!IsPostBack)
        {
            if (u_name != "" && u_name != null)
            {
                uID = pd.getUserNameUserNum(Foosun.Common.Input.Filter(u_name.ToString()));
                if (uID == "0")
                {
                    PageError("找不到用户 [" + u_name.ToString() + "] 的信息.<li>原因：此用户未注册或者被管理员删除!</li>", "");
                }
                Contentlist(1);
                Photolists(1);
                grouplist(1);
                switch (_s)
                {
                    case "content":
                        contentClass.InnerHtml = cclass(uID);
                        this.Constrlist.Visible = true;
                        this.infobase.Visible = false;
                        this.Photolist.Visible = false;
                        this.infogroup.Visible = false;
                        this.infolink.Visible = false;
                        this.bbslist.Visible = false;
                        break;
                    case "photo":
                        this.Constrlist.Visible = false;
                        this.infobase.Visible = false;
                        this.Photolist.Visible = true;
                        this.infogroup.Visible = false;
                        this.infolink.Visible = false;
                        this.bbslist.Visible = false;
                        break;
                    case "group":
                        this.Constrlist.Visible = false;
                        this.infobase.Visible = false;
                        this.Photolist.Visible = false;
                        this.infogroup.Visible = true;
                        this.infolink.Visible = false;
                        this.bbslist.Visible = false;
                        break;

                    case "bbs":
                        this.Constrlist.Visible = false;
                        this.infobase.Visible = false;
                        this.Photolist.Visible = false;
                        this.infogroup.Visible = false;
                        this.bbslist.Visible = true;
                        break;

                    case "link":
                        linkList.InnerHtml = linkContent();
                        this.Constrlist.Visible = false;
                        this.infobase.Visible = false;
                        this.Photolist.Visible = false;
                        this.infogroup.Visible = false;
                        this.bbslist.Visible = false;
                        this.infolink.Visible = true;
                        break;

                    default:
                        break;
                }
            }
            else
            {
                PageError("参数传递错误", "");
            }
            #region 个人资料
            DataTable dt = rd.getUserUserNumRecord(uID);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if(rd.getisAdmin()!=1)
                    {
                        if (dt.Rows[0]["islock"].ToString() == "1"){PageError("此会员已被锁定!", "");}
                    }
                    string isOpen = dt.Rows[0]["isOpen"].ToString();
                    if (isOpen == "0")
                    {
                        if ((u_name.ToString()).ToUpper() != (Foosun.Global.Current.UserName).ToUpper())
                        {
                            marriTF.Visible = false;
                            mobileTF.Visible = false;
                            otherTF.Visible = false;
                            linkTF.Visible = false;
                        }
                    }
                    #region 基本资料--------------------------------------
                    string userflag = pd.getGroupNameFlag(uID);
                    string _Tmpls = dt.Rows[0]["UserName"].ToString();
                    if (userflag.IndexOf("|") != -1)
                    {
                        string[] userflagARR = userflag.Split('|');
                        _Tmpls = userflagARR[0] + _Tmpls + userflagARR[1];
                    }
                    else
                    {
                        _Tmpls = userflag + _Tmpls;
                    }
                    this.UserNamex.Text = _Tmpls;//用户名
                    this.NickNamex.Text = dt.Rows[0]["NickName"].ToString();//会员昵称
                    this.RealNamex.Text = dt.Rows[0]["RealName"].ToString();//会员真实姓名
                    if (dt.Rows[0]["Sex"].ToString() != "")
                    {
                        int Sex = Convert.ToInt32(dt.Rows[0]["Sex"].ToString());//会员性别
                        switch (Sex)
                        {
                            case 0:
                                this.Sexx.Text = "保密";
                                break;
                            case 1:
                                this.Sexx.Text = "男";
                                break;
                            case 2:
                                this.Sexx.Text = "女";
                                break;
                        }
                    }
                    else
                    {
                        this.Sexx.Text = "保密";
                    }
                    #endregion
                    levelsFace.InnerHtml = getlevels(int.Parse(dt.Rows[0]["iPoint"].ToString()));
                    //--------------------------出生日期-----------------------------------
                    this.birthdayx.Text = dt.Rows[0]["birthday"].ToString();
                    //-------------------------------------------------------------
                    if (dt.Rows[0]["UserFace"].ToString() != "")
                    {
                        this.UserFacex.ImageUrl = dt.Rows[0]["UserFace"].ToString().Replace("{@userdirfile}", tmpDir).Replace("{@dirfile}", tmpDir1);//头像 
                    }
                    if (dt.Rows[0]["marriage"].ToString() != "")
                    {
                        int marriage = int.Parse(dt.Rows[0]["marriage"].ToString());//是否结婚
                        switch (marriage)
                        {
                            case 0:
                                this.marriagex.Text = "保密";
                                break;
                            case 1:
                                this.marriagex.Text = "未婚";
                                break;
                            case 2:
                                this.marriagex.Text = "已婚";
                                break;
                        }
                    }
                    else
                    {
                        this.marriagex.Text = "保密";
                    }
                    this.Userinfox.Text = dt.Rows[0]["Userinfo"].ToString();//用户签名
                    this.UserGroupNumberx.Text = pd.getGroupName("" + dt.Rows[0]["UserGroupNumber"] + "");//会员所属于的会员组
                    #region 以前的
                    this.iPointx.Text = dt.Rows[0]["iPoint"].ToString();//积分
                    this.gPointx.Text = dt.Rows[0]["gPoint"].ToString();//金币
                    #endregion 
                    this.Emailx.Text = dt.Rows[0]["Email"].ToString();
                    this.cPointx.Text = dt.Rows[0]["cPoint"].ToString();//魅力值
                    this.ePointx.Text = dt.Rows[0]["ePoint"].ToString();//人气值
                    this.aPointx.Text = dt.Rows[0]["aPoint"].ToString();//活跃值
                    this.RegTimex.Text = dt.Rows[0]["RegTime"].ToString();//注册日期
                    this.OnlineTimex.Text = dt.Rows[0]["OnlineTime"].ToString();//用户在线时间
                    this.LoginNumberx.Text = dt.Rows[0]["LoginNumber"].ToString();//用户登陆次数
                    //-----------------------联系方式--------------------------------------
                    this.Mobilex.Text = dt.Rows[0]["Mobile"].ToString();	//手机
                    //-------------------------------------------------------------
                    dt.Clear();
                    dt.Dispose();
                }
                else
                {
                    PageError("错误的参数<li>找不到此用户</li>", "index.aspx");
                }
            }
            DataTable dts = rd.getUserUserfields(pd.getUserNameUserNum(u_name.ToString()));
            if (dts != null)
            {
                if (dts.Rows.Count > 0)
                {
                    this.Jobx.Text = dts.Rows[0]["Job"].ToString();//职业
                    this.provincex.Text = dts.Rows[0]["province"].ToString();//省
                    this.Cityx.Text = dts.Rows[0]["City"].ToString();//市
                    //------------------------详细资料-------------------------------------
                    this.Nationx.Text = dts.Rows[0]["Nation"].ToString();//民族
                    this.nativeplacex.Text = dts.Rows[0]["nativeplace"].ToString();//籍贯
                    this.orgSchx.Text = dts.Rows[0]["orgSch"].ToString();//组织关系
                    this.characterx.Text = dts.Rows[0]["character"].ToString();//性格
                    this.UserFanx.Text = dts.Rows[0]["UserFan"].ToString();//用户爱好
                    this.educationx.Text = dts.Rows[0]["education"].ToString();//学历
                    this.Lastschoolx.Text = dts.Rows[0]["Lastschool"].ToString();//毕业学校
                    this.Addressx.Text = dts.Rows[0]["Address"].ToString();//地址
                    this.Postcodex.Text = dts.Rows[0]["Postcode"].ToString();	//邮政编码
                    this.FaTelx.Text = dts.Rows[0]["FaTel"].ToString(); 	//家庭联系电话
                    this.WorkTelx.Text = dts.Rows[0]["WorkTel"].ToString();	//工作单位联系电话
                    this.Faxx.Text = dts.Rows[0]["Fax"].ToString();	//传真
                    this.QQx.Text = dts.Rows[0]["QQ"].ToString(); 	//QQ
                    this.MSNx.Text = dts.Rows[0]["MSN"].ToString(); 	//MSN号
                }
                dts.Clear();
                dts.Dispose();
            }
            #endregion
        }
    }

    protected string getlevels(int ipoint)
    {
        string _Str = "";
        DataTable dt = rd.getleves();
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ipoint >= int.Parse(dt.Rows[i]["iPoint"].ToString()))
                {
                    _Str = "&nbsp;&nbsp;等级:&nbsp;<img src=\"../../sysImages/face/" + dt.Rows[i]["Lpicurl"].ToString() + "\" border=\"0\" alt=\"" + dt.Rows[i]["LTitle"].ToString() + "\" />" + dt.Rows[i]["LTitle"].ToString() + "";
                }
            }
            dt.Clear(); dt.Dispose();
        }
        return _Str;
    }


    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        Contentlist(PageIndex);
    }
    protected void Contentlist(int PageIndex)
    {
        string UserNum = Request.QueryString["uid"];
        string ClassID = Request.QueryString["ClassID"];
        string UserNumID = "";
        if (UserNum != "" && UserNum != null)
        {
            UserNumID = pd.getUserNameUserNum(Foosun.Common.Input.Filter(UserNum.ToString()));
        }
        int i, j;
        DataTable dt = null;
        if (ClassID != null && ClassID != "")
        {
            SQLConditionInfo[] st = new SQLConditionInfo[2];
            st[0] = new SQLConditionInfo("@UserNum", UserNumID);
            st[1] = new SQLConditionInfo("@ClassID", Foosun.Common.Input.Filter(ClassID));
            dt = Foosun.CMS.Pagination.GetPage("user_ShowUser_1_aspx", PageIndex, 20, out i, out j, st);
        }
        else
        {
            SQLConditionInfo st = new SQLConditionInfo("@UserNum", UserNumID);
            dt = Foosun.CMS.Pagination.GetPage("user_ShowUser_1_1_aspx", PageIndex, 20, out i, out j, st);
        }
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null && dt.Rows.Count != 0)
        {
            dt.Columns.Add("cNames", typeof(string));
            dt.Columns.Add("UserName", typeof(string));
            for (int s = 0; s < dt.Rows.Count; s++)
            {
                dt.Rows[s]["cNames"] = con.sel_cName(dt.Rows[s]["ClassID"].ToString());
                dt.Rows[s]["UserName"] = UserNum;
            }
            DataList1.DataSource = dt;
            DataList1.DataBind();
        }
    }

    protected string cclass(string UserNum)
    {
        string _STR = "文章分类：<a class=\"list_link\" href=\"info.aspx?s=content&uid=" + Request.QueryString["uid"] + "\">全部</a>&nbsp;│&nbsp;";
        DataTable dt = rd.getConstrClass(UserNum);
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                _STR += "<a class=\"list_link\" href=\"info.aspx?s=content&uid="+Request.QueryString["uid"]+"&ClassID=" + dt.Rows[i]["Ccid"].ToString() + "\" title=\"" + dt.Rows[i]["Content"].ToString() + "\">" + dt.Rows[i]["cName"].ToString() + "</a>&nbsp;│&nbsp;";
            }
            dt.Clear(); dt.Dispose();
        }
        return _STR;
    }

    protected void PageNavigator2_PageChange(object sender, int PageIndex)
    {
        Photolists(PageIndex);
    }
    protected void Photolists(int PageIndex)
    {
        string UserNum = Request.QueryString["uid"];
        string UserNumID = "";
        if (UserNum != "" && UserNum != null)
        {
            UserNumID = pd.getUserNameUserNum(Foosun.Common.Input.Filter(UserNum.ToString()));
        }
        SQLConditionInfo st = new SQLConditionInfo("@UserNum", UserNumID);
        int i, j;
        DataTable dt = Foosun.CMS.Pagination.GetPage("user_ShowUser_2_aspx", PageIndex, 30, out i, out j, st);
        this.PageNavigator2.PageCount = j;
        this.PageNavigator2.PageIndex = PageIndex;
        this.PageNavigator2.RecordCount = i;
        if (dt != null && dt.Rows.Count != 0)
        {
            dt.Columns.Add("PhotoalbumNames", typeof(string));
            dt.Columns.Add("picnum", typeof(string));
            dt.Columns.Add("pwds", typeof(string));
            dt.Columns.Add("Pic", typeof(string));
            for (int s = 0; s < dt.Rows.Count; s++)
            {
                string PicURL = "";
                int sel_picnumber = rd.sel_picnum(dt.Rows[s]["PhotoalbumID"].ToString());
                if (sel_picnumber != 0)
                {
                    string _dirDumm = Foosun.Config.UIConfig.dirDumm;
                    if (_dirDumm.Trim() != ""){_dirDumm = _dirDumm + "/";}
                    dt.Rows[s]["picnum"] = "(" + sel_picnumber + ")";
                    PicURL = rd.sel_pic(dt.Rows[s]["PhotoalbumID"].ToString()).Replace("{@userdirfile}", _dirDumm + Foosun.Config.UIConfig.UserdirFile);
                }
                else
                {
                    dt.Rows[s]["picnum"] = "(0)";
                    PicURL = "../../sysImages/user/nopic_supply.gif";
                }
                if (dt.Rows[s]["pwd"].ToString() != "")
                {
                    dt.Rows[s]["pwds"] = "<span class=\"tbie\" title=\"有密码\">(*)</span>";
                }
                else
                {
                    dt.Rows[s]["pwds"] = "";
                }

                dt.Rows[s]["Pic"] = "<a href=\"showphoto.aspx?PhotoalbumID=" + dt.Rows[s]["PhotoalbumID"].ToString() + "&uid=" + UserNum + "\" class=\"list_link\"><Img ID=\"PicImage\" border=\"0\" Height=\"90px\" Width=\"90px\" src=\"" + PicURL + "\" /></a>";
                dt.Rows[s]["PhotoalbumNames"] = "<a href=\"showphoto.aspx?PhotoalbumID=" + dt.Rows[s]["PhotoalbumID"].ToString() + "&uid=" + UserNum + "\" class=\"list_link\">" + dt.Rows[s]["PhotoalbumName"].ToString() + "</a>";

            }
            DataList2.DataSource = dt;
            DataList2.DataBind();
        }
    }

    protected void PageNavigator3_PageChange(object sender, int PageIndex)
    {
        grouplist(PageIndex);
    }
    protected void grouplist(int PageIndex)
    {
        string UserNum = Request.QueryString["uid"];
        int i, j;
        SQLConditionInfo sts = new SQLConditionInfo("@UserName", Foosun.Common.Input.Filter(UserNum.ToString()));
        DataTable cjlistdts = Foosun.CMS.Pagination.GetPage("user_discuss_discussManageestablish_list_aspx", PageIndex, 20, out i, out j, sts);
        this.PageNavigator3.PageCount = j;
        this.PageNavigator3.PageIndex = PageIndex;
        this.PageNavigator3.RecordCount = i;
        if (cjlistdts != null && cjlistdts.Rows.Count != 0)
        {
            cjlistdts.Columns.Add("cutDisID2", typeof(string));
            DataTable selectcjDisID = dis.sel_22();
            foreach (DataRow h in cjlistdts.Rows)
            {
                int v = (int)selectcjDisID.Compute("Count(DisID)", "DisID='" + h["DisID"].ToString() + "'");
                h["cutDisID2"] = v;
            }
            Repeater2.DataSource = cjlistdts;
            Repeater2.DataBind();
        }
    }

    /// <summary>
    /// 友情连接
    /// </summary>
    /// <returns></returns>
    protected string linkContent()
    {
        Info ud = new Info();
        string _uid = Request.QueryString["uid"];
        string listSTR = "<div style=\"padding-top:10px;font-weight:bold;height:20px;\">文字连接</div>";
        if (_uid != null && _uid != "")
        {
            string userNum = pd.getUserNameUserNum(Foosun.Common.Input.Filter(_uid.ToString()));
            DataTable dt = ud.getflist(1, userNum);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listSTR += "<a title=\"" + dt.Rows[i]["Content"].ToString() + "\" href=\"" + dt.Rows[i]["URL"].ToString() + "\" class=\"list_link\" target=\"_blank\">" + dt.Rows[i]["Name"].ToString() + "</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                dt.Clear(); dt.Dispose();
            }
            listSTR += "<div style=\"padding-top:20px;font-weight:bold;height:20px;\">图片连接</div>";
            DataTable dt1 = ud.getflist(0, userNum);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                for (int m = 0; m < dt1.Rows.Count; m++)
                {
                    listSTR += "<a href=\"" + dt1.Rows[m]["URL"].ToString() + "\" target=\"_blank\"><img src=\"" + dt1.Rows[m]["Picurl"].ToString() + "\" title=\"" + dt1.Rows[m]["Content"].ToString() + "\" style=\"width:88px;height:31px;\" border=\"0\" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                dt1.Clear(); dt1.Dispose();
            }
        }
        else
        {
            PageError("错误的参数<li>参数传递有错误!</li>", "");
        }
        return listSTR;
    }
 
}
