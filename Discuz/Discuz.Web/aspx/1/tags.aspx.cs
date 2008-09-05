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
    /// ��ǩ�б�ҳ
    /// </summary>
    public class tags : PageBase
    {
        #region ҳ�����

        /// <summary>
        /// ʹ����ָ����ǩ�������б�
        /// </summary>
        public List<MyTopicInfo> topiclist;
        /// <summary>
        /// ʹ����ָ����ǩ�Ŀռ���־�б�
        /// </summary>
        public List<SpacePostInfo> spacepostlist;
        /// <summary>
        /// ʹ����ָ����ǩ��ͼƬ�б�
        /// </summary>
        public List<PhotoInfo> photolist;
        /// <summary>
        /// �����������TagId
        /// </summary>
        public int tagid;
        /// <summary>
        /// Tag����ϸ��Ϣ
        /// </summary>
        public TagInfo tag;
        /// <summary>
        /// ҳ��
        /// </summary>
        public int pageid = 1;
        /// <summary>
        /// ������
        /// </summary>
        public int topiccount = 0;
        /// <summary>
        /// ��־��
        /// </summary>
        public int spacepostcount = 0;
        /// <summary>
        /// ͼƬ��
        /// </summary>
        public int photocount = 0;
        /// <summary>
        /// ҳ��
        /// </summary>
        public int pagecount = 1;
        /// <summary>
        /// ��ҳҳ������
        /// </summary>
        public string pagenumbers;
        /// <summary>
        /// ��ǰ�б�����,topic=�����б�,spacepost=��־�б�,photo=ͼƬ�б�
        /// </summary>
        public string listtype;
        /// <summary>
        /// ��Ʒ�б�
        /// </summary>
        public GoodsinfoCollection goodslist;
        /// <summary>
        /// ��Ʒ��
        /// </summary>
        public int goodscount;
        /// <summary>
        /// ��ǩ����
        /// </summary>
        public TagInfo[] taglist;
        #endregion

        protected override void ShowPage()
        {
            
            if (config.Enabletag != 1)
            {
                AddErrLine("û������Tag����");
                return;
            }

            tagid = DNTRequest.GetInt("tagid", 0);

            if (tagid > 0)
            {
                tag = Tags.GetTagInfo(tagid);
                if (tag == null)
                {
                    AddErrLine("ָ���ı�ǩ������");
                    return;
                }

                if (tag.Orderid < 0)
                {
                    AddErrLine("ָ���ı�ǩ�ѱ��ر�");
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
                            AddErrLine("δ��װ�ռ���");
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
                            AddMsgLine("�ñ�ǩ��������־");
                        }
                        break;
                    case "photo":
                        AlbumPluginBase apb = AlbumPluginProvider.GetInstance();
                        if (apb == null)
                        {
                            AddErrLine("δ��װ�����");
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
                                //����Զ����Ƭʱ
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
                            AddMsgLine("�ñ�ǩ������ͼƬ");
                        }
                        break;
                    case "mall":
                        if (MallPluginProvider.GetInstance() == null)
                        {
                            AddErrLine("δ��װ�̳ǲ��");
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
                            //    //����Զ����Ƭʱ
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
                            AddMsgLine("�ñ�ǩ��������Ʒ");
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
                            AddMsgLine("�ñ�ǩ����������");
                        }
                        break;


                }
            }
            else
            {
                pagetitle = "��ǩ";

                taglist = ForumTags.GetCachedHotForumTags(100);

            }
        }
        
    }
}
