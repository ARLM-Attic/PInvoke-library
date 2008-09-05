using System;
using System.Web.UI;

using Discuz.Control;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Data;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ���ӹ���
    /// </summary>
#if NET1
    public class addannounce : AdminPage
#else
    public partial class addannounce : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TextBox displayorder;
        protected Discuz.Control.TextBox title;
        protected Discuz.Control.TextBox starttime;
        protected Discuz.Control.TextBox endtime;
        protected Discuz.Web.Admin.OnlineEditor message;
        protected Discuz.Control.Button AddAnnounceInfo;
        protected Discuz.Control.TextBox poster;
        #endregion
#endif

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if ((this.username != null) && (this.username != ""))
                {
                    poster.Text = this.username;
                    starttime.Text = DateTime.Now.ToString();
                    endtime.Text = DateTime.Now.AddDays(7).ToString();
                    AddAnnounceInfo.ValidateForm = true;
                    title.AddAttributes("maxlength", "200");
                    title.AddAttributes("rows", "2");
                }
            }
        }

        private void AddAnnounceInfo_Click(object sender, EventArgs e)
        {
            #region ��ӹ���

            if (this.CheckCookie())
            {

                DatabaseProvider.GetInstance().AddAnnouncement(this.username, this.userid, title.Text, Utils.StrToInt(displayorder.Text, 0), starttime.Text, endtime.Text, message.Text);

                Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/AnnouncementList");

                Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/SimplifiedAnnouncementList");

                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "��ӹ���", "��ӹ���,����Ϊ:" + title.Text);

                base.RegisterStartupScript( "PAGE", "window.location.href='global_announcegrid.aspx';");
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
            this.AddAnnounceInfo.Click += new EventHandler(this.AddAnnounceInfo_Click);
            this.Load += new EventHandler(this.Page_Load);

        }

        #endregion


    }
}