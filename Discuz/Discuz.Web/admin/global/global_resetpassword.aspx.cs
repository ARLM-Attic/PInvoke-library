using System;
using System.Data;
using System.Web.UI;

using Discuz.Control;
using Discuz.Forum;
using Discuz.Common;
using Discuz.Config;
using Discuz.Data;



namespace Discuz.Web.Admin
{
    /// <summary>
    /// �����û�����
    /// </summary>
    
#if NET1
    public class resetpassword : AdminPage
#else
    public partial class resetpassword : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TextBox userName;
        protected Discuz.Control.TextBox password;
        protected Discuz.Control.TextBox passwordagain;
        protected Discuz.Control.Button ResetUserPWs;
        #endregion
#endif


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (DNTRequest.GetString("uid") == "")
                {
                    Response.Write("�û�UID����Ϊ��");
                    Response.End();
                    return;
                }
                else
                {
                    #region �����û�����Ϣ

                    //DataTable dt = DbHelper.ExecuteDataset("SELECT TOP 1 [username] FROM [" + BaseConfigs.GetTablePrefix + "users] WHERE [uid]=" + DNTRequest.GetString("uid")).Tables[0];
                    DataTable dt = DatabaseProvider.GetInstance().GetUserNameByUid(int.Parse(DNTRequest.GetString("uid")));
                    if (dt.Rows.Count > 0)
                    {
                        userName.Text = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        Response.Write("��ǰ�û�������");
                        Response.End();
                        return;
                    }

                    #endregion
                }
            }
        }


        private void ResetUserPWs_Click(object sender, EventArgs e)
        {
            #region �����û�����

            if (password.Text == passwordagain.Text)
            {
                //DbHelper.ExecuteNonQuery("UPDATE [" + BaseConfigs.GetTablePrefix + "users] SET [password]='" + Utils.MD5(password.Text.Trim()) + "' WHERE [uid]=" + DNTRequest.GetString("uid"));
                DatabaseProvider.GetInstance().ResetPasswordUid(Utils.MD5(password.Text.Trim()), int.Parse(DNTRequest.GetString("uid")));
                base.RegisterStartupScript( "PAGE",  "window.location.href='global_edituser.aspx?uid=" + DNTRequest.GetString("uid") + "';");
            }
            else
            {
                base.RegisterStartupScript( "", "<script>alert('������������벻��ͬ,�����������µ�����');</script>");
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
            this.ResetUserPWs.Click += new EventHandler(this.ResetUserPWs_Click);
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion

    }
}