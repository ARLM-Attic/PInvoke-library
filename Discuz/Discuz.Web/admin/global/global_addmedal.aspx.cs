using System;
using System.Data;
using System.Web.UI;

using Discuz.Control;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Data;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ���ѫ����Ϣ
    /// </summary>
    
#if NET1
    public class addmedal : AdminPage
#else
    public partial class addmedal : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TextBox name;
        protected Discuz.Control.RadioButtonList available;
        protected Discuz.Control.UpFile image;
        protected Discuz.Control.Button AddMedalInfo;
        #endregion
#endif


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dt = DatabaseProvider.GetInstance().GetMedal();
                if (dt.Rows.Count >= 100)
                {
                    base.RegisterStartupScript( "", "<script>alert('ѫ���б��¼�Ѿ��ﵽ99ö,���ϵͳ�����������ѫ��');window.location.href='global_medalgrid.aspx';</script>");
                    return;
                }
                string path = Utils.GetMapPath("../../images/medals");
                image.UpFilePath = path;
            }
        }

        public void AddMedalInfo_Click(object sender, EventArgs e)
        {
            #region ���ѫ�½�

            if (this.CheckCookie())
            {
                if (image.Text == "")
                {
                    base.RegisterStartupScript( "", "<script>alert('�ϴ�ͼƬ����Ϊ��');</script>");
                    return;
                }
                //string sqlstring = string.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "medals] (medalid,name,available,image) Values ('{0}','{1}','{2}','{3}')",
                //                                 Convert.ToString(Convert.ToInt32(DbHelper.SelectMaxID("" + BaseConfigs.GetTablePrefix + "medals", "medalid")) + 1),
                //                                 name.Text,
                //                                 available.SelectedValue,
                //                                 image.Text);
                //DbHelper.ExecuteNonQuery(sqlstring);
                DatabaseProvider.GetInstance().AddMedal(
                     DatabaseProvider.GetInstance().GetMaxMedalId(),
                     name.Text,
                     int.Parse(available.SelectedValue),
                     image.Text);
                Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/UI/MedalsList");
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "ѫ���ļ����", name.Text);
                base.RegisterStartupScript( "PAGE", "window.location.href='global_medalgrid.aspx';");
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
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion

    }
}