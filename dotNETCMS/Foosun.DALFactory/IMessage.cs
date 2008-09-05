using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IMessage
    {
        #region Message_box.aspx
        int sel(string ID);
        int Update(string ID);
        int sel_3(string ID);
        int Update_1(string ID);
        DataTable sel_1(string ID);
        int Delete(string ID);
        DataTable sel_2(string ID);
        void Delete_1(string ID);
        int Update_2(string ID);
        int sel_4(string ID);
        int Update_3(string ID);
        int sel_5(string ID);
        int Delete_3(string ID, string UserNum);
        int Update_4(string ID);
        int sel_6(string ID);
        int Update_5(string ID);
        int sel_7(string ID);
        int Update_6(string ID);
        int sel_8(string ID);
        int Delete_4(string ID);
        #endregion

        #region message_write.aspx
        DataTable sel_9(string UserNum);
        string sel_10(string UserNum);
        DataTable sel_11(string u_meGroupNumber);
        int sel_12(string UserNum);
        DataTable sel_15(string UserName);
        void Add(Foosun.Model.message uc);
        int Add_1(string MfID, string Mid, string UserNum, string fileName, string FileUrl, DateTime CreatTime);
        #endregion

        #region Message_read
        int sel_16(string Mid);
        void Update_7(string Mid);
        DataTable sel_17(string Mid);
        string sel_18(string Rec_UserNum);
        DataTable sel_19(string Mids);
        string sel_20(string Midw);
        int clearmessage();
        void clearmessagerecyle();
        #endregion

    }
    public sealed partial class DataAccess
    {
        public static IMessage CreateMessage()
        {
            string className = path + ".Message";
            return (IMessage)Assembly.Load(path).CreateInstance(className);
        }
    }
}