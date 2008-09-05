using System;
using System.Web.UI;
using System.Data;

using Discuz.Control;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Data;
using Discuz.Common;
using Discuz.Plugin.Space;
using Discuz.Plugin.Album;
using Discuz.Plugin.Mall;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ��������
    /// </summary>
    
#if NET1
    public class baseset : AdminPage
#else
    public partial class baseset : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TextBox forumtitle;
        protected Discuz.Control.TextBox forumurl;
        protected Discuz.Control.TextBox webtitle;
        protected Discuz.Control.TextBox weburl;
        protected Discuz.Control.TextBox icp;
        protected Discuz.Control.RadioButtonList licensed;
        protected Discuz.Control.RadioButtonList rssstatus;
        protected Discuz.Control.RadioButtonList sitemapstatus;
        protected Discuz.Control.RadioButtonList debug;
        protected Discuz.Control.TextBox rssttl;
        protected Discuz.Control.TextBox sitemapttl;
        protected Discuz.Control.RadioButtonList nocacheheaders;
        protected Discuz.Control.TextBox extname;
        protected Discuz.Control.RadioButtonList fulltextsearch;
        protected Discuz.Control.RadioButtonList enablesilverlight;
        protected Discuz.Control.RadioButtonList EnableSpace;
        protected Discuz.Control.RadioButtonList EnableAlbum;
        protected Discuz.Control.RadioButtonList passwordmode;
        protected Discuz.Control.RadioButtonList bbcodemode;
        protected Discuz.Control.TextBox CookieDomain;
        protected Discuz.Control.RadioButtonList memliststatus;
        protected Discuz.Control.RadioButtonList cachelog;
        protected Discuz.Control.RadioButtonList Indexpage;
        protected Discuz.Web.Admin.TextareaResize Linktext;
        protected Discuz.Control.RadioButtonList closed;
        protected Discuz.Web.Admin.TextareaResize closedreason;
        protected Discuz.Control.Hint Hint1;
        protected Discuz.Control.Button SaveInfo;

        protected Discuz.Control.TextBox spacename;
        protected Discuz.Control.TextBox spaceurl;
        protected Discuz.Control.TextBox albumname;
        protected Discuz.Control.TextBox albumurl; 
        protected Discuz.Control.RadioButtonList aspxrewrite;
        #endregion
