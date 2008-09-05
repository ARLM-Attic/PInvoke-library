using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

using Discuz.Forum;
using Button = Discuz.Control.Button;
using TextBox = Discuz.Control.TextBox;
using Discuz.Config;
using Discuz.Data;
using Discuz.Common;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ������̳ͳ��
    /// </summary>
    
#if NET1
    public class updateforumstatic : AdminPage
#else
    public partial class updateforumstatic : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TextBox pertask1;
        protected Discuz.Control.TextBox pertask2;
        protected Discuz.Control.TextBox pertask3;
        protected Discuz.Control.TextBox pertask4;
        protected Discuz.Control.TextBox startfid;
        protected Discuz.Control.TextBox endfid;
        protected Discuz.Control.TextBox startuid_digest;
        protected Discuz.Control.TextBox enduid_digest;
        protected Discuz.Control.TextBox startuid_post;
        protected Discuz.Control.TextBox enduid_post;
        protected Discuz.Control.TextBox starttid;
        protected Discuz.Control.TextBox endtid;
        protected Discuz.Control.Button SubmitClearFlag;
        protected Discuz.Control.Button ReSetStatistic;
        protected Discuz.Control.Button SysteAutoSet;
        protected Discuz.Control.Button UpdatePostSP;
        protected Discuz.Control.Button UpdatePostMaxMinTid;
        protected Discuz.Control.Button CreateFullTextIndex;
        protected Discuz.Control.Button UpdateCurTopics;
        protected System.Web.UI.WebControls.Panel UpdateStoreProcPanel;
        protected Discuz.Control.Button UpdateMyTopic;
        protected Discuz.Control.Button UpdateMyPost;
        #endregion
