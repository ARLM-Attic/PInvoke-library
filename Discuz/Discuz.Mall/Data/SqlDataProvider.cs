#if NET1
#else
using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Discuz.Data;
using Discuz.Config;
using Discuz.Common;
using Discuz.Entity;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Discuz.Mall.Data
{
    /// <summary>
    /// 数据提供者
    /// </summary>
    public class DataProvider 
    {

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="__goods">要添加的商品信息实例</param>
        /// <returns></returns>
        public int CreateGoods(Goodsinfo __goods)
        {

            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@shopid", (DbType)SqlDbType.Int, 4,__goods.Shopid),
						DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4,__goods.Categoryid),
                        DbHelper.MakeInParam("@parentcategorylist", (DbType)SqlDbType.Char, 300,__goods.Parentcategorylist),
                        DbHelper.MakeInParam("@shopcategorylist", (DbType)SqlDbType.Char, 300,__goods.Shopcategorylist),
						DbHelper.MakeInParam("@recommend", (DbType)SqlDbType.TinyInt, 1,__goods.Recommend),
						DbHelper.MakeInParam("@discount", (DbType)SqlDbType.TinyInt, 1,__goods.Discount),
                        DbHelper.MakeInParam("@selleruid", (DbType)SqlDbType.Int, 4,__goods.Selleruid),
						DbHelper.MakeInParam("@seller", (DbType)SqlDbType.NChar, 20,__goods.Seller),
						DbHelper.MakeInParam("@account", (DbType)SqlDbType.NChar, 50,__goods.Account),
						DbHelper.MakeInParam("@title", (DbType)SqlDbType.NChar, 30,__goods.Title),
						DbHelper.MakeInParam("@magic", (DbType)SqlDbType.Int, 4,__goods.Magic),
						DbHelper.MakeInParam("@price", (DbType)SqlDbType.Decimal, 18,__goods.Price),
						DbHelper.MakeInParam("@amount", (DbType)SqlDbType.SmallInt, 2,__goods.Amount),
						DbHelper.MakeInParam("@quality", (DbType)SqlDbType.TinyInt, 1,__goods.Quality),
						DbHelper.MakeInParam("@lid", (DbType)SqlDbType.Int, 4,__goods.Lid),
						DbHelper.MakeInParam("@locus", (DbType)SqlDbType.NChar, 20,__goods.Locus),
						DbHelper.MakeInParam("@transport", (DbType)SqlDbType.TinyInt, 1,__goods.Transport),
						DbHelper.MakeInParam("@ordinaryfee", (DbType)SqlDbType.Decimal, 18,__goods.Ordinaryfee),
						DbHelper.MakeInParam("@expressfee", (DbType)SqlDbType.Decimal, 18,__goods.Expressfee),
						DbHelper.MakeInParam("@emsfee", (DbType)SqlDbType.Decimal, 18,__goods.Emsfee),
						DbHelper.MakeInParam("@itemtype", (DbType)SqlDbType.TinyInt, 1,__goods.Itemtype),
						DbHelper.MakeInParam("@dateline", (DbType)SqlDbType.DateTime, 8,__goods.Dateline),
						DbHelper.MakeInParam("@expiration", (DbType)SqlDbType.DateTime, 8,__goods.Expiration),
						DbHelper.MakeInParam("@lastbuyer", (DbType)SqlDbType.NChar, 10,__goods.Lastbuyer),
						DbHelper.MakeInParam("@lasttrade", (DbType)SqlDbType.DateTime, 8,__goods.Lasttrade),
						DbHelper.MakeInParam("@lastupdate", (DbType)SqlDbType.DateTime, 8,__goods.Lastupdate),
						DbHelper.MakeInParam("@totalitems", (DbType)SqlDbType.SmallInt, 2,__goods.Totalitems),
						DbHelper.MakeInParam("@tradesum", (DbType)SqlDbType.Decimal, 18,__goods.Tradesum),
						DbHelper.MakeInParam("@closed", (DbType)SqlDbType.TinyInt, 1,__goods.Closed),
						DbHelper.MakeInParam("@aid", (DbType)SqlDbType.Int, 4,__goods.Aid),
                        DbHelper.MakeInParam("@goodspic", (DbType)SqlDbType.NChar, 100,__goods.Goodspic),
						DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 1,__goods.Displayorder),
						DbHelper.MakeInParam("@costprice", (DbType)SqlDbType.Decimal, 18,__goods.Costprice),
						DbHelper.MakeInParam("@invoice", (DbType)SqlDbType.TinyInt, 1,__goods.Invoice),
						DbHelper.MakeInParam("@repair", (DbType)SqlDbType.SmallInt, 2,__goods.Repair),
						DbHelper.MakeInParam("@message", (DbType)SqlDbType.NText, 0,__goods.Message),
						DbHelper.MakeInParam("@otherlink", (DbType)SqlDbType.NChar, 250,__goods.Otherlink),
						DbHelper.MakeInParam("@readperm", (DbType)SqlDbType.Int, 4,__goods.Readperm),
						DbHelper.MakeInParam("@tradetype", (DbType)SqlDbType.TinyInt, 1,__goods.Tradetype),
                        DbHelper.MakeInParam("@viewcount", (DbType)SqlDbType.Int, 4,__goods.Viewcount),
                        //DbHelper.MakeInParam("@invisible", (DbType)SqlDbType.Int, 4,__goods.Invisible),
						DbHelper.MakeInParam("@smileyoff", (DbType)SqlDbType.Int, 4,__goods.Smileyoff),
						DbHelper.MakeInParam("@bbcodeoff", (DbType)SqlDbType.Int, 4,__goods.Bbcodeoff),
						DbHelper.MakeInParam("@parseurloff", (DbType)SqlDbType.Int, 4,__goods.Parseurloff),
                        DbHelper.MakeInParam("@highlight", (DbType)SqlDbType.VarChar, 500,__goods.Highlight)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "goods] ([shopid], [categoryid], [parentcategorylist], [shopcategorylist], [recommend], [discount], [selleruid], [seller], [account], [title], [magic], [price], [amount], [quality], [lid], [locus], [transport], [ordinaryfee], [expressfee], [emsfee], [itemtype], [dateline], [expiration], [lastbuyer], [lasttrade], [lastupdate], [totalitems], [tradesum], [closed], [aid], [goodspic], [displayorder], [costprice], [invoice], [repair], [message], [otherlink], [readperm], [tradetype], [viewcount], [smileyoff], [bbcodeoff], [parseurloff], [highlight] ) VALUES (@shopid, @categoryid, @parentcategorylist,@shopcategorylist, @recommend, @discount, @selleruid, @seller, @account, @title, @magic, @price, @amount, @quality, @lid, @locus, @transport, @ordinaryfee, @expressfee, @emsfee, @itemtype, @dateline, @expiration, @lastbuyer, @lasttrade, @lastupdate, @totalitems, @tradesum, @closed, @aid, @goodspic, @displayorder, @costprice, @invoice, @repair, @message, @otherlink, @readperm, @tradetype, @viewcount, @smileyoff, @bbcodeoff, @parseurloff, @highlight);SELECT SCOPE_IDENTITY()  AS 'goodsid'");

            return Utils.StrToInt(DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0].Rows[0][0].ToString(), -1);
        }

        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="goods">要更新的商品信息实例</param>
        /// <returns></returns>
        public bool UpdateGoods(Goodsinfo __goods)
        {

            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@shopid", (DbType)SqlDbType.Int, 4,__goods.Shopid),
						DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4,__goods.Categoryid),
                        DbHelper.MakeInParam("@parentcategorylist", (DbType)SqlDbType.Char, 300,__goods.Parentcategorylist),
                        DbHelper.MakeInParam("@shopcategorylist", (DbType)SqlDbType.Char, 300,__goods.Shopcategorylist),
						DbHelper.MakeInParam("@recommend", (DbType)SqlDbType.TinyInt, 1,__goods.Recommend),
						DbHelper.MakeInParam("@discount", (DbType)SqlDbType.TinyInt, 1,__goods.Discount),
                        DbHelper.MakeInParam("@selleruid", (DbType)SqlDbType.Int, 4,__goods.Selleruid),
						DbHelper.MakeInParam("@seller", (DbType)SqlDbType.NChar, 20,__goods.Seller),
						DbHelper.MakeInParam("@account", (DbType)SqlDbType.NChar, 50,__goods.Account),
						DbHelper.MakeInParam("@title", (DbType)SqlDbType.NChar, 30,__goods.Title),
						DbHelper.MakeInParam("@magic", (DbType)SqlDbType.Int, 4,__goods.Magic),
						DbHelper.MakeInParam("@price", (DbType)SqlDbType.Decimal, 18,__goods.Price),
						DbHelper.MakeInParam("@amount", (DbType)SqlDbType.SmallInt, 2,__goods.Amount),
						DbHelper.MakeInParam("@quality", (DbType)SqlDbType.TinyInt, 1,__goods.Quality),
						DbHelper.MakeInParam("@lid", (DbType)SqlDbType.Int, 4,__goods.Lid),
						DbHelper.MakeInParam("@locus", (DbType)SqlDbType.NChar, 20,__goods.Locus),
						DbHelper.MakeInParam("@transport", (DbType)SqlDbType.TinyInt, 1,__goods.Transport),
			            DbHelper.MakeInParam("@ordinaryfee", (DbType)SqlDbType.Decimal, 18,__goods.Ordinaryfee),
						DbHelper.MakeInParam("@expressfee", (DbType)SqlDbType.Decimal, 18,__goods.Expressfee),
						DbHelper.MakeInParam("@emsfee", (DbType)SqlDbType.Decimal, 18,__goods.Emsfee),
						DbHelper.MakeInParam("@itemtype", (DbType)SqlDbType.TinyInt, 1,__goods.Itemtype),
						DbHelper.MakeInParam("@dateline", (DbType)SqlDbType.DateTime, 8,__goods.Dateline),
						DbHelper.MakeInParam("@expiration", (DbType)SqlDbType.DateTime, 8,__goods.Expiration),
						DbHelper.MakeInParam("@lastbuyer", (DbType)SqlDbType.NChar, 10,__goods.Lastbuyer),
						DbHelper.MakeInParam("@lasttrade", (DbType)SqlDbType.DateTime, 8,__goods.Lasttrade),
						DbHelper.MakeInParam("@lastupdate", (DbType)SqlDbType.DateTime, 8,__goods.Lastupdate),
						DbHelper.MakeInParam("@totalitems", (DbType)SqlDbType.SmallInt, 2,__goods.Totalitems),
						DbHelper.MakeInParam("@tradesum", (DbType)SqlDbType.Decimal, 18,__goods.Tradesum),
						DbHelper.MakeInParam("@closed", (DbType)SqlDbType.TinyInt, 1,__goods.Closed),
						DbHelper.MakeInParam("@aid", (DbType)SqlDbType.Int, 4,__goods.Aid),
                        DbHelper.MakeInParam("@goodspic", (DbType)SqlDbType.NChar, 100,__goods.Goodspic),
						DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 1,__goods.Displayorder),
						DbHelper.MakeInParam("@costprice", (DbType)SqlDbType.Decimal, 18,__goods.Costprice),
						DbHelper.MakeInParam("@invoice", (DbType)SqlDbType.TinyInt, 1,__goods.Invoice),
						DbHelper.MakeInParam("@repair", (DbType)SqlDbType.SmallInt, 2,__goods.Repair),
						DbHelper.MakeInParam("@message", (DbType)SqlDbType.NText, 0,__goods.Message),
						DbHelper.MakeInParam("@otherlink", (DbType)SqlDbType.NChar, 250,__goods.Otherlink),
						DbHelper.MakeInParam("@readperm", (DbType)SqlDbType.Int, 4,__goods.Readperm),
						DbHelper.MakeInParam("@tradetype", (DbType)SqlDbType.TinyInt, 1,__goods.Tradetype),
						DbHelper.MakeInParam("@viewcount", (DbType)SqlDbType.Int, 4,__goods.Viewcount),
                        //DbHelper.MakeInParam("@invisible", (DbType)SqlDbType.Int, 4,__goods.Invisible),
						DbHelper.MakeInParam("@smileyoff", (DbType)SqlDbType.Int, 4,__goods.Smileyoff),
						DbHelper.MakeInParam("@bbcodeoff", (DbType)SqlDbType.Int, 4,__goods.Bbcodeoff),
						DbHelper.MakeInParam("@parseurloff", (DbType)SqlDbType.Int, 4,__goods.Parseurloff),
                        DbHelper.MakeInParam("@highlight", (DbType)SqlDbType.VarChar, 500,__goods.Highlight),
                        DbHelper.MakeInParam("@goodsid", (DbType)SqlDbType.Int, 4,__goods.Goodsid)
				};
            string sqlstring = String.Format("Update [" + BaseConfigs.GetTablePrefix + "goods]  Set [shopid] = @shopid, [categoryid] = @categoryid, [parentcategorylist] = @parentcategorylist, [shopcategorylist] = @shopcategorylist, [recommend] = @recommend, [discount] = @discount, selleruid = @selleruid, [seller] = @seller, [account] = @account, [title] = @title, [magic] = @magic, [price] = @price, [amount] = @amount, [quality] = @quality, [lid] = @lid, [locus] = @locus, [transport] = @transport, [ordinaryfee] = @ordinaryfee, [expressfee] = @expressfee, [emsfee] = @emsfee, [itemtype] = @itemtype, [dateline] = @dateline, [expiration] = @expiration, [lastbuyer] = @lastbuyer, [lasttrade] = @lasttrade, [lastupdate] = @lastupdate, [totalitems] = @totalitems, [tradesum] = @tradesum, [closed] = @closed, [aid] = @aid, [goodspic] = @goodspic, [displayorder] = @displayorder, [costprice] = @costprice, [invoice] = @invoice, [repair] = @repair, [message] = @message, [otherlink] = @otherlink, [readperm] = @readperm, [tradetype] = @tradetype, [viewcount] = @viewcount, [smileyoff] = @smileyoff, [bbcodeoff] = @bbcodeoff, [highlight] = @highlight  WHERE [goodsid] = @goodsid");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
        }

        /// <summary>
        /// 更新指定商品分类的显示顺序
        /// </summary>
        /// <param name="displayorder">显示顺序值</param>
        /// <param name="categoryid">更新的商品分类id</param>
        public void UpdateGoodsCategoryDisplayorder(int displayorder, int categoryid)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, displayorder),
                DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4, categoryid)
			};
            DbHelper.ExecuteDataset(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "goodscategories] SET [displayorder]=@displayorder WHERE [categoryid]=@categoryid", parms);
        }

       
        /// <summary>
        /// 返回商品分类数据表(已绑定fid)
        /// </summary>
        /// <returns></returns>
        public DataTable GetCategoriesTable()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "goodscategories] WHERE [fid] > 0 ORDER BY [categoryid] ASC").Tables[0];
        }

        /// <summary>
        /// 获取全部商品分类
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllCategoriesTable()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "goodscategories] ORDER BY [categoryid] ASC").Tables[0];
        }

        /// <summary>
        /// 返回商品一级(root)分类数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetRootCategoriesTable()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "goodscategories] WHERE [parentid] = 0 ORDER BY [categoryid] ASC").Tables[0];
        }

        /// <summary>
        /// 返回用于生成JSON格式串的商品分类数据表
        /// </summary>
        /// <returns>返回JSON数据串</returns>
        public DataTable GetCategoriesTableToJson()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT [categoryid] AS [id], [parentid] AS [pid], [layer] AS [layer], [parentidlist] AS [pidlist], [categoryname] AS [name], [haschild] AS [child], [fid] FROM [" + BaseConfigs.GetTablePrefix + "goodscategories] ORDER BY [categoryid] ASC").Tables[0];
        }

        /// <summary>
        /// 通过商品分类id得到其所指向的父id
        /// </summary>
        /// <param name="categoryid">要查询的分类id</param>
        /// <returns>父id</returns>
        public int GetCategoriesParentidByID(int categoryid)
        {
            DbParameter[] parms = 
			{
				DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4, categoryid)
			};
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, "SELECT TOP 1 [parentid] FROM [" + BaseConfigs.GetTablePrefix + "goodscategories] WHERE categoryid=@categoryid", parms));
        }

        /// <summary>
        /// 创建商品分类
        /// </summary>
        /// <param name="goodscategories">要创建的商品分类信息</param>
        /// <returns>创建的商品分类id</returns>
        public int CreateGoodscategory(Goodscategoryinfo __goodscategories)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@parentid", (DbType)SqlDbType.Int, 4,__goodscategories.Parentid),
						DbHelper.MakeInParam("@layer", (DbType)SqlDbType.SmallInt, 2,__goodscategories.Layer),
						DbHelper.MakeInParam("@parentidlist", (DbType)SqlDbType.Char, 300,__goodscategories.Parentidlist),
                        DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4,__goodscategories.Displayorder),
						DbHelper.MakeInParam("@categoryname", (DbType)SqlDbType.NChar, 50,__goodscategories.Categoryname),
						DbHelper.MakeInParam("@haschild", (DbType)SqlDbType.Bit, 1,__goodscategories.Haschild),
						DbHelper.MakeInParam("@fid", (DbType)SqlDbType.Int, 4,__goodscategories.Fid),
						DbHelper.MakeInParam("@pathlist", (DbType)SqlDbType.NChar, 3000,__goodscategories.Pathlist),
						DbHelper.MakeInParam("@goodscount", (DbType)SqlDbType.Int, 4,__goodscategories.Goodscount)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "goodscategories] ([parentid], [layer], [parentidlist], [displayorder], [categoryname], [haschild], [fid], [pathlist], [goodscount]) VALUES (@parentid, @layer, @parentidlist, @displayorder, @categoryname, @haschild, @fid, @pathlist, @goodscount);SELECT SCOPE_IDENTITY()  AS aid");
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }

        /// <summary>
        /// 删除商品分类
        /// </summary>
        /// <param name="categoryid">要删除的商品分类ID</param>
        public void DeleteGoodsCategory(int categoryid)
        {
            DbParameter parm = DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4, categoryid);

            DbHelper.ExecuteReader(CommandType.Text, string.Format("DELETE FROM [{0}goodscategories] WHERE [categoryid] = @categoryid", BaseConfigs.GetTablePrefix), parm);
        }

        /// <summary>
        /// 更新商品分类
        /// </summary>
        /// <param name="goodscategories">要更新的商品分类信息</param>
        /// <returns></returns>
        public bool UpdateGoodscategory(Goodscategoryinfo __goodscategories)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@parentid", (DbType)SqlDbType.Int, 4,__goodscategories.Parentid),
						DbHelper.MakeInParam("@layer", (DbType)SqlDbType.SmallInt, 2,__goodscategories.Layer),
						DbHelper.MakeInParam("@parentidlist", (DbType)SqlDbType.Char, 300,__goodscategories.Parentidlist),
                        DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4,__goodscategories.Displayorder),
						DbHelper.MakeInParam("@categoryname", (DbType)SqlDbType.NChar, 50,__goodscategories.Categoryname),
						DbHelper.MakeInParam("@haschild", (DbType)SqlDbType.Bit, 1,__goodscategories.Haschild),
						DbHelper.MakeInParam("@fid", (DbType)SqlDbType.Int, 4,__goodscategories.Fid),
						DbHelper.MakeInParam("@pathlist", (DbType)SqlDbType.NChar, 3000,__goodscategories.Pathlist),
						DbHelper.MakeInParam("@goodscount", (DbType)SqlDbType.Int, 4,__goodscategories.Goodscount),
                        DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4,__goodscategories.Categoryid)
				};
            string sqlstring = String.Format("Update [" + BaseConfigs.GetTablePrefix + "goodscategories]  Set [parentid] = @parentid, [layer] = @layer, [parentidlist] = @parentidlist, [displayorder] = @displayorder, [categoryname] = @categoryname, [haschild] = @haschild, [fid] = @fid, [pathlist] = @pathlist, [goodscount] = @goodscount WHERE [categoryid] = @categoryid");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
        }

        /// <summary>
        /// 获取指定商品分类ID的数据
        /// </summary>
        /// <param name="categoryid">商品分类ID</param>
        /// <returns></returns>
        public IDataReader GetGoodsCategoryInfoById(int categoryid)
        {
            DbParameter parm = DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4, categoryid);

            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}goodscategories] WHERE [categoryid] = @categoryid", BaseConfigs.GetTablePrefix), parm);
        }

        /// <summary>
        /// 更新指定商品分类的layer,parentidlist,haschild
        /// </summary>
        /// <param name="categoryid">要更新的商品分类id</param>
        /// <param name="layer">要更新的层</param>
        /// <param name="parentidlist">要更新的父id列表</param>
        /// <param name="haschild">要更新的haschild数据(是否有子分类)</param>
        public void UpdateCategoriesInfo(int categoryid, int layer, string parentidlist, int haschild)
        {
            DbParameter[] parms = 
			{
				DbHelper.MakeInParam("@layer", (DbType)SqlDbType.SmallInt, 2, layer),
				DbHelper.MakeInParam("@parentidlist", (DbType)SqlDbType.Char, 300, parentidlist),
                DbHelper.MakeInParam("@haschild", (DbType)SqlDbType.Bit, 1, haschild),
				DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4, categoryid)
			};
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "goodscategories] SET [layer]=@layer, [parentidlist]=@parentidlist, [haschild]=@haschild WHERE [categoryid]=@categoryid", parms);
        }

        /// <summary>
        /// 返回商品所在地数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetLocationsTable()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "locations] ORDER BY [lid] ASC").Tables[0];
        }

        /// <summary>
        /// 返回商品所在地的Sql语句
        /// </summary>
        /// <returns></returns>
        public string GetLocationsTableSql()
        {
            return "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "locations] ORDER BY [lid] ASC";
        }

        /// <summary>
        /// 返回省(州)的数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetStatesTable()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT DISTINCT [state] FROM  [" + BaseConfigs.GetTablePrefix + "locations] ORDER BY [state]").Tables[0];
        }

        /// <summary>
        /// 返回国家的数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetCountriesTable()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT DISTINCT [country] FROM  [" + BaseConfigs.GetTablePrefix + "locations] ORDER BY [country]").Tables[0];
        }

        /// <summary>
        /// 获取指定商品分类的根(root)结点分类
        /// </summary>
        /// <param name="categoryid">商品分类id</param>
        /// <returns></returns>
        public DataTable GetRootCategoryID(int categoryid)
        {
            DbParameter[] parms = 
			{
				DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4, categoryid)
			};
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT TOP 1 * FROM  [" + BaseConfigs.GetTablePrefix + "goodscategories] WHERE [categoryid]=@categoryid", parms).Tables[0];
        }

        /// <summary>
        /// 通过指定的LID获取商品所有地信息
        /// </summary>
        /// <param name="lid">所在地的lid</param>
        /// <returns></returns>
        public DataTable GetLocusByLID(int lid)
        {
            DbParameter[] parms = 
			{
				DbHelper.MakeInParam("@lid", (DbType)SqlDbType.Int, 4, lid)
			};
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT TOP 1 * FROM  [" + BaseConfigs.GetTablePrefix + "locations] WHERE [lid]=@lid", parms).Tables[0];
        }



        /// <summary>
        /// 产生商品附件
        /// </summary>
        /// <param name="attachmentinfo">商品附件描述类实体</param>
        /// <returns>商品附件id</returns>
        public int CreateGoodsAttachment(Goodsattachmentinfo __goodsattachments)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,__goodsattachments.Uid),
						DbHelper.MakeInParam("@goodsid", (DbType)SqlDbType.Int, 4,__goodsattachments.Goodsid),
						DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4,__goodsattachments.Categoryid),
						DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8,DateTime.Parse(__goodsattachments.Postdatetime)),
                        DbHelper.MakeInParam("@readperm", (DbType)SqlDbType.Int, 4,__goodsattachments.Readperm),
						DbHelper.MakeInParam("@filename", (DbType)SqlDbType.NChar, 100,__goodsattachments.Filename),
						DbHelper.MakeInParam("@description", (DbType)SqlDbType.NChar, 100,__goodsattachments.Description),
						DbHelper.MakeInParam("@filetype", (DbType)SqlDbType.NChar, 50,__goodsattachments.Filetype),
						DbHelper.MakeInParam("@filesize", (DbType)SqlDbType.Int, 4,__goodsattachments.Filesize),
						DbHelper.MakeInParam("@attachment", (DbType)SqlDbType.NChar, 100,__goodsattachments.Attachment)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "goodsattachments] ([uid], [goodsid], [categoryid], [postdatetime], [readperm], [filename], [description], [filetype], [filesize], [attachment]) VALUES (@uid, @goodsid, @categoryid, @postdatetime, @readperm, @filename, @description, @filetype, @filesize, @attachment);SELECT SCOPE_IDENTITY()  AS aid");
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }

        /// <summary>
        /// 更新商品附件信息
        /// </summary>
        /// <param name="attachmentinfo">商品附件描述类实体</param>
        /// <returns></returns>
        public bool SaveGoodsAttachment(Goodsattachmentinfo __goodsattachments)
        {

            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,__goodsattachments.Uid),
						DbHelper.MakeInParam("@goodsid", (DbType)SqlDbType.Int, 4,__goodsattachments.Goodsid),
						DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4,__goodsattachments.Categoryid),
						DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8,DateTime.Parse(__goodsattachments.Postdatetime)),
                        DbHelper.MakeInParam("@readperm", (DbType)SqlDbType.Int, 4,__goodsattachments.Readperm),
						DbHelper.MakeInParam("@filename", (DbType)SqlDbType.NChar, 100,__goodsattachments.Filename),
						DbHelper.MakeInParam("@description", (DbType)SqlDbType.NChar, 100,__goodsattachments.Description),
						DbHelper.MakeInParam("@filetype", (DbType)SqlDbType.NChar, 50,__goodsattachments.Filetype),
						DbHelper.MakeInParam("@filesize", (DbType)SqlDbType.Int, 4,__goodsattachments.Filesize),
						DbHelper.MakeInParam("@attachment", (DbType)SqlDbType.NChar, 100,__goodsattachments.Attachment),
                    	DbHelper.MakeInParam("@aid", (DbType)SqlDbType.Int, 4,__goodsattachments.Aid)
				};
            string sqlstring = String.Format("Update [" + BaseConfigs.GetTablePrefix + "goodsattachments]  Set [uid] = @uid, [goodsid] = @goodsid, [categoryid] = @categoryid, [postdatetime] = @postdatetime, [readperm] = @readperm, [filename] = @filename, [description] = @description, [filetype] = @filetype, [filesize] = @filesize, [attachment] = @attachment  WHERE [aid] = @aid");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
        }

        /// <summary>
        /// 更新商品附件信息
        /// </summary>
        /// <param name="aid">附件id</param>
        /// <param name="readperm">读取权限</param>
        /// <param name="description">附件描述</param>
        /// <returns></returns>
        public bool SaveGoodsAttachment(int aid, int readperm, string description)
        {

            DbParameter[] parms = 
				{
                        DbHelper.MakeInParam("@readperm", (DbType)SqlDbType.Int, 4,readperm),
						DbHelper.MakeInParam("@description", (DbType)SqlDbType.NChar, 100,description),
                    	DbHelper.MakeInParam("@aid", (DbType)SqlDbType.Int, 4,aid)
				};

            DbHelper.ExecuteNonQuery(CommandType.Text, String.Format("Update [" + BaseConfigs.GetTablePrefix + "goodsattachments]  Set [readperm] = @readperm, [description] = @description  WHERE [aid] = @aid"), parms);

            return true;
        }


        /// <summary>
        /// 删除指定附件
        /// </summary>
        /// <param name="aid">附件id</param>
        /// <returns></returns>
        public int DeleteGoodsAttachment(int aid)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int,4,aid)
								   };

            return DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}goodsattachments] WHERE [aid]=@aid", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 批量删除附件
        /// </summary>
        /// <param name="aidList">附件Id列表，以英文逗号分割</param>
        /// <returns></returns>
        public int DeleteGoodsAttachment(string aidList)
        {
            return DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}goodsattachments] WHERE [aid] IN ({1})", BaseConfigs.GetTablePrefix, aidList));
        }

        /// <summary>
        /// 获取指定商品的所有附件信息
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <returns></returns>
        public IDataReader GetGoodsAttachmentsByGoodsid(int goodsid)
        {
            DbParameter parm = DbHelper.MakeInParam("@goodsid", (DbType)SqlDbType.Int, 4, goodsid);

            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM [{0}goodsattachments] WHERE [goodsid] = @goodsid ORDER BY [aid] ASC", BaseConfigs.GetTablePrefix), parm);
        }

        /// <summary>
        /// 获取指定附件ID的信息
        /// </summary>
        /// <param name="aid">附件id</param>
        /// <returns></returns>
        public IDataReader GetGoodsAttachmentsByAid(int aid)
        {
            DbParameter parm = DbHelper.MakeInParam("@aid", (DbType)SqlDbType.Int, 4, aid);

            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}goodsattachments] WHERE [aid] = @aid ", BaseConfigs.GetTablePrefix), parm);
        }

        /// <summary>
        /// 获取指定aid列表(","分割)附件
        /// </summary>
        /// <param name="aidList">aid列表(","分割)</param>
        /// <returns></returns>
        public IDataReader GetGoodsAttachmentListByAidList(string aidList)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM [{0}goodsattachments] WHERE [aid] IN ({1})", BaseConfigs.GetTablePrefix, aidList));
        }

        /// <summary>
        /// 获取商品所包含的Tag
        /// </summary>
        /// <param name="goodsid">商品Id</param>
        /// <returns></returns>
        public IDataReader GetTagsListByGoods(int goodsid)
        {
            string sql = string.Format("SELECT [{0}tags].* FROM [{0}tags], [{0}goodstags] WHERE [{0}goodstags].[tagid] = [{0}tags].[tagid] AND [{0}goodstags].[goodsid] = @goodsid ORDER BY [orderid]", BaseConfigs.GetTablePrefix);

            DbParameter parm = DbHelper.MakeInParam("@goodsid", (DbType)SqlDbType.Int, 4, goodsid);

            return DbHelper.ExecuteReader(CommandType.Text, sql, parm);
        }

        /// <summary>
        /// 创建商品标签(已存在的标签不会被创建)
        /// </summary>
        /// <param name="tags">标签, 以半角空格分隔</param>
        /// <param name="goodsid">商品Id</param>
        /// <param name="userid">用户Id</param>
        /// <param name="curdatetime">提交时间</param>
        public void CreateGoodsTags(string tags, int goodsid, int userid, string curdatetime)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@tags", (DbType)SqlDbType.NVarChar, 55, tags),
                DbHelper.MakeInParam("@goodsid", (DbType)SqlDbType.Int, 4, goodsid),
                DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid),
                DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8, curdatetime)                
            };

            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}creategoodstags", BaseConfigs.GetTablePrefix), parms);
        }


        /// <summary>
        /// 设置指定商品分类id的路径信息
        /// </summary>
        /// <param name="pathlist">路径信息</param>
        /// <param name="categoryid">指定商品分类id</param>
        public void SetGoodsCategoryPathList(string pathlist, int categoryid)
        {
            DbParameter[] parms = 
            {
			    DbHelper.MakeInParam("@pathlist", (DbType)SqlDbType.VarChar, 3000, pathlist),
                DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4, categoryid)
		    };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "goodscategories] SET pathlist=@pathlist  WHERE [categoryid]=@categoryid", parms);
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="goodsid">商品Id</param>
        public IDataReader GetGoodsInfo(int goodsid)
        {
            DbParameter parm = DbHelper.MakeInParam("@goodsid", (DbType)SqlDbType.Int, 4, goodsid);

            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}goods] WHERE [goodsid] = @goodsid ", BaseConfigs.GetTablePrefix), parm);
        }

        /// <summary>
        /// 更新指定商品分类下的商品数
        /// </summary>
        /// <param name="categoryid">指定的商品分类</param>
        /// <param name="parentidlist">指定分类的父分类列表</param>
        /// <param name="goodscount">要更新的商品数</param>
        public void UpdateCategoryGoodsCounts(int categoryid, string parentidlist, int goodscount)
        {
            string sql = string.Format("UPDATE [{0}goodscategories] SET [goodscount] = [goodscount] + @goodscount  WHERE  (categoryid = " + categoryid, BaseConfigs.GetTablePrefix);

           
            if (!Utils.StrIsNullOrEmpty(parentidlist) && Utils.IsNumericArray(parentidlist.Split(',')))
            {
                sql += "  OR [categoryid] IN (" + parentidlist.Trim() + ")";
            }

            sql += ")";

            if (goodscount < 0)
            {   //添加条件判断,以免出现负数情况             
                sql += "  AND [goodscount] >= @goodscount ";
            }

            DbParameter[] parms = {
                DbHelper.MakeInParam("@goodscount", (DbType)SqlDbType.Int, 4, goodscount),
                DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4, categoryid)
            };
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        /// <summary>
        /// 获得指定分类的商品列表
        /// </summary>
        /// <param name="categoryid">指定分类</param>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="ascdesc">排序方式(0:升序 1:降序)</param>
        /// <returns></returns>
        public IDataReader GetGoodsList(int categoryid, int pagesize, int pageindex, string condition, string orderby, int ascdesc)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@categoryid",(DbType)SqlDbType.Int,4,categoryid),
									   DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int,4,pagesize),
									   DbHelper.MakeInParam("@pageindex", (DbType)SqlDbType.Int,4,pageindex),
									   DbHelper.MakeInParam("@condition", (DbType)SqlDbType.VarChar,500, condition),
                                       DbHelper.MakeInParam("@orderby", (DbType)SqlDbType.VarChar,100, orderby),
                                       DbHelper.MakeInParam("@ascdesc",(DbType)SqlDbType.Int,4,ascdesc)
								   };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, BaseConfigs.GetTablePrefix + "getgoodslistbycid", parms);
        }

        /// <summary>
        /// 获得指定卖家的商品列表
        /// </summary>
        /// <param name="selleruid">卖家uid</param>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="ascdesc">排序方式(0:升序 1:降序)</param>
        /// <returns></returns>
        public IDataReader GetGoodsListBySellerUID(int selleruid, int pagesize, int pageindex, string condition, string orderby, int ascdesc)
        {
            string sorttype = "";
            string sql = "";
            if (ascdesc == 0)
            {
                sorttype = "ASC";
            }
            else
            {
                sorttype = "DESC";
            }

            if (pageindex <= 1)
            {
                sql = "SELECT TOP " + pagesize + " * FROM [{0}goods] WHERE [selleruid] = " + selleruid + condition + " ORDER BY [" + orderby + "] " + sorttype + " , [goodsid] DESC";
            }
            else if (sorttype == "DESC")
            {
                sql = "SELECT TOP " + pagesize + " * FROM [{0}goods] WHERE [goodsid] < (SELECT MIN([goodsid])  FROM (SELECT TOP " + ((pageindex - 1) * pagesize) + " [goodsid] FROM [{0}goods]  WHERE  [selleruid] = " + selleruid + condition + " ORDER BY [" + orderby + "] " + sorttype + ", [goodsid] DESC) AS tblTmp ) AND [selleruid] = " + selleruid + condition + " ORDER BY [" + orderby + "] " + sorttype + ", [goodsid] DESC";
            }
            else
            {
                sql = "SELECT TOP " + pagesize + " * FROM [{0}goods] WHERE [goodsid] > (SELECT MAX([goodsid])  FROM (SELECT TOP " + ((pageindex - 1) * pagesize) + " [goodsid] FROM [{0}goods]  WHERE  [selleruid] = " + selleruid + condition + " ORDER BY " + orderby + " " + sorttype + ") AS tblTmp ) AND [selleruid] = " + selleruid + condition + " ORDER BY " + orderby + " " + sorttype;
            }


            return DbHelper.ExecuteReader(CommandType.Text, string.Format(sql, BaseConfigs.GetTablePrefix));
        }

        /// <summary>
        /// 获取指定条件的商品信息
        /// </summary>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="ascdesc">排序方式</param>
        /// <returns></returns>
        public IDataReader GetGoodsList(int pagesize, int pageindex, string condition, string orderby, int ascdesc)
        {
            string sorttype = "";
            string sql = "";
            if (ascdesc == 0)
            {
                sorttype = "ASC";
            }
            else
            {
                sorttype = "DESC";
            }

            condition = condition.Trim().ToUpper().StartsWith("AND") ? condition.Substring(condition.IndexOf("AND") + 3, condition.Length - condition.IndexOf("AND") - 3) : condition;

            if (pageindex <= 1)
            {
                sql = "SELECT TOP " + pagesize + " * FROM [{0}goods] WHERE " + condition + " ORDER BY [" + orderby + "] " + sorttype;
            }
            else if (sorttype == "DESC")
            {
                sql = "SELECT TOP " + pagesize + " * FROM [{0}goods] WHERE [goodsid] < (SELECT MIN([goodsid])  FROM (SELECT TOP " + ((pageindex - 1) * pagesize) + " [goodsid] FROM [{0}goods]  WHERE  " + condition + " ORDER BY [" + orderby + "] " + sorttype + ") AS tblTmp ) AND " + condition + " ORDER BY [" + orderby + "] " + sorttype;
            }
            else
            {
                sql = "SELECT TOP " + pagesize + " * FROM [{0}goods] WHERE [goodsid] > (SELECT MAX([goodsid])  FROM (SELECT TOP " + ((pageindex - 1) * pagesize) + " [goodsid] FROM [{0}goods]  WHERE  " + condition + " ORDER BY [" + orderby + "] " + sorttype + ") AS tblTmp ) AND " + condition + " ORDER BY [" + orderby + "] " + sorttype;
            }
            return DbHelper.ExecuteReader(CommandType.Text, string.Format(sql, BaseConfigs.GetTablePrefix));
        }

        /// <summary>
        /// 获取指定条件的商品信息数
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public int GetGoodsCount(string condition)
        {
            condition = condition.Trim().ToUpper().StartsWith("AND") ? condition.Substring(condition.IndexOf("AND") + 3, condition.Length - condition.IndexOf("AND") - 3) : condition;
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(goodsid) FROM [{0}goods] WHERE " + condition, BaseConfigs.GetTablePrefix)).ToString(), 0);
        }

        /// <summary>
        /// 获得指定卖家的商品数
        /// </summary>
        /// <param name="selleruid">卖家uid</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public int GetGoodsCountBySellerUid(int selleruid, string condition)
        {
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(goodsid) FROM [{0}goods] WHERE [selleruid] = " + selleruid + condition, BaseConfigs.GetTablePrefix)).ToString(), 0);
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
        /// <returns></returns>
        public IDataReader GetGoodsInfoListByShopCategory(int shopcategoryid, int pagesize, int pageindex, string condition, string orderby, int ascdesc)
        {
            string sorttype = "";
            string sql = "";
            if (ascdesc == 0)
            {
                sorttype = "ASC";
            }
            else
            {
                sorttype = "DESC";
            }

            if (pageindex <= 1)
            {
                sql = "SELECT TOP " + pagesize + " * FROM [{0}goods] WHERE CHARINDEX('," + shopcategoryid + ",', RTRIM([shopcategorylist])) > 0 " + condition + " ORDER BY [" + orderby + "] " + sorttype + " , [goodsid] DESC";
            }
            else if (sorttype == "DESC")
            {
                sql = "SELECT TOP " + pagesize + " * FROM [{0}goods] WHERE [goodsid] < (SELECT MIN([goodsid])  FROM (SELECT TOP " + ((pageindex - 1) * pagesize) + " [goodsid] FROM [{0}goods]  WHERE  CHARINDEX('," + shopcategoryid + ",', RTRIM([shopcategorylist])) > 0 " + condition + " ORDER BY [" + orderby + "] " + sorttype + ", [goodsid] DESC) AS tblTmp ) AND CHARINDEX('," + shopcategoryid + ",', RTRIM([shopcategorylist])) > 0 " + condition + " ORDER BY [" + orderby + "] " + sorttype + ", [goodsid] DESC";
            }
            else
            {
                sql = "SELECT TOP " + pagesize + " * FROM [{0}goods] WHERE [goodsid] > (SELECT MAX([goodsid])  FROM (SELECT TOP " + ((pageindex - 1) * pagesize) + " [goodsid] FROM [{0}goods]  WHERE  CHARINDEX('," + shopcategoryid + ",', RTRIM([shopcategorylist])) > 0 " + condition + " ORDER BY " + orderby + " " + sorttype + ") AS tblTmp ) AND CHARINDEX('," + shopcategoryid + ",', RTRIM([shopcategorylist])) > 0 " + condition + " ORDER BY " + orderby + " " + sorttype;
            }


            return DbHelper.ExecuteReader(CommandType.Text, string.Format(sql, BaseConfigs.GetTablePrefix));
        }

        /// <summary>
        /// 获取指定店铺商品分类id的商品数
        /// </summary>
        /// <param name="shopcategoryid">店铺商品分类id</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public int GetGoodsCountByShopCategory(int shopcategoryid, string condition)
        {
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(goodsid) FROM [{0}goods] WHERE CHARINDEX('," + shopcategoryid + ",', RTRIM([shopcategorylist])) > 0 " + condition, BaseConfigs.GetTablePrefix)).ToString(), 0);
        }

        /// <summary>
        /// 根据操作码获取操作符
        /// </summary>
        /// <param name="opcode"></param>
        /// <returns></returns>
        public string GetOperaCode(int opcode)
        {
            switch (opcode)
            {
                case 1: return "=";
                case 2: return "<>";
                case 3: return ">";
                case 4: return ">=";
                case 5: return "<";
                case 6: return "<=";
                default: return "";
            }
        }


        /// <summary>
        /// 获取商品显示字段条件
        /// </summary>
        /// <param name="opcode">操作码</param>
        /// <param name="displayorder">显示信息</param>
        /// <returns>查询条件</returns>
        public string GetGoodsDisplayCondition(int opcode, int displayorder)
        {
            string condition = "";
            if (displayorder > -3 && displayorder <= 6)
            {
                condition = GetOperaCode(opcode);
                if (!Utils.StrIsNullOrEmpty(condition))
                {
                    condition = string.Format(" AND [displayorder] {0} {1} ", condition, displayorder);
                }
            }
            return condition;
        }

        /// <summary>
        /// 获取商品关闭字段条件
        /// </summary>
        /// <param name="opcode">操作码</param>
        /// <param name="closed">关闭信息</param>
        /// <returns>查询条件</returns>
        public string GetGoodsCloseCondition(int opcode, int closed)
        {
            string condition = "";
            if (closed == 0 || closed == 1)
            {
                condition = GetOperaCode(opcode);
                if (!Utils.StrIsNullOrEmpty(condition))
                {
                    condition = string.Format(" AND [closed] {0} {1} ", condition, closed);
                }
            }
            return condition;
        }

        /// <summary>
        /// 获取推荐商品字段条件
        /// </summary>
        /// <param name="opcode">操作码</param>
        /// <param name="recommend">推荐信息</param>
        /// <returns>查询条件</returns>
        public string GetGoodsRecommendCondition(int opcode, int recommend)
        {
            string condition = "";
            if (recommend == 0 || recommend == 1)
            {
                condition = GetOperaCode(opcode);
                if (!Utils.StrIsNullOrEmpty(condition))
                {
                    condition = string.Format(" AND [recommend] {0} {1} ", condition, recommend);
                }
            }
            return condition;
        }

        /// <summary>
        /// 获取指定用户商品字段条件
        /// </summary>
        /// <param name="opcode">操作码</param>
        /// <param name="selleruid">卖家信息</param>
        /// <returns>查询条件</returns>
        public string GetGoodsSellerUidCondition(int opcode, int selleruid)
        {
            string condition = "";
            if (selleruid > 0)
            {
                condition = GetOperaCode(opcode);
                if (!Utils.StrIsNullOrEmpty(condition))
                {
                    condition = string.Format(" AND [selleruid] {0} {1} ", condition, selleruid);
                }
            }
            return condition;
        }

        /// <summary>
        /// 获取商品Id字段条件
        /// </summary>
        /// <param name="opcode">操作码</param>
        /// <param name="goodsid">商品id</param>
        /// <returns>查询条件</returns>
        public string GetGoodsIdCondition(int opcode, int goodsid)
        {
            string condition = "";
            if (goodsid > 0)
            {
                condition = GetOperaCode(opcode);
                if (!Utils.StrIsNullOrEmpty(condition))
                {
                    condition = string.Format(" AND [goodsid] {0} {1} ", condition, goodsid);
                }
            }
            return condition;
        }

        /// <summary>
        /// 获取商品到期日期条件
        /// </summary>
        /// <param name="opcode">操作码</param>
        /// <param name="day">天数</param>
        /// <returns>查询条件</returns>
        public string GetGoodsExpirationCondition(int opcode, int day)
        {
            string condition = "";
            condition = GetOperaCode(opcode);
            if (!Utils.StrIsNullOrEmpty(condition))
            {
                condition = string.Format(" AND DATEDIFF(day, [expiration], getdate()) {0} {1} ", condition, day);
            }
            return condition;
        }

        /// <summary>
        /// 获取商品价格字段条件
        /// </summary>
        /// <param name="opcode">操作码</param>
        /// <param name="price">价格</param>
        /// <returns>查询条件</returns>
        public string GetGoodsPriceCondition(int opcode, decimal price)
        {
            string condition = "";
            if (price > -3 && price <= 6)
            {
                condition = GetOperaCode(opcode);
                if (!Utils.StrIsNullOrEmpty(condition))
                {
                    condition = string.Format(" AND [price] {0} {1} ", condition, price);
                }
            }
            return condition;
        }

        /// <summary>
        /// 获取商品类型(全新,二手)字段条件
        /// </summary>
        /// <param name="opcode">操作码</param>
        /// <param name="quality">数量</param>
        /// <returns>查询条件</returns>
        public string GetGoodsQualityCondition(int opcode, int quality)
        {
            string condition = "";
            if (quality > -3 && quality <= 6)
            {
                condition = GetOperaCode(opcode);
                if (!Utils.StrIsNullOrEmpty(condition))
                {
                    condition = string.Format(" AND [quality] {0} {1} ", condition, quality);
                }
            }
            return condition;
        }

        /// <summary>
        /// 获取商品开始日期条件
        /// </summary>
        /// <param name="opcode">操作码</param>
        /// <param name="day">天数</param>
        /// <returns>查询条件</returns>
        public string GetGoodsDateLineCondition(int opcode, int day)
        {
            string condition = GetOperaCode(opcode);
            if (!Utils.StrIsNullOrEmpty(condition))
            {
                condition = string.Format(" AND DATEDIFF(day, [dateline], getdate()) {0} {1} ", condition, day);
            }
            return condition;
        }

        /// <summary>
        /// 获取剩余商品数条件
        /// </summary>
        /// <param name="opcode">操作码</param>
        /// <param name="amount">数量</param>
        /// <returns>查询条件</returns>
        public string GetGoodsRemainCondition(int opcode, int amount)
        {
            string condition = GetOperaCode(opcode);
            if (!Utils.StrIsNullOrEmpty(condition))
            {
                condition = string.Format(" AND [amount] {0} {1} ", condition, amount);
            }
            return condition;
        }

        /// <summary>
        /// 获得指定分类的商品数
        /// </summary>
        /// <param name="categoryid">指定分类</param>
        /// <param name="condition">条件</param>
        /// <returns>商品数</returns>
        public int GetGoodsCount(int categoryid, string condition)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@categoryid",(DbType)SqlDbType.Int,4,categoryid),
									   DbHelper.MakeInParam("@condition", (DbType)SqlDbType.VarChar,500,condition)									   
								   };
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.StoredProcedure, BaseConfigs.GetTablePrefix + "getgoodscountbycid", parms));
        }

        /// <summary>
        /// 获得指定商品分类下的子分类
        /// </summary>
        /// <param name="categoryid">指定分类</param>
        /// <returns>子分类数据对象</returns>
        public IDataReader GetSubGoodsCategories(int categoryid)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@categoryid",(DbType)SqlDbType.Int,4,categoryid)
								   };
            string sql = string.Format("SELECT * FROM [{0}goodscategories] WHERE [parentid] = @categoryid", BaseConfigs.GetTablePrefix);
            if (GeneralConfigs.GetConfig().Enablemall == 1) //当开启普通模式时
            {
                sql += " AND [fid]>0 ";
            }
            sql += " ORDER BY [displayorder] ";
            return DbHelper.ExecuteReader(CommandType.Text, sql, parms);
        }

        /// <summary>
        /// 获取指定层数的商品分类 
        /// </summary>
        /// <param name="layer">层数</param>
        /// <returns>商品分类信息</returns>
        public IDataReader GetGoodsCategoriesByLayer(int layer)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@layer",(DbType)SqlDbType.Int,4,layer)
								   };
            string sql = string.Format("SELECT * FROM [{0}goodscategories] WHERE [layer] <= @layer ORDER BY [displayorder]", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, sql, parms);
        }

        /// <summary>
        /// 判断商品列表是否都在当前分类下
        /// </summary>
        /// <param name="goodsidlist">商品Id列表，以英文逗号分割</param>
        /// <param name="categoryid">指定分类</param>
        /// <returns></returns>
        public bool InSameCategory(string goodsidlist, int categoryid)
        {
            return Utils.SplitString(goodsidlist, ",").Length == GetGoodsCount(categoryid, " AND [goodsid] IN (" + goodsidlist + ")");
        }


        /// <summary>
        /// 将商品设置关闭/打开
        /// </summary>
        /// <param name="goodsidlist">商品Id列表,以英文逗号分割</param>
        /// <param name="intValue">关闭/打开标志( 0 为打开,1 为关闭)</param>
        /// <returns>更新商品个数</returns>
        public int SetGoodsClose(string goodslist, short intValue)
        {
            DbParameter[] parms = {
				DbHelper.MakeInParam("@field", (DbType)SqlDbType.TinyInt, 1, intValue)
			};
            return DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "goods] SET [closed] = @field WHERE [goodsid] IN (" + goodslist + ") AND [closed] IN (0,1)", parms);
        }

        /// <summary>
        /// 删除指定的商品
        /// </summary>
        /// <param name="goodsidlist">商品Id列表(以","分割)</param>
        /// <returns></returns>
        public int DeleteGoods(string goodslist)
        {
            return DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE [" + BaseConfigs.GetTablePrefix + "goods] WHERE [goodsid] IN (" + goodslist + ")");
        }

        /// <summary>
        /// 删除指定的商品附件
        /// </summary>
        /// <param name="goodsidlist">要删除的商品Id列表(以","分割)</param>
        /// <returns></returns>
        public int DeleteGoodsAttachments(string goodslist)
        {
            return DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE [" + BaseConfigs.GetTablePrefix + "goodsattachments] WHERE [goodsid] IN (" + goodslist + ")");
        }

        /// <summary>
        /// 设置商品指定字段的属性值
        /// </summary>
        /// <param name="goodsidlist">要设置的商品Id列表(以","分割)</param>
        /// <param name="field">要设置的字段</param>
        /// <param name="intValue">属性值</param>
        /// <returns>更新主题个数</returns>
        public int SetGoodsStatus(string goodslist, string field, int intValue)
        {
            DbParameter[] parms = {
				DbHelper.MakeInParam("@field", (DbType)SqlDbType.Int, 1, intValue)
			};
            return DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}goods] SET [{1}] = @field WHERE [goodsid] IN ({2})", BaseConfigs.GetTablePrefix, field, goodslist), parms);
        }

        /// <summary>
        /// 设置商品指定字段的属性值
        /// </summary>
        /// <param name="goodsidlist">要设置的商品Id列表(以","分割)</param>
        /// <param name="field">要设置的字段</param>
        /// <param name="intValue">属性值</param>
        /// <returns>更新主题个数</returns>
        public int SetGoodsStatus(string goodslist, string field, string intValue)
        {
            DbParameter[] parms = {
				DbHelper.MakeInParam("@field", (DbType)SqlDbType.VarChar, 500, intValue)
			};
            return DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}goods] SET [{1}] = @field WHERE [goodsid] IN ({2})", BaseConfigs.GetTablePrefix, field, goodslist), parms);
        }

        /// <summary>
        /// 获得指定商品的所有附件
        /// </summary>
        /// <param name="goodsidlist">商品Id列表(以","分割)</param>
        /// <returns></returns>
        public IDataReader GetGoodsAttachmentList(string goodsidlist)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT [aid],[filename] FROM [{0}goodsattachments] WHERE [goodsid] IN ({1})", BaseConfigs.GetTablePrefix, goodsidlist));
        }

        /// <summary>
        /// 获得指定商品ID的商品列表
        /// </summary>
        /// <param name="goodsidlist">商品Id列表(以","分割)</param>
        /// <returns></returns>
        public DataTable GetGoodsList(string goodsidlist)
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "goods] WHERE [goodsid] IN (" + goodsidlist + ")").Tables[0];
        }

        /// <summary>
        /// 获取指定分类的最新商品列表
        /// </summary>
        /// <param name="categoryid">商品分类ID</param>
        /// <param name="topnumber">获取记录条数</param>
        /// <returns></returns>
        public IDataReader GetNewGoodsList(int categoryid, int topnumber)
        {
            if (topnumber > 0)
            {
                if (categoryid > 0)
                {
                    return GetGoodsList(categoryid, topnumber, 1, " AND [closed] = 0 AND [displayorder] >=0 ", "goodsid", 1);
                }
                else
                {
                    return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM [{0}goods] WHERE [closed] = 0 AND [displayorder] >=0 AND CHARINDEX('','" + categoryid + "','' , '',[parentcategorylist],'')>0) ORDER BY [goodsid] DESC", BaseConfigs.GetTablePrefix));
                }
            }
            return null;
        }

        /// <summary>
        /// 创建商品交易信息
        /// </summary>
        /// <param name="goodstradelog">要创建的交易信息</param>
        /// <returns></returns>
        public int CreateGoodsTradeLog(Goodstradeloginfo __goodstradelog)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@goodsid", (DbType)SqlDbType.Int, 4,__goodstradelog.Goodsid),
						DbHelper.MakeInParam("@orderid", (DbType)SqlDbType.VarChar, 50,__goodstradelog.Orderid),
						DbHelper.MakeInParam("@tradeno", (DbType)SqlDbType.VarChar, 50,__goodstradelog.Tradeno),
						DbHelper.MakeInParam("@subject", (DbType)SqlDbType.NChar, 60,__goodstradelog.Subject),
						DbHelper.MakeInParam("@price", (DbType)SqlDbType.Decimal, 18,__goodstradelog.Price),
						DbHelper.MakeInParam("@quality", (DbType)SqlDbType.TinyInt, 1,__goodstradelog.Quality),
						DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4,__goodstradelog.Categoryid),
						DbHelper.MakeInParam("@number", (DbType)SqlDbType.SmallInt, 2,__goodstradelog.Number),
						DbHelper.MakeInParam("@tax", (DbType)SqlDbType.Decimal, 18,__goodstradelog.Tax),
						DbHelper.MakeInParam("@locus", (DbType)SqlDbType.VarChar, 50,__goodstradelog.Locus),
						DbHelper.MakeInParam("@sellerid", (DbType)SqlDbType.Int, 4,__goodstradelog.Sellerid),
						DbHelper.MakeInParam("@seller", (DbType)SqlDbType.NChar, 20,__goodstradelog.Seller),
						DbHelper.MakeInParam("@selleraccount", (DbType)SqlDbType.VarChar, 50,__goodstradelog.Selleraccount),
						DbHelper.MakeInParam("@buyerid", (DbType)SqlDbType.Int, 4,__goodstradelog.Buyerid),
						DbHelper.MakeInParam("@buyer", (DbType)SqlDbType.NChar, 20,__goodstradelog.Buyer),
						DbHelper.MakeInParam("@buyercontact", (DbType)SqlDbType.NChar, 100,__goodstradelog.Buyercontact),
						DbHelper.MakeInParam("@buyercredit", (DbType)SqlDbType.SmallInt, 2,__goodstradelog.Buyercredit),
						DbHelper.MakeInParam("@buyermsg", (DbType)SqlDbType.NChar, 100,__goodstradelog.Buyermsg),
						DbHelper.MakeInParam("@status", (DbType)SqlDbType.TinyInt, 1,__goodstradelog.Status),
						DbHelper.MakeInParam("@lastupdate", (DbType)SqlDbType.DateTime, 8,__goodstradelog.Lastupdate),
						DbHelper.MakeInParam("@offline", (DbType)SqlDbType.TinyInt, 1,__goodstradelog.Offline),
						DbHelper.MakeInParam("@buyername", (DbType)SqlDbType.NChar, 20,__goodstradelog.Buyername),
						DbHelper.MakeInParam("@buyerzip", (DbType)SqlDbType.VarChar, 50,__goodstradelog.Buyerzip),
						DbHelper.MakeInParam("@buyerphone", (DbType)SqlDbType.VarChar, 50,__goodstradelog.Buyerphone),
						DbHelper.MakeInParam("@buyermobile", (DbType)SqlDbType.VarChar, 50,__goodstradelog.Buyermobile),
						DbHelper.MakeInParam("@transport", (DbType)SqlDbType.TinyInt, 1,__goodstradelog.Transport),
                        DbHelper.MakeInParam("@transportpay", (DbType)SqlDbType.TinyInt, 1,__goodstradelog.Transportpay),
						DbHelper.MakeInParam("@transportfee", (DbType)SqlDbType.Decimal, 18,__goodstradelog.Transportfee),
                        DbHelper.MakeInParam("@tradesum", (DbType)SqlDbType.Decimal, 18,__goodstradelog.Tradesum),
						DbHelper.MakeInParam("@baseprice", (DbType)SqlDbType.Decimal, 18,__goodstradelog.Baseprice),
						DbHelper.MakeInParam("@discount", (DbType)SqlDbType.TinyInt, 1,__goodstradelog.Discount),
						DbHelper.MakeInParam("@ratestatus", (DbType)SqlDbType.TinyInt, 1,__goodstradelog.Ratestatus),
						DbHelper.MakeInParam("@message", (DbType)SqlDbType.NText, 0,__goodstradelog.Message)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "goodstradelogs] ([goodsid], [orderid], [tradeno], [subject], [price], [quality], [categoryid], [number], [tax], [locus], [sellerid], [seller], [selleraccount], [buyerid], [buyer], [buyercontact], [buyercredit], [buyermsg], [status], [lastupdate], [offline], [buyername], [buyerzip], [buyerphone], [buyermobile], [transport], [transportpay], [transportfee], [tradesum], [baseprice], [discount], [ratestatus], [message]) VALUES (@goodsid, @orderid, @tradeno, @subject, @price, @quality, @categoryid, @number, @tax, @locus, @sellerid, @seller, @selleraccount, @buyerid, @buyer, @buyercontact, @buyercredit, @buyermsg, @status, @lastupdate, @offline, @buyername, @buyerzip, @buyerphone, @buyermobile, @transport, @transportpay, @transportfee, @tradesum, @baseprice, @discount, @ratestatus, @message);SELECT SCOPE_IDENTITY()  AS 'id'");

            return Utils.StrToInt(DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0].Rows[0][0].ToString(), -1);
        }

        /// <summary>
        /// 更新商品交易信息
        /// </summary>
        /// <param name="goodstradelog">要更新的交易信息</param>
        /// <returns></returns>
        public bool UpdateGoodsTradeLog(Goodstradeloginfo __goodstradelog)
        {

            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@goodsid", (DbType)SqlDbType.Int, 4,__goodstradelog.Goodsid),
						DbHelper.MakeInParam("@orderid", (DbType)SqlDbType.VarChar, 50,__goodstradelog.Orderid),
						DbHelper.MakeInParam("@tradeno", (DbType)SqlDbType.VarChar, 50,__goodstradelog.Tradeno),
						DbHelper.MakeInParam("@subject", (DbType)SqlDbType.NChar, 60,__goodstradelog.Subject),
						DbHelper.MakeInParam("@price", (DbType)SqlDbType.Decimal, 18,__goodstradelog.Price),
						DbHelper.MakeInParam("@quality", (DbType)SqlDbType.TinyInt, 1,__goodstradelog.Quality),
						DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4,__goodstradelog.Categoryid),
						DbHelper.MakeInParam("@number", (DbType)SqlDbType.SmallInt, 2,__goodstradelog.Number),
						DbHelper.MakeInParam("@tax", (DbType)SqlDbType.Decimal, 18,__goodstradelog.Tax),
						DbHelper.MakeInParam("@locus", (DbType)SqlDbType.VarChar, 50,__goodstradelog.Locus),
						DbHelper.MakeInParam("@sellerid", (DbType)SqlDbType.Int, 4,__goodstradelog.Sellerid),
						DbHelper.MakeInParam("@seller", (DbType)SqlDbType.NChar, 20,__goodstradelog.Seller),
						DbHelper.MakeInParam("@selleraccount", (DbType)SqlDbType.VarChar, 50,__goodstradelog.Selleraccount),
						DbHelper.MakeInParam("@buyerid", (DbType)SqlDbType.Int, 4,__goodstradelog.Buyerid),
						DbHelper.MakeInParam("@buyer", (DbType)SqlDbType.NChar, 20,__goodstradelog.Buyer),
						DbHelper.MakeInParam("@buyercontact", (DbType)SqlDbType.NChar, 100,__goodstradelog.Buyercontact),
						DbHelper.MakeInParam("@buyercredit", (DbType)SqlDbType.SmallInt, 2,__goodstradelog.Buyercredit),
						DbHelper.MakeInParam("@buyermsg", (DbType)SqlDbType.NChar, 100,__goodstradelog.Buyermsg),
						DbHelper.MakeInParam("@status", (DbType)SqlDbType.TinyInt, 1,__goodstradelog.Status),
						DbHelper.MakeInParam("@lastupdate", (DbType)SqlDbType.DateTime, 8,__goodstradelog.Lastupdate),
						DbHelper.MakeInParam("@offline", (DbType)SqlDbType.TinyInt, 1,__goodstradelog.Offline),
						DbHelper.MakeInParam("@buyername", (DbType)SqlDbType.NChar, 20,__goodstradelog.Buyername),
						DbHelper.MakeInParam("@buyerzip", (DbType)SqlDbType.VarChar, 50,__goodstradelog.Buyerzip),
						DbHelper.MakeInParam("@buyerphone", (DbType)SqlDbType.VarChar, 50,__goodstradelog.Buyerphone),
						DbHelper.MakeInParam("@buyermobile", (DbType)SqlDbType.VarChar, 50,__goodstradelog.Buyermobile),
						DbHelper.MakeInParam("@transport", (DbType)SqlDbType.TinyInt, 1,__goodstradelog.Transport),
                        DbHelper.MakeInParam("@transportpay", (DbType)SqlDbType.TinyInt, 1,__goodstradelog.Transportpay),
						DbHelper.MakeInParam("@transportfee", (DbType)SqlDbType.Decimal, 18,__goodstradelog.Transportfee),
                        DbHelper.MakeInParam("@tradesum", (DbType)SqlDbType.Decimal, 18,__goodstradelog.Tradesum),
						DbHelper.MakeInParam("@baseprice", (DbType)SqlDbType.Decimal, 18,__goodstradelog.Baseprice),
						DbHelper.MakeInParam("@discount", (DbType)SqlDbType.TinyInt, 1,__goodstradelog.Discount),
						DbHelper.MakeInParam("@ratestatus", (DbType)SqlDbType.TinyInt, 1,__goodstradelog.Ratestatus),
						DbHelper.MakeInParam("@message", (DbType)SqlDbType.NText, 0,__goodstradelog.Message),
                        DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4,__goodstradelog.Id)
				};
            string sqlstring = String.Format("Update [" + BaseConfigs.GetTablePrefix + "goodstradelogs]  Set [goodsid] = @goodsid, [orderid] = @orderid, [tradeno] = @tradeno, [subject] = @subject, [price] = @price, [quality] = @quality, [categoryid] = @categoryid, [number] = @number, [tax] = @tax, [locus] = @locus, [sellerid] = @sellerid, [seller] = @seller, [selleraccount] = @selleraccount, [buyerid] = @buyerid, [buyer] = @buyer, [buyercontact] = @buyercontact, [buyercredit] = @buyercredit, [buyermsg] = @buyermsg, [status] = @status, [lastupdate] = @lastupdate, [offline] = @offline, [buyername] = @buyername, [buyerzip] = @buyerzip, [buyerphone] = @buyerphone, [buyermobile] = @buyermobile, [transport] = @transport, [transportpay] = @transportpay, [transportfee] = @transportfee, [tradesum] = @tradesum, [baseprice] = @baseprice, [discount] = @discount, [ratestatus] = @ratestatus, [message] = @message WHERE [id] = @id");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
        }

        /// <summary>
        /// 设置商品交易状态条件
        /// </summary>
        /// <param name="opcode">操作码</param>
        /// <param name="status">交易状态</param>
        /// <returns>查询条件</returns>
        public string SetGoodsTradeStatusCond(int opcode, int status)
        {
            string condition = GetOperaCode(opcode);
            if (!Utils.StrIsNullOrEmpty(condition))
            {
                condition = string.Format("  AND [status] {0} {1} ", condition, status);
            }
            return condition;
        }

        /// <summary>
        /// 获取指定商品的交易日志
        /// </summary>
        /// <param name="goodsid">商品Id</param>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="ascdesc">排序方式(0:升序 1:降序)</param>
        /// <returns></returns>
        public IDataReader GetGoodsTradeLogByGid(int goodsid, int pagesize, int pageindex, string condition, string orderby, int ascdesc)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@goodsid",(DbType)SqlDbType.Int,4,goodsid)
								  };

            string sql = "";
            if (pageindex <= 1)
            {
                sql = "SELECT TOP {0} * FROM [{1}goodstradelogs] WHERE [goodsid] = @goodsid  {2} ORDER BY  [id] {4}, {3} {4}";
            }
            else
            {
                if (ascdesc == 1)
                {
                    sql = "SELECT TOP {0} * FROM [{1}goodstradelogs] WHERE [id] < (SELECT MIN([id])  FROM (SELECT TOP " + ((pageindex - 1) * pagesize) + " [id] FROM [{1}goodstradelogs]  WHERE  [goodsid] = @goodsid  {2}  ORDER BY [id] {4},{3} {4}) AS tblTmp ) AND [goodsid] = @goodsid  {2}  ORDER BY [id] {4},{3} {4}";
                }
                else
                {
                    sql = "SELECT TOP {0} * FROM [{1}goodstradelogs] WHERE [id] > (SELECT MAX([id])  FROM (SELECT TOP " + ((pageindex - 1) * pagesize) + " [id] FROM [{1}goodstradelogs]  WHERE  [goodsid] = @goodsid  {2}  ORDER BY [id] {4},{3} {4}) AS tblTmp ) AND [goodsid] = @goodsid  {2}  ORDER BY [id] {4},{3} {4}";
                }
            }

            return DbHelper.ExecuteReader(CommandType.Text, string.Format(sql, pagesize, BaseConfigs.GetTablePrefix, condition, orderby, ascdesc == 0 ? "ASC" : "DESC"), parms);
        }

        /// <summary>
        /// 获取指定用户(或商品id，过滤filter)的交易信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="goodsidlist">商品id串(格式:1,2,3)</param>
        /// <param name="uidtype">用户类型(1卖家, 2买家)</param>
        /// <param name="filter">过滤条件</param>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页</param>
        /// <returns></returns>
        public DataTable GetGoodsTradeLogList(int userid, string goodsidlist, int uidtype, string fileter, int pagesize, int pageindex)
        {
            string sql = "";
            string condition = " [buyerid] = " + userid;
            if (uidtype == 1) //卖家
            {
                condition = " [sellerid] = " + userid;
            }

            if (!Utils.StrIsNullOrEmpty(goodsidlist) && Utils.IsNumericArray(goodsidlist.Split(',')))
            {
                condition += " AND [" + BaseConfigs.GetTablePrefix + "goodstradelogs].[goodsid] IN (" + goodsidlist + ")";
            }

            condition += GetTradeStatus(fileter);

            condition += " ORDER BY [" + BaseConfigs.GetTablePrefix + "goodstradelogs].[id] DESC";
            if (pageindex <= 1)
            {
                sql = "SELECT TOP {0} * FROM [{1}goodstradelogs] LEFT JOIN [{1}goods] ON [{1}goodstradelogs].[goodsid] = [{1}goods].[goodsid] WHERE {2}";
            }
            else
            {
                sql = "SELECT TOP {0} * FROM [{1}goodstradelogs] LEFT JOIN [{1}goods] ON [{1}goodstradelogs].[goodsid] = [{1}goods].[goodsid] WHERE [id] < (SELECT MIN([id])  FROM (SELECT TOP " + ((pageindex - 1) * pagesize) + " [id] FROM [{1}goodstradelogs]  WHERE  {2}) AS tblTmp ) AND {2}";
            }
            return DbHelper.ExecuteDataset(CommandType.Text, string.Format(sql, pagesize, BaseConfigs.GetTablePrefix, condition)).Tables[0];
        }

        /// <summary>
        /// 将过滤条件转换成为查询条件
        /// </summary>
        /// <param name="filter">过滤参数</param>
        /// <returns>查询过滤条件</returns>
        public string GetTradeStatus(string filter)
        {
            switch (filter)
            {
                case "attention": return " AND [status] IN (1,2,3,5,6,10,11,12,13) "; //关注的交易
                case "eccredit": return " AND [status] IN (7,17) "; //评价的交易
                case "success": return " AND [status] = 7 "; //成功的交易
                case "refund": return " AND [status] IN (10,16,17,18) "; //退款的交易
                case "closed": return " AND [status] IN (8,17,18) "; //失败的交易
                case "unstart": return " AND [status] = 0 "; //未生效的交易
                case "all": return ""; // 全部交易
                default: return " AND [status] IN (1,2,3,4,5,6,10,11,12,13) "; //进行中的交易
            }
        }

        /// <summary>
        /// 获取指定用户的商品交易统计数据
        /// </summary>
        /// <returns></returns>
        public IDataReader GetTradeStatistic(int userid)
        {

            string sql = string.Format("SELECT (SELECT COUNT(id) FROM [{0}goodstradelogs] WHERE [sellerid] = {1} AND [status] IN (1,2,3,5,6,10,11,12,13)) AS SellerAttention," + //卖家关注交易数
                                              "(SELECT COUNT(id) FROM [{0}goodstradelogs] WHERE [sellerid] = {1} AND [status] IN (1,2,3,4,5,6,10,11,12,13)) AS SellerTrading," + //卖家交易进行中的交易数
                                              "(SELECT COUNT(id) FROM [{0}goodstradelogs] WHERE [sellerid] = {1} AND [ratestatus] IN (0,2) AND [status] IN (7,17)) AS SellerRate," + //需卖家评价的交易数
                                              "ISNULL((SELECT SUM(number) FROM [{0}goodstradelogs] WHERE [sellerid] = {1} AND [status]=7),0)  AS SellNumberSum," + //卖家售出商品总数
                                              "ISNULL((SELECT SUM(tradesum) FROM [{0}goodstradelogs] WHERE [sellerid] = {1} AND [status]=7),0)  AS SellTradeSum," + //卖家销售成交总额

                                              "(SELECT COUNT(id) FROM [{0}goodstradelogs] WHERE [buyerid] = {1} AND [status] IN (1,2,3,5,6,10,11,12,13)) AS BuyERAttention," + //买家关注交易数
                                              "(SELECT COUNT(id) FROM [{0}goodstradelogs] WHERE [buyerid] = {1} AND [status] IN (1,2,3,4,5,6,10,11,12,13)) AS BuyerTrading," + //买家交易进行中的交易数
                                              "(SELECT COUNT(id) FROM [{0}goodstradelogs] WHERE [buyerid] = {1} AND [ratestatus] IN (0,2) AND [status] IN (7,17)) AS BuyerRate," +  //需买家评价的交易数
                                              "ISNULL((SELECT SUM(number) FROM [{0}goodstradelogs] WHERE [buyerid] = {1} AND [status]=7),0)  AS BuyNumberSum," + //买入商品总数
                                              "ISNULL((SELECT SUM(tradesum) FROM [{0}goodstradelogs] WHERE [buyerid] = {1} AND [status]=7),0)  AS BuyTradeSum",  //买入成交总额
                                              BaseConfigs.GetTablePrefix,
                                              userid);
            return DbHelper.ExecuteReader(CommandType.Text, sql);
        }

        /// <summary>
        /// 获取指定用户(或商品id列表或过滤filter)的交易信息数
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="goodsidlist">商品id串(格式:1,2,3)</param>
        /// <param name="uidtype">用户类型(1卖家, 2买家)</param>
        /// <param name="fileter">过滤条件</param>
        /// <returns></returns>
        public int GetGoodsTradeLogCount(int userid, string goodsidlist, int uidtype, string fileter)
        {
            string condition = " [buyerid] = " + userid;
            if (uidtype == 1) //卖家
            {
                condition = " [sellerid] = " + userid;
            }

            if (!Utils.StrIsNullOrEmpty(goodsidlist) && Utils.IsNumericArray(goodsidlist.Split(',')))
            {
                condition += " AND [goodsid] IN (" + goodsidlist + ")";
            }

            if (!Utils.StrIsNullOrEmpty(fileter))
            {
                condition += GetTradeStatus(fileter);
            }

            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT count(id) FROM [{0}goodstradelogs] WHERE {1}", BaseConfigs.GetTablePrefix, condition)));
        }

        /// <summary>
        /// 获取指定商品的交易日志数
        /// </summary>
        /// <param name="goodsid">商品Id</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int GetTradeLogCountByGid(int goodsid, string condition)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@goodsid",(DbType)SqlDbType.Int,4,goodsid),
								  };

            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT count(id) FROM [{0}goodstradelogs] WHERE [goodsid] = @goodsid {1}", BaseConfigs.GetTablePrefix, condition), parms));
        }

        /// <summary>
        /// 获得指定交易日志ID的商品交易日志
        /// </summary>
        /// <param name="goodstradelogid">交易日志ID</param>
        /// <returns></returns>
        public IDataReader GetGoodsTradeLogByID(int goodstradelogid)
        {
            DbParameter parm = DbHelper.MakeInParam("@goodstradelogid", (DbType)SqlDbType.Int, 4, goodstradelogid);

            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}goodstradelogs] WHERE [id] = @goodstradelogid", BaseConfigs.GetTablePrefix), parm);
        }

        /// <summary>
        /// 获得指定交易号的商品交易日志
        /// </summary>
        /// <param name="tradeno"></param>
        /// <returns></returns>
        public IDataReader GetGoodsTradeLogByTradeNo(string tradeno)
        {
            DbParameter parm = DbHelper.MakeInParam("@tradeno", (DbType)SqlDbType.VarChar, 50, tradeno);

            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}goodstradelogs] WHERE [tradeno] = @tradeno", BaseConfigs.GetTablePrefix), parm);
        }

        /// <summary>
        /// 创建留言
        /// </summary>
        /// <param name="goodsleaveword">要创建的留言信息</param>
        /// <returns></returns>
        public int CreateGoodsLeaveWord(Goodsleavewordinfo __goodsleaveword)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@goodsid", (DbType)SqlDbType.Int, 4,__goodsleaveword.Goodsid),
						DbHelper.MakeInParam("@tradelogid", (DbType)SqlDbType.Int, 4,__goodsleaveword.Tradelogid),
						DbHelper.MakeInParam("@isbuyer", (DbType)SqlDbType.TinyInt, 1,__goodsleaveword.Isbuyer),
						DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,__goodsleaveword.Uid),
						DbHelper.MakeInParam("@username", (DbType)SqlDbType.NChar, 20,__goodsleaveword.Username),
						DbHelper.MakeInParam("@message", (DbType)SqlDbType.NChar, 200,__goodsleaveword.Message),
						DbHelper.MakeInParam("@invisible", (DbType)SqlDbType.Int, 4,__goodsleaveword.Invisible),
						DbHelper.MakeInParam("@ip", (DbType)SqlDbType.NVarChar, 15,__goodsleaveword.Ip),
						DbHelper.MakeInParam("@usesig", (DbType)SqlDbType.Int, 4,__goodsleaveword.Usesig),
						DbHelper.MakeInParam("@htmlon", (DbType)SqlDbType.Int, 4,__goodsleaveword.Htmlon),
						DbHelper.MakeInParam("@smileyoff", (DbType)SqlDbType.Int, 4,__goodsleaveword.Smileyoff),
						DbHelper.MakeInParam("@parseurloff", (DbType)SqlDbType.Int, 4,__goodsleaveword.Parseurloff),
						DbHelper.MakeInParam("@bbcodeoff", (DbType)SqlDbType.Int, 4,__goodsleaveword.Bbcodeoff),
						DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8,__goodsleaveword.Postdatetime)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "goodsleavewords] ([goodsid], [tradelogid], [isbuyer], [uid], [username], [message], [invisible], [ip], [usesig], [htmlon], [smileyoff], [parseurloff], [bbcodeoff], [postdatetime]) VALUES (@goodsid, @tradelogid, @isbuyer, @uid, @username, @message, @invisible, @ip, @usesig, @htmlon, @smileyoff, @parseurloff, @bbcodeoff, @postdatetime);SELECT SCOPE_IDENTITY()  AS 'id'");

            return Utils.StrToInt(DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0].Rows[0][0].ToString(), -1);
        }

        /// <summary>
        /// 更新留言
        /// </summary>
        /// <param name="goodsleaveword">要更新的留言信息</param>
        /// <returns></returns>
        public bool UpdateGoodsLeaveWord(Goodsleavewordinfo __goodsleaveword)
        {

            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@goodsid", (DbType)SqlDbType.Int, 4,__goodsleaveword.Goodsid),
						DbHelper.MakeInParam("@tradelogid", (DbType)SqlDbType.Int, 4,__goodsleaveword.Tradelogid),
						DbHelper.MakeInParam("@isbuyer", (DbType)SqlDbType.TinyInt, 1,__goodsleaveword.Isbuyer),
						DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,__goodsleaveword.Uid),
						DbHelper.MakeInParam("@username", (DbType)SqlDbType.NChar, 20,__goodsleaveword.Username),
						DbHelper.MakeInParam("@message", (DbType)SqlDbType.NChar, 200,__goodsleaveword.Message),
						DbHelper.MakeInParam("@invisible", (DbType)SqlDbType.Int, 4,__goodsleaveword.Invisible),
						DbHelper.MakeInParam("@ip", (DbType)SqlDbType.NVarChar, 15,__goodsleaveword.Ip),
						DbHelper.MakeInParam("@usesig", (DbType)SqlDbType.Int, 4,__goodsleaveword.Usesig),
						DbHelper.MakeInParam("@htmlon", (DbType)SqlDbType.Int, 4,__goodsleaveword.Htmlon),
						DbHelper.MakeInParam("@smileyoff", (DbType)SqlDbType.Int, 4,__goodsleaveword.Smileyoff),
						DbHelper.MakeInParam("@parseurloff", (DbType)SqlDbType.Int, 4,__goodsleaveword.Parseurloff),
						DbHelper.MakeInParam("@bbcodeoff", (DbType)SqlDbType.Int, 4,__goodsleaveword.Bbcodeoff),
						DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8,__goodsleaveword.Postdatetime),
                        DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4,__goodsleaveword.Id)
				};
            string sqlstring = String.Format("Update [" + BaseConfigs.GetTablePrefix + "goodsleavewords]  Set [goodsid] = @goodsid, [tradelogid] = @tradelogid, [isbuyer] = @isbuyer, [uid] = @uid, [username] = @username, [message] = @message, [invisible] = @invisible, [ip] = @ip, [usesig] = @usesig, [htmlon] = @htmlon, [smileyoff] = @smileyoff, [parseurloff] = @parseurloff, [bbcodeoff] = @bbcodeoff, [postdatetime] = @postdatetime WHERE [id] = @id");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
        }

        /// <summary>
        /// 通过交易日志id获得留言列表
        /// </summary>
        /// <param name="goodstradelogid">交易日志id</param>
        /// <returns></returns>
        public IDataReader GetGoodsLeaveWordListByTradeLogId(int goodstradelogid)
        {
            DbParameter parm = DbHelper.MakeInParam("@tradelogid", (DbType)SqlDbType.Int, 4, goodstradelogid);

            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM [{0}goodsleavewords] WHERE [tradelogid] = @tradelogid AND [invisible] = 0 ORDER BY [postdatetime] ASC", BaseConfigs.GetTablePrefix), parm);
        }

        /// <summary>
        /// 获取指定商品的留言
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <returns></returns>
        public int GetGoodsLeaveWordCountByGid(int goodsid)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@goodsid",(DbType)SqlDbType.Int,4,goodsid),
								  };

            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT count(id) FROM [{0}goodsleavewords] WHERE [goodsid] = @goodsid AND [tradelogid]=0 AND [invisible] = 0", BaseConfigs.GetTablePrefix), parms));
        }

        /// <summary>
        /// 获取指定商品分页的留言
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <param name="pagesize">分页大小</param>
        /// <param name="pageindex">当前分页</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="ascdesc">排序方向(0:asc 1:desc)</param>
        /// <returns></returns>
        public IDataReader GetGoodsLeaveWordByGid(int goodsid, int pagesize, int pageindex, string orderby, int ascdesc)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@goodsid",(DbType)SqlDbType.Int,4,goodsid)
    							  };

            string sql = "";
            if (pageindex <= 1)
            {
                sql = "SELECT TOP {0} * FROM [{1}goodsleavewords] WHERE [goodsid] = @goodsid AND [tradelogid]=0 AND [invisible] = 0 ORDER BY  {2} {3}";
            }
            else
            {
                if (ascdesc == 1)
                {
                    sql = "SELECT TOP {0} * FROM [{1}goodsleavewords] WHERE [id] < (SELECT MIN([id])  FROM (SELECT TOP " + ((pageindex - 1) * pagesize) + " [id] FROM [{1}goodsleavewords]  WHERE  [goodsid] = @goodsid AND [tradelogid]=0 AND [invisible] = 0  ORDER BY  {2} {3}) AS tblTmp ) AND [goodsid] = @goodsid AND [tradelogid]=0 AND [invisible] = 0 ORDER BY  {2} {3}";
                }
                else
                {
                    sql = "SELECT TOP {0} * FROM [{1}goodsleavewords] WHERE [id] > (SELECT MAX([id])  FROM (SELECT TOP " + ((pageindex - 1) * pagesize) + " [id] FROM [{1}goodsleavewords]  WHERE  [goodsid] = @goodsid AND [tradelogid]=0 AND [invisible] = 0  ORDER BY  {2} {3}) AS tblTmp ) AND [goodsid] = @goodsid AND [tradelogid]=0 AND [invisible] = 0 ORDER BY  {2} {3}";
                }
            }
            sql = string.Format(sql, pagesize, BaseConfigs.GetTablePrefix, orderby, ascdesc == 0 ? "ASC" : "DESC");

            return DbHelper.ExecuteReader(CommandType.Text, sql, parms);
        }

        /// <summary>
        /// 获取指定ID的留言信息
        /// </summary>
        /// <param name="id">留言id</param>
        /// <returns></returns>
        public IDataReader GetGoodsLeaveWordById(int id)
        {
            DbParameter parm = DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id);

            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}goodsleavewords] WHERE [id] = @id", BaseConfigs.GetTablePrefix), parm);
        }

        /// <summary>
        /// 删除指定ID的留言信息
        /// </summary>
        /// <param name="id">留言id</param>
        /// <returns></returns>
        public bool DeleteGoodsLeaveWordById(int id)
        {
            DbParameter parm = DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id);

            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}goodsleavewords] WHERE [id] = @id", BaseConfigs.GetTablePrefix), parm);

            return true;
        }

        /// <summary>
        /// 创建商品用户信用记录
        /// </summary>
        /// <param name="goodsusercredits">要创建的用户信用信息</param>
        /// <returns></returns>
        public int CreateGoodsUserCredit(Goodsusercreditinfo __goodsusercredits)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,__goodsusercredits.Uid),
						DbHelper.MakeInParam("@oneweek", (DbType)SqlDbType.Int, 4,__goodsusercredits.Oneweek),
						DbHelper.MakeInParam("@onemonth", (DbType)SqlDbType.Int, 4,__goodsusercredits.Onemonth),
						DbHelper.MakeInParam("@sixmonth", (DbType)SqlDbType.Int, 4,__goodsusercredits.Sixmonth),
						DbHelper.MakeInParam("@sixmonthago", (DbType)SqlDbType.Int, 4,__goodsusercredits.Sixmonthago),
						DbHelper.MakeInParam("@ratefrom", (DbType)SqlDbType.TinyInt, 1,__goodsusercredits.Ratefrom),
						DbHelper.MakeInParam("@ratetype", (DbType)SqlDbType.TinyInt, 1,__goodsusercredits.Ratetype)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "goodsusercredits] ([uid], [oneweek], [onemonth], [sixmonth], [sixmonthago], [ratefrom], [ratetype]) VALUES (@uid, @oneweek, @onemonth, @sixmonth, @sixmonthago, @ratefrom, @ratetype);SELECT SCOPE_IDENTITY()  AS 'id';");

            return Utils.StrToInt(DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0].Rows[0][0].ToString(), -1);
        }

        /// <summary>
        /// 初始化用户评价信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public int InitGoodsUserCredit(int userid)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,userid),
				};
            StringBuilder sb_sql = new StringBuilder();
            sb_sql.Append("INSERT INTO [" + BaseConfigs.GetTablePrefix + "goodsusercredits] ([uid],[ratefrom],[ratetype]) VALUES (@uid, 2, 1);");
            sb_sql.Append("INSERT INTO [" + BaseConfigs.GetTablePrefix + "goodsusercredits] ([uid],[ratefrom],[ratetype]) VALUES (@uid, 2, 2);");
            sb_sql.Append("INSERT INTO [" + BaseConfigs.GetTablePrefix + "goodsusercredits] ([uid],[ratefrom],[ratetype]) VALUES (@uid, 2, 3);");
            sb_sql.Append("INSERT INTO [" + BaseConfigs.GetTablePrefix + "goodsusercredits] ([uid],[ratefrom],[ratetype]) VALUES (@uid, 1, 1);");
            sb_sql.Append("INSERT INTO [" + BaseConfigs.GetTablePrefix + "goodsusercredits] ([uid],[ratefrom],[ratetype]) VALUES (@uid, 1, 2);");
            sb_sql.Append("INSERT INTO [" + BaseConfigs.GetTablePrefix + "goodsusercredits] ([uid],[ratefrom],[ratetype]) VALUES (@uid, 1, 3);SELECT SCOPE_IDENTITY()  AS 'id';");

            return Utils.StrToInt(DbHelper.ExecuteDataset(CommandType.Text, sb_sql.ToString(), parms).Tables[0].Rows[0][0].ToString(), -1);
        }

        /// <summary>
        /// 更新商品用户信用记录
        /// </summary>
        /// <param name="goodsusercredits">要更新的用户信用信息</param>
        /// <returns></returns>
        public bool UpdateGoodsUserCredit(Goodsusercreditinfo __goodsusercredits)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,__goodsusercredits.Uid),
						DbHelper.MakeInParam("@oneweek", (DbType)SqlDbType.Int, 4,__goodsusercredits.Oneweek),
						DbHelper.MakeInParam("@onemonth", (DbType)SqlDbType.Int, 4,__goodsusercredits.Onemonth),
						DbHelper.MakeInParam("@sixmonth", (DbType)SqlDbType.Int, 4,__goodsusercredits.Sixmonth),
						DbHelper.MakeInParam("@sixmonthago", (DbType)SqlDbType.Int, 4,__goodsusercredits.Sixmonthago),
						DbHelper.MakeInParam("@ratefrom", (DbType)SqlDbType.TinyInt, 1,__goodsusercredits.Ratefrom),
						DbHelper.MakeInParam("@ratetype", (DbType)SqlDbType.TinyInt, 1,__goodsusercredits.Ratetype),
                        DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4,__goodsusercredits.Id)
				};
            string sqlstring = String.Format("Update [" + BaseConfigs.GetTablePrefix + "goodsusercredits]  Set [uid] = @uid, [oneweek] = @oneweek, [onemonth] = @onemonth, [sixmonth] = @sixmonth, [sixmonthago] = @sixmonthago, [ratefrom] = @ratefrom, [ratetype] = @ratetype WHERE [id] = @id");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
        }

        /// <summary>
        /// 获取指定用户id的评价信息
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public IDataReader GetGoodsUserCreditByUid(int uid)
        {
            DbParameter parm = DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid);

            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM [{0}goodsusercredits] WHERE [uid] = @uid ORDER BY [id] ASC", BaseConfigs.GetTablePrefix), parm);
        }

        /// <summary>
        /// 发表商品评价
        /// </summary>
        /// <param name="goodsrates">要创建的商品评价信息</param>
        /// <returns></returns>
        public int CreateGoodsRate(Goodsrateinfo __goodsrates)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@goodstradelogid", (DbType)SqlDbType.Int, 4,__goodsrates.Goodstradelogid),
						DbHelper.MakeInParam("@message", (DbType)SqlDbType.NChar, 200,__goodsrates.Message),
						DbHelper.MakeInParam("@explain", (DbType)SqlDbType.NChar, 200,__goodsrates.Explain),
						DbHelper.MakeInParam("@ip", (DbType)SqlDbType.NVarChar, 15,__goodsrates.Ip),
						DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,__goodsrates.Uid),
						DbHelper.MakeInParam("@uidtype", (DbType)SqlDbType.TinyInt, 1,__goodsrates.Uidtype),
                        DbHelper.MakeInParam("@ratetouid", (DbType)SqlDbType.Int, 4,__goodsrates.Ratetouid),
                        DbHelper.MakeInParam("@ratetousername", (DbType)SqlDbType.NChar, 20,__goodsrates.Ratetousername),
						DbHelper.MakeInParam("@username", (DbType)SqlDbType.NChar, 20,__goodsrates.Username),
						DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8,__goodsrates.Postdatetime),
						DbHelper.MakeInParam("@goodsid", (DbType)SqlDbType.Int, 4,__goodsrates.Goodsid),
						DbHelper.MakeInParam("@goodstitle", (DbType)SqlDbType.NChar, 60,__goodsrates.Goodstitle),
						DbHelper.MakeInParam("@price", (DbType)SqlDbType.Decimal, 18,__goodsrates.Price),
						DbHelper.MakeInParam("@ratetype", (DbType)SqlDbType.TinyInt, 1,__goodsrates.Ratetype)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "goodsrates] ([goodstradelogid], [message], [explain], [ip], [uid], [uidtype], [ratetouid], [ratetousername], [username], [postdatetime], [goodsid], [goodstitle], [price], [ratetype]) VALUES (@goodstradelogid, @message, @explain, @ip, @uid, @uidtype, @ratetouid, @ratetousername, @username, @postdatetime, @goodsid, @goodstitle, @price, @ratetype);SELECT SCOPE_IDENTITY()  AS 'id'");

            return Utils.StrToInt(DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0].Rows[0][0].ToString(), -1);
        }

        /// <summary>
        /// 更新商品评价
        /// </summary>
        /// <param name="goodsrates">要更新的商品评价信息</param>
        /// <returns></returns>
        public bool UpdateGoodsRate(Goodsrateinfo __goodsrates)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@goodstradelogid", (DbType)SqlDbType.Int, 4,__goodsrates.Goodstradelogid),
						DbHelper.MakeInParam("@message", (DbType)SqlDbType.NChar, 200,__goodsrates.Message),
						DbHelper.MakeInParam("@explain", (DbType)SqlDbType.NChar, 200,__goodsrates.Explain),
						DbHelper.MakeInParam("@ip", (DbType)SqlDbType.NVarChar, 15,__goodsrates.Ip),
						DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,__goodsrates.Uid),
						DbHelper.MakeInParam("@uidtype", (DbType)SqlDbType.TinyInt, 1,__goodsrates.Uidtype),
                        DbHelper.MakeInParam("@ratetouid", (DbType)SqlDbType.Int, 4,__goodsrates.Ratetouid),
						DbHelper.MakeInParam("@username", (DbType)SqlDbType.NChar, 20,__goodsrates.Username),
						DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8,__goodsrates.Postdatetime),
						DbHelper.MakeInParam("@goodsid", (DbType)SqlDbType.Int, 4,__goodsrates.Goodsid),
						DbHelper.MakeInParam("@goodstitle", (DbType)SqlDbType.NChar, 60,__goodsrates.Goodstitle),
						DbHelper.MakeInParam("@price", (DbType)SqlDbType.Decimal, 18,__goodsrates.Price),
						DbHelper.MakeInParam("@ratetype", (DbType)SqlDbType.TinyInt, 1,__goodsrates.Ratetype),
                        DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4,__goodsrates.Id)
				};
            string sqlstring = String.Format("Update [" + BaseConfigs.GetTablePrefix + "goodsrates]  Set [goodstradelogid] = @goodstradelogid, [message] = @message, [explain] = @explain, [ip] = @ip, [uid] = @uid, [uidtype] = @uidtype, [ratetouid] = @ratetouid, [ratetousername] = @ratetousername, [username] = @username, [postdatetime] = @postdatetime, [goodsid] = @goodsid, [goodstitle] = @goodstitle, [price] = @price, [ratetype] = @ratetype WHERE [id] = @id");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
        }

        /// <summary>
        /// 通过交易日志id获取评价记录
        /// </summary>
        /// <param name="goodstradelogid">交易日志id</param>
        /// <returns></returns>
        public IDataReader GetGoodsRateByTradeLogID(int goodstradelogid)
        {
            DbParameter parm = DbHelper.MakeInParam("@goodstradelogid", (DbType)SqlDbType.Int, 4, goodstradelogid);

            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM [{0}goodsrates] WHERE [goodstradelogid] = @goodstradelogid", BaseConfigs.GetTablePrefix), parm);
        }

        /// <summary>
        /// 获取指定条件的评价数
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="uidtype">用户类型</param>
        /// <param name="ratetype">评价类型</param>
        /// <returns></returns>
        public IDataReader GetGoodsRateCount(int uid, int uidtype, int ratetype)
        {
            string sql = string.Format("SELECT  (SELECT COUNT([id])  FROM  [{0}goodsrates]  WHERE  [ratetouid]={1} AND [uidtype]={2} AND [ratetype]={3} AND DATEDIFF(day, GETDATE(), [postdatetime]) < 7) AS oneweek," +
                                               "(SELECT COUNT([id])  FROM  [{0}goodsrates]  WHERE  [ratetouid]={1} AND [uidtype]={2} AND [ratetype]={3} AND DATEDIFF(month, GETDATE(), [postdatetime]) < 1) AS onemonth," +
                                               "(SELECT COUNT([id])  FROM  [{0}goodsrates]  WHERE  [ratetouid]={1} AND [uidtype]={2} AND [ratetype]={3} AND DATEDIFF(month, GETDATE(), postdatetime) < 6) AS sixmonth," +
                                               "(SELECT COUNT([id])  FROM  [{0}goodsrates]  WHERE  [ratetouid]={1} AND [uidtype]={2} AND [ratetype]={3} AND DATEDIFF(month, GETDATE(), postdatetime) > 6) AS sixmonthago",
                                               BaseConfigs.GetTablePrefix,
                                               uid,
                                               uidtype,
                                               ratetype);
            return DbHelper.ExecuteReader(CommandType.Text, sql);
        }

        /// <summary>
        /// 获取指定用户的评价记录
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="uidtype">用户类型</param>
        /// <param name="ratetype">评价类型</param>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        public IDataReader GetGoodsRates(int uid, int uidtype, int ratetype, string filter)
        {
            string sql = "";
            DbParameter[] parms = 
                {
						DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4,uid)
                };

            switch (uidtype)
            {
                case 0: sql = string.Format("SELECT * FROM [{0}goodsrates] WHERE [ratetouid] = @userid ", BaseConfigs.GetTablePrefix); break; //收到的所有评价
                case 3: sql = string.Format("SELECT * FROM [{0}goodsrates] WHERE [uid] = @userid ", BaseConfigs.GetTablePrefix); break; //给他人的评价
                default: sql = string.Format("SELECT * FROM [{0}goodsrates] WHERE [ratetouid] = @userid AND [uidtype] = " + uidtype, BaseConfigs.GetTablePrefix); break; //收到卖家(1)或买家(2) 的评价
            }

            if (ratetype > 0 && ratetype <= 3)
            {
                sql += " AND [ratetype] = " + ratetype;
            }

            switch (filter.ToLower().Trim())
            {
                case "oneweek": sql += " AND DATEDIFF(day, GETDATE(), [postdatetime]) < 7 "; break; //一周内
                case "onemonth": sql += " AND DATEDIFF(month, GETDATE(), [postdatetime]) < 1 "; break; //一月内
                case "sixmonth": sql += " AND DATEDIFF(month, GETDATE(), [postdatetime]) < 6 "; break; //半年内
                case "sixmonthago": sql += " AND DATEDIFF(month, GETDATE(), [postdatetime]) > 6 "; break; //半年之前
            }

            sql += " ORDER BY [id] DESC ";
            return DbHelper.ExecuteReader(CommandType.Text, sql, parms);
        }

        /// <summary>
        /// 获取诚信规则列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetGoodsCreditRules()
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 15 * FROM [{0}goodscreditrules] ORDER BY [id]", BaseConfigs.GetTablePrefix));
        }

        /// <summary>
        /// 创建所在地信息
        /// </summary>
        /// <param name="locations">要创建的所在地信息</param>
        /// <returns></returns>
        public int CreateLocations(Locationinfo __locations)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@city", (DbType)SqlDbType.NVarChar, 50,__locations.City),
						DbHelper.MakeInParam("@state", (DbType)SqlDbType.NVarChar, 50,__locations.State),
						DbHelper.MakeInParam("@country", (DbType)SqlDbType.NVarChar, 50,__locations.Country),
						DbHelper.MakeInParam("@zipcode", (DbType)SqlDbType.NVarChar, 20,__locations.Zipcode)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "locations] ([city], [state], [country], [zipcode]) VALUES (@city, @state, @country, @zipcode);SELECT SCOPE_IDENTITY()  AS 'lid'");
            return Utils.StrToInt(DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0].Rows[0][0].ToString(), -1);
        }

        /// <summary>
        /// 更新所在地信息
        /// </summary>
        /// <param name="locations">要更新的所在地信息</param>
        /// <returns></returns>
        public bool UpdateLocations(Locationinfo __locations)
        {

            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@city", (DbType)SqlDbType.NVarChar, 50,__locations.City),
						DbHelper.MakeInParam("@state", (DbType)SqlDbType.NVarChar, 50,__locations.State),
						DbHelper.MakeInParam("@country", (DbType)SqlDbType.NVarChar, 50,__locations.Country),
						DbHelper.MakeInParam("@zipcode", (DbType)SqlDbType.NVarChar, 20,__locations.Zipcode),
                        DbHelper.MakeInParam("@lid", (DbType)SqlDbType.Int, 4,__locations.Lid)
				};
            string sqlstring = String.Format("Update [" + BaseConfigs.GetTablePrefix + "locations]  Set [city] = @city, [state] = @state, [country] = @country, [zipcode] = @zipcode WHERE [lid] = @lid");
            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
            return true;
        }

        /// <summary>
        /// 删除所在地信息
        /// </summary>
        /// <param name="lidlist">要删除的所在地id列表(以","分割)</param>
        public void DeleteLocations(string lidlist)
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM [" + BaseConfigs.GetTablePrefix + "locations] WHERE [lid] IN (" + lidlist + ")");
        }

        /// <summary>
        /// 更新指定诚信规则id的信息
        /// </summary>
        /// <param name="id">诚信规则id</param>
        /// <param name="lowerlimit">下限</param>
        /// <param name="upperlimit">上限</param>
        public void UpdateCreditRules(int id, int lowerlimit, int upperlimit)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@lowerlimit", (DbType)SqlDbType.Int, 4,lowerlimit),
						DbHelper.MakeInParam("@upperlimit", (DbType)SqlDbType.Int,4,upperlimit),
                        DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4,id)
				};

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "goodscreditrules]  SET [lowerlimit] = @lowerlimit, [upperlimit] = @upperlimit WHERE [id] = @id", parms);
        }

        /// <summary>
        /// 获取指定返回数的商品TAG数据
        /// </summary>
        /// <param name="count">返回数</param>
        /// <returns></returns>
        public IDataReader GetHotTagsListForGoods(int count)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP {0} * FROM [{1}tags] WHERE [gcount] > 0 ORDER BY [gcount] DESC,[orderid]", count, BaseConfigs.GetTablePrefix));
        }

        /// <summary>
        /// 获取指定标签商品数量
        /// </summary>
        /// <param name="tagid">TAG id</param>
        /// <returns></returns>
        public int GetGoodsCountWithSameTag(int tagid)
        {
            DbParameter parm = DbHelper.MakeInParam("@tagid", (DbType)SqlDbType.Int, 4, tagid);

            string sql = string.Format("SELECT COUNT(1) FROM [{0}goodstags] AS [gt],[{0}goods] AS [g] WHERE [gt].[tagid] = @tagid AND [g].[goodsid] = [gt].[goodsid] AND [g].[displayorder]>=0 AND [g].[closed]=0 ", BaseConfigs.GetTablePrefix);

            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sql, parm), 0);
        }

        /// <summary>
        /// 获取指定标签的商品数据列表
        /// </summary>
        /// <param name="tagid">TAG id</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="pagesize">页面尺寸</param>
        /// <returns></returns>
        public IDataReader GetGoodsWithSameTag(int tagid, int pageindex, int pagesize)
        {
            string sql = "";
            if (pageindex <= 1)
            {
                sql = "SELECT TOP " + pagesize + " [g].* FROM [{0}goods] AS [g], [{0}goodstags] AS [gt]	WHERE [g].[goodsid] = [gt].[goodsid] AND [g].[displayorder]>=0 AND [g].[closed]=0 AND [gt].[tagid] = " + tagid + " ORDER BY [g].[goodsid] DESC";
            }
            else
            {
                sql = "SELECT TOP " + pagesize + " [g].* FROM [{0}goods] AS [g], [{0}goodstags] AS [gt]	WHERE [g].[goodsid] = [gt].[goodsid] AND [g].[displayorder]>=0 AND [g].[closed]=0 AND [gt].[tagid] = " + tagid + " AND [g].[goodsid] < (SELECT MIN([goodsid]) FROM (SELECT TOP " + ((pageindex - 1) * pagesize) + " [g].[goodsid] FROM [{0}goods] AS [g], [{0}_goodstags] AS [gt] WHERE [g].[goodsid] = [gt].[goodsid] AND [g].[displayorder]>=0 AND [gt].[tagid] = " + tagid + "	ORDER BY [g].[goodsid] DESC) as tblTmp) ORDER BY [g].[goodsid] DESC";
            }
            return DbHelper.ExecuteReader(CommandType.Text, string.Format(sql, BaseConfigs.GetTablePrefix));
        }

        /// <summary>
        /// 创建店铺分类
        /// </summary>
        /// <param name="shopcategoryinfo">店铺分类信息</param>
        /// <returns></returns>
        public int CreateShopCategory(Shopcategoryinfo __shopcategoryinfo)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@parentid", (DbType)SqlDbType.Int, 4,__shopcategoryinfo.Parentid),
                        DbHelper.MakeInParam("@parentidlist", (DbType)SqlDbType.Char, 300,__shopcategoryinfo.Parentidlist),
                        DbHelper.MakeInParam("@layer", (DbType)SqlDbType.Char, 300,__shopcategoryinfo.Layer),
                        DbHelper.MakeInParam("@childcount", (DbType)SqlDbType.Char, 300,__shopcategoryinfo.Childcount),
						DbHelper.MakeInParam("@syscategoryid", (DbType)SqlDbType.Int, 4,__shopcategoryinfo.Syscategoryid),
						DbHelper.MakeInParam("@name", (DbType)SqlDbType.NChar, 50,__shopcategoryinfo.Name),
						DbHelper.MakeInParam("@categorypic", (DbType)SqlDbType.NVarChar, 100,__shopcategoryinfo.Categorypic),
						DbHelper.MakeInParam("@shopid", (DbType)SqlDbType.Int, 4,__shopcategoryinfo.Shopid),
						DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4,__shopcategoryinfo.Displayorder)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "shopcategories] ( [parentid], [parentidlist], [layer], [childcount], [syscategoryid], [name], [categorypic], [shopid], [displayorder]) VALUES (@parentid, @parentidlist, @layer, @childcount, @syscategoryid, @name, @categorypic, @shopid, @displayorder);SELECT SCOPE_IDENTITY()  AS 'id'");

            return Utils.StrToInt(DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0].Rows[0][0].ToString(), -1);
        }

        /// <summary>
        /// 更新店铺分类
        /// </summary>
        /// <param name="shopcategoryinfo">店铺分类信息</param>
        /// <returns></returns>
        public bool UpdateShopCategory(Shopcategoryinfo __shopcategoryinfo)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@parentid", (DbType)SqlDbType.Int, 4,__shopcategoryinfo.Parentid),
                        DbHelper.MakeInParam("@parentidlist", (DbType)SqlDbType.Char, 300,__shopcategoryinfo.Parentidlist),
                        DbHelper.MakeInParam("@layer", (DbType)SqlDbType.Char, 300,__shopcategoryinfo.Layer),
                        DbHelper.MakeInParam("@childcount", (DbType)SqlDbType.Char, 300,__shopcategoryinfo.Childcount),
						DbHelper.MakeInParam("@syscategoryid", (DbType)SqlDbType.Int, 4,__shopcategoryinfo.Syscategoryid),
                        DbHelper.MakeInParam("@name", (DbType)SqlDbType.NChar, 50,__shopcategoryinfo.Name),
						DbHelper.MakeInParam("@categorypic", (DbType)SqlDbType.NVarChar, 100,__shopcategoryinfo.Categorypic),
						DbHelper.MakeInParam("@shopid", (DbType)SqlDbType.Int, 4,__shopcategoryinfo.Shopid),
						DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4,__shopcategoryinfo.Displayorder),
                    	DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4,__shopcategoryinfo.Categoryid)
				};
            string sqlstring = String.Format("Update [" + BaseConfigs.GetTablePrefix + "shopcategories]  Set  [parentid] = @parentid, [parentidlist] = @parentidlist, [layer] = @layer, [childcount] = @childcount, [syscategoryid] = @syscategoryid, [name] = @name, [categorypic] = @categorypic, [shopid] = @shopid, [displayorder] = @displayorder WHERE [categoryid] = @categoryid");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
        }

        /// <summary>
        /// 更新指定店铺商品分类的子分类数字段
        /// </summary>
        /// <param name="childcount">更新的子分类数</param>
        /// <param name="categoryid">店铺商品分类id</param>
        public void UpdateShopCategoryChildCount(int childcount, int categoryid)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@childcount", (DbType)SqlDbType.Int, 4, childcount),
                DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4, categoryid)
			};

            DbHelper.ExecuteDataset(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "shopcategories] SET [childcount]=@childcount WHERE [categoryid]=@categoryid", parms);
        }

        /// <summary>
        /// 创建店铺友情链接
        /// </summary>
        /// <param name="shoplink">店铺友情链接信息</param>
        /// <returns></returns>
        public int CreateShopLink(Shoplinkinfo __shoplink)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4,__shoplink.Displayorder),
						DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 100,__shoplink.Name),
						DbHelper.MakeInParam("@linkshopid", (DbType)SqlDbType.Int, 4,__shoplink.Linkshopid),
						DbHelper.MakeInParam("@shopid", (DbType)SqlDbType.Int, 4,__shoplink.Shopid)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "shoplinks] ([displayorder], [name], [linkshopid], [shopid]) VALUES (@displayorder, @name, @linkshopid, @shopid);SELECT SCOPE_IDENTITY()  AS 'id'");

            return Utils.StrToInt(DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0].Rows[0][0].ToString(), -1);
        }


        /// <summary>
        /// 更新店铺友情链接
        /// </summary>
        /// <param name="shoplink">店铺友情链接信息</param>
        /// <returns></returns>
        public bool UpdateShopLink(Shoplinkinfo __shoplink)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4,__shoplink.Displayorder),
						DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 100,__shoplink.Name),
						DbHelper.MakeInParam("@linkshopid", (DbType)SqlDbType.Int, 4,__shoplink.Linkshopid),
						DbHelper.MakeInParam("@shopid", (DbType)SqlDbType.Int, 4,__shoplink.Shopid),
   						DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4,__shoplink.Id)
				};
            string sqlstring = String.Format("Update [" + BaseConfigs.GetTablePrefix + "shoplinks]  Set [displayorder] = @displayorder, [name] = @name, [linkshopid] = @linkshopid, [shopid] = @shopid WHERE [id] = @id");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
        }

        /// <summary>
        /// 获取指定店铺的友情链接信息
        /// </summary>
        /// <param name="shopid">店铺id</param>
        /// <returns></returns>
        public IDataReader GetShopLinkByShopId(int shopid)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@shopid",(DbType)SqlDbType.Int,4,shopid)
								  };

            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT  * FROM [{0}shoplinks] WHERE [shopid] = @shopid  ORDER BY  [displayorder] ASC", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 删除指定id的店铺友情链接信息
        /// </summary>
        /// <param name="shoplinkidlist">店铺链接id串(格式:1,2,3)</param>
        /// <returns></returns>
        public int DeleteShopLink(string shoplinkidlist)
        {
            return DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}shoplinks] WHERE [id] IN (" + shoplinkidlist + ")", BaseConfigs.GetTablePrefix));
        }
        /// <summary>
        /// 创建店铺
        /// </summary>
        /// <param name="shopinfo">店铺信息</param>
        /// <returns></returns>
        public int CreateShop(Shopinfo __shopinfo)
        {
            DbParameter[] parms = 
				{
                        DbHelper.MakeInParam("@logo", (DbType)SqlDbType.NVarChar, 50,__shopinfo.Logo),
						DbHelper.MakeInParam("@shopname", (DbType)SqlDbType.NVarChar, 100,__shopinfo.Shopname),
						DbHelper.MakeInParam("@themeid", (DbType)SqlDbType.Int, 4,__shopinfo.Themeid),
						DbHelper.MakeInParam("@themepath", (DbType)SqlDbType.NChar, 50,__shopinfo.Themepath),
						DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,__shopinfo.Uid),
						DbHelper.MakeInParam("@username", (DbType)SqlDbType.NChar, 20,__shopinfo.Username),
						DbHelper.MakeInParam("@introduce", (DbType)SqlDbType.NVarChar, 500,__shopinfo.Introduce),
						DbHelper.MakeInParam("@lid", (DbType)SqlDbType.Int, 4,__shopinfo.Lid),
						DbHelper.MakeInParam("@locus", (DbType)SqlDbType.NChar, 20,__shopinfo.Locus),
						DbHelper.MakeInParam("@bulletin", (DbType)SqlDbType.NVarChar, 500,__shopinfo.Bulletin),
						DbHelper.MakeInParam("@createdatetime", (DbType)SqlDbType.DateTime, 8,__shopinfo.Createdatetime),
						DbHelper.MakeInParam("@invisible", (DbType)SqlDbType.Int, 4,__shopinfo.Invisible),
                        DbHelper.MakeInParam("@viewcount", (DbType)SqlDbType.Int, 4,__shopinfo.Viewcount)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "shops] ([logo], [shopname], [themeid], [themepath], [uid], [username], [introduce], [lid], [locus], [bulletin], [createdatetime], [invisible], [viewcount]) VALUES (@logo, @shopname, @themeid, @themepath, @uid, @username, @introduce, @lid, @locus, @bulletin, @createdatetime, @invisible, @viewcount);SELECT SCOPE_IDENTITY()  AS 'id'");

            return Utils.StrToInt(DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0].Rows[0][0].ToString(), -1);
        }

        /// <summary>
        /// 更新店铺信息
        /// </summary>
        /// <param name="shopinfo">店铺信息</param>
        public bool UpdateShop(Shopinfo __shopinfo)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@logo", (DbType)SqlDbType.NVarChar, 50,__shopinfo.Logo),
                        DbHelper.MakeInParam("@shopname", (DbType)SqlDbType.NVarChar, 100,__shopinfo.Shopname),
						DbHelper.MakeInParam("@themeid", (DbType)SqlDbType.Int, 4,__shopinfo.Themeid),
						DbHelper.MakeInParam("@themepath", (DbType)SqlDbType.NChar, 50,__shopinfo.Themepath),
						DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,__shopinfo.Uid),
						DbHelper.MakeInParam("@username", (DbType)SqlDbType.NChar, 20,__shopinfo.Username),
						DbHelper.MakeInParam("@introduce", (DbType)SqlDbType.NVarChar, 500,__shopinfo.Introduce),
						DbHelper.MakeInParam("@lid", (DbType)SqlDbType.Int, 4,__shopinfo.Lid),
						DbHelper.MakeInParam("@locus", (DbType)SqlDbType.NChar, 20,__shopinfo.Locus),
						DbHelper.MakeInParam("@bulletin", (DbType)SqlDbType.NVarChar, 500,__shopinfo.Bulletin),
						DbHelper.MakeInParam("@createdatetime", (DbType)SqlDbType.DateTime, 8,__shopinfo.Createdatetime),
						DbHelper.MakeInParam("@invisible", (DbType)SqlDbType.Int, 4,__shopinfo.Invisible),
                        DbHelper.MakeInParam("@viewcount", (DbType)SqlDbType.Int, 4,__shopinfo.Viewcount),
                        DbHelper.MakeInParam("@shopid", (DbType)SqlDbType.Int, 4,__shopinfo.Shopid)
				};
            string sqlstring = String.Format("Update [" + BaseConfigs.GetTablePrefix + "shops]  Set [logo] = @logo, [shopname] = @shopname, [themeid] = @themeid, [themepath] = @themepath, [uid] = @uid, [username] = @username, [introduce] = @introduce, [lid] = @lid, [locus] = @locus, [bulletin] = @bulletin, [createdatetime] = @createdatetime, [invisible] = @invisible, [viewcount] = @viewcount WHERE [shopid] = @shopid");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
        }

        /// <summary>
        /// 创建店铺主题
        /// </summary>
        /// <param name="shopthemeinfo">店铺主题信息</param>
        /// <returns></returns>
        public int CreateShopTheme(Shopthemeinfo __shopthemeinfo)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@directory", (DbType)SqlDbType.VarChar, 100,__shopthemeinfo.Directory),
						DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 50,__shopthemeinfo.Name),
						DbHelper.MakeInParam("@author", (DbType)SqlDbType.NVarChar, 100,__shopthemeinfo.Author),
						DbHelper.MakeInParam("@createdate", (DbType)SqlDbType.NVarChar, 50,__shopthemeinfo.Createdate),
						DbHelper.MakeInParam("@copyright", (DbType)SqlDbType.NVarChar, 100,__shopthemeinfo.Copyright)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "shopthemes] ([directory], [name], [author], [createdate], [copyright]) VALUES (@directory, @name, @author, @createdate, @copyright);SELECT SCOPE_IDENTITY()  AS 'id'");

            return Utils.StrToInt(DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0].Rows[0][0].ToString(), -1);
        }

        /// <summary>
        /// 更新店铺主题
        /// </summary>
        /// <param name="shopthemeinfo">店铺主题信息</param>
        /// <returns></returns>
        public bool UpdateShopTheme(Shopthemeinfo __shopthemeinfo)
        {
            DbParameter[] parms = 
				{
						DbHelper.MakeInParam("@directory", (DbType)SqlDbType.VarChar, 100,__shopthemeinfo.Directory),
						DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 50,__shopthemeinfo.Name),
						DbHelper.MakeInParam("@author", (DbType)SqlDbType.NVarChar, 100,__shopthemeinfo.Author),
						DbHelper.MakeInParam("@createdate", (DbType)SqlDbType.NVarChar, 50,__shopthemeinfo.Createdate),
						DbHelper.MakeInParam("@copyright", (DbType)SqlDbType.NVarChar, 100,__shopthemeinfo.Copyright),
                    	DbHelper.MakeInParam("@themeid", (DbType)SqlDbType.Int, 4,__shopthemeinfo.Themeid)
				};
            string sqlstring = String.Format("Update [" + BaseConfigs.GetTablePrefix + "shopthemes]  Set [directory] = @directory, [name] = @name, [author] = @author, [createdate] = @createdate, [copyright] = @copyright WHERE [themeid] = @themeid");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
        }

        /// <summary>
        /// 获取热门或新开的店铺信息
        /// </summary>
        /// <param name="shoptype">热门店铺(1:热门店铺, 2 :新开店铺)</param>
        /// <param name="topnumber">返回数</param>
        /// <returns></returns>
        public IDataReader GetHotOrNewShops(int shoptype, int topnumber)
        {
            string sql = "SELECT TOP " + topnumber + " * FROM [" + BaseConfigs.GetTablePrefix + "shops] WHERE [invisible] = 0 ORDER BY ";

            if (shoptype == 1)
            {
                sql += "[viewcount] DESC ";
            }
            else
            {
                sql += "[createdatetime] DESC ";
            }
            return DbHelper.ExecuteReader(CommandType.Text, sql);
        }

        /// <summary>
        /// 获取指定主题ID的店铺风格信息
        /// </summary>
        /// <param name="themeid">主题ID</param>
        /// <returns></returns>
        public IDataReader GetShopThemeByThemeId(int themeid)
        {
            DbParameter[] parms = 
				{
                    	DbHelper.MakeInParam("@themeid", (DbType)SqlDbType.Int, 4,themeid)
				};
            return DbHelper.ExecuteReader(CommandType.Text, String.Format("SELECT  TOP 1 * FROM  [{0}shopthemes]  WHERE  [themeid] = @themeid", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 通过指定的论坛版块id获取相应的商品分类
        /// </summary>
        /// <param name="forumid">版块id</param>
        /// <returns></returns>
        public int GetGoodsCategoryIdByFid(int forumid)
        {
            DbParameter[] parms = 
				{
                    	DbHelper.MakeInParam("@fid", (DbType)SqlDbType.Int, 4,forumid)
				};
            string sqlstring = String.Format("SELECT  ISNULL  ((SELECT  TOP 1 [categoryid]  FROM  [" + BaseConfigs.GetTablePrefix + "goodscategories]  WHERE  [fid] = @fid), 0)");

            return Utils.StrToInt(DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0].Rows[0][0].ToString(), -1);
        }

        /// <summary>
        /// 获取绑定论坛版块ID的商品分类
        /// </summary>
        /// <returns></returns>
        public IDataReader GetGoodsCategoryWithFid()
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT  [fid], [categoryid] FROM  [{0}goodscategories] GROUP BY [fid], [layer], [categoryid] HAVING      (fid > 0) AND (layer <= 1) ORDER BY [layer]", BaseConfigs.GetTablePrefix));
        }

        /// <summary>
        /// 获取指定店铺的商品类型数据(json格式)
        /// </summary>
        /// <param name="shopid">店铺id</param>
        /// <returns></returns>
        public DataTable GetShopCategoryTableToJson(int shopid)
        {
            DbParameter[] parms = 
				{
                    	DbHelper.MakeInParam("@shopid", (DbType)SqlDbType.Int, 4,shopid)
				};
            return DbHelper.ExecuteDataset(CommandType.Text, String.Format("SELECT  * FROM  [{0}shopcategories]  WHERE  [shopid] = @shopid ORDER BY [displayorder] ASC ", BaseConfigs.GetTablePrefix), parms).Tables[0];
        }

        /// <summary>
        /// 获取指定shopid的店铺信息
        /// </summary>
        /// <returns></returns>
        public IDataReader GetShopByUserId(int userid)
        {
            DbParameter[] parms = 
				{
                    	DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,userid)
				};
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}shops] WHERE [uid] = @uid", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 获取指定分类id的店铺商品类型数据
        /// </summary>
        /// <param name="categoryid">分类id</param>
        /// <returns></returns>
        public IDataReader GetShopCategoryByCategoryId(int categoryid)
        {
            DbParameter[] parms = 
				{
                    	DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4,categoryid)
				};
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}shopcategories] WHERE [categoryid] = @categoryid", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 指定的商品分类下有无子分类
        /// </summary>
        /// <param name="shopcategoryinfo">指定的商品分类</param>
        /// <returns></returns>
        public bool IsExistSubShopCategory(Shopcategoryinfo shopcategoryinfo)
        {
            DbParameter[] parms = 
            {
                DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4, shopcategoryinfo.Categoryid)
		    };

            if (DbHelper.ExecuteDataset(CommandType.Text, "SELECT TOP 1 * FROM [" + BaseConfigs.GetTablePrefix + "shopcategories] WHERE [parentid]=@categoryid", parms).Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除指定的商品分类信息
        /// </summary>
        /// <param name="shopcategoryinfo"></param>
        /// <returns></returns>
        public bool DeleteShopCategory(Shopcategoryinfo shopcategoryinfo)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(DbHelper.ConnectionString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    //调整在当前节点排序位置之后的节点,做减1操作
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "shopcategories] SET [displayorder]=[displayorder]-1 WHERE [displayorder]>" + shopcategoryinfo.Displayorder);
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "goods] SET [shopcategorylist] = REPLACE([shopcategorylist], '," + shopcategoryinfo.Categoryid + ",', ',') WHERE  CHARINDEX('," + shopcategoryinfo.Categoryid + ",', RTRIM([shopcategorylist])) > 0");
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "DELETE FROM [" + BaseConfigs.GetTablePrefix + "shopcategories] WHERE [categoryid] = " + shopcategoryinfo.Categoryid);
                    trans.Commit();
                    result = true;
                }
                catch
                {
                    trans.Rollback();
                }
            }
            conn.Close();
            return result;
        }

        /// <summary>
        /// 移动商品分类
        /// </summary>
        /// <param name="shopcategoryinfo">源分类</param>
        /// <param name="targetshopcategoryinfo">目标分类</param>
        /// <param name="isaschildnode">是否作为子节点</param>
        public void MovingShopCategoryPos(Shopcategoryinfo shopcategoryinfo, Shopcategoryinfo targetshopcategoryinfo, bool isaschildnode)
        {
            SqlConnection conn = new SqlConnection(DbHelper.ConnectionString);
            conn.Open();

            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    //当前商品分类带子分类时
                    if (DbHelper.ExecuteDataset(CommandType.Text, "SELECT TOP 1 [categoryid] FROM [" + BaseConfigs.GetTablePrefix + "shopcategories] WHERE [parentid]=" + shopcategoryinfo.Categoryid).Tables[0].Rows.Count > 0)
                    {
                        #region

                        string sqlstring = "";
                        if (isaschildnode) //作为商品分类子分类插入
                        {
                            //让位于当前商品分类(分类)显示顺序之后的商品分类全部加1(为新加入的商品分类让位结果)
                            sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "shopcategories] SET [displayorder]=[displayorder]+1 WHERE [displayorder]>={0} AND [shopid] = {1}",
                                                      Convert.ToString(targetshopcategoryinfo.Displayorder + 1),
                                                      targetshopcategoryinfo.Shopid);
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);

                            //更新当前商品分类的相关信息
                            sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "shopcategories] SET [parentid]={1},[displayorder]={2} WHERE [categoryid]={0}", shopcategoryinfo.Categoryid, targetshopcategoryinfo.Categoryid, Convert.ToString(targetshopcategoryinfo.Displayorder + 1));
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);
                        }
                        else //作为同级商品分类,在目标商品分类之前插入
                        {
                            //让位于包括当前商品分类显示顺序之后的商品分类全部加1(为新加入的商品分类让位结果)
                            sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "shopcategories] SET [displayorder]=[displayorder]+1 WHERE ([displayorder]>={0} AND [shopid] = {1}) OR [categoryid]={2}",
                                                      targetshopcategoryinfo.Shopid,
                                                      targetshopcategoryinfo.Displayorder,
                                                      targetshopcategoryinfo.Categoryid);
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);

                            //更新当前商品分类的相关信息
                            sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "shopcategories] SET [parentid]={1},[displayorder]={2}  WHERE [categoryid]={0}", shopcategoryinfo.Categoryid, targetshopcategoryinfo.Parentid, Convert.ToString(targetshopcategoryinfo.Displayorder));
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);
                        }

                        #endregion
                    }
                    else //当前商品分类不带子分类
                    {
                        #region

                        //让位于当前节点显示顺序之后的节点全部减1 [起到删除节点的效果]
                        if (isaschildnode) //作为子分类插入
                        {
                            //让位于当前商品分类显示顺序之后的商品分类全部加1(为新加入的商品分类让位结果)
                            string sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "shopcategories] SET [displayorder]=[displayorder]+1 WHERE [displayorder]>={0}  AND [shopid] = {1}",
                                                             Convert.ToString(targetshopcategoryinfo.Displayorder + 1),
                                                             targetshopcategoryinfo.Shopid);
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);

                            string parentidlist = null;
                            if (targetshopcategoryinfo.Parentidlist == "0")
                            {
                                parentidlist = targetshopcategoryinfo.Categoryid.ToString();
                            }
                            else
                            {
                                parentidlist = targetshopcategoryinfo.Parentidlist.Trim() + "," + targetshopcategoryinfo.Categoryid;
                            }

                            //更新当前商品分类的相关信息
                            sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "shopcategories] SET [parentid]={1},[layer]={2},[parentidlist]='{3}',[displayorder]={4} WHERE [categoryid]={0}",
                                                      shopcategoryinfo.Categoryid,
                                                      targetshopcategoryinfo.Categoryid,
                                                      parentidlist.Split(',').Length,
                                                      parentidlist,
                                                      Convert.ToString(targetshopcategoryinfo.Displayorder + 1)
                                );
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);

                        }
                        else //作为同级商品分类,在目标商品分类之前插入
                        {
                            //让位于包括当前商品分类显示顺序之后的商品分类全部加1(为新加入的商品分类让位结果)
                            string sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "shopcategories] SET [displayorder]=[displayorder]+1 WHERE ([displayorder]>={0} AND [shopid] = {1}) OR [categoryid]={2}",
                                                             Convert.ToString(targetshopcategoryinfo.Displayorder + 1),
                                                             targetshopcategoryinfo.Shopid,
                                                             targetshopcategoryinfo.Categoryid);
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);



                            //更新当前商品分类的相关信息
                            sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "shopcategories]  SET [parentid]={1},[layer]={2},[parentidlist]='{3}',[displayorder]={4} WHERE [categoryid]={0}",
                                                      shopcategoryinfo.Categoryid,
                                                      targetshopcategoryinfo.Parentid,
                                                      targetshopcategoryinfo.Parentidlist.Trim().Split(',').Length,
                                                      targetshopcategoryinfo.Parentidlist.Trim(),
                                                      targetshopcategoryinfo.Displayorder
                                );
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);
                        }

                        #endregion
                    }
                    trans.Commit();
                }

                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                conn.Close();
            }
        }

        /// <summary>
        /// 更新商品分类的显示顺序
        /// </summary>
        /// <param name="displayorder">显示信息</param>
        /// <param name="categoryid">商品分类id</param>
        public void UpdateShopCategoryDisplayOrder(int displayorder, int categoryid)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, displayorder),
                DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4, categoryid)
			};

            DbHelper.ExecuteDataset(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "shopcategories] SET [displayorder]=@displayorder WHERE [categoryid] = @categoryid", parms);
        }


        /// <summary>
        /// 获取指定店铺的商品分类
        /// </summary>
        /// <param name="shopid">店铺id</param>
        /// <returns></returns>
        public DataTable GetShopCategoryByShopId(int shopid)
        {
            DbParameter[] parms = 
				{
                    	DbHelper.MakeInParam("@shopid", (DbType)SqlDbType.Int, 4,shopid)
				};
            return DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT [categoryid], [parentid] FROM [{0}shopcategories] WHERE [shopid] = @shopid", BaseConfigs.GetTablePrefix), parms).Tables[0];
        }

        /// <summary>
        /// 获取以当前分类为父分类的所有分类数据
        /// </summary>
        /// <param name="parentid">当前分类id</param>
        /// <returns></returns>
        public DataTable GetCategoryidInShopByParentid(int parentid)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@parentid", (DbType)SqlDbType.Int, 4, parentid)
			};
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT [categoryid] FROM [" + BaseConfigs.GetTablePrefix + "shopcategories] WHERE [parentid]=@parentid ORDER BY [displayorder] ASC", parms).Tables[0];
        }

        /// <summary>
        /// 更新指定商品的店铺商品分类字段
        /// </summary>
        /// <param name="goodsidlist">指定商品id串(格式:1,2,3)</param>
        /// <param name="shopgoodscategoryid">要绑定的店铺商品分类id</param>
        /// <returns></returns>
        public int MoveGoodsShopCategory(string goodsidlist, int shopgoodscategoryid)
        {
            return DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}goods] SET [shopcategorylist] = RTRIM([shopcategorylist]) + '" + shopgoodscategoryid + ",' WHERE [goodsid] IN (" + goodsidlist + ") AND CHARINDEX('," + shopgoodscategoryid + ",', RTRIM([shopcategorylist])) <= 0 ", BaseConfigs.GetTablePrefix));
        }

        /// <summary>
        /// 移除指定商品的店铺商品分类
        /// </summary>
        /// <param name="removegoodsid">指定的商品id</param>
        /// <param name="removeshopgoodscategoryid">要移除的店铺商品分类id</param>
        /// <returns></returns>
        public int RemoveGoodsShopCategory(int removegoodsid, int removeshopgoodscategoryid)
        {
            return DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}goods] SET [shopcategorylist] = REPLACE([shopcategorylist], '," + removeshopgoodscategoryid + ",', ',')  WHERE [goodsid] = " + removegoodsid + " AND CHARINDEX('," + removeshopgoodscategoryid + ",', RTRIM([shopcategorylist])) >= 0 ", BaseConfigs.GetTablePrefix));
        }

        /// <summary>
        /// 设置指定商品的推荐值信息
        /// </summary>
        /// <param name="goodsidlist">指定的商品id串(格式:1,2,3)</param>
        /// <param name="recommendvalue">推荐值</param>
        /// <returns></returns>
        public int RecommendGoods(string goodsidlist, int recommendvalue)
        {
            return DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}goods] SET [recommend] = " + recommendvalue + " WHERE [goodsid] IN (" + goodsidlist + ")", BaseConfigs.GetTablePrefix));
        }

        /// <summary>
        /// 获取店铺主题信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetShopThemes()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "shopthemes] ORDER BY [themeid] ASC").Tables[0];
        }

        /// <summary>
        ///  获取热门商品信息
        /// </summary>
        /// <param name="datetype">天数</param>
        /// <param name="categroyid">商品分类</param>
        /// <param name="count">返回记录条数</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public IDataReader GetHotGoods(int days, int categoryid, int count, string condition)
        {
            string sql = "";
            if (categoryid <= 0)
            {
                sql = string.Format("SELECT * FROM  [{0}goods] WHERE [goodsid] IN (SELECT TOP {1} [goodsid] FROM  [{0}goodstradelogs]  WHERE DATEDIFF(day, [lastupdate], GETDATE()) <= {2} GROUP BY [goodsid] ORDER BY COUNT(goodsid) DESC) {3}", BaseConfigs.GetTablePrefix, count, days, condition);
            }
            else
            {
                sql = string.Format("SELECT * FROM  [{0}goods] WHERE [goodsid] IN (SELECT TOP {1} [goodsid] FROM  [{0}goodstradelogs]  WHERE DATEDIFF(day, [lastupdate], GETDATE()) <= {2} AND [categoryid] = {3}  GROUP BY [goodsid] ORDER BY COUNT(goodsid) DESC) {4}", BaseConfigs.GetTablePrefix, count, days, categoryid, condition);
            }

            return DbHelper.ExecuteReader(CommandType.Text, sql);
        }

        /// <summary>
        /// 获取版块在商品分类中绑定的个数
        /// </summary>
        /// <param name="fid">版块id</param>
        /// <returns></returns>
        public int GetCategoriesFidCount(int fid)
        {
            DbParameter parms = DbHelper.MakeInParam("@fid", (DbType)SqlDbType.Int, 4, fid);

            return int.Parse(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(*) FROM [" + BaseConfigs.GetTablePrefix + "goodscategories] WHERE [fid]=@fid", parms).ToString());
        }

        /// <summary>
        /// 获取所有商品交易记录的Sql语句
        /// </summary>
        /// <returns></returns>
        public string GetAllGoodstradelogs(string status)
        {
            if (status == "")
                return string.Format("SELECT * FROM  [{0}goodstradelogs]", BaseConfigs.GetTablePrefix);
            else
                return string.Format("SELECT * FROM  [{0}goodstradelogs] WHERE [status]={1}", BaseConfigs.GetTablePrefix, status);
        }

        /// <summary>
        /// 获取所有回收站中的商品语句
        /// </summary>
        /// <returns></returns>
        public string GetAllRecyclebinGoods()
        {
            return string.Format("SELECT g.*,c.categoryname FROM [{0}goods] g LEFT JOIN [{0}goodscategories] c ON g.[categoryid]=c.[categoryid] WHERE g.[displayorder]=-1", BaseConfigs.GetTablePrefix);
        }

        /// <summary>
        /// 获取全部审核的商品
        /// </summary>
        /// <returns></returns>
        public string GetAllAuditGoods()
        {
            return string.Format("SELECT g.*,c.categoryname FROM [{0}goods] g LEFT JOIN [{0}goodscategories] c ON g.[categoryid]=c.[categoryid] WHERE g.[displayorder]=-2", BaseConfigs.GetTablePrefix);
        }

        /// <summary>
        /// 恢复回收站中的商品
        /// </summary>
        /// <param name="goodsid">要恢复商品的ID列表</param>
        public void ResetRecyclebinGoods(string goodsid)
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}goods] SET [displayorder]=0 WHERE [goodsid] IN ({1})", BaseConfigs.GetTablePrefix, goodsid));
        }

        /// <summary>
        /// 更新指定商品为通过审核
        /// </summary>
        /// <param name="goodsid"></param>
        public void PassAuditGoods(string goodsid)
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}goods] SET [displayorder]=0 WHERE [goodsid] IN ({1})", BaseConfigs.GetTablePrefix, goodsid));
        }

    }

}
#endif