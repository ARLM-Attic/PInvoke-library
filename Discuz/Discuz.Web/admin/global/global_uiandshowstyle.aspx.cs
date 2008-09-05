using System;
using System.Web.UI;

using Discuz.Control;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Data;

using System.Collections;
using Discuz.Common;

namespace Discuz.Web.Admin
{
    /// <summary>
    /// ��������ʾ��ʽ����
    /// </summary>
    
#if NET1
    public class uiandshowstyle : AdminPage
#else
    public partial class uiandshowstyle : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.DropDownList templateid;
        protected Discuz.Control.RadioButtonList stylejump;
        protected Discuz.Control.RadioButtonList browsecreatetemplate;
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

            templateid.Attributes.Add("onchange", "LoadImage(this.selectedIndex)");
            GeneralConfigInfo __configinfo = GeneralConfigs.Deserialize(Server.MapPath("../../config/general.config"));
            templateid.SelectedValue = __configinfo.Templateid.ToString();
            stylejump.SelectedValue = __configinfo.Stylejump.ToString();
            browsecreatetemplate.SelectedValue = __configinfo.BrowseCreateTemplate.ToString();
            templateid.AddTableData(DatabaseProvider.GetInstance().GetTemplates(), __configinfo.Templateid.ToString());
            templateid.Items.RemoveAt(0);
            string scriptstr = "<script type=\"text/javascript\">\r\n";
            scriptstr += "images = new Array();\r\n";
            for (int i = 0; i < templateid.Items.Count; i++)
            {
                scriptstr += "images[" + i + "]=\"../../templates/" + templateid.Items[i].Text + "/about.png\";\r\n";
            }
            scriptstr += "</script>";
            base.RegisterStartupScript("", scriptstr);
            preview.Src = "../../templates/" + templateid.SelectedItem.Text + "/about.png";
            isframeshow.SelectedValue = __configinfo.Isframeshow.ToString();
            whosonlinestatus.SelectedValue = __configinfo.Whosonlinestatus.ToString();
            maxonlinelist.Text = __configinfo.Maxonlinelist.ToString();
            forumjump.SelectedValue = __configinfo.Forumjump.ToString();
            if (__configinfo.Onlinetimeout >= 0) showauthorstatusinpost.SelectedValue = "2";
            else showauthorstatusinpost.SelectedValue = "1";
            onlinetimeout.Text = Math.Abs(__configinfo.Onlinetimeout).ToString();
            smileyinsert.SelectedValue = __configinfo.Smileyinsert.ToString();
            visitedforums.Text = __configinfo.Visitedforums.ToString();
            moddisplay.SelectedValue = __configinfo.Moddisplay.ToString();
            showsignatures.SelectedValue = __configinfo.Showsignatures.ToString();
            showavatars.SelectedValue = __configinfo.Showavatars.ToString();
            showimages.SelectedValue = __configinfo.Showimages.ToString();
            maxsigrows.Text = __configinfo.Maxsigrows.ToString();
            smiliesmax.Text = __configinfo.Smiliesmax.ToString();
            viewnewtopicminute.Text = __configinfo.Viewnewtopicminute.ToString();
            whosonlinecontact.SelectedValue = __configinfo.Whosonlinecontract.ToString();
            #endregion
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            #region ����������Ϣ