#endif


        #region �ؼ�����

        protected TextBox startfid_id;
        protected TextBox endfid_id;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (DbHelper.Provider.IsStoreProc())
                UpdateStoreProcPanel.Visible = true;
            else
                UpdateStoreProcPanel.Visible = false;
            if (!Page.IsPostBack)
            { }
        }


        private void SubmitClearFlag_Click(object sender, EventArgs e)
        {
            #region �����ƶ����

            if (this.CheckCookie())
            {
                AdminForumStats.ReSetClearMove();
                base.RegisterStartupScript( "", "<script language=javascript>clearflag();</script>");
            }

            #endregion
        }


        private void ReSetStatistic_Click(object sender, EventArgs e)
        {
            #region �ؽ���̳ͳ��(��)����

            if (this.CheckCookie())
            {
                AdminForumStats.ReSetStatistic();
                AdminCaches.ReSetStatistics();
                base.RegisterStartupScript( "", "<script language=javascript>clearflag();</script>");
            }

            #endregion
        }

        private void SysteAutoSet_Click(object sender, EventArgs e)
        {
            #region ϵͳ������̳���

            if (this.CheckCookie())
            {
                AdminForums.SetForumslayer();
                AdminForums.SetForumsSubForumCountAndDispalyorder();
                AdminForums.SetForumsPathList();
                AdminForums.SetForumsStatus();
                AdminCaches.ReSetForumLinkList();
                AdminCaches.ReSetForumList();
                AdminCaches.ReSetForumListBoxOptions();

                base.RegisterStartupScript( "", "<script language=javascript>clearflag();</script>");
            }

            #endregion
        }

        private void UpdatePostSP_Click(object sender, EventArgs e)
        {
            if (this.CheckCookie())
            {
                UpdatePostStoreProc();
                base.RegisterStartupScript("", "<script language=javascript>clearflag();</script>");
            }
        }

        public void UpdatePostStoreProc()
        {
            if(DbHelper.Provider.IsStoreProc())
                DatabaseProvider.GetInstance().UpdatePostSP();
        }

        private void UpdatePostMaxMinTid_Click(object sender, EventArgs e)
        {
            #region �������ӷֱ�����ص����/С������ID

            if (this.CheckCookie())
            {
                string tableprefix = BaseConfigs.GetTablePrefix + "posts";
                foreach (DataRow dr in DatabaseProvider.GetInstance().GetDatechTableIds())
                {
                    //�Գ���ǰ��֮������ӱ����ͳ��
                    if (Posts.GetPostTableName() != (tableprefix + dr["id"].ToString()))
                    {
                        //���µ�ǰ�������ID�ļ�¼�õ�������Сtid�ֶ�		
                        DatabaseProvider.GetInstance().UpdateMinMaxField(tableprefix + dr["id"].ToString(), Utils.StrToInt(dr["id"], 0));
                    }
                }
                base.RegisterStartupScript( "", "<script language=javascript>clearflag();</script>");
            }

            #endregion
        }

        public void UpdateCurTopics_Click(object sender, EventArgs e)
        {
            #region �������а��ĵ�ǰ����

            foreach (DataRow dr in DatabaseProvider.GetInstance().GetForumIdList())
            {
                Forums.SetRealCurrentTopics(Convert.ToInt32(dr["fid"]));
            }

            #endregion
        }

        public void UpdateForumLastPost_Click(object sender, EventArgs e)
        {
            #region ���°�������

            foreach (Discuz.Entity.ForumInfo foruminfo in Forums.GetForumList())
            {
                Forums.UpdateLastPost(foruminfo);
            }

            #endregion
        }

        
        public void CreateFullTextIndex_Click(object sender, EventArgs e)
        {
            #region ����ȫ������

            if (this.CheckCookie())
            {
                if (DbHelper.Provider.IsFullTextSearchEnabled()==false)
                {

                    return;
                
                }

                try
                {
                    DatabaseProvider.GetInstance().CreateFullTextIndex(DatabaseProvider.GetInstance().GetDbName());

                    aysncallback = new delegateCreateFillIndex(CreateFullText);
                    AsyncCallback myCallBack = new AsyncCallback(CallBack);
                    aysncallback.BeginInvoke(DatabaseProvider.GetInstance().GetDbName(), myCallBack, this.username); //
                    base.LoadRegisterStartupScript("PAGE", "window.location.href='forum_updateforumstatic.aspx';");
                }
                catch (Exception ex)
                {
                    string message = ex.Message.Replace("'", " ");
                    message = message.Replace("\\", "/");
                    message = message.Replace("\r\n", "\\r\\n");
                    message = message.Replace("\r", "\\r");
                    message = message.Replace("\n", "\\n");
                    base.RegisterStartupScript( "", "<script>alert('" + message + "');</script>");
                }
            }

            #endregion
        }

        #region �첽�����������������Ĵ���

        private delegate void delegateCreateFillIndex(string DbName);

        //�첽�����������������Ĵ���
        private delegateCreateFillIndex aysncallback;

        public void CallBack(IAsyncResult e)
        {
            aysncallback.EndInvoke(e);
        }

        public void CreateFullText(string DbName)
        {            
            foreach (DataRow dr in Posts.GetAllPostTableName().Rows)
            {
                DatabaseProvider.GetInstance().CreateORFillIndex(DbName, dr["id"].ToString());
            }
        }
        private void UpdateMyTopic_Click(object sender, EventArgs e)
        {
            DatabaseProvider.GetInstance().UpdateMyTopic();
        }

        private void UpdateMyPost_Click(object sender, EventArgs e)
        {
            DatabaseProvider.GetInstance().UpdateMyPost();
        }
        #endregion


        #region Web ������������ɵĴ���

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SysteAutoSet.Click += new EventHandler(this.SysteAutoSet_Click);
            this.SubmitClearFlag.Click += new EventHandler(this.SubmitClearFlag_Click);
            this.UpdatePostSP.Click += new EventHandler(this.UpdatePostSP_Click);
            this.UpdatePostMaxMinTid.Click += new EventHandler(this.UpdatePostMaxMinTid_Click);
            this.CreateFullTextIndex.Click += new EventHandler(this.CreateFullTextIndex_Click);
            this.ReSetStatistic.Click += new EventHandler(this.ReSetStatistic_Click);
            this.UpdateCurTopics.Click += new EventHandler(this.UpdateCurTopics_Click);
            this.UpdateMyTopic.Click += new EventHandler(this.UpdateMyTopic_Click);
            this.UpdateMyPost.Click += new EventHandler(this.UpdateMyPost_Click);
            this.UpdateForumLastPost.Click += new EventHandler(this.UpdateForumLastPost_Click);
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion
    }
}
