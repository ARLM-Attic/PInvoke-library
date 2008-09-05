using System;
using System.IO;
using System.Web.UI;
using Discuz.Cache;
using Discuz.Control;
using Discuz.Forum;

using Discuz.Aggregation;
using Discuz.Common;

namespace Discuz.Web.Admin
{
    /// <summary>
    /// �������
    /// </summary>
    
#if NET1
    public class cachemanage : AdminPage
#else
    public partial class cachemanage : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.Button ResetAllCache;
        protected Discuz.Control.Button ReSetPostTableInfo;
        protected Discuz.Control.Button ResetMGinf;
        protected Discuz.Control.Button ResetUGinf;
        protected Discuz.Control.Button ResetForumInf;
        protected Discuz.Control.Button ResetAnnonceList;
        protected Discuz.Control.Button ResetFirstAnnounce;
        protected Discuz.Control.Button ResetForumDropList;
        protected Discuz.Control.Button ResetSmiles;
        protected Discuz.Control.Button ResetAddressRefer;
        protected Discuz.Control.Button ResetThemeIcon;
        protected Discuz.Control.Button ResetFlag;
        protected Discuz.Control.Button ReSetDigestTopicList;
        protected Discuz.Control.Button ReSetHotTopicList;
        protected Discuz.Control.Button ResetForumsStaticInf;
        protected Discuz.Control.Button ResetAttachSize;
        protected Discuz.Control.Button ResetTemplateDropDown;
        protected Discuz.Control.Button ResetOnlineInco;
        protected Discuz.Control.Button ResetLink;
        protected Discuz.Control.Button ResetWord;
        protected Discuz.Control.Button ResetForumList;
        protected Discuz.Control.Button ResetRss;
        protected Discuz.Control.Button ResetRssAll;
        protected Discuz.Control.Button ResetTemplateIDList;
        protected Discuz.Control.Button ResetValidUserExtField;
        protected Discuz.Control.Button ResetOnlineUserInfo;
        protected Discuz.Control.Button ResetMedalList;
        protected Discuz.Control.Button ReSetAdsList;
        protected Discuz.Control.Button ReSetStatisticsSearchtime;
        protected Discuz.Control.Button ReSetStatisticsSearchcount;
        protected Discuz.Control.Button ReSetCommonAvatarList;
        protected Discuz.Control.Button ReSetJammer;
        protected Discuz.Control.Button ReSetMagicList;
        protected Discuz.Control.Button ReSetScorePaySet;
        protected Discuz.Control.Button ReSetScoreset;
        protected Discuz.Control.Button ResetForumBaseSet;
        protected Discuz.Control.Button ReSetRecentTopicList;
        protected Discuz.Control.Button ResetRssByFid;
        protected Discuz.Control.Button ReSetAggregation;
        protected Discuz.Control.TextBox txtRssfid;
        protected Discuz.Control.Button ReSetTopiclistByFid;
        protected Discuz.Control.TextBox txtTopiclistFid;
        protected Discuz.Control.Button ReSetAlbumCategory;
        #endregion
#endif


        protected void Page_Load(object sender, EventArgs e)
        { }

        private void ReSetDigestTopicList_Click(object sender, EventArgs e)
        {
            #region ��������ȫ����龫�������б�

            if (this.CheckCookie())
            {
                AdminCaches.ReSetDigestTopicList(16);
                SubmitReturnInf();
            }

            #endregion
        }

        private void ReSetHotTopicList_Click(object sender, EventArgs e)
        {
            #region ��������ȫ��������������б�

            if (this.CheckCookie())
            {
                AdminCaches.ReSetHotTopicList(16, 30);
                SubmitReturnInf();
            }

            #endregion
        }

