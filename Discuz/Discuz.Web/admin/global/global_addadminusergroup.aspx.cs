using System;
using System.Data;
using System.Collections;
using System.Web.UI;

using Discuz.Control;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Data;

using Discuz.Entity;
using Discuz.Plugin.Album;
using Discuz.Plugin.Space;

namespace Discuz.Web.Admin
{
    /// <summary>
    /// ��ӹ�����
    /// </summary>
#if NET1
    public class addadminusergroup : AdminPage
#else
    public partial class addadminusergroup : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TabControl TabControl1;
        protected Discuz.Control.TabPage tabPage51;
        protected Discuz.Control.DropDownList radminid;
        protected Discuz.Control.TextBox groupTitle;
        protected Discuz.Control.ColorPicker color;
        protected Discuz.Control.TextBox stars;
        protected Discuz.Control.TextBox readaccess;
        protected Discuz.Control.TextBox maxprice;
        protected Discuz.Control.TextBox maxpmnum;
        protected Discuz.Control.TextBox maxsigsize;
        protected Discuz.Control.TextBox maxattachsize;
        protected Discuz.Control.TextBox maxsizeperday;
        protected Discuz.Control.TextBox maxspaceattachsize;
        protected Discuz.Control.TextBox maxspacephotosize;
        protected Discuz.Control.CheckBoxList attachextensions;
        protected Discuz.Control.TabPage tabPage22;
        protected Discuz.Control.RadioButtonList allowavatar;
        protected Discuz.Control.RadioButtonList allowsearch;
        protected Discuz.Control.CheckBoxList usergroupright;
        protected Discuz.Control.TabPage tabPage33;
        protected Discuz.Control.CheckBoxList admingroupright;
        protected Discuz.Control.DropDownList allowstickthread;
        protected Discuz.Control.Hint Hint1;
        protected Discuz.Control.Button AddUserGroupInf;
        protected Discuz.Control.TextBox creditshigher;
        protected Discuz.Control.TextBox creditslower;
        protected Discuz.Control.TextBox groupavatar;
        #endregion
#endif
        protected bool haveAlbum;
        protected bool haveSpace;

        protected void Page_Load(object sender, EventArgs e)
        {
            haveAlbum = AlbumPluginProvider.GetInstance() != null;
            haveSpace = SpacePluginProvider.GetInstance() != null;
            if (!Page.IsPostBack)
            {
                usergrouppowersetting.Bind();
            }
        }

