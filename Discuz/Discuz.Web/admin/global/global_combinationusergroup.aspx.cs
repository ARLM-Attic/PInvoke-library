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
    /// �ϲ��û���
    /// </summary>
    
#if NET1
    public class combinationusergroup : AdminPage
#else
    public partial class combinationusergroup : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.DropDownList sourceusergroup;
        protected Discuz.Control.DropDownList targetusergroup;
        protected Discuz.Control.Button ComUsergroup;
        protected Discuz.Control.DropDownList sourceadminusergroup;
        protected Discuz.Control.DropDownList targetadminusergroup;
        protected Discuz.Control.Button ComAdminUsergroup;
        #endregion
#endif

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                sourceusergroup.AddTableData(DatabaseProvider.GetInstance().GetUserGroupTitle());
                targetusergroup.AddTableData(DatabaseProvider.GetInstance().GetUserGroupTitle());
                sourceadminusergroup.AddTableData(DatabaseProvider.GetInstance().GetAdminUserGroupTitle());
                targetadminusergroup.AddTableData(DatabaseProvider.GetInstance().GetAdminUserGroupTitle());
            }
        }

        private void ComUsergroup_Click(object sender, EventArgs e)
        {
            #region �ϲ��û���

            if (this.CheckCookie())
            {
                if ((sourceusergroup.SelectedIndex == 0) || (targetusergroup.SelectedIndex == 0))
                {
                    base.RegisterStartupScript( "", "<script>alert('����ʧ��,����ѡ����Ч���û���!');</script>");
                    return;
                }

                if (sourceusergroup.SelectedValue == targetusergroup.SelectedValue)
                {
                    base.RegisterStartupScript( "", "<script>alert('����ʧ��,ͬһ���û��鲻�ܹ��ϲ�!');</script>");
                    return;
                }
                int sourceusergroupcreditslower = UserGroups.GetUserGroupInfo(int.Parse(sourceusergroup.SelectedValue)).Creditslower;
                int targetusergroupcreditshigher = UserGroups.GetUserGroupInfo(int.Parse(targetusergroup.SelectedValue)).Creditshigher;
                if (sourceusergroupcreditslower != targetusergroupcreditshigher)
                {
                    base.RegisterStartupScript("", "<script>alert('����ʧ��,Ҫ�ϲ����û�������ǻ��������������û���!');</script>");
                    return;
                }

                //�ϲ��û�����������
                DatabaseProvider.GetInstance().CombinationUsergroupScore(int.Parse(sourceusergroup.SelectedValue), int.Parse(targetusergroup.SelectedValue));
                //ɾ�����ϲ���Դ�û���
                DatabaseProvider.GetInstance().DeleteUserGroupInfo(int.Parse(sourceusergroup.SelectedValue));

                //�����û����е���Ϣ
                DatabaseProvider.GetInstance().ChangeUsergroup(int.Parse(sourceusergroup.SelectedValue), int.Parse(targetusergroup.SelectedValue));

                Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/UserGroupList");
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "�ϲ��û���", "����ID:" + sourceusergroup.SelectedIndex + " �ϲ�����ID:" + targetusergroup.SelectedIndex);
                base.RegisterStartupScript("PAGE", "window.location.href='global_usergroupgrid.aspx';");
            }

            #endregion
        }

        private void ComAdminUsergroup_Click(object sender, EventArgs e)
        {
            #region �ϲ�������

            if (this.CheckCookie())
            {
                if ((sourceadminusergroup.SelectedIndex == 0) || (targetadminusergroup.SelectedIndex == 0))
                {
                    base.RegisterStartupScript( "", "<script>alert('����ʧ��,����ѡ����Ч�Ĺ�����!');</script>");
                    return;
                }

                if ((Convert.ToInt32(sourceadminusergroup.SelectedValue) <= 3) || (Convert.ToInt32(sourceadminusergroup.SelectedValue) <= 3))
                {
                    base.RegisterStartupScript( "", "<script>alert('����ʧ��,��ѡ�����Ϊϵͳ��ʼ���Ĺ�����,��Щ�鲻����ϲ�!');</script>");
                    return;
                }

                if (sourceadminusergroup.SelectedValue == targetadminusergroup.SelectedValue)
                {
                    base.RegisterStartupScript( "", "<script>alert('����ʧ��,ͬһ�������鲻�ܹ��ϲ�!');</script>");
                    return;
                }

                //ɾ�����ϲ���Դ�û���
                DatabaseProvider.GetInstance().DeleteAdminGroupInfo(int.Parse(sourceadminusergroup.SelectedValue));

                //ɾ�����ϲ���Դ�û���
                DatabaseProvider.GetInstance().DeleteUserGroupInfo(int.Parse(sourceadminusergroup.SelectedValue));
             
                //�����û����е���Ϣ
                DatabaseProvider.GetInstance().UpdateAdminUsergroup(targetadminusergroup.SelectedValue.ToString(), sourceadminusergroup.SelectedValue.ToString());
                DatabaseProvider.GetInstance().ChangeUsergroup(int.Parse(sourceadminusergroup.SelectedValue), int.Parse(targetadminusergroup.SelectedValue));

                Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/UserGroupList");
                Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/AdminGroupList");
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "�ϲ�������", "����ID:" + sourceusergroup.SelectedIndex + " �ϲ�����ID:" + targetusergroup.SelectedIndex);
                base.RegisterStartupScript("PAGE", "window.location.href='global_adminusergroupgrid.aspx';");
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
            this.ComUsergroup.Click += new EventHandler(this.ComUsergroup_Click);
            this.ComAdminUsergroup.Click += new EventHandler(this.ComAdminUsergroup_Click);
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion


    }
}