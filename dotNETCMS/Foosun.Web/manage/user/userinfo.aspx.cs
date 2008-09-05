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

public partial class manage_user_userinfo : Foosun.Web.UI.ManagePage
{
    public manage_user_userinfo()
    {
        Authority_Code = "U003";
    }
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {

            copyright.InnerHtml = CopyRight;
            string uids = Foosun.Common.Input.Filter(Request.QueryString["id"]);
            int uid = 0;
            try
            {
                uid = int.Parse(uids);
            }
            catch (Exception UX)
            {
                PageError("错误的参数.<li>" + UX + "</li>", "");
            }
            suid.Value = Request.QueryString["id"];
            sex.InnerHtml = sexlist();
            marriage.InnerHtml = marriagelist();
            isopen.InnerHtml = isopenlist();
            string strUserNum = "";
            DataTable dt = rd.getUserInfobase1(uid);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    strUserNum = dt.Rows[0]["UserNum"].ToString();
                    NickName.Text = dt.Rows[0]["NickName"].ToString();
                    string birthdays = dt.Rows[0]["birthday"].ToString();
                    if (birthdays != "" || birthdays != null || birthdays == string.Empty)
                    {
                        try
                        {
                            birthday.Text = ((DateTime)dt.Rows[0]["birthday"]).ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            birthday.Text = "1900-1-1";
                        }
                    }

                    this.RealName.Text = dt.Rows[0]["RealName"].ToString();
                    this.Userinfo.Text = Foosun.Common.Input.ToTxt(dt.Rows[0]["Userinfo"].ToString());
                    this.UserFace.Text = dt.Rows[0]["UserFace"].ToString();
                    this.userFacesize.Text = dt.Rows[0]["userFacesize"].ToString();
                    this.email.Text = dt.Rows[0]["email"].ToString();
                    string UserGroupNumber = "<select name=\"UserGroupNumber\">\r";
                    rootPublic pd = new rootPublic();
                    IDataReader dr = pd.GetGroupList();
                    while (dr.Read())
                    {
                        if (dt.Rows[0]["UserGroupNumber"].ToString() == dr["GroupNumber"].ToString())
                        {
                            UserGroupNumber += "<option selected value=\"" + dr["GroupNumber"].ToString() + "\">" + dr["GroupName"].ToString() + "</option>\r";
                        }
                        else
                        {
                            UserGroupNumber += "<option value=\"" + dr["GroupNumber"].ToString() + "\">" + dr["GroupName"].ToString() + "</option>\r";
                        }
                    }
                    dr.Close();
                    UserGroupNumber += "</select>\r";
                    GroupNumber.InnerHtml = UserGroupNumber;
                }
            }

            DataTable dts = rd.getUserInfobase2(strUserNum);
            if (dts != null)
            {
                if (dts.Rows.Count > 0)
                {
                    this.job.Text = dts.Rows[0]["Job"].ToString();//职业
                    //------------------------详细资料-------------------------------------
                    this.Nation.Text = dts.Rows[0]["Nation"].ToString();//民族
                    this.orgSch.Text = dts.Rows[0]["orgSch"].ToString();//组织关系
                    this.character.Text = dts.Rows[0]["character"].ToString();//性格
                    this.UserFan.Text = dts.Rows[0]["UserFan"].ToString();//用户爱好
                    this.education.Text = dts.Rows[0]["education"].ToString();//学历
                    this.Lastschool.Text = dts.Rows[0]["Lastschool"].ToString();//毕业学校
                    this.nativeplace.Text = dts.Rows[0]["nativeplace"].ToString();
                }
                dts.Clear();
                dts.Dispose();
            }
        }
    }
    string sexlist()
    {
        string _Str = "";
        DataTable dt = rd.sexlist(int.Parse(Foosun.Common.Input.Filter(Request.QueryString["id"])));
        if (dt != null)
        {
            _Str += "<select name=\"sex\"  Class=\"form\">";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["sex"].ToString() == "0")
                {
                    _Str += "<option value=\"0\" selected>保密</option>";
                }
                else
                {
                    _Str += "<option value=\"0\">保密</option>";
                }
                if (dt.Rows[0]["sex"].ToString() == "1")
                {
                    _Str += "<option value=\"1\" selected>男</option>";
                }
                else
                {
                    _Str += "<option value=\"1\">男</option>";
                }
                if (dt.Rows[0]["sex"].ToString() == "2")
                {
                    _Str += "<option value=\"2\" selected>女</option>";
                }
                else
                {
                    _Str += "<option value=\"2\">女</option>";
                }
            }
            else
            {
                _Str += "<option value=\"0\" selected>保密</option>";
                _Str += "<option value=\"1\">男</option>";
                _Str += "<option value=\"2\">女</option>";
            }
            _Str += "</select>";
        }
        return _Str;
    }

    string marriagelist()
    {
        string _Str = "";
        DataTable dt = rd.marriagelist(int.Parse(Foosun.Common.Input.Filter(Request.QueryString["id"])));
        if (dt != null)
        {
            _Str += "<select name=\"marriage\">";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["marriage"].ToString() == "0")
                {
                    _Str += "<option value=\"0\" selected>保密</option>";
                }
                else
                {
                    _Str += "<option value=\"0\">保密</option>";
                }
                if (dt.Rows[0]["marriage"].ToString() == "1")
                {
                    _Str += "<option value=\"1\" selected>未婚</option>";
                }
                else
                {
                    _Str += "<option value=\"1\">未婚</option>";
                }
                if (dt.Rows[0]["marriage"].ToString() == "2")
                {
                    _Str += "<option value=\"2\" selected>已婚</option>";
                }
                else
                {
                    _Str += "<option value=\"2\">已婚</option>";
                }
                _Str += "</select>";
            }
            else
            {
                _Str += "<option value=\"0\" selected>保密</option>";
                _Str += "<option value=\"1\">未婚</option>";
                _Str += "<option value=\"2\">已婚</option>";
            }
        }
        return _Str;
    }

    string isopenlist()
    {
        string _Str = "";
        DataTable dt = rd.isopenlist(int.Parse(Foosun.Common.Input.Filter(Request.QueryString["id"])));
        if (dt != null)
        {
            _Str += "<select name=\"isopen\">";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["isopen"].ToString() == "0")
                {
                    _Str += "<option value=\"0\" selected>不开放</option>";
                }
                else
                {
                    _Str += "<option value=\"0\">不开放</option>";
                }
                if (dt.Rows[0]["isopen"].ToString() == "1")
                {
                    _Str += "<option value=\"1\" selected>开放</option>";
                }
                else
                {
                    _Str += "<option value=\"1\">开放</option>";
                }
                _Str += "</select>";
            }
            else
            {
                _Str += "<option value=\"1\" selected>开放</option>";
                _Str += "<option value=\"0\">不开放</option>";
            }
        }
        return _Str;
    }


    protected void submitSave(object sender, EventArgs e)
    {
        if (Page.IsValid == true)                       //判断是否验证成功
        {

            string NickName = Request.Form["NickName"];
            if (NickName == "")
            {
                PageError("请填写昵称", "");
            }
            string sex = Request.Form["sex"];
            string birthday = this.birthday.Text;
            string Nation = this.Nation.Text;
            string nativeplace = this.nativeplace.Text;
            string Userinfo = this.Userinfo.Text;
            string UserFace = this.UserFace.Text;
            string userFacesize = this.userFacesize.Text;
            string email = this.email.Text;
            string character = this.character.Text;
            string UserFan = this.UserFan.Text;
            string orgSch = this.orgSch.Text;
            string job = this.job.Text;
            string education = this.education.Text;
            string Lastschool = this.Lastschool.Text;
            string RealName = this.RealName.Text;
            string marriage = Request.Form["marriage"];
            string isopen = Request.Form["isopen"];
            string UserGroupNumber = Request.Form["UserGroupNumber"];
            string[] userFacesizes = userFacesize.Split('|');
            int suid = int.Parse(Request.Form["suid"]);
            int uf = 0, uf1 = 0;
            try
            {
                uf = int.Parse(userFacesizes[0].ToString());
                uf1 = int.Parse(userFacesizes[1].ToString());
            }
            catch
            {
                userFacesize = "80|60";
            }
            if (uf > 120) { PageError("头像宽度不能超过120px", ""); }
            if (uf1 > 120) { PageError("头像高度不能超过120px", ""); }
            DataTable dt = rd.getUserInfoParam(suid);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (Userinfo.Length > int.Parse(dt.Rows[0]["CharLenContent"].ToString()))
                    {
                        PageError("签名长度大于" + dt.Rows[0]["CharLenContent"] + "字符", "");
                    }
                    if (dt.Rows[0]["CharHTML"].ToString() == "0")
                    {
                        Userinfo = Foosun.Common.Input.ToHtml(Userinfo);
                    }
                }
                else
                {
                    if (Userinfo.Length > 300)
                    {
                        PageError("签名长度大于300字符", "");
                    }
                }
            }
            else
            {
                if (Userinfo.Length > 300)
                {
                    PageError("签名长度大于300字符", "");
                }
            }

            ///更新基本表
            Foosun.Model.UserInfo uc = new Foosun.Model.UserInfo();
            uc.Id = suid;
            uc.NickName = NickName;
            uc.RealName = RealName;
            uc.sex = int.Parse(sex);
            if (birthday.Trim() != "")
            {
                uc.birthday = DateTime.Parse(birthday);
            }
            else
            {
                uc.birthday = DateTime.Parse("3000-1-1");
            }
            uc.Userinfo = Userinfo;
            uc.UserFace = UserFace;
            uc.userFacesize = userFacesize;
            uc.UserGroupNumber = UserGroupNumber;
            uc.marriage = int.Parse(marriage);
            uc.isopen = int.Parse(isopen);
            uc.email = email;
            rd.UpdateUserInfoBase(uc);

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

            //获得UserID


            DataTable getdt = rd.getUserInfoNum(suid);
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
            PageRight("修改基本资料成功！", "userlist.aspx");
        }
    }
}