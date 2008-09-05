using System;
using System.Data;
using Discuz.Common;
using Discuz.Space.Entities;
using Discuz.Entity;
using Discuz.Data;
using Discuz.Config;
using System.Data.Common;
using Discuz.Cache;
using System.Collections;
using Discuz.Space.Utilities;

#if NET1
#else
using Discuz.Common.Generic;
using Discuz.Space.Data;
#endif

namespace Discuz.Space.Provider
{
	/// <summary>
	/// SpaceTemplateProvider 的摘要说明。
	/// </summary>
	public class BlogProvider
	{

		public BlogProvider()
		{
		}

        /// <summary>
        /// 通过spaceid得到UID
        /// </summary>
        /// <returns>用户ID</returns>
        public static int GetUidBySpaceid(string spaceid)
        {

            return Utils.StrToInt(DbProvider.GetInstance().GetUidBySpaceid(spaceid), -1);
        }


		#region Space 个人操作
		/// <summary>
		/// 通过数据读取获得当前用户的个性化设置
		/// </summary>
		/// <param name="datareader">数据读取</param>
		/// <returns></returns>
		public static SpaceConfigInfo GetSpaceConfigInfo(int userid)
		{
            IDataReader idatareader = DbProvider.GetInstance().GetSpaceConfigDataByUserID(userid);
		
			if(idatareader.Read())
			{
				SpaceConfigInfo spaceconfiginfo = new SpaceConfigInfo();

				spaceconfiginfo.SpaceID = Convert.ToInt32(idatareader["spaceid"].ToString());
				spaceconfiginfo.UserID = Convert.ToInt32(idatareader["userid"].ToString());
				spaceconfiginfo.Spacetitle = idatareader["spacetitle"].ToString().Trim();
				spaceconfiginfo.Description = idatareader["description"].ToString().Trim();
				spaceconfiginfo.BlogDispMode = Convert.ToInt32(idatareader["blogdispmode"].ToString());
				spaceconfiginfo.Bpp = Convert.ToInt32(idatareader["bpp"].ToString());
				spaceconfiginfo.Commentpref = Convert.ToInt32(idatareader["commentpref"].ToString());
				spaceconfiginfo.MessagePref = Convert.ToInt32(idatareader["messagepref"].ToString());
				spaceconfiginfo.Rewritename = idatareader["rewritename"].ToString();
				spaceconfiginfo.ThemeID = Convert.ToInt32(idatareader["themeid"].ToString());
				spaceconfiginfo.ThemePath = idatareader["themepath"].ToString().Trim();
				spaceconfiginfo.PostCount = Convert.ToInt32(idatareader["postcount"].ToString());
				spaceconfiginfo.CommentCount = Convert.ToInt32(idatareader["commentcount"].ToString());
				spaceconfiginfo.VisitedTimes = Convert.ToInt32(idatareader["visitedtimes"].ToString());
				spaceconfiginfo.CreateDateTime = Convert.ToDateTime(idatareader["createdatetime"].ToString());
				spaceconfiginfo.UpdateDateTime = Convert.ToDateTime(idatareader["updatedatetime"].ToString());
				spaceconfiginfo.DefaultTab = Convert.ToInt32(idatareader["defaulttab"]);
				spaceconfiginfo.Status = (SpaceStatusType) Convert.ToInt32(idatareader["status"]);

				idatareader.Close();
				return spaceconfiginfo;
			}
			else
			{
                idatareader.Close();
				return null;
			}
		}


