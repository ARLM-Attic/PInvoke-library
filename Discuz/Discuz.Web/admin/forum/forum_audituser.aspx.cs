using System;
using System.Data;
using System.Data.Common;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

using Discuz.Common;
using Discuz.Data;
using Discuz.Forum;
using Button = Discuz.Control.Button;
using DataGrid = Discuz.Control.DataGrid;
using TextBox = Discuz.Control.TextBox;
using Discuz.Config;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ����û�
    /// </summary>

#if NET1
    public class auditnewuser : AdminPage
#else
    public partial class auditnewuser : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TextBox regbefore;
        protected Discuz.Control.TextBox regip;
        protected Discuz.Control.Button clearuser;
        protected Discuz.Control.Button SelectPass;
        protected Discuz.Control.Button SelectDelete;
        protected Discuz.Control.Button AllPass;
        protected Discuz.Control.Button AllDelete;
        protected System.Web.UI.HtmlControls.HtmlInputCheckBox sendemail;
        protected Discuz.Control.DataGrid DataGrid1;
        #endregion
#endif


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        public void BindData()
        {
            #region ������û��б�
            DataGrid1.AllowCustomPaging = false;
            DataGrid1.TableHeaderName = "����û��б�";
            DataGrid1.DataKeyField = "uid";
            DataGrid1.BindData(DatabaseProvider.GetInstance().GetAudituserSql());
            #endregion
        }

        protected void Sort_Grid(Object sender, DataGridSortCommandEventArgs e)
        {
            DataGrid1.Sort = e.SortExpression.ToString();
        }

        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.LoadCurrentPageIndex(e.NewPageIndex);
        }


        private void SelectPass_Click(object sender, EventArgs e)
        {
            #region ��ѡ���û���������Ӧ���û���

            if (this.CheckCookie())
            {
                string uidlist = DNTRequest.GetString("uid");
                if (uidlist != "")
                {
                    //���û���������Ӧ���û���
                    if (UserCredits.GetCreditsUserGroupID(0) != null)
                    {
                        int tmpGroupID = UserCredits.GetCreditsUserGroupID(0).Groupid; //���ע���û���˻��ƺ���Ҫ�޸�
                        //DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "users] SET [groupid]=" + tmpGroupID.ToString() + "  WHERE [uid] IN (" + uidlist + ")");
                        DatabaseProvider.GetInstance().ChangeUserGroupByUid(tmpGroupID, uidlist);

                        foreach (string uid in uidlist.Split(','))
                        {
                            UserCredits.UpdateUserCredits(Convert.ToInt32(uid));
                        }

                        //DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "userfields] SET [authstr]=''  WHERE [uid] IN (" + uidlist + ")");
                        DatabaseProvider.GetInstance().ClearAuthstrByUidlist(uidlist);
                    }
                    if (sendemail.Checked)
                    {
                        SendEmail(uidlist);
                    }
                    base.RegisterStartupScript( "PAGE", "window.location='forum_audituser.aspx';");
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('��ѡ����Ӧ���û�!');window.location='forum_audituser.aspx';</script>");
                }
            }

            #endregion
        }

        private void SelectDelete_Click(object sender, EventArgs e)
        {
            #region ɾ��ѡ�е��û���Ϣ

            if (this.CheckCookie())
            {
                string uidlist = DNTRequest.GetString("uid");
                if (uidlist != "")
                {
                    //DbHelper.ExecuteNonQuery("DELETE FROM [" + BaseConfigs.GetTablePrefix + "userfields] WHERE [uid] IN(" + uidlist + ")");
                    //DbHelper.ExecuteNonQuery("DELETE FROM [" + BaseConfigs.GetTablePrefix + "users] WHERE [uid] IN(" + uidlist + ")");
                    DatabaseProvider.GetInstance().DeleteUserByUidlist(uidlist);
                    DatabaseProvider.GetInstance().LessenTotalUsers();
                    base.RegisterStartupScript("PAGE", "window.location='forum_audituser.aspx';");
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('��ѡ����Ӧ���û�!');window.location='forum_audituser.aspx';</script>");
                }
            }

            #endregion
        }

        private void AllPass_Click(object sender, EventArgs e)
        {
            #region ���û���������Ӧ���û���

            if (this.CheckCookie())
            {
                if (UserCredits.GetCreditsUserGroupID(0) != null)
                {
                    int tmpGroupID = UserCredits.GetCreditsUserGroupID(0).Groupid; //���ע���û���˻��ƺ���Ҫ�޸�
                    //DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "users] SET [groupid]=" + tmpGroupID.ToString() + "  WHERE [groupid]=8");
                    DatabaseProvider.GetInstance().ChangeUsergroup(8, tmpGroupID);
                    //foreach (DataRow dr in DbHelper.ExecuteDataset("SELECT [uid] FROM [" + BaseConfigs.GetTablePrefix + "users] WHERE [groupid]=8").Tables[0].Rows)
                    foreach (DataRow dr in DatabaseProvider.GetInstance().GetAudituserUid().Tables[0].Rows)
                    {
                        UserCredits.UpdateUserCredits(Convert.ToInt32(dr["uid"].ToString()));
                    }
                    //DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "userfields] SET [authstr]=''  WHERE [uid] IN (SELECT [uid] FROM [" + BaseConfigs.GetTablePrefix + "users] WHERE [groupid]=8 )");
                    DatabaseProvider.GetInstance().ClearAllUserAuthstr();
                }

                if (sendemail.Checked)
                {
                    SendEmail();
                }
                base.RegisterStartupScript("PAGE", "window.location='forum_audituser.aspx';");
            }

            #endregion
        }

        private void AllDelete_Click(object sender, EventArgs e)
        {
            #region ɾ�����д�����û������Ϣ

            if (this.CheckCookie())
            {
                //DbHelper.ExecuteNonQuery("DELETE FROM [" + BaseConfigs.GetTablePrefix + "userfields] WHERE [uid] IN (SELECT [uid] FROM [" + BaseConfigs.GetTablePrefix + "users] WHERE [groupid]=8 )");
                //DbHelper.ExecuteNonQuery("DELETE FROM [" + BaseConfigs.GetTablePrefix + "users] WHERE [groupid]=8 ");
                DatabaseProvider.GetInstance().DeleteAuditUser();
                base.RegisterStartupScript("PAGE", "window.location='forum_audituser.aspx';");
            }

            #endregion
        }

        public void SendEmail()
        {
            #region ������ͨ����˵��û������ʼ�

            //foreach (DataRow dr in DbHelper.ExecuteDataset("SELECT [username],[password],[email] FROM [" + BaseConfigs.GetTablePrefix + "users] WHERE [groupid]=8").Tables[0].Rows)
            foreach (DataRow dr in DatabaseProvider.GetInstance().GetAuditUserEmail().Rows)
            {
                Emails.DiscuzSmtpMail(dr["username"].ToString().Trim(), dr["email"].ToString().Trim(), dr["password"].ToString().Trim());
            }

            #endregion
        }

        public void SendEmail(string uidlist)
        {
            #region ��ָ����ͨ����˵��û������ʼ�

            //foreach (DataRow dr in DbHelper.ExecuteDataset("SELECT [username],[password],[email] FROM [" + BaseConfigs.GetTablePrefix + "users] WHERE [uid] IN(" + uidlist + ")").Tables[0].Rows)
            foreach (DataRow dr in DatabaseProvider.GetInstance().GetUserEmailByUidlist(uidlist).Rows)
            {
                Emails.DiscuzSmtpMail(dr["username"].ToString().Trim(), dr["email"].ToString().Trim(), "");
            }

            #endregion
        }

        protected void searchuser_Click(object sender, System.EventArgs e)
        {
            if (this.CheckCookie())
            {
                string sqlstring = string.Empty;
                sqlstring = DatabaseProvider.GetInstance().AuditNewUserClear(searchusername.Text, regbefore.Text, regip.Text);
                DataGrid1.BindData(sqlstring);
            }
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SelectPass.Click += new EventHandler(this.SelectPass_Click);
            this.SelectDelete.Click += new EventHandler(this.SelectDelete_Click);
            this.AllPass.Click += new EventHandler(this.AllPass_Click);
            this.AllDelete.Click += new EventHandler(this.AllDelete_Click);
           // this.Load += new EventHandler(this.Page_Load);

            DataGrid1.DataKeyField = "uid";
            DataGrid1.TableHeaderName = "����û��б�";
            DataGrid1.ColumnSpan = 8;
        }

        #endregion
    }
}