//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By ZhaoHui.Chen                    ==
//===========================================================
using System;
using System.Data;
using Foosun.Model;

namespace Foosun.CMS
{
    public class News
    {
        private Foosun.DALFactory.INews dal;
        public News()
        {
            dal = Foosun.DALFactory.DataAccess.CreateNews();
        }
        public DataTable GetTables()
        {
            return dal.GetTables();
        }
        #region 归档新闻
        public DataTable CoverTabNews1(string SeleStr, string TableID_Sql, string boxs)
        {
            return dal.CoverTabNews1(SeleStr, TableID_Sql, boxs);
        }

        public int delPP(string boxs)
        {
            return dal.delPP(boxs);
        }
        public int locks(string boxs)
        {
            return dal.locks(boxs);
            
        }
        public int unlovkc(string boxs)
        {
            return dal.unlovkc(boxs);
        }
        public int delalpl()
        {
            return dal.delalpl();
        }
        #endregion

        public int AddNewsClick(string NewsID)
        {
            return dal.AddNewsClick(NewsID);
        }

        /// <summary>
        /// 添加评论信息
        /// </summary>
        /// <param name="ci">实体类</param>
        /// <returns>如果添加成功返回1</returns>
        public int AddComment(Foosun.Model.Comment ci)
        {
            return dal.AddComment(ci);
        }

        /// <summary>
        /// 取得评论列表
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        /// <returns>返回数据表</returns>
        public DataTable getCommentList(string NewsID)
        {
            return dal.getCommentList(NewsID);
        }

        /// <summary>
        /// 得到评论数
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="Todays"></param>
        /// <returns></returns>
        public string getCommCounts(string NewsID, string Todays)
        {
            return dal.getCommCounts(NewsID, Todays);
        }

        /// <summary>
        /// 得到评论观点统计
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int returnCommentGD(string infoID, int num)
        {
            return dal.returnCommentGD(infoID, num);
        }

        /// <summary>
        /// 得到新闻的DIGG数量
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public int gettopnum(string NewsID, string getNum)
        {
            return dal.gettopnum(NewsID, getNum);
        }

        /// <summary>
        /// 得到新闻的投票
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public DataTable getvote(string NewsID)
        {
            return dal.getvote(NewsID);
        }

        /// <summary>
        /// 得到某个新闻的内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public IDataReader getNewsInfo(string NewsID,int ChID)
        {
            return dal.getNewsInfo(NewsID, ChID);
        }

        public IDataReader getClassInfo(string ClassID,int ChID)
        {
            return dal.getClassInfo(ClassID, ChID);
        }
    }
}
