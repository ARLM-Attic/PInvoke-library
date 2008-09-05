using System;
using System.Web.UI;

using Discuz.Control;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Data;
using Discuz.Entity;


namespace Discuz.Web.Admin
{

#if NET1
    public class addhelpclass : AdminPage
#else
    public partial class addhelpclass : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TextBox title;
        protected Discuz.Control.TextBox poster;
        protected Discuz.Control.Button add;
        #endregion
#endif

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if ((this.username != null) && (this.username != ""))
                {
                       poster.Text = this.username;
                     //   add.ValidateForm = true;
                }
            }
        }

        protected void add_Click(object sender, EventArgs e)
        {
            #region ���Ӱ������
            if (this.CheckCookie())
            {

                Helps.addhelp(title.Text,"", 0);

                Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/helplist");



                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "��Ӱ�������", "��Ӱ�������,����Ϊ:" + title.Text);
                
            #if NET1
                    if (!base.IsStartupScriptRegistered("page"))
                    {
                        base.RegisterStartupScript("", "<script>window.location.href='global_helplist.aspx';</script>");
                    }
#else
                base.RegisterStartupScript("", "<script>window.location.href='global_helplist.aspx';</script>");
#endif

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
          //  this.add.Click += new EventHandler(this.add_Click);
            this.Load += new EventHandler(this.Page_Load);

        }

        #endregion
    }
}
