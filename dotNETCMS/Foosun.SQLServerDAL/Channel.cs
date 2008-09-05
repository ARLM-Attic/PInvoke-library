//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==              Code By Simplt.Xie                       == 
//===========================================================
using System;
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
    public class Model : DbBase, IModel
    {
        #region 公共部分
        public IDataReader GetTopicInfo(int ID, int ChID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[0].Value = ID;
            param[1] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[1].Value = ChID;

            string DTable = getChannelTable(ChID);
            string sql = "select * from " + DTable + " where ID=@ID";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public string getUrl(string Type, int ID,int ChID)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string urls = string.Empty;
            string sql = string.Empty;
            switch (Type)
            { 
                case "content":
                    sql = "select a.SavePath,a.FileName,a.isDelPoint,b.savePath as savePath1 from " + getChannelTable(ChID) + " a,"+Pre+"sys_channelclass b Where a.ID=@ID and a.ClassID=b.id";
                    break;
                case "class":
                    sql = "select SavePath,FileName,isDelPoint from " + Pre + "sys_channelclass Where ID=@ID and ChID=" + ChID + "";
                    break;
                case "special":
                    sql = "select SavePath,FileName from " + Pre + "sys_channelspecial Where ID=@ID and ChID=" + ChID + "";
                    break;
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, param);
            if (dr.Read())
            {
                switch (Type)
                { 
                    case "content":
                        if (dr["isDelPoint"].ToString() != "0")
                        {
                            urls = "/content.aspx?id=" + ID + "&ChID=" + ChID + "";
                        }
                        else
                        {
                            urls = "/" + dr["savePath1"].ToString() + "/" + dr["SavePath"].ToString() + "/" + dr["FileName"].ToString();
                        }
                        break;
                    case "class":
                        if (dr["isDelPoint"].ToString() != "0")
                        {
                            urls = "/content.aspx?id=" + ID + "&ChID=" + ChID + "";
                        }
                        else
                        {
                            urls = "/" + dr["SavePath"].ToString() + "/" + dr["FileName"].ToString();
                        }
                        break;
                    case "special":
                        urls = "/" + dr["SavePath"].ToString() + "/" + dr["FileName"].ToString();
                        break;
                }
                urls = urls.Replace("//", "/");
            }
            dr.Close();
            return urls;
        }

        /// <summary>
        /// 获取频道英文名称
        /// </summary>
        public string GetChannEName(int ChID)
        {
            SqlParameter param = new SqlParameter("@ChID", ChID);
            string sql = "select channelEItem from " + Pre + "sys_channel where ID=@ChID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        public int GetTopChID(string EName)
        {
            SqlParameter param = new SqlParameter("@EName", EName);
            string sql = "select top 1 id from " + Pre + "sys_channel where channelEItem=@EName";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }
        #endregion
        #region 基础，创建频道部分
        public IDataReader getModelTemplet()
        {
            string Sql = "select id,channelName from " + Pre + "sys_channel where islock=0 order by id asc";
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }

        public IDataReader getModelTempletisConstr()
        {
            string Sql = "select * from " + Pre + "sys_channel where islock=0 and isConstr=1 order by id asc";
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }
        public IDataReader getModelinfo(int ID)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string Sql = "select * from " + Pre + "sys_channel where ID=@ID order by id desc";
            IDataReader dt = DbHelper.ExecuteReader(CommandType.Text, Sql, param);
            return dt;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="DataTable"></param>
        /// <param name="channelType"></param>
        public void creatModeltable(string DataTable, int channelType, int isConstr)
        {
            //根据模型标志读取默认配置
            string getModelContentField = Foosun.Common.Public.getModelContentField(channelType.ToString());
            //string[] getDefaultValue = null;
            //string[] getDefaultItemValue = null;
            //string CreatField = "";
            //if (getModelContentField.IndexOf(",") > -1)
            //{
            //    getDefaultValue = getModelContentField.Split(',');
            //    for (int i = 0; i < getDefaultValue.Length; i++)
            //    {
            //        getDefaultItemValue = getDefaultValue[i].Split('|');
            //        CreatField += "[" + getDefaultItemValue[0] + "] [" + getDefaultItemValue[1] + "]";
            //        if (getDefaultItemValue[1].Trim().ToLower() == "nvarchar" || getDefaultItemValue[1].Trim().ToLower() == "varchar" || getDefaultItemValue[1].Trim().ToLower() == "char" || getDefaultItemValue[1].Trim().ToLower() == "nchar" || getDefaultItemValue[1].Trim().ToLower() == "varbinary")
            //        {
            //            CreatField += " (" + getDefaultItemValue[2] + ") " + getDefaultItemValue[3] + ",";
            //        }
            //        else
            //        {
            //            CreatField += " " + getDefaultItemValue[3] + ",";
            //        }
            //    }
            //}
            string Sql = "CREATE TABLE [" + DataTable + "](" +
                        "[Id] [int] IDENTITY (1, 1) NOT NULL ," +
                        "[ChID] [int] NOT NULL ," +//信息ＩＤ
                        "[title] [nvarchar](100) NOT NULL ," +//标题
                        "[ClassID] [int] NOT NULL ," +//栏目
                        "[SpecialID] [nvarchar] (200) NULL ," +//专题
                        "[TitleColor] [nvarchar] (10) NULL ," +//标题颜色
                        "[TitleITF] [tinyint] NULL ," +//标题是否为斜体
                        "[TitleBTF] [tinyint] NULL ," +//标题是否为粗体
                        "[PicURL] [nvarchar] (200) NULL ," +//图片地址
                        "[Content] [ntext] NULL ," +//内容描述
                        "[NaviContent] [nvarchar] (200) NULL ," +//内容导读
                        "[ContentProperty] [nvarchar] (9) NULL ," +//属性,推荐|热点|幻灯|滚动|头条
                        "[Author] [nvarchar] (100) NULL ," +//作者
                        "[Editor] [nvarchar] (50) NULL ," +//编辑
                        "[Souce] [nvarchar] (100) NULL ," +//来源
                        "[OrderID] [tinyint] NOT NULL ," +//权重
                        "[Tags] [nvarchar] (100) NULL ," +//关键字
                        "[Templet] [nvarchar] (200) NULL ," +//模板
                        "[SavePath] [nvarchar] (200) NULL ," +
                        "[FileName] [nvarchar] (100) NULL ," +//包含扩展名
                        "[isDelPoint] [tinyint] NOT NULL ," +//是否具有浏览权限
                        "[Gpoint] [int] NULL ," +//G币
                        "[iPoint] [int] NULL ," +//积分
                        "[GroupNumber] [ntext] NULL ," +//会员组
                        "[Metakeywords] [nvarchar] (200) NULL ," +//meta关键字
                        "[Metadesc] [nvarchar] (200) NULL ," +//meta描述
                        "[Click] [int] NULL ," +//点击
                        "[CreatTime] [datetime] NULL ," +//创建日期
                        "[isHTML] [tinyint] NOT NULL ," +//是否生成了静态
                        "[isConstr] [tinyint] NOT NULL ,"+ //专区
                        "[ConstrTF] [tinyint] NOT NULL ,"; //投稿审核
            Sql += "[islock] [tinyint] NULL ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
            Sql += "ALTER TABLE [" + DataTable + "] WITH NOCHECK ADD CONSTRAINT [PK_" + DataTable + "] PRIMARY KEY  CLUSTERED([Id])  ON [PRIMARY] ";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            //根据模型类型插入字段

        }

        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="uc"></param>
        public void updateDate(Foosun.Model.ChannelInfo uc)
        {
            string Sql = "insert into " + Pre + "sys_channel (";
            Sql += "channelType,channelName,channelItem,channelDescript,DataLib,islock,channelunit,htmldir,indexFileName,upfilessize,upfiletype,ischeck,indextemplet,classtemplet,specialtemplet,newstemplet,isHTML,SiteID,issys,isConstr,channelEItem,ClassSave,ClassFileName,SavePath,FileName,binddomain,TempletPath,isDelPoint,Gpoint,iPoint,GroupNumber";
            Sql += ") values (";
            Sql += "@channelType,@channelName,@channelItem,@channelDescript,@DataLib,0,@channelunit,@htmldir,@indexFileName,@upfilessize,@upfiletype,@ischeck,@indextemplet,@classtemplet,@specialtemplet,@newstemplet,@isHTML,'0',0,@isConstr,@channelEItem,@ClassSave,@ClassFileName,@SavePath,@FileName,@binddomain,@TempletPath,@isDelPoint,@Gpoint,@iPoint,@GroupNumber)";
            SqlParameter[] parm = InsertParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 更新记录记录
        /// </summary>
        /// <param name="uc"></param>
        public void updateDate1(Foosun.Model.ChannelInfo uc)
        {
            string Sql = "Update " + Pre + "sys_channel set channelName=@channelName,channelItem=@channelItem,channelDescript=@channelDescript,channelunit=@channelunit,htmldir=@htmldir,indexFileName=@indexFileName,upfilessize=@upfilessize,upfiletype=@upfiletype,ischeck=@ischeck,indextemplet=@indextemplet,classtemplet=@classtemplet,specialtemplet=@specialtemplet,newstemplet=@newstemplet,isHTML=@isHTML,isConstr=@isConstr,ClassSave=@ClassSave,ClassFileName=@ClassFileName,SavePath=@SavePath,FileName=@FileName,issys=@issys,binddomain=@binddomain,TempletPath=@TempletPath,isDelPoint=@isDelPoint,Gpoint=@Gpoint,iPoint=@iPoint,GroupNumber=@GroupNumber where ID=" + uc.Id + "";
            SqlParameter[] parm = InsertParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        private SqlParameter[] InsertParameters(Foosun.Model.ChannelInfo uc1)
        {
            SqlParameter[] param = new SqlParameter[31];
            param[0] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[0].Value = uc1.Id;
            param[1] = new SqlParameter("@isConstr", SqlDbType.TinyInt, 1);
            param[1].Value = uc1.isConstr;
            param[2] = new SqlParameter("@channelType", SqlDbType.TinyInt, 1);
            param[2].Value = uc1.channelType;
            param[3] = new SqlParameter("@channelName", SqlDbType.NVarChar, 50);
            param[3].Value = uc1.channelName;
            param[4] = new SqlParameter("@channelItem", SqlDbType.NVarChar, 50);
            param[4].Value = uc1.channelItem;
            param[5] = new SqlParameter("@channelDescript", SqlDbType.NVarChar, 200);
            param[5].Value = uc1.channelDescript;
            param[6] = new SqlParameter("@DataLib", SqlDbType.NVarChar, 30);
            param[6].Value = uc1.DataLib;
            param[7] = new SqlParameter("@islock", SqlDbType.TinyInt, 1);
            param[7].Value = uc1.islock;
            param[8] = new SqlParameter("@channelunit", SqlDbType.NVarChar, 50);
            param[8].Value = uc1.channelunit;
            param[9] = new SqlParameter("@htmldir", SqlDbType.NVarChar, 100);
            param[9].Value = uc1.htmldir;
            param[10] = new SqlParameter("@upfilessize", SqlDbType.Int, 4);
            param[10].Value = uc1.upfilessize;
            param[11] = new SqlParameter("@upfiletype", SqlDbType.NVarChar, 100);
            param[11].Value = uc1.upfiletype;
            param[12] = new SqlParameter("@ischeck", SqlDbType.TinyInt, 1);
            param[12].Value = uc1.ischeck;
            param[13] = new SqlParameter("@indextemplet", SqlDbType.NVarChar, 200);
            param[13].Value = uc1.indextemplet;
            param[14] = new SqlParameter("@classtemplet", SqlDbType.NVarChar, 200);
            param[14].Value = uc1.classtemplet;
            param[15] = new SqlParameter("@specialtemplet", SqlDbType.NVarChar, 200);
            param[15].Value = uc1.specialtemplet;
            param[16] = new SqlParameter("@newstemplet", SqlDbType.NVarChar, 200);
            param[16].Value = uc1.newstemplet;
            param[17] = new SqlParameter("@isHTML", SqlDbType.TinyInt, 1);
            param[17].Value = uc1.isHTML;
            param[18] = new SqlParameter("@channelEItem", SqlDbType.NVarChar, 20);
            param[18].Value = uc1.channelEItem;
            param[19] = new SqlParameter("@ClassSave", SqlDbType.NVarChar, 50);
            param[19].Value = uc1.ClassSave;
            param[20] = new SqlParameter("@ClassFileName", SqlDbType.NVarChar, 50);
            param[20].Value = uc1.ClassFileName;
            param[21] = new SqlParameter("@SavePath", SqlDbType.NVarChar, 50);
            param[21].Value = uc1.SavePath;
            param[22] = new SqlParameter("@FileName", SqlDbType.NVarChar, 50);
            param[22].Value = uc1.FileName;
            param[23] = new SqlParameter("@issys", SqlDbType.TinyInt, 1);
            param[23].Value = uc1.issys;
            param[24] = new SqlParameter("@binddomain", SqlDbType.NVarChar, 150);
            param[24].Value = uc1.binddomain;
            param[25] = new SqlParameter("@TempletPath", SqlDbType.NVarChar, 100);
            param[25].Value = uc1.TempletPath;
            param[26] = new SqlParameter("@indexFileName", SqlDbType.NVarChar, 50);
            param[26].Value = uc1.indexFileName;

            param[27] = new SqlParameter("@isDelPoint", SqlDbType.TinyInt);
            param[27].Value = uc1.isDelPoint;
            param[28] = new SqlParameter("@Gpoint", SqlDbType.Int, 4);
            param[28].Value = uc1.Gpoint;
            param[29] = new SqlParameter("@iPoint", SqlDbType.Int, 4);
            param[29].Value = uc1.iPoint;
            param[30] = new SqlParameter("@GroupNumber", SqlDbType.NVarChar, 200);
            param[30].Value = uc1.GroupNumber;
            return param;
        }

        public int getItemCount(string eName, int ChID)
        {
            SqlParameter param = new SqlParameter("@eName", eName);
            string wStr = string.Empty;
            if (ChID != 0)
            {
                wStr = " and ID<>" + ChID + "";
            }
            string sql = "select count(id) from " + Pre + "sys_channel where channelEItem=@eName " + wStr + "";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }
        public int getDbCount(string Table, int ChID)
        {
            SqlParameter param = new SqlParameter("@Table", Table);
            string wStr = string.Empty;
            if (ChID != 0)
            {
                wStr = " and ID<>" + ChID + "";
            }
            string sql = "select count(id) from " + Pre + "sys_channel where DataLib=@Table " + wStr + "";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        public int getSysCord(int ID)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string sql = "select issys from " + Pre + "sys_channel where ID=@ID";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        public void delModel(int ID)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string gSQL = "select Datalib from " + Pre + "sys_channel where ID=@ID";
            string DbTable = Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, gSQL, param));
            string sql = "delete from " + Pre + "sys_channel where ID=@ID";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
            try
            {
                string delDB = "drop table [" + DbTable + "]";
                DbHelper.ExecuteNonQuery(CommandType.Text, delDB, null);
            }
            catch
            { }
        }

        public void ModelStat(int ID, int isLock)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string sql = "update " + Pre + "sys_channel set islock=" + isLock + " where ID=@ID";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public IDataReader getChInfoMenu(int ChID)
        {
            SqlParameter param = new SqlParameter("@ID", ChID);
            string sql = "select id,channelName,channelItem,TempletPath,isConstr from " + Pre + "sys_channel where ID=@ID";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public IDataReader getChValue(int ID)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string sql = "select * from " + Pre + "sys_channelvalue where ID=@ID";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public string getChannelTable(int ChID)
        {
            string TableStr = "#";
            string TmpTable = string.Empty;
            int GetTableRecord = 0;
            SqlParameter param = new SqlParameter("@ChID", ChID);
            string sql = "select DataLib from " + Pre + "sys_channel where ID=@ChID";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, param);
            if (dr.Read())
            {
                TmpTable = dr["DataLib"].ToString();
                string TableSQL = "select count(*) from sysobjects where id = object_id(N'[" + TmpTable + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
                GetTableRecord = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, TableSQL, null));
                if (GetTableRecord > 0)
                {
                    TableStr = TmpTable;
                }
            }
            dr.Close();
            return TableStr;
        }

        public bool getChannelValueTF(int ChID, string EName, int vID)
        {
            SqlParameter param = new SqlParameter("@ChID", @ChID);
            string sql = "select Count(id) from " + Pre + "sys_channelvalue where ChID=@ChID and EName='" + EName + "' and ID<>" + vID + "";
            int count = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 插入字段入数据库进行保存
        /// </summary>
        /// <param name="uc"></param>
        public void insertFields(Foosun.Model.ChannelValue uc, string TableSTR)
        {
            string Sql = "insert into " + Pre + "sys_channelvalue (";
            Sql += "ChID,OrderID,CName,EName,vDescript,vType,vLength,vValue,isNulls,isUser,vitem,isLock,SiteID,fieldLength,HTMLedit,isSearch,vHeight";
            Sql += ") values (";
            Sql += "@ChID,@OrderID,@CName,@EName,@vDescript,@vType,@vLength,@vValue,@isNulls,@isUser,@vitem,@isLock,@SiteID,@fieldLength,@HTMLedit,@isSearch,@vHeight)";
            SqlParameter[] param = ValueParameters(uc);
            //创建数据库字段
            string CreateSql = "ALTER TABLE [" + TableSTR + "] ADD [" + uc.EName + "] " + CreatevType(uc.vType) + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, CreateSql, param);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        /// <summary>
        /// 更新字段
        /// </summary>
        /// <param name="uc"></param>
        /// <param name="TableSTR"></param>
        public void UpdateFields(Foosun.Model.ChannelValue uc, string TableSTR)
        {
            string Sql = "update " + Pre + "sys_channelvalue set ";
            Sql += "OrderID=@OrderID,CName=@CName,vDescript=@vDescript,vLength=@vLength,vValue=@vValue,isNulls=@isNulls,isUser=@isUser,vitem=@vitem,isLock=@isLock,isSearch=@isSearch,vHeight=@vHeight,HTMLedit=@HTMLedit where ID=@Id and SiteID=@SiteID";
            SqlParameter[] param = ValueParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        /// <summary>
        /// 得到字段类型
        /// </summary>
        /// <param name="vType"></param>
        /// <returns></returns>
        protected string CreatevType(int vType)
        {
            string LenStr = string.Empty;
            string NullStr = "NULL";
            switch (vType)
            {
                case 0:
                    LenStr = "[nvarchar] (20) " + NullStr + "";
                    break;
                case 1:
                    LenStr = "[nvarchar] (50) " + NullStr + "";
                    break;
                case 2:
                    LenStr = "[nvarchar] (100) " + NullStr + "";
                    break;
                case 3:
                    LenStr = "[nvarchar] (180) " + NullStr + "";
                    break;
                case 4:
                    LenStr = "[nvarchar] (225) " + NullStr + "";
                    break;
                case 5:
                    LenStr = "[ntext] " + NullStr + "";
                    break;
                case 6:
                    LenStr = "[nvarchar] (200) " + NullStr + "";
                    break;
                case 7:
                    LenStr = "[int] " + NullStr + "";
                    break;
                case 8:
                    LenStr = "[tinyint] " + NullStr + "";
                    break;
                case 9:
                    LenStr = "[money] " + NullStr + "";
                    break;
                case 10:
                    LenStr = "[datetime] " + NullStr + "";
                    break;
                case 11:
                    LenStr = "[smalldatetime] " + NullStr + "";
                    break;
                case 12:
                    LenStr = "[nvarchar] (200) " + NullStr + "";
                    break;
                case 13:
                    LenStr = "[ntext] " + NullStr + "";
                    break;
                case 14:
                    LenStr = "[nvarchar] (200) " + NullStr + "";
                    break;
                case 15:
                    LenStr = "[ntext] " + NullStr + "";
                    break;
                case 16:
                    LenStr = "[ntext] " + NullStr + "";
                    break;
                case 17:
                    LenStr = "[ntext] " + NullStr + "";
                    break;
                default:
                    LenStr = "[nvarchar] (200) " + NullStr + "";
                    break;
            }
            return LenStr;
        }

        /// <summary>
        /// 得到所有参数
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private SqlParameter[] ValueParameters(Foosun.Model.ChannelValue uc1)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[0].Value = uc1.Id;
            param[1] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[1].Value = uc1.ChID;
            param[2] = new SqlParameter("@OrderID", SqlDbType.TinyInt, 1);
            param[2].Value = uc1.OrderID;
            param[3] = new SqlParameter("@CName", SqlDbType.NVarChar, 50);
            param[3].Value = uc1.CName;
            param[4] = new SqlParameter("@EName", SqlDbType.NVarChar, 50);
            param[4].Value = uc1.EName;
            param[5] = new SqlParameter("@vDescript", SqlDbType.NVarChar, 200);
            param[5].Value = uc1.vDescript;
            param[6] = new SqlParameter("@vType", SqlDbType.TinyInt, 1);
            param[6].Value = uc1.vType;
            param[7] = new SqlParameter("@vLength", SqlDbType.NVarChar, 10);
            param[7].Value = uc1.vLength;
            param[8] = new SqlParameter("@vValue", SqlDbType.NVarChar, 150);
            param[8].Value = uc1.vValue;
            param[9] = new SqlParameter("@isNulls", SqlDbType.TinyInt, 1);
            param[9].Value = uc1.isNulls;
            param[10] = new SqlParameter("@isUser", SqlDbType.TinyInt, 1);
            param[10].Value = uc1.isUser;
            param[11] = new SqlParameter("@vitem", SqlDbType.NText);
            param[11].Value = uc1.vitem;
            param[12] = new SqlParameter("@isLock", SqlDbType.TinyInt, 1);
            param[12].Value = uc1.isLock;
            param[13] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[13].Value = uc1.SiteID;
            param[14] = new SqlParameter("@fieldLength", SqlDbType.NVarChar, 5);
            param[14].Value = uc1.fieldLength;
            param[15] = new SqlParameter("@isSearch", SqlDbType.TinyInt, 1);
            param[15].Value = uc1.isSearch;
            param[16] = new SqlParameter("@HTMLedit", SqlDbType.TinyInt, 1);
            param[16].Value = uc1.HTMLedit;
            param[17] = new SqlParameter("@vHeight", SqlDbType.NVarChar, 6);
            param[17].Value = uc1.vHeight;
            return param;
        }

        /// <summary>
        /// 删除字段
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="TableStr"></param>
        public void delFileds(int ID, string TableStr)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string gSQL = "select EName from " + Pre + "sys_channelvalue where ID=@ID";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, gSQL, param);
            if (dr.Read())
            {
                string DeleteSql = "ALTER TABLE [" + TableStr + "] Drop column [" + dr["EName"].ToString() + "]";
                DbHelper.ExecuteNonQuery(CommandType.Text, DeleteSql, param);
                string delSQL = "delete from " + Pre + "sys_channelvalue where ID=@ID";
                DbHelper.ExecuteNonQuery(CommandType.Text, delSQL, param);
            }
            dr.Close();
        }
        /// <summary>
        /// 更新字段数据锁定状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Num"></param>
        public void updateValueFileds(int ID, int Num)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string sql = "update " + Pre + "sys_channelvalue set isLock=" + Num + " where ID=@ID";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }
        #endregion
        #region 栏目部分
        public void updateOrder(int ID, int OrderID, int Num)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string sql = string.Empty;
            if (Num == 0)
            {
                sql = "update " + Pre + "sys_channelclass set OrderID=" + OrderID + " where ID=@ID";
            }
            else
            {
                sql = "update " + Pre + "sys_channelspecial set OrderID=" + OrderID + " where ID=@ID";
            }
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public string getClassName(int ClassID)
        {
            SqlParameter param = new SqlParameter("@ClassID", ClassID);
            string sql = "select classCName from " + Pre + "sys_channelclass where ID=@ClassID";
            string CName= Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
            if (CName == string.Empty)
            {
                CName = "根栏目"; 
            }
            return CName;
        }
        /// <summary>
        /// 继承频道信息
        /// </summary>
        /// <param name="ChID">频道ＩＤ</param>
        /// <returns>记录集</returns>
        public IDataReader ChannelInfo(int ChID)
        {
            SqlParameter param = new SqlParameter("@ChID", ChID);
            string sql = "select * from " + Pre + "sys_channel where ID=@ChID";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public int getClassInfoCord(string EName, int ID)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string sql = "select count(id) from " + Pre + "sys_channelclass where ID<>@ID and classEName='" + EName + "'";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        /// <summary>
        /// 插入栏目数据
        /// </summary>
        /// <param name="uc"></param>
        public void insertClassInfo(Foosun.Model.ChannelClassInfo uc)
        {
            SqlParameter[] param = ClassInfoParameter(uc);
            string Sql = "insert into " + Pre + "sys_channelclass (";
            Sql += "ChID,OrderID,ParentID,classCName,classEName,isPage,PageContent,Templet,ContentTemplet,SavePath,FileName,ContentSavePath,ContentFileNameRule,isShowNavi,NaviContent,KeyMeta,DescMeta,PicURL,isDelPoint,Gpoint,iPoint,GroupNumber,isLock,ClassNavi,ContentNavi,SiteID";
            Sql += ") values (";
            Sql += "@ChID,@OrderID,@ParentID,@classCName,@classEName,@isPage,@PageContent,@Templet,@ContentTemplet,@SavePath,@FileName,@ContentSavePath,@ContentFileNameRule,@isShowNavi,@NaviContent,@KeyMeta,@DescMeta,@PicURL,@isDelPoint,@Gpoint,@iPoint,@GroupNumber,@isLock,@ClassNavi,@ContentNavi,@SiteID)";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public void updateClassInfo(Foosun.Model.ChannelClassInfo uc)
        {
            SqlParameter[] param = ClassInfoParameter(uc);
            string Sql = "update " + Pre + "sys_channelclass set ";
            Sql += "OrderID=@OrderID,classCName=@classCName,isPage=@isPage,PageContent=@PageContent,Templet=@Templet,ContentTemplet=@ContentTemplet,SavePath=@SavePath,ContentSavePath=@ContentSavePath,ContentFileNameRule=@ContentFileNameRule,isShowNavi=@isShowNavi,NaviContent=@NaviContent,KeyMeta=@KeyMeta,DescMeta=@DescMeta,PicURL=@PicURL,isDelPoint=@isDelPoint,Gpoint=@Gpoint,iPoint=@iPoint,GroupNumber=@GroupNumber,isLock=@isLock,ClassNavi=@ClassNavi,ContentNavi=@ContentNavi,SiteID=@SiteID where id=" + uc.Id + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        private SqlParameter[] ClassInfoParameter(Foosun.Model.ChannelClassInfo uc1)
        {
            SqlParameter[] param = new SqlParameter[27];
            param[0] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[0].Value = uc1.Id;
            param[1] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[1].Value = uc1.ChID;
            param[2] = new SqlParameter("@OrderID", SqlDbType.TinyInt, 1);
            param[2].Value = uc1.OrderID;
            param[3] = new SqlParameter("@ParentID", SqlDbType.Int, 4);
            param[3].Value = uc1.ParentID;
            param[4] = new SqlParameter("@classCName", SqlDbType.NVarChar, 50);
            param[4].Value = uc1.classCName;
            param[5] = new SqlParameter("@classEName", SqlDbType.NVarChar, 50);
            param[5].Value = uc1.classEName;
            param[6] = new SqlParameter("@isPage", SqlDbType.TinyInt, 1);
            param[6].Value = uc1.isPage;
            param[7] = new SqlParameter("@PageContent", SqlDbType.NText);
            param[7].Value = uc1.PageContent;
            param[8] = new SqlParameter("@Templet", SqlDbType.NVarChar, 200);
            param[8].Value = uc1.Templet;
            param[9] = new SqlParameter("@ContentTemplet", SqlDbType.NVarChar, 200);
            param[9].Value = uc1.ContentTemplet;
            param[10] = new SqlParameter("@SavePath", SqlDbType.NVarChar, 100);
            param[10].Value = uc1.SavePath;
            param[11] = new SqlParameter("@FileName", SqlDbType.NVarChar, 100);
            param[11].Value = uc1.FileName;
            param[12] = new SqlParameter("@ContentSavePath", SqlDbType.NVarChar, 100);
            param[12].Value = uc1.ContentSavePath;
            param[13] = new SqlParameter("@ContentFileNameRule", SqlDbType.NVarChar, 150);
            param[13].Value = uc1.ContentFileNameRule;
            param[14] = new SqlParameter("@isShowNavi", SqlDbType.TinyInt, 1);
            param[14].Value = uc1.isShowNavi;

            param[15] = new SqlParameter("@NaviContent", SqlDbType.NVarChar, 200);
            param[15].Value = uc1.NaviContent;
            param[16] = new SqlParameter("@KeyMeta", SqlDbType.NVarChar, 100);
            param[16].Value = uc1.KeyMeta;
            param[17] = new SqlParameter("@DescMeta", SqlDbType.NVarChar, 150);
            param[17].Value = uc1.DescMeta;
            param[18] = new SqlParameter("@PicURL", SqlDbType.NVarChar, 200);
            param[18].Value = uc1.PicURL;
            param[19] = new SqlParameter("@isDelPoint", SqlDbType.TinyInt, 1);
            param[19].Value = uc1.isDelPoint;
            param[20] = new SqlParameter("@Gpoint", SqlDbType.Int, 4);
            param[20].Value = uc1.Gpoint;
            param[21] = new SqlParameter("@iPoint", SqlDbType.Int, 4);
            param[21].Value = uc1.iPoint;
            param[22] = new SqlParameter("@GroupNumber", SqlDbType.NVarChar, 200);
            param[22].Value = uc1.GroupNumber;
            param[23] = new SqlParameter("@isLock", SqlDbType.TinyInt, 1);
            param[23].Value = uc1.isLock;
            param[24] = new SqlParameter("@ClassNavi", SqlDbType.NText);
            param[24].Value = uc1.ClassNavi;
            param[25] = new SqlParameter("@ContentNavi", SqlDbType.NText);
            param[25].Value = uc1.ContentNavi;
            param[26] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[26].Value = uc1.SiteID;
            return param;
        }

        public IDataReader GetClassInfo(int ClassID)
        {
            SqlParameter param = new SqlParameter("@ClassID", ClassID);
            string sql = "select * from " + Pre + "sys_channelclass where ID=@ClassID";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public int GetTopClassID()
        {
            string sql = "select top 1 id from " + Pre + "sys_channelclass";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
        }

        public int getClassNumber(int ClassID)
        {
            SqlParameter param = new SqlParameter("@ClassID", ClassID);
            string sql = "select count(id) from " + Pre + "sys_channelclass where ParentID=@ClassID";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        public IDataReader getClassList(int ClassID,int ChID)
        {
            SqlParameter param = new SqlParameter("@ClassID", ClassID);
            string sql = "select id,ClassCName from " + Pre + "sys_channelclass where ParentID=@ClassID and isPage=0 and islock=0 and ChID=" + ChID + " order by Orderid desc,id desc";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public int delClass(int ClassID)
        {
            SqlParameter param = new SqlParameter("@ClassID", ClassID);
            string sql = "delete from " + Pre + "sys_channelclass where ID=@ClassID";
            delcClass(ClassID);
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, sql, param));
        }

        public void delcClass(int ClassID)
        {
            SqlParameter param = new SqlParameter("@ClassID", ClassID);
            string sqlc = "select id from " + Pre + "sys_channelclass where ParentID=@ClassID order by id desc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sqlc, param);
            while (dr.Read())
            {
                int gID = int.Parse(dr["id"].ToString());
                string sql = "delete from " + Pre + "sys_channelclass where ID=" + gID + "";
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
                delcClass(gID);
            }
            dr.Close();
        }

        public int Reset_allClass(int ClassID, int ChID)
        {
            string dTable = getChannelTable(ChID);
            if (dTable != string.Empty)
            {
                SqlParameter param = new SqlParameter("@ClassID", ClassID);
                string sql = "delete from " + dTable + " where ClassID=@ClassID";
                return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, sql, param));
            }
            else
            {
                return 0;
            }
        }

        public int lockstat(int ClassID, int num)
        {
            SqlParameter param = new SqlParameter("@ClassID", ClassID);
            string sql = "update " + Pre + "sys_channelclass set islock=" + num + " where Id=@ClassID";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, sql, param));
        }


        public void utilClass(int sClassID, int tClassID,int ChID)
        { 
            //Copy content
            SqlParameter param = new SqlParameter("@ChID",ChID);
            string sql = "select DataLib from " + Pre + "sys_channel where ID=@ChID";
            string dbTable = Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
            if (dbTable != string.Empty)
            {
                string usql = "update " + dbTable + " set ClassID=" + tClassID + " where ClassID=" + sClassID + "";
                DbHelper.ExecuteNonQuery(CommandType.Text, usql, null);
            }
            //更新源栏目下级的父类
            string ssql = "select ParentID from " + Pre + "sys_channelclass where ID=" + sClassID + "";
            string ParentID = Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, ssql, null));
            if (ParentID != string.Empty)
            {
                string usql = "update " + Pre + "sys_channelclass set ParentID=" + int.Parse(ParentID) + " where ParentID=" + sClassID + "";
                DbHelper.ExecuteNonQuery(CommandType.Text, usql, null);
            }
            //删除源栏目
            string delsql = "delete from " + Pre + "sys_channelclass where id=" + sClassID + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, delsql, null);
        }

        public void moveClass(int sClassID, int tClassID)
        {
            string sql = "update " + Pre + "sys_channelclass set ParentID=" + tClassID + " where Id=" + sClassID + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        #endregion
        #region 内容部分
        public DataTable GetChannelValueFormInfo(int ChID,string DTable,int ID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[0].Value = ChID;
            param[1] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[1].Value = ID;
            string sql = string.Empty;
            if (ID != 0)
            {
                sql = "select a.*,b.* from " + Pre + "sys_channelvalue a," + DTable + " b where a.ChID=@ChID and b.ID=@ID and a.isLock=0 order by a.OrderID desc,a.id desc";
            }
            else
            {
                sql = "select * from " + Pre + "sys_channelvalue where ChID=@ChID and isLock=0 order by OrderID desc,id desc";
            }
            return DbHelper.ExecuteTable(CommandType.Text, sql, param);
        }

        public DataTable GetChannelUserValueFormInfo(int ChID, string DTable, int ID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[0].Value = ChID;
            param[1] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[1].Value = ID;
            string sql = string.Empty;
            if (ID != 0)
            {
                sql = "select a.*,b.* from " + Pre + "sys_channelvalue a," + DTable + " b where a.ChID=@ChID and b.ID=@ID and a.isLock=0 and a.isUser=1 order by a.OrderID desc,a.id desc";
            }
            else
            {
                sql = "select * from " + Pre + "sys_channelvalue where ChID=@ChID and isLock=0 and isUser=1 order by OrderID desc,id desc";
            }
            return DbHelper.ExecuteTable(CommandType.Text, sql, param);
        }

        public DataTable GetPage(string keywords,string islock,string author, string ClassID,string SpecialID, string stat, int ChID,string dbTable, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            string sFilter = " where a.ChID=" + ChID + " and a.ClassID=b.id";
            if (ClassID != "#0")
            {
                sFilter += " and a.ClassID=" + int.Parse(ClassID) + "";
            }
            if (islock != "#0")
            {
                sFilter += " and a.islock=" + int.Parse(islock) + "";
            }
            if (SpecialID != "#0")
            {
                sFilter += " and a.SpecialID like '%" + SpecialID + "%'";
            }
            if (keywords != "#0")
            {
                sFilter += " and (a.Content like '%" + keywords + "%' or a.Author like '%" + keywords + "%' or a.Title like '%" + keywords + "%')";
            }
            string gSQLstr = "order by a.OrderID desc";
            if (stat != "#0")
            {
                switch (stat)//推荐|热点|幻灯|滚动|头条
                { 
                    case "rec":
                        sFilter += "  And ContentProperty like '1%'";
                        break;
                    case "hot":
                        sFilter += " And ContentProperty like '__1%'";
                        break;
                    case "filt":
                        sFilter += " And ContentProperty like '____1%'";
                        break;
                    case "mar":
                        sFilter += " And ContentProperty like '______1%'";
                        break;
                    case "hnews":
                        sFilter += " And ContentProperty like '________1%'";
                        break;
                    case "constr":
                        sFilter += " and isConstr=1";
                        break;
                    case "isadmin":
                        sFilter += " and isConstr=0";
                        break;
                    case "unlock":
                        sFilter += " and a.islock=0";
                        break;
                    case "lock":
                        sFilter += " and a.islock=1";
                        break;
                    case "click":
                        gSQLstr = "order by a.click desc";
                        break;
                    default:
                        if (stat.IndexOf("SP|") > -1)
                        {
                            string[] SPSTRARR = stat.Split('|');
                            sFilter += " and a.SpecialID='" + SPSTRARR[1] + "'";
                        }
                        break;
                }
            }
            if (author != "#0")
            {
                sFilter += " and a.author='" + author + "'";
            }
            string AllFields = "a.*";
            string Condition = dbTable + " a," + Pre + "sys_channelclass b" + sFilter;
            string IndexField = "a.Id";
            string OrderFields = gSQLstr + ",a.Id desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        public int delContent(int id, int ChID,int Num)
        {
            string sql = string.Empty;
            SqlParameter param = new SqlParameter("@id", id);
            string dTable = getChannelTable(ChID);
            if (Num == 0)
            {
                sql = "delete from " + dTable + " where id=@id and ChID=" + ChID + "";
            }
            else
            {
                if (id == 0)
                {
                    sql = "delete from " + dTable + " where ChID=" + ChID + "";
                }
                else
                {
                    sql = "delete from " + dTable + " where ClassID=@id and ChID=" + ChID + "";
                }
            }
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, sql, param));
        }

        public int inserContentInfo(Foosun.Model.ChInfoContent uc,string DTable)
        {
            SqlParameter[] param = GetChInfoParams(uc);
            string Sql = "insert into " + DTable + " (";
            Sql += "ChID,ClassID,SpecialID,title,TitleColor,TitleITF,TitleBTF,PicURL,NaviContent,Content,Author,Souce,OrderID,Tags,";
            Sql += "Templet,SavePath,FileName,isDelPoint,Gpoint,iPoint,GroupNumber,Metakeywords,Metadesc,Click,CreatTime,isHTML,isConstr,islock,ContentProperty,Editor,ConstrTF";
            Sql += ") values (";
            Sql += "@ChID,@ClassID,@SpecialID,@title,@TitleColor,@TitleITF,@TitleBTF,@PicURL,@NaviContent,@Content,@Author,@Souce,@OrderID,@Tags,";
            Sql += "@Templet,@SavePath,@FileName,@isDelPoint,@Gpoint,@iPoint,@GroupNumber,@Metakeywords,@Metadesc,@Click,'" + DateTime.Now + "',@isHTML,@isConstr,@islock,@ContentProperty,@Editor,0)";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
            string sqlid = "select top 1 id from " + DTable + " where title=@title and FileName=@FileName order by id desc";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sqlid, param));
        }

        public void updateContentInfo(Foosun.Model.ChInfoContent uc, string DTable)
        {
            SqlParameter[] param = GetChInfoParams(uc);
            string Sql = "update " + DTable + " set ";
            Sql += "ChID=@ChID,ClassID=@ClassID,SpecialID=@SpecialID,title=@title,TitleColor=@TitleColor,TitleITF=@TitleITF,TitleBTF=@TitleBTF,PicURL=@PicURL,NaviContent=@NaviContent,Content=@Content,Author=@Author,Souce=@Souce,OrderID=@OrderID,Tags=@Tags,";
            Sql += "Templet=@Templet,SavePath=@SavePath,FileName=@FileName,isDelPoint=@isDelPoint,Gpoint=@Gpoint,iPoint=@iPoint,GroupNumber=@GroupNumber,Metakeywords=@Metakeywords,Metadesc=@Metadesc,Click=@Click,isHTML=@isHTML,isConstr=@isConstr,islock=@islock,ContentProperty=@ContentProperty,Editor=@Editor where ID=@Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public void updateUserContentInfo(Foosun.Model.ChInfoContent uc, string DTable)
        {
            SqlParameter[] param = GetChInfoParams1(uc);
            string Sql = "update " + DTable + " set ";
            Sql += "ChID=@ChID,ClassID=@ClassID,title=@title,PicURL=@PicURL,NaviContent=@NaviContent,Content=@Content,Author=@Author,Souce=@Souce,Tags=@Tags,";
            Sql += "isConstr=@isConstr,islock=@islock where ID=@Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public void updatePreContentInfo(int ID, string PreContentName, object PreContent, string DTable)
        {
            SqlParameter param = new SqlParameter("@ContentParam", PreContent+"");
            string sql = "update " + DTable + " set " + PreContentName + "=@ContentParam where ID=" + ID + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        private SqlParameter[] GetChInfoParams(Foosun.Model.ChInfoContent uc1)
        {
            SqlParameter[] param = new SqlParameter[30];
            param[0] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[0].Value = uc1.Id;
            param[1] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[1].Value = uc1.ChID;
            param[2] = new SqlParameter("@ClassID", SqlDbType.Int, 4);
            param[2].Value = uc1.ClassID;
            param[3] = new SqlParameter("@SpecialID", SqlDbType.NVarChar, 200);
            param[3].Value = uc1.SpecialID;
            param[4] = new SqlParameter("@title", SqlDbType.NVarChar, 100);
            param[4].Value = uc1.title;
            param[5] = new SqlParameter("@TitleColor", SqlDbType.NVarChar, 10);
            param[5].Value = uc1.TitleColor;
            param[6] = new SqlParameter("@TitleITF", SqlDbType.TinyInt, 1);
            param[6].Value = uc1.TitleITF;
            param[7] = new SqlParameter("@TitleBTF", SqlDbType.TinyInt, 1);
            param[7].Value = uc1.TitleBTF;
            param[8] = new SqlParameter("@PicURL", SqlDbType.NVarChar, 200);
            param[8].Value = uc1.PicURL;
            param[9] = new SqlParameter("@NaviContent", SqlDbType.NVarChar, 200);
            param[9].Value = uc1.NaviContent;
            param[10] = new SqlParameter("@Content", SqlDbType.NText);
            param[10].Value = uc1.Content;
            param[11] = new SqlParameter("@Author", SqlDbType.NVarChar, 100);
            param[11].Value = uc1.Author;
            param[12] = new SqlParameter("@Souce", SqlDbType.NVarChar, 100);
            param[12].Value = uc1.Souce;
            param[13] = new SqlParameter("@OrderID", SqlDbType.TinyInt, 1);
            param[13].Value = uc1.OrderID;
            param[14] = new SqlParameter("@Tags", SqlDbType.NVarChar, 100);
            param[14].Value = uc1.Tags;
            param[15] = new SqlParameter("@Templet", SqlDbType.NVarChar, 200);
            param[15].Value = uc1.Templet;
            param[16] = new SqlParameter("@SavePath", SqlDbType.NVarChar, 200);
            param[16].Value = uc1.SavePath;
            param[17] = new SqlParameter("@FileName", SqlDbType.NVarChar, 100);
            param[17].Value = uc1.FileName;
            param[18] = new SqlParameter("@isDelPoint", SqlDbType.TinyInt, 1);
            param[18].Value = uc1.isDelPoint;
            param[19] = new SqlParameter("@Gpoint", SqlDbType.Int, 4);
            param[19].Value = uc1.Gpoint;
            param[20] = new SqlParameter("@iPoint", SqlDbType.Int, 4);
            param[20].Value = uc1.iPoint;
            param[21] = new SqlParameter("@GroupNumber", SqlDbType.NText);
            param[21].Value = uc1.GroupNumber;
            param[22] = new SqlParameter("@Metakeywords", SqlDbType.NVarChar, 200);
            param[22].Value = uc1.Metakeywords;
            param[23] = new SqlParameter("@Metadesc", SqlDbType.NVarChar, 200);
            param[23].Value = uc1.Metadesc;
            param[24] = new SqlParameter("@Click", SqlDbType.Int, 4);
            param[24].Value = uc1.Click;
            param[25] = new SqlParameter("@isHTML", SqlDbType.TinyInt, 1);
            param[25].Value = uc1.isHTML;
            param[26] = new SqlParameter("@isConstr", SqlDbType.TinyInt, 1);
            param[26].Value = uc1.isConstr;
            param[27] = new SqlParameter("@islock", SqlDbType.TinyInt, 1);
            param[27].Value = uc1.islock;
            param[28] = new SqlParameter("@Editor", SqlDbType.NVarChar, 150);
            param[28].Value = uc1.Editor;
            param[29] = new SqlParameter("@ContentProperty", SqlDbType.NVarChar, 9);
            param[29].Value = uc1.ContentProperty;
            return param;
        }

        private SqlParameter[] GetChInfoParams1(Foosun.Model.ChInfoContent uc1)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[0].Value = uc1.Id;
            param[1] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[1].Value = uc1.ChID;
            param[2] = new SqlParameter("@ClassID", SqlDbType.Int, 4);
            param[2].Value = uc1.ClassID;
            param[3] = new SqlParameter("@title", SqlDbType.NVarChar, 100);
            param[3].Value = uc1.title;
            param[4] = new SqlParameter("@PicURL", SqlDbType.NVarChar, 200);
            param[4].Value = uc1.PicURL;
            param[5] = new SqlParameter("@NaviContent", SqlDbType.NVarChar, 200);
            param[5].Value = uc1.NaviContent;
            param[6] = new SqlParameter("@Content", SqlDbType.NText);
            param[6].Value = uc1.Content;
            param[7] = new SqlParameter("@Author", SqlDbType.NVarChar, 100);
            param[7].Value = uc1.Author;
            param[8] = new SqlParameter("@Souce", SqlDbType.NVarChar, 100);
            param[8].Value = uc1.Souce;
            param[9] = new SqlParameter("@Tags", SqlDbType.NVarChar, 100);
            param[9].Value = uc1.Tags;
            param[10] = new SqlParameter("@isConstr", SqlDbType.TinyInt, 1);
            param[10].Value = uc1.isConstr;
            param[11] = new SqlParameter("@islock", SqlDbType.TinyInt, 1);
            param[11].Value = uc1.islock;
            return param;
        }
        public int lockContent(int id, int ChID, int num)
        {
            string sql = string.Empty;
            SqlParameter param = new SqlParameter("@id", id);
            string dTable = getChannelTable(ChID);
            if (num != 2)
            {
                sql = "update " + dTable + " set islock=" + num + " where id=@id and ChID=" + ChID + "";
            }
            else
            {
                sql = "update " + dTable + " set orderId=0 where id=@id and ChID=" + ChID + "";
            }
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, sql, param));
        }

        public void setOrderContent(int id, int ChID, int num)
        {
            SqlParameter param = new SqlParameter("@id", id);
            string dTable = getChannelTable(ChID);
            string sql = "update " + dTable + " set orderId=" + num + " where id=@id and ChID=" + ChID + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public IDataReader getContentAll(int ChID, int ID)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string DTable = getChannelTable(ChID);
            if (DTable != "#")
            {
                string sql = "select * from " + DTable + " where ID=@ID";
                return DbHelper.ExecuteReader(CommandType.Text, sql, param);
            }
            else
            {
                throw new Exception("找不到数据库表，可能是数据库表已被移除");
            }
        }
        #endregion 
        #region 专题部分
        public string getSpecialName(int SpecialID)
        {
            SqlParameter param = new SqlParameter("@SpecialID",SpecialID);
            string sql = "select specialCName from " + Pre + "sys_channelspecial where ID=@SpecialID";
            string CName = Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
            if (CName == string.Empty)
            {
                CName = "根专题";
            }
            return CName;
        }

        public IDataReader getSpecialInfo(int SpecialID)
        {
            SqlParameter param = new SqlParameter("@SpecialID", SpecialID);
            string sql = "select * from " + Pre + "sys_channelspecial where ID=@SpecialID";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }
        /// <summary>
        /// 得到专题英文名称是否重复
        /// </summary>
        /// <param name="EName"></param>
        /// <param name="speicalId"></param>
        /// <returns></returns>
        public int getSpecialCord(string EName, int speicalId)
        {
            SqlParameter param = new SqlParameter("@speicalId", speicalId);
            string sql = "select count(id) from " + Pre + "sys_channelspecial where ID<>@speicalId and specialEName='" + EName + "'";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        public void insertSpecialInfo(Foosun.Model.ChannelSpecialInfo uc)
        {
            SqlParameter[] param = SpecialInfoParameter(uc);
            string Sql = "insert into " + Pre + "sys_channelspecial (";
            Sql += "ChID,OrderID,ParentID,specialCName,specialEName,binddomain,navicontent,savePath,filename,templet,islock,isRec,PicURL";
            Sql += ") values (";
            Sql += "@ChID,@OrderID,@ParentID,@specialCName,@specialEName,@binddomain,@navicontent,@savePath,@filename,@templet,@islock,@isRec,@PicURL)";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public void updateSpecialInfo(Foosun.Model.ChannelSpecialInfo uc)
        {
            SqlParameter[] param = SpecialInfoParameter(uc);
            string Sql = "update " + Pre + "sys_channelspecial set ";
            Sql += "OrderID=@OrderID,specialCName=@specialCName,specialEName=@specialEName,binddomain=@binddomain,navicontent=@navicontent,savePath=@savePath,filename=@filename,templet=@templet,islock=@islock,isRec=@isRec,PicURL=@PicURL";
            Sql += " where ID=@Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        private SqlParameter[] SpecialInfoParameter(Foosun.Model.ChannelSpecialInfo uc1)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[0].Value = uc1.Id;
            param[1] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[1].Value = uc1.ChID;
            param[2] = new SqlParameter("@ParentID", SqlDbType.Int, 4);
            param[2].Value = uc1.ParentID;
            param[3] = new SqlParameter("@OrderID", SqlDbType.TinyInt, 1);
            param[3].Value = uc1.OrderID;
            param[4] = new SqlParameter("@specialCName", SqlDbType.NVarChar, 100);
            param[4].Value = uc1.specialCName;
            param[5] = new SqlParameter("@specialEName", SqlDbType.NVarChar, 100);
            param[5].Value = uc1.specialEName;
            param[6] = new SqlParameter("@binddomain", SqlDbType.NVarChar, 100);
            param[6].Value = uc1.binddomain;
            param[7] = new SqlParameter("@navicontent", SqlDbType.NVarChar,200);
            param[7].Value = uc1.navicontent;
            param[8] = new SqlParameter("@savePath", SqlDbType.NVarChar, 100);
            param[8].Value = uc1.savePath;
            param[9] = new SqlParameter("@filename", SqlDbType.NVarChar, 100);
            param[9].Value = uc1.filename;
            param[10] = new SqlParameter("@templet", SqlDbType.NVarChar, 200);
            param[10].Value = uc1.templet;
            param[11] = new SqlParameter("@islock", SqlDbType.TinyInt, 1);
            param[11].Value = uc1.islock;
            param[12] = new SqlParameter("@isRec", SqlDbType.TinyInt, 1);
            param[12].Value = uc1.isRec;
            param[13] = new SqlParameter("@PicURL", SqlDbType.NVarChar, 200);
            param[13].Value = uc1.PicURL;
            return param;
        }


        public int getSpecialNumber(int SpecialID)
        {
            SqlParameter param = new SqlParameter("@SpecialID", SpecialID);
            string sql = "select count(id) from " + Pre + "sys_channelspecial where ParentID=@SpecialID";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        public int Reset_allSpecial(int SpecialID, int ChID)
        {
            SqlParameter paramd = new SqlParameter("@ChID", ChID);
            string sqld = "select DataLib from " + Pre + "sys_channel where ID=@ChID";
            string dTable = Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sqld, paramd));
            if (dTable != string.Empty)
            {
                SqlParameter param = new SqlParameter("@SpecialID", SpecialID);
                string sql = "update " + dTable + " set SpecialID = replace('SpecialID','" + SpecialID + "' , '')";
                return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, sql, param));
            }
            else
            {
                return 0;
            }
        }

        public int lockstatSpecial(int SpecialID, int num)
        {
            SqlParameter param = new SqlParameter("@SpecialID", SpecialID);
            string sql = "update " + Pre + "sys_channelspecial set islock=" + num + " where Id=@SpecialID";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, sql, param));
        }

        public int delSpecial(int SpecialID)
        {
            SqlParameter param = new SqlParameter("@SpecialID", SpecialID);
            string sql = "delete from " + Pre + "sys_channelspecial where ID=@SpecialID";
            delcSpecial(SpecialID);
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, sql, param));
        }
        public void delcSpecial(int SpecialID)
        {
            SqlParameter param = new SqlParameter("@SpecialID", SpecialID);
            string sqlc = "select id from " + Pre + "sys_channelspecial where ParentID=@SpecialID order by id desc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sqlc, param);
            while (dr.Read())
            {
                int gID = int.Parse(dr["id"].ToString());
                string sql = "delete from " + Pre + "sys_channelspecial where ID=" + gID + "";
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
                delcClass(gID);
            }
            dr.Close();
        }

        public IDataReader getSpecialList(int SpecialID, int ChID)
        {
            SqlParameter param = new SqlParameter("@SpecialID", SpecialID);
            string sql = "select id,specialCName from " + Pre + "sys_channelspecial where ParentID=@SpecialID and islock=0 and ChID=" + ChID + " order by Orderid desc,id desc";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public void utilSpecial(int sSpecialID, int tSpecialID, int ChID)
        {
            //Copy content
            SqlParameter param = new SqlParameter("@ChID", ChID);
            string sql = "select DataLib from " + Pre + "sys_channel where ID=@ChID";
            string dbTable = Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
            if (dbTable != string.Empty)
            {
                string usql = "update " + dbTable + " set SpecialID=replace('SpecialID','" + sSpecialID + "' , '" + tSpecialID + "')";
                DbHelper.ExecuteNonQuery(CommandType.Text, usql, null);
            }
            //更新源栏目下级的父类
            string ssql = "select ParentID from " + Pre + "sys_channelspecial where ID=" + sSpecialID + "";
            string ParentID = Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, ssql, null));
            if (ParentID != string.Empty)
            {
                string usql = "update " + Pre + "sys_channelspecial set ParentID=" + int.Parse(ParentID) + " where ParentID=" + sSpecialID + "";
                DbHelper.ExecuteNonQuery(CommandType.Text, usql, null);
            }
            //删除源栏目
            string delsql = "delete from " + Pre + "sys_channelspecial where id=" + sSpecialID + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, delsql, null);
        }

        public void moveSpecial(int sSpecialID, int tSpecialID)
        {
            string sql = "update " + Pre + "sys_channelspecial set ParentID=" + tSpecialID + " where Id=" + sSpecialID + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int GetSpecialInfoCount(int ID,int ChID)
        {
            SqlParameter param = new SqlParameter("@ID", ID.ToString());
            string sql = "select count(id) from " + getChannelTable(ChID) + " where SpecialID=@ID";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        #endregion 

        public IDataReader getStyleClassList(int ClassID, int ChID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[0].Value = ChID;
            param[1] = new SqlParameter("@ClassID", SqlDbType.Int, 4);
            param[1].Value = ClassID;
            string sql = "select ID,ParentID,cName,ChID,SiteID from " + Pre + "sys_channelstyleclass where ParentID=@ClassID and ChID=@ChID order by id desc";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public DataTable GetStylePage(string keywords, string ClassID, int ChID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            string sFilter = " where a.ChID=" + ChID + " and a.ClassID=b.id";
            if (ClassID != "#0")
            {
                sFilter += " and a.ClassID=" + int.Parse(ClassID) + "";
            }
            if (keywords != "#0")
            {
                sFilter += " and (a.styleName like '%" + keywords + "%' or a.styleContent like '%" + keywords + "%' or a.styleDescript like '%" + keywords + "%')";
            }
            string AllFields = "a.*";
            string Condition = Pre + "sys_channelstyle a," + Pre + "sys_channelstyleclass b" + sFilter;
            string IndexField = "a.Id";
            string OrderFields = "order by a.Id desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        public string getStyleClassName(int ClassID)
        {
            SqlParameter param = new SqlParameter("@ClassID", ClassID);
            string sql = "select cName from " + Pre + "sys_channelstyleclass where ID=@ClassID";
            string CName = Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
            if (CName == string.Empty)
            {
                CName = "根栏目";
            }
            return CName;
        }

        public int delStyleContent(int id, int ChID, int Num)
        {
            string sql = string.Empty;
            SqlParameter param = new SqlParameter("@id", id);
            if (Num == 0)
            {
                sql = "delete from " + Pre + "sys_channelstyle where id=@id and ChID=" + ChID + "";
            }
            else
            {
                if (id == 0)
                {
                    sql = "delete from " + Pre + "sys_channelstyle where ChID=" + ChID + "";
                }
                else
                {
                    sql = "delete from " + Pre + "sys_channelstyle where ClassID=@id and ChID=" + ChID + "";
                }
            }
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, sql, param));
        }

        public int lockStyleContent(int id, int ChID, int num)
        {
            string sql = string.Empty;
            SqlParameter param = new SqlParameter("@id", id);
            if (num != 2)
            {
                sql = "update " + Pre + "sys_channelstyle set islock=" + num + " where id=@id and ChID=" + ChID + "";
            }
            else
            {
                sql = "update " + Pre + "sys_channelstyle set orderId=0 where id=@id and ChID=" + ChID + "";
            }
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, sql, param));
        }


        public void InsertStyleContent(Foosun.Model.styleChContent uc)
        {
            SqlParameter[] param = StyleChInfoParameter(uc);
            string Sql = "insert into " + Pre + "sys_channelstyle (";
            Sql += "ChID,classID,styleName,styleContent,isLock,styleDescript,SiteID,creattime";
            Sql += ") values (";
            Sql += "@ChID,@classID,@styleName,@styleContent,@isLock,@styleDescript,@SiteID,@creattime)";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public void UpdateStyleContent(Foosun.Model.styleChContent uc)
        {
            SqlParameter[] param = StyleChInfoParameter(uc);
            string Sql = "update " + Pre + "sys_channelstyle set ";
            Sql += "classID=@classID,styleName=@styleName,styleContent=@styleContent,isLock=@isLock,styleDescript=@styleDescript,SiteID=@SiteID where id=@Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        private SqlParameter[] StyleChInfoParameter(Foosun.Model.styleChContent uc1)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[0].Value = uc1.Id;
            param[1] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[1].Value = uc1.ChID;
            param[2] = new SqlParameter("@styleName", SqlDbType.NVarChar, 50);
            param[2].Value = uc1.styleName;
            param[3] = new SqlParameter("@styleContent", SqlDbType.NText);
            param[3].Value = uc1.styleContent;
            param[4] = new SqlParameter("@isLock", SqlDbType.TinyInt, 1);
            param[4].Value = uc1.isLock;
            param[5] = new SqlParameter("@styleDescript", SqlDbType.NVarChar, 200);
            param[5].Value = uc1.styleDescript;
            param[6] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[6].Value = uc1.SiteID;
            param[7] = new SqlParameter("@creattime", SqlDbType.DateTime,8);
            param[7].Value = uc1.creattime;
            param[8] = new SqlParameter("@classID", SqlDbType.Int, 4);
            param[8].Value = uc1.classID;
            
            return param;
        }

        public IDataReader GetStyleContent(int Id, int ChID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[0].Value = ChID;
            param[1] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[1].Value = Id;
            string sql = "select * from " + Pre + "sys_channelstyle where Id=@Id and ChID=@ChID";
            return DbHelper.ExecuteReader(CommandType.Text,sql,param);
        }

        public int GetStyleRecord(string CName, int ID,int ChID)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[0].Value = ID;
            param[1] = new SqlParameter("@CName", SqlDbType.NVarChar, 50);
            param[1].Value = CName;
            param[2] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[2].Value = ChID;
            string sql = "select count(id) from " + Pre + "sys_channelstyle where ChID=@ChID and styleName=@CName and ID<>@ID";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        public int GetStyleClassRecord(string cName, int ID, int ChID)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[0].Value = ID;
            param[1] = new SqlParameter("@CName", SqlDbType.NVarChar, 50);
            param[1].Value = cName;
            param[2] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[2].Value = ChID;
            string sql = "select count(id) from " + Pre + "sys_channelstyleclass where ChID=@ChID and cName=@CName and ID<>@ID";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        public void InsertStyleClassContent(int ID, int ChID, string cName)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[0].Value = ChID;
            param[1] = new SqlParameter("@cName", SqlDbType.NVarChar, 50);
            param[1].Value = cName;
            string Sql = "insert into " + Pre + "sys_channelstyleclass (";
            Sql += "ChID,cName,SiteID,ParentID";
            Sql += ") values (";
            Sql += "@ChID,@cName,'0',0)";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public void UpdateStyleClassContent(int ID, int ChID, string cName)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[0].Value = ChID;
            param[1] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[1].Value = ID;
            param[2] = new SqlParameter("@cName", SqlDbType.NVarChar, 50);
            param[2].Value = cName;
            string Sql = "update " + Pre + "sys_channelstyleclass set ";
            Sql += "cName=@cName where id=@Id and ChID=@ChID";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public IDataReader GetStyleClassListManage(int ChID, int ParentID)
        {
            SqlParameter param = new SqlParameter("@ChID", ChID);
            string sql = "select * from " + Pre + "sys_channelstyleclass where ChID=@ChID and ParentID="+ParentID+" order by ID desc";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public IDataReader GetStyleClassInfo(int id, int ChID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[0].Value = ChID;
            param[1] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[1].Value = id;
            string sql = "select * from " + Pre + "sys_channelstyleclass where ChID=@ChID and ID=@Id";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }


        public IDataReader GetDefineStyle(int ChID)
        {
            SqlParameter param = new SqlParameter("@ChID", ChID);
            string sql = "select id,CName,EName,vType,isNulls,isLock from " + Pre + "sys_channelvalue where ChID=@ChID order by OrderID desc,id desc";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public IDataReader GetDefineUserStyle(int ChID)
        {
            SqlParameter param = new SqlParameter("@ChID", ChID);
            string sql = "select id,CName,EName,vType,isNulls,isLock from " + Pre + "sys_channelvalue where ChID=@ChID and isUser=1 order by OrderID desc,id desc";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }
        public IDataReader GetLabelClassList(int ChID, int ParentID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[0].Value = ChID;
            param[1] = new SqlParameter("@ParentID", SqlDbType.Int, 4);
            param[1].Value = ParentID;
            string sql = "select id,ClassName,ParentID from " + Pre + "sys_channellabelclass where ChID=@ChID and ParentID=@ParentID order by id desc";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public IDataReader GetLabelContent(int ChID, int ID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[0].Value = ChID;
            param[1] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[1].Value = ID;
            string sql = "select * from " + Pre + "sys_channellabel where ID=@ID and ChID=@ChID order by id desc";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public int GetLabelNameTF(int ChID, string CName, int ID)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[0].Value = ChID;
            param[1] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[1].Value = ID;
            param[2] = new SqlParameter("@CName", SqlDbType.NVarChar, 80);
            param[2].Value = CName;
            string sql = "select count(ID) from " + Pre + "sys_channellabel where ID<>@ID and LabelName=@CName and ChID=@ChID";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        public void InsertLabelContent(Foosun.Model.LabelChContent uc)
        {
            SqlParameter[] param = LabelChInfoParameter(uc);
            string Sql = "insert into " + Pre + "sys_channellabel (";
            Sql += "ChID,ClassID,LabelName,LabelContent,isLock,LabelDescript,SiteID,creattime";
            Sql += ") values (";
            Sql += "@ChID,@ClassID,@LabelName,@LabelContent,@isLock,@LabelDescript,@SiteID,@CreatTime)";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public void UpdateLabelContent(Foosun.Model.LabelChContent uc)
        {
            SqlParameter[] param = LabelChInfoParameter(uc);
            string Sql = "update " + Pre + "sys_channellabel set ";
            Sql += "ClassID=@ClassID,LabelName=@LabelName,LabelContent=@LabelContent,isLock=@isLock,LabelDescript=@LabelDescript,SiteID=@SiteID where id=@Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        private SqlParameter[] LabelChInfoParameter(Foosun.Model.LabelChContent uc1)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[0].Value = uc1.Id;
            param[1] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[1].Value = uc1.ChID;
            param[2] = new SqlParameter("@LabelName", SqlDbType.NVarChar, 80);
            param[2].Value = uc1.LabelName;
            param[3] = new SqlParameter("@LabelContent", SqlDbType.NText);
            param[3].Value = uc1.LabelContent;
            param[4] = new SqlParameter("@isLock", SqlDbType.TinyInt, 1);
            param[4].Value = uc1.isLock;
            param[5] = new SqlParameter("@LabelDescript", SqlDbType.NVarChar, 200);
            param[5].Value = uc1.LabelDescript;
            param[6] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[6].Value = uc1.SiteID;
            param[7] = new SqlParameter("@creattime", SqlDbType.DateTime, 8);
            param[7].Value = uc1.CreatTime;
            param[8] = new SqlParameter("@ClassID", SqlDbType.Int, 4);
            param[8].Value = uc1.ClassID;
            return param;
        }


        public DataTable GetLabelPage(string keywords, string ClassID, int ChID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            string sFilter = " where a.ChID=" + ChID + " and a.ClassID=b.id";
            if (ClassID != "#0")
            {
                sFilter += " and a.ClassID=" + int.Parse(ClassID) + "";
            }
            if (keywords != "#0")
            {
                sFilter += " and (a.LabelName like '%" + keywords + "%' or a.LabelContent like '%" + keywords + "%' or a.LabelDescript like '%" + keywords + "%')";
            }
            string AllFields = "a.*";
            string Condition = Pre + "sys_channellabel a," + Pre + "sys_channellabelclass b" + sFilter;
            string IndexField = "a.Id";
            string OrderFields = "order by a.Id desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        public string getLabelClassName(int ClassID)
        {
            SqlParameter param = new SqlParameter("@ClassID", ClassID);
            string sql = "select ClassName from " + Pre + "sys_channellabelclass where ID=@ClassID";
            string CName = Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
            if (CName == string.Empty)
            {
                CName = "根栏目";
            }
            return CName;
        }

        public int delLabelContent(int id, int ChID, int Num)
        {
            string sql = string.Empty;
            SqlParameter param = new SqlParameter("@id", id);
            if (Num == 0)
            {
                sql = "delete from " + Pre + "sys_channellabel where id=@id and ChID=" + ChID + "";
            }
            else
            {
                if (id == 0)
                {
                    sql = "delete from " + Pre + "sys_channellabel where ChID=" + ChID + "";
                }
                else
                {
                    sql = "delete from " + Pre + "sys_channellabel where ClassID=@id and ChID=" + ChID + "";
                }
            }
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, sql, param));
        }

        public int lockLabelContent(int id, int ChID, int num)
        {
            string sql = string.Empty;
            SqlParameter param = new SqlParameter("@id", id);
            if (num != 2)
            {
                sql = "update " + Pre + "sys_channellabel set islock=" + num + " where id=@id and ChID=" + ChID + "";
            }
            else
            {
                sql = "update " + Pre + "sys_channellabel set orderId=0 where id=@id and ChID=" + ChID + "";
            }
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, sql, param));
        }
        public IDataReader GetLabelClassInfo(int id, int ChID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[0].Value = ChID;
            param[1] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[1].Value = id;
            string sql = "select * from " + Pre + "sys_channellabelclass where ChID=@ChID and ID=@Id";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public int GetLabelClassRecord(string cName, int ID, int ChID)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[0].Value = ID;
            param[1] = new SqlParameter("@ClassName", SqlDbType.NVarChar, 80);
            param[1].Value = cName;
            param[2] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[2].Value = ChID;
            string sql = "select count(id) from " + Pre + "sys_channellabelclass where ChID=@ChID and ClassName=@ClassName and ID<>@ID";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        public void InsertLabelClassContent(int ID, int ChID, string cName)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[0].Value = ChID;
            param[1] = new SqlParameter("@ClassName", SqlDbType.NVarChar, 80);
            param[1].Value = cName;
            string Sql = "insert into " + Pre + "sys_channellabelclass (";
            Sql += "ChID,ClassName,SiteID,ParentID";
            Sql += ") values (";
            Sql += "@ChID,@ClassName,'0',0)";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public void UpdateLabelClassContent(int ID, int ChID, string cName)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[0].Value = ChID;
            param[1] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[1].Value = ID;
            param[2] = new SqlParameter("@ClassName", SqlDbType.NVarChar, 80);
            param[2].Value = cName;
            string Sql = "update " + Pre + "sys_channellabelclass set ";
            Sql += "ClassName=@ClassName where id=@Id and ChID=@ChID";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public IDataReader GetLabelClassListManage(int ChID, int ParentID)
        {
            SqlParameter param = new SqlParameter("@ChID", ChID);
            string sql = "select * from " + Pre + "sys_channellabelclass where ChID=@ChID and ParentID=" + ParentID + " order by ID desc";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public int delLabelClassContent(int id, int chid)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[0].Value = chid;
            param[1] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[1].Value = id;
            string dsql = "delete from " + Pre + "sys_channellabel where ClassID=" + id + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, dsql, null);
            string Sql = "delete from " + Pre + "sys_channellabelclass where id=@Id and ChID=@ChID";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }

        public int delStyleClassContent(int id, int chid)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[0].Value = chid;
            param[1] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[1].Value = id;
            string dsql = "delete from " + Pre + "sys_channelstyle where ClassID=" + id + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, dsql, null);
            string Sql = "delete from " + Pre + "sys_channelstyleclass where id=@Id and ChID=@ChID";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }

        public DataTable GetSLabelPage(int ChID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            string sFilter = string.Empty;
            if (ChID != 0)
            {
                sFilter = " where ChID=" + ChID + " and islock=0";
            }
            else
            {
                sFilter = " where islock=0";
            }
            string AllFields = "*";
            string Condition = Pre + "sys_channellabel" + sFilter;
            string IndexField = "Id";
            string OrderFields = "order by Id desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }
        /// <summary>
        /// 得到所有样式分类
        /// </summary>
        /// <param name="ChID"></param>
        /// <returns></returns>
        public IDataReader GetStyleListAll(int ChID)
        {
            SqlParameter param = new SqlParameter("@ChID",ChID);
            string sql = "select id,styleName from " + Pre + "sys_channelstyle where ChID=@ChID and islock=0 order by id desc";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        /// <summary>
        /// 频道栏目是否是单页面
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public int getclassPage(int ClassID)
        {
            string sql = "select isPage from " + Pre + "sys_channelclass where ID=@ClassID";
            SqlParameter Param = new SqlParameter("@ClassID", ClassID);
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, Param));
        }

        public int getClassIDfromTable(int ID, int ChID)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string sql = "select ClassID from " + getChannelTable(ChID) + " where Id=@ID";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        public void updateInfoSpecial(string ID, string SpecialID,int ChID)
        {
            string sql = "update " + getChannelTable(ChID) + " set SpecialID='" + SpecialID + "' where Id in (" + ID + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }
        #region 前台会员部分
        public DataTable GetUserChannelPage(string Author,string keywords, string ClassID, int ChID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@keywords", SqlDbType.NVarChar, 100);
            param[0].Value = keywords;
            param[1] = new SqlParameter("@ClassID", SqlDbType.Int, 4);
            param[1].Value = int.Parse(ClassID);

            string sqlcon = " where Author='" + Author + "'";
            if (keywords != string.Empty)
            {
                sqlcon += " and (title like '%@keywords%' or content like '%@keywords%')";
            }
            if (ClassID != "0")
            {
                sqlcon += " and ClassID=@ClassID";
            }
            
            string dbTable = getChannelTable(ChID);
            string AllFields = "*";
            string Condition = dbTable + sqlcon;
            string IndexField = "Id";
            string OrderFields = "order by OrderID desc,Id desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, param);
        }

        public void updateUserInfo(int Id, int ChID, int Num, string UserName)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[0].Value = Id;
            param[1] = new SqlParameter("@Num", SqlDbType.TinyInt, 1);
            param[1].Value = Num;
            param[2] = new SqlParameter("@UserName", SqlDbType.NVarChar, 30);
            param[2].Value = UserName;
            string DTalbe = getChannelTable(ChID);
            string sql = string.Empty;
            if (Num == 2)
            {
                sql = "delete from " + DTalbe + " where ID=@ID and Author=@UserName";
            }
            else
            {
                sql = "update " + DTalbe + " set ConstrTF=@Num where ID=@ID and Author=@UserName";
            }
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);

        }

        public string getfUrl(int ID, int ChID)
        {
            string DTable = getChannelTable(ChID);
            string UrlStr = string.Empty;
            SqlParameter param = new SqlParameter("@ID", ID);
            string sql = "select * from " + DTable + " where ID=@ID";
            string dirHTML = Foosun.Common.Public.readCHparamConfig("htmldir", ChID);
            dirHTML = dirHTML.Replace("{@dirHTML}", Foosun.Config.UIConfig.dirHtml);
            string dimm = Foosun.Config.UIConfig.dirDumm;
            if (dimm.Trim() != string.Empty)
            {
                dimm = "/" + dimm;
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, param);
            if (dr.Read())
            {
                IDataReader cdr = GetClassInfo(int.Parse(dr["ClassID"].ToString()));
                if (cdr.Read())
                {
                    UrlStr = "<a href=\"" + dimm + "/" + dirHTML + "/" + cdr["SavePath"].ToString() + "/" + dr["SavePath"].ToString() + "/" + dr["FileName"] + "\" target=\"_blank\" class=\"list_link\">" + dr["Title"].ToString() + "</a>";
                }
                cdr.Close();
            }
            dr.Close();
            UrlStr = UrlStr.Replace("//", "/");
            return UrlStr;
        }

        public int AddinfoClick(int ID, int ChID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[0].Value = ID;
            param[1] = new SqlParameter("@ChID", SqlDbType.Int, 4);
            param[1].Value = ChID;
            string DTable = getChannelTable(ChID);
            string sql = "update " + DTable + " set click=click+1 where ID=@ID";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
            string csql = "select click from " + DTable + " where ID=@ID";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, csql, param));
        }
        #endregion 
    }
}
