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

public partial class user_info_userinfo_update : Foosun.Web.UI.UserPage
{
    UserMisc rd = new UserMisc();
    rootPublic pd = new rootPublic();
    public static string dirDumm = Foosun.Config.UIConfig.dirDumm;
    public static string UserdirFile = Foosun.Config.UIConfig.UserdirFile;
    public static string Rdir = UserdirFile;
    public static string gEmaill = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            
            if (dirDumm.Trim() != "")
            {
                Rdir = Foosun.Config.UIConfig.dirDumm + "/" + UserdirFile;
            }
            copyright.InnerHtml = CopyRight;
            sex.InnerHtml = sexlist();
            marriage.InnerHtml = marriagelist();
            isopen.InnerHtml = isopenlist();

            DataTable dt = rd.getUserInfobase1_user(Foosun.Global.Current.UserNum);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    this.NickName.Text = dt.Rows[0]["NickName"].ToString();
                    if (dt.Rows[0]["birthday"].ToString() != "")
                    {
                        this.birthday.Text = ((DateTime)dt.Rows[0]["birthday"]).ToString("yyyy-MM-dd");
                    }
                    if (pd.getUserUserInfo(Foosun.Global.Current.UserNum) == 1)
                    {
                        this.Userinfo.Text = Foosun.Common.Input.ToTxt(dt.Rows[0]["Userinfo"].ToString());
                    }
                    else
                    {
                        UserInfo_div.Visible = false;
                    }
                    gEmaill = dt.Rows[0]["email"].ToString();
                    this.UserFace.Text = dt.Rows[0]["UserFace"].ToString();
                    this.userFacesize.Text = dt.Rows[0]["userFacesize"].ToString();
                    this.RealName.Text = dt.Rows[0]["RealName"].ToString();


                    string str_userHeadpic = dt.Rows[0]["UserFace"].ToString().Replace("{@userdirfile}", UserdirFile);
                    string str_userHeadSize = dt.Rows[0]["userFacesize"].ToString();
                    string[] arr_userheadSize = str_userHeadSize.Split('|');

