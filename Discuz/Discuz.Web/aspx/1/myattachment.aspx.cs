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
    public partial class myattachment : PageBase
    {
        #region ҳ�����
#if NET1
		

         public MyAttachmentInfoCollection myattachmentlist;

        public System.Collections.ArrayList typelist;

#else
        /// <summary>
        /// ���������������б�
        /// </summary>
        /// 
        
        public List<MyAttachmentInfo> myattachmentlist;

        public List<AttachmentType> typelist;
       
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
        public int attachmentcount;
        /// <summary>
        /// ��ҳҳ������
        /// </summary>
        public string pagenumbers;
        /// <summary>
        /// ��ǰ��¼���û���Ϣ
        /// </summary>
        public UserInfo user = new UserInfo();
        /// <summary>
        /// �ļ�����
        /// </summary>
        public int typeid = 0;
        #endregion

        private int pagesize = 16;

       

        protected override void ShowPage()
        {
           
            string linkurl = "";
            pagetitle = "�û��������";
            
            if (userid == -1)
            {
                AddErrLine("����δ��¼");
                return;
            }

            user = Users.GetUserInfo(userid);
            pageid = DNTRequest.GetInt("page", 1);
            typeid = DNTRequest.GetInt("typeid", 0);

            typelist = Attachments.AttachTypeList();
           
            if (typeid > 0)
            {
                attachmentcount = Attachments.GetUserAttachmentCount(this.userid,typeid);
                linkurl=string.Format("myattachment.aspx?typeid={0}", typeid.ToString());
            }
            else
            {
                attachmentcount = Attachments.GetUserAttachmentCount(this.userid);
                linkurl="myattachment.aspx";
            }
            pagecount = attachmentcount % pagesize == 0 ? attachmentcount / pagesize : attachmentcount / pagesize + 1;
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
            myattachmentlist = Attachments.GetAttachmentByUid(this.userid, typeid, pageid, pagesize);
           
                pagenumbers = Utils.GetPageNumbers(pageid, pagecount,linkurl, 10);
            
        }
    }
}