		public static SpaceConfigInfo[] GetSpaceConfigInfoArray(DataTable dt)
		{
			if(dt == null || dt.Rows.Count == 0)
				return null;
			SpaceConfigInfo[] spaceconfiginfoarray = new SpaceConfigInfo[dt.Rows.Count];

			for(int i = 0 ; i < dt.Rows.Count ; i++)
			{
				spaceconfiginfoarray[i] = new SpaceConfigInfo();
				spaceconfiginfoarray[i].SpaceID = Convert.ToInt32(dt.Rows[i]["spaceid"].ToString());
				spaceconfiginfoarray[i].UserID = Convert.ToInt32(dt.Rows[i]["userid"].ToString());
				spaceconfiginfoarray[i].Spacetitle = dt.Rows[i]["spacetitle"].ToString();
				spaceconfiginfoarray[i].Description = dt.Rows[i]["description"].ToString();
				spaceconfiginfoarray[i].BlogDispMode = Convert.ToInt32(dt.Rows[i]["blogdispmode"].ToString());
				spaceconfiginfoarray[i].Bpp = Convert.ToInt32(dt.Rows[i]["bpp"].ToString());
				spaceconfiginfoarray[i].Commentpref = Convert.ToInt32(dt.Rows[i]["commentpref"].ToString());
				spaceconfiginfoarray[i].MessagePref = Convert.ToInt32(dt.Rows[i]["messagepref"].ToString());
				spaceconfiginfoarray[i].Rewritename = dt.Rows[i]["rewritename"].ToString();
				spaceconfiginfoarray[i].ThemeID = Convert.ToInt32(dt.Rows[i]["themeid"].ToString());
				spaceconfiginfoarray[i].ThemePath = dt.Rows[i]["themepath"].ToString();
				spaceconfiginfoarray[i].PostCount = Convert.ToInt32(dt.Rows[i]["postcount"].ToString());
				spaceconfiginfoarray[i].CommentCount = Convert.ToInt32(dt.Rows[i]["commentcount"].ToString());
				spaceconfiginfoarray[i].VisitedTimes = Convert.ToInt32(dt.Rows[i]["visitedtimes"].ToString());
				spaceconfiginfoarray[i].CreateDateTime = Convert.ToDateTime(dt.Rows[i]["createdatetime"].ToString());
				spaceconfiginfoarray[i].UpdateDateTime = Convert.ToDateTime(dt.Rows[i]["updatedatetime"].ToString());
				spaceconfiginfoarray[i].Status = (SpaceStatusType) Convert.ToInt32(dt.Rows[i]["status"]);
			}

			dt.Dispose();
			return spaceconfiginfoarray;
		}
		#endregion

        //#region Space 模板操作
        ///// <summary>
        ///// 通过数据读取获得模板内容
        ///// </summary>
        ///// <param name="datareader">数据读取</param>
        ///// <returns></returns>
        //public static TemplateInfo GetSpaceTemplateInfo(IDataReader datareader)
        //{
        //    if(datareader == null)
        //        return null;

        //    if (datareader.Read())
        //    {
        //        TemplateInfo __spacetemplateinfo = new TemplateInfo();
        //        __spacetemplateinfo.TemplateId = Convert.ToInt32(datareader["templateid"].ToString());
        //        __spacetemplateinfo.Name = datareader["name"].ToString();
        //        __spacetemplateinfo.Path = datareader["path"].ToString();

        //        datareader.Dispose();
        //        return __spacetemplateinfo;
        //    }
        //    else
        //        return null;
        //}


        //public static TemplateInfo[] GetSpaceTemplateInfoArray(DataTable dt)
        //{
        //    if(dt == null || dt.Rows.Count == 0)
        //        return null;
        //    TemplateInfo[] __spacetemplatinfoarray = new TemplateInfo[dt.Rows.Count];
        //    for(int i = 0; i < dt.Rows.Count ; i++)
        //    {
        //        __spacetemplatinfoarray[i] = new TemplateInfo();
        //        __spacetemplatinfoarray[i].TemplateId = Convert.ToInt32(dt.Rows[i]["templateid"].ToString());
        //        __spacetemplatinfoarray[i].Name = dt.Rows[i]["name"].ToString();
        //        __spacetemplatinfoarray[i].Path = dt.Rows[i]["path"].ToString();
        //    }

        //    dt.Dispose();
        //    return __spacetemplatinfoarray;
        //}

        //#endregion

		#region Space 主题操作
		/// <summary>
		/// 通过数据读取获得主题内容
		/// </summary>
		/// <param name="datareader">数据读取</param>
		/// <returns></returns>
		public static ThemeInfo GetSpaceThemeInfo(IDataReader idatareader)
		{
			if (idatareader == null)
				return null;

			if (idatareader.Read())
			{
				ThemeInfo spacethemeinfo = new ThemeInfo();
				spacethemeinfo.ThemeId = Convert.ToInt32(idatareader["themeid"].ToString());
				spacethemeinfo.Directory = idatareader["directory"].ToString();
				spacethemeinfo.Name = idatareader["name"].ToString();
				spacethemeinfo.Type = Convert.ToInt32(idatareader["type"].ToString());
				spacethemeinfo.Author = idatareader["author"].ToString();
				spacethemeinfo.CreateDate = idatareader["createdate"].ToString();
				spacethemeinfo.CopyRight = idatareader["copyright"].ToString();

				idatareader.Close();
				return spacethemeinfo;
			}
			else
			{
                idatareader.Close();
				return null;
			}
		}