        private void ReSetAdsList_Click(object sender, EventArgs e)
        {
            #region �������б�

            if (this.CheckCookie())
            {
                AdminCaches.ReSetAdsList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ReSetStatisticsSearchtime_Click(object sender, EventArgs e)
        {
            #region ���������û���һ��ִ������������ʱ��

            if (this.CheckCookie())
            {
                AdminCaches.ReSetStatisticsSearchtime();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ReSetStatisticsSearchcount_Click(object sender, EventArgs e)
        {
            #region ���������û���һ�����������Ĵ���

            if (this.CheckCookie())
            {
                AdminCaches.ReSetStatisticsSearchcount();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ReSetCommonAvatarList_Click(object sender, EventArgs e)
        {
            #region ���������û�ͷ���б�

            if (this.CheckCookie())
            {
                AdminCaches.ReSetCommonAvatarList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ReSetJammer_Click(object sender, EventArgs e)
        {
            #region �������ø������ַ���

            if (this.CheckCookie())
            {
                AdminCaches.ReSetJammer();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ReSetMagicList_Click(object sender, EventArgs e)
        {
            #region ��������ħ���б�

            if (this.CheckCookie())
            {
                AdminCaches.ReSetMagicList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ReSetScorePaySet_Click(object sender, EventArgs e)
        {
            #region �������öһ����ʵĿɽ��׻��ֲ���

            if (this.CheckCookie())
            {
                AdminCaches.ReSetScorePaySet();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ReSetPostTableInfo_Click(object sender, EventArgs e)
        {
            #region �������õ�ǰ���ӱ������Ϣ

            if (this.CheckCookie())
            {
                AdminCaches.ReSetPostTableInfo();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ReSetTopiclistByFid_Click(object sender, EventArgs e)
        {
            #region ����������Ӧ�������б�

            if (this.CheckCookie())
            {
                if (txtTopiclistFid.Text == "")
                {
                    base.RegisterStartupScript( "", "<script>alert('����������Ӧ�����б�İ�������Ч!');window.location.href='global_cachemanage.aspx';</script>");
                    return;
                }
                AdminCaches.ReSetTopiclistByFid(txtTopiclistFid.Text);
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetMGinf_Click(object sender, EventArgs e)
        {
            #region �������ù�������Ϣ

            if (this.CheckCookie())
            {
                AdminCaches.ReSetAdminGroupList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetUGinf_Click(object sender, EventArgs e)
        {
            #region ���������û�����Ϣ

            if (this.CheckCookie())
            {
                AdminCaches.ReSetUserGroupList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetForumInf_Click(object sender, EventArgs e)
        {
            #region �������ð�����Ϣ

            if (this.CheckCookie())
            {
                AdminCaches.ReSetModeratorList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetAnnonceList_Click(object sender, EventArgs e)
        {
            #region ��������ָ��ʱ���ڵĹ����б�

            if (this.CheckCookie())
            {
                AdminCaches.ReSetAnnouncementList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetFirstAnnounce_Click(object sender, EventArgs e)
        {
            #region �������õ�һ������

            if (this.CheckCookie())
            {
                AdminCaches.ReSetSimplifiedAnnouncementList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetForumDropList_Click(object sender, EventArgs e)
        {
            #region �������ð�������б�

            if (this.CheckCookie())
            {
                AdminCaches.ReSetForumListBoxOptions();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetSmiles_Click(object sender, EventArgs e)
        {
            #region �������ñ���

            if (this.CheckCookie())
            {
                AdminCaches.ReSetSmiliesList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetThemeIcon_Click(object sender, EventArgs e)
        {
            #region ������������ͼ��

            if (this.CheckCookie())
            {
                AdminCaches.ReSetIconsList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetForumBaseSet_Click(object sender, EventArgs e)
        {
            #region ����������̳��������

            if (this.CheckCookie())
            {
                AdminCaches.ReSetConfig();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetAddressRefer_Click(object sender, EventArgs e)
        {
            #region �������õ�ַ���ձ�

            if (this.CheckCookie())
            {
                AdminCaches.ReSetSiteUrls();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetForumsStaticInf_Click(object sender, EventArgs e)
        {
            #region ����������̳ͳ����Ϣ

            if (this.CheckCookie())
            {
                AdminCaches.ReSetStatistics();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetAllCache_Click(object sender, EventArgs e)
        {
            #region �������л���

            if (this.CheckCookie())
            {
                AdminCaches.ReSetAllCache();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ReSetScoreset_Click(object sender, EventArgs e)
        {
            #region ����������̳��������

            if (this.CheckCookie())
            {
                AdminCaches.ReSetScoreset();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetAttachSize_Click(object sender, EventArgs e)
        {
            #region ��������ϵͳ����ĸ������ͺʹ�С

            if (this.CheckCookie())
            {
                AdminCaches.ReSetAttachmentTypeArray();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetTemplateDropDown_Click(object sender, EventArgs e)
        {
            #region ��������ģ���б��������html

            if (this.CheckCookie())
            {
                AdminCaches.ReSetTemplateListBoxOptionsCache();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetOnlineInco_Click(object sender, EventArgs e)
        {
            #region �������������û��б�ͼ��

            if (this.CheckCookie())
            {
                AdminCaches.ReSetOnlineGroupIconList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetLink_Click(object sender, EventArgs e)
        {
            #region �����������������б�

            if (this.CheckCookie())
            {
                AdminCaches.ReSetForumLinkList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetWord_Click(object sender, EventArgs e)
        {
            #region �����������ֹ����б�

            if (this.CheckCookie())
            {
                AdminCaches.ReSetBanWordList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetForumList_Click(object sender, EventArgs e)
        {
            #region ����������̳�б�

            if (this.CheckCookie())
            {
                AdminCaches.ReSetForumList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetRss_Click(object sender, EventArgs e)
        {
            #region ����������̳RSS

            if (this.CheckCookie())
            {
                AdminCaches.ReSetRss();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetOnlineUserInfo_Click(object sender, EventArgs e)
        {
            #region �������������û���Ϣ

            if (this.CheckCookie())
            {
                AdminCaches.ReSetOnlineUserTable();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetRssByFid_Click(object sender, EventArgs e)
        {
            #region ��������ָ�����RSS

            if (this.CheckCookie())
            {
                if (txtRssfid.Text == "")
                {
                    base.RegisterStartupScript( "", "<script>alert('��������ָ�����RSS�İ�������Ч!');window.location.href='global_cachemanage.aspx';</script>");
                    return;
                }

                AdminCaches.ReSetForumRssXml(Convert.ToInt32(txtRssfid.Text));
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetRssAll_Click(object sender, EventArgs e)
        {
            #region ����������̳����RSS

            if (this.CheckCookie())
            {
                AdminCaches.ReSetRssXml();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetTemplateIDList_Click(object sender, EventArgs e)
        {
            #region ����������̳ģ��id�б�

            if (this.CheckCookie())
            {
                AdminCaches.ReSetValidTemplateIDList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetValidUserExtField_Click(object sender, EventArgs e)
        {
            #region ����������Ч���û�����չ�ֶ�

            if (this.CheckCookie())
            {
                AdminCaches.ReSetValidScoreName();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetFlag_Click(object sender, EventArgs e)
        {
            #region ���������Զ����ǩ

            if (this.CheckCookie())
            {
                AdminCaches.ReSetCustomEditButtonList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ResetMedalList_Click(object sender, EventArgs e)
        {
            #region ��������ѫ���б�

            if (this.CheckCookie())
            {
                AdminCaches.ReSetMedalsList();
                SubmitReturnInf();
            }

            #endregion
        }

        private void ReSetAggregation_Click(object sender, EventArgs e)
        {
            #region �������þۺ�
            if (this.CheckCookie())
            {
                AggregationFacade.BaseAggregation.ClearAllDataBind();
                SubmitReturnInf();
            }
            #endregion
        }

        protected void ReSetNavPopupMenu_Click(object sender, EventArgs e)
        {
            #region ���赼�������˵�
            if (this.CheckCookie())
            {
                AdminCaches.ReSetNavPopupMenu();
            }
            #endregion
        }

        private void SubmitReturnInf()
        {
            if (this.CheckCookie())
            {
                base.RegisterStartupScript( "PAGE",  "window.location.href='global_cachemanage.aspx';");
            }
        }

        private void ReSetTag_Click(object sender, EventArgs e)
        {
            DNTCache cache = DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/Tag/Hot-" + config.Hottagcount);
        }

        protected void ReSetAlbumCategory_Click(object sender, EventArgs e)
        {
            #region ��������������
            if (this.CheckCookie())
            {
                AdminCaches.ResetAlbumCategory();
                SubmitReturnInf();
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
            this.ResetMGinf.Click += new EventHandler(this.ResetMGinf_Click);
            this.ResetUGinf.Click += new EventHandler(this.ResetUGinf_Click);
            this.ResetForumInf.Click += new EventHandler(this.ResetForumInf_Click);
            this.ResetAnnonceList.Click += new EventHandler(this.ResetAnnonceList_Click);
            this.ResetFirstAnnounce.Click += new EventHandler(this.ResetFirstAnnounce_Click);
            this.ResetForumDropList.Click += new EventHandler(this.ResetForumDropList_Click);
            this.ResetSmiles.Click += new EventHandler(this.ResetSmiles_Click);
            this.ResetThemeIcon.Click += new EventHandler(this.ResetThemeIcon_Click);
            this.ResetForumBaseSet.Click += new EventHandler(this.ResetForumBaseSet_Click);
            this.ReSetScoreset.Click += new EventHandler(this.ReSetScoreset_Click);
            this.ResetAddressRefer.Click += new EventHandler(this.ResetAddressRefer_Click);
            this.ResetForumsStaticInf.Click += new EventHandler(this.ResetForumsStaticInf_Click);
            this.ResetAttachSize.Click += new EventHandler(this.ResetAttachSize_Click);
            this.ResetTemplateDropDown.Click += new EventHandler(this.ResetTemplateDropDown_Click);
            this.ResetOnlineInco.Click += new EventHandler(this.ResetOnlineInco_Click);
            this.ResetLink.Click += new EventHandler(this.ResetLink_Click);
            this.ResetWord.Click += new EventHandler(this.ResetWord_Click);
            this.ResetForumList.Click += new EventHandler(this.ResetForumList_Click);
            this.ResetRss.Click += new EventHandler(this.ResetRss_Click);
            this.ResetRssByFid.Click += new EventHandler(this.ResetRssByFid_Click);
            this.ResetRssAll.Click += new EventHandler(this.ResetRssAll_Click);
            this.ResetTemplateIDList.Click += new EventHandler(this.ResetTemplateIDList_Click);
            this.ResetValidUserExtField.Click += new EventHandler(this.ResetValidUserExtField_Click);
            this.ResetOnlineUserInfo.Click += new EventHandler(this.ResetOnlineUserInfo_Click);
            this.ResetAllCache.Click += new EventHandler(this.ResetAllCache_Click);
            this.ResetFlag.Click += new EventHandler(this.ResetFlag_Click);
            this.ResetMedalList.Click += new EventHandler(this.ResetMedalList_Click);
            this.ReSetAdsList.Click += new EventHandler(this.ReSetAdsList_Click);
            this.ReSetStatisticsSearchtime.Click += new EventHandler(this.ReSetStatisticsSearchtime_Click);
            this.ReSetStatisticsSearchcount.Click += new EventHandler(this.ReSetStatisticsSearchcount_Click);
            this.ReSetCommonAvatarList.Click += new EventHandler(this.ReSetCommonAvatarList_Click);
            this.ReSetJammer.Click += new EventHandler(this.ReSetJammer_Click);
            this.ReSetMagicList.Click += new EventHandler(this.ReSetMagicList_Click);
            this.ReSetScorePaySet.Click += new EventHandler(this.ReSetScorePaySet_Click);
            this.ReSetPostTableInfo.Click += new EventHandler(this.ReSetPostTableInfo_Click);
            this.ReSetTopiclistByFid.Click += new EventHandler(this.ReSetTopiclistByFid_Click);
            this.ReSetDigestTopicList.Click += new EventHandler(this.ReSetDigestTopicList_Click);
            this.ReSetHotTopicList.Click += new EventHandler(this.ReSetHotTopicList_Click);
            this.ReSetAggregation.Click += new EventHandler(this.ReSetAggregation_Click);
            this.ReSetTag.Click += new EventHandler(this.ReSetTag_Click);

            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion

    }
}