                    userFace_div.InnerHtml = "<img id=\"changeFace\" src=\"" + str_userHeadpic + "\" border=\"0\" width=\"" + arr_userheadSize[0].ToString() + "\" height=\"" + arr_userheadSize[1].ToString() + "\" />";
                }
            }

            DataTable dt1 = rd.getUserInfobase2_user(Foosun.Global.Current.UserNum);
            if (dt1 != null)
            {
                if (dt1.Rows.Count > 0)
                {
                    this.Nation.Text = dt1.Rows[0]["Nation"].ToString();
                    this.nativeplace.Text = dt1.Rows[0]["nativeplace"].ToString();
                    this.character.Text = dt1.Rows[0]["character"].ToString();
                    this.orgSch.Text = dt1.Rows[0]["orgSch"].ToString();
                    this.job.Text = dt1.Rows[0]["job"].ToString();
                    this.education.Text = dt1.Rows[0]["education"].ToString();
                    this.Lastschool.Text = dt1.Rows[0]["Lastschool"].ToString();
                    this.UserFan.Text = dt1.Rows[0]["UserFan"].ToString();
                }
            }
        }
    }

    protected string sexlist()
    {
        string liststr = "";
        DataTable dt = rd.sexlist(pd.getUserName_uid(Foosun.Global.Current.UserNum));
        if (dt != null)
        {
            liststr += "<select name=\"sex\">";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["sex"].ToString() == "0")
                {
                    liststr += "<option value=\"0\" selected>保密</option>";
                }
                else
                {
                    liststr += "<option value=\"0\">保密</option>";
                }
                if (dt.Rows[0]["sex"].ToString() == "1")
                {
                    liststr += "<option value=\"1\" selected>男</option>";
                }
                else
                {
                    liststr += "<option value=\"1\">男</option>";
                }
                if (dt.Rows[0]["sex"].ToString() == "2")
                {
                    liststr += "<option value=\"2\" selected>女</option>";
                }
                else
                {
                    liststr += "<option value=\"2\">女</option>";
                }
                liststr += "</select>";
            }
            else
            {
                liststr += "<option value=\"0\" selected>保密</option>";
                liststr += "<option value=\"1\">男</option>";
                liststr += "<option value=\"2\">女</option>";
            }
        }
        return liststr;
    }

    protected string marriagelist()
    {
        string liststr = "";
        DataTable dt = rd.marriagelist(pd.getUserName_uid(Foosun.Global.Current.UserNum));
        if (dt != null)
        {
            liststr += "<select name=\"marriage\">";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["marriage"].ToString() == "0")
                {
                    liststr += "<option value=\"0\" selected>保密</option>";
                }
                else
                {
                    liststr += "<option value=\"0\">保密</option>";
                }
                if (dt.Rows[0]["marriage"].ToString() == "1")
                {
                    liststr += "<option value=\"1\" selected>未婚</option>";
                }
                else
                {
                    liststr += "<option value=\"1\">未婚</option>";
                }
                if (dt.Rows[0]["marriage"].ToString() == "2")
                {
                    liststr += "<option value=\"2\" selected>已婚</option>";
                }
                else
                {
                    liststr += "<option value=\"2\">已婚</option>";
                }
                liststr += "</select>";
            }
            else
            {
                liststr += "<option value=\"0\" selected>保密</option>";
                liststr += "<option value=\"1\">未婚</option>";
                liststr += "<option value=\"2\">已婚</option>";
            }
        }
        return liststr;
    }

    protected string isopenlist()
    {
        string liststr = "";
        DataTable dt = rd.isopenlist(pd.getUserName_uid(Foosun.Global.Current.UserNum));
        if (dt != null)
        {
            liststr += "<select name=\"isopen\">";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["isopen"].ToString() == "0")
                {
                    liststr += "<option value=\"0\" selected>不开放</option>";
                }
                else
                {
                    liststr += "<option value=\"0\">不开放</option>";
                }
                if (dt.Rows[0]["isopen"].ToString() == "1")
                {
                    liststr += "<option value=\"1\" selected>开放</option>";
                }
                else
                {
                    liststr += "<option value=\"1\">开放</option>";
                }
               liststr += "</select>";
            }
            else
            {
                liststr += "<option value=\"1\" selected>开放</option>";
                liststr += "<option value=\"0\">不开放</option>";
            }
        }
        return liststr;
    }

    
    protected void submitSave(object sender, EventArgs e)
    {
        if (Page.IsValid == true)                       //判断是否验证成功
        {

            string NickName = Request.Form["NickName"];
            if (NickName == "")
            {
                PageError("请填写昵称", "userinfo_update.aspx");
            }
            string sex = Request.Form["sex"];
            string birthday = this.birthday.Text;
            string Nation = this.Nation.Text;
            string nativeplace = Foosun.Common.Input.Htmls(this.nativeplace.Text);
            string Userinfo = "";
            if (this.Userinfo.Text != "")
            {
                Userinfo = Foosun.Common.Input.Htmls(this.Userinfo.Text); 
            }
            string UserFace = Foosun.Common.Input.Htmls(this.UserFace.Text);
            string userFacesize = Foosun.Common.Input.Htmls(this.userFacesize.Text);
            string character =  Foosun.Common.Input.Htmls(this.character.Text);
            string UserFan = Foosun.Common.Input.Htmls(this.UserFan.Text);
            string orgSch = Foosun.Common.Input.Htmls(this.orgSch.Text);
            string job = this.job.Text;
            string education = this.education.Text;
            string Lastschool = Foosun.Common.Input.Htmls(this.Lastschool.Text);
            string marriage = Request.Form["marriage"];
            string isopen = Request.Form["isopen"];
            string RealName = Foosun.Common.Input.Htmls(this.RealName.Text);
            string[] userFacesizes = userFacesize.Split('|');
            int uf = 0, uf1 = 0;
            try
            {
                uf = int.Parse(userFacesizes[0].ToString());
                uf1 = int.Parse(userFacesizes[1].ToString());
            }
            catch (Exception ei)
            {
                PageError("头像宽度/或者高度格式不正确。<li>"+ei.ToString()+"</li>", "userinfo_update.aspx");
            }

            if (uf > 120)
            {
                PageError("头像宽度不能超过120px", "userinfo_update.aspx");
            }
            if (uf1 > 120)
            {
                PageError("头像高度不能超过120px", "userinfo_update.aspx");
            }
            DataTable dt = rd.getUserInfoParam(pd.getUserName_uid(Foosun.Global.Current.UserNum));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string lenthstr = dt.Rows[0]["CharLenContent"].ToString();
                    if (lenthstr == null && lenthstr == "")
                    {
                        lenthstr = "300";
                    }
                    if (lenthstr != null)
                    {
                        if (Userinfo.Length > int.Parse(lenthstr))
                        {
                            PageError("您的签名长度大于" + lenthstr + "字符", "userinfo_update.aspx");
                        }
                        if (dt.Rows[0]["CharHTML"].ToString() == "0")
                        {
                            Userinfo = Foosun.Common.Input.ToHtml(Userinfo);
                        }
                    }
                }
                else
                {
                    if (Userinfo.Length > 300)
                    {
                        PageError("您的签名长度大于300字符", "userinfo_update.aspx");
                    }
                }
            }
            else
            {
                if (Userinfo.Length > 200)
                {
                    PageError("您的签名长度大于200字符", "userinfo_update.aspx");
                }
            }
            
            ///更新基本表
            Foosun.Model.UserInfo uc = new Foosun.Model.UserInfo();
            uc.Id = pd.getUserName_uid(Foosun.Global.Current.UserNum);
            uc.NickName = NickName;
            uc.RealName = RealName;
            uc.email = Request.Form["gEmaill"];
            uc.sex = int.Parse(sex);
            uc.birthday = DateTime.Parse(birthday);
            uc.Userinfo = Userinfo;
            uc.UserFace = UserFace;
            uc.userFacesize = userFacesize;
            uc.marriage = int.Parse(marriage);
            uc.isopen = int.Parse(isopen);
            uc.UserGroupNumber = pd.getUserGroupNumber(Foosun.Global.Current.UserNum);

            //同步更新用户信息
            Foosun.PlugIn.Passport.DPO_Request request = new Foosun.PlugIn.Passport.DPO_Request(Context);
            request.Birthday = uc.birthday.ToString("yyyy-MM-dd");
            switch (uc.sex)
            {
                case 2:
                    request.Sex = "0";
                    break;
                case 1:
                    request.Sex = "1";
                    break;
                default:
                    request.Sex = "2";
                    break;
            }
            request.TrueName = uc.RealName;
            request.UserName = Foosun.Global.Current.UserName;
            request.ProcessMultiPing("update");

            if (request.FoundErr)
            {
                PageError("同步更新用户信息失败", "userinfo_update.aspx");
            }
            rd.UpdateUserInfoBase(uc);

            //获得UserID


            DataTable getdt = rd.getUserInfoNum(pd.getUserName_uid(Foosun.Global.Current.UserNum));
            string strUsernum = "";
            if (getdt != null)
            {
                if (getdt.Rows.Count > 0)
                {
                    strUsernum = getdt.Rows[0]["userNum"].ToString();
                }
                getdt.Clear();
                getdt.Dispose();
            }

            //获取记录
            DataTable sdt = rd.getUserInfoRecord(strUsernum);
            if (sdt != null)
            {
                if (sdt.Rows.Count > 0)
                {
                    Foosun.Model.UserInfo1 uc1 = new Foosun.Model.UserInfo1();
                    uc1.UserNum = strUsernum;
                    uc1.Nation = Nation;
                    uc1.nativeplace = nativeplace;
                    uc1.character = character;
                    uc1.UserFan = UserFan;
                    uc1.orgSch = orgSch;
                    uc1.job = job;
                    uc1.education = education;
                    uc1.Lastschool = Lastschool;
                    rd.UpdateUserInfoBase1(uc1);
                }
                else
                {
                    Foosun.Model.UserInfo1 uc1 = new Foosun.Model.UserInfo1();
                    uc1.UserNum = strUsernum;
                    uc1.Nation = Nation;
                    uc1.nativeplace = nativeplace;
                    uc1.character = character;
                    uc1.UserFan = UserFan;
                    uc1.orgSch = orgSch;
                    uc1.job = job;
                    uc1.education = education;
                    uc1.Lastschool = Lastschool;
                    rd.UpdateUserInfoBase2(uc1);
                }
                sdt.Clear();
                sdt.Dispose();
            }
            else
            {
                Foosun.Model.UserInfo1 uc1 = new Foosun.Model.UserInfo1();
                uc1.UserNum = strUsernum;
                uc1.Nation = Nation;
                uc1.nativeplace = nativeplace;
                uc1.character = character;
                uc1.UserFan = UserFan;
                uc1.orgSch = orgSch;
                uc1.job = job;
                uc1.education = education;
                uc1.Lastschool = Lastschool;
                rd.UpdateUserInfoBase1(uc1);
            }
            PageRight("修改基本资料成功！", "userinfo_update.aspx");
        }
    }

}