        /// <summary>
        /// 获取空间主题信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
		public static ThemeInfo[] GetSpaceThemeInfoArray(DataTable dt)
		{
			if(dt == null || dt.Rows.Count == 0)
				return null;
			ThemeInfo[] spacethemeinfoarray = new ThemeInfo[dt.Rows.Count];
			for(int i = 0 ; i < dt.Rows.Count ; i++)
			{
				spacethemeinfoarray[i] = new ThemeInfo();
				spacethemeinfoarray[i].ThemeId = Convert.ToInt32(dt.Rows[i]["themeid"].ToString());
				spacethemeinfoarray[i].Directory = dt.Rows[i]["datareader"].ToString();
				spacethemeinfoarray[i].Name = dt.Rows[i]["name"].ToString();
				spacethemeinfoarray[i].Type = Convert.ToInt32(dt.Rows[i]["type"].ToString());
				spacethemeinfoarray[i].Author = dt.Rows[i]["author"].ToString();
				spacethemeinfoarray[i].CreateDate = dt.Rows[i]["createdate"].ToString();
				spacethemeinfoarray[i].CopyRight = dt.Rows[i]["copyright"].ToString();
			}

			dt.Dispose();
			return spacethemeinfoarray;
		}
		#endregion

		#region Space 评论操作

        /// <summary>
        /// 获取单个评论对象
        /// </summary>
        /// <param name="__idatareader"></param>
        /// <returns></returns>
		public static SpaceCommentInfo GetSpaceCommentInfo (IDataReader idatareader)
		{
			if(idatareader == null)
				return null;

			if(idatareader.Read())
			{

				SpaceCommentInfo spacecommentsinfo = new SpaceCommentInfo();
				spacecommentsinfo.CommentID = Convert.ToInt32(idatareader["commentid"].ToString());
				spacecommentsinfo.PostID = Convert.ToInt32(idatareader["postid"].ToString());
				spacecommentsinfo.Author = idatareader["author"].ToString();
				spacecommentsinfo.Email = idatareader["email"].ToString();
				spacecommentsinfo.Url = idatareader["url"].ToString();
				spacecommentsinfo.Ip = idatareader["ip"].ToString();
				spacecommentsinfo.PostDateTime = Convert.ToDateTime(idatareader["postdatetime"].ToString());
				spacecommentsinfo.Content = idatareader["content"].ToString();
				spacecommentsinfo.ParentID = Convert.ToInt32(idatareader["parentid"].ToString());
				spacecommentsinfo.Uid = Convert.ToInt32(idatareader["uid"].ToString());
				spacecommentsinfo.PostTitle = idatareader["posttitle"].ToString();

				idatareader.Close();
				return spacecommentsinfo;
			}
			else
			{
                idatareader.Close();
				return null;
			}
		}

        /// <summary>
        /// 获取评论对象数组
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
		public static SpaceCommentInfo[] GetSpaceCommentInfo (DataTable dt)
		{
			if(dt == null || dt.Rows.Count == 0)
				return null;

			SpaceCommentInfo[] spacecommentsinfoarray = new SpaceCommentInfo[dt.Rows.Count];
			for(int i = 0 ; i < dt.Rows.Count ; i++)
			{
				spacecommentsinfoarray[i] = new SpaceCommentInfo();
				spacecommentsinfoarray[i].CommentID = Convert.ToInt32(dt.Rows[i]["commentid"].ToString());
				spacecommentsinfoarray[i].PostID = Convert.ToInt32(dt.Rows[i]["postid"].ToString());
				spacecommentsinfoarray[i].Author = dt.Rows[i]["author"].ToString();
				spacecommentsinfoarray[i].Email = dt.Rows[i]["email"].ToString();
				spacecommentsinfoarray[i].Url = dt.Rows[i]["url"].ToString();
				spacecommentsinfoarray[i].Ip = dt.Rows[i]["ip"].ToString();
				spacecommentsinfoarray[i].PostDateTime = Convert.ToDateTime(dt.Rows[i]["postdatetime"].ToString());
				spacecommentsinfoarray[i].Content = dt.Rows[i]["content"].ToString();
				spacecommentsinfoarray[i].ParentID = Convert.ToInt32(dt.Rows[i]["parentid"].ToString());
				spacecommentsinfoarray[i].Uid = Convert.ToInt32(dt.Rows[i]["uid"].ToString());
				spacecommentsinfoarray[i].PostTitle = dt.Rows[i]["posttitle"].ToString();
			}

			dt.Dispose();
			return spacecommentsinfoarray;
		}
		#endregion

