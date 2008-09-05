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

public partial class user_userinfo : Foosun.Web.UI.UserPage
{
    UserMisc rd = new UserMisc();
    rootPublic pd = new rootPublic();
    public string type = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        copyright.InnerHtml = CopyRight;
        string u_sql = Request.QueryString["UserNum"];
        string strUserNum = "";
        if (u_sql != "" && u_sql != null){strUserNum = Foosun.Common.Input.Filter(u_sql.ToString());}
        else{strUserNum = Foosun.Global.Current.UserNum;}
        DataTable dt = rd.getUserUserNumRecord(strUserNum);
        string geGroup=rd.getUserGChange(dt.Rows[0]["UserGroupNumber"].ToString());
        if (geGroup == string.Empty)
        {
            geGroup = "0|0";
        }
        string[] GIChange = geGroup.Split('|');
        reviewGroup.InnerHtml = "&nbsp;(<a class=\"list_link\" href=\"reviewGroup.aspx?UserGroupNumber=" + dt.Rows[0]["UserGroupNumber"].ToString() + "\" title=\"查看我能做什么!\">查看权限</a>&nbsp;&nbsp;<a href=\"UpdateGroup.aspx?UserGroupNumber=" + dt.Rows[0]["UserGroupNumber"].ToString() + "\" title=\"升级为高级会员组\"><font color=\"red\">我要升级</font></a>)";
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                #region 基本资料--------------------------------------
                string userflag = pd.getGroupNameFlag(Foosun.Global.Current.UserNum);
                string _Tmpls = dt.Rows[0]["UserName"].ToString();
                Reviewmyfinfo.InnerHtml = "<a href=\"../ShowUser.aspx?uid=" + _Tmpls + "\" target=\"_blank\"><font color=\"red\">预览我的资料</a>";
                if (userflag.IndexOf("|") != -1)
                {
                    string[] userflagARR = userflag.Split('|');
                    _Tmpls = userflagARR[0] + _Tmpls + userflagARR[1];
                }
                else
                { 
                   _Tmpls =  userflag + _Tmpls;
                }
                this.UserNamex.Text = _Tmpls; //用户名
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
                    this.UserFacex.ImageUrl = dt.Rows[0]["UserFace"].ToString().Replace("{@userdirfile}", Foosun.Config.UIConfig.UserdirFile);//头像 
                    string str_userHeadSize = dt.Rows[0]["userFacesize"].ToString();
                    string[] arr_userheadSize = str_userHeadSize.Split('|');

                    this.UserFacex.Width = Convert.ToInt32(arr_userheadSize[0].ToString());
                    this.UserFacex.Height = Convert.ToInt32(arr_userheadSize[1].ToString());
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
                string G1 = "";
                string G2 = "";
                if (GIChange[0] == "1")
                {
                    G1 = "&nbsp;<a href=\"Exchange.aspx?types=I\"><font color=\"red\">兑换成金币</font></a>";//积分
                }
                if (GIChange[1] == "1")
                {
                    G2 = "&nbsp;<a href=\"Exchange.aspx?types=G\"><font color=\"red\">兑换成积分</font></a>";//积分
                }
                this.iPointx.Text = dt.Rows[0]["iPoint"].ToString() + G1;//积分
                this.gPointx.Text = dt.Rows[0]["gPoint"].ToString() + "&nbsp;" + pd.getgPointName() + G2;//金币

                this.Emailx.Text = dt.Rows[0]["Email"].ToString();  
                this.cPointx.Text = dt.Rows[0]["cPoint"].ToString();//魅力值
                this.ePointx.Text = dt.Rows[0]["ePoint"].ToString();//人气值
                this.aPointx.Text = dt.Rows[0]["aPoint"].ToString();//活跃值
                this.RegTimex.Text = dt.Rows[0]["RegTime"].ToString();//注册日期
                this.OnlineTimex.Text = dt.Rows[0]["OnlineTime"].ToString();//用户在线时间
                this.LoginNumberx.Text = dt.Rows[0]["LoginNumber"].ToString();//用户登陆次数
                //-----------------------联系方式--------------------------------------
                string _tmpMobie = "";
                if (dt.Rows[0]["BindTF"].ToString() == "1")
                {
                    _tmpMobie = "<a href=\"getMobile.aspx?MobileNum=" + dt.Rows[0]["Mobile"].ToString() + "\" class=\"list_link\">(已捆绑)</a>";
                }
                else
                {
                    _tmpMobie = "<a href=\"getMobile.aspx?MobileNum=" + dt.Rows[0]["Mobile"].ToString() + "\" class=\"list_link\">(捆绑/修改手机)</a>";
                }
                this.Mobilex.Text = dt.Rows[0]["Mobile"].ToString() + " " + _tmpMobie + " ";	//手机
                //-------------------------------------------------------------
                dt.Clear();
                dt.Dispose();
            }
        }
        DataTable dts = rd.getUserUserfields(strUserNum);
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
     }


    protected string getlevels(int ipoint)
    {
        string _Str = "";
        DataTable dt  = rd.getleves();
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ipoint>=int.Parse(dt.Rows[i]["iPoint"].ToString()))
                {
                    _Str = "&nbsp;&nbsp;等级:&nbsp;<img src=\"../../sysImages/face/" + dt.Rows[i]["Lpicurl"].ToString() + "\" border=\"0\" alt=\"" + dt.Rows[i]["LTitle"].ToString() + "\" />" + dt.Rows[i]["LTitle"].ToString() + "";
                }
            }
            dt.Clear(); dt.Dispose();
        }
        return _Str;
    }
}
