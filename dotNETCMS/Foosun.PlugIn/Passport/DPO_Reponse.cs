using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Xml;
using Foosun.Model;
using Foosun.DALFactory;
using Foosun.CMS;
using Foosun.Config.API;
namespace Foosun.PlugIn.Passport
{
    /// <summary>
    /// 响应整合
    /// </summary>
    public class DPO_Reponse
    {

        public System.Collections.Generic.List<string> ErrStr = new System.Collections.Generic.List<string>();
        Foosun.Config.API.APIConfig config = Foosun.Config.API.APIConfigs.GetConfig();
        public bool FoundErr = false;
        public string Action = string.Empty;
        public string syskey = string.Empty;
        public string username = string.Empty;
        public string password = string.Empty;
        public string savecookie = string.Empty;
        public string appid = string.Empty;
        public string Sex = string.Empty;
        public string QQ = string.Empty;
        public string MSN = string.Empty;
        public string UserStatus = string.Empty;
        public string TrueName = string.Empty;
        public string Birthday = string.Empty;
        public string TelePhone = string.Empty;
        public string HomePage = string.Empty;
        public string userip = string.Empty;
        public string email = string.Empty;
        public string Question = string.Empty;
        public string Answer = string.Empty;
        public string province = string.Empty;
        public string city = string.Empty;
        public string address = string.Empty;
        System.Xml.XmlDocument xmlDoc = new XmlDocument();
        System.Xml.XmlDocument xmlRequestDoc = new XmlDocument();
        System.Web.HttpContext context;
        public DPO_Reponse(System.Web.HttpContext context)
        {
            this.context = context;
            //调入配置信息
            LoadXmlFile();
        }
      
