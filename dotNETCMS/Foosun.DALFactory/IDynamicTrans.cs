using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Foosun.DALFactory
{
    public interface IDynamicTrans
    {
        IDataReader GetNewsInfo(string NewsID,int Num,int ChID, string DTable);
        IDataReader getUserInfo(string UserNum);
        int UpdateHistory(int InfoType,string InfoID, int iPoint, int Gpoint, string UserNum,string IP);
        bool getUserNote(string UserNum, string infoID,int Num);
    }
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class DataAccess
    {
        public static IDynamicTrans CreateDynamicTrans()
        {
            string className = path + ".DynamicTrans";
            return (IDynamicTrans)Assembly.Load(path).CreateInstance(className);
        }
    }
}
