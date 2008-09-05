using System;
using System.Web.UI.HtmlControls;
using System.Web.UI;

using Discuz.Control;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Data;
using Discuz.Entity;



namespace Discuz.Web.Admin
{
    /// <summary>
    /// ����û�
    /// </summary>
    
#if NET1
    public class adduser : AdminPage
#else
    public partial class adduser : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TextBox userName;
        protected Discuz.Control.TextBox password;
        protected Discuz.Control.TextBox email;
        protected Discuz.Control.DropDownList groupid;
        protected Discuz.Control.TextBox credits;
        protected Discuz.Control.TextBox realname;
        protected Discuz.Control.TextBox idcard;
        protected Discuz.Control.TextBox mobile;
        protected Discuz.Control.TextBox phone;
        protected System.Web.UI.HtmlControls.HtmlInputCheckBox sendemail;
        protected Discuz.Control.Hint Hint1;
        protected Discuz.Control.Button AddUserInfo;
        #endregion
#endif


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                #region ��ʼ���ؼ�
                groupid.AddTableData(DatabaseProvider.GetInstance().GetGroupInfo());
                AddUserInfo.Attributes.Add("onclick", "return IsValidPost();");
                //�������������ݼ��ص�Javascript���飬��ǰ̨�ı�
                string scriptText = "var creditarray = new Array(";
                for(int i = 1; i < groupid.Items.Count; i++)
                {
                    scriptText += AdminUserGroups.AdminGetUserGroupInfo(Convert.ToInt32(groupid.Items[i].Value)).Creditshigher.ToString() + ",";
                }
                scriptText = scriptText.TrimEnd(',') + ");";
                this.RegisterStartupScript("begin", "<script type='text/javascript'>" + scriptText + "</script>");
                groupid.Attributes.Add("onchange", "document.getElementById('" + credits.ClientID + "').value=creditarray[this.selectedIndex];");
                groupid.Items.RemoveAt(0);
                try
                {
                    groupid.SelectedValue = "10";
                }
                catch
                {
                    if (UserCredits.GetCreditsUserGroupID(0) != null)
                    {
                        groupid.SelectedValue = UserCredits.GetCreditsUserGroupID(0).Groupid.ToString();
                    }
                    else
                    {
                        groupid.SelectedValue = "3";
                    }
                }

                try
                {
                    UserGroupInfo _usergroupinfo = AdminUserGroups.AdminGetUserGroupInfo(Convert.ToInt32(groupid.SelectedValue));
                    credits.Text = _usergroupinfo.Creditshigher.ToString();
                }
                catch
                {
                    ;
                }

