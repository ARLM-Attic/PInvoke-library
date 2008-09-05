using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using Discuz.Common;
using Discuz.Entity;
using Discuz.Config;
using Discuz.Mall.Data;
using Discuz.Forum;
using Discuz.Plugin.Preview;

namespace Discuz.Mall
{
    /// <summary>
    /// 商品管理操作类
    /// </summary>
    public class Goods
    {
        #region 正则表达式静态变量声明

        private static Regex regexAttach = new Regex(@"\[attach\](\d+?)\[\/attach\]", RegexOptions.IgnoreCase);

        private static Regex regexHide = new Regex(@"\s*\[hide\][\n\r]*([\s\S]+?)[\n\r]*\[\/hide\]\s*", RegexOptions.IgnoreCase);

        private static Regex regexAttachImg = new Regex(@"\[attachimg\](\d+?)\[\/attachimg\]", RegexOptions.IgnoreCase);

        #endregion

        /// <summary>
        /// 商品信息字段(message)转换
        /// </summary>
        /// <param name="goodspramsinfo">要转换的信息和相关参数设置</param>
        /// <param name="attcoll">当前商品所包括的附件集合</param>
        /// <returns>返回转换后的信息</returns>
        public static string MessgeTranfer(GoodspramsInfo goodspramsinfo, GoodsattachmentinfoCollection attcoll)
        {
            goodspramsinfo.Hide = 0;
            //先简单判断是否是动网兼容模式
            if (!goodspramsinfo.Ubbmode)
            {
                goodspramsinfo.Sdetail = UBB.UBBToHTML((PostpramsInfo)goodspramsinfo);
            }
            else
            {
                goodspramsinfo.Sdetail = Utils.HtmlEncode(goodspramsinfo.Sdetail);
            }

            string message = goodspramsinfo.Sdetail;
            if (GoodsAttachments.GetGoodsAttachmentsByGoodsid(goodspramsinfo.Goodsid).Count > 0 || regexAttach.IsMatch(message) || regexAttachImg.IsMatch(message))
            {
                //获取在[hide]标签中的附件id
                string[] attHidArray = GetHiddenAttachIdList(goodspramsinfo.Sdetail, goodspramsinfo.Hide);

                for (int i = 0; i < attcoll.Count; i++)
                {
                    message = GetMessageWithAttachInfo(goodspramsinfo, 1, attHidArray, attcoll[i], message);
                    if (Utils.InArray(attcoll[i].Aid.ToString(), attHidArray))
                    {
                        attcoll.RemoveAt(i);
                    }
                }
                goodspramsinfo.Sdetail = message;
            }

            return goodspramsinfo.Sdetail;
        }

        /// <summary>
        /// 获取被包含在[hide]标签内的附件id
        /// </summary>
        /// <param name="content">帖子内容</param>
        /// <param name="hide">隐藏标记</param>
        /// <returns>隐藏的附件id数组</returns>
        private static string[] GetHiddenAttachIdList(string content, int hide)
        {
            if (hide == 0)
            {
                return new string[0];
            }

            StringBuilder tmpStr = new StringBuilder();
            StringBuilder hidContent = new StringBuilder();
            foreach (Match m in regexHide.Matches(content))
            {
                if (hide == 1)
                {
                    hidContent.Append(m.Groups[0].ToString());
                }
            }


            foreach (Match ma in regexAttach.Matches(hidContent.ToString()))
            {
                tmpStr.Append(ma.Groups[1].ToString());
                tmpStr.Append(",");
            }

            foreach (Match ma in regexAttachImg.Matches(hidContent.ToString()))
            {
                tmpStr.Append(ma.Groups[1].ToString());
                tmpStr.Append(",");
            }

            if (tmpStr.Length == 0)
            {
                return new string[0];
            }

            return tmpStr.Remove(tmpStr.Length - 1, 1).ToString().Split(',');
        }

