using System;
using System.Data;
using System.Threading;
using System.Web.UI.WebControls;
using System.Web.UI;

using Discuz.Forum;
using Button = Discuz.Control.Button;
using CheckBoxList = Discuz.Control.CheckBoxList;
using DropDownList = Discuz.Control.DropDownList;
using TextBox = Discuz.Control.TextBox;
using Discuz.Config;
using Discuz.Data;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ���鷢�Ͷ���Ϣ
    /// </summary>
    
#if NET1
    public class sendsmtogroup : AdminPage
#else
    public partial class sendsmtogroup : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TextBox subject;
        protected Discuz.Control.TextBox msgfrom;
        protected Discuz.Control.DropDownList folder;
        protected Discuz.Control.TextBox postdatetime;
        protected Discuz.Web.Admin.TextareaResize message;
        protected Discuz.Control.CheckBoxList Usergroups;
        protected Discuz.Control.Button BatchSendSM;
        protected System.Web.UI.WebControls.Label lblClientSideCheck;
        protected System.Web.UI.WebControls.Label lblCheckedNodes;
        protected System.Web.UI.WebControls.Label lblServerSideCheck;
        #endregion
#endif

        #region �ؼ�����

        protected CheckBox selectall;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.username != "")
                {
                    msgfrom.Text = this.username;
                    postdatetime.Text = DateTime.Now.ToShortDateString();
                }
            }
        }

        private void BatchSendSM_Click(object sender, EventArgs e)
        {
            #region ��������Ϣ����

            if (this.CheckCookie())
            {
                string groupidlist = Usergroups.GetSelectString(",");

                if (groupidlist == "")
                {
                    base.RegisterStartupScript( "", "<script>alert('������ѡȡ��ص��û���,�ٵ���ύ��ť');</script>");
                    return;
                }

                int percount = 10; //ÿ���ټ�¼Ϊһ�εȴ�
                int count = 0; //��ǰ��¼��

                //foreach (DataRow dr in DbHelper.ExecuteDataset("SELECT [uid] ,[username]  From [" + BaseConfigs.GetTablePrefix + "users] WHERE [groupid] IN(" + groupidlist + ")").Tables[0].Rows)
                foreach (DataRow dr in DatabaseProvider.GetInstance().GetUserNameListByGroupid(groupidlist).Rows)
                {
                    //DbHelper.ExecuteNonQuery("INSERT INTO [" + BaseConfigs.GetTablePrefix + "pms] (msgfrom,msgfromid,msgto,msgtoid,folder,new,subject,postdatetime,message) VALUES ('" + this.username.Replace("'", "''") + "','" + this.userid.ToString() + "','" + dr["username"].ToString().Replace("'", "''") + "','" + dr["uid"].ToString() + "','" + folder.SelectedValue + "','1','" + subject.Text + "','" + postdatetime.Text + "','" + message.Text + "')");
                    //DbHelper.ExecuteNonQuery("UPDATE [" + BaseConfigs.GetTablePrefix + "users] SET [newpmcount]=[newpmcount]+1  WHERE [uid] =" + dr["uid"].ToString());
                    DatabaseProvider.GetInstance().SendPMToUser(username.Replace("'", "''"), userid, dr["username"].ToString().Replace("'", "''"), Convert.ToInt32(dr["uid"].ToString()), int.Parse(folder.SelectedValue), subject.Text, Convert.ToDateTime(postdatetime.Text), message.Text);
                    if (count >= percount)
                    {
                        Thread.Sleep(3500);
                        count = 0;
                    }
                    count++;
                }
                base.RegisterStartupScript( "PAGE",  "window.location.href='global_sendSMtogroup.aspx';");
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
            this.BatchSendSM.Click += new EventHandler(this.BatchSendSM_Click);
            this.Load += new EventHandler(this.Page_Load);

            message.is_replace = true;

            DataTable dt = DatabaseProvider.GetInstance().GetUserGroupWithOutGuestTitle();
            foreach (DataRow dr in dt.Rows)
            {
                dr["grouptitle"] = "<img src=../images/usergroup.GIF border=0  style=\"position:relative;top:2 ;height:18 \">" + dr["grouptitle"];
            }
            Usergroups.AddTableData(dt);
        }

        #endregion
    }
}