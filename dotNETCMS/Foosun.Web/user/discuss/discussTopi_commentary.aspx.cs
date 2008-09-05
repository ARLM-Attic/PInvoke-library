//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                     Forum:bbs.foosun.net                        ==
//==                     WebSite:www.foosun.net                      ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                   Tel:86-28-85098980/66026180                   ==
//==                   QQ:655071,MSN:ikoolls@gmail.com               ==
//==                   Email:Service@foosun.cn                       ==
//==                      Code By WangZhenjiang                      ==
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
using Foosun.Model;

public partial class user_discuss_discussTopi_commentary : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        
        string DisIDs = Request.QueryString["DisID"];
        string Dtd = Request.QueryString["DtID"];
        if ((DisIDs != "") && (Dtd != ""))
        {
            DisIDs = Foosun.Common.Input.Filter(DisIDs.ToString());
            Dtd = Foosun.Common.Input.Filter(Dtd.ToString());
        }
        else
        {
            PageError("错误的参数", "");
        }
        sc.InnerHtml = Show_sc(DisIDs);
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!Page.IsPostBack)
        {
            ///************************************************************************************************************
            ///**********检查主题是否是投票********************************************************************************
            ///************************************************************************************************************
            DataTable dt_VoteTF = dis.sel_37(Dtd);
            int VoteTFIDs = int.Parse(dt_VoteTF.Rows[0]["VoteTF"].ToString());
            DateTime voteTime = DateTime.Parse(dt_VoteTF.Rows[0]["voteTime"].ToString());
            DateTime tms=DateTime.Now;
            if (VoteTFIDs == 1)
            {
                if (voteTime > tms)
                {
                    VoteTF.InnerHtml = Show_VoteTF(Dtd);
                }
            }
            ///************************************************************************************************************
            Show_cjlist(1);
            DataTable dt_sel_dis = dis.sel_47(DisIDs);
            int cut_sel_dis = dt_sel_dis.Rows.Count;
            if (cut_sel_dis == 0)
            {
                PageError("错误", "discussTopi_list.aspx?DisID=" + DisIDs + "");
            }
            string[] Authority = dt_sel_dis.Rows[0]["Authority"].ToString().Split('|');
            int Authority1 = int.Parse(Authority[2].ToString());
            DataTable dt_sel_diss = dis.sel_37(Dtd);
            int cut_sel_diss = dt_sel_diss.Rows.Count;
            if (cut_sel_diss == 0)
            {
                PageError("错误", "discussTopi_list.aspx?DisID=" + DisIDs + "");
            }
            DateTime Cutofftime1 = DateTime.Parse(dt_sel_diss.Rows[0]["voteTime"].ToString());
            DateTime dtm = DateTime.Now;
            int vtf = int.Parse(dt_VoteTF.Rows[0]["VoteTF"].ToString());
            if (Authority1 != 0 )
            {
                if (Cutofftime1 > dtm)
                {
                    if (vtf == 1)
                    {
                        this.Panel1.Visible = true;
                    }
                    cmment.InnerHtml = Show_cmm();
                    cmment1.InnerHtml = Show_cmm();
                }
            }

       }
        sc.InnerHtml = Show_sc(DisIDs);
    }
    protected void PageNavigator1_PageChange(object sender, int PageIndex1)
    {
        Show_cjlist(PageIndex1);
    }
    protected void Show_cjlist(int PageIndex1)
    {
        string DtIDs = Foosun.Common.Input.Filter(Request.QueryString["DtID"].ToString());
        int ib, jb;

        SQLConditionInfo sts = new SQLConditionInfo("@DtIDs", DtIDs);
        DataTable cjlistdts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex1, 20, out ib, out jb, sts);
        this.PageNavigator1.PageCount = jb;
        this.PageNavigator1.PageIndex = PageIndex1;
        this.PageNavigator1.RecordCount = ib;
        if (cjlistdts.Rows.Count > 0)
        {
            cjlistdts.Columns.Add("UserName", typeof(string));
            cjlistdts.Columns.Add("iPoint", typeof(string));
            cjlistdts.Columns.Add("aPoint", typeof(string));
            cjlistdts.Columns.Add("cPoint", typeof(string));
            cjlistdts.Columns.Add("gPoint", typeof(string));
            cjlistdts.Columns.Add("ePoint", typeof(string));
            cjlistdts.Columns.Add("RegTime", typeof(string));
            cjlistdts.Columns.Add("Content", typeof(string));
            cjlistdts.Columns.Add("chars", typeof(string));
            cjlistdts.Columns.Add("infos", typeof(string));
            cjlistdts.Columns.Add("userfaces", typeof(string));
            foreach (DataRow s in cjlistdts.Rows)
            {
                DataTable dt2=dis.sel_46(s["UserNum"].ToString());
                s["chars"] = pd.getUserChar(s["UserNum"].ToString());
                s["infos"] = "<a href=\"../message/message_write.aspx?uid=" + pd.getUserName(s["UserNum"].ToString()) + "\" title=\"给他发短消息\" class=\"list_link\">消息</a>&nbsp;&nbsp;&nbsp;<a href=\"../friend/friend_add.aspx?uid=" + pd.getUserName(s["UserNum"].ToString()) + "\" title=\"加他为好友\" class=\"list_link\">好友</a>";
                int cut = dt2.Rows.Count;
                if (cut != 0)
                {
                    string userflag = pd.getGroupNameFlag(s["UserNum"].ToString());
                    string _Tmpls = dt2.Rows[0]["UserName"].ToString();
                    if (userflag.IndexOf("|") != -1)
                    {
                        string[] userflagARR = userflag.Split('|');
                        _Tmpls = userflagARR[0] + _Tmpls + userflagARR[1];
                    }
                    else
                    {
                        _Tmpls = userflag + _Tmpls;
                    }
                    s["UserName"] = "<a href=\"../../" + Foosun.Config.UIConfig.dirUser + "/ShowUser.aspx?uid=" + dt2.Rows[0]["UserName"].ToString() + "\" target=\"_blank\" class=\"list_link\" title=\"点击查看" + dt2.Rows[0]["UserName"].ToString() + "的资料.\">" + _Tmpls + "</a>";//用户名
                    s["iPoint"] = dt2.Rows[0]["iPoint"].ToString();
                    s["aPoint"] = dt2.Rows[0]["aPoint"].ToString();
                    s["cPoint"] = dt2.Rows[0]["cPoint"].ToString();
                    s["gPoint"] = dt2.Rows[0]["gPoint"].ToString();
                    s["ePoint"] = dt2.Rows[0]["ePoint"].ToString();
                    s["RegTime"] = DateTime.Parse(dt2.Rows[0]["RegTime"].ToString()).ToShortDateString();
                    string _userface = dt2.Rows[0]["UserFace"].ToString();
                    string _userfacesize = "100|100";
                    if (dt2.Rows[0]["userFacesize"].ToString() != null || dt2.Rows[0]["userFacesize"].ToString() != "")
                    {
                        if (dt2.Rows[0]["userFacesize"].ToString().IndexOf("|") == -1)
                        {
                            _userfacesize = "100|100";
                        }
                        else
                        {
                            _userfacesize = dt2.Rows[0]["userFacesize"].ToString();
                        }
                    }
                    string[] tmpsize = _userfacesize.Split('|');
                    string _height = tmpsize[1];
                    string _width = tmpsize[0];
                    s["userfaces"] = "<img src=\"" + _userface.Replace("{@UserdirFile}", Foosun.Config.UIConfig.dirUser) + "\" border=\"0\" style=\"width:" + _width + "px;height:" + _height + ";\" title=\"用户未设置头像\" />";
                }
                else 
                {
                    s["UserName"] = "--";
                    s["iPoint"] = "0";
                    s["aPoint"] = "0";
                    s["cPoint"] = "0";
                    s["gPoint"] = "0";
                    s["ePoint"] = "0";
                    s["RegTime"] = "--";
                    s["userfaces"] = "<img src=\"../../sysImages/user/noHeadpic.gif\" border=\"0\" title=\"用户未设置头像\" />";
                }
                DataTable dt = dis.sel_48(s["DtID"].ToString());
                string _str = "";
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string editTF = "";
                        if ((dt.Rows[0]["UserNum"].ToString()).ToUpper() == (Foosun.Global.Current.UserNum).ToUpper())
                        {
                            editTF = "<a href=\"discussTopi_commentaryEdit.aspx?DtID=" + dt.Rows[0]["DtID"].ToString() + "&DisID=" + Request.QueryString["DisID"] + "\" class=\"list_link\"><img src=\"../../sysImages/folder/re.gif\" border=\"0\"></a>&nbsp;&nbsp;";
                        }
                        if ((dt.Rows[0]["title"].ToString()).Trim() != "")
                        {
                            _str = "<div style=\"width:98%\" align=\"right\">" + editTF + "<span style=\"font-size:10px;\">(" + dt.Rows[0]["creatTime"].ToString() + ")</span>&nbsp;&nbsp;</div><div style=\"height:25px;\"><strong>" + dt.Rows[0]["title"].ToString() + "</strong></div>\r<div>" + dt.Rows[0]["Content"].ToString() + "</div>";
                        }
                        else
                        {
                            _str = "<div style=\"width:98%\" align=\"right\">" + editTF + "<span style=\"font-size:10px;\">(" + dt.Rows[0]["creatTime"].ToString() + ")</span>&nbsp;&nbsp;</div><div>" + dt.Rows[0]["Content"].ToString() + "</div>";
                        }
                    }
                    dt.Clear(); dt.Dispose();
                }
                s["Content"] = _str;
            }
        }
        else 
        {
            no.InnerHtml = Show_no();
            this.PageNavigator1.Visible = false;
        }
        DataList1.DataSource = cjlistdts;
        DataList1.DataBind();
    }

    protected string Show_no()
    {
        string nos = "<table border=0 width=\"98%\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"table\">";
        nos = nos + "<tr class=\"TR_BG_list\">";
        nos = nos + "<td class=\"navi_link\">没有数据</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
    protected string Show_sc(string DidID)
    {
        string sc = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\"><tr><td height=\"1\" colspan=\"2\"></td></tr>";
        sc += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >讨论组主题管理</td><td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" >";
        sc += "<div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussTopi_list.aspx?DisID=" + DidID + "\" class=\"list_link\">讨论组主题管理</a></div></td></tr></table>";
        sc += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\"><tr><td style=\"padding-left:14px;\"><a href=\"discussTopi_list.aspx?DisID=" + DidID + "\" class=\"list_link\">讨论组主题</a>&nbsp;&nbsp;<a href=\"discussTopi_add.aspx?DisID=" + DidID + "\" class=\"menulist\">发表主题</a>&nbsp;&nbsp;<a href=\"discussTopi_ballot.aspx?DisID=" + DidID + "\" class=\"menulist\">发起投票</a>&nbsp;&nbsp;</td></tr></table>";
        return sc;
    }
    protected string Show_cmm()
    {
        string comm = "<td class=\"list_link\" style=\"width: 100px\"><a href=\"#bottom\" style=\"cursor:pointer;font-size:14px;\" class=\"list_link\"><img src=\"../../sysImages/normal/reply.gif\" border=\"0\"></a></td>";
        return comm;
    }
    /// <summary>
    /// 提交回复
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void subset_Click(object sender, EventArgs e)
    {
        string UserNum = Foosun.Global.Current.UserNum;
        string DisIDx = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());
        string DtIDa = Foosun.Common.Input.Filter(Request.QueryString["DtID"].ToString());
        string Titles = "";
        //Titles = Foosun.Common.Input.Filter(Request.Form["titlesd"].ToString());
        string Contentss = Foosun.Common.Input.Htmls(contentBox.Value);
        string DtsID = Foosun.Common.Rand.Number(12);
        DataTable dtd = dis.sel_35();
        int cut = dtd.Rows.Count;
        string DtIDda = "";
        if (cut > 0)
        {
            DtIDda = dtd.Rows[0]["DtID"].ToString();
        }
        DataTable dt_usd = dis.sel_29(DisIDx);
        int cut_um = dt_usd.Rows.Count;
        if (cut_um == 0)
        {
            if (dis.sel_30(DisIDx).ToUpper() == Foosun.Global.Current.UserName.ToUpper())
            {

                if (DtIDda != DtsID)
                {
                    STADDDiscuss uc = new STADDDiscuss();
                    uc.DtID = DtsID;
                    uc.Title = Titles;
                    uc.Content = Contentss;
                    uc.UserNum = UserNum;
                    uc.ParentID = DtIDa;
                    uc.creatTime = DateTime.Now;
                    uc.DisID = DisIDx;
                    dis.Add_9(uc);
                    PageRight("<meta http-equiv=\"refresh\" content=\"5;URL=discussTopi_commentary.aspx?DtID=" + Request.QueryString["DtID"] + "&DisID=" + Request.QueryString["DisID"] + "#btom\" />操作成功.<li><a href=\"discussTopi_commentary.aspx?DtID=" + Request.QueryString["DtID"] + "&DisID=" + Request.QueryString["DisID"] + "\"><font color=\"red\">返回主题</font></a></li><li>5秒后自动返回</li>", "discussTopi_commentary.aspx?DtID=" + Request.QueryString["DtID"] + "&DisID=" + Request.QueryString["DisID"] + "#btom");
                }
                else
                {
                    PageError("发帖错误可能编号重复<br>", "");
                }
            }
            else
            {
                PageError("对不起你不是该组组员不能发贴", "");

            }
        }
        else
        {
            if (DtIDda != DtsID)
            {
                STADDDiscuss uc = new STADDDiscuss();
                uc.DtID = DtsID;
                uc.Title = Titles;
                uc.Content = Contentss;
                uc.UserNum = UserNum;
                uc.ParentID = DtIDa;
                uc.creatTime = DateTime.Now;
                uc.DisID = DisIDx;
                dis.Add_9(uc);
                PageRight("<meta http-equiv=\"refresh\" content=\"5;URL=discussTopi_commentary.aspx?DtID=" + Request.QueryString["DtID"] + "&DisID=" + Request.QueryString["DisID"] + "#btom\" />操作成功.<li><a href=\"discussTopi_commentary.aspx?DtID=" + Request.QueryString["DtID"] + "&DisID=" + Request.QueryString["DisID"] + "\"><font color=\"red\">返回主题</font></a></li><li>5秒后自动返回</li>", "discussTopi_commentary.aspx?DtID=" + Request.QueryString["DtID"] + "&DisID=" + Request.QueryString["DisID"] + "#btom");
            }
            else
            {
                PageError("发帖错误可能编号重复", "");
            }
        }
    }
    ///************************************************************************************************************
    ///**********检查显示投票项********************************************************************************
    ///************************************************************************************************************
    protected string Show_VoteTF(string dtidd1)
    {
        DataTable dt_V = dis.sel_38(dtidd1);
        string helpTempStr ="";
        int Cnt_V = dt_V.Rows.Count;
        string Value = null;
        for (int i = 0; i < Cnt_V; i++)
        {
            string VoteID = dt_V.Rows[i]["VoteID"].ToString();
            string Voteitem = dt_V.Rows[i]["Voteitem"].ToString();
            string VoteNum = dt_V.Rows[i]["VoteNum"].ToString();
            int votegenre = int.Parse(dt_V.Rows[i]["votegenre"].ToString());
            string vg = "";
            if (votegenre == 0)
            {
                vg = "<input id=\"Radio1\" type=\"radio\" value=\"" + VoteID + "\"  name=\"vg\" />";
            }
            else
            {
                vg = "<input name=\"checkbox1\" type=\"checkbox\" value=\"" + VoteID + "\" />";
            }
            Value += "'";
            if (i > 0)
                Value += ",'";
            Value += VoteID;
            if (i == Cnt_V - 1)
                Value += "'";
            helpTempStr = helpTempStr + "<tr class='TR_BG_list'>";
            helpTempStr = helpTempStr + "<td class='navi_link' align='left' width=\"20%\">" + vg + "&nbsp;&nbsp;" + Voteitem + "</td>";
            helpTempStr = helpTempStr + "<td class='navi_link' align='left' width=\"80%\">投票数： &nbsp;&nbsp;" + VoteNum + "</td>";
            helpTempStr = helpTempStr + "</tr>";
        }
        helpTempStr = helpTempStr + "<input id=\"Hidden1\" type=\"hidden\" value=\"" + Value + "\" name=\"hidden\"/>";
        return helpTempStr;
    }

    protected void vot_Click(object sender, EventArgs e)
    {
        string Dtsid = Foosun.Common.Input.Filter(Request.QueryString["DtID"].ToString());
        DateTime nowtime = DateTime.Now;
        DateTime timesy = nowtime;
        DataTable dt1 = dis.sel_38(Dtsid);
        if (dt1.Rows[0]["CreaTime"].ToString() != "" && dt1.Rows[0]["CreaTime"].ToString() != null)
        {
            string s = dt1.Rows[0]["CreaTime"].ToString();

            timesy = DateTime.Parse(dt1.Rows[0]["CreaTime"].ToString());
            TimeSpan st = nowtime - timesy;
            if (st.Days < 1)
            {
                PageError("对不起请在一天后在投票", "");
            }
        }
        int vots = int.Parse(dt1.Rows[0]["votegenre"].ToString());
        DateTime tm=DateTime.Now;
        if (vots == 1)
        {
            string checkboxq = Request.Form["checkbox1"];
            if (checkboxq == null || checkboxq == String.Empty)
            {
                PageError("请先选择要投票的项目!", "");
            }
            else
            {
                string[] chSplit = checkboxq.Split(',');
                string hidd = Request.Form["hidden"];
                for (int i = 0; i < chSplit.Length; i++)
                {
                        int VoteNumsel = int.Parse(dis.sel_39(chSplit[i])) + 1;
                        if (dis.Update_5(VoteNumsel, chSplit[i]) == 0)
                        {
                            PageError("投票失败", "");
                            break;
                        }
                } 
                dis.Update_4(tm,hidd);
                PageRight("投票成功", "");
            }
        }
        else
        {
            string checkboxqr = Request.Form["vg"];
            if (checkboxqr == null || checkboxqr == String.Empty)
            {
                PageError("请先选择要投票的项目!", "");
            }
            else
            {
                string[] chSplitr = checkboxqr.Split(',');
                for (int i = 0; i < chSplitr.Length; i++)
                {
                    int VoteNumselR = int.Parse(dis.sel_39(chSplitr[i])) + 1;
                    if (checkboxqr != null || checkboxqr != String.Empty)
                    {
                        if (dis.Update_5(VoteNumselR, chSplitr[i]) == 0)
                        {
                            PageError("投票失败", "");
                        }
                    }
                    dis.Update_6(tm, chSplitr[i]);
                }
                PageRight("投票成功", "");
            }        
        }
    }
    protected void view_Click(object sender, EventArgs e)
    {
        string DisIDvis = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());
        string Dtsidview = Foosun.Common.Input.Filter(Request.QueryString["DtID"].ToString());
        Response.Redirect("discussTopi_view.aspx?DtID=" + Dtsidview + "&DisID=" + DisIDvis + "");
    }

    protected void DataList1_ItemCommand(object sender, EventArgs e)
    { 
        
    }
}