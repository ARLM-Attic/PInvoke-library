using System;
using System.Data;
using System.Data.SqlClient;
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
    /// 用户收件箱页面
    /// </summary>
    public class usercpinbox : PageBase
    {
        #region 页面变量

#if NET1
		public PrivateMessageInfoCollection pmlist;
#else
        /// <summary>
        /// 短消息列表
        /// </summary>
        public List<PrivateMessageInfo> pmlist = new List<PrivateMessageInfo>();
#endif

        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid;

        /// <summary>
        /// 短消息数
        /// </summary>
        public int pmcount;

        /// <summary>
        /// 分页页数
        /// </summary>
        public int pagecount;

        /// <summary>
        /// 分页页码链接
        /// </summary>
        public string pagenumbers;

        /// <summary>
        /// 用户最大短消息数
        /// </summary>
        public int maxmsg;

        /// <summary>
        /// 已使用的短消息数
        /// </summary>
        public int usedmsgcount;

        /// <summary>
        /// 已使用的短消息条宽度
        /// </summary>
        public int usedmsgbarwidth;

        /// <summary>
        /// 未使用的短消息条宽度
        /// </summary>
        public int unusedmsgbarwidth;

        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserInfo user = new UserInfo();

        #endregion

        protected override void ShowPage()
        {
            pagetitle = "短消息收件箱";

            if (userid == -1)
            {
                AddErrLine("你尚未登录");
                return;
            }
            user = Users.GetUserInfo(userid);

            if (DNTRequest.IsPost())
            {
                if (ForumUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }
                if(Utils.StrIsNullOrEmpty(DNTRequest.GetFormString("pmitemid")))
                {
                    AddErrLine("您未选中任何短消息，当前操作失败！");
                    return;
                }

                if (!Utils.IsNumericList(DNTRequest.GetFormString("pmitemid")))
                {
                    AddErrLine("参数信息错误！");
                    return;
                }

                string[] pmitemid = Utils.SplitString(DNTRequest.GetFormString("pmitemid"), ",");

                int retval = PrivateMessages.DeletePrivateMessage(userid, pmitemid);

                if (retval == -1)
                {
                    AddErrLine("参数无效");
                    return;
                }

                SetUrl("usercpinbox.aspx");
                SetMetaRefresh();
                SetShowBackLink(true);
                AddMsgLine("删除完毕");
            }
            else
            {
                BindItems();
            }
        }

        /// <summary>
        /// 加载用户当前请求页数的短消息列表并装入DataTable中
        /// </summary>
        private void BindItems()
        {
            //得到当前用户请求的页数
            pageid = DNTRequest.GetInt("page", 1);
            //获取主题总数
            pmcount = PrivateMessages.GetPrivateMessageCount(userid, 0);
            //获取总页数
            pagecount = pmcount%16 == 0 ? pmcount/16 : pmcount/16 + 1;
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

            usedmsgcount = PrivateMessages.GetPrivateMessageCount(userid, -1);
            maxmsg = usergroupinfo.Maxpmnum;

            if (maxmsg <= 0)
            {
                usedmsgbarwidth = 0;
                unusedmsgbarwidth = 0;
            }
            else
            {
                usedmsgbarwidth = usedmsgcount*100/maxmsg;
                unusedmsgbarwidth = 100 - usedmsgbarwidth;
            }

//				//用户是不是有新的短信息,如果没有则设置用户的短信息状态为没有状态(状态为 0 )
//				pmlist = PrivateMessages.GetPrivateMessageList(userid, 0, 1, 1, "[new]=1");
//				if (pmlist!=null)
//				{
//					if (pmlist.Rows.Count<=0)
//					{
//						Users.SetUserNewPMCount(userid,0);
//					}
//				}
//				else
//				{
//					Users.SetUserNewPMCount(userid,0);
//				}

            pmlist = PrivateMessages.GetPrivateMessageCollection(userid, 0, 16, pageid, 2);
            pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "usercpinbox.aspx", 8);
        }
    }
}