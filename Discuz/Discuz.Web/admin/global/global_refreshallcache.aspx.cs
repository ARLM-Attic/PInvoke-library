using System;
using System.Web.UI;

using Discuz.Common;
using Discuz.Forum;

namespace Discuz.Web.Admin
{
	/// <summary>
	/// ���»���
	/// </summary>
    public class global_refreshallcache : Page
	{
		private void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				int opnumber = DNTRequest.GetInt("opnumber", 0);
				int result = -1;

				#region ���ݻ������ѡ�������Ӧ�Ļ�������

				switch (opnumber)
				{
					case 1:
					{
                        //�����������Ϣ
						AdminCaches.ReSetAdminGroupList();
						result = 2;
						break;
					}
					case 2:
					{
                        //�����û�����Ϣ
                        AdminCaches.ReSetUserGroupList();
						result = 3;
						break;
					}
					case 3:
					{
                        //���������Ϣ
                        AdminCaches.ReSetModeratorList();
						result = 4;
						break;
					}

					case 4:
					{
                        //����ָ��ʱ���ڵĹ����б�
                        AdminCaches.ReSetAnnouncementList();
                        AdminCaches.ReSetSimplifiedAnnouncementList();
						result = 5;
						break;
					}
					case 5:
					{
                        //�����һ������
                        AdminCaches.ReSetSimplifiedAnnouncementList();
						result = 6;
						break;
					}
					case 6:
					{
                        //�����������б�
                        AdminCaches.ReSetForumListBoxOptions();
						result = 7;
						break;
					}
					case 7:
					{
                        //�������
                        AdminCaches.ReSetSmiliesList();
						result = 8;
						break;
					}
					case 8:
					{
                        //��������ͼ��
                        AdminCaches.ReSetIconsList();
						result = 9;
						break;
					}
					case 9:
					{
                        //�����Զ����ǩ
                        AdminCaches.ReSetCustomEditButtonList();
						result = 10;
						break;
					}
					case 10:
					{
                        //������̳��������
						//AdminCaches.ReSetConfig();
						result = 11;
						break;
					}
					case 11:
					{
                        //������̳����
                        AdminCaches.ReSetScoreset();
						result = 12;
						break;
					}
					case 12:
					{
                        //�����ַ���ձ�
                        AdminCaches.ReSetSiteUrls();
						result = 13;
						break;
					}
					case 13:
					{
                        //������̳ͳ����Ϣ
                        AdminCaches.ReSetStatistics();
						result = 14;
						break;
					}
					case 14:
					{
                        //����ϵͳ����ĸ������ͺʹ�С
                        AdminCaches.ReSetAttachmentTypeArray();
						result = 15;
						break;
					}
					case 15:
					{
                        //����ģ���б��������html
                        AdminCaches.ReSetTemplateListBoxOptionsCache();
						result = 16;
						break;
					}
					case 16:
					{
                        //���������û��б�ͼ��
                        AdminCaches.ReSetOnlineGroupIconList();
						result = 17;
						break;
					}
					case 17:
					{
                        //�������������б�
                        AdminCaches.ReSetForumLinkList();
						result = 18;
						break;
					}
					case 18:
					{
                        //�������ֹ����б�
                        AdminCaches.ReSetBanWordList();
						result = 19;
						break;
					}
					case 19:
					{
                        //������̳�б�
                        AdminCaches.ReSetForumList();
						result = 20;
						break;
					}
					case 20:
					{
                        //���������û���Ϣ
                        AdminCaches.ReSetOnlineUserTable();
						result = 21;
						break;
					}
					case 21:
					{
                        //������̳����RSS��ָ�����RSS
                        AdminCaches.ReSetRss();
						result = 22;
						break;
					}
					case 22:
					{
                        //������̳����RSS
                        AdminCaches.ReSetRssXml();
						result = 23;
						break;
					}
					case 23:
					{
                        //����ģ��ID�б�
                        AdminCaches.ReSetValidTemplateIDList();
						result = 24;
						break;
					}
					case 24:
					{
                        //������Ч�û�����չ�ֶ�
                        AdminCaches.ReSetValidScoreName();
						result = 25;
						break;
					}
					case 25:
					{
                        //����ѫ���б�
                        AdminCaches.ReSetMedalsList();
						result = 26;
						break;
					}
					case 26:
					{
                        //�����������Ӵ��ͱ�ǰ׺
                        AdminCaches.ReSetDBlinkAndTablePrefix();
						result = 27;
						break;
					}
					case 27:
					{
                        //���������б�
                        AdminCaches.ReSetAllPostTableName();
						result = 28;
						break;
					}
					case 28:
					{
                        //����������ӱ�
                        AdminCaches.ReSetLastPostTableName();
						result = 29;
						break;
					}
					case 29:
					{
                        //�������б�
                        AdminCaches.ReSetAdsList();
						result = 30;
						break;
					}
					case 30:
					{
                        //�����û���һ��ִ����������ʱ��
                        AdminCaches.ReSetStatisticsSearchtime();
						result = 31;
						break;
					}
					case 31:
					{
                        //�����û�һ��������������
                        AdminCaches.ReSetStatisticsSearchcount();
						result = 32;
						break;
					}
					case 32:
					{
                        //�����û�ͷ���б�
                        AdminCaches.ReSetCommonAvatarList();
						result = 33;
						break;
					}
					case 33:
					{
                        //����������ַ���
						AdminCaches.ReSetJammer();
						result = 34;
						break;
					}
					case 34:
					{
                        //����ħ���б�
						AdminCaches.ReSetMagicList();
						result = 35;
						break;
					}
					case 35:
					{
                        //����һ����ʿɽ��׻��ֲ���
						AdminCaches.ReSetScorePaySet();
						result = 36;
						break;
					}
					case 36:
					{
                        //���赱ǰ���ӱ������Ϣ
						AdminCaches.ReSetPostTableInfo();
						result = 37;
						break;
					}
					case 37:
					{
                        //����ȫ����龫�������б�
						AdminCaches.ReSetDigestTopicList(16);
						result = 38;
						break;
					}
					case 38:
					{
                        //����ȫ��������������б�
						AdminCaches.ReSetHotTopicList(16, 30);
						result = 39;
						break;
					}
					case 39:
					{
                        //������������б�
						AdminCaches.ReSetRecentTopicList(16);
						result = 40;
						break;
					}
					case 40:
					{
                        //����BaseConfig
						AdminCaches.EditDntConfig();
						result = 41;
						break;
					}
					case 41:
					{
                        //���������û���
						OnlineUsers.InitOnlineList();
						result = 42;
						break;
					}
                    case 42:
			        {
                        //���赼�������˵�
			            AdminCaches.ReSetNavPopupMenu();
			            result = -1;
			            break;
			        }
				}

				#endregion

				Response.Write(result);
				Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
				Response.Expires = -1;
				Response.End();
			}
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