        /// <summary>
        /// 获取加载附件信息的商品内容
        /// </summary>
        /// <param name="goodspramsinfo">参数列表</param>
        /// <param name="allowGetAttach">是否允许获取附件</param>
        /// <param name="attHidArray">隐藏在hide标签中的附件数组</param>
        /// <param name="attinfo">附件信息</param>
        /// <param name="message">内容信息</param>
        /// <returns>商品内容信息</returns>
        private static string GetMessageWithAttachInfo(GoodspramsInfo goodspramsinfo, int allowGetAttach, string[] attHidArray, Goodsattachmentinfo attinfo, string message)
        {
            string forumPath = BaseConfigs.GetBaseConfig().Forumpath;
            string filesize;
            string replacement;
            if (Utils.InArray(attinfo.Aid.ToString(), attHidArray))
                return message;
        
            attinfo.Filename = attinfo.Filename.ToString().Replace("\\", "/");

            if (message.IndexOf("[attach]" + attinfo.Aid.ToString() + "[/attach]") != -1 || message.IndexOf("[attachimg]" + attinfo.Aid.ToString() + "[/attachimg]") != -1)
            {

                if (attinfo.Filesize > 1024)
                {
                    filesize = Convert.ToString(Math.Round(Convert.ToDecimal(attinfo.Filesize) / 1024, 2)) + " K";
                }
                else
                {
                    filesize = attinfo.Filesize + " B";
                }

                if (Utils.IsImgFilename(attinfo.Attachment))
                {
                    if (attinfo.Filename.ToLower().IndexOf("http") == 0)
                    {
                        replacement = "<span style=\"position: absolute; display: none;\" onmouseover=\"showMenu(this.id, 0, 1)\" id=\"attach_" + attinfo.Aid + "\"><img border=\"0\" src=\"" + forumPath + "images/attachicons/attachimg.gif\" /></span><img src=\"" + attinfo.Filename + "\" onload=\"attachimg(this, 'load');\" onmouseover=\"attachimginfo(this, 'attach_" + attinfo.Aid + "', 1);attachimg(this, 'mouseover')\" onclick=\"zoom(this);\" onmouseout=\"attachimginfo(this, 'attach_" + attinfo.Aid + "', 0, event)\" /><div id=\"attach_" + attinfo.Aid + "_menu\" style=\"display: none;\" class=\"t_attach\"><img border=\"0\" alt=\"\" class=\"absmiddle\" src=\"" + forumPath + "images/attachicons/image.gif\" /><a target=\"_blank\" href=\"" + attinfo.Filename + "\"><strong>" + attinfo.Attachment + "</strong></a>(" + filesize + ")<br/><div class=\"t_smallfont\">" + attinfo.Postdatetime + "</div></div>";
                    }
                    else
                    {
                        replacement = "<span style=\"position: absolute; display: none;\" onmouseover=\"showMenu(this.id, 0, 1)\" id=\"attach_" + attinfo.Aid + "\"><img border=\"0\" src=\"" + forumPath + "images/attachicons/attachimg.gif\" /></span><img src=\"" + forumPath + "upload/" + attinfo.Filename + "\" onload=\"attachimg(this, 'load');\" onmouseover=\"attachimginfo(this, 'attach_" + attinfo.Aid + "', 1);attachimg(this, 'mouseover')\" onclick=\"zoom(this);\" onmouseout=\"attachimginfo(this, 'attach_" + attinfo.Aid + "', 0, event)\" /><div id=\"attach_" + attinfo.Aid + "_menu\" style=\"display: none;\" class=\"t_attach\"><img border=\"0\" alt=\"\" class=\"absmiddle\" src=\"" + forumPath + "images/attachicons/image.gif\" /><a target=\"_blank\" href=\"" + forumPath + "upload/" + attinfo.Filename + "\"><strong>" + attinfo.Attachment + "</strong></a>(" + filesize + ")<br/><div class=\"t_smallfont\">" + attinfo.Postdatetime + "</div></div>";
                    }
                }
                else
                {
                    if (attinfo.Filename.ToLower().IndexOf("http") == 0)
                    {
                        replacement = string.Format("<p><img alt=\"\" src=\"{0}images/attachicons/attachment.gif\" border=\"0\" /><span class=\"bold\">附件</span>: <a href=\""+ attinfo.Filename +"\" target=\"_blank\">{2}</a> ({3}, {4})", forumPath, attinfo.Aid.ToString(), attinfo.Attachment.ToString().Trim(), attinfo.Postdatetime, filesize);
                    }
                    else
                    {
                        replacement = string.Format("<p><img alt=\"\" src=\"{0}images/attachicons/attachment.gif\" border=\"0\" /><span class=\"bold\">附件</span>: <a href=\"" + forumPath + "upload/" + attinfo.Filename + "\" target=\"_blank\">{2}</a> ({3}, {4})", forumPath, attinfo.Aid.ToString(), attinfo.Attachment.ToString().Trim(), attinfo.Postdatetime, filesize);
                    }
                }


                Regex r = new Regex(string.Format(@"\[attach\]{0}\[/attach\]|\[attachimg\]{0}\[/attachimg\]", attinfo.Aid));
                message = r.Replace(message, replacement, 1);

                message = message.Replace("[attach]" + attinfo.Aid.ToString() + "[/attach]", string.Empty);
                message = message.Replace("[attachimg]" + attinfo.Aid.ToString() + "[/attachimg]", string.Empty);

            }
            else
            {
                if (attinfo.Goodsid == goodspramsinfo.Goodsid)
                {
                    ;
                    //if (Utils.IsImgFilename(attinfo.Attachment))
                    //{
                    //    attinfo.Attachimgpost = 1;
                    //}
                    //else
                    //{
                    //    attinfo.Attachimgpost = 0;
                    //}

                    //加载文件预览类指定方法
                    //IPreview preview = PreviewProvider.GetInstance(Path.GetExtension(attinfo.Filename).Remove(0, 1).Trim());

                    //if (preview != null)
                    //{
                    //    //当支持FTP上传附件时
                    //    if (FTPs.GetMallAttachInfo.Allowupload == 1)
                    //    {
                    //        preview.UseFTP = true;
                    //        attinfo.Preview = preview.GetPreview(attinfo.Filename, attinfo);
                    //    }
                    //    else
                    //    {
                    //        preview.UseFTP = false;
                    //        attinfo.Preview = preview.GetPreview(Utils.GetMapPath(BaseConfigs.GetForumPath + @"upload/" + attinfo.Filename), attinfo);
                    //    }
                    //}
                }
            }
            return message;
        }

        /// <summary>
        /// 更新指定商品数据信息
        /// </summary>
        /// <param name="goodsinfo">商品信息</param>
        /// <returns></returns>
        public static void UpdateGoods(Goodsinfo goodsinfo)
        {
            DbProvider.GetInstance().UpdateGoods(goodsinfo); 
        }

        /// <summary>
        /// 更新指定商品数据信息
        /// </summary>
        /// <param name="goodsinfo">商品信息</param>
        /// <param name="oldgoodscategoryid">商品分类原值</param>
        /// <param name="oldparentcategorylist">商品父分类原值</param>
        public static void UpdateGoods(Goodsinfo goodsinfo, int oldgoodscategoryid, string oldparentcategorylist)
        {
            if (goodsinfo.Categoryid != oldgoodscategoryid && goodsinfo.Categoryid >0)
            {
                DbProvider.GetInstance().UpdateCategoryGoodsCounts(goodsinfo.Categoryid, goodsinfo.Parentcategorylist, 1);
                DbProvider.GetInstance().UpdateCategoryGoodsCounts(oldgoodscategoryid, oldparentcategorylist, -1);
            }

            DbProvider.GetInstance().UpdateGoods(goodsinfo);
        }

        /// <summary>
        /// 创建商品数据信息
        /// </summary>
        /// <param name="goodsinfo">商品信息</param>
        /// <returns>创建商品的id</returns>
        public static int CreateGoods(Goodsinfo goodsinfo)
        {
            int goodsid = DbProvider.GetInstance().CreateGoods(goodsinfo);

            //当成功创建商品信息且可在前台正常显示时
            if (goodsid > 0 && goodsinfo.Displayorder>=0) 
            {
                DbProvider.GetInstance().UpdateCategoryGoodsCounts(goodsinfo.Categoryid, goodsinfo.Parentcategorylist, 1);
            }
            return goodsid;
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="goodsid">商品Id</param>
        public static Goodsinfo GetGoodsInfo(int goodsid)
        {
            return DTO.GetGoodsInfo(DbProvider.GetInstance().GetGoodsInfo(goodsid)); 
        }

        /// <summary>
        /// 输出htmltitle
        /// </summary>
        /// <param name="htmltitle">html标题</param>
        /// <param name="goodsid">商品id</param>
        public static void WriteHtmlSubjectFile(string htmltitle, int goodsid)
        {
            StringBuilder dir = new StringBuilder();
            dir.Append(BaseConfigs.GetForumPath);
            dir.Append("cache/goods/magic/");

            if (!Directory.Exists(Utils.GetMapPath(dir.ToString())))
            {
                Utils.CreateDir(Utils.GetMapPath(dir.ToString()));
            }

            dir.Append((goodsid / 1000 + 1).ToString());
            dir.Append("/");

            if (!Directory.Exists(Utils.GetMapPath(dir.ToString())))
            {
                Utils.CreateDir(Utils.GetMapPath(dir.ToString()));
            }


            string filename = Utils.GetMapPath(dir.ToString() + goodsid + "_htmltitle.config");
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    Byte[] info = System.Text.Encoding.UTF8.GetBytes(Utils.RemoveUnsafeHtml(htmltitle));
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }

            }
            catch
            {
            }
        }

