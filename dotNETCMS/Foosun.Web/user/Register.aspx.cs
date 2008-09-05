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
using System.IO;
using System.Net.Mail;
using Foosun.CMS.Common;
using Foosun.CMS;
using Foosun.Model;
using System.Xml;
using System.Net;
using Foosun.Config;
using Foosun.PlugIn.Passport;
using System.Text.RegularExpressions;

public partial class user_Register : Foosun.Web.UI.BasePage
{
    string Userfiles = Foosun.Config.UIConfig.UserdirFile;
    Foosun.CMS.user User = new Foosun.CMS.user();
    Foosun.Model.UserParam upi = new Foosun.Model.UserParam();
    public string agreement = null;
    string _dirdum = Foosun.Config.UIConfig.dirDumm;
    ContentManage cmd = new ContentManage();

    protected void Page_Init(object sernder, EventArgs e)
    {
        Response.AppendHeader("P3P", "CP=CURa ADMa DEVa PSAo PSDo OUR BUS UNI PUR INT DEM STA PRE COM NAV OTC NOI DSP COR");
        getRegInfo();
        
    }

    protected void getRegInfo()
    {
        checkUserName();

        if (_dirdum.Trim() != "")
            _dirdum = "/" + _dirdum;
        copyright.InnerHtml = CopyRight;
        string siteID = "0";
        //if (siteID == string.Empty && SiteID == null)
        //    siteID = "0";
        if (upi == null)
            PageError("错误的频道ID，找不到记录.", "");
        upi = User.UserParam(siteID);
        if (upi.RegTF == 0)
            PageError("系统已关闭注册，不能注册", "");
        agreement = upi.RegContent;
        SiteID.Value = siteID;
        CreateControl();
        Button bt2 = (Button)Page.FindControl("storeBut");
        bt2.Command += new CommandEventHandler(this.storeBut);
    }

    protected void checkUserName()
    {
        if (Request.QueryString["Action"] == "checkusername")
        {
            string str_Username = Request.QueryString["username"];
            Regex re = new Regex(@"[\u4e00-\u9fa5]", RegexOptions.Compiled);
            Match m = re.Match(str_Username);
            if (m.Success)
            {
                if (str_Username.Length < 2)
                {
                    Response.Write("用户名为中文时不少于两个字符!");
                    Response.End();
                }
                if (str_Username.Length > 16)
                {
                    Response.Write("用户名为中文时不大于十八个字符!");
                    Response.End();
                }
            }
            else
            {
                if (string.IsNullOrEmpty(str_Username))
                {
                    Response.Write("用户名不能为空!");
                    Response.End();
                }
                if (str_Username.Length < 3)
                {
                    Response.Write("用户名为英文时不少于三个字符!");
                    Response.End();
                }
                if (str_Username.Length > 18)
                {
                    Response.Write("用户名为英文时不大于十八个字符!");
                    Response.End();
                }
            }
            //if (str_Username == "" || str_Username == null || str_Username.Length < 3 || str_Username.Length > 18)
            //    Response.End();
            if (User.sel_username(str_Username) != 0)
            {
                Response.Write("用户名(" + str_Username + ")已存在!");
                Response.End();
            }
            else
            {
                Response.Write("恭喜，此用户名(" + str_Username + ")可以注册!");
                Response.End();
            }
        }
    }


    protected void submit_Click(object sender, EventArgs e)
    {
        this.Panel1.Visible = false;
        this.Panel2.Visible = true;
    }

