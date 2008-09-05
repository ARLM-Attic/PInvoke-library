using System;
using System.Web.UI;

using Discuz.Control;
using Discuz.Forum;
using Discuz.Config;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ����������. 
    /// </summary>
    
#if NET1
    public class setting : AdminPage
#else
    public partial class setting : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TabControl TabControl1;
        protected Discuz.Control.TabPage tabPage51;
        protected Discuz.Control.RadioButtonList size;
        protected Discuz.Control.RadioButtonList safe;
        protected Discuz.Control.RadioButtonList func;
        protected Discuz.Control.TabPage tabPage22;
        protected Discuz.Control.TextBox forumtitle;
        protected Discuz.Control.TextBox forumurl;
        protected Discuz.Control.TextBox webtitle;
        protected Discuz.Control.TextBox weburl;
        protected Discuz.Control.Button submitsetting;
        #endregion
#endif    
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GeneralConfigInfo __configinfo = GeneralConfigs.Deserialize(Server.MapPath("../../config/general.config"));
                forumtitle.Text = __configinfo.Forumtitle.ToString();
                forumurl.Text = __configinfo.Forumurl.ToString();
                webtitle.Text = __configinfo.Webtitle.ToString();
                weburl.Text = __configinfo.Weburl.ToString().ToLower();

                SetOption(__configinfo);
            }
        }

        public void SetOption(GeneralConfigInfo __configinfo)
        {
            if (__configinfo.Maxonlines == 500) size.SelectedValue = "1";
            if (__configinfo.Maxonlines == 5000) size.SelectedValue = "2";
            if (__configinfo.Maxonlines == 50000) size.SelectedValue = "3";

            if (__configinfo.Regctrl == 0) safe.SelectedValue = "1";
            if (__configinfo.Regctrl == 12) safe.SelectedValue = "2";
            if (__configinfo.Regctrl == 48) safe.SelectedValue = "3";

            if (__configinfo.Visitedforums == 0) func.SelectedValue = "1";
            if (__configinfo.Visitedforums == 10) func.SelectedValue = "2";
            if (__configinfo.Visitedforums == 20) func.SelectedValue = "3";
        }

        private void submitsetting_Click(object sender, EventArgs e)
        {
            if (this.CheckCookie())
            {
                GeneralConfigInfo __configinfo = GeneralConfigs.Deserialize(Server.MapPath("../../config/general.config"));

                #region

                switch (size.SelectedValue)
                {
                    case "1":
                        {
                            __configinfo.Attachsave = 0;
                            __configinfo.Fullmytopics = 0;
                            __configinfo.Maxonlines = 500;
                            __configinfo.Starthreshold = 2;
                            __configinfo.Searchctrl = 10;
                            __configinfo.Hottopic = 10;
                            __configinfo.Losslessdel = 365;
                            __configinfo.Maxmodworksmonths = 5;
                            __configinfo.Moddisplay = 0;
                            __configinfo.Tpp = 30;
                            __configinfo.Ppp = 20;
                            __configinfo.Maxpolloptions = 10;
                            __configinfo.Maxpostsize = 10000;
                            __configinfo.Maxfavorites = 500;
                            __configinfo.Nocacheheaders = 1;
                            __configinfo.Guestcachepagetimeout = 0;
                            __configinfo.Topiccachemark = 0;
                            __configinfo.Postinterval = 5;
                            __configinfo.Maxspm = 5;
                            __configinfo.Fulltextsearch = 0;
                            __configinfo.TopicQueueStats = 0;
                            __configinfo.TopicQueueStatsCount = 20;
                            break;
                        }
                    case "2":
                        {
                            __configinfo.Attachsave = 1;
                            __configinfo.Fullmytopics = 1;
                            __configinfo.Maxonlines = 5000;
                            __configinfo.Starthreshold = 2;
                            __configinfo.Searchctrl = 30;
                            __configinfo.Hottopic = 20;
                            __configinfo.Losslessdel = 200;
                            __configinfo.Maxmodworksmonths = 3;
                            __configinfo.Moddisplay = 0;
                            __configinfo.Tpp = 20;
                            __configinfo.Ppp = 15;
                            __configinfo.Maxpolloptions = 1000;
                            __configinfo.Maxpostsize = 10000;
                            __configinfo.Maxfavorites = 200;
                            __configinfo.Nocacheheaders = 0;
                            __configinfo.Guestcachepagetimeout = 10;
                            __configinfo.Topiccachemark = 20;
                            __configinfo.Postinterval = 10;
                            __configinfo.Maxspm = 4;
                            __configinfo.Fulltextsearch = 0;
                            __configinfo.TopicQueueStats = 1;
                            __configinfo.TopicQueueStatsCount = 30;
                            break;
                        }
                    case "3":
                        {
                            __configinfo.Attachsave = 2;
                            __configinfo.Fullmytopics = 1;
                            __configinfo.Maxonlines = 50000;
                            __configinfo.Starthreshold = 2;
                            __configinfo.Searchctrl = 60;
                            __configinfo.Hottopic = 100;
                            __configinfo.Maxmodworksmonths = 1;
                            __configinfo.Moddisplay = 1;
                            __configinfo.Tpp = 15;
                            __configinfo.Ppp = 10;
                            __configinfo.Maxpolloptions = 20000;
                            __configinfo.Maxfavorites = 100;
                            __configinfo.Nocacheheaders = 0;
                            __configinfo.Guestcachepagetimeout = 20;
                            __configinfo.Topiccachemark = 50;
                            __configinfo.Postinterval = 15;
                            __configinfo.Maxspm = 3;
                            __configinfo.Fulltextsearch = 0;
                            __configinfo.TopicQueueStats = 1;
                            __configinfo.TopicQueueStatsCount = 100;
                            break;
                        }
                }

                #endregion

                #region

                switch (safe.SelectedValue)
                {
                    case "1":
                        {
                            __configinfo.Doublee = 1; //����ͬһ Email ע�᲻ͬ�û�
                            __configinfo.Dupkarmarate = 1; //debug ��ȫ	�����ظ����� (����)
                            __configinfo.Hideprivate = 0; //debug ��ȫ	������Ȩ���ʵ���̳ (����)
                            __configinfo.Memliststatus = 1; //debug ��ȫ	����鿴��Ա�б� (����)
                            __configinfo.Seccodestatus = ""; //debug ��ȫ	������֤��
                            __configinfo.Rules = 0; //debug ��ȫ	ע�����Э��, ���bbrulestxtʹ�� (����)
                            __configinfo.Edittimelimit = 0; //debug ��ȫ	�༭����ʱ������ (����)
                            __configinfo.Karmaratelimit = 0; //debug ��ȫ	����ʱ������ (Сʱ)
                            __configinfo.Regctrl = 0; //debug ��ȫ	ͬһIP ע��������(Сʱ)
                            __configinfo.Regstatus = 1; //debug ��ȫ	�������û�ע�� (����) ?
                            __configinfo.Regverify = 0; //debug ����	���û�ע����֤ (0:ֱ��ע��ɹ� 1:Email ��֤ 2:�˹����)
                            __configinfo.Secques = 5;
                            __configinfo.Defaulteditormode = 0;
                            __configinfo.Allowswitcheditor = 0;
                            __configinfo.Watermarktype = 0;
                            __configinfo.Attachimgquality = 80;
                            break;
                        }
                    case "2":
                        {
                            __configinfo.Attachrefcheck = 1;
                            __configinfo.Doublee = 0; //����ͬһ Email ע�᲻ͬ�û�
                            __configinfo.Dupkarmarate = 0; //debug ��ȫ	�����ظ����� (����)
                            __configinfo.Hideprivate = 1; //debug ��ȫ	������Ȩ���ʵ���̳ (����)
                            __configinfo.Memliststatus = 1; //debug ��ȫ	����鿴��Ա�б� (����)
                            __configinfo.Seccodestatus = "login.aspx"; //debug ��ȫ	������֤��
                            __configinfo.Rules = 1; //debug ��ȫ	ע�����Э��, ���bbrulestxtʹ�� (����)
                            __configinfo.Edittimelimit = 20; //debug ��ȫ	�༭����ʱ������ (����)
                            __configinfo.Karmaratelimit = 1; //debug ��ȫ	����ʱ������ (Сʱ)
                            __configinfo.Newbiespan = 1; //debug ��ȫ	���ּ�ϰ���� (Сʱ)
                            __configinfo.Regctrl = 12; //debug ��ȫ	ͬһIP ע��������(Сʱ)
                            __configinfo.Regstatus = 1; //debug ��ȫ	�������û�ע�� (����) ?
                            __configinfo.Regverify = 1; //debug ����	���û�ע����֤ (0:ֱ��ע��ɹ� 1:Email ��֤ 2:�˹����)
                            __configinfo.Secques = 10;
                            __configinfo.Defaulteditormode = 0;
                            __configinfo.Allowswitcheditor = 1;
                            __configinfo.Watermarktype = 1;
                            __configinfo.Attachimgquality = 85;
                            break;
                        }
                    case "3":
                        {
                            __configinfo.Attachrefcheck = 1;
                            __configinfo.Doublee = 0; //����ͬһ Email ע�᲻ͬ�û�
                            __configinfo.Dupkarmarate = 0; //debug ��ȫ	�����ظ����� (����)
                            __configinfo.Hideprivate = 1; //debug ��ȫ	������Ȩ���ʵ���̳ (����)
                            __configinfo.Memliststatus = 0; //debug ��ȫ	����鿴��Ա�б� (����)
                            __configinfo.Seccodestatus = "login.aspx"; //debug ��ȫ	������֤��
                            __configinfo.Rules = 1; //debug ��ȫ	ע�����Э��, ���bbrulestxtʹ�� (����)
                            __configinfo.Edittimelimit = 10; //debug ��ȫ	�༭����ʱ������ (����)
                            __configinfo.Karmaratelimit = 4; //debug ��ȫ	����ʱ������ (Сʱ)
                            __configinfo.Newbiespan = 4; //debug ��ȫ	���ּ�ϰ���� (Сʱ)
                            __configinfo.Regctrl = 48; //debug ��ȫ	ͬһIP ע��������(Сʱ)
                            __configinfo.Regstatus = 1; //debug ��ȫ	�������û�ע�� (����) ?
                            __configinfo.Regverify = 1; //debug ����	���û�ע����֤ (0:ֱ��ע��ɹ� 1:Email ��֤ 2:�˹����)
                            __configinfo.Secques = 20;
                            __configinfo.Defaulteditormode = 1;
                            __configinfo.Allowswitcheditor = 1;
                            __configinfo.Watermarktype = 1;
                            __configinfo.Attachimgquality = 100;
                            break;
                        }
                }

                #endregion

                #region

                switch (func.SelectedValue)
                {
                    case "1":
                        {
                            __configinfo.Archiverstatus = 0; //debug ����	���� Archiver (����)
                            __configinfo.Attachimgpost = 0; //debug ����	��������ʾͼƬ���� (����)
                            __configinfo.Fastpost = 0; //debug ����	���ٷ��� (����)
                            __configinfo.Editedby = 0; //debug ����	��ʾ�༭��Ϣ (����)
                            __configinfo.Forumjump = 0; //debug ����	��ʾ��̳��ת�˵� (����)
                            __configinfo.Modworkstatus = 0; //debug ����	��̳������ͳ�� (����)
                            __configinfo.Rssstatus = 0; //debug ����	���� RSS
                            __configinfo.Smileyinsert = 0; //debug ����	��ʾ�ɵ�� Smilies , ��smcols����ʹ�ÿ��Կ����Ƿ���ʾ�༭��
                            __configinfo.Stylejump = 0; //debug ����	��ʾ��������˵�
                            __configinfo.Subforumsindex = 0; //debug ����	��ҳ��ʾ��̳���¼�����̳
                            __configinfo.Visitedforums = 0; //debug ����	��ʾ���������̳����
                            __configinfo.Welcomemsg = 0; //debug ����	���ͻ�ӭ����Ϣ
                            __configinfo.Watermarkstatus = 0; //debug ����	ͼƬ�������ˮӡ
                            __configinfo.Whosonlinestatus = 0; //debug ����	������ʾ״̬
                            __configinfo.Debug = 0; //debug ����	debug ��ģʽ
                            __configinfo.Regadvance = 0; //debug ����	�Ƿ���ʾ�߼�ע��ѡ��
                            __configinfo.Showsignatures = 0; //debug ����	�Ƿ���ʾǩ��, ͷ��
                            break;
                        }

                    case "2":
                        {
                            __configinfo.Archiverstatus = 1; //debug ����	���� Archiver (����)
                            __configinfo.Attachimgpost = 1; //debug ����	��������ʾͼƬ���� (����)
                            __configinfo.Fastpost = 1; //debug ����	���ٷ��� (����)
                            __configinfo.Editedby = 1; //debug ����	��ʾ�༭��Ϣ (����)
                            __configinfo.Forumjump = 1; //debug ����	��ʾ��̳��ת�˵� (����)
                            __configinfo.Modworkstatus = 0; //debug ����	��̳������ͳ�� (����)
                            __configinfo.Rssstatus = 1; //debug ����	���� RSS
                            __configinfo.Smileyinsert = 1; //debug ����	��ʾ�ɵ�� Smilies , ��smcols����ʹ�ÿ��Կ����Ƿ���ʾ�༭��
                            __configinfo.Stylejump = 0; //debug ����	��ʾ��������˵�
                            __configinfo.Subforumsindex = 0; //debug ����	��ҳ��ʾ��̳���¼�����̳
                            __configinfo.Visitedforums = 10; //debug ����	��ʾ���������̳����
                            __configinfo.Welcomemsg = 0; //debug ����	���ͻ�ӭ����Ϣ
                            __configinfo.Watermarkstatus = 0; //debug ����	ͼƬ�������ˮӡ
                            __configinfo.Whosonlinestatus = 1; //debug ����	������ʾ״̬
                            __configinfo.Debug = 1; //debug ����	debug ��ģʽ
                            __configinfo.Regadvance = 0; //debug ����	�Ƿ���ʾ�߼�ע��ѡ��
                            __configinfo.Showsignatures = 1; //debug ����	�Ƿ���ʾǩ��, ͷ��
                            break;
                        }
                    case "3":
                        {
                            __configinfo.Archiverstatus = 1; //debug ����	���� Archiver (����)
                            __configinfo.Attachimgpost = 1; //debug ����	��������ʾͼƬ���� (����)
                            __configinfo.Fastpost = 1; //debug ����	���ٷ��� (����)
                            __configinfo.Editedby = 1; //debug ����	��ʾ�༭��Ϣ (����)
                            __configinfo.Forumjump = 1; //debug ����	��ʾ��̳��ת�˵� (����)
                            __configinfo.Modworkstatus = 1; //debug ����	��̳������ͳ�� (����)
                            __configinfo.Rssstatus = 1; //debug ����	���� RSS
                            __configinfo.Smileyinsert = 1; //debug ����	��ʾ�ɵ�� Smilies , ��smcols����ʹ�ÿ��Կ����Ƿ���ʾ�༭��
                            __configinfo.Stylejump = 1; //debug ����	��ʾ��������˵�
                            __configinfo.Subforumsindex = 1; //debug ����	��ҳ��ʾ��̳���¼�����̳
                            __configinfo.Visitedforums = 20; //debug ����	��ʾ���������̳����
                            __configinfo.Welcomemsg = 1; //debug ����	���ͻ�ӭ����Ϣ
                            __configinfo.Watermarkstatus = 1; //debug ����	ͼƬ�������ˮӡ
                            __configinfo.Whosonlinestatus = 1; //debug ����	������ʾ״̬
                            __configinfo.Debug = 1; //debug ����	debug ��ģʽ
                            __configinfo.Regadvance = 1; //debug ����	�Ƿ���ʾ�߼�ע��ѡ��
                            __configinfo.Showsignatures = 1; //debug ����	�Ƿ���ʾǩ��, ͷ��
                            break;
                        }
                }

                #endregion

                __configinfo.Forumtitle = forumtitle.Text.Trim();
                __configinfo.Forumurl = forumurl.Text.Trim().ToLower();
                __configinfo.Webtitle = webtitle.Text.Trim();
                __configinfo.Weburl = weburl.Text.Trim().ToLower();

                GeneralConfigs.Serialiaze(__configinfo, Server.MapPath("../../config/general.config"));
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "��������", "");
                base.RegisterStartupScript( "PAGE",  "window.location.href='setting.aspx';");
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
            this.submitsetting.Click += new EventHandler(this.submitsetting_Click);
            this.Load += new EventHandler(this.Page_Load);

            forumtitle.IsReplaceInvertedComma = false;
            forumurl.IsReplaceInvertedComma = false;
            webtitle.IsReplaceInvertedComma = false;
            weburl.IsReplaceInvertedComma = false;
        }

        #endregion

    }
}