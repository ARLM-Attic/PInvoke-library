using Discuz.Common.Generic;
using Discuz.Entity;
using Discuz.Forum;
using Discuz.Common;
using Discuz.Plugin.Space;
using Discuz.Plugin.Album;
using Discuz.Plugin.Mall;

namespace Discuz.Web
{
    /// <summary>
    /// 标签列表页
    /// </summary>
    public class tags : PageBase
    {
        #region 页面变量

        /// <summary>
        /// 使用了指定标签的主题列表
        /// </summary>
        public List<MyTopicInfo> topiclist;
        /// <summary>
        /// 使用了指定标签的空间日志列表
        /// </summary>
        public List<SpacePostInfo> spacepostlist;
        /// <summary>
        /// 使用了指定标签的图片列表
        /// </summary>
        public List<PhotoInfo> photolist;
        /// <summary>
        /// 访问所请求的TagId
        /// </summary>
        public int tagid;
        /// <summary>
        /// Tag的详细信息
        /// </summary>
        public TagInfo tag;
        /// <summary>
        /// 页码
        /// </summary>
        public int pageid = 1;
        /// <summary>
        /// 主题数
        /// </summary>
        public int topiccount = 0;
        /// <summary>
        /// 日志数
        /// </summary>
        public int spacepostcount = 0;
        /// <summary>
        /// 图片数
        /// </summary>
        public int photocount = 0;
        /// <summary>
        /// 页数
        /// </summary>
        public int pagecount = 1;
        /// <summary>
        /// 分页页码链接
        /// </summary>
        public string pagenumbers;
        /// <summary>
        /// 当前列表类型,topic=主题列表,spacepost=日志列表,photo=图片列表
        /// </summary>
        public string listtype;
        /// <summary>
        /// 商品列表
        /// </summary>
        public GoodsinfoCollection goodslist;
        /// <summary>
        /// 商品数
        /// </summary>
        public int goodscount;
        /// <summary>
        /// 标签数组
        /// </summary>
        public TagInfo[] taglist;
        #endregion

        protected override void ShowPage()
        {
            
            if (config.Enabletag != 1)
            {
                AddErrLine("没有启用Tag功能");
                return;
            }

            tagid = DNTRequest.GetInt("tagid", 0);

            if (tagid > 0)
            {
                tag = Tags.GetTagInfo(tagid);
                if (tag == null)
                {
                    AddErrLine("指定的标签不存在");
                    return;
                }

                if (tag.Orderid < 0)
                {
                    AddErrLine("指定的标签已被关闭");
                }

                if (IsErr())
                {
                    return;
                }

                listtype = DNTRequest.GetString("t");

                pageid = DNTRequest.GetInt("page", 1);
                if (pageid < 1)
                {
                    pageid = 1;
                }
                pagetitle = tag.Tagname;
                if (listtype.Equals(""))
                {
                    listtype = "topic";
                
                }
                switch (listtype)
                {

                    case "spacepost":
                        SpacePluginBase spb = SpacePluginProvider.GetInstance();
                        if (spb == null)
                        {
                            AddErrLine("未安装空间插件");
                            return;
                        }
                        spacepostcount = spb.GetSpacePostCountWithSameTag(tagid);
                        pagecount = spacepostcount % config.Tpp == 0 ? spacepostcount / config.Tpp : spacepostcount / config.Tpp + 1;
                        if (pagecount == 0)
                        {
                            pagecount = 1;
                        }
                        if (pageid > pagecount)
                        {
                            pageid = pagecount;
                        }
                        if (spacepostcount > 0)
                        {
                            spacepostlist = spb.GetSpacePostsWithSameTag(tagid, pageid, config.Tpp);
                            pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "tags.aspx?t=spacepost&tagid=" + tagid, 8);
                        }
                        else
                        {
                            AddMsgLine("该标签下暂无日志");
                        }
                        break;
                    case "photo":
                        AlbumPluginBase apb = AlbumPluginProvider.GetInstance();
                        if (apb == null)
                        {
                            AddErrLine("未安装相册插件");
                            return;
                        }
                        photocount = apb.GetPhotoCountWithSameTag(tagid);

                        pagecount = photocount % config.Tpp == 0 ? photocount / config.Tpp : photocount / config.Tpp + 1;
                        if (pagecount == 0)
                        {
                            pagecount = 1;
                        }
                        if (pageid > pagecount)
                        {
                            pageid = pagecount;
                        }
                        if (photocount > 0)
                        {
                            photolist = apb.GetPhotosWithSameTag(tagid, pageid, config.Tpp);

                            foreach (PhotoInfo photo in photolist)
                            {
                                //当是远程照片时
                                if (photo.Filename.IndexOf("http") < 0)
                                {
                                    photo.Filename = forumpath + AlbumPluginProvider.GetInstance().GetThumbnailImage(photo.Filename);
                                }
                                else
                                {
                                    photo.Filename = AlbumPluginProvider.GetInstance().GetThumbnailImage(photo.Filename);
                                }

                            }

                            pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "tags.aspx?t=photo&tagid=" + tagid, 8);
                        }
                        else
                        {
                            AddMsgLine("该标签下暂无图片");
                        }
                        break;
                    case "mall":
                        if (MallPluginProvider.GetInstance() == null)
                        {
                            AddErrLine("未安装商城插件");
                            break;
                        }
                        goodscount = MallPluginProvider.GetInstance().GetGoodsCountWithSameTag(tagid);

                        pagecount = goodscount % config.Tpp == 0 ? goodscount / config.Tpp : goodscount / config.Tpp + 1;
                        if (pagecount == 0)
                        {
                            pagecount = 1;
                        }
                        if (pageid > pagecount)
                        {
                            pageid = pagecount;
                        }
                        if (goodscount > 0)
                        {
                            goodslist = MallPluginProvider.GetInstance().GetGoodsWithSameTag(tagid, pageid, config.Tpp);

                            //foreach (GoodsinfoPhotoInfo photo in goodslist)
                            //{
                            //    //当是远程照片时
                            //    if (photo.Filename.IndexOf("http") < 0)
                            //    {
                            //        photo.Filename = forumpath + Globals.GetThumbnailImage(photo.Filename);
                            //    }
                            //    else
                            //    {
                            //        photo.Filename = Globals.GetThumbnailImage(photo.Filename);
                            //    }

                            //}

                            pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "tags.aspx?t=mall&tagid=" + tagid, 8);
                        }
                        else
                        {
                            AddMsgLine("该标签下暂无商品");
                        }
                        break;

                    case "topic":
                        topiccount = Topics.GetTopicsCountWithSameTag(tagid);
                        pagecount = topiccount % config.Tpp == 0 ? topiccount / config.Tpp : topiccount / config.Tpp + 1;

                        if (pagecount == 0)
                        {
                            pagecount = 1;
                        }
                        if (pageid > pagecount)
                        {
                            pageid = pagecount;
                        }

                        if (topiccount > 0)
                        {
                            topiclist = Topics.GetTopicsWithSameTag(tagid, pageid, config.Tpp);
                            pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "tags.aspx?t=topic&tagid=" + tagid, 8);
                        }
                        else
                        {
                            AddMsgLine("该标签下暂无主题");
                        }
                        break;


                }
            }
            else
            {
                pagetitle = "标签";

                taglist = ForumTags.GetCachedHotForumTags(100);

            }
        }
        
    }
}
