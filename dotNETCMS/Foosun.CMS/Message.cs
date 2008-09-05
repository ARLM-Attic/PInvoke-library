//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By ZhenJiang.Wang                  ==
//===========================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Foosun.DALFactory;
using Foosun.Model;

namespace Foosun.CMS
{
    public class Message
    {
        private IMessage dal;
        public Message()
        {
            dal = Foosun.DALFactory.DataAccess.CreateMessage();
        }
        #region Message_box.aspx
        public int sel(string ID)
        {
            return dal.sel(ID);
        }
        public int Update(string ID)
        {
            return dal.Update(ID);
        }
        public int sel_3(string ID)
        {
            return dal.sel_3(ID);
        }

        public int Update_1(string ID)
        {
            return dal.Update_1(ID);
        }
        public DataTable sel_1(string ID)
        {
            return dal.sel_1(ID);
        }
        public int Delete(string ID)
        {
            return dal.Delete(ID);
        }
        public DataTable sel_2(string ID)
        {
            return dal.sel_2(ID);
        }

        public void Delete_1(string ID)
        {
            dal.Delete_1(ID);
        }
        public int Update_2(string ID)
        {
            return dal.Update_2(ID);
        }

        public int sel_4(string ID)
        {
            return dal.sel_4(ID);
        }
        public int Update_3(string ID)
        {
            return dal.Update_3(ID);
        }
        public int sel_5(string ID)
        {
            return dal.sel_5(ID);
        }
        public int Delete_3(string ID, string UserNum)
        {
            return dal.Delete_3(ID,UserNum);
        }
        public int Update_4(string ID)
        {
            return dal.Update_4(ID);
        }
        public int sel_6(string ID)
        {
            return dal.sel_6(ID);
        }
        public int Update_5(string ID)
        {
            return dal.Update_5(ID);
        }
        public int sel_7(string ID)
        {
            return dal.sel_7(ID);
        }
        public int Update_6(string ID)
        {
            return dal.Update_6(ID);
        }
        public int sel_8(string ID)
        {
            return dal.sel_8(ID);
        }
        public int Delete_4(string ID)
        {
            return dal.Delete_4(ID);
        }
        #endregion

        #region message_write.aspx
        public DataTable sel_9(string UserNum)
        {
            return dal.sel_9(UserNum);
        }
        public string sel_10(string UserNum)
        {
            return dal.sel_10(UserNum);
        }
        public DataTable sel_11(string u_meGroupNumber)
        {
            return dal.sel_11(u_meGroupNumber);
        }
        public int sel_12(string UserNum)
        {
            return dal.sel_12(UserNum);
        }
        public DataTable sel_15(string UserName)
        {
            return dal.sel_15(UserName);
        }
        public void Add(Foosun.Model.message uc)
        {
            dal.Add(uc);
        }
        public int Add_1(string MfID, string Mid, string UserNum, string fileName, string FileUrl, DateTime CreatTime)
        {
            return dal.Add_1(MfID, Mid, UserNum, fileName, FileUrl, CreatTime);
        }

        public int clearmessage()
        {
            return dal.clearmessage();
        }

        public void clearmessagerecyle()
        {
            dal.clearmessagerecyle();
        }

        #endregion

        #region Message_read
        public int sel_16(string Mid)
        {
            return dal.sel_16(Mid);
        }
        public void Update_7(string Mid)
        {
            dal.Update_7(Mid);
        }
        public DataTable sel_17(string Mid)
        {
            return dal.sel_17(Mid);
        }
        public string sel_18(string Rec_UserNum)
        {
            return dal.sel_18(Rec_UserNum);
        }
        public DataTable sel_19(string Mids)
        {
            return dal.sel_19(Mids);
        }
        public string sel_20(string Midw)
        {
            return dal.sel_20(Midw);
        }
        #endregion
    }
}