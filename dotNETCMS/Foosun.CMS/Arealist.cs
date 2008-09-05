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
    public class Arealist
    {
        private IArealist dal;
        public Arealist()
        {
            dal = Foosun.DALFactory.DataAccess.CreateArealist();
        }
        #region arealist.aspx
        public DataTable sel(string Cid)
        {
            return dal.sel(Cid);
        }
        public string sel_1(string ID)
        {
            return dal.sel_1(ID);
        }
        public int Delete(string ID)
        {
            return dal.Delete(ID);
        }
        public int Delete_2(string ID)
        {
            return dal.Delete_2(ID);
        }
        #endregion

        #region arealist_add.aspx
        public int Add(string Cid, string cityName, DateTime creatTime, int orderID)
        {
            return dal.Add(Cid, cityName, creatTime, orderID);
        }
        public DataTable sel_2()
        {
            return dal.sel_2();
        }
        #endregion

        #region Arealist.cs
        public DataTable sel_3()
        {
            return dal.sel_3();
        }
        public int Add_1(string Pid, string Cid, string cityName, DateTime creatTime,int orderID)
        {
            return dal.Add_1(Pid, Cid, cityName, creatTime, orderID);
        }
        public DataTable sel_4()
        {
            return dal.sel_4();
        }

        public int sel_nameTF(string aName)
        {
            return dal.sel_nameTF(aName);
        }

        #endregion

        #region arealist_City.aspx
        public int Delete_3(string ID)
        {
            return dal.Delete_3(ID);
        }
        #endregion

        #region arealist_upc.aspx
        public DataTable sel_5()
        {
            return dal.sel_5();
        }
        public DataTable sel_6(string pname)
        {
            return dal.sel_6(pname);
        }
        public int Update(string Pid, string cityName, DateTime creatTime, string cids,int OrderID)
        {
            return dal.Update(Pid, cityName, creatTime, cids, OrderID);
        }
        #endregion

        #region arealist_upp.aspx
        public DataTable  sel_7(string Cid)
        {
            return dal.sel_7(Cid);
        }
        public int Update_1(string cityName, DateTime creatTime, string Cids,int OrderID)
        {
            return dal.Update_1(cityName, creatTime, Cids, OrderID);
        }
        #endregion
    }
}