    protected void storeBut(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string UserName = Request.Form["usernameBox"].ToString();
            if (User.sel_username(UserName) != 0)
            {
                PageError("注册失败，用户名已经被占用", "Register.aspx");
            }

            #region 取得会员表注册参数
            string pwd = Request.Form["pwdBox"].ToString();
            string UserPassword = Foosun.Common.Input.MD5(pwd, true);
            string UserNum = Foosun.Common.Rand.Number(12);//产生12位随机字符

            Foosun.Model.User ui = new Foosun.Model.User();
            Foosun.Model.UserFields ufi = new Foosun.Model.UserFields();

            ui.Id = 0;
            ui.UserNum = UserNum;
            ui.UserName = UserName;
            ui.UserPassword = UserPassword;

            ui.isAdmin = 0;
            ui.UserGroupNumber = upi.RegGroupNumber;///取得注册时默认组编号
            ui.Sex = 0;
            ui.birthday = Convert.ToDateTime("1980-11-11");
            ui.Userinfo = "";
            ui.UserFace = "" + Foosun.Publish.CommonData.getUrl() + "/sysImages/user/noHeadpic.gif";
            ui.userFacesize = "80|80";
            ui.marriage = 0;

            ///取得注册时获得积分
            string[] selsetPoint = upi.setPoint.Split('|');
            string selectiPoint = selsetPoint[0].ToString();
            string selectgPoint = selsetPoint[1].ToString();
            ui.iPoint = Convert.ToInt32(selectiPoint);
            ui.gPoint = Convert.ToInt32(selectgPoint);

            ui.cPoint = 0;
            ui.aPoint = 0;
            ui.isLock = 0;
            ui.RegTime = DateTime.Now;
            ui.LastLoginTime = DateTime.Now;
            ui.OnlineTime = 0;
            ui.OnlineTF = 0;
            ui.LoginNumber = 0;
            ui.FriendClass = "";
            ui.LoginLimtNumber = 0;
            ui.LastIP = Foosun.Common.Public.getUserIP();
            ui.SiteID = SiteID.Value;
            ui.Addfriend = "2";
            ui.isOpen = 0;
            ui.ParmConstrNum = 0;

            ///注册是否需要实名验证
            Foosun.Model.UserGroup ugi = new Foosun.Model.UserGroup();
            ugi = User.UserGroup(upi.RegGroupNumber);
            ui.isIDcard = 0;
            ui.IDcardFiles = "";

            ui.Addfriendbs = 2;

            ///注册是否需要电子邮件验证
            if (upi.returnemail == 1)
            {
                ui.EmailATF = 0;
                ui.EmailCode = Foosun.Common.Input.MD5(Foosun.Common.Rand.Str(15), false); ;
            }
            else
            {
                ui.EmailATF = 1;
                ui.EmailCode = "";
            }

            ///注册是否需要手机验证
            if (upi.returnmobile == 1)
            {
                ui.isMobile = 0;
                ui.MobileCode = Foosun.CMS.FSSecurity.FDESEncrypt(Foosun.Common.Rand.Str(8), 1);
            }
            else
            {
                ui.isMobile = 1;
                ui.MobileCode = "";
            }
            ui.BindTF = 0;


            string[] regItem = upi.regItem.Split(',');
            // bug 修改, 程序流程错误 by arjun
            ui.NickName = "";
            ui.RealName = "";
            ui.PassQuestion = "";
            ui.PassKey = "";
            ui.CertType = "";
            ui.CertNumber = "";
            ui.Email = "";
            ui.mobile = "";
            ufi.province = "";
            ufi.City = "";
            ufi.Address = "";
            ufi.Postcode = "";
            ufi.FaTel = "";
            ufi.WorkTel = "";
            ufi.Fax = "";
            ufi.QQ = "";
            ufi.MSN = "";
            for (int i = 0; i < regItem.Length; i++)
            {
                if (regItem[i] == "NickName")
                    ui.NickName = Request.Form["NickNameBox"].ToString();
                //else
                //    ui.NickName = "";

                if (regItem[i] == "RealName")
                    ui.RealName = Request.Form["RealNameBox"].ToString();
                //else
                //    ui.RealName = "";

                if (regItem[i] == "PassQuestion")
                    ui.PassQuestion = Request.Form["PassQuestionBox"].ToString();
                //else
                //    ui.PassQuestion = "";

                if (regItem[i] == "PassKey")
                    ui.PassKey = Request.Form["PassKeyBox"].ToString();
                //else
                //    ui.PassKey = "";

                if (regItem[i] == "CertType")
                    ui.CertType = Request.Form["CertTypeBox"].ToString();
                //else
                //    ui.CertType = "";

                if (regItem[i] == "CertNumber")
                    ui.CertNumber = Request.Form["CertNumberBox"].ToString();
                //else
                //    ui.CertNumber = "";

                if (regItem[i] == "email")
                {
                    ui.Email = Request.Form["emailBox"].ToString();
                    if (User.sel_email(Request.Form["emailBox"].ToString()) != 0)
                    {
                        PageError("注册失败，电子邮件已经被占用", "Register.aspx");
                    }
                }
                //else
                //    ui.Email = "";

                if (regItem[i] == "Mobile")
                    ui.mobile = Request.Form["MobileBox"].ToString();
                //else
                //    ui.mobile = "";
            #endregion

                #region 取得会员附表参数
                if (regItem[i] == "province")
                { 
                    //bug修改，周峻平，查询出编号对应的名称
                    ufi.province = Request.Form["province"].ToString();
                    DataTable dt = cmd.getProvinceOrCityList("0");
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        //如果编号对应上了，则设置为名称
                        if (dt.Rows[j][1].ToString().Equals(ufi.province))
                        {
                            ufi.province = dt.Rows[j][0].ToString();
                            break;
                        }
                    }
                }

                if (regItem[i] == "City")
                {
                    ufi.City = Request.Form["City"].ToString();
                    //查询出此省份下的城市
                    DataTable dt = cmd.getProvinceOrCityList(Request.Form["province"].ToString());
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        //如果编号对应上了，则设置为名称
                        if (dt.Rows[j][1].ToString().Equals(ufi.City))
                        {
                            ufi.City = dt.Rows[j][0].ToString();
                            break;
                        }
                    }
                }
                //else
                //    ufi.City = "";

