using System.Data;
using System.Data.OleDb;
using Foosun.DALFactory;
using Foosun.Model;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
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
            string Sql = " CREATE TABLE [" + TableName + "](" +
                        "[Id] COUNTER(1, 1) CONSTRAINT PK_" + TableName + " PRIMARY KEY," +
                        "[NewsID] varchar (12) NOT NULL ," +
                        "[NewsType] byte NOT NULL ," +
                        "[OrderID] byte NOT NULL ," +
                        "[NewsTitle] varchar (100) NOT NULL ," +
                        "[sNewsTitle] varchar (100) NULL ," +
                        "[TitleColor] varchar (10) NULL ," +
                        "[TitleITF] byte NULL ," +
                        "[TitleBTF] byte NULL ," +
                        "[CommLinkTF] byte NULL ," +
                        "[SubNewsTF] byte NULL ," +
                        "[URLaddress] varchar (200) NULL ," +
                        "[PicURL] varchar (200) NULL ," +
                        "[SPicURL] varchar (200) NULL ," +
                        "[ClassID] varchar (12) NULL ," +
                        "[SpecialID] varchar (20) NULL ," +
                        "[Author] varchar (100) NULL ," +
                        "[Souce] varchar (100) NULL ," +
                        "[Tags] varchar (100) NULL ," +
                        "[NewsProperty] varchar (30) NULL ," +
                        "[NewsPicTopline] byte NULL ," +
                        "[Templet] varchar (200) NULL ," +
                        "[Content] text NULL ," +
                        "[Metakeywords] varchar (200) NULL ," +
                        "[Metadesc] varchar (200) NULL ," +
                        "[naviContent] varchar(255) NULL ," +
                        "[Click] [int] NULL ," +
                        "[CreatTime] datetime NULL ," +
                        "[EditTime] datetime NULL ," +
                        "[SavePath] varchar (200) NULL ," +
                        "[FileName] varchar (100) NULL ," +
                        "[FileEXName] varchar (6) NULL ," +
                        "[isDelPoint] byte NULL ," +
                        "[Gpoint] [int] NULL ," +
                        "[iPoint] [int] NULL ," +
                        "[GroupNumber] text NULL ," +
                        "[ContentPicTF] byte NULL ," +
                        "[ContentPicURL] varchar (200) NULL ," +
                        "[ContentPicSize] varchar (10) NULL ," +
                        "[CommTF] byte NULL ," +
                        "[DiscussTF] byte NULL ," +
                        "[TopNum] [int] NULL ," +
                        "[VoteTF] byte NULL ," +
                        "[CheckStat] varchar (10) NULL ," +
                        "[isLock] byte NULL ," +
                        "[isRecyle] byte NULL ," +
                        "[SiteID] varchar (12) NULL ," +
                        "[DataLib] varchar (20) NULL ," +
                        "[DefineID] byte NULL ," +
                        "[isVoteTF] byte NULL ," +
                        "[Editor] varchar (18) NULL ," +
                        "[isConstr] byte NULL ," +
                        "[vURL] varchar (200) NULL ," +//视频地址
                        "[isFiles] byte NULL ," +
                        "[isHtml] byte NULL " +
                       // ") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                        ") ";
            //Sql += "ALTER TABLE [dbo].[" + TableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + TableName + "] PRIMARY KEY  CLUSTERED([Id])  ON [PRIMARY] ";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            Sql = "insert into fs_tables(tabname) values('" + TableName + "')";
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
            string Sql = "Insert Into " + Pre + "news_Gen(gType,Cname,URL,EmailURL,SiteID,isLock) Values(" + _Sel_Type + ",'" + _Name + "','" + _LinkUrl + "','" + _Email + "','" + Foosun.Global.Current.SiteID + "',0)";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 修改表记录
        /// </summary>
        /// <param name="TableName"></param>
        public void UpdateGeneral(string _Sel_Type, string _Name, string _LinkUrl, string _Email, int GID)
        {
            string Sql = "Update " + Pre + "news_Gen Set gType=" + _Sel_Type + ",Cname='" + _Name + "',EmailURL='" + _Email + "',URL='" + _LinkUrl + "' where id=" + GID + " and SiteID = '" + Foosun.Global.Current.SiteID + "'";
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
            OleDbParameter[] parm = GetBaseInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSql, parm);
        }
        private OleDbParameter[] GetBaseInfo(STsys_param sys)
        {
            OleDbParameter[] parm = new OleDbParameter[25];
            parm[0] = new OleDbParameter("@Str_SiteName", OleDbType.VarWChar, 50);
            parm[0].Value = sys.Str_SiteName;
            parm[1] = new OleDbParameter("@Str_SiteDomain", OleDbType.VarWChar, 100);
            parm[1].Value = sys.Str_SiteDomain;
            parm[2] = new OleDbParameter("@Str_IndexTemplet", OleDbType.VarWChar, 200);
            parm[2].Value = sys.Str_IndexTemplet;
            parm[3] = new OleDbParameter("@Str_Txt_IndexFileName", OleDbType.VarWChar, 20);
            parm[3].Value = sys.Str_Txt_IndexFileName;
            parm[4] = new OleDbParameter("@Str_FileEXName", OleDbType.VarWChar, 30);
            parm[4].Value = sys.Str_FileEXName;
            parm[5] = new OleDbParameter("@Str_ReadNewsTemplet", OleDbType.VarWChar, 200);
            parm[5].Value = sys.Str_ReadNewsTemplet;
            parm[6] = new OleDbParameter("@Str_ClassListTemplet", OleDbType.VarWChar, 200);
            parm[6].Value = sys.Str_ClassListTemplet;
            parm[7] = new OleDbParameter("@Str_SpecialTemplet", OleDbType.VarWChar, 200);
            parm[7].Value = sys.Str_SpecialTemplet;
            parm[8] = new OleDbParameter("@readtypef", OleDbType.Integer);
            parm[8].Value = sys.readtypef;
            parm[9] = new OleDbParameter("@Str_LoginTimeOut", OleDbType.Integer, 4);
            parm[9].Value = sys.Str_LoginTimeOut;
            parm[10] = new OleDbParameter("@Str_Email", OleDbType.VarWChar, 150);
            parm[10].Value = sys.Str_Email;
            parm[11] = new OleDbParameter("@linktypef", OleDbType.Integer);
            parm[11].Value = sys.linktypef;
            parm[12] = new OleDbParameter("@Str_BaseCopyRight", OleDbType.VarWChar);
            parm[12].Value = sys.Str_BaseCopyRight;
            parm[13] = new OleDbParameter("@Str_CheckInt", OleDbType.Integer);
            parm[13].Value = sys.Str_CheckInt;
            parm[14] = new OleDbParameter("@picc", OleDbType.Integer);
            parm[14].Value = sys.picc;
            parm[15] = new OleDbParameter("@Str_LenSearch", OleDbType.VarWChar, 8);
            parm[15].Value = sys.Str_LenSearch;
            parm[16] = new OleDbParameter("@chetitle", OleDbType.Integer);
            parm[16].Value = sys.chetitle;
            parm[17] = new OleDbParameter("@collect", OleDbType.Integer);
            parm[17].Value = sys.collect;
            parm[18] = new OleDbParameter("@Str_SaveClassFilePath", OleDbType.VarWChar, 200);
            parm[18].Value = sys.Str_SaveClassFilePath;
            parm[19] = new OleDbParameter("@Str_SaveIndexPage", OleDbType.VarWChar, 100);
            parm[19].Value = sys.Str_SaveIndexPage;
            parm[20] = new OleDbParameter("@Str_SaveNewsFilePath", OleDbType.VarWChar, 200);
            parm[20].Value = sys.Str_SaveNewsFilePath;
            parm[21] = new OleDbParameter("@Str_SaveNewsDirPath", OleDbType.VarWChar, 100);
            parm[21].Value = sys.Str_SaveNewsDirPath;
            parm[22] = new OleDbParameter("@Str_Pram_Index", OleDbType.Integer, 4);
            parm[22].Value = sys.Str_Pram_Index;
            parm[23] = new OleDbParameter("@InsertPicPosition", OleDbType.VarWChar, 20);
            parm[23].Value = sys.InsertPicPosition;
            parm[24] = new OleDbParameter("@HistoryNum", OleDbType.Integer, 4);
            parm[24].Value = sys.HistoryNum;
            return parm;
        }
        #endregion

        #region 更新会员参数表
        #region 会员注册参数SQL语句
        public int Update_UserRegInfo(STsys_param sys)//保存基本参数设置
        {
            string SQL = "update " + Pre + "sys_PramUser set regItem=@strContent,returnemail=@returnemail,returnmobile=@returnmobile";
            OleDbParameter[] parm = GetUserRegInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, SQL, parm);
        }
        private OleDbParameter[] GetUserRegInfo(STsys_param sys)
        {
            OleDbParameter[] parm = new OleDbParameter[3];
            parm[0] = new OleDbParameter("@strContent", OleDbType.VarWChar);
            parm[0].Value = sys.strContent;
            parm[1] = new OleDbParameter("@returnemail", OleDbType.Integer);
            parm[1].Value = sys.returnemail;
            parm[2] = new OleDbParameter("@returnmobile", OleDbType.Integer);
            parm[2].Value = sys.returnmobile;
            return parm;
        }
        #endregion

        #region 会员基本参数设置
        public int Update_UserInfo(STsys_param sys)//保存基本参数设置
        {
            string Str_InSql = "Update " + Pre + "Sys_PramUser Set RegGroupNumber=@Str_RegGroupNumber,ConstrTF=@constrr,RegTF=@reg,UserLoginCodeTF=@code,CommCodeTF=@diss,SendMessageTF=@senemessage,UnRegCommTF=@n,CommHTMLLoad=@htmls,Commfiltrchar=@Str_Commfiltrchar,IPLimt=@Str_IPLimt,GpointName=@Str_GpointName,LoginLock=@Str_LoginLock,setPoint=@Str_setPoint,RegContent=@Str_RegContent,GhClass=@ghclass,cPointParam=@Str_cPointParam,aPointparam=@Str_aPointparam,CommCheck=@CommCheck";
            OleDbParameter[] parm = GetUserInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSql, parm);
        }
        private OleDbParameter[] GetUserInfo(STsys_param sys)
        {
            OleDbParameter[] parm = new OleDbParameter[18];
            parm[0] = new OleDbParameter("@Str_RegGroupNumber", OleDbType.VarWChar, 20);
            parm[0].Value = sys.Str_RegGroupNumber;
            parm[1] = new OleDbParameter("@constrr", OleDbType.Integer);
            parm[1].Value = sys.constrr;
            parm[2] = new OleDbParameter("@reg", OleDbType.Integer);
            parm[2].Value = sys.reg;
            parm[3] = new OleDbParameter("@code", OleDbType.Integer);
            parm[3].Value = sys.code;
            parm[4] = new OleDbParameter("@diss", OleDbType.Integer);
            parm[4].Value = sys.diss;
            parm[5] = new OleDbParameter("@senemessage", OleDbType.Integer);
            parm[5].Value = sys.senemessage;
            parm[6] = new OleDbParameter("@n", OleDbType.Integer);
            parm[6].Value = sys.n;
            parm[7] = new OleDbParameter("@htmls", OleDbType.Integer);
            parm[7].Value = sys.htmls;
            parm[8] = new OleDbParameter("@Str_Commfiltrchar", OleDbType.VarWChar);
            parm[8].Value = sys.Str_Commfiltrchar;
            parm[9] = new OleDbParameter("@Str_IPLimt", OleDbType.VarWChar);
            parm[9].Value = sys.Str_IPLimt;
            parm[10] = new OleDbParameter("@Str_GpointName", OleDbType.VarWChar, 10);
            parm[10].Value = sys.Str_GpointName;
            parm[11] = new OleDbParameter("@Str_LoginLock", OleDbType.VarWChar, 10);
            parm[11].Value = sys.Str_LoginLock;
            parm[12] = new OleDbParameter("@Str_setPoint", OleDbType.VarWChar, 20);
            parm[12].Value = sys.Str_setPoint;
            parm[13] = new OleDbParameter("@Str_RegContent", OleDbType.VarWChar);
            parm[13].Value = sys.Str_RegContent;
            parm[14] = new OleDbParameter("@ghclass", OleDbType.Integer);
            parm[14].Value = sys.ghclass;
            parm[15] = new OleDbParameter("@Str_cPointParam", OleDbType.VarWChar, 30);
            parm[15].Value = sys.Str_cPointParam;
            parm[16] = new OleDbParameter("@Str_aPointparam", OleDbType.VarWChar, 30);
            parm[16].Value = sys.Str_aPointparam;
            parm[17] = new OleDbParameter("@CommCheck", OleDbType.Integer, 1);
            parm[17].Value = sys.CommCheck;
            return parm;
        }
        #endregion

        #region 会员等级设置
        public int Update_Leavel(STsys_param sys, int k)//保存等级设置
        {
            string Str_InSqlLeavel = "Update " + Pre + "Sys_UserLevel Set LTitle=@Str_LtitleArr,Lpicurl=@Str_LpicurlArr,iPoint=@Str_iPointArr  where id=" + k + "";
            OleDbParameter[] parm = GetUserleavelInfo(sys, k);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSqlLeavel, parm);
        }
        private OleDbParameter[] GetUserleavelInfo(STsys_param sys, int k)
        {
            OleDbParameter[] parm = new OleDbParameter[3];
            parm[0] = new OleDbParameter("@Str_LtitleArr", OleDbType.VarWChar, 50);
            parm[0].Value = sys.Str_LtitleArr[k];
            parm[1] = new OleDbParameter("@Str_LpicurlArr", OleDbType.VarWChar, 200);
            parm[1].Value = sys.Str_LpicurlArr[k];
            parm[2] = new OleDbParameter("@Str_iPointArr", OleDbType.Integer, 4);
            parm[2].Value = sys.Str_iPointArr[k];
            return parm;
        }
        #endregion
        #endregion

        #region 更新上传
        public int Update_FtpInfo(STsys_param sys)//保存基本参数设置
        {
            string Str_InSql = "Update " + Pre + "Sys_Param Set PicServerTF=@picsa,PicServerDomain=@Str_PicServerDomain,PicUpLoad=@Str_PicUpLoad,UpfilesType=@Str_UpfilesType,UpFilesSize=@Str_UpFilesSize,ReMoteDomainTF=@domainnn,RemoteDomain=@Str_RemoteDomain,RemoteSavePath=@Str_RemoteSavePath,ClassListNum=@Str_ClassListNum,NewsNum=@Str_NewsNum,BatDelNum=@Str_BatDelNum,SpecialNum=@Str_SpecialNum";
            OleDbParameter[] parm = GetFtpInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSql, parm);
        }
        private OleDbParameter[] GetFtpInfo(STsys_param sys)
        {
            OleDbParameter[] parm = new OleDbParameter[12];
            parm[0] = new OleDbParameter("@picsa", OleDbType.Integer);
            parm[0].Value = sys.picsa;
            parm[1] = new OleDbParameter("@Str_PicServerDomain", OleDbType.VarWChar);
            parm[1].Value = sys.Str_PicServerDomain;
            parm[2] = new OleDbParameter("@Str_PicUpLoad", OleDbType.VarWChar, 200);
            parm[2].Value = sys.Str_PicUpLoad;
            parm[3] = new OleDbParameter("@Str_UpfilesType", OleDbType.VarWChar, 150);
            parm[3].Value = sys.Str_UpfilesType;
            parm[4] = new OleDbParameter("@Str_UpFilesSize", OleDbType.Integer, 4);
            parm[4].Value = sys.Str_UpFilesSize;
            parm[5] = new OleDbParameter("@domainnn", OleDbType.Integer);
            parm[5].Value = sys.domainnn;
            parm[6] = new OleDbParameter("@Str_RemoteDomain", OleDbType.VarWChar, 100);
            parm[6].Value = sys.Str_RemoteDomain;
            parm[7] = new OleDbParameter("@Str_RemoteSavePath", OleDbType.VarWChar, 200);
            parm[7].Value = sys.Str_RemoteSavePath;
            parm[8] = new OleDbParameter("@Str_ClassListNum", OleDbType.Integer, 4);
            parm[8].Value = sys.Str_ClassListNum;
            parm[9] = new OleDbParameter("@Str_NewsNum", OleDbType.Integer, 4);
            parm[9].Value = sys.Str_NewsNum;
            parm[10] = new OleDbParameter("Str_BatDelNum", OleDbType.Integer, 4);
            parm[10].Value = sys.Str_BatDelNum;
            parm[11] = new OleDbParameter("@Str_SpecialNum", OleDbType.Integer, 4);
            parm[11].Value = sys.Str_SpecialNum;
            return parm;
        }
        #endregion

        #region JS
        public int Update_JS(STsys_param sys)//更新JS
        {
            string Str_InSqljs = "Update " + Pre + "Sys_Param Set HotNewsJs=@Str_HotJS,LastNewsJs=@Str_LastJS,RecNewsJS=@Str_RecJS,HotCommJS=@Str_HoMJS,TNewsJS=@Str_TMJS";
            OleDbParameter[] parm = GetJSInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSqljs, parm);
        }
        private OleDbParameter[] GetJSInfo(STsys_param sys)
        {
            OleDbParameter[] parm = new OleDbParameter[5];
            parm[0] = new OleDbParameter("@Str_HotJS", OleDbType.VarWChar, 200);
            parm[0].Value = sys.Str_HotJS;
            parm[1] = new OleDbParameter("@Str_LastJS", OleDbType.VarWChar, 200);
            parm[1].Value = sys.Str_LastJS;
            parm[2] = new OleDbParameter("@Str_RecJS", OleDbType.VarWChar, 200);
            parm[2].Value = sys.Str_RecJS;
            parm[3] = new OleDbParameter("@Str_HoMJS", OleDbType.VarWChar, 200);
            parm[3].Value = sys.Str_HoMJS;
            parm[4] = new OleDbParameter("@Str_TMJS", OleDbType.VarWChar, 200);
            parm[4].Value = sys.Str_TMJS;
            return parm;
        }
        #endregion

        #region ftp
        public int Update_JFtP(STsys_param sys)//更新JS
        {
            string Str_InSqlftp = "Update " + Pre + "sys_Pramother Set FtpTF=@ftpp,FTPIP=@Str_FTPIP,Ftpport=@Str_Ftpport,FtpUserName=@Str_FtpUserName,FTPPASSword=@Str_FTPPASSword";
            OleDbParameter[] parm = GetftpInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSqlftp, parm);
        }
        private OleDbParameter[] GetftpInfo(STsys_param sys)
        {
            OleDbParameter[] parm = new OleDbParameter[5];
            parm[0] = new OleDbParameter("@ftpp", OleDbType.Integer, 4);
            parm[0].Value = sys.ftpp;
            parm[1] = new OleDbParameter("@Str_FTPIP", OleDbType.VarWChar, 100);
            parm[1].Value = sys.Str_FTPIP;
            parm[2] = new OleDbParameter("@Str_Ftpport", OleDbType.VarWChar, 5);
            parm[2].Value = sys.Str_Ftpport;
            parm[3] = new OleDbParameter("@Str_FtpUserName", OleDbType.VarWChar, 20);
            parm[3].Value = sys.Str_FtpUserName;
            parm[4] = new OleDbParameter("@Str_FTPPASSword", OleDbType.VarWChar, 50);
            parm[4].Value = sys.Str_FTPPASSword;
            return parm;
        }
        #endregion

        #region 水印
        public int Update_Water(STsys_param sys)//更新JS
        {
            string Str_InSql = "Update " + Pre + "Sys_ParmPrint Set PrintTF=@water,PrintPicTF=@Str_PrintPicTF,PrintWord=@Str_PrintWord,Printfontsize=@Str_Printfontsize,Printfontfamily=@Str_Printfontfamily,Printfontcolor=@Str_Printfontcolor,PrintBTF=@Str_PrintBTF,PintPicURL=@Str_PintPicURL,PrintPicsize=@Str_PrintPicsize,PintPictrans=@Str_PintPictrans,PrintPosition=@Str_PrintPosition,PrintSmallTF=@Str_PrintSmallTF,PrintSmallSizeStyle=@Str_PrintSmallSizeStyle,PrintSmallSize=@Str_PrintSmallSize,PrintSmallinv=@Str_PrintSmallinv";
            OleDbParameter[] parm = GetwaterInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSql, parm);
        }
        private OleDbParameter[] GetwaterInfo(STsys_param sys)
        {
            OleDbParameter[] parm = new OleDbParameter[15];
            parm[0] = new OleDbParameter("@water", OleDbType.Integer);
            parm[0].Value = sys.water;
            parm[1] = new OleDbParameter("@Str_PrintPicTF", OleDbType.Integer);
            parm[1].Value = sys.Str_PrintPicTF;
            parm[2] = new OleDbParameter("@Str_PrintWord", OleDbType.VarWChar, 50);
            parm[2].Value = sys.Str_PrintWord;
            parm[3] = new OleDbParameter("@Str_Printfontsize", OleDbType.Integer, 4);
            parm[3].Value = sys.Str_Printfontsize;
            parm[4] = new OleDbParameter("@Str_Printfontfamily", OleDbType.VarWChar, 30);
            parm[4].Value = sys.Str_Printfontfamily;
            parm[5] = new OleDbParameter("@Str_Printfontcolor", OleDbType.VarWChar, 10);
            parm[5].Value = sys.Str_Printfontcolor;
            parm[6] = new OleDbParameter("@Str_PrintBTF", OleDbType.Integer);
            parm[6].Value = sys.Str_PrintBTF;
            parm[7] = new OleDbParameter("@Str_PintPicURL", OleDbType.VarWChar, 150);
            parm[7].Value = sys.Str_PintPicURL;
            parm[8] = new OleDbParameter("@Str_PrintPicsize", OleDbType.VarWChar, 8);
            parm[8].Value = sys.Str_PrintPicsize;
            parm[9] = new OleDbParameter("@Str_PintPictrans", OleDbType.VarWChar, 20);
            parm[9].Value = sys.Str_PintPictrans;
            parm[10] = new OleDbParameter("@Str_PrintPosition", OleDbType.Integer);
            parm[10].Value = sys.Str_PrintPosition;
            parm[11] = new OleDbParameter("@Str_PrintSmallTF", OleDbType.Integer);
            parm[11].Value = sys.Str_PrintSmallTF;
            parm[12] = new OleDbParameter("@Str_PrintSmallSizeStyle", OleDbType.Integer);
            parm[12].Value = sys.Str_PrintSmallSizeStyle;
            parm[13] = new OleDbParameter("@Str_PrintSmallSize", OleDbType.VarWChar, 8);
            parm[13].Value = sys.Str_PrintSmallSize;
            parm[14] = new OleDbParameter("@Str_PrintSmallinv", OleDbType.VarWChar, 20);
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
            OleDbParameter[] parm = GetrssrInfo(sys);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSql, parm);
        }
        private OleDbParameter[] GetrssrInfo(STsys_param sys)
        {
            OleDbParameter[] parm = new OleDbParameter[8];
            parm[0] = new OleDbParameter("@Str_RssNum", OleDbType.Integer, 4);
            parm[0].Value = sys.Str_RssNum;
            parm[1] = new OleDbParameter("@Str_RssContentNum", OleDbType.Integer, 4);
            parm[1].Value = sys.Str_RssContentNum;
            parm[2] = new OleDbParameter("@Str_RssTitle", OleDbType.VarWChar, 50);
            parm[2].Value = sys.Str_RssTitle;
            parm[3] = new OleDbParameter("@Str_RssPicURL", OleDbType.VarWChar, 200);
            parm[3].Value = sys.Str_RssPicURL;
            parm[4] = new OleDbParameter("@wapp", OleDbType.Integer);
            parm[4].Value = sys.wapp;
            parm[5] = new OleDbParameter("@Str_WapPath", OleDbType.VarWChar, 50);
            parm[5].Value = sys.Str_WapPath;
            parm[6] = new OleDbParameter("@Str_WapDomain", OleDbType.VarWChar, 50);
            parm[6].Value = sys.Str_WapDomain;
            parm[7] = new OleDbParameter("@Str_WapLastNum", OleDbType.Integer, 4);
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