                #endregion
            }
        }


        private void AddUserInfo_Click(object sender, EventArgs e)
        {
            #region ������û���Ϣ

            if (this.CheckCookie())
            {
                if (userName.Text.Trim() == "" || password.Text.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('�û���������Ϊ��,����޷��ύ!');window.location.href='global_adduser.aspx';</script>");
                    return;
                }
                if (!Utils.IsSafeSqlString(userName.Text))
                {
                    base.RegisterStartupScript( "", "<script>alert('��������û�����������ȫ���ַ�,����޷��ύ!');window.location.href='global_adduser.aspx';</script>");
                    return;
                }

                if (PrivateMessages.SystemUserName == userName.Text)
                {
                    base.RegisterStartupScript( "", "<script>alert('�����ܴ������û���,��Ϊ����ϵͳ�������û���,���������������û���!');window.location.href='global_adduser.aspx';</script>");
                    return;
                }

                if (!Utils.IsValidEmail(email.Text.Trim()))
                {
                    base.RegisterStartupScript("", "<script>alert('E-mailΪ�ջ��ʽ����ȷ,����޷��ύ!');window.location='global_adduser.aspx';</script>");
                    return;
                }

                int selectgroupid = Convert.ToInt32(groupid.SelectedValue);
                UserInfo __userinfo = new UserInfo();
                __userinfo.Username = userName.Text;
                __userinfo.Nickname = userName.Text;
                __userinfo.Password = Utils.MD5(password.Text);
                __userinfo.Secques = "";
                __userinfo.Gender = 0;
                __userinfo.Adminid = AdminUserGroups.AdminGetUserGroupInfo(selectgroupid).Radminid;
                __userinfo.Groupid = selectgroupid;
                __userinfo.Groupexpiry = 0;
                __userinfo.Extgroupids = "";
                __userinfo.Regip = "";
                __userinfo.Joindate = Utils.GetDate(); //DateTime.Now.ToString();
                __userinfo.Lastip = "";
                __userinfo.Lastvisit = Utils.GetDate(); //DateTime.Now.ToString();
                __userinfo.Lastactivity = Utils.GetDate(); //DateTime.Now.ToString();
                __userinfo.Lastpost = Utils.GetDate(); //DateTime.Now.ToString();
                __userinfo.Lastpostid = 0;
                __userinfo.Lastposttitle = "";
                __userinfo.Posts = 0;
                __userinfo.Digestposts = 0;
                __userinfo.Oltime = 0;
                __userinfo.Pageviews = 0;
                __userinfo.Credits = Convert.ToInt32(credits.Text);
                __userinfo.Extcredits1 = 0;
                __userinfo.Extcredits2 = 0;
                __userinfo.Extcredits3 = 0;
                __userinfo.Extcredits4 = 0;
                __userinfo.Extcredits5 = 0;
                __userinfo.Extcredits6 = 0;
                __userinfo.Extcredits7 = 0;
                __userinfo.Extcredits8 = 0;
                __userinfo.Avatarshowid = 1;
                __userinfo.Email = email.Text;
                __userinfo.Bday = "";
                __userinfo.Sigstatus = 0;

                GeneralConfigInfo __configinfo = GeneralConfigs.Deserialize(Server.MapPath("../../config/general.config"));
                __userinfo.Templateid = __configinfo.Templateid;
                __userinfo.Tpp = 16;
                __userinfo.Ppp = 16;
                __userinfo.Pmsound = 1;
                __userinfo.Showemail = 1;
                __userinfo.Newsletter = (ReceivePMSettingType)7;
                __userinfo.Invisible = 0;
                __userinfo.Newpm = 0;
                __userinfo.Accessmasks = 0;

                //��չ��Ϣ
                __userinfo.Website = "";
                __userinfo.Icq = "";
                __userinfo.Qq = "";
                __userinfo.Yahoo = "";
                __userinfo.Msn = "";
                __userinfo.Skype = "";
                __userinfo.Location = "";
                __userinfo.Customstatus = "";
                __userinfo.Avatar = "";
                __userinfo.Avatarwidth = 32;
                __userinfo.Avatarheight = 32;
                __userinfo.Medals = "";
                __userinfo.Bio = "";
                __userinfo.Signature = userName.Text;
                __userinfo.Sightml = "";
                __userinfo.Authstr = "";
                __userinfo.Realname = realname.Text;
                __userinfo.Idcard = idcard.Text;
                __userinfo.Mobile = mobile.Text;
                __userinfo.Phone = phone.Text;


                if (AdminUsers.GetUserID(userName.Text) != -1)
                {
                    base.RegisterStartupScript( "", "<script>alert('����������û����ѱ�ʹ�ù�, �������������û���!');window.location.href='global_adduser.aspx';</script>");
                    return;
                }

                if (__configinfo.Doublee == 0)
                {
                    if (AdminUsers.FindUserEmail(email.Text) != -1)
                    {
                        base.RegisterStartupScript( "", "<script>alert('��������������ַ�ѱ�ʹ�ù�, ����������������!');window.location.href='global_adduser.aspx';</script>");
                        return;
                    }
                }

                AdminUsers.CreateUser(__userinfo);

                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "��̨����û�", "�û���:" + userName.Text);

                string emailresult = null;
                if (sendemail.Checked)
                {
                    emailresult = SendEmail(email.Text);
                }
                base.RegisterStartupScript( "PAGE", "window.location.href='global_usergrid.aspx';");
            }

            #endregion
        }

        public string SendEmail(string emailaddress)
        {
            #region �����ʼ�

            bool send = Emails.DiscuzSmtpMail(userName.Text, emailaddress, password.Text);

            if (send)
            {
                return "���������Ѿ��ɹ����͵�����E-mail��, ��ע�����!";
            }
            else
            {
                return "�������ʼ�����, ��������ȡ������!";
            }

            #endregion
        }

        #region Web ������������ɵĴ���

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.AddUserInfo.Click += new EventHandler(this.AddUserInfo_Click);
            //this.groupid.SelectedIndexChanged += new EventHandler(this.groupid_SelectedIndexChanged);
            this.Load += new EventHandler(this.Page_Load);

            userName.IsReplaceInvertedComma = false;
            password.IsReplaceInvertedComma = false;
            email.IsReplaceInvertedComma = false;

        }

        #endregion

    }
}