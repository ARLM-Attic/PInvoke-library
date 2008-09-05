using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface INews
    {
        DataTable GetTables();
        DataTable CoverTabNews1(string SeleStr, string TableID_Sql, string boxs);
        int delPP(string boxs);
        int locks(string boxs);
        int unlovkc(string boxs);
        int delalpl();
        int AddNewsClick(string NewsID);
        int AddComment(Foosun.Model.Comment ci);
        DataTable getCommentList(string NewsID);
        int returnCommentGD(string infoID, int num);
        int gettopnum(string NewsID, string getNum);
        string getCommCounts(string NewsID, string Todays);
        DataTable getvote(string NewsID);
        IDataReader getNewsInfo(string NewsID, int ChID);
        IDataReader getClassInfo(string ClassID, int ChID);
    }

    public sealed partial class DataAccess
    {
        public static INews CreateNews()
        {
            string className = path + ".News";
            return (INews)Assembly.Load(path).CreateInstance(className);
        }
    }
}
