using System;
using System.Web.UI;

using Discuz.Control;
using Discuz.Forum;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// �ϲ��û�
    /// </summary>
    
#if NET1
    public class combinationuser : AdminPage
#else
    public partial class combinationuser : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TextBox username1;
        protected Discuz.Control.TextBox username2;
        protected Discuz.Control.TextBox username3;
        protected Discuz.Control.TextBox targetusername;
        protected Discuz.Control.Hint Hint1;
        protected Discuz.Control.Button CombinationUserInfo;
        #endregion
#endif


        protected void Page_Load(object sender, EventArgs e)
        { }


        private void CombinationUserInfo_Click(object sender, EventArgs e)
        {
            #region �ϲ��û�

            if (this.CheckCookie())
            {
                int targetuid = AdminUsers.GetuidByusername(targetusername.Text);
                string result = null;
                if (targetuid > 0)
                {
                    int srcuid = 0;
                    if ((username1.Text != "") && (targetusername.Text.Trim() != username1.Text.Trim()))
                    {
                        srcuid = AdminUsers.GetuidByusername(username1.Text);
                        if (srcuid > 0)
                        {
                            AdminUsers.CombinationUser(srcuid, targetuid);
                            AdminUsers.UpdateForumsFieldModerators(username1.Text);
                            AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "�ϲ��û�", "���û�" + username1.Text + " �ϲ���" + targetusername.Text);
                        }
                        else
                        {
                            result += "�û�:" + username1.Text + "������!,";
                        }
                    }

                    srcuid = 0;
                    if ((username2.Text != "") && (targetusername.Text.Trim() != username2.Text.Trim()))
                    {
                        srcuid = AdminUsers.GetuidByusername(username2.Text);
                        if (srcuid > 0)
                        {
                            AdminUsers.CombinationUser(srcuid, targetuid);
                            AdminUsers.UpdateForumsFieldModerators(username2.Text);
                            AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "�ϲ��û�", "���û�" + username2.Text + " �ϲ���" + targetusername.Text);
                        }
                        else
                        {
                            result += "�û�:" + username2.Text + "������!,";
                        }
                    }

                    srcuid = 0;
                    if ((username3.Text != "") && (targetusername.Text.Trim() != username3.Text.Trim()))
                    {
                        srcuid = AdminUsers.GetuidByusername(username3.Text);
                        if (srcuid > 0)
                        {
                            AdminUsers.CombinationUser(srcuid, targetuid);
                            AdminUsers.UpdateForumsFieldModerators(username3.Text);
                            AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "�ϲ��û�", "���û�" + username3.Text + " �ϲ���" + targetusername.Text);
                        }
                        else
                        {
                            result += "�û�:" + username3.Text + "������!,";
                        }
                    }
                }
                else
                {
                    result += "Ŀ���û�:" + targetusername.Text + "������!,";
                }

                if (result == null)
                {
                    base.RegisterStartupScript( "PAGE",  "window.location.href='global_usergrid.aspx';");
                }
                else
                {
                    result = result.Replace("'", "��");
                    base.RegisterStartupScript( "", "<script>alert('" + result + "');</script>");
                }
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
            this.CombinationUserInfo.Click += new EventHandler(this.CombinationUserInfo_Click);
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion

    }
}