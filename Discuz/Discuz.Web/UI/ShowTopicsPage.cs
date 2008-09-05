using System.Data;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Entity;
using System.Text;
using Discuz.Aggregation;
#if NET1
#else
using Discuz.Common.Generic;
using Discuz.Plugin.Space;
using Discuz.Plugin.Album;
#endif


namespace Discuz.Web.UI
{
	/// <summary>
	/// RSS页面类
	/// </summary>
	public class ShowTopicsPage : PageBase
	{
        SpacePluginBase spb = SpacePluginProvider.GetInstance();
        AlbumPluginBase apb = AlbumPluginProvider.GetInstance();

        int length = DNTRequest.GetQueryInt("length", -1);
        int count = DNTRequest.GetQueryInt("count", 10);
        int cachetime = DNTRequest.GetQueryInt("cachetime", 20);
        string rooturl = "http://" + DNTRequest.GetCurrentFullHost() + Discuz.Config.BaseConfigs.GetForumPath;
        string spacerooturl = "http://" + DNTRequest.GetCurrentFullHost() + Discuz.Config.BaseConfigs.GetForumPath + "space/";

        protected void OutPutUpdatedSpaces(string template, string alternatingTemplate)
	    {
            if (spb == null)
            {
                Response.Write("document.write('未安装空间插件');");
                return;
            }

            StringBuilder result = new StringBuilder();

            DataTable dt = Focuses.GetUpdatedSpaces(count, cachetime);
            int i = 0;
            string title = "";
            foreach (DataRow dr in dt.Rows)
	        {
	            title = dr["spacetitle"].ToString().Trim();
	            result.AppendFormat((i%2 == 0 ? template : alternatingTemplate), string.Empty, (length == -1 ? title : Utils.GetSubString(title, length, "")), string.Empty, string.Empty, title, string.Empty, spacerooturl + "?uid=" + dr["userid"].ToString());
 	    	    i++;
            }
            Response.Write("document.write('" + result.ToString().Replace("'", "\\'") + "');");
        }

        protected void OutPutRecommendedSpaces(string template, string alternatingTemplate)
        {
            if (spb == null)
            {
                Response.Write("document.write('未安装空间插件');");
                return;
            }


            SpaceConfigInfoExt[] spaces = AggregationFacade.SpaceAggregation.GetSpaceListFromFile("Website");
            StringBuilder result = new StringBuilder();
            int i = 0;
            foreach (SpaceConfigInfoExt space in spaces)
            {
                if (i >= count)
                    break;
                result.AppendFormat((i % 2 == 0 ? template : alternatingTemplate), string.Empty, (length == -1 ? space.Spacetitle : Utils.GetSubString(space.Spacetitle, length, "")), string.Empty, string.Empty, space.Spacetitle, string.Empty, spacerooturl + "?uid=" + space.Userid.ToString());
                i++;
            }
            Response.Write("document.write('" + result.ToString().Replace("'", "\\'") + "');");
        }

        protected void OutPutNewSpacePosts(string template, string alternatingTemplate)
        {
            if (spb == null)
            {
                Response.Write("document.write('未安装空间插件');");
                return;
            }

            StringBuilder result = new StringBuilder();

            DataTable dt = Focuses.GetNewSpacePosts(count, cachetime);
            int i = 0;
            string title = "";
            foreach (DataRow dr in dt.Rows)
            {
                title = dr["title"].ToString().Trim();
                result.AppendFormat((i % 2 == 0 ? template : alternatingTemplate), string.Empty, (length == -1 ? title : Utils.GetSubString(title, length, "")), string.Empty, string.Empty, title, string.Empty, spacerooturl + "?uid=" + dr["uid"].ToString(), spacerooturl + "viewspacepost.aspx?postid=" + dr["postid"].ToString());
                i++;
            }
            Response.Write("document.write('" + result.ToString().Replace("'", "\\'") + "');");
        }

        protected void OutPutRecommendedSpacePosts(string template, string alternatingTemplate)
        {
            if (spb == null)
            {
                Response.Write("document.write('未安装空间插件');");
                return;
            }

            StringBuilder result = new StringBuilder();

            SpaceShortPostInfo[] posts = AggregationFacade.SpaceAggregation.GetSpacePostList("Website");
            int i = 0;
            string title = "";
            foreach (SpaceShortPostInfo post in posts)
            {
                if (i > count)
                    break;
                title = post.Title;
                result.AppendFormat((i % 2 == 0 ? template : alternatingTemplate), string.Empty, (length == -1 ? title : Utils.GetSubString(title, length, "")), string.Empty, string.Empty, title, string.Empty, spacerooturl + "?uid=" + post.Uid.ToString(), spacerooturl + "viewspacepost.aspx?postid=" + post.Postid.ToString());
                i++;
            }
            Response.Write("document.write('" + result.ToString().Replace("'", "\\'") + "');");
        }
        
        protected void OutPutRecommendedAlbum(string template, string alternatingTemplate)
        {
            if (apb == null)
            {
                Response.Write("document.write('未安装相册插件');");
                return;
            }

            StringBuilder result = new StringBuilder();
#if NET1
            AlbumInfoCollection albums = AggregationFacade.AlbumAggregation.GetRecommandAlbumList("Website");
#else
            List<AlbumInfo> albums = AggregationFacade.AlbumAggregation.GetRecommandAlbumList("Website");
#endif
            int i = 0;
            string title = "";
            foreach (AlbumInfo album in albums)
            {
                if (i > count)
                    break;
                title = album.Title;
                result.AppendFormat((i % 2 == 0 ? template : alternatingTemplate), string.Empty, (length == -1 ? title : Utils.GetSubString(title, length, "")), string.Empty, string.Empty, title, string.Empty, spacerooturl + "?uid=" + album.Userid.ToString(), string.Empty, rooturl +  "showalbum.aspx?albumid=" + album.Albumid.ToString());
                i++;
            }
            Response.Write("document.write('" + result.ToString().Replace("'", "\\'") + "');");
        }



	}
}
