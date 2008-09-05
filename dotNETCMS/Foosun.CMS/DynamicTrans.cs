using System;
using System.Collections.Generic;
using System.Data;
using Foosun.Model;
using Foosun.DALFactory;

namespace Foosun.CMS
{
    public class DynamicTrans
    {
        private IDynamicTrans dal;
        public DynamicTrans()
        {
            dal = DataAccess.CreateDynamicTrans();
        }

        /// <summary>
        /// 得到新闻相关信息
        /// </summary>
        /// <param name="NewsID">传入的NewsID</param>
        /// <returns>记录集</returns>
        public IDataReader GetNewsInfo(string NewsID,int Num,int ChID, string DTable)
        {
            return dal.GetNewsInfo(NewsID, Num, ChID, DTable);
        }

        /// <summary>
        /// 得到用户信息
        /// </summary>
        /// <param name="UserNum"></param>
        /// <returns></returns>
        public IDataReader getUserInfo(string UserNum)
        {
            return dal.getUserInfo(UserNum);
        }

        public int UpdateHistory(int InfoType,string InfoID, int iPoint, int Gpoint, string UserNum, string IP)
        {
            return dal.UpdateHistory(InfoType,InfoID, iPoint, Gpoint, UserNum, IP);
        }

        public bool getUserNote(string UserNum, string infoID,int Num)
        {
            return dal.getUserNote(UserNum, infoID,Num);
        }
    }
}
