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

public partial class user_friend_add : Foosun.Web.UI.UserPage
{
    Friend fri = new Friend();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            
            Response.CacheControl = "no-cache";
            string UserNum = Foosun.Global.Current.UserNum;
            this.usernameBox.Text = Request.QueryString["uid"];
            DataTable Q_dfriend = fri.sel_1(UserNum);
            this.friendmanageList.DataTextField = "FriendName";
            this.friendmanageList.DataValueField = "HailFellow";
            this.friendmanageList.DataSource = Q_dfriend;
            this.friendmanageList.DataBind();

            //------------------------好友分类编号------------------------

            string HailFellow = Request.QueryString["FCID"];   
            for (int i = 1; i < this.friendmanageList.Items.Count; i++)
            {
                if (this.friendmanageList.Items[i].Value == HailFellow)
                {
                    this.friendmanageList.Items[i].Selected = true;
                }
            }
            //-------------------------------------------------------------           
        }
    }


    protected void addfriend_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string UserNum = Foosun.Global.Current.UserNum;

            //查询自己的用户名
            string qUserName = fri.sel_2(UserNum);

            //获取请求信息
            string Contentx = Foosun.Common.Input.Filter(Request.Form["AddfriendContent"]);

            //好友分类编号
            string Hail_Fellow = this.friendmanageList.SelectedValue;

            //现在时间
            DateTime CreatTime = DateTime.Now;

            //获取被添加的好友用户名
            string bUserName = Foosun.Common.Input.Filter(Request.Form["usernameBox"].ToString());
            if (bUserName == qUserName)
            {
                PageError("对不起自己不能添加自己为好友", "friendList.aspx");
            }
            //判断被添加的会员是否已经被加为好友
            int bselect = fri.sel_3(UserNum, bUserName);

            //判断被添加的会员是否在会员库中存在
            int cut = fri.sel_4(bUserName);

            if (bselect == 0)//当会员没有被加为好友时
            {
                if (cut > 0)//当会员在会员库中存在时
                {
                    //-------查询要添加好友的添加好友权限-------------------------
                    DataTable dt = fri.sel_5(bUserName);
                    int Addfriendbs = int.Parse(dt.Rows[0]["Addfriendbs"].ToString());
                    string bdUserName = dt.Rows[0]["UserNum"].ToString();

                    Foosun.Model.STRequestinformation Req;
                    Req.qUsername = qUserName;
                    Req.bUsername = bUserName;
                    Req.Content = Contentx;

                    Foosun.Model.STFriend Fri;
                    Fri.UserName = bUserName;
                    Fri.bUserNum = bdUserName;
                    Fri.HailFellow = Hail_Fellow;


                    if (Addfriendbs == 0)
                    {
                        PageError("对方拒绝加为好友", "friend_add.aspx");
                    }
                    else if (Addfriendbs == 1)
                    {
                        if (Contentx == "")//判断是否填写了请求信息
                        {
                            PageError("添加失败，对方需要验证请输入验证信息", "friendList.aspx");
                        }
                        else
                        {
                            if ((fri.Add_2(Fri, UserNum)==0) || (fri.Add_1(Req)==0))
                            {
                                PageError("添加失败", "");
                            }
                            else
                            {
                                PageRight("添加成功,请等待对方验证", "friendList.aspx");
                            }
                        }
                    }

                    else
                    {
                        if ((fri.Add_4(Fri, UserNum) == 0) || (fri.Add_3(Req) == 0))
                        {
                            PageError("添加失败<br>", "");
                        }
                        else
                        {
                            PageRight("添加成功", "friendList.aspx");
                        }
                    }

                }
                else
                {
                    PageError("所添加的好友不存在", "friendList.aspx");
                }
            }
            else 
            {
                PageError("你已经将该会员加为自己的好友了", "friendList.aspx");
            }
        }
    }

    protected void remove_Click(object sender, EventArgs e)
    {
        this.usernameBox.Text = "";
        this.AddfriendContent.Text = "";
    }
}
