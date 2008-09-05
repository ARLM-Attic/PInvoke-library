using System;
using System.Web.UI;

using Discuz.Control;
using Discuz.Common;
using Discuz.Forum;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// �ϴ��û�ͷ��
    /// </summary>
    
#if NET1
    public class uploadavatar : AdminPage
#else
    public partial class uploadavatar : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.UpFile url;
        protected Discuz.Control.Hint Hint1;
        protected Discuz.Control.Button UpdateAvatarCache;
        #endregion
#endif

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                url.UpFilePath = Server.MapPath(url.UpFilePath);
            }
        }

        private void UpdateAvatarCache_Click(object sender, EventArgs e)
        {
            if (this.CheckCookie())
            {
                Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
                cache.RemoveObject("/Forum/CommonAvatarList");
                url.UpdateFile();
                base.RegisterStartupScript( "PAGE",  "window.location.href='global_avatargrid.aspx';");
            }
        }

        #region Web ������������ɵĴ���

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.UpdateAvatarCache.Click += new EventHandler(this.UpdateAvatarCache_Click);
        }

        #endregion
    }
}