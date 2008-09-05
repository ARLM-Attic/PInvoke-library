using System.Data;
using System.Data.SqlClient;
using Foosun.DALFactory;
using Foosun.Model;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class sys : DbBase, Isys
    {
        /// <summary>
        /// 检查新闻表数量
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableRecord()
        {
            string Sql = "Select id From " + Pre + "sys_NewsIndex";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 检查新闻表是否存在
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public DataTable GetTableExsit(string TableName)
        {
            string Sql = "Select tableName From " + Pre + "sys_NewsIndex Where tableName='" + TableName + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 插入表记录
        /// </summary>
        /// <param name="TableName"></param>
        public void InsertTableLab(string TableName)
        {
            string Sql = "Insert Into " + Pre + "sys_NewsIndex(TableName,CreatTime)Values('" + TableName + "','" + System.DateTime.Now + "')";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 创建新表数据结构
        /// </summary>
        /// <param name="TableName"></param>
        public void CreatTableData(string TableName)
        {
            string Sql = " CREATE TABLE [dbo].[" + TableName + "](" +
                        "[Id] [int] IDENTITY (1, 1) NOT NULL ," +
                        "[NewsID] [nvarchar] (12) NOT NULL ," +
                        "[NewsType] [tinyint] NOT NULL ," +
                        "[OrderID] [tinyint] NOT NULL ," +
                        "[NewsTitle] [nvarchar] (100) NOT NULL ," +
                        "[sNewsTitle] [nvarchar] (100) NULL ," +
                        "[TitleColor] [nvarchar] (10) NULL ," +
                        "[TitleITF] [tinyint] NULL ," +
                        "[TitleBTF] [tinyint] NULL ," +
                        "[CommLinkTF] [tinyint] NULL ," +
                        "[SubNewsTF] [tinyint] NULL ," +
                        "[URLaddress] [nvarchar] (200) NULL ," +
                        "[PicURL] [nvarchar] (200) NULL ," +
                        "[SPicURL] [nvarchar] (200) NULL ," +
                        "[ClassID] [nvarchar] (12) NULL ," +
                        "[SpecialID] [nvarchar] (20) NULL ," +
                        "[Author] [nvarchar] (100) NULL ," +
                        "[Souce] [nvarchar] (100) NULL ," +
                        "[Tags] [nvarchar] (100) NULL ," +
                        "[NewsProperty] [nvarchar] (30) NULL ," +
                        "[NewsPicTopline] [tinyint] NULL ," +
                        "[Templet] [nvarchar] (200) NULL ," +
                        "[Content] [ntext] NULL ," +
                        "[Metakeywords] [nvarchar] (200) NULL ," +
                        "[Metadesc] [nvarchar] (200) NULL ," +
                        "[naviContent] [nvarchar](255) NULL ," +
                        "[Click] [int] NULL ," +
                        "[CreatTime] [datetime] NULL ," +
                        "[EditTime] [datetime] NULL ," +
                        "[SavePath] [nvarchar] (200) NULL ," +
                        "[FileName] [nvarchar] (100) NULL ," +
                        "[FileEXName] [nvarchar] (6) NULL ," +
                        "[isDelPoint] [tinyint] NULL ," +
                        "[Gpoint] [int] NULL ," +
                        "[iPoint] [int] NULL ," +
                        "[GroupNumber] [ntext] NULL ," +
                        "[ContentPicTF] [tinyint] NULL ," +
                        "[ContentPicURL] [nvarchar] (200) NULL ," +
                        "[ContentPicSize] [nvarchar] (10) NULL ," +
                        "[CommTF] [tinyint] NULL ," +
                        "[DiscussTF] [tinyint] NULL ," +
                        "[TopNum] [int] NULL ," +
                        "[VoteTF] [tinyint] NULL ," +
                        "[CheckStat] [nvarchar] (10) NULL ," +
                        "[isLock] [tinyint] NULL ," +
                        "[isRecyle] [tinyint] NULL ," +
                        "[SiteID] [nvarchar] (12) NULL ," +
                        "[DataLib] [nvarchar] (20) NULL ," +
                        "[DefineID] [tinyint] NULL ," +
                        "[isVoteTF] [tinyint] NULL ," +
                        "[Editor] [nvarchar] (18) NULL ," +
                        "[isConstr] [tinyint] NULL ," +
                        "[vURL] [nvarchar] (200) NULL ," +//视频地址
                        "[isFiles] [tinyint] NULL ," +
                        "[isHtml] [tinyint] NULL " +
                        ") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
            Sql += "ALTER TABLE [dbo].[" + TableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + TableName + "] PRIMARY KEY  CLUSTERED([Id])  ON [PRIMARY] ";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 检查新闻表是否存在
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public DataTable GetTableList()
        {
            string Sql = "select id,TableName,creattime From " + Pre + "sys_NewsIndex order by id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        #region 常规管
        /// <summary>
        /// 删除单个记录
        /// </summary>
        /// <param name="TableName"></param>
        public void General_M_Del(int Gid)
        {
            string Sql = "Delete From " + Pre + "News_Gen where id=" + Gid + " " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        /// <summary>
        /// 锁定单个记录
        /// </summary>
        /// <param name="TableName"></param>
        public void General_M_Suo(int Gid)
        {
            string Sql = "Update " + Pre + "News_Gen Set isLock=1 where id=" + Gid + " " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 解锁单个记录
        /// </summary>
        /// <param name="TableName"></param>
        public void General_M_UnSuo(int Gid)
        {
            string Sql = "Update " + Pre + "News_Gen Set isLock=0 where id=" + Gid + " " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 删除单个记录
        /// </summary>
        /// <param name="TableName"></param>
        public void General_DelAll()
        {
            string Sql = "Delete From " + Pre + "News_Gen where 1=1 " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public DataTable GetGeneralRecord(string Cname)
        {
            string Sql = "Select Cname,gType From " + Pre + "News_Gen Where Cname='" + Cname + "' " + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 插入表记录
        /// </summary>
        /// <param name="TableName"></param>
        public void insertGeneral(string _Sel_Type, string _Name, string _LinkUrl, string _Email)
        {
            string Sql = "Insert Into " + Pre + "news_Gen(gType,Cname,URL,EmailURL,SiteID) Values(" + _Sel_Type + ",'" + _Name + "','" + _LinkUrl + "','" + _Email + "','" + Foosun.Global.Current.SiteID + "')";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 修改表记录
        /// </summary>
        /// <param name="TableName"></param>
        public void UpdateGeneral(string _Sel_Type, string _Name, string _LinkUrl, string _Email, int GID)
        {
            string Sql = "Update " + Pre + "news_Gen Set gType=" + _Sel_Type + ",Cname='" + _Name + "',EmailURL='" + _Email + "',URL='" + _LinkUrl + "' where id=" + GID + " and SiteID = " + Foosun.Global.Current.SiteID + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 获取某个常规管理的记录
        /// </summary>
        /// <param name="GID"></param>
        /// <returns></returns>
        public DataTable getGeneralIdInfo(int GID)
        {
            string Sql = "Select id,Cname,gType,URL,EmailURL From " + Pre + "news_Gen where id=" + GID + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        #endregion 常规管理

        #region 参数设置

        #region 初始参数设置初始化
        public DataTable UserGroup()//取会员组
        {
            string Sql = "Select GroupNumber,GroupName From " + Pre + "user_Group";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable UserReg()//会员注册
        {
            string Str_CheckSql = "Select regItem,returnemail,returnmobile From " + Pre + "sys_PramUser";
            return DbHelper.ExecuteTable(CommandType.Text, Str_CheckSql, null);
        }
        public DataTable BasePramStart()//初始基本参数设置
        {
            string Str_StartSql = "Select * From " + Pre + "Sys_Param";
            return DbHelper.ExecuteTable(CommandType.Text, Str_StartSql, null);
        }
        public DataTable FtpRss()//FTP，RSS初始化
        {
            string Str_StartSql = "Select * From " + Pre + "sys_Pramother";
            return DbHelper.ExecuteTable(CommandType.Text, Str_StartSql, null);
        }
        public DataTable UserPram()//会员参数基本设置
        {
            string Str_StartSql = "Select * From " + Pre + "Sys_PramUser";
            return DbHelper.ExecuteTable(CommandType.Text, Str_StartSql, null);
        }
        public DataTable UserLeavel()//会员等级设置
        {
            string Str_LeavlStartSql = "Select * From " + Pre + "Sys_UserLevel";
            return DbHelper.ExecuteTable(CommandType.Text, Str_LeavlStartSql, null);
        }
        public DataTable WaterStart()//水印参数初始化
        {
            string Str_StartSql = "Select * From " + Pre + "Sys_ParmPrint";
            return DbHelper.ExecuteTable(CommandType.Text, Str_StartSql, null);
        }
        #endregion

        #region 更新参数表
        public int Update_BaseInfo(STsys_param sys)//保存基本参数设置
        {
            string Str_InSql = "Update " + Pre + "Sys_Param Set SiteName=@Str_SiteName ,SiteDomain=@Str_SiteDomain,IndexTemplet=@Str_IndexTemplet,IndexFileName=@Str_Txt_IndexFileName,FileEXName=@Str_FileEXName,ReadNewsTemplet=@Str_ReadNewsTemplet,ClassListTemplet=@Str_ClassListTemplet,SpecialTemplet=@Str_SpecialTemplet,ReadType=@readtypef,LoginTimeOut=@Str_LoginTimeOut,Email=@Str_Email,LinkType=@linktypef,CopyRight=@Str_BaseCopyRight,CheckInt=@Str_CheckInt,UnLinkTF=@picc,LenSearch=@Str_LenSearch,CheckNewsTitle=@chetitle,CollectTF=@collect,SaveClassFilePath=@Str_SaveClassFilePath,SaveIndexPage=@Str_SaveIndexPage,SaveNewsFilePath=@Str_SaveNewsFilePath,SaveNewsDirPath=@Str_SaveNewsDirPath,Pram_Index=@Str_Pram_Index,InsertPicPosition=@InsertPicPosition,HistoryNum=@HistoryNum";
            SqlParameter[] parm = GetBaseInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSql, parm);
        }
        private SqlParameter[] GetBaseInfo(STsys_param sys)
        {
            SqlParameter[] parm = new SqlParameter[25];
            parm[0] = new SqlParameter("@Str_SiteName", SqlDbType.NVarChar, 50);
            parm[0].Value = sys.Str_SiteName;
            parm[1] = new SqlParameter("@Str_SiteDomain", SqlDbType.NVarChar, 100);
            parm[1].Value = sys.Str_SiteDomain;
            parm[2] = new SqlParameter("@Str_IndexTemplet", SqlDbType.NVarChar, 200);
            parm[2].Value = sys.Str_IndexTemplet;
            parm[3] = new SqlParameter("@Str_Txt_IndexFileName", SqlDbType.NVarChar, 20);
            parm[3].Value = sys.Str_Txt_IndexFileName;
            parm[4] = new SqlParameter("@Str_FileEXName", SqlDbType.NVarChar, 30);
            parm[4].Value = sys.Str_FileEXName;
            parm[5] = new SqlParameter("@Str_ReadNewsTemplet", SqlDbType.NVarChar, 200);
            parm[5].Value = sys.Str_ReadNewsTemplet;
            parm[6] = new SqlParameter("@Str_ClassListTemplet", SqlDbType.NVarChar, 200);
            parm[6].Value = sys.Str_ClassListTemplet;
            parm[7] = new SqlParameter("@Str_SpecialTemplet", SqlDbType.NVarChar, 200);
            parm[7].Value = sys.Str_SpecialTemplet;
            parm[8] = new SqlParameter("@readtypef", SqlDbType.TinyInt);
            parm[8].Value = sys.readtypef;
            parm[9] = new SqlParameter("@Str_LoginTimeOut", SqlDbType.Int, 4);
            parm[9].Value = sys.Str_LoginTimeOut;
            parm[10] = new SqlParameter("@Str_Email", SqlDbType.NVarChar, 150);
            parm[10].Value = sys.Str_Email;
            parm[11] = new SqlParameter("@linktypef", SqlDbType.TinyInt);
            parm[11].Value = sys.linktypef;
            parm[12] = new SqlParameter("@Str_BaseCopyRight", SqlDbType.NText);
            parm[12].Value = sys.Str_BaseCopyRight;
            parm[13] = new SqlParameter("@Str_CheckInt", SqlDbType.TinyInt);
            parm[13].Value = sys.Str_CheckInt;
            parm[14] = new SqlParameter("@picc", SqlDbType.TinyInt);
            parm[14].Value = sys.picc;
            parm[15] = new SqlParameter("@Str_LenSearch", SqlDbType.NVarChar, 8);
            parm[15].Value = sys.Str_LenSearch;
            parm[16] = new SqlParameter("@chetitle", SqlDbType.TinyInt);
            parm[16].Value = sys.chetitle;
            parm[17] = new SqlParameter("@collect", SqlDbType.TinyInt);
            parm[17].Value = sys.collect;
            parm[18] = new SqlParameter("@Str_SaveClassFilePath", SqlDbType.NVarChar, 200);
            parm[18].Value = sys.Str_SaveClassFilePath;
            parm[19] = new SqlParameter("@Str_SaveIndexPage", SqlDbType.NVarChar, 100);
            parm[19].Value = sys.Str_SaveIndexPage;
            parm[20] = new SqlParameter("@Str_SaveNewsFilePath", SqlDbType.NVarChar, 200);
            parm[20].Value = sys.Str_SaveNewsFilePath;
            parm[21] = new SqlParameter("@Str_SaveNewsDirPath", SqlDbType.NVarChar, 100);
            parm[21].Value = sys.Str_SaveNewsDirPath;
            parm[22] = new SqlParameter("@Str_Pram_Index", SqlDbType.Int, 4);
            parm[22].Value = sys.Str_Pram_Index;
            parm[23] = new SqlParameter("@InsertPicPosition", SqlDbType.NVarChar, 20);
            parm[23].Value = sys.InsertPicPosition;
            parm[24] = new SqlParameter("@HistoryNum", SqlDbType.Int, 4);
            parm[24].Value = sys.HistoryNum;
            return parm;
        }
        #endregion

        #region 更新会员参数表
        #region 会员注册参数SQL语句
        public int Update_UserRegInfo(STsys_param sys)//保存基本参数设置
        {
            string SQL = "update " + Pre + "sys_PramUser set regItem=@strContent,returnemail=@returnemail,returnmobile=@returnmobile";
            SqlParameter[] parm = GetUserRegInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, SQL, parm);
        }
        private SqlParameter[] GetUserRegInfo(STsys_param sys)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@strContent", SqlDbType.NText);
            parm[0].Value = sys.strContent;
            parm[1] = new SqlParameter("@returnemail", SqlDbType.TinyInt);
            parm[1].Value = sys.returnemail;
            parm[2] = new SqlParameter("@returnmobile", SqlDbType.TinyInt);
            parm[2].Value = sys.returnmobile;
            return parm;
        }
        #endregion

        #region 会员基本参数设置
        public int Update_UserInfo(STsys_param sys)//保存基本参数设置
        {
            string Str_InSql = "Update " + Pre + "Sys_PramUser Set RegGroupNumber=@Str_RegGroupNumber,ConstrTF=@constrr,RegTF=@reg,UserLoginCodeTF=@code,CommCodeTF=@diss,SendMessageTF=@senemessage,UnRegCommTF=@n,CommHTMLLoad=@htmls,Commfiltrchar=@Str_Commfiltrchar,IPLimt=@Str_IPLimt,GpointName=@Str_GpointName,LoginLock=@Str_LoginLock,setPoint=@Str_setPoint,RegContent=@Str_RegContent,GhClass=@ghclass,cPointParam=@Str_cPointParam,aPointparam=@Str_aPointparam,CommCheck=@CommCheck";
            SqlParameter[] parm = GetUserInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSql, parm);
        }
        private SqlParameter[] GetUserInfo(STsys_param sys)
        {
            SqlParameter[] parm = new SqlParameter[18];
            parm[0] = new SqlParameter("@Str_RegGroupNumber", SqlDbType.NVarChar, 20);
            parm[0].Value = sys.Str_RegGroupNumber;
            parm[1] = new SqlParameter("@constrr", SqlDbType.TinyInt);
            parm[1].Value = sys.constrr;
            parm[2] = new SqlParameter("@reg", SqlDbType.TinyInt);
            parm[2].Value = sys.reg;
            parm[3] = new SqlParameter("@code", SqlDbType.TinyInt);
            parm[3].Value = sys.code;
            parm[4] = new SqlParameter("@diss", SqlDbType.TinyInt);
            parm[4].Value = sys.diss;
            parm[5] = new SqlParameter("@senemessage", SqlDbType.TinyInt);
            parm[5].Value = sys.senemessage;
            parm[6] = new SqlParameter("@n", SqlDbType.TinyInt);
            parm[6].Value = sys.n;
            parm[7] = new SqlParameter("@htmls", SqlDbType.TinyInt);
            parm[7].Value = sys.htmls;
            parm[8] = new SqlParameter("@Str_Commfiltrchar", SqlDbType.NText);
            parm[8].Value = sys.Str_Commfiltrchar;
            parm[9] = new SqlParameter("@Str_IPLimt", SqlDbType.NText);
            parm[9].Value = sys.Str_IPLimt;
            parm[10] = new SqlParameter("@Str_GpointName", SqlDbType.NVarChar, 10);
            parm[10].Value = sys.Str_GpointName;
            parm[11] = new SqlParameter("@Str_LoginLock", SqlDbType.NVarChar, 10);
            parm[11].Value = sys.Str_LoginLock;
            parm[12] = new SqlParameter("@Str_setPoint", SqlDbType.NVarChar, 20);
            parm[12].Value = sys.Str_setPoint;
            parm[13] = new SqlParameter("@Str_RegContent", SqlDbType.NText);
            parm[13].Value = sys.Str_RegContent;
            parm[14] = new SqlParameter("@ghclass", SqlDbType.TinyInt);
            parm[14].Value = sys.ghclass;
            parm[15] = new SqlParameter("@Str_cPointParam", SqlDbType.NVarChar, 30);
            parm[15].Value = sys.Str_cPointParam;
            parm[16] = new SqlParameter("@Str_aPointparam", SqlDbType.NVarChar, 30);
            parm[16].Value = sys.Str_aPointparam;
            parm[17] = new SqlParameter("@CommCheck", SqlDbType.TinyInt, 1);
            parm[17].Value = sys.CommCheck;
            return parm;
        }
        #endregion

        #region 会员等级设置
        public int Update_Leavel(STsys_param sys, int k)//保存等级设置
        {
            string Str_InSqlLeavel = "Update " + Pre + "Sys_UserLevel Set LTitle=@Str_LtitleArr,Lpicurl=@Str_LpicurlArr,iPoint=@Str_iPointArr  where id=" + k + "";
            SqlParameter[] parm = GetUserleavelInfo(sys, k);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSqlLeavel, parm);
        }
        private SqlParameter[] GetUserleavelInfo(STsys_param sys, int k)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@Str_LtitleArr", SqlDbType.NVarChar, 50);
            parm[0].Value = sys.Str_LtitleArr[k];
            parm[1] = new SqlParameter("@Str_LpicurlArr", SqlDbType.NVarChar, 200);
            parm[1].Value = sys.Str_LpicurlArr[k];
            parm[2] = new SqlParameter("@Str_iPointArr", SqlDbType.Int, 4);
            parm[2].Value = sys.Str_iPointArr[k];
            return parm;
        }
        #endregion
        #endregion

        #region 更新上传
        public int Update_FtpInfo(STsys_param sys)//保存基本参数设置
        {
            string Str_InSql = "Update " + Pre + "Sys_Param Set PicServerTF=@picsa,PicServerDomain=@Str_PicServerDomain,PicUpLoad=@Str_PicUpLoad,UpfilesType=@Str_UpfilesType,UpFilesSize=@Str_UpFilesSize,ReMoteDomainTF=@domainnn,RemoteDomain=@Str_RemoteDomain,RemoteSavePath=@Str_RemoteSavePath,ClassListNum=@Str_ClassListNum,NewsNum=@Str_NewsNum,BatDelNum=@Str_BatDelNum,SpecialNum=@Str_SpecialNum";
            SqlParameter[] parm = GetFtpInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSql, parm);
        }
        private SqlParameter[] GetFtpInfo(STsys_param sys)
        {
            SqlParameter[] parm = new SqlParameter[12];
            parm[0] = new SqlParameter("@picsa", SqlDbType.TinyInt);
            parm[0].Value = sys.picsa;
            parm[1] = new SqlParameter("@Str_PicServerDomain", SqlDbType.NText);
            parm[1].Value = sys.Str_PicServerDomain;
            parm[2] = new SqlParameter("@Str_PicUpLoad", SqlDbType.NVarChar, 200);
            parm[2].Value = sys.Str_PicUpLoad;
            parm[3] = new SqlParameter("@Str_UpfilesType", SqlDbType.NVarChar, 150);
            parm[3].Value = sys.Str_UpfilesType;
            parm[4] = new SqlParameter("@Str_UpFilesSize", SqlDbType.Int, 4);
            parm[4].Value = sys.Str_UpFilesSize;
            parm[5] = new SqlParameter("@domainnn", SqlDbType.TinyInt);
            parm[5].Value = sys.domainnn;
            parm[6] = new SqlParameter("@Str_RemoteDomain", SqlDbType.NVarChar, 100);
            parm[6].Value = sys.Str_RemoteDomain;
            parm[7] = new SqlParameter("@Str_RemoteSavePath", SqlDbType.NVarChar, 200);
            parm[7].Value = sys.Str_RemoteSavePath;
            parm[8] = new SqlParameter("@Str_ClassListNum", SqlDbType.Int, 4);
            parm[8].Value = sys.Str_ClassListNum;
            parm[9] = new SqlParameter("@Str_NewsNum", SqlDbType.Int, 4);
            parm[9].Value = sys.Str_NewsNum;
            parm[10] = new SqlParameter("Str_BatDelNum", SqlDbType.Int, 4);
            parm[10].Value = sys.Str_BatDelNum;
            parm[11] = new SqlParameter("@Str_SpecialNum", SqlDbType.Int, 4);
            parm[11].Value = sys.Str_SpecialNum;
            return parm;
        }
        #endregion

        #region JS
        public int Update_JS(STsys_param sys)//更新JS
        {
            string Str_InSqljs = "Update " + Pre + "Sys_Param Set HotNewsJs=@Str_HotJS,LastNewsJs=@Str_LastJS,RecNewsJS=@Str_RecJS,HotCommJS=@Str_HoMJS,TNewsJS=@Str_TMJS";
            SqlParameter[] parm = GetJSInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSqljs, parm);
        }
        private SqlParameter[] GetJSInfo(STsys_param sys)
        {
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@Str_HotJS", SqlDbType.NVarChar, 200);
            parm[0].Value = sys.Str_HotJS;
            parm[1] = new SqlParameter("@Str_LastJS", SqlDbType.NVarChar, 200);
            parm[1].Value = sys.Str_LastJS;
            parm[2] = new SqlParameter("@Str_RecJS", SqlDbType.NVarChar, 200);
            parm[2].Value = sys.Str_RecJS;
            parm[3] = new SqlParameter("@Str_HoMJS", SqlDbType.NVarChar, 200);
            parm[3].Value = sys.Str_HoMJS;
            parm[4] = new SqlParameter("@Str_TMJS", SqlDbType.NVarChar, 200);
            parm[4].Value = sys.Str_TMJS;
            return parm;
        }
        #endregion

        #region ftp
        public int Update_JFtP(STsys_param sys)//更新JS
        {
            string Str_InSqlftp = "Update " + Pre + "sys_Pramother Set FtpTF=@ftpp,FTPIP=@Str_FTPIP,Ftpport=@Str_Ftpport,FtpUserName=@Str_FtpUserName,FTPPASSword=@Str_FTPPASSword";
            SqlParameter[] parm = GetftpInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSqlftp, parm);
        }
        private SqlParameter[] GetftpInfo(STsys_param sys)
        {
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@ftpp", SqlDbType.Int, 4);
            parm[0].Value = sys.ftpp;
            parm[1] = new SqlParameter("@Str_FTPIP", SqlDbType.NVarChar, 100);
            parm[1].Value = sys.Str_FTPIP;
            parm[2] = new SqlParameter("@Str_Ftpport", SqlDbType.NVarChar, 5);
            parm[2].Value = sys.Str_Ftpport;
            parm[3] = new SqlParameter("@Str_FtpUserName", SqlDbType.NVarChar, 20);
            parm[3].Value = sys.Str_FtpUserName;
            parm[4] = new SqlParameter("@Str_FTPPASSword", SqlDbType.NVarChar, 50);
            parm[4].Value = sys.Str_FTPPASSword;
            return parm;
        }
        #endregion

        #region 水印
        public int Update_Water(STsys_param sys)//更新JS
        {
            string Str_InSql = "Update " + Pre + "Sys_ParmPrint Set PrintTF=@water,PrintPicTF=@Str_PrintPicTF,PrintWord=@Str_PrintWord,Printfontsize=@Str_Printfontsize,Printfontfamily=@Str_Printfontfamily,Printfontcolor=@Str_Printfontcolor,PrintBTF=@Str_PrintBTF,PintPicURL=@Str_PintPicURL,PrintPicsize=@Str_PrintPicsize,PintPictrans=@Str_PintPictrans,PrintPosition=@Str_PrintPosition,PrintSmallTF=@Str_PrintSmallTF,PrintSmallSizeStyle=@Str_PrintSmallSizeStyle,PrintSmallSize=@Str_PrintSmallSize,PrintSmallinv=@Str_PrintSmallinv";
            SqlParameter[] parm = GetwaterInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSql, parm);
        }
        private SqlParameter[] GetwaterInfo(STsys_param sys)
        {
            SqlParameter[] parm = new SqlParameter[15];
            parm[0] = new SqlParameter("@water", SqlDbType.TinyInt);
            parm[0].Value = sys.water;
            parm[1] = new SqlParameter("@Str_PrintPicTF", SqlDbType.TinyInt);
            parm[1].Value = sys.Str_PrintPicTF;
            parm[2] = new SqlParameter("@Str_PrintWord", SqlDbType.NVarChar, 50);
            parm[2].Value = sys.Str_PrintWord;
            parm[3] = new SqlParameter("@Str_Printfontsize", SqlDbType.Int, 4);
            parm[3].Value = sys.Str_Printfontsize;
            parm[4] = new SqlParameter("@Str_Printfontfamily", SqlDbType.NVarChar, 30);
            parm[4].Value = sys.Str_Printfontfamily;
            parm[5] = new SqlParameter("@Str_Printfontcolor", SqlDbType.NVarChar, 10);
            parm[5].Value = sys.Str_Printfontcolor;
            parm[6] = new SqlParameter("@Str_PrintBTF", SqlDbType.TinyInt);
            parm[6].Value = sys.Str_PrintBTF;
            parm[7] = new SqlParameter("@Str_PintPicURL", SqlDbType.NVarChar, 150);
            parm[7].Value = sys.Str_PintPicURL;
            parm[8] = new SqlParameter("@Str_PrintPicsize", SqlDbType.NVarChar, 8);
            parm[8].Value = sys.Str_PrintPicsize;
            parm[9] = new SqlParameter("@Str_PintPictrans", SqlDbType.NVarChar, 20);
            parm[9].Value = sys.Str_PintPictrans;
            parm[10] = new SqlParameter("@Str_PrintPosition", SqlDbType.TinyInt);
            parm[10].Value = sys.Str_PrintPosition;
            parm[11] = new SqlParameter("@Str_PrintSmallTF", SqlDbType.TinyInt);
            parm[11].Value = sys.Str_PrintSmallTF;
            parm[12] = new SqlParameter("@Str_PrintSmallSizeStyle", SqlDbType.TinyInt);
            parm[12].Value = sys.Str_PrintSmallSizeStyle;
            parm[13] = new SqlParameter("@Str_PrintSmallSize", SqlDbType.NVarChar, 8);
            parm[13].Value = sys.Str_PrintSmallSize;
            parm[14] = new SqlParameter("@Str_PrintSmallinv", SqlDbType.NVarChar, 20);
            parm[14].Value = sys.Str_PrintSmallinv;
            return parm;
        }

        /// <summary>
        /// 取得站点水印信息
        /// </summary>
        /// <returns></returns>
        public DataTable ParmPrintInfo()
        {
            string Sql = "Select * From " + Pre + "Sys_ParmPrint";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        #endregion

        #region rss wap
        public int Update_RssWap(STsys_param sys)//更新JS
        {
            string Str_InSql = "Update " + Pre + "sys_Pramother Set RssNum=@Str_RssNum,RssContentNum=@Str_RssContentNum,RssTitle=@Str_RssTitle,RssPicURL=@Str_RssPicURL,WapTF=@wapp,WapPath=@Str_WapPath,WapDomain=@Str_WapDomain,WapLastNum=@Str_WapLastNum";
            SqlParameter[] parm = GetrssrInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSql, parm);
        }
        private SqlParameter[] GetrssrInfo(STsys_param sys)
        {
            SqlParameter[] parm = new SqlParameter[8];
            parm[0] = new SqlParameter("@Str_RssNum", SqlDbType.Int, 4);
            parm[0].Value = sys.Str_RssNum;
            parm[1] = new SqlParameter("@Str_RssContentNum", SqlDbType.Int, 4);
            parm[1].Value = sys.Str_RssContentNum;
            parm[2] = new SqlParameter("@Str_RssTitle", SqlDbType.NVarChar, 50);
            parm[2].Value = sys.Str_RssTitle;
            parm[3] = new SqlParameter("@Str_RssPicURL", SqlDbType.NVarChar, 200);
            parm[3].Value = sys.Str_RssPicURL;
            parm[4] = new SqlParameter("@wapp", SqlDbType.TinyInt);
            parm[4].Value = sys.wapp;
            parm[5] = new SqlParameter("@Str_WapPath", SqlDbType.NVarChar, 50);
            parm[5].Value = sys.Str_WapPath;
            parm[6] = new SqlParameter("@Str_WapDomain", SqlDbType.NVarChar, 50);
            parm[6].Value = sys.Str_WapDomain;
            parm[7] = new SqlParameter("@Str_WapLastNum", SqlDbType.Int, 4);
            parm[7].Value = sys.Str_WapLastNum;
            return parm;
        }
        #endregion

        public DataTable ShowJS1()
        {
            string Str_StartSql = "Select PicServerTF,ReMoteDomainTF From " + Pre + "Sys_Param";//从参数设置表中读出数据并初始化赋值
            return DbHelper.ExecuteTable(CommandType.Text, Str_StartSql, null);
        }
        public DataTable ShoeJs2()
        {
            string Str_StartSqlf = "Select FtpTF,WapTF From " + Pre + "Sys_Pramother";//从其他参数表中去数据
            return DbHelper.ExecuteTable(CommandType.Text, Str_StartSqlf, null);
        }
        public DataTable showJs3()
        {
            string Str_StartSqlp = "Select PrintPicTF,PrintSmallTF,PrintSmallSizeStyle From " + Pre + "Sys_ParmPrint";//从水印参数表中去数据
            return DbHelper.ExecuteTable(CommandType.Text, Str_StartSqlp, null);
        }

        public DataTable JsTemplet1()
        {
            string Str_SelectSql1 = "Select JsID,jsTName From " + Pre + "news_JSTemplet";
            return DbHelper.ExecuteTable(CommandType.Text, Str_SelectSql1, null);
        }
        public DataTable JsTemplet2()
        {
            string Str_SelectSql2 = "Select JsID,jsTName From " + Pre + "news_JSTemplet";
            return DbHelper.ExecuteTable(CommandType.Text, Str_SelectSql2, null);
        }
        public DataTable JsTemplet3()
        {
            string Str_SelectSql3 = "Select JsID,jsTName From " + Pre + "news_JSTemplet";
            return DbHelper.ExecuteTable(CommandType.Text, Str_SelectSql3, null);
        }
        public DataTable JsTemplet4()
        {
            string Str_SelectSql4 = "Select JsID,jsTName From " + Pre + "news_JSTemplet";
            return DbHelper.ExecuteTable(CommandType.Text, Str_SelectSql4, null);
        }
        public DataTable JsTemplet5()
        {
            string Str_SelectSql5 = "Select JsID,jsTName From " + Pre + "news_JSTemplet";
            return DbHelper.ExecuteTable(CommandType.Text, Str_SelectSql5, null);
        }
        #endregion
    }
}