        /// <summary>
        /// 检查验证票是否正确
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="appKey">应用密钥</param>
        /// <param name="auth_token">验证票</param>
        /// <returns>检查验证票是否正确</returns>
        static bool validateAuth_token(string username, string appKey, string auth_token)
        {
            switch (auth_token.Length)
            {
                case 16:
                    if (System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(string.Concat(username.ToLower(), appKey), "MD5").Substring(8, 16).ToLower() == auth_token.ToLower())
                        return true;
                    return false;
                case 32:
                    if (System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(string.Concat(username.ToLower(), appKey), "MD5").ToLower() == auth_token.ToLower())
                        return true;
                    return false;
                default:
                    return false;
            }
        }
        private void LoadXmlFile()
        {
            string filename = string.Empty;
            if (HttpContext.Current == null)
            {
                filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration/config/Response.xml");
            }
            else
            {
                string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                if (applicationPath.EndsWith("/") == false && applicationPath.EndsWith(@"\") == false)
                    applicationPath += "/";
                applicationPath += "configuration/config/Response.xml";
                filename = System.Web.HttpContext.Current.Server.MapPath(applicationPath);
            }

            xmlDoc.Load(filename);
        }
        void loaddata()
        {
            //获取数据
            XmlNode node;

            node = this.xmlRequestDoc.SelectSingleNode("//appid");
            if (node != null)
                appid = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//syskey");
            if (node != null)
                syskey = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//action");
            if (node != null)
                Action = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//username");
            if (node != null)
                username = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//password");
            if (node != null)
                this.password = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//savecookie");
            if (node != null)
                this.savecookie = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//userip");
            if (node != null)
                userip = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//email");
            if (node != null)
                this.email = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//question");
            if (node != null)
                Question = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//answer");
            if (node != null)
                Answer = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//gender");
            if (node != null)
                Sex = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//qq");
            if (node != null)
                QQ = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//msn");
            if (node != null)
                MSN = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//userstatus");
            if (node != null)
                this.UserStatus = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//truename");
            if (node != null)
                this.TrueName = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//birthday");
            if (node != null)
                this.Birthday = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//homepage");
            if (node != null)
                this.HomePage = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//telephone");
            if (node != null)
                this.TelePhone = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//province");
            if (node != null)
                province = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//city");
            if (node != null)
                city = node.InnerText;
            node = xmlRequestDoc.SelectSingleNode("//address");
            if (node != null)
                address = node.InnerText;
        }
        /// <summary>
        /// 返回信息到请求端
        /// </summary>
        /// <param name="status"></param>
        /// <param name="strMsg"></param>
        public void SendResult(string status, string strMsg)
        {
            string SendResult = string.Empty;
            SetNodeValue("appid", appid);
            SetNodeValue("status", Convert.ToString(status));
            SetNodeValue("message", Convert.ToString(strMsg));
            this.context.Response.ContentType = "text/xml";
            this.context.Response.Charset = "gb2312";
            this.context.Response.Clear();

            this.context.Response.Write(xmlDoc.OuterXml);
            return;
        }

        /// <summary>
        /// 读取用户信息，并返回请求
        /// </summary>
        public void GetUser()
        {
            SetNodeValue("username", this.username);
            SetNodeValue("email", this.email);
            SetNodeValue("question", this.Question);
            SetNodeValue("answer", this.Answer);
            SetNodeValue("savecookie", this.savecookie);
            SetNodeValue("truename", this.TrueName);
            SetNodeValue("gender", this.Sex);
            SetNodeValue("birthday", Birthday);
            SetNodeValue("qq", QQ);
            SetNodeValue("msn", MSN);
            SetNodeValue("telephone", TelePhone);
            SetNodeValue("homepage", HomePage);
            SetNodeValue("userip", userip);
            SetNodeValue("userstatus", UserStatus);
            SetNodeValue("province", this.province);
            SetNodeValue("city", this.city);
            SetNodeValue("address", address);
        }
        


        //将读取到XML模板中的各个元素赋值
        public void SetNodeValue(string strNodeName, string strNodeValue)
        {
            try
            {
                xmlDoc.SelectSingleNode("//" + strNodeName).InnerText = strNodeValue;
            }
            catch
            {
                AddErrStr("写入信息发生错误，请重试！");
            }
        }
        //错误处理函数
        public void AddErrStr(string Message)
        {
            ErrStr.Add(string.Format("{0}提示您：{1}", appid, Message));

            FoundErr = true;
        }
        //开始处理请求
        public void DoWork()
        {
            


            string syskey = context.Request.QueryString["syskey"];
            if (string.IsNullOrEmpty(syskey))
                syskey = string.Empty;
            string username = context.Request.QueryString["username"];
            if (string.IsNullOrEmpty(username))
                username = string.Empty;
            string password = context.Request.QueryString["password"];
            if (string.IsNullOrEmpty(password))
                password = string.Empty;
            string savecookie = context.Request.QueryString["savecookie"];

            if (string.IsNullOrEmpty(savecookie) || savecookie == "0")
                savecookie = string.Empty;


             

            if (string.IsNullOrEmpty(syskey) == false)
            {
                 
                context.Response.AppendHeader("P3P" , "CP=CURa ADMa DEVa PSAo PSDo OUR BUS UNI PUR INT DEM STA PRE COM NAV OTC NOI DSP COR");
                if (string.IsNullOrEmpty(password))
                {
                    //注销
                    context.Session.Remove("SITEINFO");
                }
                else 
                {
                    if (validateAuth_token(username, config.AppKey, syskey) == false)
                    {
                        context.Response.Write("document.write('安全验证码错误！');");
                        return;
                    }
                    
                    //设置登录状态
                    GlobalUserInfo info = null;
                    UserMisc ud = new UserMisc();
                    IDataReader dr = ud.GetUserAPiInfo(username);
                    if (dr.Read())
                    {
                        info = new GlobalUserInfo(dr.GetString(0),
                            dr.GetString(1),
                            "0",
                            "0");
                    }
                    dr.Close();
                    if (info != null)
                        Global.Current.Set(info);
                    else
                    {
                        context.Response.Write("document.write('同步登录失败！');");
                    }

                }
            }
            else
            {
                //响应整合端请求
                if (config.Enable == false)
                {
                    SendResult("1", "系统并未开启整合接口！");
                    return;
                }

               

                StreamReader reader1 = new StreamReader(context.Request.InputStream, System.Text.Encoding.GetEncoding("GB2312"));
                string xml = reader1.ReadToEnd();
                reader1.Close();
                try
                {
                    this.xmlRequestDoc.LoadXml(xml);
                }
                catch
                {
                    this.SendResult("1", "解析数据出错!");
                    return;
                }
                this.loaddata();



                if (validateAuth_token(this.username, config.AppKey, this.syskey) == false)
                {
                    ErrStr.Add(string.Format("{0}提示您：{1}", appid, "安全码校验失败"));
                    FoundErr = true;

                }
                switch (this.Action)
                {
                    case "reguser"://注册新用户	
                        reguser();
                        break;
                    case "login"://登录
                        login();
                        break;
                    case "logout"://注销
                        logout();
                        break;
                    case "update"://更新用户信息
                        update();
                        break;
                    case "delete"://删除用户
                        delete();
                        break;
                    case "getinfo"://查询用户详细资料
                        getinfo();
                        break;
                    case "checkname"://检查用户名是否可注册
                        checkname();
                        break;
                }
                if (this.FoundErr)
                {
                    this.SendResult("1", String.Join(",", this.ErrStr.ToArray()));
                }
                else
                {
                    this.SendResult("0", string.Empty);
                }
                this.ErrStr.Clear();
                this.FoundErr = false;
            }




        }
        /// <summary>
        /// 注册新用户
        /// </summary>
        void reguser()
        {
            Foosun.CMS.user User = new Foosun.CMS.user();
            Foosun.Model.UserParam upi = User.UserParam("0");
            

            if (User.sel_username(this.username) != 0)
            {
                this.AddErrStr("用户名已被占用！");
                return;
            }

            #region 取得会员表注册参数
            string pwd = this.password;
            string UserPassword = Foosun.Common.Input.MD5(pwd, true);
            string UserNum = Foosun.Common.Rand.Number(12);//产生12位随机字符

            Foosun.Model.User ui = new Foosun.Model.User();
            Foosun.Model.UserFields ufi = new Foosun.Model.UserFields();

            ui.Id = 0;
            ui.UserNum = UserNum;
            ui.UserName = this.username;
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
            ui.SiteID = "0";
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
                    ui.NickName = this.username;
                //else
                //    ui.NickName = "";

                if (regItem[i] == "RealName")
                    ui.RealName = this.TrueName;
                //else
                //    ui.RealName = "";

                if (regItem[i] == "PassQuestion")
                    ui.PassQuestion = this.Question;
                //else
                //    ui.PassQuestion = "";

                if (regItem[i] == "PassKey")
                    ui.PassKey = this.Answer;
                //else
                //    ui.PassKey = "";

                if (regItem[i] == "CertType")
                    ui.CertType = string.Empty;
                //else
                //    ui.CertType = "";

                if (regItem[i] == "CertNumber")
                    ui.CertNumber = string.Empty;
                //else
                //    ui.CertNumber = "";

                if (regItem[i] == "email")
                {
                    ui.Email = this.email;
                    if (User.sel_email(this.email) != 0)
                    {
                        this.AddErrStr("电子邮件已经被占用");
                    }
                }
                //else
                //    ui.Email = "";

                if (regItem[i] == "Mobile")
                    ui.mobile = string.Empty;
                //else
                //    ui.mobile = "";
            #endregion

                #region 取得会员附表参数
                if (regItem[i] == "province")
                    ufi.province = this.province;
                //else
                //    ufi.province = "";

                if (regItem[i] == "City")
                    ufi.City = this.city;
                //else
                //    ufi.City = "";

                if (regItem[i] == "Address")
                    ufi.Address = this.address;
                //else
                //    ufi.Address = "";

                if (regItem[i] == "Postcode")
                    ufi.Postcode = string.Empty;
                //else
                //    ufi.Postcode = "";

                if (regItem[i] == "FaTel")
                    ufi.FaTel = this.TelePhone;
                //else
                //    ufi.FaTel = "";

                if (regItem[i] == "WorkTel")
                    ufi.WorkTel = this.TelePhone;
                //else
                //    ufi.WorkTel = "";

                if (regItem[i] == "Fax")
                    ufi.Fax = string.Empty;
                //else
                //    ufi.Fax = "";

                if (regItem[i] == "QQ")
                    ufi.QQ = this.QQ;
                //else
                //    ufi.QQ = "";

                if (regItem[i] == "MSN")
                    ufi.MSN = string.Empty;
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

            if (User.Add_User(ui) == 1 && User.Add_userfields(ufi) == 1 && User.Add_Ghistory(ughi) == 1)
            {
                //CreateFolder
                string Path = string.Empty;
                string Userfiles = Foosun.Config.UIConfig.UserdirFile;
                string _dirdum = Foosun.Config.UIConfig.dirDumm;

                if (_dirdum.Trim() != string.Empty)
                {
                    _dirdum = "/" + _dirdum;
                }
                Path = _dirdum + "/" + Userfiles;
                string CreatePath = this.context.Server.MapPath(Path);
                try
                {
                    Foosun.CMS.Templet.Templet tc = new Foosun.CMS.Templet.Templet();
                    tc.AddDir(CreatePath, ui.UserNum);
                }
                catch { }




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

                    string bodys = "亲爱的" + this.username + ":<br />";
                    bodys += "&nbsp;&nbsp;您注册的用户名：" + this.username + ",用户编号：" + UserNum + ",密码：" + pwd + "<br />";
                    bodys += "&nbsp;&nbsp;请点击此联接激活您的电子邮件:" + Foosun.Publish.CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirUser + "/info" +
                             "/getPassport.aspx?t=mail&e=" + Foosun.Common.Input.MD5(ui.Email, true) + "&" +
                             "u=" + Foosun.Common.Input.MD5(ui.UserNum, true) + "&c=" + ui.EmailCode + "";

                    Foosun.Common.Public.sendMail(EmailSmtpServer, EmailUserName, EmailPwd, EmailFrom, Emailto, subj, bodys);

                }
            }
            else
            {
                this.AddErrStr("创建用户失败！");
            }

        }

        void login()
        {
            GlobalUserInfo info;
            EnumLoginState state = new Foosun.CMS.UserLogin().PersonLogin(username, password, out info);
            if (state != EnumLoginState.Succeed)
                this.AddErrStr("用户名或密码错误！");
        }
        void logout()
        {
            return;
        }
        void update()
        {
            Foosun.CMS.UserMisc userMisc = new UserMisc();
            SysUserInfo userInfo= userMisc.GetUserInfo(this.username);
            if (userInfo == null)
            {
                this.AddErrStr("用户名不存在！");
                return;
            }
            if (!string.IsNullOrEmpty(this.Birthday))
            {
                try
                {
                    userInfo.birthday = DateTime.Parse(this.Birthday);
                }
                catch
                {
                }
            } 
            if (!string.IsNullOrEmpty(this.email))
            {
                userInfo.Email = this.email;
            }
            if (!string.IsNullOrEmpty(this.password))
            {
                userInfo.UserPassword = Foosun.Common.Input.MD5(this.password, true);
            }
            if (!string.IsNullOrEmpty(this.Sex))
            {
                switch (this.Sex)
                {
                    case "0":
                        userInfo.Sex = 2;
                        break;
                    case "1":
                        userInfo.Sex = 1;
                        break;
                    default:
                        userInfo.Sex = 0;
                        break;
                }
            }
            if (!string.IsNullOrEmpty(this.TrueName))
            {
                userInfo.RealName = this.TrueName;
            }
            if (!string.IsNullOrEmpty(this.userip))
            {
                userInfo.LastIP = this.userip;
            }

            if (!string.IsNullOrEmpty(this.address))
            {
                userInfo.Fields.Address = this.address;
            }
            if (!string.IsNullOrEmpty(this.city))
            {
                userInfo.Fields.City = this.city;
            }
            if (!string.IsNullOrEmpty(this.MSN))
            {
                userInfo.Fields.MSN = this.MSN;
            }
            if (!string.IsNullOrEmpty(this.province))
            {
                userInfo.Fields.province = this.province;
            }
            if (!string.IsNullOrEmpty(this.QQ))
            {
                userInfo.Fields.QQ = this.QQ;
            }
            if (!string.IsNullOrEmpty(this.TelePhone))
            {
                userInfo.Fields.FaTel = this.TelePhone;
            }
            
            if(!userMisc.UpdateUserInfo(userInfo))
                this.AddErrStr("同步更新用户信息失败！");
            

        }
        void delete()
        {

        }
        void getinfo()
        {
            Foosun.CMS.UserMisc userMisc = new UserMisc();
            SysUserInfo userInfo = userMisc.GetUserInfo(this.username);
            if (userInfo == null)
            {
                this.AddErrStr("用户名不存在！");
                return;
            }
            this.address = userInfo.Fields.Address;
            this.Birthday = userInfo.birthday.ToString("yyyy-MM-dd");
            this.city = userInfo.Fields.City;
            this.email = userInfo.Email;
            this.MSN = userInfo.Fields.MSN;
            this.province = userInfo.Fields.province;
            this.QQ = userInfo.Fields.QQ;
            switch (userInfo.Sex)
            {
                case 2:
                    this.Sex = "0";
                    break;
                case 1:
                    this.Sex = "1";
                    break;
                default:
                    this.Sex = "2";
                    break;
            }
            this.TelePhone = userInfo.Fields.FaTel;
            this.TrueName = userInfo.RealName;

            this.username = userInfo.UserName;
            
        }
        void checkname()
        {
            Foosun.DALFactory.IUser dal = DataAccess.CreateUser();
            if (dal.sel_username(this.username) != 0)
            {
                this.AddErrStr("用户名已被占用！");
            }
        }
    }
}