            if (this.CheckCookie())
            {
                SortedList sl = new SortedList();
                sl.Add("�޶�������ʱ��", onlinetimeout.Text);
                sl.Add("���ǩ���߶�", maxsigrows.Text);
                sl.Add("��ʾ���������̳����", visitedforums.Text);
                sl.Add("������ͬһ��������ֵ�������", smiliesmax.Text);

                foreach (DictionaryEntry s in sl)
                {
                    if (!Utils.IsInt(s.Value.ToString()))
                    {
                        base.RegisterStartupScript("", "<script>alert('�������:" + s.Key.ToString() + ",ֻ����0����������');window.location.href='forum_uisetting.aspx';</script>");
                        return;
                    }
                }

                if (Convert.ToInt32(onlinetimeout.Text) <= 0)
                {
                    base.RegisterStartupScript("", "<script>alert('�޶�������ʱ��������0');</script>");
                    return;
                }
                if (Convert.ToInt16(maxsigrows.Text) > 9999 || (Convert.ToInt16(maxsigrows.Text) < 0))
                {
                    base.RegisterStartupScript("", "<script>alert('���ǩ���߶�ֻ����0-9999֮��');window.location.href='.aspx';</script>");
                    return;
                }


                if (Convert.ToInt16(visitedforums.Text) > 9999 || (Convert.ToInt16(visitedforums.Text) < 0))
                {
                    base.RegisterStartupScript("", "<script>alert('��ʾ���������̳����ֻ����0-9999֮��');window.location.href='forum_uisetting.aspx';</script>");
                    return;
                }


                if (Convert.ToInt16(smiliesmax.Text) > 1000 || (Convert.ToInt16(smiliesmax.Text) < 0))
                {
                    base.RegisterStartupScript("", "<script>alert('������ͬһ��������ֵ�������ֻ����0-1000֮��');window.location.href='forum_uisetting.aspx';</script>");
                    return;
                }

                if (Convert.ToInt16(viewnewtopicminute.Text) > 14400 || (Convert.ToInt16(viewnewtopicminute.Text) < 5))
                {
                    base.RegisterStartupScript("", "<script>alert('�鿴���������ñ�����5-14400֮��');window.location.href='forum_uisetting.aspx';</script>");
                    return;
                }

                GeneralConfigInfo __configinfo = GeneralConfigs.Deserialize(Server.MapPath("../../config/general.config"));

                __configinfo.Templateid = Convert.ToInt16(templateid.SelectedValue);
                __configinfo.Subforumsindex = 1;
                __configinfo.Stylejump = Convert.ToInt16(stylejump.SelectedValue);
                __configinfo.BrowseCreateTemplate = Convert.ToInt32(browsecreatetemplate.SelectedValue);
                __configinfo.Isframeshow = Convert.ToInt16(isframeshow.SelectedValue);
                __configinfo.Whosonlinestatus = Convert.ToInt16(whosonlinestatus.SelectedValue);

                if (showauthorstatusinpost.SelectedValue == "1") __configinfo.Onlinetimeout = 0 - Convert.ToInt32(onlinetimeout.Text);
                else __configinfo.Onlinetimeout = Convert.ToInt16(onlinetimeout.Text);

                __configinfo.Maxonlinelist = Convert.ToInt16(maxonlinelist.Text);
                __configinfo.Forumjump = Convert.ToInt16(forumjump.SelectedValue);
                __configinfo.Smileyinsert = Convert.ToInt16(smileyinsert.SelectedValue);
                __configinfo.Visitedforums = Convert.ToInt16(visitedforums.Text);
                __configinfo.Moddisplay = Convert.ToInt16(moddisplay.SelectedValue);
                __configinfo.Showsignatures = Convert.ToInt16(showsignatures.SelectedValue);
                __configinfo.Showavatars = Convert.ToInt16(showavatars.SelectedValue);
                __configinfo.Showimages = Convert.ToInt16(showimages.SelectedValue);
                __configinfo.Smiliesmax = Convert.ToInt16(smiliesmax.Text);
                __configinfo.Maxsigrows = Convert.ToInt16(maxsigrows.Text);
                __configinfo.Viewnewtopicminute = Convert.ToInt16(viewnewtopicminute.Text);
                __configinfo.Whosonlinecontract = Convert.ToInt16(whosonlinecontact.SelectedValue);

                GeneralConfigs.Serialiaze(__configinfo, Server.MapPath("../../config/general.config"));
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "��������ʾ��ʽ����", "");
                base.RegisterStartupScript( "PAGE",  "window.location.href='global_uiandshowstyle.aspx';");
            }

            #endregion
        }

        #region Web ������������ɵĴ���

        override protected void OnInit(EventArgs e)
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