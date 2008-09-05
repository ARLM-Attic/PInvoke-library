using System;
using System.Data;
using System.Data.Common;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Entity;
using Discuz.Plugin.Space;
using Discuz.Plugin.Album;


namespace Discuz.Web.UI
{
	/// <summary>
	/// RSS“≥√Ê¿‡
	/// </summary>
	public class RssPage : PageBase
	{
		public RssPage()
		{
			System.Web.HttpContext.Current.Response.ContentType = "application/xml";
			if (config.Rssstatus == 1)
			{

                if (DNTRequest.GetString("type") == "space" && config.Enablespace == 1)
                {
                    SpacePluginBase spb = SpacePluginProvider.GetInstance();

                    int uid = DNTRequest.GetInt("uid", -1);
                    if (uid == -1)
                    {
                        System.Web.HttpContext.Current.Response.Write(spb.GetFeed(config.Rssttl));
                        System.Web.HttpContext.Current.Response.End();
                        return;
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Response.Write(spb.GetFeed(config.Rssttl, uid));
                        System.Web.HttpContext.Current.Response.End();
                        return;
                    }

                }

                if (DNTRequest.GetString("type") == "photo" && config.Enablealbum == 1)
                {
                    AlbumPluginBase apb = AlbumPluginProvider.GetInstance();

                    int uid = DNTRequest.GetInt("uid", -1);
                    if (uid == -1)
                    {
                        System.Web.HttpContext.Current.Response.Write(apb.GetFeed(config.Rssttl));
                        System.Web.HttpContext.Current.Response.End();
                        return;
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Response.Write(apb.GetFeed(config.Rssttl, uid));
                        System.Web.HttpContext.Current.Response.End();
                        return;
                    }

                }
		    

			
				int forumid = DNTRequest.GetInt("forumid", -1);
				if (forumid == -1)
				{
					System.Web.HttpContext.Current.Response.Write(Feeds.GetRssXml(config.Rssttl));
					System.Web.HttpContext.Current.Response.End();
					return;
				}
				else
				{
					ForumInfo forum = Forums.GetForumInfo(forumid);
					if (forum != null)
					{
						if (forum.Allowrss == 1)
						{
							System.Web.HttpContext.Current.Response.Write(Feeds.GetForumRssXml(config.Rssttl, forumid));
							System.Web.HttpContext.Current.Response.End();
							return;
						}
					}
				}
			}
			System.Web.HttpContext.Current.Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n");
			System.Web.HttpContext.Current.Response.Write("<Rss>Error</Rss>\r\n");
			System.Web.HttpContext.Current.Response.End();

		}
	}
}
