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

public partial class user_discuss_Manageadd : Foosun.Web.UI.UserPage
{
    //连接数据库
    Discuss dis = new Discuss();
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserNum = Foosun.Global.Current.UserNum;
        //验证用户是否登录
        

        //讨论组编号
        string DisID=Foosun.Common.Input.Filter(Request.QueryString["DisID"]);

        //会员自己的基金       
        DataTable selectig = dis.sel_7(UserNum);
        int iPoint = int.Parse(selectig.Rows[0]["iPoint"].ToString());
        int gPoint = int.Parse(selectig.Rows[0]["gPoint"].ToString());
        string[] Authority = dis.sel_8(DisID).Split('|');
        int Authority1 = int.Parse(Authority[0].ToString());
        int Authority2 = int.Parse(Authority[1].ToString());
        int Authority3 = int.Parse(Authority[2].ToString());

        //产生12位随机字符
        string GhID = Foosun.Common.Rand.Number(12);
        string Member = Foosun.Common.Rand.Number(12);
        //当前系统时间
        DateTime Creatime = DateTime.Now;

        //讨论组原有基金
        DataTable selFundwarehouse1 = dis.sel_9(DisID);
        string[] Fund = selFundwarehouse1.Rows[0]["Fundwarehouse"].ToString().Split('|');
        int iPoint2 = int.Parse(Fund[0].ToString());
        int gPoint2 = int.Parse(Fund[1].ToString());
        int iPoint3 = iPoint2 + Authority2;
        int gPoint3 = gPoint2 + Authority3;
        string Fundwarehouse1 = iPoint3 + " | " + gPoint3;
        int ct = dis.sel_10(UserNum,DisID);
        DataTable dta = dis.sel_11();
        int cutb = dta.Rows.Count;
        string Memberss = "";
        if (cutb > 0)
        {
            Memberss = dta.Rows[0]["Member"].ToString();
        }
        if (Memberss != Member)
        {

            if (Authority1 == 0)
            {
                if (ct > 0)
                {
                    PageError("您已经加入此组", "discussManage_list.aspx");
                }
                else
                {
                    if (dis.Add_3(Member, DisID, UserNum, Creatime) == 0)
                    {
                        PageError("加入失败.<li>可能原因：你已经加入此组</li>", "discussManage_list.aspx");
                    }
                    else
                    {
                        PageRight("加入成功", "discussManage_list.aspx");
                    }
                }

            }
            else if (Authority1 == 2)
            {
                if (iPoint <= Authority2 || gPoint <= Authority3)
                {
                    Response.Write("<script language=\"javascript\" type=\"text/javascript\">alert(\"您的资金不足不能加入\");</script>");
                }
                else
                {
                    if (ct > 0 )
                    {
                        PageError("你已经加入这个组了<br>", "discussManage_list.aspx");
                    }
                    else
                    {
                        if (dis.Add_3(Member, DisID, UserNum, Creatime) == 0 || dis.Update(Authority2, Authority3, UserNum) == 0 || dis.Update_1(Fundwarehouse1, DisID) == 0 || dis.Add_2(GhID,UserNum,Authority3,Authority2,Creatime) == 0)
                        {
                            PageError("加入失败.<li>可能原因：你已经加入这个组</li>", "discussManage_list.aspx");
                        }
                        else
                        {
                            PageRight("加入成功", "discussManage_list.aspx");
                        }
                    }
                }
            }
        }
        else 
        {
            PageError("对不起加入失败可能编号重复", "discussManage_list.aspx");
        }
     }
    #endregion
}