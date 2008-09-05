using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

using Discuz.Common;
using Discuz.Forum;
using Button = Discuz.Control.Button;
using Repeater = Discuz.Control.Repeater;
using TextBox = Discuz.Control.TextBox;
using Discuz.Config;
using Discuz.Data;
using Discuz.Entity;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ����ѫ��
    /// </summary>
    
#if NET1
    public class givemedals : AdminPage
#else
    public partial class givemedals : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected System.Web.UI.WebControls.Literal givenusername;
        protected Discuz.Control.Repeater medallist;
        protected Discuz.Control.TextBox reason;
        protected Discuz.Control.Button GivenMedal;
        #endregion
#endif


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (DNTRequest.GetString("uid") == "") return;
                else
                {
                    int uid = DNTRequest.GetInt("uid", -1);
                    UserInfo ui = Users.GetUserInfo(uid);
                    givenusername.Text = ui.Username;

                    if (ui.Medals.Trim() == "")
                    {
                        ui.Medals = "0";
                    }

                    LoadDataInfo("," + ui.Medals + ",");
                }
            }
        }

        public void LoadDataInfo(string begivenmedal)
        {
            #region ���������ð󶨵��ؼ�

            //DataTable dt = DbHelper.ExecuteDataset("SELECT * FROM [" + BaseConfigs.GetTablePrefix + "medals] WHERE [available]=1").Tables[0];
            DataTable dt = DatabaseProvider.GetInstance().GetAvailableMedal();

            if (dt != null)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = "isgiven";
                dc.DataType = Type.GetType("System.Boolean");
                dc.DefaultValue = false;
                dc.AllowDBNull = false;
                dt.Columns.Add(dc);

                foreach (DataRow dr in dt.Rows)
                {
                    if (begivenmedal.IndexOf("," + dr["medalid"].ToString() + ",") >= 0)
                    {
                        dr["isgiven"] = true;
                    }
                }
                medallist.DataSource = dt;
                medallist.DataBind();
            }

            #endregion
        }

        public string BeGivenMedal(string isgiven, string medalid)
        {
            #region ѫ�µ���ʾ��ʽ

            if (isgiven == "True")
            {
                return "<INPUT id=\"medalid\" type=\"checkbox\" value=\"" + medalid + "\"  name=\"medalid\" checked>";
            }
            else
            {
                return "<INPUT id=\"medalid\" type=\"checkbox\" value=\"" + medalid + "\"  name=\"medalid\">";
            }

            #endregion
        }

        private void GivenMedal_Click(object sender, EventArgs e)
        {
            #region ����ѫ��

            if (this.CheckCookie())
            {
                int uid = DNTRequest.GetInt("uid", -1);
                Users.UpdateMedals(uid, DNTRequest.GetString("medalid"));
                string givenusername = Users.GetUserInfo(uid).Username;
                foreach (string medalid in DNTRequest.GetString("medalid").Split(','))
                {
                    if (medalid != "")
                    {
                        if (!DatabaseProvider.GetInstance().IsExistMedalAwardRecord(int.Parse(medalid), uid))
                        {
                            DatabaseProvider.GetInstance().AddMedalslog(userid, username, DNTRequest.GetIP(), givenusername, uid, "����", int.Parse(medalid), reason.Text.Trim());
                        }
                        else
                        {
                            DatabaseProvider.GetInstance().UpdateMedalslog("����", DateTime.Now, reason.Text.Trim(), "�ջ�", int.Parse(medalid), uid);
                        }
                    }
                }

                if (DNTRequest.GetString("medalid") == "")
                {
                    DatabaseProvider.GetInstance().UpdateMedalslog("�ջ�", DateTime.Now, reason.Text.Trim(), uid);
                }
                else
                {
                    DatabaseProvider.GetInstance().UpdateMedalslog("�ջ�", DateTime.Now, reason.Text.Trim(), "����", DNTRequest.GetString("medalid"), uid);
                }

                if (DNTRequest.GetString("codition") == "")
                {
                    Session["codition"] = null;
                }
                else
                {
                    Session["codition"] = DNTRequest.GetString("codition").Replace("^", "'");
                }

                base.RegisterStartupScript( "PAGE", "window.location.href='global_edituser.aspx?uid=" + uid + "&condition=" + DNTRequest.GetString("condition") + "';");
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
            this.GivenMedal.Click += new EventHandler(this.GivenMedal_Click);
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion

    }
}