        /// <summary>
        /// 获取指定商品Id的商品列表
        /// </summary>
        /// <param name="goodsidlist">商品id列表</param>
        /// <returns>商品列表</returns>
        public static DataTable GetGoodsList(string goodsidlist)
        {
            return DbProvider.GetInstance().GetGoodsList(goodsidlist);
        }


        /// <summary>
        /// 指定用户id是否是商品id列表的卖家
        /// </summary>
        /// <param name="goodsidlist">商品id列表</param>
        /// <param name="userid">用户id</param>
        /// <returns>是否为卖家</returns>
        public static bool IsSeller(string goodsidlist, int userid)
        {
            bool isseller = true;
            foreach (DataRow dr in GetGoodsList(goodsidlist).Rows)
            {
                if (dr["selleruid"].ToString() != userid.ToString())
                {
                    isseller = false;
                }
            }
            return isseller;
        }


        /// <summary>
        /// 获取推荐商品列表
        /// </summary>
        /// <param name="selleruid">卖家id</param>
        /// <param name="pagesize">分页大小</param>
        /// <param name="pageindex">当前页面</param>
        /// <param name="condition">查询条件</param>
        /// <returns>推荐商品列表</returns>
        public static GoodsinfoCollection GetGoodsRecommendList(int selleruid, int pagesize, int pageindex, string condition)
        {
            condition += DbProvider.GetInstance().GetGoodsRecommendCondition((int)MallUtils.OperaCode.MorethanOrEqual, 1);
            condition += DbProvider.GetInstance().GetGoodsCloseCondition((int)MallUtils.OperaCode.Equal, 0);
            condition += DbProvider.GetInstance().GetGoodsExpirationCondition((int)MallUtils.OperaCode.LessthanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsDateLineCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsRemainCondition((int)MallUtils.OperaCode.Morethan, 0);
            condition += DbProvider.GetInstance().GetGoodsDisplayCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);

            return DTO.GetGoodsInfoList(DbProvider.GetInstance().GetGoodsListBySellerUID(selleruid, pagesize, pageindex, condition, "displayorder", 0));
        }

        /// <summary>
        /// 获取推荐商品列表
        /// </summary>
        /// <param name="selleruid">卖家id</param>
        /// <param name="pagesize">分页大小</param>
        /// <param name="pageindex">当前页面</param>
        /// <param name="condition">查询条件</param>
        /// <returns>推荐商品列表</returns>
        public static GoodsinfoCollection GetGoodsRecommendManageList(int selleruid, int pagesize, int pageindex, string condition)
        {
            condition += DbProvider.GetInstance().GetGoodsRecommendCondition((int)MallUtils.OperaCode.MorethanOrEqual, 1);
            return DTO.GetGoodsInfoList(DbProvider.GetInstance().GetGoodsListBySellerUID(selleruid, pagesize, pageindex, condition, "displayorder", 0));
        }

