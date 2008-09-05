using System;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;

using Discuz.Control;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Data;
using Discuz.Entity;



namespace Discuz.Web.Admin
{
    /// <summary>
    /// ����ָ����SQL���
    /// </summary>
    
#if NET1
    public class runsql : AdminPage
#else
    public partial class runsql : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Web.Admin.TextareaResize sqlstring;
        protected Discuz.Control.Button RunSqlString;
        #endregion
#endif


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!base.IsFounderUid(userid))
                {
                    Response.Write(base.GetShowMessage());
                    Response.End();
                    return;
                }
            }
        }

        private void RunSqlString_Click(object sender, EventArgs e)
        {
            #region ����ָ����SQL���

            if (this.CheckCookie())
            {
                if (!base.IsFounderUid(userid))
                {
                    Response.Write(base.GetShowMessage());
                    Response.End();
                    return;
                }

                if (sqlstring.Text.Trim() == "")
                {
                    base.RegisterStartupScript( "", "<script language=\"javascript\">alert('��������SQL���!');</script>");
                    return;
                }

                string message = DatabaseProvider.GetInstance().RunSql(sqlstring.Text.Replace("dnt_", BaseConfigs.GetTablePrefix));
                if (message != string.Empty)
                {
                    base.RegisterStartupScript("", "<script language=\"javascript\">showalert('" + message + "');</script>");
                    return;
                }

                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "����SQL���", sqlstring.Text);
                base.RegisterStartupScript( "PAGE", "window.location.href='global_runsql.aspx';");
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
            this.RunSqlString.Click += new EventHandler(this.RunSqlString_Click);
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion
    }
}