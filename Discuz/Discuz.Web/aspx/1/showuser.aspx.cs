﻿using System;
using System.Data;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Web.UI;

namespace Discuz.Web
{
    /// <summary>
    /// 用户列表页面
    /// </summary>
    public class showuser : PageBase
    {
        #region 页面变量

        /// <summary>
        /// 用户列表
        /// </summary>
        public DataTable userlist;

        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid;

        /// <summary>
        /// 总用户数
        /// </summary>
        public int totalusers;

        /// <summary>
        /// 分页页数
        /// </summary>
        public int pagecount;

        /// <summary>
        /// 分页页码链接
        /// </summary>
        public string pagenumbers;

        #endregion

        protected override void ShowPage()
        {
            pagetitle = "用户";

            userlist = new DataTable();
            if (config.Memliststatus != 1)
            {
                AddErrLine("系统不允许查看用户列表");
                return;
            }

            string orderby = DNTRequest.GetString("orderby").Trim();
            //进行参数过滤
            if (!Utils.StrIsNullOrEmpty(orderby) && !Utils.InArray(orderby, "uid,username,credits,posts,admin,joindate,lastactivity"))
            {
                orderby = "uid";
            }
            
            string ordertype = DNTRequest.GetString("ordertype").Trim();
            //进行参数过滤      
            if (!ordertype.Equals("desc") && !ordertype.Equals("asc") )
            {
                ordertype = "desc";
            }

            //得到当前用户请求的页数
            pageid = DNTRequest.GetInt("page", 1);
            if (DNTRequest.GetString("orderby").Equals("admin"))
            {
                //获管理组用户总数
                totalusers = Users.GetUserCountByAdmin();
            }
            else
            {
                //获取用户总数
                totalusers = Users.GetUserCount();
            }
            //获取总页数
            pagecount = totalusers%20 == 0 ? totalusers/20 : totalusers/20 + 1;
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


            //获取当前页主题列表
            userlist = Users.GetUserList(20, pageid, orderby, ordertype);
            //得到页码链接
            pagenumbers = Utils.GetPageNumbers(pageid, pagecount, string.Format("showuser.aspx{0}", string.Format("?orderby={0}&ordertype={1}", orderby, ordertype)), 8);
        }
    }
}