                if (regItem[i] == "Address")
                    ufi.Address = Request.Form["AddressBox"].ToString();
                //else
                //    ufi.Address = "";

                if (regItem[i] == "Postcode")
                    ufi.Postcode = Request.Form["PostcodeBox"].ToString();
                //else
                //    ufi.Postcode = "";

                if (regItem[i] == "FaTel")
                    ufi.FaTel = Request.Form["FaTelBox"].ToString();
                //else
                //    ufi.FaTel = "";

                if (regItem[i] == "WorkTel")
                    ufi.WorkTel = Request.Form["WorkTelBox"].ToString();
                //else
                //    ufi.WorkTel = "";

                if (regItem[i] == "Fax")
                    ufi.Fax = Request.Form["FaxBox"].ToString();
                //else
                //    ufi.Fax = "";

                if (regItem[i] == "QQ")
                    ufi.QQ = Request.Form["QQBox"].ToString();
                //else
                //    ufi.QQ = "";

                if (regItem[i] == "MSN")
                    ufi.MSN = Request.Form["MSNBox"].ToString();
                //else
                //    ufi.MSN = "";
            }
            ufi.ID = 0;
            ufi.userNum = UserNum;
            ufi.character = "";
            ufi.UserFan = "";
            ufi.Nation = "";
            ufi.nativeplace = "";
            ufi.Job = "";
            ufi.education = "";
            ufi.Lastschool = "";
            ufi.orgSch = "";
                #endregion


            #region 取得会员冲值记录参数
            Foosun.Model.UserGhistory ughi = new Foosun.Model.UserGhistory();
            ughi.Id = 0;
            ughi.GhID = Foosun.Common.Rand.Number(12);//产生12位随机字符
            ughi.ghtype = 1;
            ughi.Gpoint = ui.gPoint;
            ughi.iPoint = ugi.iPoint;
            ughi.Money = 0;
            ughi.CreatTime = DateTime.Now;
            ughi.userNum = ui.UserNum;
            ughi.gtype = 7;
            ughi.content = "注册获得";
            ughi.SiteID = ui.SiteID;
            #endregion


            //在其他系统中同步注册
            DPO_Request request = new DPO_Request(this.Context);
            request.UserName = ui.UserName;
            request.EMail = ui.Email;
            request.PassWord = pwd;
            string question = Request.Form["PassQuestionBox"];

            string answer = Request.Form["PassKeyBox"];
            request.Question = question;
            request.Answer = answer;
            request.ProcessMultiPing("reguser");
            if (request.FoundErr)
            {
                PageError("同步注册失败!<br/>" + string.Join(",", request.ErrStr.ToArray()), "Register.aspx");
            }
            else if (User.Add_User(ui) == 1 && User.Add_userfields(ufi) == 1 && User.Add_Ghistory(ughi) == 1)
            {
                CreateFolder(ui.UserNum);
                Foosun.Global.Current.Set(new GlobalUserInfo(ui.UserNum, ui.UserName, ui.SiteID, "0"));
                 
                if (upi.returnemail == 1)
                {
                    //发送电子邮件
                    string Emailto = ui.Email;
                    string EmailUserNum = ui.UserNum;
                    string EmailCode = ui.EmailCode;
                    string EmailFrom = Foosun.Config.UIConfig.emailfrom;
                    string EmailSmtpServer = Foosun.Config.UIConfig.smtpserver;
                    string EmailUserName = Foosun.Config.UIConfig.emailuserName;
                    string EmailPwd = Foosun.Config.UIConfig.emailuserpwd;
                    string subj = "邮件密码验证";

                    string bodys = "亲爱的" + UserName + ":<br />";
                    bodys += "&nbsp;&nbsp;您注册的用户名：" + UserName + ",用户编号：" + UserNum + ",密码：" + pwd + "<br />";
                    bodys += "&nbsp;&nbsp;请点击此联接激活您的电子邮件:" + Foosun.Publish.CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirUser + "/info" +
                             "/getPassport.aspx?t=mail&e=" + Foosun.Common.Input.MD5(ui.Email, true) + "&" +
                             "u=" + Foosun.Common.Input.MD5(ui.UserNum, true) + "&c=" + ui.EmailCode + "";

                    Foosun.Common.Public.sendMail(EmailSmtpServer, EmailUserName, EmailPwd, EmailFrom, Emailto, subj, bodys);

                    PageRight("<span style=\"font-size:14px;font-weight:bold;\">" +
                                                        "恭喜(" + UserName + ")!您已经在本站注册成功。</span>" +
                                                        "<span style=\"color:red\">但是您需要验证电子邮件才能登陆.</span>" +
                                                        "<li>一封电子邮件已经发送到您的邮件：" + ui.Email + "</li>" +
                                                        "<li>您的用户名：" + UserName + "&nbsp;&nbsp;&nbsp;" +
                                                        "用户唯一编号：" + UserNum + "</li>", "login.aspx");
                }

                if (upi.returnmobile == 1)
                {
                    //发送验证码到ISP
                    //如果成功转向到下面页面
                    Response.Redirect("info/MobileValidate.aspx?uid=" + UserName);
                }

                if (ugi.IsCert == 1)
                {
                    Response.Write("<script language=\"javascript\" type=\"text/javascript\">alert" +
                                   "('注册成功！\\n但是要求实名认证.\\n点 [确定] 进行实名认证。');" +
                                   "location.href='index.aspx?urls=" + Foosun.Publish.CommonData.SiteDomain + "/info/userinfo" +
                                   "_idcard.aspx?type=CreatCert\';</script>");
                    DPO_Request dporequest = new DPO_Request(Context);
                    dporequest.RequestLogin(UserName, pwd, string.Empty);
                    Response.End();
                }
                else
                {
                    DPO_Request dporequest = new DPO_Request(Context);
                    dporequest.RequestLogin(UserName, pwd,
                        string.Format("{0}/user/index.aspx", Request.ApplicationPath == "/" ? string.Empty : Request.ApplicationPath)
                        );
                    Response.Write("注册成功，页面转接中，请稍候……");
                    Response.End();
                }

                #region 整合Discuz!NT
                //采用新的整合接口，此段代码停用（陈仕欣，2008－3－27）
                /*
                string adUserPassword = Foosun.Common.Input.MD5(pwd, false);
                XmlDocument xmlDoc = new XmlDocument();
                string xmlName = Server.MapPath("../api/dz/Adapt.config");
                AdaptConfig adConfig = new AdaptConfig(xmlName);

                if (adConfig.isAdapt)
                {
                    try
                    {
                        string adaptePath = adConfig.adaptPath;
                        adaptePath += "?username=" + UserName + "&password=" + adUserPassword + "&tag=register";
                        adaptePath += "&Gender=" + ui.Sex;
                        adaptePath += "&Regip=" + ui.LastIP;
                        Response.Write("<script type=\"text/javascript\" language=\"javascript\">window.open(\"" + adaptePath + "\",\"\",\"left=5000,top=5000\");</script>");
                        Uri uri = new Uri(adaptePath);
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                        request.KeepAlive = false;
                        request.ProtocolVersion = HttpVersion.Version10;
                        request.Method = "GET";
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.Proxy = System.Net.WebProxy.GetDefaultProxy();
                        request.AllowAutoRedirect = true;
                        request.MaximumAutomaticRedirections = 10;
                        request.Timeout = (int)new TimeSpan(0, 0, 1).TotalMilliseconds;
                        request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream responseStream = response.GetResponseStream();
                        StreamReader readStream = new StreamReader(responseStream, System.Text.Encoding.Default);
                        readStream.ReadToEnd();
                    }
                    catch
                    {
                        Response.Redirect("Reg_Result.aspx?Error=开启了整合，但整合失败，可能是远程BBS论坛地址有错误！");
                    }
                }
                 */
                #endregion

                //Response.Redirect("Reg_Result.aspx");
            }
            else
            {
                PageError("注册失败!", "Register.aspx");
            }
        }
    }

    #region  创建文件夹
    public void CreateFolder(string FolderPathName)
    {
        string Path = string.Empty;
        if (_dirdum.Trim() != string.Empty)
        {
            _dirdum = "/" + _dirdum;
        }
        Path = _dirdum + "/" + Userfiles;
        string CreatePath = Server.MapPath(Path);
        try
        {
            Foosun.CMS.Templet.Templet tc = new Foosun.CMS.Templet.Templet();
            tc.AddDir(CreatePath, FolderPathName);
        }
        catch { }
    }
    #endregion


    protected void CreateControl()
    {
        string[] arr_regItem = upi.regItem.Split(',');
        for (int i = 0; i < arr_regItem.Length; i++)
        {
            if (arr_regItem[i] == "UserName")
            {
                string ctr = "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"table\"><tr class=\"TR_BG_list\"><td class=\"list_link\" Width=\"30%\" style=\"text-align: right;\">用户名：</td><td class=\"list_link\" Width=\"70%\"><asp:TextBox ID=\"usernameBox\" MaxLength=\"18\" runat=\"server\" Width=\"184px\"></asp:TextBox>&nbsp;<input id=\"b_checkusername\" type=\"button\" onclick=\"javascript:checkusername(document.form1.usernameBox.value)\" value=\"检查用户\" class=\"form\"/>&nbsp;<label id=\"div_content\" style=\"color:blue;\"></label><asp:RequiredFieldValidator ID=\"RequiredFieldValidator1\" runat=\"server\" ErrorMessage=\"用户名不能为空\" ControlToValidate=\"usernameBox\" Display=\"Dynamic\"></asp:RequiredFieldValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "UserPassword")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">密码：</td><td class=\"list_link\"><asp:TextBox ID=\"pwdBox\" runat=\"server\" Width=\"184px\" MaxLength=\"18\" TextMode=\"Password\" onblur=\"chkpwd(this)\"></asp:TextBox>&nbsp;<span id=\"chkResult\"></span><asp:RequiredFieldValidator ID=\"RequiredFieldValidator2\" runat=\"server\" ControlToValidate=\"pwdBox\" ErrorMessage=\"请输入密码\"></asp:RequiredFieldValidator></td></tr><tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">确认密码：</td><td class=\"list_link\"><asp:TextBox ID=\"pwdsBox\" runat=\"server\" Width=\"184px\" TextMode=\"Password\"></asp:TextBox>&nbsp;<asp:CompareValidator ID=\"CompareValidator1\" runat=\"server\" ControlToCompare=\"pwdBox\" ControlToValidate=\"pwdsBox\" ErrorMessage=\"两次输入的密码不一致\" Display=\"Dynamic\"></asp:CompareValidator><asp:RequiredFieldValidator ID=\"RequiredFieldValidator3\" runat=\"server\" ControlToValidate=\"pwdsBox\" ErrorMessage=\"请输入确认密码\"></asp:RequiredFieldValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "email")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">电子邮件：</td><td class=\"list_link\"><asp:TextBox ID=\"emailBox\" runat=\"server\" Width=\"184px\" MaxLength=\"50\"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID=\"RequiredFieldValidator4\" runat=\"server\" ControlToValidate=\"emailBox\" ErrorMessage=\"请输入电子邮件\" Display=\"Dynamic\"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID=\"RegularExpressionValidator2\" runat=\"server\" ErrorMessage=\"电子邮件格式不对\" ValidationExpression=\"\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*\" ControlToValidate=\"emailBox\" Display=\"Dynamic\"></asp:RegularExpressionValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "PassQuestion")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">密码问题：</td><td class=\"list_link\"><asp:TextBox ID=\"PassQuestionBox\" runat=\"server\" Width=\"184px\" MaxLength=\"16\"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID=\"RequiredFieldValidator5\" runat=\"server\" ControlToValidate=\"PassQuestionBox\" ErrorMessage=\"请输入密码问题\"></asp:RequiredFieldValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "PassKey")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">密码问题答案：</td><td class=\"list_link\"><asp:TextBox ID=\"PassKeyBox\" runat=\"server\" Width=\"184px\" MaxLength=\"16\"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID=\"RequiredFieldValidator6\" runat=\"server\" ControlToValidate=\"PassKeyBox\" ErrorMessage=\"请输入密码问题答案\"></asp:RequiredFieldValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "RealName")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">真实姓名：</td><td class=\"list_link\"><asp:TextBox ID=\"RealNameBox\" runat=\"server\" Width=\"184px\" MaxLength=\"16\"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID=\"RequiredFieldValidator7\" runat=\"server\" ControlToValidate=\"RealNameBox\" ErrorMessage=\"请输入真实姓名\"></asp:RequiredFieldValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "NickName")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">昵称：</td><td class=\"list_link\"><asp:TextBox ID=\"NickNameBox\" runat=\"server\" Width=\"184px\" MaxLength=\"16\"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID=\"RequiredFieldValidator8\" runat=\"server\" ControlToValidate=\"NickNameBox\" ErrorMessage=\"请输入昵称\"></asp:RequiredFieldValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "CertType")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">证件类型：</td><td class=\"list_link\"><asp:DropDownList ID=\"CertTypeBox\" runat=\"server\" Width=\"184px\"><asp:ListItem>身份证</asp:ListItem><asp:ListItem>学生证</asp:ListItem><asp:ListItem>驾驶证</asp:ListItem><asp:ListItem>军人证</asp:ListItem><asp:ListItem>护照</asp:ListItem></asp:DropDownList></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "CertNumber")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">证件号码：</td><td class=\"list_link\"><asp:TextBox ID=\"CertNumberBox\" runat=\"server\" Width=\"184px\" MaxLength=\"18\"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID=\"RequiredFieldValidator10\" runat=\"server\" ControlToValidate=\"CertNumberBox\" ErrorMessage=\"请输入证件号码\"></asp:RequiredFieldValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "province")
            {
                DataTable dt = cmd.getProvinceOrCityList("0");
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">省份：</td><td class=\"list_link\"><select id=\"province\" name=\"province\" style=\"width:184px\" onchange=\"GetSubClass(this.options[this.selectedIndex].value)\">";
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    ctr += "<option value=\"" + dt.Rows[j][1].ToString() + "\">" + dt.Rows[j][0].ToString() + "</option>";

                }
                ctr += "</select></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }

            if (arr_regItem[i] == "City")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">城市：</td><td class=\"list_link\"><div id=\"citydiv\"></div></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }

            //if (arr_regItem[i] == "province")
            //{
            //    DataTable dt = cmd.getProvinceOrCityList("0");
            //    string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">城市：</td><td class=\"list_link\"><asp:DropDownList ID=\"provinceBox\" runat=\"server\" Width=\"184px\" AutoPostBack=\"true\" OnSelectedIndexChanged=\"provinceBox_SelectedIndexChanged\">";
            //    ctr += "<asp:ListItem>请选择省份</asp:ListItem>";
            //    for (int j = 0; j < dt.Rows.Count; j++)
            //    {
            //        ctr += "<asp:ListItem value = \"" + dt.Rows[j][1].ToString() + "\">" + dt.Rows[j][0].ToString() + "</asp:ListItem>";
            //    }
            //    //ctr += "<asp:ListItem>北京</asp:ListItem><asp:ListItem>天津</asp:ListItem><asp:ListItem>上海</asp:ListItem><asp:ListItem>江苏</asp:ListItem><asp:ListItem>广东</asp:ListItem><asp:ListItem>福建</asp:ListItem><asp:ListItem>浙江</asp:ListItem><asp:ListItem>陕西</asp:ListItem><asp:ListItem>山西</asp:ListItem><asp:ListItem>四川</asp:ListItem><asp:ListItem>贵州</asp:ListItem><asp:ListItem>云南</asp:ListItem><asp:ListItem>海南</asp:ListItem><asp:ListItem>甘肃</asp:ListItem><asp:ListItem>内蒙</asp:ListItem><asp:ListItem>新疆</asp:ListItem><asp:ListItem>西藏</asp:ListItem><asp:ListItem>青海</asp:ListItem><asp:ListItem>安徽</asp:ListItem><asp:ListItem>广西</asp:ListItem><asp:ListItem>湖北</asp:ListItem><asp:ListItem>湖南</asp:ListItem><asp:ListItem>重庆</asp:ListItem><asp:ListItem>河北</asp:ListItem><asp:ListItem>河南</asp:ListItem><asp:ListItem>吉林</asp:ListItem><asp:ListItem>辽林</asp:ListItem><asp:ListItem>山东</asp:ListItem><asp:ListItem>黑龙江</asp:ListItem><asp:ListItem>江西</asp:ListItem><asp:ListItem>宁夏</asp:ListItem><asp:ListItem>香港</asp:ListItem><asp:ListItem>澳门</asp:ListItem><asp:ListItem>台湾</asp:ListItem><asp:ListItem>海外</asp:ListItem><asp:ListItem>其它</asp:ListItem>";
            //    ctr += "</asp:DropDownList></td></tr>";
            //    Control ctrl = Page.ParseControl(ctr);
            //    PlaceHolder1.Controls.Add(ctrl);
            //}
            //if (arr_regItem[i] == "City")
            //{
            //    DataTable dt = cmd.getProvinceOrCityList(Request.Form["provinceBox"]);
            //    string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">城市：</td><td class=\"list_link\"><asp:DropDownList ID=\"CityBox\" runat=\"server\" Width=\"184px\">";
            //    for (int j = 0; j < dt.Rows.Count; j++)
            //    {
            //        ctr += "<asp:ListItem>" + dt.Rows[j][0].ToString() + "</asp:ListItem>";
            //    }
            //    ctr += "</asp:DropDownList></td></tr>";
            //    Control ctrl = Page.ParseControl(ctr);
            //    PlaceHolder1.Controls.Add(ctrl);
            //}
            if (arr_regItem[i] == "Address")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">地址：</td><td class=\"list_link\"><asp:TextBox ID=\"AddressBox\" runat=\"server\" Width=\"184px\" MaxLength=\"16\"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID=\"RequiredFieldValidator13\" runat=\"server\" ControlToValidate=\"AddressBox\" ErrorMessage=\"请输入地址\" ></asp:RequiredFieldValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "Postcode")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">邮政编码：</td><td class=\"list_link\"><asp:TextBox ID=\"PostcodeBox\" runat=\"server\" Width=\"184px\" MaxLength=\"16\"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID=\"RequiredFieldValidator14\" runat=\"server\" ControlToValidate=\"PostcodeBox\" ErrorMessage=\"请输入邮政编码\"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID=\"RegularExpressionValidator3\" runat=\"server\" ErrorMessage=\"邮政编码格式不对\" ValidationExpression=\"\\d{6}\" ControlToValidate=\"PostcodeBox\" Display=\"Dynamic\"></asp:RegularExpressionValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "Mobile")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">手机：</td><td class=\"list_link\"><asp:TextBox ID=\"MobileBox\" runat=\"server\" Width=\"184px\" MaxLength=\"16\"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID=\"RequiredFieldValidator15\" runat=\"server\" ControlToValidate=\"MobileBox\" ErrorMessage=\"请输入手机\"></asp:RequiredFieldValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "Fax")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">传真：</td><td class=\"list_link\"><asp:TextBox ID=\"FaxBox\" runat=\"server\" Width=\"184px\" MaxLength=\"16\"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID=\"RequiredFieldValidator16\" runat=\"server\" ControlToValidate=\"FaxBox\" ErrorMessage=\"请输入传真\"></asp:RequiredFieldValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "WorkTel")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">工作电话：</td><td class=\"list_link\"><asp:TextBox ID=\"WorkTelBox\" runat=\"server\" Width=\"184px\" MaxLength=\"16\"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID=\"RequiredFieldValidator17\" runat=\"server\" ControlToValidate=\"WorkTelBox\" ErrorMessage=\"请输入工作电话\"　Display=\"Dynamic\"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID=\"RegularExpressionValidator4\" runat=\"server\" ErrorMessage=\"电话号码格式不对\" ValidationExpression=\"(\\(\\d{3}\\)|\\d{3}-)?\\d{8}\" ControlToValidate=\"WorkTelBox\" Display=\"Dynamic\"></asp:RegularExpressionValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "FaTel")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">家庭电话：</td><td class=\"list_link\"><asp:TextBox ID=\"FaTelBox\" runat=\"server\" Width=\"184px\" MaxLength=\"16\"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID=\"RequiredFieldValidator18\" runat=\"server\" ControlToValidate=\"FaTelBox\" ErrorMessage=\"请输入家庭电话\"　Display=\"Dynamic\"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID=\"RegularExpressionValidator5\" runat=\"server\" ErrorMessage=\"电话号码格式不对\" ValidationExpression=\"(\\(\\d{3}\\)|\\d{3}-)?\\d{8}\" ControlToValidate=\"FaTelBox\" Display=\"Dynamic\"></asp:RegularExpressionValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "QQ")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">QQ：</td><td class=\"list_link\"><asp:TextBox ID=\"QQBox\" runat=\"server\" Width=\"184px\" MaxLength=\"16\"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID=\"RequiredFieldValidator19\" runat=\"server\" ControlToValidate=\"QQBox\" ErrorMessage=\"请输入QQ\"　Display=\"Dynamic\"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID=\"RegularExpressionValidator6\" runat=\"server\" ErrorMessage=\"QQ号码格式不对\" ValidationExpression=\"[1-9][0-9]{4,}\" ControlToValidate=\"QQBox\" Display=\"Dynamic\"></asp:RegularExpressionValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
            if (arr_regItem[i] == "MSN")
            {
                string ctr = "<tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right;\">MSN：</td><td class=\"list_link\"><asp:TextBox ID=\"MSNBox\" runat=\"server\" Width=\"184px\" MaxLength=\"16\"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID=\"RequiredFieldValidator20\" runat=\"server\" ControlToValidate=\"MSNBox\" ErrorMessage=\"请输入MSN\"></asp:RequiredFieldValidator></td></tr>";
                Control ctrl = Page.ParseControl(ctr);
                PlaceHolder1.Controls.Add(ctrl);
            }
        }

        string ctrs = "<tr class=\"TR_BG_list\"><td class=\"list_link\" align=\"center\"></td><td class=\"list_link\"><asp:Button ID=\"storeBut\" runat=\"server\" OnClick=\"storeBut_Click\" Text=\"确认注册\" CssClass=\"form\" CommandArgument = \"bt2\"/>&nbsp;&nbsp;&nbsp;&nbsp;<input type=\"reset\" name=\"Submit3\" value=\"重新填写\" class=\"form\"/></td></tr></table>";
        Control ctrls = Page.ParseControl(ctrs);
        PlaceHolder1.Controls.Add(ctrls);
    }


}