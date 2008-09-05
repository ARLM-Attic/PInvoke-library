using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface Isys
    {
        DataTable GetTableRecord();
        DataTable GetTableExsit(string TableName);
        DataTable GetTableList();
        DataTable GetGeneralRecord(string Cname);
        DataTable getGeneralIdInfo(int GID);
        void InsertTableLab(string TableName);
        void CreatTableData(string TableName);
        void General_M_Del(int Gid);
        void General_M_Suo(int Gid);
        void General_M_UnSuo(int Gid);
        void General_DelAll();
        void insertGeneral(string _Sel_Type, string _Name, string _LinkUrl, string _Email);
        void UpdateGeneral(string _Sel_Type, string _Name, string _LinkUrl, string _Email, int GID);
        #region 参数设置
        DataTable UserGroup();
        DataTable UserReg();
        DataTable BasePramStart();
        DataTable FtpRss();
        DataTable UserPram();
        DataTable UserLeavel();
        DataTable WaterStart();
        int Update_BaseInfo(STsys_param sys);
        int Update_UserRegInfo(STsys_param sys);
        int Update_UserInfo(STsys_param sys);
        int Update_Leavel(STsys_param sys, int k);
        int Update_FtpInfo(STsys_param sys);
        int Update_JS(STsys_param sys);
        int Update_JFtP(STsys_param sys);
        int Update_Water(STsys_param sys);
        int Update_RssWap(STsys_param sys);
        DataTable ShowJS1();
        DataTable ShoeJs2();
        DataTable showJs3();
        DataTable JsTemplet1();
        DataTable JsTemplet2();
        DataTable JsTemplet3();
        DataTable JsTemplet4();
        DataTable JsTemplet5();
        #endregion
    }

    public sealed partial class DataAccess
    {
        public static Isys Createsys()
        {
            string className = path + ".sys";
            return (Isys)Assembly.Load(path).CreateInstance(className);
        }
    }
}
