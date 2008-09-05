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
using Foosun.Common;

public partial class manage_user_usergroupadd : Foosun.Web.UI.ManagePage
{
    public manage_user_usergroupadd()
    {
        Authority_Code = "U012";
    }
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
        }
    }

    protected void buttonsave(object sender, EventArgs e)
    {
        if (Page.IsValid == true)                       //判断是否验证成功
        {
            if (this.GroupName.Text.Trim() != "")
            {
                #region 获取表单值
                string TxtGroupName = this.GroupName.Text;
                int intiPoint = 0,intgPoint = 0,intLenCommContent = 0,intCommCheckTF = 0,intPostCommTime = 0,intupfileNum = 0, intupfileSize = 0, intDayUpfilenum = 0, intContrNum = 0, intDicussTF = 0, intPostTitle = 0, intReadUser = 0, intMessageNum = 0,  intIsCert = 0, intCharTF = 0, intCharHTML = 0, intCharLenContent = 0, intRegMinute = 0, intPostTitleHTML = 0, intDelSelfTitle = 0, intDelOTitle = 0, intEditSelfTitle = 0, intEditOtitle = 0, intReadTitle = 0, intMoveSelfTitle = 0, intMoveOTitle = 0, intTopTitle = 0, intGoodTitle = 0, intLockUser = 0,  intGroupTF = 0, intGroupSize = 0, intGroupPerNum = 0, intGroupCreatNum = 0, intCheckTtile = 0, intIPTF = 0, intEncUser = 0, intOCTF = 0, intStyleTF = 0, intUpfaceSize = 0,intRtime =0;
                double Discount = 0;
                string TxtupfileType = this.upfileType.Text;
                string TxtUserFlag = this.UserFlag.Text;
                string TxtGTChageRate = this.GTChageRate.Text;
                string TxtMessageGroupNum = this.MessageGroupNum.Text;
                string TxtLoginPoint = this.LoginPoint.Text;
                string TxtRegPoint = this.RegPoint.Text;
                string TxtGIChange = this.GIChange.Text;
                if (TxtGIChange.IndexOf("|") == -1 || TxtGTChageRate.IndexOf("|") == -1 || TxtLoginPoint.IndexOf("|") == -1 || TxtRegPoint.IndexOf("|") == -1)
                {
                    PageError("填写有错误,部分表单需要用|分开", "");
                }
                try
                {
                    intiPoint = int.Parse(this.iPoint.Text);
                    intgPoint = int.Parse(this.gPoint.Text);
                    intRtime = int.Parse(this.Rtime.Text);
                    intLenCommContent = int.Parse(this.LenCommContent.Text);
                    intCommCheckTF = int.Parse(this.CommCheckTF.SelectedValue);
                    intPostCommTime = int.Parse(this.PostCommTime.Text);
                    intupfileNum = int.Parse(this.upfileNum.Text);
                    intupfileSize = int.Parse(this.upfileSize.Text);
                    intDayUpfilenum = int.Parse(this.DayUpfilenum.Text);
                    intContrNum = int.Parse(this.ContrNum.Text);
                    intDicussTF = int.Parse(this.DicussTF.SelectedValue);
                    intPostTitle = int.Parse(this.PostTitle.SelectedValue);
                    intReadUser = int.Parse(this.ReadUser.SelectedValue);
                    intMessageNum = int.Parse(this.MessageNum.Text);
                    intIsCert = int.Parse(this.IsCert.SelectedValue);
                    intCharTF = int.Parse(this.CharTF.SelectedValue);
                    intCharHTML = int.Parse(this.CharHTML.SelectedValue);
                    intCharLenContent = int.Parse(this.CharLenContent.Text);
                    intRegMinute = int.Parse(this.RegMinute.Text);
                    intPostTitleHTML = int.Parse(this.PostTitleHTML.SelectedValue);
                    intDelSelfTitle = int.Parse(this.DelSelfTitle.SelectedValue);
                    intDelOTitle = int.Parse(this.DelOTitle.SelectedValue);
                    intEditSelfTitle = int.Parse(this.EditSelfTitle.SelectedValue);
                    intEditOtitle = int.Parse(this.EditOtitle.SelectedValue);
                    intReadTitle = int.Parse(this.ReadTitle.SelectedValue);
                    intMoveSelfTitle = int.Parse(this.MoveSelfTitle.SelectedValue);
                    intMoveOTitle = int.Parse(this.MoveOTitle.SelectedValue);
                    intTopTitle = int.Parse(this.TopTitle.SelectedValue);
                    intGoodTitle = int.Parse(this.GoodTitle.SelectedValue);
                    intLockUser = int.Parse(this.LockUser.SelectedValue);
                    intCheckTtile = int.Parse(this.CheckTtile.SelectedValue);
                    intIPTF = int.Parse(this.IPTF.SelectedValue);
                    intEncUser = int.Parse(this.EncUser.SelectedValue);
                    intOCTF = int.Parse(this.OCTF.SelectedValue);
                    intStyleTF = int.Parse(this.StyleTF.SelectedValue);
                    intUpfaceSize = int.Parse(this.UpfaceSize.Text);
                    intGroupTF = int.Parse(this.GroupTF.SelectedValue);
                    intGroupSize = int.Parse(this.GroupSize.Text);
                    intGroupPerNum = int.Parse(this.GroupPerNum.Text);
                    intGroupCreatNum = int.Parse(this.GroupCreatNum.Text);
                    Discount = double.Parse(this.Discount.Text);
                }
                catch(Exception getvalue)
                {
                    PageError("请正确填写数字类型<li>" + getvalue.ToString() + "</li>", "");
                }
                #endregion 获取表单值

                #region 插入数据库
                string ramGroupNumber;
                ramGroupNumber = Rand.Number(12);//产生12位随机字符
                DataTable udt = rd.GetGroupNumber(ramGroupNumber);
                if (udt != null)
                {
                    if (udt.Rows.Count > 0)
                    {
                        PageError("编号意外重复", "");
                    }
                    udt.Clear(); udt.Dispose();
                }
                DateTime dateNowstr = System.DateTime.Now;
                string SessionSiteID = Foosun.Global.Current.SiteID;

                Foosun.Model.UserInfo4 uc1 = new Foosun.Model.UserInfo4();
                uc1.SiteID=SessionSiteID;
                uc1.GroupNumber=ramGroupNumber;
                uc1.GroupName=TxtGroupName;
                uc1.iPoint=intiPoint;
                uc1.Gpoint= intgPoint;
                uc1.Rtime=intRtime;
                uc1.LenCommContent=intLenCommContent;
                uc1.CommCheckTF=intCommCheckTF;
                uc1.PostCommTime=intPostCommTime;
                uc1.upfileType=TxtupfileType;
                uc1.upfileNum=intupfileNum;
                uc1.upfileSize=intupfileSize;
                uc1.DayUpfilenum=intDayUpfilenum;
                uc1.ContrNum=intContrNum;
                uc1.DicussTF=intDicussTF;
                uc1.PostTitle=intPostTitle;
                uc1.ReadUser=intReadUser;
                uc1.MessageNum=intMessageNum;
                uc1.MessageGroupNum=TxtMessageGroupNum;
                uc1.IsCert=intIsCert;
                uc1.CharTF=intCharTF;
                uc1.CharHTML=intCharHTML;
                uc1.CharLenContent=intCharLenContent;
                uc1.RegMinute=intRegMinute;
                uc1.PostTitleHTML=intPostTitleHTML;
                uc1.DelSelfTitle=intDelSelfTitle;
                uc1.DelOTitle=intDelOTitle;
                uc1.EditSelfTitle=intEditSelfTitle;
                uc1.EditOtitle=intEditOtitle;
                uc1.ReadTitle=intReadTitle;
                uc1.MoveSelfTitle=intMoveSelfTitle;
                uc1.MoveOTitle=intMoveOTitle;
                uc1.TopTitle=intTopTitle;
                uc1.GoodTitle=intGoodTitle;
                uc1.LockUser=intLockUser;
                uc1.UserFlag=TxtUserFlag;
                uc1.CheckTtile=intCheckTtile;
                uc1.IPTF=intIPTF;
                uc1.EncUser=intEncUser;
                uc1.OCTF = intOCTF;
                uc1.StyleTF = intStyleTF;
                uc1.UpfaceSize = intUpfaceSize;
                uc1.GIChange = TxtGIChange;
                uc1.GTChageRate = TxtGTChageRate;
                uc1.LoginPoint = TxtLoginPoint;
                uc1.RegPoint = TxtRegPoint; ;
                uc1.GroupTF = intGroupTF;
                uc1.GroupSize = intGroupSize;
                uc1.GroupPerNum = intGroupPerNum;
                uc1.GroupCreatNum = intGroupCreatNum;
                uc1.CreatTime = dateNowstr;
                uc1.Discount = Discount;
                rd.InsertGroup(uc1);
                PageRight("创建会员组成功。", "usergroup.aspx");
                #endregion 插入数据库
            }
            else
            {
                PageError("请填写会员组名称", "usergroupadd.aspx");
            }

        }
    }
}
