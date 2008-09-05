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
        #region 页面变量
#if NET1
		

         public MyAttachmentInfoCollection myattachmentlist;

        public System.Collections.ArrayList typelist;

#else
        /// <summary>
        /// 帖子所属的主题列表
        /// </summary>
        /// 
        
        public List<MyAttachmentInfo> myattachmentlist;

        public List<AttachmentType> typelist;
       
#endif
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid;
        /// <summary>
        /// 总页数
        /// </summary>
        public int pagecount;
        /// <summary>
        /// 附件总数
        /// </summary>
        public int attachmentcount;
        /// <summary>
        /// 分页页码链接
        /// </summary>
        public string pagenumbers;
        /// <summary>
        /// 当前登录的用户信息
        /// </summary>
        public UserInfo user = new UserInfo();
        /// <summary>
        /// 文件类型
        /// </summary>
        public int typeid = 0;
        #endregion

        private int pagesize = 16;

       

        protected override void ShowPage()
        {
           
            string linkurl = "";
            pagetitle = "用户控制面板";
            
            if (userid == -1)
            {
                AddErrLine("你尚未登录");
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
            //修正请求页数中可能的错误
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