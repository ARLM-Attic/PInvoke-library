using Discuz.Forum;
using Discuz.Config;

namespace Discuz.Forum
{
	/// <summary>
	/// AdminConfigFactory ��ժҪ˵����
	/// </summary>
	public class AdminConfigs : GeneralConfigs
	{
		/// <summary>
		/// ��ȡԭʼ��ȱʡ��̳����
		/// </summary>
		/// <returns></returns>
        public static GeneralConfigInfo GetDefaultConifg()
		{
			GeneralConfigInfo __configinfo = new GeneralConfigInfo();

			__configinfo.Forumtitle = "��̳����"; //��̳����
			__configinfo.Forumurl = "/"; //��̳url��ַ
			__configinfo.Webtitle = "��վ����"; //��վ����
			__configinfo.Weburl = "/"; //��̳��վurl��ַ
			__configinfo.Licensed = 1; //�Ƿ���ʾ��ҵ��Ȩ����
			__configinfo.Icp = ""; //��վ������Ϣ
			__configinfo.Closed = 0; //��̳�ر�
			__configinfo.Closedreason = "��Ǹ!��̳��ʱ�ر�,�Ժ���ܷ���."; //��̳�ر���ʾ��Ϣ

			__configinfo.Passwordkey = ForumUtils.CreateAuthStr(16); //�û�����Key

			__configinfo.Regstatus = 1; //�Ƿ��������û�ע��
			__configinfo.Regadvance = 1; //ע��ʱ���Ƿ���ʾ�߼�ѡ��
			__configinfo.Censoruser = "Administrator\r\nAdmin\r\n����Ա\r\n����"; //�û���Ϣ�����ؼ���
			__configinfo.Doublee = 0; //����ͬһ Email ע�᲻ͬ�û�
			__configinfo.Regverify = 0; //���û�ע����֤ 0=����֤ 1=email��֤ 2=�˹���֤
			__configinfo.Accessemail = ""; //Email�����ַ
			__configinfo.Censoremail = ""; //Email��ֹ��ַ
			__configinfo.Hideprivate = 1; //������Ȩ���ʵ���̳
			__configinfo.Regctrl = 0; //IP ע��������(Сʱ)
			__configinfo.Ipregctrl = ""; //���� IP ע������
			__configinfo.Ipaccess = ""; //IP�����б�
			__configinfo.Adminipaccess = ""; //����Ա��̨IP�����б�
			__configinfo.Newbiespan = 0; //���ּ�ϰ����(��λ:Сʱ)
			__configinfo.Welcomemsg = 1; //���ͻ�ӭ����Ϣ
			__configinfo.Welcomemsgtxt = "��ӭ��ע����뱾��̳!"; //��ӭ����Ϣ����
			__configinfo.Rules = 1; //�Ƿ���ʾע�����Э��
			__configinfo.Rulestxt = ""; //���Э������

			__configinfo.Templateid = 1; //Ĭ����̳���
			__configinfo.Hottopic = 15; //���Ż����������
			__configinfo.Starthreshold = 5; //����������ֵ
			__configinfo.Visitedforums = 10; //��ʾ���������̳����
			__configinfo.Maxsigrows = 20; //���ǩ���߶�(��)
			__configinfo.Moddisplay = 0; //������ʾ��ʽ 0=ƽ����ʾ 1=�����˵�
			__configinfo.Subforumsindex = 0; //��ҳ�Ƿ���ʾ��̳���¼�����̳
			__configinfo.Stylejump = 0; //��ʾ��������˵�
			__configinfo.Fastpost = 1; //���ٷ���
			__configinfo.Showsignatures = 1; //�Ƿ���ʾǩ��
			__configinfo.Showavatars = 1; //�Ƿ���ʾͷ��
			__configinfo.Showimages = 1; //�Ƿ�����������ʾͼƬ

			__configinfo.Archiverstatus = 1; //���� Archiver
			__configinfo.Seotitle = ""; //���⸽����
			__configinfo.Seokeywords = ""; //Meta Keywords
			__configinfo.Seodescription = ""; //Meta Description
			__configinfo.Seohead = ""; //����ͷ����Ϣ

			__configinfo.Rssstatus = 1; //rssstatus
			__configinfo.Rssttl = 60; //RSS TTL(����)
			__configinfo.Nocacheheaders = 0; //��ֹ���������
			__configinfo.Fullmytopics = 0; //�ҵĻ���ȫ������ 0=ֻ�����û������ⷢ���ߵ����� 1=�����û������ⷢ���߻�ظ��ߵ�����
			__configinfo.Debug = 1; //��ʾ����������Ϣ
			__configinfo.Rewriteurl = ""; //α��̬url���滻����

			__configinfo.Whosonlinestatus = 3; //��ʾ�����û� 0=����ʾ 1=������ҳ��ʾ 2=���ڷ���̳��ʾ 3=����ҳ�ͷ���̳��ʾ
			__configinfo.Maxonlinelist = 300; //�����ʾ��������
			__configinfo.Userstatusby = 2; //��������ʾ�û�ͷ��
			__configinfo.Forumjump = 1; //��ʾ��̳��ת�˵�
			__configinfo.Modworkstatus = 1; //��̳������ͳ��
			__configinfo.Maxmodworksmonths = 3; //�����¼����ʱ��(��)

			__configinfo.Seccodestatus = "register.aspx"; //ʹ����֤���ҳ���б�,��","�ָ� ����:register.aspx,login.aspx
			__configinfo.Maxonlines = 9000; //�����������
			__configinfo.Postinterval = 20; //������ˮԤ��(��)
			__configinfo.Searchctrl = 0; //����ʱ������(��)
			__configinfo.Maxspm = 0; //60 �������������

			__configinfo.Visitbanperiods = ""; //��ֹ����ʱ���
			__configinfo.Postbanperiods = ""; //��ֹ����ʱ���
			__configinfo.Postmodperiods = ""; //�������ʱ���
			__configinfo.Attachbanperiods = ""; //��ֹ���ظ���ʱ���
			__configinfo.Searchbanperiods = ""; //��ֹȫ������ʱ���

			__configinfo.Memliststatus = 1; //����鿴��Ա�б�
			__configinfo.Dupkarmarate = 0; //�����ظ�����
			__configinfo.Minpostsize = 10; //������С����(��)
			__configinfo.Maxpostsize = 500000; //�����������(��)
			__configinfo.Tpp = 25; //ÿҳ������
			__configinfo.Ppp = 20; //ÿҳ������
			__configinfo.Maxfavorites = 100; //�ղؼ�����
			__configinfo.Maxavatarsize = 20480; //ͷ�����ߴ�(�ֽ�)
			__configinfo.Maxavatarwidth = 120; //ͷ�������(����)
            __configinfo.Maxavatarheight = 120; //ͷ�����߶�(����);
			__configinfo.Maxpolloptions = 10; //ͶƱ���ѡ����
			__configinfo.Maxattachments = 10; //���������ϴ�������

			__configinfo.Attachimgpost = 1; //��������ʾͼƬ����
			__configinfo.Attachrefcheck = 0; //���ظ�����·���
			__configinfo.Attachsave = 3; //�������淽ʽ 0=ȫ������ͬһĿ¼ 1=����̳���벻ͬĿ¼ 2=���ļ����ʹ��벻ͬĿ¼ 3=�������մ��벻ͬĿ¼
			__configinfo.Watermarkstatus = 0; //ͼƬ�������ˮӡ 0=��ʹ�� 1=���� 2=���� 3=���� 4=���� ... 9=����

			__configinfo.Karmaratelimit = 10; //����ʱ������(Сʱ)
			__configinfo.Losslessdel = 5; //ɾ����������ʱ������(��)
			__configinfo.Edittimelimit = 0; //�༭����ʱ������(����)
			__configinfo.Editedby = 1; //�༭���Ӹ��ӱ༭��¼
			__configinfo.Defaulteditormode = 1; //Ĭ�ϵı༭��ģʽ 0=ubb����༭�� 1=���ӻ��༭��
			__configinfo.Allowswitcheditor = 1; //�Ƿ������л��༭��ģʽ
			__configinfo.Smileyinsert = 1; //��ʾ�ɵ������

			return __configinfo;

		}
	}
}