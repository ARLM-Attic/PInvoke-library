using System;
using System.Web.UI;

using Discuz.Control;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Data;


namespace Discuz.Web.Admin
{

#if NET1
    public  class addhelp : AdminPage
#else
    public partial class addhelp : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TextBox title;
        protected Discuz.Control.DropDownList type;
        protected Discuz.Web.Admin.OnlineEditor message;
        protected Discuz.Control.TextBox poster;
        protected Discuz.Control.Button Addhelp;
        #endregion
#endif

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if ((this.username != null) && (this.username != ""))
                {
                    poster.Text = this.username;
                    type.AddTableData(Helps.bindhelptype());
                    Addhelp.ValidateForm = true;
                    title.AddAttributes("maxlength", "200");
                    title.AddAttributes("rows", "2");
                    type.DataBind();
                }
            }
        }

        protected void Addhelp_Click(object sender, EventArgs e)
        {
            #region ���Ӱ�����
            if (this.CheckCookie())
            {
                if (int.Parse(type.SelectedItem.Value) == 0)
                {
                    base.RegisterStartupScript( "", "<script>alert('��δѡ���κ�ѡ��');window.location.href='global_addhelp.aspx';</script>");
                }
                else
                {
                    Helps.addhelp(title.Text, message.Text, int.Parse(type.SelectedItem.Value));
                    Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/AnnouncementList");
                    AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "��Ӱ���", "��Ӱ���,����Ϊ:" + title.Text);
            #if NET1
                    if (!base.IsStartupScriptRegistered("page"))
                    {
                        base.RegisterStartupScript("", "<script>window.location.href='global_helplist.aspx';</script>");
                    }
            #else
                    base.RegisterStartupScript("", "<script>window.location.href='global_helplist.aspx';</script>");
            #endif
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
            this.Addhelp.Click += new EventHandler(this.Addhelp_Click);
            this.Load += new EventHandler(this.Page_Load);

        }

        #endregion
    }
}
