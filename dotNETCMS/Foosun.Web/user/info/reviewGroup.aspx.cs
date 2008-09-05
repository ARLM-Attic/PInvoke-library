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

public partial class user_info_reviewGroup : Foosun.Web.UI.UserPage
{
    UserMisc rd = new UserMisc();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        copyright.InnerHtml = CopyRight;
        if (Request.QueryString["UserGroupNumber"].Trim() != null || Request.QueryString["UserGroupNumber"].Trim() != "")
        {
            string UserGroupNumber = "";
            UserGroupNumber = Foosun.Common.Input.Filter(Request.QueryString["UserGroupNumber"].Trim());
            DataTable dt = rd.getGroupEdit(int.Parse(pd.getIDGroupNumber(UserGroupNumber)));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    #region 为表单赋值
                    this.GroupName.Text = dt.Rows[0]["GroupName"].ToString();
                    this.iPoint.Text = dt.Rows[0]["iPoint"].ToString();
                    this.gPoint.Text = dt.Rows[0]["gPoint"].ToString();
                    this.Rtime.Text = dt.Rows[0]["Rtime"].ToString();
                    this.Discount.Text = dt.Rows[0]["Discount"].ToString();
                    this.LenCommContent.Text = dt.Rows[0]["LenCommContent"].ToString();
                    int n = int.Parse(dt.Rows[0]["CommCheckTF"].ToString());
                    if (n == 0)
                        this.CommCheckTF.Items[1].Selected = true;
                    else
                        this.CommCheckTF.Items[0].Selected = true;

                    this.PostCommTime.Text = dt.Rows[0]["PostCommTime"].ToString();
                    this.upfileType.Text = dt.Rows[0]["upfileType"].ToString();
                    this.upfileNum.Text = dt.Rows[0]["upfileNum"].ToString();
                    this.upfileSize.Text = dt.Rows[0]["upfileSize"].ToString();
                    this.DayUpfilenum.Text = dt.Rows[0]["DayUpfilenum"].ToString();
                    this.ContrNum.Text = dt.Rows[0]["ContrNum"].ToString();

                    if (int.Parse(dt.Rows[0]["DicussTF"].ToString()) == 0)
                        this.DicussTF.Items[1].Selected = true;
                    else
                        this.DicussTF.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["PostTitle"].ToString()) == 0)
                        this.PostTitle.Items[1].Selected = true;
                    else
                        this.PostTitle.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["ReadUser"].ToString()) == 0)
                        this.ReadUser.Items[1].Selected = true;
                    else
                        this.ReadUser.Items[0].Selected = true;

                    this.MessageNum.Text = dt.Rows[0]["MessageNum"].ToString();

                    this.MessageGroupNum.Text = dt.Rows[0]["MessageGroupNum"].ToString();

                    if (int.Parse(dt.Rows[0]["IsCert"].ToString()) == 0)
                        this.IsCert.Items[1].Selected = true;
                    else
                        this.IsCert.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["CharTF"].ToString()) == 0)
                        this.CharTF.Items[1].Selected = true;
                    else
                        this.CharTF.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["CharHTML"].ToString()) == 0)
                        this.CharHTML.Items[1].Selected = true;
                    else
                        this.CharHTML.Items[0].Selected = true;
                    this.CharLenContent.Text = dt.Rows[0]["CharLenContent"].ToString();
                    this.RegMinute.Text = dt.Rows[0]["RegMinute"].ToString();

                    if (int.Parse(dt.Rows[0]["PostTitleHTML"].ToString()) == 0)
                        this.PostTitleHTML.Items[1].Selected = true;
                    else
                        this.PostTitleHTML.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["DelSelfTitle"].ToString()) == 0)
                        this.DelSelfTitle.Items[1].Selected = true;
                    else
                        this.DelSelfTitle.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["DelOTitle"].ToString()) == 0)
                        this.DelOTitle.Items[1].Selected = true;
                    else
                        this.DelOTitle.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["EditSelfTitle"].ToString()) == 0)
                        this.EditSelfTitle.Items[1].Selected = true;
                    else
                        this.EditSelfTitle.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["EditOtitle"].ToString()) == 0)
                        this.EditOtitle.Items[1].Selected = true;
                    else
                        this.EditOtitle.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["ReadTitle"].ToString()) == 0)
                        this.ReadTitle.Items[1].Selected = true;
                    else
                        this.ReadTitle.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["MoveSelfTitle"].ToString()) == 0)
                        this.MoveSelfTitle.Items[1].Selected = true;
                    else
                        this.MoveSelfTitle.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["MoveOTitle"].ToString()) == 0)
                        this.MoveOTitle.Items[1].Selected = true;
                    else
                        this.MoveOTitle.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["TopTitle"].ToString()) == 0)
                        this.TopTitle.Items[1].Selected = true;
                    else
                        this.TopTitle.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["GoodTitle"].ToString()) == 0)
                        this.GoodTitle.Items[1].Selected = true;
                    else
                        this.GoodTitle.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["LockUser"].ToString()) == 0)
                        this.LockUser.Items[1].Selected = true;
                    else
                        this.LockUser.Items[0].Selected = true;

                    this.UserFlag.Text = dt.Rows[0]["UserFlag"].ToString();

                    if (int.Parse(dt.Rows[0]["CheckTtile"].ToString()) == 0)
                        this.CheckTtile.Items[1].Selected = true;
                    else
                        this.CheckTtile.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["IPTF"].ToString()) == 0)
                        this.IPTF.Items[1].Selected = true;
                    else
                        this.IPTF.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["EncUser"].ToString()) == 0)
                        this.EncUser.Items[1].Selected = true;
                    else
                        this.EncUser.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["OCTF"].ToString()) == 0)
                        this.OCTF.Items[1].Selected = true;
                    else
                        this.OCTF.Items[0].Selected = true;

                    if (int.Parse(dt.Rows[0]["StyleTF"].ToString()) == 0)
                        this.StyleTF.Items[1].Selected = true;
                    else
                        this.StyleTF.Items[0].Selected = true;

                    this.UpfaceSize.Text = dt.Rows[0]["UpfaceSize"].ToString();
                    this.GIChange.Text = dt.Rows[0]["GIChange"].ToString();
                    this.GTChageRate.Text = dt.Rows[0]["GTChageRate"].ToString();
                    this.LoginPoint.Text = dt.Rows[0]["LoginPoint"].ToString();
                    this.RegPoint.Text = dt.Rows[0]["RegPoint"].ToString();


                    if (int.Parse(dt.Rows[0]["GroupTF"].ToString()) == 0)
                        this.GroupTF.Items[1].Selected = true;
                    else
                        this.GroupTF.Items[0].Selected = true;

                    this.GroupSize.Text = dt.Rows[0]["GroupSize"].ToString();
                    this.GroupPerNum.Text = dt.Rows[0]["GroupPerNum"].ToString();
                    this.GroupCreatNum.Text = dt.Rows[0]["GroupCreatNum"].ToString();
                    #endregion 为表单赋值
                }
            }
        }
    }
}