		#region Space 日志操作

        /// <summary>
        /// 获取空间日志
        /// </summary>
        /// <param name="__idatareader"></param>
        /// <returns></returns>
		public static SpacePostInfo GetSpacepostsInfo(IDataReader idatareader)
		{
			if(idatareader == null)
				return null;

			if(idatareader.Read())
			{

				SpacePostInfo spacepostsinfo = new SpacePostInfo();
				spacepostsinfo.Postid = Convert.ToInt32(idatareader["postid"].ToString());
				spacepostsinfo.Author = idatareader["author"].ToString();
				spacepostsinfo.Uid = Convert.ToInt32(idatareader["uid"].ToString());
				spacepostsinfo.Postdatetime = Convert.ToDateTime(idatareader["postdatetime"].ToString());
				spacepostsinfo.Content = idatareader["content"].ToString();
				spacepostsinfo.Title = idatareader["title"].ToString();
				spacepostsinfo.Category = idatareader["category"].ToString();
				spacepostsinfo.PostStatus = Convert.ToInt16(idatareader["poststatus"].ToString());
				spacepostsinfo.CommentStatus = Convert.ToInt16(idatareader["commentstatus"].ToString());
				spacepostsinfo.PostUpDateTime = Convert.ToDateTime(idatareader["postupdatetime"].ToString());
				spacepostsinfo.Commentcount = Convert.ToInt32(idatareader["commentcount"].ToString());
				spacepostsinfo.Views = Convert.ToInt32(idatareader["views"].ToString());

				idatareader.Close();
				return spacepostsinfo;
			}
			else
			{
                idatareader.Close();
				return null;
			}
		}

        /// <summary>
        /// 获取空间日志
        /// </summary>
        /// <param name="spacepostid"></param>
        /// <returns></returns>
        public static SpacePostInfo GetSpacepostsInfo(int spacepostid)
        {
            return GetSpacepostsInfo(Space.Data.DbProvider.GetInstance().GetSpacePost(spacepostid));
        }

        /// <summary>
        /// 获取日志数组
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
		public static SpacePostInfo[] GetSpacepostsInfo (DataTable dt)
		{
			if(dt == null || dt.Rows.Count == 0)
				return null;

			SpacePostInfo[] spacepostsinfoarray = new SpacePostInfo[dt.Rows.Count];
			for(int i = 0 ; i < dt.Rows.Count ; i++)
			{
				spacepostsinfoarray[i] = new SpacePostInfo();
				spacepostsinfoarray[i].Postid = Convert.ToInt32(dt.Rows[i]["postid"].ToString());
				spacepostsinfoarray[i].Author = dt.Rows[i]["author"].ToString();
				spacepostsinfoarray[i].Uid = Convert.ToInt32(dt.Rows[i]["uid"].ToString());
				spacepostsinfoarray[i].Postdatetime = Convert.ToDateTime(dt.Rows[i]["postdatetime"].ToString());
				spacepostsinfoarray[i].Content = dt.Rows[i]["content"].ToString();
				spacepostsinfoarray[i].Title = dt.Rows[i]["title"].ToString();
				spacepostsinfoarray[i].Category = dt.Rows[i]["category"].ToString();
				spacepostsinfoarray[i].PostStatus = Convert.ToInt32(dt.Rows[i]["poststatus"].ToString());
				spacepostsinfoarray[i].CommentStatus = Convert.ToInt32(dt.Rows[i]["commentstatus"].ToString());
				spacepostsinfoarray[i].PostUpDateTime = Convert.ToDateTime(dt.Rows[i]["postupdatetime"].ToString());
				spacepostsinfoarray[i].Commentcount = Convert.ToInt32(dt.Rows[i]["commentcount"].ToString());
				spacepostsinfoarray[i].Views = Convert.ToInt32(dt.Rows[i]["views"].ToString());
		

			}

			dt.Dispose();
			return spacepostsinfoarray;
		}
		#endregion

