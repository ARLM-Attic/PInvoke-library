using System;
using System.Web.UI;

using Discuz.Common;
using Discuz.Forum;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ɾ�����
    /// </summary>
    
#if NET1
    public class delforums : AdminPage
#else
    public partial class delforums : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected System.Web.UI.HtmlControls.HtmlForm Form1;
        #endregion
#endif

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                #region ��FIDɾ����Ӧ�İ��

                if (AdminForums.DeleteForumsByFid(DNTRequest.GetString("fid")))
                {
                    AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "ɾ����̳���", "ɾ����̳���,fidΪ:" + DNTRequest.GetString("fid"));
                    base.RegisterStartupScript( "", "<script>window.location.href='forum_ForumsTree.aspx';</script>");
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('�Բ���,��ǰ�ڵ����滹���ӽ��,��˲���ɾ����');window.location.href='forum_ForumsTree.aspx';</script>");
                }

                #endregion
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
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion
    }
}