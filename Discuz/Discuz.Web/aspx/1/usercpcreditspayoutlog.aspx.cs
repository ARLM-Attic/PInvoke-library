using System;
using System.Data;
using System.Data.SqlClient;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;

namespace Discuz.Web
{
	/// <summary>
	/// 支出记录
	/// </summary>
	public class usercpcreditspayoutlog : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 当前页码
        /// </summary>
		public int pageid;
        /// <summary>
        /// 积分支出记录数
        /// </summary>
		public int payoutlogcount;
		/// <summary>
        /// 分页页数
		/// </summary>
		public int pagecount;
        /// <summary>
        /// 分页页码链接
        /// </summary>
		public string pagenumbers;
        /// <summary>
        /// 积分支出记录列表
        /// </summary>
		public DataTable payloglist=new DataTable();
        /// <summary>
        /// 当前用户信息
        /// </summary>
		public UserInfo user = new UserInfo();
        #endregion
        private int pagesize=16;
		protected override void ShowPage()
		{
			pagetitle = "用户控制面板";

			if (userid == -1)
			{
				AddErrLine("你尚未登录");
				
				return;
			}
			user = Users.GetUserInfo(userid);

			//得到当前用户请求的页数
			pageid = DNTRequest.GetInt("page", 1);
			//获取主题总数
			payoutlogcount = PaymentLogs.GetPaymentLogOutRecordCount(userid);
			//获取总页数
			pagecount = payoutlogcount%pagesize==0 ? payoutlogcount/pagesize : payoutlogcount/pagesize + 1;
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

			//获取收入记录并分页显示
			payloglist = PaymentLogs.GetPayLogOutList(pagesize,pageid,userid);

			pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "usercpcreditspayoutlog.aspx", 8);
				

		}
	}
}
