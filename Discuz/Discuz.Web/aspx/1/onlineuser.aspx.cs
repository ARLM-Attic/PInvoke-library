using System;
using System.Data;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;

namespace Discuz.Web
{
	/// <summary>
	/// 在线用户列表页
	/// </summary>
	public class onlineuser : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 在线用户列表
		/// </summary>
		public DataTable onlineuserlist;
        /// <summary>
        /// 在线用户数
        /// </summary>
        public int onlineusernumber = 0;
		/// <summary>
        /// 当前页码
		/// </summary>
		public int pageid = 0;
        /// <summary>
        /// 总页数
        /// </summary>
		public int pagecount = 0;
        /// <summary>
        /// 分页页码链接
        /// </summary>
		public string  pagenumbers = "";
        /// <summary>
        /// 在线用户总数
        /// </summary>
		public int totalonline;
        /// <summary>
        /// 在线注册用户数
        /// </summary>
		public int totalonlineuser;
        /// <summary>
        /// 在线游客数
        /// </summary>
		public int totalonlineguest;
        /// <summary>
        /// 在线隐身用户数
        /// </summary>
		public int totalonlineinvisibleuser;
        /// <summary>
        /// 最高在线用户数
        /// </summary>
		public string highestonlineusercount;
        /// <summary>
        /// 最高在线用户数发生时间
        /// </summary>
		public string highestonlineusertime;
        #endregion

        private int upp = 16;
	    //开始行数
		private int startrow = 0;
		//结束行数
		private int endrow = 0;
		protected override void ShowPage()
		{
            pagetitle = "在线列表";
			DataTable allonlineuserlist = OnlineUsers.GetOnlineUserList(onlineusercount, out totalonlineguest, out totalonlineuser, out totalonlineinvisibleuser);;
			onlineusernumber = onlineusercount;

			onlineuserlist = CreateUserTable();
		
			//获取总页数
			pagecount = onlineusernumber%upp == 0 ? onlineusernumber/upp : onlineusernumber/upp + 1;
			if (pagecount == 0)
			{
                pagecount = 1;
			}

			//得到当前用户请求的页数
			pageid = DNTRequest.GetInt("page", 1);
			//修正请求页数中可能的错误
			if (pageid <= 1)
			{
				pageid = 1;
				startrow = 0;
				endrow = upp - 1;
			}
			else
			{
				if (pageid > pagecount)
				{
					pageid = pagecount;
				}
                
				startrow = (pageid - 1) * upp;
				endrow = pageid * upp;
			}
		  
            if (startrow >= onlineusernumber) 
				startrow = onlineusernumber - 1;
			if (endrow >= onlineusernumber) 
				endrow = onlineusernumber - 1;
			
			for (;startrow <= endrow; startrow++)
			{

                try
                {
					DataRow newrow = onlineuserlist.NewRow();
				
					newrow["username"] = allonlineuserlist.Rows[startrow]["username"].ToString();
					newrow["userid"] = Convert.ToInt32(allonlineuserlist.Rows[startrow]["userid"].ToString());
					newrow["invisible"] = Convert.ToInt32(allonlineuserlist.Rows[startrow]["invisible"].ToString());
					newrow["lastupdatetime"] = Convert.ToDateTime(allonlineuserlist.Rows[startrow]["lastupdatetime"].ToString());
					string actionid = allonlineuserlist.Rows[startrow]["action"].ToString().Trim();
					if (actionid != "")
					{
						newrow["action"] = UserAction.GetActionDescriptionByID(Convert.ToInt32(actionid));
					}
			
					newrow["forumid"] = Convert.ToInt32(allonlineuserlist.Rows[startrow]["forumid"].ToString());
					newrow["forumname"] = allonlineuserlist.Rows[startrow]["forumname"].ToString();
					newrow["topicid"] = Convert.ToInt32(allonlineuserlist.Rows[startrow]["titleid"].ToString());
					newrow["title"] = allonlineuserlist.Rows[startrow]["title"].ToString();
				
					onlineuserlist.Rows.Add(newrow);
					onlineuserlist.AcceptChanges();
                }
                catch
                { ;}
			}

			//得到页码链接
			if (DNTRequest.GetString("search") == "")
			{
				pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "onlineuser.aspx", 8);
			}
			else
			{
				pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "onlineuser.aspx", 8);
			}

			totalonline = onlineusercount;
			highestonlineusercount = Statistics.GetStatisticsRowItem("highestonlineusercount");
			highestonlineusertime = Statistics.GetStatisticsRowItem("highestonlineusertime");
	   	}


		public DataTable CreateUserTable()
		{
			DataTable dt = new DataTable("onlineuser");
		
			dt.Columns.Add("userid", System.Type.GetType("System.Int32"));
			dt.Columns["userid"].AllowDBNull = false;

			dt.Columns.Add("invisible", System.Type.GetType("System.Int32"));
			dt.Columns["invisible"].AllowDBNull = false;


			dt.Columns.Add("username", System.Type.GetType("System.String"));
			dt.Columns["username"].AllowDBNull = false;
			dt.Columns["username"].MaxLength = 20;
			dt.Columns["username"].DefaultValue = "";
			
			dt.Columns.Add("lastupdatetime", System.Type.GetType("System.DateTime"));
		
			dt.Columns.Add("action", System.Type.GetType("System.String"));
			dt.Columns["action"].MaxLength = 40;
			dt.Columns["action"].DefaultValue = "";
		
			dt.Columns.Add("forumid", System.Type.GetType("System.Int32"));
			dt.Columns.Add("forumname", System.Type.GetType("System.String"));
			dt.Columns["forumname"].MaxLength = 50;
			dt.Columns["forumname"].DefaultValue = "";

			dt.Columns.Add("topicid", System.Type.GetType("System.Int32"));
			dt.Columns.Add("title", System.Type.GetType("System.String"));
			dt.Columns["title"].MaxLength = 80;
			dt.Columns["title"].DefaultValue = "";

            return dt;
		}
      

	}
}
