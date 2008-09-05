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
    /// ����ƶ�
    /// </summary>

#if NET1
    public class forumsmove : AdminPage
#else
    public partial class forumsmove : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.DropDownTreeList sourceforumid;
        protected Discuz.Control.RadioButtonList movetype;
        protected Discuz.Control.ListBoxTreeList targetforumid;
        protected Discuz.Control.Button SaveMoveInfo;
        #endregion
#endif    
    
 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DNTRequest.GetString("currentfid") == "")
            {
                Server.Transfer("forum_ForumsTree.aspx");
            }
            else
            {
                #region ��ʼ���ؼ����ݰ�

                sourceforumid.BuildTree(DatabaseProvider.GetInstance().GetForumsTree());
                sourceforumid.TypeID.Items.RemoveAt(0);
                sourceforumid.SelectedValue = DNTRequest.GetString("currentfid");
                targetforumid.BuildTree(DatabaseProvider.GetInstance().GetForumsTree());
                targetforumid.TypeID.Items.RemoveAt(0);
                targetforumid.TypeID.Height = 290;
                targetforumid.TypeID.SelectedIndex = 0;

                #endregion
            }
        }


        private void SaveMoveInfo_Click(object sender, EventArgs e)
        {
            #region �������ƶ�����

            if (sourceforumid.SelectedValue == targetforumid.SelectedValue)
            {
                base.RegisterStartupScript( "", "<script>alert('����Ҫ�ƶ��İ����Ŀ������ͬ, ����޷��ύ!');</script>");
                return;
            }

            bool aschild = movetype.SelectedValue == "1" ? true : false;
            if (!AdminForums.MovingForumsPos(sourceforumid.SelectedValue, targetforumid.SelectedValue, aschild))
            {
                base.RegisterStartupScript( "", "<script>alert('��ǰԴ����ƶ�ʧ��!');</script>");
                return;
            }

            AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "�ƶ���̳���", "�ƶ���̳���ID:" + sourceforumid.SelectedValue + "��ID:" + targetforumid.SelectedValue);
            base.RegisterStartupScript( "PAGE", "window.location.href='forum_forumsTree.aspx';");

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
            this.SaveMoveInfo.Click += new EventHandler(this.SaveMoveInfo_Click);

        }

        #endregion

    }
}