        public void SetGroupRights(string groupid)
        {
            #region ������Ȩ�������Ϣ
            UserGroupInfo __usergroupinfo = AdminUserGroups.AdminGetUserGroupInfo(Convert.ToInt32(groupid));

            creditshigher.Text = __usergroupinfo.Creditshigher.ToString();
            creditslower.Text = __usergroupinfo.Creditslower.ToString();
            stars.Text = __usergroupinfo.Stars.ToString();
            color.Text = __usergroupinfo.Color;
            groupavatar.Text = __usergroupinfo.Groupavatar;
            readaccess.Text = __usergroupinfo.Readaccess.ToString();
            maxprice.Text = __usergroupinfo.Maxprice.ToString();
            maxpmnum.Text = __usergroupinfo.Maxpmnum.ToString();
            maxsigsize.Text = __usergroupinfo.Maxsigsize.ToString();
            maxattachsize.Text = __usergroupinfo.Maxattachsize.ToString();
            maxsizeperday.Text = __usergroupinfo.Maxsizeperday.ToString();
            radminid.SelectedValue = __usergroupinfo.Radminid.ToString();

            DataTable dt = DatabaseProvider.GetInstance().GetAttchType().Tables[0];
            attachextensions.AddTableData(dt,__usergroupinfo.Attachextensions.ToString());

            //�����û�Ȩ�����ʼ����Ϣ
            usergrouppowersetting.Bind(__usergroupinfo);

            AdminGroupInfo __admingroupinfo = AdminUserGroups.AdminGetAdminGroupInfo(Convert.ToInt32(groupid));
            if (__admingroupinfo != null)
            {
                admingroupright.Items[0].Selected = __admingroupinfo.Alloweditpost == 1;
                admingroupright.Items[1].Selected = __admingroupinfo.Alloweditpoll == 1;
                admingroupright.Items[2].Selected = __admingroupinfo.Allowdelpost == 1;
                admingroupright.Items[3].Selected = __admingroupinfo.Allowmassprune == 1;
                admingroupright.Items[4].Selected = __admingroupinfo.Allowviewip == 1;
                admingroupright.Items[5].Selected = __admingroupinfo.Allowedituser == 1;
                admingroupright.Items[6].Selected = __admingroupinfo.Allowviewlog == 1;
                admingroupright.Items[7].Selected = __admingroupinfo.Disablepostctrl == 1;
                admingroupright.Items[8].Selected = __admingroupinfo.Allowviewrealname == 1;
                admingroupright.Items[9].Selected = __admingroupinfo.Allowbanuser == 1;
                admingroupright.Items[10].Selected = __admingroupinfo.Allowbanip == 1;
                GeneralConfigInfo configinfo = GeneralConfigs.Deserialize(Server.MapPath("../../config/general.config"));
                admingroupright.Items[11].Selected = ("," + configinfo.Reportusergroup + ",").IndexOf("," + groupid + ",") != -1; //�Ƿ�������վٱ���Ϣ
                admingroupright.Items[12].Selected = ("," + configinfo.Photomangegroups + ",").IndexOf("," + groupid + ",") != -1;//�Ƿ��������ͼƬ���� 
            }

            if (radminid.SelectedValue == "1")
            {
                allowstickthread.Enabled = false;
                allowstickthread.SelectedValue = "3";
            }
            else
            {
                allowstickthread.Enabled = true;
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
            this.TabControl1.InitTabPage();
            this.AddUserGroupInf.Click += new EventHandler(this.AddUserGroupInf_Click);
            this.radminid.SelectedIndexChanged += new EventHandler(this.radminid_SelectedIndexChanged);
            this.Load += new EventHandler(this.Page_Load);

            DataTable dt = DatabaseProvider.GetInstance().GetAttachTypes();
            attachextensions.AddTableData(dt);
            radminid.AddTableData(DatabaseProvider.GetInstance().AddTableData());

            if (DNTRequest.GetString("groupid") != "")
            {
                SetGroupRights(DNTRequest.GetString("groupid"));
            }
        }

        #endregion

        public int BoolToInt(bool a)
        {
            return a ? 1 : 0;
        }

        public byte BoolToByte(bool a)
        {
            return (byte)(a ? 1 : 0);
        }

        private void AddUserGroupInf_Click(object sender, EventArgs e)
        {
            #region �����������Ϣ����

            if (this.CheckCookie())
            {

                Hashtable ht = new Hashtable();
                ht.Add("�������ߴ�", maxattachsize.Text);
                ht.Add("ÿ����󸽼��ܳߴ�", maxsizeperday.Text);
                ht.Add("���˿ռ丽���ܳߴ�", maxspaceattachsize.Text);
                ht.Add("���ռ��ܳߴ�", maxspacephotosize.Text);

                foreach (DictionaryEntry de in ht)
                {
                    if (!Utils.IsInt(de.Value.ToString()))
                    {
                        base.RegisterStartupScript("", "<script>alert('�������," + de.Key.ToString() + "ֻ����0����������');window.location.href='global_addadminusergroup.aspx';</script>");
                        return;
                    }

                }
                UserGroupInfo __usergroupinfo = new UserGroupInfo();
                __usergroupinfo.System = 0;
                __usergroupinfo.Type = 0;
                __usergroupinfo.Readaccess = Convert.ToInt32(readaccess.Text == "" ? "0" : readaccess.Text);
                __usergroupinfo.Allowdirectpost = 1;
                __usergroupinfo.Allowmultigroups = 0;
                __usergroupinfo.Allowcstatus = 0;
                __usergroupinfo.Allowuseblog = 0;
                __usergroupinfo.Allowinvisible = 0;
                __usergroupinfo.Allowtransfer = 0;
                __usergroupinfo.Allowhtml = 0;
                __usergroupinfo.Allownickname = 0;
                __usergroupinfo.Allowviewstats = 0;
                __usergroupinfo.Grouptitle = groupTitle.Text;
                __usergroupinfo.Creditshigher = Convert.ToInt32(creditshigher.Text);
                __usergroupinfo.Creditslower = Convert.ToInt32(creditslower.Text);
                __usergroupinfo.Stars = Convert.ToInt32(stars.Text);
                __usergroupinfo.Color = color.Text;
                __usergroupinfo.Groupavatar = groupavatar.Text;
                __usergroupinfo.Maxprice = Convert.ToInt32(maxprice.Text);
                __usergroupinfo.Maxpmnum = Convert.ToInt32(maxpmnum.Text);
                __usergroupinfo.Maxsigsize = Convert.ToInt32(maxsigsize.Text);
                __usergroupinfo.Maxattachsize = Convert.ToInt32(maxattachsize.Text);
                __usergroupinfo.Maxsizeperday = Convert.ToInt32(maxsizeperday.Text);
                __usergroupinfo.Maxspaceattachsize = Convert.ToInt32(maxspaceattachsize.Text);
                __usergroupinfo.Maxspacephotosize = Convert.ToInt32(maxspacephotosize.Text);
                __usergroupinfo.Attachextensions = attachextensions.GetSelectString(",");
                __usergroupinfo.Raterange = "";
                if (radminid.SelectedValue == "0")
                {
                    base.RegisterStartupScript("", "<script>alert('����ʧ��,����ѡ����Ӧ�Ĺ�����, �ٵ���ύ��ť!');</script>");
                    return;
                }
                __usergroupinfo.Radminid = Convert.ToInt32(radminid.SelectedValue);
                usergrouppowersetting.GetSetting(ref __usergroupinfo);
                if (AdminUserGroups.AddUserGroupInfo(__usergroupinfo))
                {
                    //usergrouppowersetting.GetReportAndPhotomangePower(DatabaseProvider.GetInstance().GetMaxUserGroupId(), ref __configinfo);
                    #region �Ƿ�������վٱ���Ϣ�͹���ͼƬ����
                    GeneralConfigInfo configinfo = GeneralConfigs.Deserialize(Server.MapPath("../../config/general.config"));
                    //�Ƿ�������վٱ���Ϣ
                    int groupid = DatabaseProvider.GetInstance().GetMaxUserGroupId();
                    if (admingroupright.Items[11].Selected)
                    {
                        if (("," + configinfo.Reportusergroup + ",").IndexOf("," + groupid + ",") == -1)
                        {
                            if (configinfo.Reportusergroup == "")
                            {
                                configinfo.Reportusergroup = groupid.ToString();
                            }
                            else
                            {
                                configinfo.Reportusergroup += "," + groupid.ToString();
                            }
                        }
                    }
                    //�Ƿ��������ͼƬ����
                    if (admingroupright.Items[12].Selected)
                    {
                        if (("," + configinfo.Photomangegroups + ",").IndexOf("," + groupid + ",") == -1)
                        {
                            if (configinfo.Photomangegroups == "")
                            {
                                configinfo.Photomangegroups = groupid.ToString();
                            }
                            else
                            {
                                configinfo.Photomangegroups += "," + groupid.ToString();
                            }
                        }
                    }
                    GeneralConfigs.Serialiaze(configinfo, Server.MapPath("../../config/general.config"));
                    #endregion
                    AdminGroupInfo __admingroupinfo = new AdminGroupInfo();
                    int adminid = DatabaseProvider.GetInstance().GetMaxUserGroupId() + 1;
                    __admingroupinfo.Admingid = (short)adminid;

                    //������Ӧ�Ĺ�����
                    __admingroupinfo.Alloweditpost = BoolToByte(admingroupright.Items[0].Selected);
                    __admingroupinfo.Alloweditpoll = BoolToByte(admingroupright.Items[1].Selected);
                    __admingroupinfo.Allowstickthread = (byte)Convert.ToInt16(allowstickthread.SelectedValue);
                    __admingroupinfo.Allowmodpost = 0;
                    __admingroupinfo.Allowdelpost = BoolToByte(admingroupright.Items[2].Selected);
                    __admingroupinfo.Allowmassprune = BoolToByte(admingroupright.Items[3].Selected);
                    __admingroupinfo.Allowrefund = 0;
                    __admingroupinfo.Allowcensorword = 0;
                    __admingroupinfo.Allowviewip = BoolToByte(admingroupright.Items[4].Selected);
                    __admingroupinfo.Allowbanip = 0;
                    __admingroupinfo.Allowedituser = BoolToByte(admingroupright.Items[5].Selected);
                    __admingroupinfo.Allowmoduser = 0;
                    __admingroupinfo.Allowbanuser = 0;
                    __admingroupinfo.Allowpostannounce = 0;
                    __admingroupinfo.Allowviewlog = BoolToByte(admingroupright.Items[6].Selected);
                    __admingroupinfo.Disablepostctrl = BoolToByte(admingroupright.Items[7].Selected);
                    __admingroupinfo.Allowviewrealname = BoolToByte(admingroupright.Items[8].Selected);
                    __admingroupinfo.Allowbanuser = BoolToByte(admingroupright.Items[9].Selected);
                    __admingroupinfo.Allowbanip = BoolToByte(admingroupright.Items[10].Selected);


                    AdminGroups.CreateAdminGroupInfo(__admingroupinfo);

                    Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/UserGroupList");
                    Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/AdminGroupList");
                    AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "��̨��ӹ�����", "����:" + groupTitle.Text);

                    base.RegisterStartupScript("PAGE", "window.location.href='global_adminusergroupgrid.aspx';");
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('����ʧ��');window.location.href='global_adminusergroupgrid.aspx';</script>");
                }
            }

            #endregion
        }


        private void radminid_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region ���û�����Ϣ
            SetGroupRights(radminid.SelectedValue);
            #endregion
        }
    }
}