#endif
        protected bool haveAlbum;
        protected bool haveSpace;
        protected bool haveMall;
        protected void Page_Load(object sender, EventArgs e)
        {
            haveAlbum = AlbumPluginProvider.GetInstance() != null;
            haveSpace = SpacePluginProvider.GetInstance() != null;
            haveMall = MallPluginProvider.GetInstance() != null;
            if (!Page.IsPostBack)
            {
                LoadConfigInfo();
                closed.Items[0].Attributes.Add("onclick", "setStatus(true)");
                closed.Items[1].Attributes.Add("onclick", "setStatus(false)");
            }
        }

        public void LoadConfigInfo()
        {
            #region ����������Ϣ

            GeneralConfigInfo __configinfo = GeneralConfigs.Deserialize(Server.MapPath("../../config/general.config"));
            forumtitle.Text = __configinfo.Forumtitle.ToString();
            forumurl.Text = __configinfo.Forumurl.ToString();
            webtitle.Text = __configinfo.Webtitle.ToString();
            weburl.Text = __configinfo.Weburl.ToString();
            licensed.SelectedValue = __configinfo.Licensed.ToString();
            closed.SelectedValue = __configinfo.Closed.ToString();
            closedreason.Text = __configinfo.Closedreason.ToString();
            icp.Text = __configinfo.Icp.ToString();
            rssttl.Text = __configinfo.Rssttl.ToString();
            sitemapttl.Text = __configinfo.Sitemapttl.ToString();
            nocacheheaders.SelectedValue = __configinfo.Nocacheheaders.ToString();
            debug.SelectedValue = __configinfo.Debug.ToString();
            rssstatus.SelectedValue = __configinfo.Rssstatus.ToString();
            sitemapstatus.SelectedValue = __configinfo.Sitemapstatus.ToString();
            cachelog.SelectedValue = "0";
            fulltextsearch.SelectedValue = __configinfo.Fulltextsearch.ToString();
            passwordmode.SelectedValue = __configinfo.Passwordmode.ToString();
            bbcodemode.SelectedValue = __configinfo.Bbcodemode.ToString();
            extname.Text = __configinfo.Extname.Trim();
            enablesilverlight.SelectedValue = __configinfo.Silverlight.ToString();
            EnableSpace.SelectedValue = __configinfo.Enablespace.ToString();
            EnableAlbum.SelectedValue = __configinfo.Enablealbum.ToString();
            EnableMall.SelectedValue = __configinfo.Enablemall.ToString();
            CookieDomain.Text = __configinfo.CookieDomain.ToString();
            memliststatus.SelectedValue = __configinfo.Memliststatus.ToString();
            Indexpage.SelectedIndex = Convert.ToInt32(__configinfo.Indexpage.ToString());
            Linktext.Text = __configinfo.Linktext;
            Statcode.Text = __configinfo.Statcode;
            spacename.Text = __configinfo.Spacename.ToString();
            spaceurl.Text = __configinfo.Spaceurl.ToString();
            albumname.Text = __configinfo.Albumname.ToString();
            albumurl.Text = __configinfo.Albumurl.ToString();
            aspxrewrite.SelectedValue = __configinfo.Aspxrewrite.ToString();
            if (SpacePluginProvider.GetInstance() == null)
            {
                EnableSpace.Visible = false;
                EnableSpaceLabel.Visible = false;
            }
            if (AlbumPluginProvider.GetInstance() == null)
            {
                EnableAlbum.Visible = false;
                EnableAlbumLabel.Visible = false;
            }
            #endregion
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            #region ������Ϣ
            if (this.CheckCookie())
            {
                GeneralConfigInfo __configinfo = GeneralConfigs.Deserialize(Server.MapPath("../../config/general.config"));
                __configinfo.Forumtitle = forumtitle.Text;
                __configinfo.Forumurl = forumurl.Text;
                if ((__configinfo.Forumurl == "") || !__configinfo.Forumurl.EndsWith(".aspx"))
                {
                    base.RegisterStartupScript("", "<script>alert('��̳URL��ַ����Ϊ���ұ�����\".aspx��β\"!');</script>");
                    return;
                }


                __configinfo.Webtitle = webtitle.Text;
                __configinfo.Weburl = weburl.Text;
                __configinfo.Licensed = Convert.ToInt16(licensed.SelectedValue);
                __configinfo.Closed = Convert.ToInt16(closed.SelectedValue);
                __configinfo.Closedreason = closedreason.Text;
                __configinfo.Icp = icp.Text;
                __configinfo.Rssttl = Convert.ToInt32(rssttl.Text);
                __configinfo.Sitemapttl = Convert.ToInt32(sitemapttl.Text);
                __configinfo.Nocacheheaders = Convert.ToInt16(nocacheheaders.SelectedValue);
                __configinfo.Debug = Convert.ToInt16(debug.SelectedValue);
                __configinfo.Rewriteurl = "";
                __configinfo.Maxmodworksmonths = 3;
                __configinfo.Rssstatus = Convert.ToInt16(rssstatus.SelectedValue);
                __configinfo.Sitemapstatus = Convert.ToInt16(sitemapstatus.SelectedValue);
                __configinfo.Cachelog = 0;
                if (fulltextsearch.SelectedValue == "1")
                {
                    __configinfo.Fulltextsearch = 1;
                    foreach (DataRow dr in Posts.GetAllPostTableName().Rows)
                    {
                        try
                        {
                            DatabaseProvider.GetInstance().TestFullTextIndex(Utils.StrToInt(dr["id"], 0));
                        }
                        catch
                        {
                            base.RegisterStartupScript("", "<script>alert('�������ݿ����ӱ�[" + BaseConfigs.GetTablePrefix + "posts" + dr["id"] + "]����δ����ȫ����������,���ʹ�����ݿ�ȫ��������Ч');</script>");
                            __configinfo.Fulltextsearch = 0;
                            break;
                        }
                    }
                }
                else
                {
                    __configinfo.Fulltextsearch = 0;
                }
                __configinfo.Passwordmode = Convert.ToInt16(passwordmode.SelectedValue);
                __configinfo.Bbcodemode = Convert.ToInt16(bbcodemode.SelectedValue);

                if (extname.Text.Trim() == "")
                {
                    base.RegisterStartupScript( "", "<script>alert('��δ������Ӧ��α��̬url��չ��!');</script>");
                    return;
                }

                //if (__configinfo.Extname != extname.Text.Trim())
                //{
                //    AdminForums.SetForumsPathList(true, extname.Text.Trim());
                //}

                __configinfo.Extname = extname.Text.Trim();
                __configinfo.Silverlight = Convert.ToInt32(enablesilverlight.SelectedValue);
                __configinfo.Enablespace = Convert.ToInt32(EnableSpace.SelectedValue);
                __configinfo.Enablealbum = Convert.ToInt32(EnableAlbum.SelectedValue);
                __configinfo.Enablemall = Convert.ToInt32(EnableMall.SelectedValue);
                __configinfo.CookieDomain = CookieDomain.Text;
                __configinfo.Memliststatus = Convert.ToInt32(memliststatus.SelectedValue);
                __configinfo.Indexpage = Convert.ToInt32(Indexpage.SelectedValue);
                __configinfo.Linktext = Linktext.Text;
                __configinfo.Statcode = Statcode.Text;

                __configinfo.Spacename = spacename.Text;
                __configinfo.Spaceurl = spaceurl.Text;

                if ((__configinfo.Spaceurl == "") || !__configinfo.Spaceurl.EndsWith(".aspx"))
                {
                    base.RegisterStartupScript("", "<script>alert('�ռ�URL��ַ����Ϊ���ұ�����\".aspx��β\"!');</script>");
                    return;
                }
                __configinfo.Albumname = albumname.Text;
                __configinfo.Albumurl = albumurl.Text;

                if ((__configinfo.Albumurl == "") || !__configinfo.Albumurl.EndsWith(".aspx"))
                {
                    base.RegisterStartupScript("", "<script>alert('���URL��ַ����Ϊ���ұ�����\".aspx��β\"!');</script>");
                    return;
                }

                __configinfo.Aspxrewrite = Convert.ToInt16(aspxrewrite.SelectedValue);

                GeneralConfigs.Serialiaze(__configinfo, Server.MapPath("../../config/general.config"));

                if (__configinfo.Aspxrewrite == 1)
                {
                    AdminForums.SetForumsPathList(true, __configinfo.Extname);
                }
                else 
                {
                    AdminForums.SetForumsPathList(false, __configinfo.Extname);
                }
                Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/ForumList");

                TopicStats.SetQueueCount();
                AdminCaches.ReSetConfig();
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "��������", "");
                base.RegisterStartupScript( "PAGE",  "window.location.href='global_baseset.aspx';");
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
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
            this.Load += new EventHandler(this.Page_Load);

            forumtitle.IsReplaceInvertedComma = false;
            forumurl.IsReplaceInvertedComma = false;
            webtitle.IsReplaceInvertedComma = false;
            weburl.IsReplaceInvertedComma = false;
            icp.IsReplaceInvertedComma = false;
        }

        #endregion

    }
}