		#region Space 日志类型操作
        /// <summary>
        /// 获取日志分类
        /// </summary>
        /// <param name="__idatareader"></param>
        /// <returns></returns>
		public static SpaceCategoryInfo GetSpaceCategoryInfo (IDataReader idatareader)
		{
			if(idatareader == null)
				return null;

			if(idatareader.Read())
			{
		
				SpaceCategoryInfo spacecategoriesinfo = new SpaceCategoryInfo();
				spacecategoriesinfo.CategoryID = Convert.ToInt32(idatareader["categoryid"].ToString());
				spacecategoriesinfo.Title = idatareader["title"].ToString();
				spacecategoriesinfo.Uid = Convert.ToInt32(idatareader["uid"].ToString());
				spacecategoriesinfo.Description = idatareader["description"].ToString();
				spacecategoriesinfo.TypeID = Convert.ToInt32(idatareader["typeid"].ToString());
				spacecategoriesinfo.CategoryCount = Convert.ToInt32(idatareader["categorycount"].ToString());
				spacecategoriesinfo.Displayorder = Convert.ToInt32(idatareader["displayorder"].ToString());

				idatareader.Close();
				return spacecategoriesinfo;
			}
			else
			{
                idatareader.Close();
				return null;
			}
		}
	
        /// <summary>
        /// 获取日志分类
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
		public static SpaceCategoryInfo[] GetSpaceCategories (DataTable dt)
		{

			if(dt == null || dt.Rows.Count == 0)
				return null;

			SpaceCategoryInfo[] spacecategoriesinfoarray = new SpaceCategoryInfo[dt.Rows.Count];
			for(int i = 0 ; i < dt.Rows.Count ; i++)
			{
				spacecategoriesinfoarray[i] = new SpaceCategoryInfo();
				spacecategoriesinfoarray[i].CategoryID = Convert.ToInt32(dt.Rows[i]["categoryid"].ToString());
				spacecategoriesinfoarray[i].Title = dt.Rows[i]["title"].ToString();
				spacecategoriesinfoarray[i].Uid = Convert.ToInt32(dt.Rows[i]["uid"].ToString());
				spacecategoriesinfoarray[i].Description = dt.Rows[i]["description"].ToString();
				spacecategoriesinfoarray[i].TypeID = Convert.ToInt32(dt.Rows[i]["typeid"].ToString());
				spacecategoriesinfoarray[i].CategoryCount = Convert.ToInt32(dt.Rows[i]["categorycount"].ToString());
				spacecategoriesinfoarray[i].Displayorder = Convert.ToInt32(dt.Rows[i]["displayorder"].ToString());
			}

			dt.Dispose();
			return spacecategoriesinfoarray;
		}
		#endregion
		
		#region Space 日志关联类型操作
		public static SpacePostCategoryInfo GetSpacePostCategoryInfo (IDataReader idatareader)
		{
			if(idatareader == null)
				return null;

			if(idatareader.Read())
			{

				SpacePostCategoryInfo spacepostcategoriesinfo = new SpacePostCategoryInfo();
				spacepostcategoriesinfo.ID = Convert.ToInt32(idatareader["id"].ToString());
				spacepostcategoriesinfo.PostID = Convert.ToInt32(idatareader["postid"].ToString());
				spacepostcategoriesinfo.CategoryID = Convert.ToInt32(idatareader["categoryid"].ToString());

                idatareader.Close();
				return spacepostcategoriesinfo;
			}
			else
			{
                idatareader.Close();
				return null;
			}
		}
		public static SpacePostCategoryInfo[] GetSpacePostCategories (DataTable dt)
		{
			if(dt == null || dt.Rows.Count == 0)
				return null;

			SpacePostCategoryInfo[] spacepostcategoriesinfoarray = new SpacePostCategoryInfo[dt.Rows.Count];
			for(int i = 0 ; i < dt.Rows.Count ; i++)
			{
				spacepostcategoriesinfoarray[i].ID = Convert.ToInt32(dt.Rows[i]["id"].ToString());
				spacepostcategoriesinfoarray[i].PostID = Convert.ToInt32(dt.Rows[i]["postid"].ToString());
				spacepostcategoriesinfoarray[i].CategoryID = Convert.ToInt32(dt.Rows[i]["categoryid"].ToString());

			}

			dt.Dispose();
			return spacepostcategoriesinfoarray;
		}
		#endregion
	
