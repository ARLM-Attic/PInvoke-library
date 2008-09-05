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

public partial class user_discussacti_add : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();
    DateTime CreaTime = DateTime.Now;//获取当前系统时间    
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            string AId=Foosun.Common.Input.Filter(Request.QueryString["AId"].ToString());
            DataTable dt = dis.sel_12(AId);
            DateTime Cutofftime1 = DateTime.Parse(dt.Rows[0]["Cutofftime"].ToString());
            int An = int.Parse(dt.Rows[0]["Anum"].ToString());
            int Pn=0;
            if (dis.sel_13(AId) != 0)
            {
                Pn = dis.sel_13(AId);
            }
            if (CreaTime > Cutofftime1)
            {
                PageError("对不起不能参加活动时间已经过期", "discussacti_list.aspx");

            }
            else
            {
                if (Pn >= An)
                {
                    PageError("对不起参加人数超出不能参加次活动", "discussacti_list.aspx");
                }
            }
        }
    }
    #endregion

    protected void inBox_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断是否通过验证
        {
            string UserNum = Foosun.Global.Current.UserNum;
            string AIds = Foosun.Common.Input.Filter(Request.QueryString["AId"].ToString());
            string Telephone = Foosun.Common.Input.Htmls(Request.Form["TelephoneBox"].ToString());
            int ParticipationNum = int.Parse(Foosun.Common.Input.Filter(Request.Form["ParticipationNumBox"].ToString()));
            int isCompanion = this.isCompanionList.SelectedIndex;
            string  PId = Foosun.Common.Rand.Number(12);//产生12位随机字符
            DataTable dta = dis.sel_14();
            int cutb = dta.Rows.Count;
            string PIda = "";
            if (cutb > 0)
            {
                PIda = dta.Rows[0]["PId"].ToString();
            }
            if (PIda != PId)
            {
                if (dis.Add_4(Telephone, ParticipationNum, isCompanion, UserNum, AIds, PId, CreaTime) != 0)
                {
                    PageRight("加入成功", "discussacti_list.aspx");
                }
                else
                {
                    PageError("加入失败", "discussacti_list.aspx");
                }
            }
            else 
            {
                PageError("加入失败可能编号重复请重新加入", "discussacti_list.aspx");
            }
        }
    }
}                   