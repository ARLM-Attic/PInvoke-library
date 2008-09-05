using System;
using System.Data;
using Discuz.Common;
#if NET1
#else
using Discuz.Common.Generic;
#endif
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;

namespace Discuz.Web
{
    /// <summary>
    /// �ҵ�����
    /// </summary>
    public class myposts : PageBase
    {
        #region ҳ�����
#if NET1
		public MyTopicInfoCollection topics;
#else
        /// <summary>
        /// ���������������б�
        /// </summary>
        public List<MyTopicInfo> topics;
#endif
        /// <summary>
        /// ��ǰҳ��
        /// </summary>
        public int pageid;
        /// <summary>
        /// ��ҳ��
        /// </summary>
        public int pagecount;
        /// <summary>
        /// ��������
        /// </summary>
        public int topiccount;
        /// <summary>
        /// ��ҳҳ������
        /// </summary>
        public string pagenumbers;
        /// <summary>
        /// ��ǰ��¼���û���Ϣ
        /// </summary>
        public UserInfo user = new UserInfo();
        #endregion

        private int pagesize = 16;

        protected override void ShowPage()
        {
            pagetitle = "�û��������";

            if (userid == -1)
            {
                AddErrLine("����δ��¼");
                return;
            }

            user = Users.GetUserInfo(userid);

            //�õ���ǰ�û������ҳ��
            pageid = DNTRequest.GetInt("page", 1);
            //��ȡ��������
            topiccount = Topics.GetTopicsCountbyReplyUserId(this.userid);
            //��ȡ��ҳ��
            pagecount = topiccount%pagesize == 0 ? topiccount/pagesize : topiccount/pagesize + 1;
            if (pagecount == 0)
            {
                pagecount = 1;
            }
            //��������ҳ���п��ܵĴ���
            if (pageid < 1)
            {
                pageid = 1;
            }
            if (pageid > pagecount)
            {
                pageid = pagecount;
            }

            this.topics = Topics.GetTopicsByReplyUserId(this.userid, pageid, pagesize, 600, config.Hottopic);

            pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "myposts.aspx", 8);
        }
    }
}