using System;
using System.Data;
using System.Web.UI;
using System.Collections;

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
    /// �����û���༭
    /// </summary>
  
#if NET1
    public class editadminusergroup : AdminPage
#else
    public partial class editadminusergroup : AdminPage
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
        protected Discuz.Control.Button UpdateUserGroupInf;
        protected Discuz.Control.Button DeleteUserGroupInf;
        protected Discuz.Control.TextBox creditshigher;
        protected Discuz.Control.TextBox creditslower;
        protected Discuz.Control.TextBox groupavatar;
        #endregion
#endif


        public AdminGroupInfo __admingroupinfo = new AdminGroupInfo();
        public UserGroupInfo __usergroupinfo = new UserGroupInfo();
        protected bool haveAlbum;
        protected bool haveSpace;

        protected void Page_Load(object sender, EventArgs e)
        {
            haveAlbum = AlbumPluginProvider.GetInstance() != null;
            haveSpace = SpacePluginProvider.GetInstance() != null;
            if (!IsPostBack)
            {
                if (DNTRequest.GetString("groupid") != "")
                {
                    LoadUserGroupInf(DNTRequest.GetInt("groupid", -1));
                }
                else
                {
                    Response.Redirect("global_adminusergroupgrid.aspx");
                    return;
                }
                if (AlbumPluginProvider.GetInstance() == null)
                {
                    admingroupright.Items.RemoveAt(12);
                }
            }
        }

        public void LoadUserGroupInf(int groupid)
        {
            #region �����������Ϣ

            __usergroupinfo = AdminUserGroups.AdminGetUserGroupInfo(groupid);

            groupTitle.Text = Utils.RemoveFontTag(__usergroupinfo.Grouptitle);
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
            maxspaceattachsize.Text = __usergroupinfo.Maxspaceattachsize.ToString();
            maxspacephotosize.Text = __usergroupinfo.Maxspacephotosize.ToString();

            if (groupid > 0 && groupid <= 3) radminid.Enabled = false;

            radminid.SelectedValue = __usergroupinfo.Radminid.ToString();

            attachextensions.SetSelectByID(__usergroupinfo.Attachextensions.Trim());

            //�����û�Ȩ�����ʼ����Ϣ
            __admingroupinfo = AdminUserGroups.AdminGetAdminGroupInfo(__usergroupinfo.Groupid);
            usergrouppowersetting.Bind(__usergroupinfo);

            if (__admingroupinfo != null)
            {
                //���ù���Ȩ�����ʼ����Ϣ
                admingroupright.SelectedIndex = -1;
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
                if (__admingroupinfo.Allowstickthread.ToString() != "") allowstickthread.SelectedValue = __admingroupinfo.Allowstickthread.ToString();

            }

            if (radminid.SelectedValue == "1")
            {
                allowstickthread.Enabled = false;
                allowstickthread.SelectedValue = "3";
            }

            #endregion
        }

        private void DeleteUserGroupInf_Click(object sender, EventArgs e)
        {
            #region ɾ���������Ϣ

            if (this.CheckCookie())
            {
                if (AdminUserGroups.DeleteUserGroupInfo(DNTRequest.GetInt("groupid", -1)))
                {
                    //ɾ���ٱ���
                    GeneralConfigInfo __configinfo = GeneralConfigs.Deserialize(Server.MapPath("../../config/general.config"));                    
                    string tempstr = "";
                    foreach (string report in __configinfo.Reportusergroup.Split(','))
                    {
                        if (report != __usergroupinfo.Groupid.ToString())
                        {
                            if (tempstr == "")
                                tempstr = report;
                            else
                                tempstr += "," + report;
                        }
                    }
                    __configinfo.Reportusergroup = tempstr;
                    tempstr = "";
                    foreach (string photomangegroup in __configinfo.Photomangegroups.Split(','))
                    {
                        if (photomangegroup != __usergroupinfo.Groupid.ToString())
                        {
                            if (tempstr == "")
                                tempstr = photomangegroup;
                            else
                                tempstr += "," + photomangegroup;
                        }
                    }
                    __configinfo.Photomangegroups = tempstr;
                    GeneralConfigs.Serialiaze(__configinfo, Server.MapPath("../../config/general.config"));
                    Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/AdminGroupList");
                    AdminGroups.GetAdminGroupList();
                    AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "��̨ɾ��������", "��ID:" + DNTRequest.GetInt("groupid", -1));
                    base.RegisterStartupScript("PAGE", "window.location.href='global_adminusergroupgrid.aspx';");
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('����ʧ��');window.location.href='global_adminusergroupgrid.aspx';</script>");
                }
            }

            #endregion
        }

        public int BoolToInt(bool a)
        {
            if (a) return 1;
            else return 0;
        }


        public byte BoolToByte(bool a)
        {
            if (a) return 1;
            else return 0;
        }


        private void UpdateUserGroupInf_Click(object sender, EventArgs e)
        {
            #region ���¹�������Ϣ

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
                        base.RegisterStartupScript("", "<script>alert('�������," + de.Key.ToString() + "ֻ����0����������');window.location.href='global_editadminusergroup.aspx';</script>");
                        return;
                    }

                }
                __usergroupinfo = AdminUserGroups.AdminGetUserGroupInfo(DNTRequest.GetInt("groupid", -1));
                __usergroupinfo.System = 0;
                __usergroupinfo.Type = 0;
                __usergroupinfo.Readaccess = Convert.ToInt32(readaccess.Text);

                if (radminid.SelectedValue == "0") //��δѡȡ�κι���ģ��ʱ
                {
                    AdminGroups.DeleteAdminGroupInfo((short)__usergroupinfo.Groupid);
                    __usergroupinfo.Radminid = 0;
                }
                else //��ѡȡ��Ӧ�Ĺ���ģ��ʱ
                {
                    int selectradminid = Convert.ToInt32(radminid.SelectedValue);
                    ///���ڵ�ǰ�û�����,�й���Ȩ�޵�,�����ù���Ȩ��
                    if (selectradminid > 0 && selectradminid <= 3)
                    {
                        __admingroupinfo = new AdminGroupInfo();
                        __admingroupinfo.Admingid = (short)__usergroupinfo.Groupid;

                        //������Ӧ�Ĺ�����
                        __admingroupinfo.Alloweditpost = BoolToByte(admingroupright.Items[0].Selected);
                        __admingroupinfo.Alloweditpoll = BoolToByte(admingroupright.Items[1].Selected);
                        __admingroupinfo.Allowstickthread = (byte)Convert.ToInt16(allowstickthread.SelectedValue);
                        __admingroupinfo.Allowmodpost = 0;
                        __admingroupinfo.Allowdelpost = BoolToByte(admingroupright.Items[2].Selected);
                        __admingroupinfo.Allowmassprune = BoolToByte(admingroupright.Items[3].Selected);
                        __admingroupinfo.Allowrefund = 0;
                        __admingroupinfo.Allowcensorword = 0; ;
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

                        //�����м�¼ʱ
                        if (DatabaseProvider.GetInstance().GetAdmingid(__usergroupinfo.Groupid).Rows.Count > 0)
                        {
                            //������Ӧ�Ĺ�����
                            AdminGroups.SetAdminGroupInfo(__admingroupinfo);
                        }
                        else
                        { //������Ӧ���û���
                            AdminGroups.CreateAdminGroupInfo(__admingroupinfo);
                        }
                        __usergroupinfo.Radminid = selectradminid;
                    }
                    else
                    {
                        __usergroupinfo.Radminid = 0;
                    }
                }

                DatabaseProvider.GetInstance().ChangeUserAdminidByGroupid(__usergroupinfo.Radminid, __usergroupinfo.Groupid);

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
                
                usergrouppowersetting.GetSetting(ref __usergroupinfo);


                if (AdminUserGroups.UpdateUserGroupInfo(__usergroupinfo))
                {
                    #region �Ƿ�������վٱ���Ϣ�͹���ͼƬ����
                    GeneralConfigInfo configinfo = GeneralConfigs.Deserialize(Server.MapPath("../../config/general.config"));
                    //�Ƿ�������վٱ���Ϣ
                    int groupid = __usergroupinfo.Groupid;
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
                    else
                    {
                        string tempstr = "";
                        foreach (string report in configinfo.Reportusergroup.Split(','))
                        {
                            if (report != groupid.ToString())
                            {
                                if (tempstr == "")
                                {
                                    tempstr = report;
                                }
                                else
                                {
                                    tempstr += "," + report;
                                }
                            }
                        }
                        configinfo.Reportusergroup = tempstr;
                    }
                    //�Ƿ��������ͼƬ����
                    if (AlbumPluginProvider.GetInstance() != null)
                    {
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
                        else
                        {
                            string tempstr = "";
                            foreach (string photomangegroup in configinfo.Photomangegroups.Split(','))
                            {
                                if (photomangegroup != groupid.ToString())
                                {
                                    if (tempstr == "")
                                    {
                                        tempstr = photomangegroup;
                                    }
                                    else
                                    {
                                        tempstr += "," + photomangegroup;
                                    }
                                }
                            }
                            configinfo.Photomangegroups = tempstr;
                        } 
                    }

                    GeneralConfigs.Serialiaze(configinfo, Server.MapPath("../../config/general.config"));
                    #endregion
                    Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/UserGroupList");
                    Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/AdminGroupList");

                    AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "��̨���¹�����", "����:" + groupTitle.Text);
                    base.RegisterStartupScript( "PAGE",  "window.location.href='global_adminusergroupgrid.aspx';");
                }
                else
                {
                    base.RegisterStartupScript( "", "<script>alert('����ʧ��');window.location.href='global_adminusergroupgrid.aspx';</script>");
                }
            }

            #endregion
        }


        private void radminid_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region �󶨹�����
            DataTable usergrouprightstable = DatabaseProvider.GetInstance().GetUserGroupInfoByGroupid(int.Parse(radminid.SelectedValue));
            if (usergrouprightstable.Rows.Count > 0)
            {
                //���ù������ʼ����Ϣ
                DataRow usergrouprights = usergrouprightstable.Rows[0];
                creditshigher.Text = usergrouprights["creditshigher"].ToString();
                creditslower.Text = usergrouprights["creditslower"].ToString();
                stars.Text = usergrouprights["stars"].ToString();
                color.Text = usergrouprights["color"].ToString();
                groupavatar.Text = usergrouprights["groupavatar"].ToString();
                readaccess.Text = usergrouprights["readaccess"].ToString();
                maxprice.Text = usergrouprights["maxprice"].ToString();
                maxpmnum.Text = usergrouprights["maxpmnum"].ToString();
                maxsigsize.Text = usergrouprights["maxsigsize"].ToString();
                maxattachsize.Text = usergrouprights["maxattachsize"].ToString();
                maxsizeperday.Text = usergrouprights["maxsizeperday"].ToString();
                DataTable dt = DatabaseProvider.GetInstance().GetAttchType().Tables[0];
                attachextensions.AddTableData(dt, usergrouprights["attachextensions"].ToString().Trim());
            }

            DataTable admingrouprights = DatabaseProvider.GetInstance().GetAdmingroupByAdmingid(int.Parse(radminid.SelectedValue));
            if (admingrouprights.Rows.Count > 0)
            {
                //���ù���Ȩ�����ʼ����Ϣ
                DataRow dr = admingrouprights.Rows[0];
                admingroupright.SelectedIndex = -1;
                if (dr["alloweditpost"].ToString() == "1") admingroupright.Items[0].Selected = true;
                if (dr["alloweditpoll"].ToString() == "1") admingroupright.Items[1].Selected = true;
                if (dr["allowdelpost"].ToString() == "1") admingroupright.Items[2].Selected = true;
                if (dr["allowmassprune"].ToString() == "1") admingroupright.Items[3].Selected = true;
                if (dr["allowviewip"].ToString() == "1") admingroupright.Items[4].Selected = true;
                if (dr["allowedituser"].ToString() == "1") admingroupright.Items[5].Selected = true;
                if (dr["allowviewlog"].ToString() == "1") admingroupright.Items[6].Selected = true;
                if (dr["disablepostctrl"].ToString() == "1") admingroupright.Items[7].Selected = true;
                if (dr["allowviewrealname"].ToString() == "1") admingroupright.Items[8].Selected = true;
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
            admingrouprights.Dispose();
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
            this.radminid.SelectedIndexChanged += new EventHandler(this.radminid_SelectedIndexChanged);
            this.UpdateUserGroupInf.Click += new EventHandler(this.UpdateUserGroupInf_Click);
            this.DeleteUserGroupInf.Click += new EventHandler(this.DeleteUserGroupInf_Click);
            //this.Load += new EventHandler(this.Page_Load);

            radminid.AddTableData(DatabaseProvider.GetInstance().AddTableData());
            DataTable dt = DatabaseProvider.GetInstance().GetAttachTypes();
            attachextensions.AddTableData(dt);
        }

        #endregion
    }
}