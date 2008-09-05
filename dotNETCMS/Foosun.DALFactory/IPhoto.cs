using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IPhoto
    {
        #region photo.aspx
        DataTable sel(string PhotoalbumID);
        string sel_1(string PhotoalbumIDs);
        DataTable sel_13(string photoID);
        string sel_14(string PhotoalbumID);
        string sel_20(string PhotoalbumIDs);
        #endregion

        #region photo_add.aspx
        DataTable sel_2(string UserNum);
        DataTable sel_3();
        int Add(STPhoto Pho, string UserNum, string PhotoalbumID, string PhotoUrl, string PhotoID);
        #endregion

        #region photo_del.aspx
        int Delete(string PhotoID);
        #endregion

        #region photo_up.aspx
        DataTable sel_4(string PhotoID);
        string sel_5(string PhotoalbumID);
        string sel_6(string PhotoIDs);
        int Update(string PhotoName, DateTime PhotoTime, string PhotoalbumID, string PhotoContent, string PhotoUrl1, string PhotoIDs);
        #endregion

        #region Photoalbum.aspx
        DataTable sel_7(string UserNum);
        int Add_1(STPhotoalbum Pb, string UserNum);
        #endregion

        #region Photoalbum_up.aspx
        DataTable sel_8(string PhotoalbumID);
        string sel_9(string ClassID);
        int Update_1(string PhotoalbumName, string PhotoalbumJurisdiction, string ClassID, DateTime Creatime, string PhotoalbumIDs);
        string sel_10();
        int Update_2(string newpwds, string PhotoalbumIDs);
        #endregion

        #region Photoalbumlist.aspx
        string sel_11(string ID);
        int Delete_1(string ID);
        int Delete_2(string ID);
        string sel_12(string UserNum);
        int sel_19(string ID);
        DataTable GetPage(string UserNum, string ClassID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        #endregion

        #region photoclass.aspx
        int Delete_3(string ID);
        int Delete_4(string ID);
        DataTable sel_15(string ID);
        DataTable sel_16();
        #endregion

        #region  photoclass_add.aspx
        int Add_2(string ClassName, string ClassID, DateTime Creatime, string UserNum, int isDisclass, string DisID);
        #endregion

        #region photoclass_up.aspx
        string sel_17(string ClassID);
        int Update_3(string ClassName, DateTime Creatime, string ClassIDs);
        #endregion

        DataTable sel_18(string PhotoalbumID);
    }
    public sealed partial class DataAccess
    {
        public static IPhoto CreatePhoto()
        {
            string className = path + ".Photo";
            return (IPhoto)Assembly.Load(path).CreateInstance(className);
        }
    }
}
