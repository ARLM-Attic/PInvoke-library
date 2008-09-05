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
    public class Photo
    {
        private IPhoto dal;
        public Photo()
        {
            dal = Foosun.DALFactory.DataAccess.CreatePhoto();
        }
        #region photo.aspx
        public DataTable sel(string PhotoalbumID)
        {
            return dal.sel(PhotoalbumID);
        }
        public string sel_1(string PhotoalbumIDs)
        {
            return dal.sel_1(PhotoalbumIDs);
        }
        public DataTable sel_13(string photoID)
        {
            return dal.sel_13(photoID);
        }
        public string sel_14(string PhotoalbumID)
        {
            return dal.sel_14(PhotoalbumID);
        }
        public string sel_20(string PhotoalbumIDs)
        {
            return dal.sel_20(PhotoalbumIDs);
        }
        #endregion

        #region photo_add.aspx
        public DataTable sel_2(string UserNum)
        {
            return dal.sel_2(UserNum);
        }
        public DataTable sel_3()
        {
            return dal.sel_3();
        }
        public int Add(STPhoto Pho, string UserNum, string PhotoalbumID, string PhotoUrl, string PhotoID)
        {
            return dal.Add(Pho, UserNum, PhotoalbumID, PhotoUrl, PhotoID);
        }
        #endregion

        #region photo_del.aspx
        public int Delete(string PhotoID)
        {
            return dal.Delete(PhotoID);
        }
        #endregion

        #region photo_up.aspx
        public DataTable sel_4(string PhotoID)
        {
            return dal.sel_4(PhotoID);
        }
        public string sel_5(string PhotoalbumID)
        {
            return dal.sel_5(PhotoalbumID);
        }
        public string sel_6(string PhotoIDs)
        {
            return dal.sel_6(PhotoIDs);
        }
        public int Update(string PhotoName, DateTime PhotoTime, string PhotoalbumID, string PhotoContent, string PhotoUrl1, string PhotoIDs)
        {
            return dal.Update(PhotoName, PhotoTime, PhotoalbumID, PhotoContent, PhotoUrl1, PhotoIDs);
        }
        #endregion

        #region Photoalbum.aspx
        public DataTable sel_7(string UserNum)
        {
            return dal.sel_7(UserNum);
        }
        public int Add_1(STPhotoalbum Pb, string UserNum)
        {
            return dal.Add_1(Pb, UserNum);
        }
        #endregion

        #region Photoalbum_up.aspx
        public DataTable sel_8(string PhotoalbumID)
        {
            return dal.sel_8(PhotoalbumID);
        }
        public string sel_9(string ClassID)
        {
            return dal.sel_9(ClassID);
        }
        public int Update_1(string PhotoalbumName, string PhotoalbumJurisdiction, string ClassID, DateTime Creatime, string PhotoalbumIDs)
        {
            return dal.Update_1(PhotoalbumName, PhotoalbumJurisdiction, ClassID, Creatime, PhotoalbumIDs);
        }
        public string sel_10()
        {
            return dal.sel_10();
        }
        public int Update_2(string newpwds, string PhotoalbumIDs)
        {
            return dal.Update_2(newpwds, PhotoalbumIDs);
        }
        #endregion

        #region Photoalbumlist.aspx
        public string sel_11(string ID)
        {
            return dal.sel_11(ID);
        }
        public int Delete_1(string ID)
        {
            return dal.Delete_1(ID);
        }
        public int Delete_2(string ID)
        {
            return dal.Delete_2(ID);
        }
        public string sel_12(string UserNum)
        {
            return dal.sel_12(UserNum);
        }
        public int sel_19(string ID)
        {
            return dal.sel_19(ID);
        }
        public DataTable GetPage(string UserNum, string ClassID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetPage(UserNum, ClassID, PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }
        #endregion

        #region photoclass.aspx
        public int Delete_3(string ID)
        {
            return dal.Delete_3(ID);
        }
        public int Delete_4(string ID)
        {
            return dal.Delete_4(ID);
        }
        public DataTable sel_15(string ID)
        {
            return dal.sel_15(ID);
        }
        public DataTable sel_16()
        {
            return dal.sel_16();
        }
        #endregion

        #region  photoclass_add.aspx
        public int Add_2(string ClassName, string ClassID, DateTime Creatime, string UserNum, int isDisclass, string DisID)
        {
            return dal.Add_2(ClassName, ClassID, Creatime, UserNum, isDisclass, DisID);
        }
        #endregion

        #region photoclass_up.aspx
        public string sel_17(string ClassID)
        {
            return dal.sel_17(ClassID);
        }
        public int Update_3(string ClassName, DateTime Creatime, string ClassIDs)
        {
            return dal.Update_3(ClassName, Creatime, ClassIDs);
        }
        #endregion

        public DataTable sel_18(string PhotoalbumID)
        {
            return dal.sel_18(PhotoalbumID);
        }

    }
}