        /// <summary>
        /// 获取指定分类和条件下的商品列表集合
        /// </summary>
        /// <param name="categoryid">商品分类</param>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="ascdesc">排序方式(0:升序, 1:降序)</param>
        /// <returns>商品列表</returns>
        public static GoodsinfoCollection GetGoodsInfoList(int categoryid, int pagesize, int pageindex, string condition, string orderby, int ascdesc)
        {
            GoodsinfoCollection coll = new GoodsinfoCollection();

            if (pageindex <= 0)
            {
                return coll;
            }

            condition += DbProvider.GetInstance().GetGoodsDisplayCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsCloseCondition((int)MallUtils.OperaCode.Equal, 0);
            condition += DbProvider.GetInstance().GetGoodsExpirationCondition((int)MallUtils.OperaCode.LessthanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsDateLineCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsRemainCondition((int)MallUtils.OperaCode.Morethan, 0);

            return DTO.GetGoodsInfoList(DbProvider.GetInstance().GetGoodsList(categoryid, pagesize, pageindex, condition, orderby, ascdesc));
        }


        /// <summary>
        /// 获取指定店铺商品分类id的商品列表
        /// </summary>
        /// <param name="shopcategoryid">店铺商品分类id</param>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="ascdesc">排序方法</param>
        /// <returns>商品列表</returns>
        public static GoodsinfoCollection GetGoodsInfoListByShopCategory(int shopcategoryid, int pagesize, int pageindex, string condition, string orderby, int ascdesc)
        {
            GoodsinfoCollection coll = new GoodsinfoCollection();

            if (pageindex <= 0)
            {
                return coll;
            }
            return DTO.GetGoodsInfoList(DbProvider.GetInstance().GetGoodsInfoListByShopCategory(shopcategoryid, pagesize, pageindex, condition, orderby, ascdesc));
        }

        /// <summary>
        /// 获取指定条件的商品信息列表
        /// </summary>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="ascdesc">排序方式</param>
        /// <returns>商品信息列表</returns>
        public static GoodsinfoCollection GetGoodsInfoList(int pagesize, int pageindex, string condition, string orderby, int ascdesc)
        {
            GoodsinfoCollection coll = new GoodsinfoCollection();

            if (pageindex <= 0)
            {
                return coll;
            }

            condition += DbProvider.GetInstance().GetGoodsDisplayCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsCloseCondition((int)MallUtils.OperaCode.Equal, 0);
            condition += DbProvider.GetInstance().GetGoodsExpirationCondition((int)MallUtils.OperaCode.LessthanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsDateLineCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsRemainCondition((int)MallUtils.OperaCode.Morethan, 0);

            return DTO.GetGoodsInfoList(DbProvider.GetInstance().GetGoodsList(pagesize, pageindex, condition, orderby, ascdesc));

        }

        /// <summary>
        /// 获取指定条件的商品数
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>商品数</returns>
        public static int GetGoodsCount(string condition)
        {
            condition += DbProvider.GetInstance().GetGoodsDisplayCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsCloseCondition((int)MallUtils.OperaCode.Equal, 0);
            condition += DbProvider.GetInstance().GetGoodsExpirationCondition((int)MallUtils.OperaCode.LessthanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsDateLineCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsRemainCondition((int)MallUtils.OperaCode.Morethan, 0);

            return DbProvider.GetInstance().GetGoodsCount(condition);
        }

        /// <summary>
        /// 获取指定店铺商品分类id的商品数
        /// </summary>
        /// <param name="shopcategoryid">店铺商品分类id</param>
        /// <param name="condition">查询条件</param>
        /// <returns>指定店铺商品分类id的商品数</returns>
        public static int GetGoodsCountByShopCategory(int shopcategoryid, string condition)
        {
            return DbProvider.GetInstance().GetGoodsCountByShopCategory(shopcategoryid, condition);
        }

        /// <summary>
        /// 获取指定分类和条件下的商品列表集合
        /// </summary>
        /// <param name="selleruid">卖家uid</param>
        /// <param name="allgoods">是否全部商品</param>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="ascdesc">排序方式(0:升序, 1:降序)</param>
        /// <returns>商品列表集合</returns>
        public static GoodsinfoCollection GetGoodsListBySellerUID(int selleruid, bool allgoods, int pagesize, int pageindex, string orderby, int ascdesc)
        {
            GoodsinfoCollection coll = new GoodsinfoCollection();

            if (pageindex <= 0)
            {
                return coll;
            }
            string condition = "";
            if (!allgoods)
            {
                condition += DbProvider.GetInstance().GetGoodsDateLineCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);
                condition += DbProvider.GetInstance().GetGoodsDisplayCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);
            }

            return DTO.GetGoodsInfoList(DbProvider.GetInstance().GetGoodsListBySellerUID(selleruid, pagesize, pageindex, condition, orderby, ascdesc));
        }

        /// <summary>
        /// 获取指定用户id的商品数
        /// </summary>
        /// <param name="selleruid">用户id</param>
        /// <param name="allgoods">是否全部商品</param>
        /// <returns>商品数</returns>
        public static int GetGoodsCountBySellerUid(int selleruid, bool allgoods)
        {
            string condition = "";
            if (!allgoods)
            {
                condition += DbProvider.GetInstance().GetGoodsDateLineCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);
                condition += DbProvider.GetInstance().GetGoodsDisplayCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);
            }
            return DbProvider.GetInstance().GetGoodsCountBySellerUid(selleruid, condition);
        }

        /// <summary>
        /// 获取指定分类和条件下的商品数
        /// </summary>
        /// <param name="categoryid">分类id</param>
        /// <param name="condition">查询条件</param>
        /// <returns>商品数</returns>
        public static int GetGoodsCount(int categoryid, string condition)
        {
            condition += DbProvider.GetInstance().GetGoodsDisplayCondition((int) MallUtils.OperaCode.MorethanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsCloseCondition((int) MallUtils.OperaCode.Equal, 0);
            condition += DbProvider.GetInstance().GetGoodsExpirationCondition((int)MallUtils.OperaCode.LessthanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsDateLineCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsRemainCondition((int)MallUtils.OperaCode.Morethan, 0);
            condition += DbProvider.GetInstance().GetGoodsDisplayCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);
            
            return DbProvider.GetInstance().GetGoodsCount(categoryid, condition);
        }

        /// <summary>
        /// 判断当前goodsidlist是否都在当前分类categoryid下
        /// </summary>
        /// <param name="goodsidlist">商品id列表</param>
        /// <param name="categoryid">分类id</param>
        /// <returns>是否都在指定分类下</returns>
        public static bool InSameCategory(string goodsidlist, int categoryid)
        {
            return DbProvider.GetInstance().InSameCategory(goodsidlist, categoryid);
        }

        /// <summary>
        /// 将商品高亮显示
        /// </summary>
        /// <param name="goodsidlist">商品id列表</param>
        /// <param name="intValue">高亮样式及颜色( 0 为解除高亮显示)</param>
        /// <returns>更新商品个数</returns>
        public static int SetHighlight(string goodsidlist, string intValue)
        {
            return SetGoodsStatus(goodsidlist, "highlight", intValue);
        }

        /// <summary>
        /// 设置商品状态属性
        /// </summary>
        /// <param name="goodsidlist">商品id列表</param>
        /// <param name="field">字段</param>
        /// <param name="intValue">整数值</param>
        /// <returns>执行设置的返回值</returns>
        private static int SetGoodsStatus(string goodsidlist, string field, string intValue)
        {
            if (!Utils.InArray(field.ToLower().Trim(), "displayorder,highlight"))
            {
                return -1;
            }

            if (!Utils.IsNumericArray(goodsidlist.Split(',')))
            {
                return -1;
            }

            return DbProvider.GetInstance().SetGoodsStatus(goodsidlist, field, intValue);
        }

        /// <summary>
        /// 设置商品属性
        /// </summary>
        /// <param name="goodsidlist">商品id列表</param>
        /// <param name="field">要更新的属性</param>
        /// <param name="intValue">要更新的值</param>
        /// <returns>执行设置的返回值</returns>
        private static int SetGoodsStatus(string goodsidlist, string field, int intValue)
        {
            if (!Utils.InArray(field.ToLower().Trim(), "displayorder,highlight"))
            {
                return -1;
            }

            if (!Utils.IsNumericArray(goodsidlist.Split(',')))
            {
                return -1;
            }

            foreach (DataRow dr in Goods.GetGoodsList(goodsidlist).Rows)
            {
                DbProvider.GetInstance().UpdateCategoryGoodsCounts(Convert.ToInt32(dr["categoryid"].ToString()), dr["parentcategorylist"].ToString().Trim(), -1);
            }

            return DbProvider.GetInstance().SetGoodsStatus(goodsidlist, field, intValue);
        }

        /// <summary>
        /// 获取Html标题
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <returns>Html标题</returns>
        public static string GetHtmlTitle(int goodsid)
        {
            StringBuilder dir = new StringBuilder();
            dir.Append(BaseConfigs.GetForumPath);
            dir.Append("cache/goods/magic/");
            dir.Append((goodsid / 1000 + 1).ToString());
            dir.Append("/");
            string filename = Utils.GetMapPath(dir.ToString() + goodsid.ToString() + "_htmltitle.config");
            if (!File.Exists(filename))
                return "";

            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
        }


        /// <summary>
        /// 将商品设置关闭/打开
        /// </summary>
        /// <param name="goodsidlist">要设置的商品列表</param>
        /// <param name="intValue">关闭/打开标志( 0 为打开,1 为关闭)</param>
        /// <returns>更新商品个数</returns>
        public static int SetClose(string goodsidlist, short intValue)
        {
            if (!Utils.IsNumericArray(goodsidlist.Split(',')))
            {
                return -1;
            }

            int result = DbProvider.GetInstance().SetGoodsClose(goodsidlist, intValue);

            //更新该商品分类的商品数
            foreach (DataRow dr in Goods.GetGoodsList(goodsidlist).Rows)
            {
                DbProvider.GetInstance().UpdateCategoryGoodsCounts(Convert.ToInt32(dr["categoryid"].ToString()), dr["parentcategorylist"].ToString().Trim(), intValue == 0 ? 1 : - 1);
            }
            return result;
        }

   
        /// <summary>
        /// 在数据库中删除指定商品
        /// </summary>
        /// <param name="goodsidlist">商品id列表</param>
        /// <param name="subtractCredits">是否减少用户积分(0不减少,1减少)</param>
        /// <param name="reserveAttach">是否保留附件</param>
        /// <returns>删除个数</returns>
        public static int DeleteGoods(string goodsidlist, int subtractCredits, bool reserveAttach)
        {
            if (!Utils.IsNumericArray(goodsidlist.Split(',')))
            {
                return -1;
            }

            if (!reserveAttach)
            {
                IDataReader reader = DbProvider.GetInstance().GetGoodsAttachmentList(goodsidlist);

                while (reader.Read())
                {
                    if (reader["filename"].ToString().Trim().ToLower().IndexOf("http") < 0)
                    {
                        if ((Utils.FileExists(Utils.GetMapPath(BaseConfigs.GetForumPath + "upload/mall/" + reader["filename"].ToString()))))
                        {
                            File.Delete(Utils.GetMapPath(BaseConfigs.GetForumPath + "upload/mall/" + reader["filename"].ToString()));
                        }
                    }
                }
                reader.Close();
                DbProvider.GetInstance().DeleteGoodsAttachments(goodsidlist);
            }

            foreach (DataRow dr in Goods.GetGoodsList(goodsidlist).Rows)
            {
                DbProvider.GetInstance().UpdateCategoryGoodsCounts(Convert.ToInt32(dr["categoryid"].ToString()), dr["parentcategorylist"].ToString().Trim(), -1);
            }

            int reval = DbProvider.GetInstance().DeleteGoods(goodsidlist);

            return reval;
        }

       


        /// <summary>
        /// 在数据库中删除指定商品
        /// </summary>
        /// <param name="goodsidlist">商品id列表</param>
        /// <param name="reserveAttach">是否保留附件</param>
        /// <returns>删除个数</returns>
        public static int DeleteGoods(string goodsidlist, bool reserveAttach)
        {
            return DeleteGoods(goodsidlist, (int)1, reserveAttach);
        }

        /// <summary>
        /// 在删除指定的商品
        /// </summary>
        /// <param name="goodsidlist">商品id列表</param>
        /// <param name="toDustbin">指定商品删除形式(0：直接从数据库中删除,并删除与之关联的信息  1：只将其从论坛列表中删除(将displayorder字段置为-1)将其放入回收站中</param>
        /// <param name="reserveAttach">是否保留附件</param>
        /// <returns>删除个数</returns>
        public static int DeleteGoods(string goodsidlist, byte toDustbin, bool reserveAttach)
        {
            if(toDustbin == 0)
            {
                return DeleteGoods(goodsidlist, reserveAttach);
            }
            else
            {
                return SetGoodsStatus(goodsidlist, "displayorder", -1);
            }
        }


        /// <summary>
        /// 获取指定商品标签id的商品信息集合
        /// </summary>
        /// <param name="tagid">tagid</param>
        /// <param name="pageid">页面id</param>
        /// <param name="pagesize">页面尺寸</param>
        /// <returns>商品信息集合</returns>
        public static GoodsinfoCollection GetGoodsWithSameTag(int tagid, int pageid, int pagesize)
        {
            return DTO.GetGoodsInfoList(DbProvider.GetInstance().GetGoodsWithSameTag(tagid, pageid, pagesize));
        }

        /// <summary>
        /// 更新指定商品的店铺商品分类字段
        /// </summary>
        /// <param name="goodsidlist">指定商品id串(格式:1,2,3)</param>
        /// <param name="shopgoodscategoryid">要绑定的店铺商品分类id</param>
        /// <returns>执行结果</returns>
        public static int MoveGoodsShopCategory(string goodsidlist, int shopgoodscategoryid)
        {
            if (!Utils.IsNumericArray(goodsidlist.Split(',')))
            {
                return -1;
            }

            return DbProvider.GetInstance().MoveGoodsShopCategory(goodsidlist, shopgoodscategoryid);
        }

        /// <summary>
        /// 移除指定商品的店铺商品分类
        /// </summary>
        /// <param name="removegoodsid">指定的商品id</param>
        /// <param name="removeshopgoodscategoryid">要移除的店铺商品分类id</param>
        /// <returns>被移除商品分类数</returns>
        public static int RemoveGoodsShopCategory(int removegoodsid, int removeshopgoodscategoryid)
        {
            return DbProvider.GetInstance().RemoveGoodsShopCategory(removegoodsid, removeshopgoodscategoryid);
        }

        /// <summary>
        /// 推荐商品
        /// </summary>
        /// <param name="goodsidlist">指定的商品id串(格式:1,2,3)</param>
        /// <returns>设置推荐商品数</returns>
        public static int RecommendGoods(string goodsidlist)
        {
            if (!Utils.IsNumericArray(goodsidlist.Split(',')))
            {
                return -1;
            }

            return DbProvider.GetInstance().RecommendGoods(goodsidlist, 1);
        }

        /// <summary>
        /// 取消推荐商品
        /// </summary>
        /// <param name="goodsidlist">指定的商品id串(格式:1,2,3)</param>
        /// <returns>取消推荐商品数</returns>
        public static int CancelRecommendGoods(string goodsidlist)
        {
            return DbProvider.GetInstance().RecommendGoods(goodsidlist, 0);
        }

        /// <summary>
        ///  获取热门商品信息
        /// </summary>
        /// <param name="datetype">天数</param>
        /// <param name="categroyid">商品分类</param>
        /// <param name="count">返回记录条数</param>
        /// <returns>返回Json数据</returns>
        public static string GetHotGoodsJsonData(int days, int categroyid, int count)
        {
            StringBuilder sb_goods = new StringBuilder();
            sb_goods.Append("[");

            string condition = DbProvider.GetInstance().GetGoodsCloseCondition((int)MallUtils.OperaCode.Equal, 0);
            condition += DbProvider.GetInstance().GetGoodsExpirationCondition((int)MallUtils.OperaCode.LessthanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsDateLineCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);
            condition += DbProvider.GetInstance().GetGoodsRemainCondition((int)MallUtils.OperaCode.Morethan, 0);
            condition += DbProvider.GetInstance().GetGoodsDisplayCondition((int)MallUtils.OperaCode.MorethanOrEqual, 0);
            IDataReader idatareader = DbProvider.GetInstance().GetHotGoods(days, categroyid, count, condition);

            while (idatareader.Read())
            {
                sb_goods.Append(string.Format("{{'goodsid' : {0}, 'title' : '{1}', 'goodspic' : '{2}', 'seller' : '{3}', 'selleruid' : {4}, 'price' :{5}}},",
                    idatareader["goodsid"].ToString(),
                    idatareader["title"].ToString().Trim(),
                    idatareader["goodspic"].ToString().Trim(),
                    idatareader["seller"].ToString().Trim(),
                    idatareader["selleruid"].ToString().Trim(),
                    idatareader["price"].ToString()
                    ));
            }
            idatareader.Close();

            if (sb_goods.ToString().EndsWith(","))
            {
                sb_goods.Remove(sb_goods.Length - 1, 1);
            }
            sb_goods.Append("]");
            return sb_goods.ToString();
        }

        /// <summary>
        ///  获取热门商品信息
        /// </summary>
        /// <param name="datetype">天数</param>
        /// <param name="categroyid">商品分类</param>
        /// <param name="count">返回记录条数</param>
        /// <returns>返回Json数据</returns>
        public static string GetGoodsListJsonData(int categroyid, int order, int topnumber)
        {
            StringBuilder sb_goods = new StringBuilder();
            sb_goods.Append("[");

            GoodsinfoCollection goodsinfocoll = GetGoodsInfoList(categroyid, topnumber, 1, "", order == 0 ? "goodsid" : "viewcount", 1);
            foreach (Goodsinfo goodsinfo in goodsinfocoll)
            {
                sb_goods.Append(string.Format("{{'goodsid' : {0}, 'title' : '{1}', 'goodspic' : '{2}', 'seller' : '{3}', 'selleruid' : {4}, 'price' :{5}, 'costprice' :{6}}},",
                    goodsinfo.Goodsid,
                    goodsinfo.Title,
                    goodsinfo.Goodspic,
                    goodsinfo.Seller,
                    goodsinfo.Selleruid,
                    goodsinfo.Price,
                    goodsinfo.Costprice
                    ));
            }
            if (sb_goods.ToString().EndsWith(","))
            {
                sb_goods.Remove(sb_goods.Length - 1, 1);
            }
            sb_goods.Append("]");
            return sb_goods.ToString();
        }
       

        /// <summary>
        /// 数据转换对象类
        /// </summary>
        public class DTO
        {
           
            /// <summary>
            /// 设置魔法主题
            /// </summary>
            /// <param name="goodsinfo">要设置的商品信息</param>
            /// <returns></returns>
            public static string SetMagicTitle(Goodsinfo goodsinfo)
            {
                if (Topics.GetMagicValue(goodsinfo.Magic, MagicType.HtmlTitle) == 1)
                {
                    return Goods.GetHtmlTitle(goodsinfo.Goodsid);
                }

                if (goodsinfo.Highlight != "")
                {
                    return "<span style=\"" + goodsinfo.Highlight + "\">" + goodsinfo.Title + "</span>";
                }
                else
                {
                    return goodsinfo.Title;
                }
            }
          
            /// <summary>
            /// 获得商品信息(DTO)
            /// </summary>
            /// <param name="idatareader">要转换的数据</param>
            /// <returns>返回商品信息</returns>
            public static GoodsinfoCollection GetGoodsInfoList(IDataReader reader)
            {
                GoodsinfoCollection goodsinfocoll = new GoodsinfoCollection();
                //StringBuilder tablefield = new StringBuilder().Capacity(2000);
                //tablefield.Append(",");
                //foreach (DataRow dr in __idatareader.GetSchemaTable().Rows)
                //{
                //    tablefield.Append(dr["ColumnName"].ToString().ToLower() + ",");
                //}

                while (reader.Read())
                {
                    Goodsinfo goodsinfo = new Goodsinfo();
                    //if (tablefield.ToString().IndexOf(",goodsid,")>=0)
                    //{
                    goodsinfo.Goodsid = Convert.ToInt32(reader["goodsid"].ToString());
                    //}
                    goodsinfo.Shopid = Convert.ToInt32(reader["shopid"].ToString());
                    goodsinfo.Parentcategorylist = reader["parentcategorylist"].ToString();
                    goodsinfo.Shopcategorylist = reader["shopcategorylist"].ToString();
                    goodsinfo.Categoryid = Convert.ToInt32(reader["categoryid"].ToString());
                    goodsinfo.Recommend = Convert.ToInt32(reader["recommend"].ToString());
                    goodsinfo.Discount = Convert.ToInt32(reader["discount"].ToString());
                    goodsinfo.Selleruid = Convert.ToInt32(reader["selleruid"].ToString());
                    goodsinfo.Seller = reader["seller"].ToString();
                    goodsinfo.Account = reader["account"].ToString();
                    goodsinfo.Magic = Convert.ToInt32(reader["magic"].ToString());
                    goodsinfo.Price = Convert.ToDecimal(reader["price"].ToString());
                    goodsinfo.Amount = Convert.ToInt32(reader["amount"].ToString());
                    goodsinfo.Quality = Convert.ToInt32(reader["quality"].ToString());
                    goodsinfo.Lid = Convert.ToInt32(reader["lid"].ToString());
                    goodsinfo.Locus = reader["locus"].ToString();
                    goodsinfo.Transport = Convert.ToInt32(reader["transport"].ToString());
                    goodsinfo.Ordinaryfee = Convert.ToDecimal(reader["ordinaryfee"].ToString());
                    goodsinfo.Expressfee = Convert.ToDecimal(reader["expressfee"].ToString());
                    goodsinfo.Emsfee = Convert.ToDecimal(reader["emsfee"].ToString());
                    goodsinfo.Itemtype = Convert.ToInt32(reader["itemtype"].ToString());
                    goodsinfo.Dateline = Convert.ToDateTime(reader["dateline"].ToString());
                    goodsinfo.Expiration = Convert.ToDateTime(reader["expiration"].ToString());
                    goodsinfo.Lastbuyer = reader["lastbuyer"].ToString();
                    goodsinfo.Lasttrade = Convert.ToDateTime(reader["lasttrade"].ToString());
                    goodsinfo.Lastupdate = Convert.ToDateTime(reader["lastupdate"].ToString());
                    goodsinfo.Totalitems = Convert.ToInt32(reader["totalitems"].ToString());
                    goodsinfo.Tradesum = Convert.ToDecimal(reader["tradesum"].ToString());
                    goodsinfo.Closed = Convert.ToInt32(reader["closed"].ToString());
                    goodsinfo.Aid = Convert.ToInt32(reader["aid"].ToString());
                    goodsinfo.Goodspic = reader["goodspic"].ToString().Trim();
                    goodsinfo.Displayorder = Convert.ToInt32(reader["displayorder"].ToString());
                    goodsinfo.Costprice = Convert.ToDecimal(reader["costprice"].ToString());
                    goodsinfo.Invoice = Convert.ToInt32(reader["invoice"].ToString());
                    goodsinfo.Repair = Convert.ToInt32(reader["repair"].ToString());
                    goodsinfo.Message = reader["message"].ToString();
                    goodsinfo.Otherlink = reader["otherlink"].ToString();
                    goodsinfo.Readperm = Convert.ToInt32(reader["readperm"].ToString());
                    goodsinfo.Tradetype = Convert.ToInt32(reader["tradetype"].ToString());
                    goodsinfo.Viewcount = Convert.ToInt32(reader["viewcount"].ToString());
                    goodsinfo.Smileyoff = Convert.ToInt32(reader["smileyoff"].ToString());
                    goodsinfo.Bbcodeoff = Convert.ToInt32(reader["bbcodeoff"].ToString());
                    goodsinfo.Parseurloff = Convert.ToInt32(reader["parseurloff"].ToString());
                    goodsinfo.Highlight = reader["highlight"].ToString().Trim();
                    goodsinfo.Title = reader["title"].ToString().Trim();
                    goodsinfo.Htmltitle = SetMagicTitle(goodsinfo);

                    goodsinfocoll.Add(goodsinfo);
                }
                reader.Close();

                return goodsinfocoll;
            }



            /// <summary>
            /// 获得商品信息(DTO)
            /// </summary>
            /// <param name="idatareader">要转换的数据</param>
            /// <returns>返回商品信息</returns>
            public static Goodsinfo GetGoodsInfo(IDataReader reader)
            {
                if (reader.Read())
                {
                    Goodsinfo goodsinfo = new Goodsinfo();
                    goodsinfo.Goodsid = Convert.ToInt32(reader["goodsid"].ToString());
                    goodsinfo.Shopid = Convert.ToInt32(reader["shopid"].ToString());
                    goodsinfo.Parentcategorylist = reader["parentcategorylist"].ToString();
                    goodsinfo.Shopcategorylist = reader["shopcategorylist"].ToString();
                    goodsinfo.Categoryid = Convert.ToInt32(reader["categoryid"].ToString());
                    goodsinfo.Recommend = Convert.ToInt16(reader["recommend"].ToString());
                    goodsinfo.Discount = Convert.ToInt16(reader["discount"].ToString());
                    goodsinfo.Selleruid = Convert.ToInt32(reader["selleruid"].ToString());
                    goodsinfo.Seller = reader["seller"].ToString().Trim();
                    goodsinfo.Account = reader["account"].ToString().Trim();
                    goodsinfo.Magic = Convert.ToInt32(reader["magic"].ToString());
                    goodsinfo.Price = Convert.ToDecimal(reader["price"].ToString());
                    goodsinfo.Amount = Convert.ToInt16(reader["amount"].ToString());
                    goodsinfo.Quality = Convert.ToInt16(reader["quality"].ToString());
                    goodsinfo.Lid = Convert.ToInt32(reader["lid"].ToString());
                    goodsinfo.Locus = reader["locus"].ToString().Trim();
                    goodsinfo.Transport = Convert.ToInt16(reader["transport"].ToString());
                    goodsinfo.Ordinaryfee = Convert.ToDecimal(reader["ordinaryfee"].ToString());
                    goodsinfo.Expressfee = Convert.ToDecimal(reader["expressfee"].ToString());
                    goodsinfo.Emsfee = Convert.ToDecimal(reader["emsfee"].ToString());
                    goodsinfo.Itemtype = Convert.ToInt16(reader["itemtype"].ToString());
                    goodsinfo.Dateline = Convert.ToDateTime(reader["dateline"].ToString());
                    goodsinfo.Expiration = Convert.ToDateTime(reader["expiration"].ToString());
                    goodsinfo.Lastbuyer = reader["lastbuyer"].ToString().Trim();
                    goodsinfo.Lasttrade = Convert.ToDateTime(reader["lasttrade"].ToString());
                    goodsinfo.Lastupdate = Convert.ToDateTime(reader["lastupdate"].ToString());
                    goodsinfo.Totalitems = Convert.ToInt16(reader["totalitems"].ToString());
                    goodsinfo.Tradesum = Convert.ToDecimal(reader["tradesum"].ToString());
                    goodsinfo.Closed = Convert.ToInt16(reader["closed"].ToString());
                    goodsinfo.Aid = Convert.ToInt32(reader["aid"].ToString());
                    goodsinfo.Goodspic = reader["goodspic"].ToString().Trim();
                    goodsinfo.Displayorder = Convert.ToInt16(reader["displayorder"].ToString());
                    goodsinfo.Costprice = Convert.ToDecimal(reader["costprice"].ToString());
                    goodsinfo.Invoice = Convert.ToInt16(reader["invoice"].ToString());
                    goodsinfo.Repair = Convert.ToInt16(reader["repair"].ToString());
                    goodsinfo.Message = reader["message"].ToString().Trim();
                    goodsinfo.Otherlink = reader["otherlink"].ToString().Trim();
                    goodsinfo.Readperm = Convert.ToInt32(reader["readperm"].ToString());
                    goodsinfo.Tradetype = Convert.ToInt16(reader["tradetype"].ToString());
                    goodsinfo.Viewcount = Convert.ToInt32(reader["viewcount"].ToString());
                    goodsinfo.Smileyoff = Convert.ToInt32(reader["smileyoff"].ToString());
                    goodsinfo.Bbcodeoff = Convert.ToInt32(reader["bbcodeoff"].ToString());
                    goodsinfo.Parseurloff = Convert.ToInt32(reader["parseurloff"].ToString());
                    goodsinfo.Highlight = reader["highlight"].ToString().Trim();
                    goodsinfo.Title = reader["title"].ToString().Trim();
                    goodsinfo.Htmltitle = SetMagicTitle(goodsinfo);

                    reader.Close();
                    return goodsinfo;
                }
                return null;
            }


            /// <summary>
            /// 获得商品信息(DTO)
            /// </summary>
            /// <param name="dt">要转换的数据表</param>
            /// <returns>返回商品信息</returns>
            public static Goodsinfo[] GetGoodsInfoArray(DataTable dt)
            {
                if (dt == null || dt.Rows.Count == 0)
                    return null;

                Goodsinfo[] goodsinfoarray = new Goodsinfo[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goodsinfoarray[i] = new Goodsinfo();
                    goodsinfoarray[i].Goodsid = Convert.ToInt32(dt.Rows[i]["goodsid"].ToString());
                    goodsinfoarray[i].Shopid = Convert.ToInt32(dt.Rows[i]["shopid"].ToString());
                    goodsinfoarray[i].Parentcategorylist = dt.Rows[i]["parentcategorylist"].ToString();
                    goodsinfoarray[i].Shopcategorylist = dt.Rows[i]["shopcategorylist"].ToString();
                    goodsinfoarray[i].Categoryid = Convert.ToInt32(dt.Rows[i]["categoryid"].ToString());
                    goodsinfoarray[i].Recommend = Convert.ToInt32(dt.Rows[i]["recommend"].ToString());
                    goodsinfoarray[i].Discount = Convert.ToInt32(dt.Rows[i]["discount"].ToString());
                    goodsinfoarray[i].Selleruid = Convert.ToInt32(dt.Rows[i]["selleruid"].ToString());
                    goodsinfoarray[i].Seller = dt.Rows[i]["seller"].ToString();
                    goodsinfoarray[i].Account = dt.Rows[i]["account"].ToString();
                    goodsinfoarray[i].Magic = Convert.ToInt32(dt.Rows[i]["magic"].ToString());
                    goodsinfoarray[i].Price = Convert.ToDecimal(dt.Rows[i]["price"].ToString());
                    goodsinfoarray[i].Amount = Convert.ToInt32(dt.Rows[i]["amount"].ToString());
                    goodsinfoarray[i].Quality = Convert.ToInt32(dt.Rows[i]["quality"].ToString());
                    goodsinfoarray[i].Lid = Convert.ToInt32(dt.Rows[i]["lid"].ToString());
                    goodsinfoarray[i].Locus = dt.Rows[i]["locus"].ToString();
                    goodsinfoarray[i].Transport = Convert.ToInt32(dt.Rows[i]["transport"].ToString());
                    goodsinfoarray[i].Ordinaryfee = Convert.ToDecimal(dt.Rows[i]["ordinaryfee"].ToString());
                    goodsinfoarray[i].Expressfee = Convert.ToDecimal(dt.Rows[i]["expressfee"].ToString());
                    goodsinfoarray[i].Emsfee = Convert.ToDecimal(dt.Rows[i]["emsfee"].ToString());
                    goodsinfoarray[i].Itemtype = Convert.ToInt32(dt.Rows[i]["itemtype"].ToString());
                    goodsinfoarray[i].Dateline = Convert.ToDateTime(dt.Rows[i]["dateline"].ToString());
                    goodsinfoarray[i].Expiration = Convert.ToDateTime(dt.Rows[i]["expiration"].ToString());
                    goodsinfoarray[i].Lastbuyer = dt.Rows[i]["lastbuyer"].ToString();
                    goodsinfoarray[i].Lasttrade = Convert.ToDateTime(dt.Rows[i]["lasttrade"].ToString());
                    goodsinfoarray[i].Lastupdate = Convert.ToDateTime(dt.Rows[i]["lastupdate"].ToString());
                    goodsinfoarray[i].Totalitems = Convert.ToInt32(dt.Rows[i]["totalitems"].ToString());
                    goodsinfoarray[i].Tradesum = Convert.ToDecimal(dt.Rows[i]["tradesum"].ToString());
                    goodsinfoarray[i].Closed = Convert.ToInt32(dt.Rows[i]["closed"].ToString());
                    goodsinfoarray[i].Aid = Convert.ToInt32(dt.Rows[i]["aid"].ToString());
                    goodsinfoarray[i].Goodspic = dt.Rows[i]["goodspic"].ToString();
                    goodsinfoarray[i].Displayorder = Convert.ToInt32(dt.Rows[i]["displayorder"].ToString());
                    goodsinfoarray[i].Costprice = Convert.ToDecimal(dt.Rows[i]["costprice"].ToString());
                    goodsinfoarray[i].Invoice = Convert.ToInt32(dt.Rows[i]["invoice"].ToString());
                    goodsinfoarray[i].Repair = Convert.ToInt32(dt.Rows[i]["repair"].ToString());
                    goodsinfoarray[i].Message = dt.Rows[i]["message"].ToString();
                    goodsinfoarray[i].Otherlink = dt.Rows[i]["otherlink"].ToString();
                    goodsinfoarray[i].Readperm = Convert.ToInt32(dt.Rows[i]["readperm"].ToString());
                    goodsinfoarray[i].Tradetype = Convert.ToInt32(dt.Rows[i]["tradetype"].ToString());
                    goodsinfoarray[i].Viewcount = Convert.ToInt32(dt.Rows[i]["viewcount"].ToString());
                    goodsinfoarray[i].Smileyoff = Convert.ToInt32(dt.Rows[i]["smileyoff"].ToString());
                    goodsinfoarray[i].Bbcodeoff = Convert.ToInt32(dt.Rows[i]["bbcodeoff"].ToString());
                    goodsinfoarray[i].Parseurloff = Convert.ToInt32(dt.Rows[i]["parseurloff"].ToString());
                    goodsinfoarray[i].Highlight = dt.Rows[i]["highlight"].ToString().Trim();
                    goodsinfoarray[i].Title = dt.Rows[i]["title"].ToString();
                    goodsinfoarray[i].Htmltitle = SetMagicTitle(goodsinfoarray[i]);

                }
                dt.Dispose();
                return goodsinfoarray;
            }

        }

    }
}
