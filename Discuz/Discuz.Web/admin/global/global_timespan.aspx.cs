using System;
using System.Web.UI;
using System.Collections;

using Discuz.Control;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Common;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ʱ�������
    /// </summary>

#if NET1
    public class timespan : AdminPage
#else
    public partial class timespan : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Web.Admin.TextareaResize visitbanperiods;
        protected Discuz.Web.Admin.TextareaResize postbanperiods;
        protected Discuz.Web.Admin.TextareaResize postmodperiods;
        protected Discuz.Web.Admin.TextareaResize attachbanperiods;
        protected Discuz.Web.Admin.TextareaResize searchbanperiods;
        protected Discuz.Control.Hint Hint1;
        protected Discuz.Control.Button SaveInfo;
        #endregion
#endif

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadConfigInfo();
            }
        }

        public void LoadConfigInfo()
        {
            #region ����������Ϣ

            GeneralConfigInfo __configinfo = GeneralConfigs.Deserialize(Server.MapPath("../../config/general.config"));
            visitbanperiods.Text = __configinfo.Visitbanperiods.ToString();
            postbanperiods.Text = __configinfo.Postbanperiods.ToString();
            postmodperiods.Text = __configinfo.Postmodperiods.ToString();
            searchbanperiods.Text = __configinfo.Searchbanperiods.ToString();
            attachbanperiods.Text = __configinfo.Attachbanperiods.ToString();

            #endregion
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            #region ����������Ϣ

            if (this.CheckCookie())
            {
                GeneralConfigInfo __configinfo = GeneralConfigs.Deserialize(Server.MapPath("../../config/general.config"));
                Hashtable TimeHash = new Hashtable();
                TimeHash.Add("��ֹ����ʱ���", visitbanperiods.Text);
                TimeHash.Add("��ֹ����ʱ���", postbanperiods.Text);
                TimeHash.Add("�������ʱ���", postmodperiods.Text);
                TimeHash.Add("��ֹ���ظ���ʱ���", attachbanperiods.Text);
                TimeHash.Add("��ֹȫ������ʱ���", searchbanperiods.Text);

             //   foreach (DictionaryEntry str in TimeHash)
             //   {

             //       try
             //       {
             //           string[] singletime = str.Value.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);

             //      foreach(string stime in singletime)
             //      {
             //          if (stime != "")
             //          {
             //              string[] splitetime = stime.Split('-');
             //              if (Utils.IsTime(splitetime[1].ToString()) == false || Utils.IsTime(splitetime[0].ToString()) == false)
             //                  throw new Exception();

             //          }
             //      }


             //       }
             //       catch
             //       {

             //           base.RegisterStartupScript("erro", "<script>alert('" + str.Key.ToString() + ",ʱ���ʽ����');</script>");
             //           return;
             //       }
             //}
                string key = "";
                if (Utils.IsRuleTip(TimeHash, "timesect", out key) == false)
                {
                    base.RegisterStartupScript("erro", "<script>alert('" + key.ToString() + ",ʱ���ʽ����');</script>");
                    return;
                }

                __configinfo.Visitbanperiods = visitbanperiods.Text;
                __configinfo.Postbanperiods = postbanperiods.Text;
                __configinfo.Postmodperiods = postmodperiods.Text;
                __configinfo.Searchbanperiods = searchbanperiods.Text;
                __configinfo.Attachbanperiods = attachbanperiods.Text;
                GeneralConfigs.Serialiaze(__configinfo, Server.MapPath("../../config/general.config"));

                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "ʱ�������", "");
                base.RegisterStartupScript( "PAGE", "window.location.href='global_timespan.aspx';");
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
        }

        #endregion
    }
}