		#region Space 日志附件操作
		public static SpaceAttachmentInfo GetSpaceAttachmentInfo(IDataReader idatareader)
		{
			if(idatareader == null)
				return null;

			if(idatareader.Read())
			{

				SpaceAttachmentInfo spaceattachmentinfo = new SpaceAttachmentInfo();
				spaceattachmentinfo.AID = Convert.ToInt32(idatareader["aid"].ToString());
				spaceattachmentinfo.UID = Convert.ToInt32(idatareader["uid"].ToString());
				spaceattachmentinfo.SpacePostID = Convert.ToInt32(idatareader["spacepostid"].ToString());
				spaceattachmentinfo.PostDateTime = Convert.ToDateTime(idatareader["postdatetime"].ToString());
				spaceattachmentinfo.FileName = idatareader["filename"].ToString();
				spaceattachmentinfo.FileType = idatareader["filetype"].ToString();
				spaceattachmentinfo.FileSize = Convert.ToInt32(idatareader["filesize"].ToString());
				spaceattachmentinfo.Attachment = idatareader["attachment"].ToString();
				spaceattachmentinfo.Downloads = Convert.ToInt32(idatareader["downloads"].ToString());

                idatareader.Close();
				return spaceattachmentinfo;
			}
			else
			{
                idatareader.Close();
				return null;
			}
		}


		public static SpaceAttachmentInfo[] GetSpaceAttachmentInfo (DataTable dt)
		{
			if(dt == null || dt.Rows.Count == 0)
				return null;

			SpaceAttachmentInfo[] spaceattachmentinfoarray = new SpaceAttachmentInfo[dt.Rows.Count];
			for(int i = 0 ; i < dt.Rows.Count ; i++)
			{
				spaceattachmentinfoarray[i] = new SpaceAttachmentInfo();
				spaceattachmentinfoarray[i].AID = Convert.ToInt32(dt.Rows[i]["aid"].ToString());
				spaceattachmentinfoarray[i].UID = Convert.ToInt32(dt.Rows[i]["uid"].ToString());
				spaceattachmentinfoarray[i].SpacePostID = Convert.ToInt32(dt.Rows[i]["spacepostid"].ToString());
				spaceattachmentinfoarray[i].PostDateTime = Convert.ToDateTime(dt.Rows[i]["postdatetime"].ToString());
				spaceattachmentinfoarray[i].FileName = dt.Rows[i]["filename"].ToString();
				spaceattachmentinfoarray[i].FileType = dt.Rows[i]["filetype"].ToString();
				spaceattachmentinfoarray[i].FileSize = Convert.ToInt32(dt.Rows[i]["filesize"].ToString());
				spaceattachmentinfoarray[i].Attachment = dt.Rows[i]["attachment"].ToString();
				spaceattachmentinfoarray[i].Downloads = Convert.ToInt32(dt.Rows[i]["downloads"].ToString());

			}
			dt.Dispose();
			return spaceattachmentinfoarray;
		}
		#endregion

		#region 友情链接操作

		public static SpaceLinkInfo GetSpaceLinksInfo (IDataReader idatareader)
		{
			if(idatareader == null)
				return null;

			if(idatareader.Read())
			{
		
				SpaceLinkInfo spacelinksinfo = new SpaceLinkInfo();
				spacelinksinfo.LinkId = Convert.ToInt32(idatareader["linkid"].ToString());
				spacelinksinfo.UserId = Convert.ToInt32(idatareader["userid"].ToString());
				spacelinksinfo.LinkTitle = idatareader["linktitle"].ToString();
				spacelinksinfo.Description = idatareader["description"].ToString();
				spacelinksinfo.LinkUrl = idatareader["linkurl"].ToString();

                idatareader.Close();
				return spacelinksinfo;
			}
			else
			{
                idatareader.Close();
				return null;
			}
		}
		public static SpaceLinkInfo[] GetSpaceLinksInfo (DataTable dt)
		{

			if(dt == null || dt.Rows.Count == 0)
				return null;

			SpaceLinkInfo[] spacelinksinfoarray = new SpaceLinkInfo[dt.Rows.Count];
			for(int i = 0 ; i < dt.Rows.Count ; i++)
			{
				spacelinksinfoarray[i] = new SpaceLinkInfo();
				spacelinksinfoarray[i].LinkId = Convert.ToInt32(dt.Rows[i]["linkid"].ToString());
				spacelinksinfoarray[i].UserId = Convert.ToInt32(dt.Rows[i]["userid"].ToString());
				spacelinksinfoarray[i].LinkTitle = dt.Rows[i]["linktitle"].ToString();
				spacelinksinfoarray[i].Description = dt.Rows[i]["description"].ToString();
				spacelinksinfoarray[i].LinkUrl =dt.Rows[i]["linkurl"].ToString();
			}

			dt.Dispose();
			return spacelinksinfoarray;
		}

		#endregion
    }
}
 