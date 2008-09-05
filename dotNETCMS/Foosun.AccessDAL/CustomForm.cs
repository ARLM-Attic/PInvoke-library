using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using Foosun.Model;
using Foosun.Config;
using Foosun.DALProfile;
using Foosun.DALFactory;

namespace Foosun.AccessDAL
{
    public class CustomForm : DbBase, ICustomForm
    {
        void ICustomForm.Edit(CustomFormInfo info)
        {
            OleDbConnection cn = new OleDbConnection(Database.FoosunConnectionString);
            cn.Open();
            OleDbTransaction tran = cn.BeginTransaction();
            try
            {
                if (info.id > 0)
                {
                    #region 修改
                    OleDbParameter[] param=GetEditParams(info);
                    string Sql = "update " + Pre + "customform set "+Database.getModifyParam(param)+" where id=" + info.id;
                    //string Sql = "update " + Pre + "customform set formname='" + info.formname + "',accessorypath='" + info.accessorypath + "',accessorysize="+info.accessorysize+",";
                    //Sql += "[memo]='"+info.memo+"',islock="+info.islock+",timelimited="+info.timelimited+",starttime=#"+info.starttime+"#,endtime=#"+info.endtime+"#,";
                    //Sql += "showvalidatecode=" + info.showvalidatecode + ",submitonce=" + info.submitonce + ",isdelpoint=" + info.isdelpoint + ",gpoint=" + info.gpoint + ",ipoint=" + info.ipoint +",";
                    //Sql += "groupnumber='"+info.groupnumber+"' where id=" + info.id;
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, param);
                    #endregion
                }
                else
                {
                    #region 新增
                    string tab = info.formtablename;
                    if (tab.IndexOf("'") >= 0)
                        tab.Replace("'", "''");
                    //string Sql = "IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[" + tab + "]') AND ";
                    //Sql += "OBJECTPROPERTY(id, N'IsUserTable') = 1) SELECT 1 ELSE SELECT 0";
                    //object obj = DbHelper.ExecuteScalar(tran, CommandType.Text, Sql, null);
                    //if (obj != null && obj != DBNull.Value)
                    //{
                    //    if (obj.ToString() == "1")
                    //    {
                    //        throw new Exception("该用户表名已经存在!");
                    //    }
                    //}
                    //else
                    //{
                    //    throw new Exception("该用户表名已经存在(null)!");
                    //}
                    string Sql = "";
                    if (Database.ExitTable(tab))
                    {
                        throw new Exception("该用户表已经存在!");
                    }
                    Sql = "insert into " + Pre + "customform (formname,formtablename,accessorypath,accessorysize,[memo],islock,timelimited,";
                    Sql += "starttime,endtime,showvalidatecode,submitonce,isdelpoint,gpoint,ipoint,groupnumber,addtime) values (";
                    Sql += "@formname,@formtablename,@accessorypath,@accessorysize,@memo,@islock,@timelimited,";
                    Sql += "@starttime,@endtime,@showvalidatecode,@submitonce,@isdelpoint,@gpoint,@ipoint,@groupnumber,'" + DateTime.Now + "')";
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, Database.getNewParam(GetEditParams(info), "formname,formtablename,accessorypath,accessorysize,memo,islock,timelimited,starttime,endtime,showvalidatecode,submitonce,isdelpoint,gpoint,ipoint,groupnumber"));

                    //Sql = "CREATE TABLE [" + tab + "] ([id] [int] IDENTITY (1, 1) NOT NULL ,";
                    //Sql += " [usernum] [nvarchar] (50) NULL ,";
                    //Sql += " [username] [nvarchar] (50) NULL ,";
                    //Sql += " [submit_ip] [nvarchar] (15) NULL ,";
                    //Sql += " [submit_time] [datetime] NULL ,";
                    //Sql += ")ON [PRIMARY]";

                    Sql = "Create Table [" + tab + "]";
                    Sql += "(";
                    Sql += "[Id] COUNTER(1, 1) CONSTRAINT PK_" + tab + " PRIMARY KEY,";
                    Sql += "usernum varchar (50) null,";
                    Sql += "username varchar (50) null,";
                    Sql += "submit_ip varchar (15) null,";
                    Sql += " submit_time date";
                    Sql += ")";


                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, null);
                    Sql = "insert into fs_tables(tabname) values('" + tab + "')";
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, null);
                    #endregion
                }
                tran.Commit();
            }
            catch
            {
                try
                {
                    tran.Rollback();
                }
                catch
                { }
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        protected OleDbParameter[] GetEditParams(CustomFormInfo info)
        {
            OleDbParameter[] Param = new OleDbParameter[15];
            Param[0] = new OleDbParameter("@formname", OleDbType.VarWChar, 50);
            Param[0].Value = info.formname;
            Param[1] = new OleDbParameter("@formtablename", OleDbType.VarWChar, 50);
            Param[1].Value = info.formtablename;
            Param[2] = new OleDbParameter("@accessorypath", OleDbType.VarWChar, 100);
            Param[3] = new OleDbParameter("@accessorysize", OleDbType.Integer);
            if (info.accessorypath == string.Empty && info.accessorysize > 0)
            {
                Param[2].Value = "";
                Param[3].Value = 0;
            }
            else
            {
                Param[2].Value = info.accessorypath;
                Param[3].Value = info.accessorysize;
            }
            Param[4] = new OleDbParameter("@memo", OleDbType.VarWChar, 255);
            if (info.memo == string.Empty || info.memo == null || info.memo == "")
                Param[4].Value = "";
            else
                Param[4].Value = info.memo;
            Param[5] = new OleDbParameter("@islock", OleDbType.Integer);
            if (info.islock == true)
            {
                Param[5].Value = 1;
            }
            else
            {
                Param[5].Value = 0;
            }
            Param[5].Value = info.islock;
            Param[6] = new OleDbParameter("@timelimited", OleDbType.Integer);
            Param[6].Value = info.timelimited;
            if (info.timelimited == true)
            {
                Param[6].Value = 1;
            }
            else
            {
                Param[6].Value = 0;
            }
            Param[7] = new OleDbParameter("@starttime", OleDbType.Date);
            Param[8] = new OleDbParameter("@endtime", OleDbType.Date);
            if (info.timelimited)
            {
                Param[7].Value = info.starttime;
                Param[8].Value = info.endtime;
            }
            else
            {
                Param[7].Value = DateTime.MinValue;
                Param[8].Value = DateTime.MinValue;
            }
            Param[9] = new OleDbParameter("@showvalidatecode", OleDbType.Integer);
            if (info.showvalidatecode == true)
            {
                Param[9].Value = 1;
            }
            else
            {
                Param[9].Value =0;
            }
           
            Param[10] = new OleDbParameter("@submitonce", OleDbType.Integer);
            if (info.submitonce == true)
            {
                Param[10].Value = 1;
            }
            else
            {
                Param[10].Value = 0;
            }
            Param[11] = new OleDbParameter("@isdelpoint", OleDbType.Integer);
            Param[11].Value = info.isdelpoint;
            Param[12] = new OleDbParameter("@gpoint", OleDbType.Integer);
            Param[12].Value = info.gpoint;
            Param[13] = new OleDbParameter("@ipoint", OleDbType.Integer);
            Param[13].Value = info.ipoint;
            Param[14] = new OleDbParameter("@groupnumber", OleDbType.VarWChar);
            //Param[14].Value = "";
            if (info.groupnumber == string.Empty || info.groupnumber == null || info.groupnumber == "")
                Param[14].Value = "";
            else
                Param[14].Value = info.groupnumber;
            return Param;
        }
        CustomFormInfo ICustomForm.GetInfo(int id)
        {
            OleDbConnection cn = new OleDbConnection(Database.FoosunConnectionString);
            cn.Open();
            try
            {
                return GetInfo(cn, id);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        protected CustomFormInfo GetInfo(OleDbConnection cn, int id)
        {
            CustomFormInfo info = new CustomFormInfo();
            string Sql = "select * from " + Pre + "customform where id=" + id;
            bool flag = false;
            IDataReader rd = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, null);
            if (rd.Read())
            {
                info.id = id;
                info.formname = rd["formname"].ToString();
                info.formtablename = rd["formtablename"].ToString();
                if (rd["accessorypath"] != DBNull.Value) info.accessorypath = rd["accessorypath"].ToString();
                if (rd["accessorysize"] != DBNull.Value) info.accessorysize = Convert.ToInt32(rd["accessorysize"]);
                if (rd["memo"] != DBNull.Value) info.memo = rd["memo"].ToString();
                if (rd["islock"] != DBNull.Value) info.islock = Convert.ToBoolean(rd["islock"]);
                info.timelimited = Convert.ToBoolean(rd["timelimited"]);
                if (rd["starttime"] != DBNull.Value) info.starttime = Convert.ToDateTime(rd["starttime"]);
                if (rd["endtime"] != DBNull.Value) info.endtime = Convert.ToDateTime(rd["endtime"]);
                info.showvalidatecode = Convert.ToBoolean(rd["showvalidatecode"]);
                info.submitonce = Convert.ToBoolean(rd["submitonce"]);
                info.isdelpoint = Convert.ToByte(rd["isdelpoint"]);
                info.gpoint = Convert.ToInt32(rd["gpoint"]);
                info.ipoint = Convert.ToInt32(rd["ipoint"]);
                if (rd["groupnumber"] != DBNull.Value) info.groupnumber = rd["groupnumber"].ToString();
                flag = true;
            }
            rd.Close();
            if (!flag)
            {
                throw new Exception("没有找到相关的自定义表单记录!");
            }
            return info;
        }
        void ICustomForm.DeleteForm(int id)
        {
            OleDbConnection cn = new OleDbConnection(Database.FoosunConnectionString);
            cn.Open();
            OleDbTransaction tran = cn.BeginTransaction();
            try
            {
                string Sql = "select formtablename from " + Pre + "customform where id=" + id;
                object obj = DbHelper.ExecuteScalar(tran, CommandType.Text, Sql, null);
                if (obj != null && obj != DBNull.Value)
                {
                    Sql = "delete from " + Pre + "customform where id=" + id;
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, null);
                    Sql = "delete from " + Pre + "customform_item where formid=" + id;
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, null);
                    Sql = "drop table [" + obj + "]";
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, null);
                    tran.Commit();
                }
                else
                {
                    throw new Exception("没有找到相关的自定义表单记录");
                }
            }
            catch
            {
                try
                {
                    tran.Rollback();
                }
                catch
                { }
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        string ICustomForm.GetFormName(int id)
        {
            string Sql = "select formname from " + Pre + "customform where id=" + id;
            object obj = DbHelper.ExecuteScalar(CommandType.Text, Sql, null);
            if (obj == null)
            {
                throw new Exception("没有找到相关的表单记录");
            }
            else
            {
                if (obj == DBNull.Value)
                    return string.Empty;
                else
                    return obj.ToString();
            }
        }
        CustomFormItemInfo ICustomForm.GetFormItemInfo(int itemid)
        {
            CustomFormItemInfo info = new CustomFormItemInfo();
            string Sql = "select a.*,b.formname from " + Pre + "customform_item a inner join " + Pre + "customform b on a.formid=b.id where a.id=" + itemid;
            bool flag = false;
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            if (rd.Read())
            {
                info.id = Convert.ToInt32(rd["id"]);
                info.formid = Convert.ToInt32(rd["formid"]);
                info.formname = rd["formname"].ToString();
                info.seriesnumber = Convert.ToInt32(rd["seriesnumber"]);
                info.fieldname = rd["fieldname"].ToString();
                info.itemname = rd["itemname"].ToString();
                info.itemtype = (EnumCstmFrmItemType)Enum.Parse(typeof(EnumCstmFrmItemType), rd["itemtype"].ToString());
                if (rd["defaultvalue"] != DBNull.Value) info.defaultvalue = rd["defaultvalue"].ToString();
                info.isnotnull = Convert.ToBoolean(rd["isnotnull"].ToString());
                if (rd["itemsize"] != DBNull.Value) info.itemsize = Convert.ToInt32(rd["itemsize"].ToString());
                info.islock = Convert.ToBoolean(rd["islock"].ToString());
                if (rd["prompt"] != DBNull.Value) info.prompt = rd["prompt"].ToString();
                if (rd["selectitem"] != DBNull.Value) info.selectitem = rd["selectitem"].ToString();
                flag = true;
            }
            rd.Close();
            if (!flag)
                throw new Exception("未找到相关的自定义表单项");
            return info;
        }
        int ICustomForm.GetItemCount(int formid)
        {
            string Sql = "select count(id) from " + Pre + "customform_item where formid=" + formid;
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, null));
        }
        void ICustomForm.EditFormItem(CustomFormItemInfo info)
        {
            if (info.id > 0)
                UpdateItem(info);
            else
                AddItem(info);
        }
        /// <summary>
        /// 修改表单项
        /// </summary>
        /// <param name="info"></param>
        protected void UpdateItem(CustomFormItemInfo info)
        {
            OleDbConnection cn = new OleDbConnection(Database.FoosunConnectionString);
            cn.Open();
            try
            {
                string Sql = "select count(id) from " + Pre + "customform_item where formid=" + info.formid + " and itemname=@itemname and id<>" + info.id;
                OleDbParameter Param1 = new OleDbParameter("@itemname", info.itemname);
                if (Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, Param1)) > 0)
                    throw new Exception("该自定义表单已存在相同的表单项名称");
                #region 检查序号
                string UpOldSql = string.Empty;
                Sql = "select id from " + Pre + "customform_item where formid=" + info.formid + " and seriesnumber=" + info.seriesnumber;
                object obj = DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null);
                if (obj != null && obj != DBNull.Value)
                {
                    int oldsnid = Convert.ToInt32(obj);
                    Sql = "select seriesnumber from " + Pre + "customform_item where id=" + info.id;
                    object oldsn = DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null);
                    if (oldsn != null && oldsn != DBNull.Value)
                    {
                        int nold = Convert.ToInt32(oldsn);
                        UpOldSql = "update " + Pre + "customform_item set seriesnumber=" + nold + " where id=" + oldsnid;
                    }
                }
                #endregion
                OleDbTransaction tran = cn.BeginTransaction();
                try
                {
                    Sql = "update " + Pre + "customform_item set "+Database.getModifyParam(GetItemEditParams(info))+" where id=" + info.id;
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, GetItemEditParams(info));
                    if (UpOldSql != string.Empty)
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, UpOldSql, null);
                    tran.Commit();
                }
                catch
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch
                    { }
                    throw;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        /// <summary>
        /// 新增表单项
        /// </summary>
        /// <param name="info"></param>
        protected void AddItem(CustomFormItemInfo info)
        {
            OleDbConnection cn = new OleDbConnection(Database.FoosunConnectionString);
            cn.Open();
            try
            {
                #region 检查字段名是否重复
                string Sql = "select count(id) from " + Pre + "customform_item where formid=" + info.formid + " and itemname=@itemname";
                OleDbParameter Param1 = new OleDbParameter("@itemname", info.itemname);
                if (Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, Param1)) > 0)
                    throw new Exception("该自定义表单已存在相同的表单项名称");
                Sql = "select count(id) from " + Pre + "customform_item where formid=" + info.formid + " and fieldname=@fieldname";
                OleDbParameter Param2 = new OleDbParameter("@fieldname", info.fieldname);
                if (Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, Param2)) > 0)
                    throw new Exception("该自定义表单中已存在相同的字段名称");
                #endregion
                #region 检查字段名是否重复
                #endregion
                #region 获取数据表名
                Sql = "select formtablename from " + Pre + "customform where id=" + info.formid;
                object obj = DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null);
                if (obj == null)
                    throw new Exception("没有找到相关的表单记录");
                else if (obj == DBNull.Value)
                    throw new Exception("相关的数据表名为空");
                string tbnm = obj.ToString();
                #endregion
                #region 检查实际表中是否存在相同名称
                bool flag = false;
                Sql = "select * from " + tbnm + " where 1=0";
                IDataReader rd = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, null);
                DataTable dt = rd.GetSchemaTable();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName.ToLower().Trim() == info.fieldname.ToLower().Trim())
                    {
                        flag = true;
                        break;
                    }
                }
                rd.Close();
                dt.Dispose();
                if (flag)
                    throw new Exception("该自定义表单的数据表中已存在相同的字段名称,或者您使用了系统默认的字段名");
                #endregion
                #region 检查数据表是否存在
                //Sql = "IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[" + tbnm + "]') AND ";
                //Sql += "OBJECTPROPERTY(id, N'IsUserTable') = 1) SELECT 1 ELSE SELECT 0";
                //obj = DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null);
                //if (obj != null && obj != DBNull.Value)
                //{
                //    if (obj.ToString() == "0")
                //    {
                //        throw new Exception("该自定义表单数据表不存在!");
                //    }
                //}
                //else
                //{
                //    throw new Exception("该自定义表单数据表不存在(null)!");
                //}
                if (!Database.ExitTable(tbnm))
                {
                    throw new Exception("该自定义表单数据表不存在!");
                }
                #endregion
                #region 查看序列号是否已被占用
                string UpOldSql = string.Empty;
                Sql = "select id from " + Pre + "customform_item where formid=" + info.formid + " and seriesnumber=" + info.seriesnumber;
                obj = DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null);
                if (obj != null && obj != DBNull.Value)
                {
                    int oldsnid = Convert.ToInt32(obj);
                    Sql = "select max(seriesnumber) from " + Pre + "customform_item where formid=" + info.formid;
                    object maxid = DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null);
                    if (maxid != null && maxid != DBNull.Value)
                    {
                        int nmax = Convert.ToInt32(maxid);
                        nmax++;
                        UpOldSql = "update " + Pre + "customform_item set seriesnumber=" + nmax + " where id=" + oldsnid;
                    }
                }
                #endregion
                OleDbTransaction tran = cn.BeginTransaction();
                try
                {
                    Sql = "insert into " + Pre + "customform_item (seriesnumber,formid,fieldname,itemname,itemtype,defaultvalue,isnotnull,";
                    Sql += "itemsize,islock,prompt,selectitem,addtime) values (";
                    Sql += "@seriesnumber,@formid,@fieldname,@itemname,@itemtype,@defaultvalue,@isnotnull,";
                    Sql += "@itemsize,@islock,@prompt,@selectitem,'" + DateTime.Now + "')";
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, Database.getNewParam(GetItemEditParams(info),Database.getSqlParam(Sql)));
                    if (UpOldSql != string.Empty)
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, UpOldSql, null);
                    string fldtype = "nvarchar(100)";
                    switch (info.itemtype)
                    {
                        case EnumCstmFrmItemType.Numberic:
                            fldtype = "numeric(10,4)";
                            break;
                        case EnumCstmFrmItemType.DateTime:
                            fldtype = "datetime";
                            break;
                        case EnumCstmFrmItemType.MultiLineText:
                            fldtype = "ntext";
                            break;
                    }
                    Sql = "ALTER TABLE [" + tbnm + "] ADD " + info.fieldname + " " + fldtype + " NULL";
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, null);
                    tran.Commit();
                }
                catch
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch
                    { }
                    throw;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        protected OleDbParameter[] GetItemEditParams(CustomFormItemInfo info)
        {
            OleDbParameter[] Param = new OleDbParameter[11];
            Param[0] = new OleDbParameter("@seriesnumber", OleDbType.Integer);
            Param[0].Value = info.seriesnumber;
            Param[1] = new OleDbParameter("@formid", OleDbType.Integer);
            Param[1].Value = info.formid;
            Param[2] = new OleDbParameter("@fieldname", OleDbType.VarWChar, 50);
            Param[2].Value = info.fieldname;
            Param[3] = new OleDbParameter("@itemname", OleDbType.VarWChar, 50);
            Param[3].Value = info.itemname;
            Param[4] = new OleDbParameter("@itemtype", OleDbType.VarWChar, 50);
            Param[4].Value = info.itemtype.ToString();
            Param[5] = new OleDbParameter("@defaultvalue", OleDbType.VarWChar, 50);
            if (info.defaultvalue.Trim() == string.Empty)
                Param[5].Value = DBNull.Value;
            else
                Param[5].Value = info.defaultvalue;
            Param[6] = new OleDbParameter("@isnotnull", OleDbType.Boolean);
            Param[6].Value = info.isnotnull;
            Param[7] = new OleDbParameter("@itemsize", OleDbType.Integer);
            Param[7].Value = info.itemsize;
            Param[8] = new OleDbParameter("@islock", OleDbType.Boolean);
            Param[8].Value = info.islock;
            Param[9] = new OleDbParameter("@prompt", OleDbType.VarWChar, 255);
            if (info.prompt.Trim() == string.Empty)
                Param[9].Value = DBNull.Value;
            else
                Param[9].Value = info.prompt;
            Param[10] = new OleDbParameter("@selectitem", OleDbType.VarWChar);
            if (info.selectitem.Trim() == string.Empty)
                Param[10].Value = DBNull.Value;
            else
                Param[10].Value = info.selectitem;
            return Param;
        }
        void ICustomForm.DeleteFormItem(int itemid)
        {
            OleDbConnection cn = new OleDbConnection(Database.FoosunConnectionString);
            cn.Open();
            try
            {
                string tabnm = string.Empty;
                string fldnm = string.Empty;
                int fm = 0;
                int sn = 0;
                string Sql = "select a.formtablename,b.fieldname,b.formid,b.seriesnumber from " + Pre + "customform a ";
                Sql += "inner join " + Pre + "customform_item b on a.id=b.formid where b.id=" + itemid;
                IDataReader rd = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, null);
                if (rd.Read())
                {
                    tabnm = rd.GetString(0);
                    fldnm = rd.GetString(1);
                    fm = rd.GetInt32(2);
                    sn = rd.GetInt32(3);
                }
                rd.Close();
                if (tabnm == string.Empty || fldnm == string.Empty)
                    throw new Exception("没有找到相关的数据表或字段");
                OleDbTransaction tran = cn.BeginTransaction();
                try
                {
                    Sql = "ALTER TABLE [" + tabnm + "] DROP COLUMN " + fldnm + "";
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, null);
                    Sql = "delete from " + Pre + "customform_item where id=" + itemid;
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, null);
                    Sql = "update " + Pre + "customform_item set seriesnumber=seriesnumber-1 where formid=" + fm + " and seriesnumber>" + sn;
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, null);
                    tran.Commit();
                }
                catch
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch
                    { }
                    throw;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        IList<CustomFormItemInfo> ICustomForm.GetAllInfo(int formid, out CustomFormInfo FormInfo)
        {
            OleDbConnection cn = new OleDbConnection(Database.FoosunConnectionString);
            cn.Open();
            try
            {
                FormInfo = GetInfo(cn, formid);
                IList<CustomFormItemInfo> list = new List<CustomFormItemInfo>();
                string Sql = "select * from " + Pre + "customform_item where islock=0 and formid=" + formid + " order by seriesnumber asc";
                IDataReader rd = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, null);
                while (rd.Read())
                {
                    CustomFormItemInfo info = new CustomFormItemInfo();
                    info.id = Convert.ToInt32(rd["id"]);
                    info.formid = Convert.ToInt32(rd["formid"]);
                    info.seriesnumber = Convert.ToInt32(rd["seriesnumber"]);
                    info.fieldname = rd["fieldname"].ToString();
                    info.itemname = rd["itemname"].ToString();
                    info.itemtype = (EnumCstmFrmItemType)Enum.Parse(typeof(EnumCstmFrmItemType), rd["itemtype"].ToString());
                    if (rd["defaultvalue"] != DBNull.Value) info.defaultvalue = rd["defaultvalue"].ToString();
                    info.isnotnull = Convert.ToBoolean(rd["isnotnull"].ToString());
                    if (rd["itemsize"] != DBNull.Value) info.itemsize = Convert.ToInt32(rd["itemsize"].ToString());
                    info.islock = Convert.ToBoolean(rd["islock"].ToString());
                    if (rd["prompt"] != DBNull.Value) info.prompt = rd["prompt"].ToString();
                    if (rd["selectitem"] != DBNull.Value) info.selectitem = rd["selectitem"].ToString();
                    list.Add(info);
                }
                rd.Close();
                return list;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        void ICustomForm.AddRecord(int formid, SQLConditionInfo[] data)
        {
            OleDbConnection cn = new OleDbConnection(Database.FoosunConnectionString);
            cn.Open();
            try
            {
                string SqlF = string.Empty;
                string SqlV = string.Empty;
                OleDbParameter[] Param = new OleDbParameter[data.Length + 4];
                #region 用户相关信息
                Param[0] = new OleDbParameter("@usernum", OleDbType.VarWChar, 50);
                Param[0].Value = DBNull.Value;
                Param[1] = new OleDbParameter("@username", OleDbType.VarWChar, 50);
                Param[1].Value = DBNull.Value;
                Param[2] = new OleDbParameter("@submit_ip", OleDbType.VarWChar, 15);
                Param[2].Value = Foosun.Common.Public.getUserIP();
                Param[3] = new OleDbParameter("@submit_time", OleDbType.Date);
                Param[3].Value = DateTime.Now;
                #endregion
                CustomFormInfo FormInfo = GetInfo(cn, formid);
                if (FormInfo.isdelpoint != 0)
                {
                    if (Global.Current.IsTimeout())
                        throw new Exception("您还未登录,请先登录才能提交表单");
                    if (FormInfo.submitonce)
                    {
                        string SqlTimes = "select count(id) from " + FormInfo.formtablename + " where usernum='" + Global.Current.UserNum + "'";
                        int times = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, SqlTimes, null));
                        if (times > 1)
                            throw new Exception("该表单只允许每个会员提交一次,您已经提交过了。");
                    }
                    Param[0].Value = Global.Current.UserNum;
                    Param[1].Value = Global.Current.UserName;
                    if (FormInfo.ipoint > 0 || FormInfo.gpoint > 0)
                    {
                        bool bread = false;
                        int ipnt = 0;
                        int gpnt = 0;
                        string grp = string.Empty;
                        string SqlUser = "select iPoint,gPoint,UserGroupNumber from " + Pre + "sys_User where UserNum='" + Global.Current.UserNum + "'";
                        IDataReader rd = DbHelper.ExecuteReader(cn, CommandType.Text, SqlUser, null);
                        if (rd.Read())
                        {
                            ipnt = rd.GetInt32(0);
                            gpnt = rd.GetInt32(1);
                            grp = rd.GetString(2);
                            bread = true;
                        }
                        rd.Close();
                        if (!bread)
                        {
                            throw new Exception("该用户的相关信息未找到");
                        }
                        switch (FormInfo.isdelpoint)
                        {
                            case 1:
                                if (gpnt < FormInfo.gpoint)
                                    throw new Exception("提交需要扣除" + FormInfo.gpoint + "金币,您的金币不足");
                                break;
                            case 2:
                                if (ipnt < FormInfo.ipoint)
                                    throw new Exception("提交需要扣除" + FormInfo.ipoint + "点数,您的点数不足");
                                break;
                            case 3:
                                if (gpnt < FormInfo.gpoint || ipnt < FormInfo.ipoint)
                                    throw new Exception("提交需要扣除金币和点数,您的金币或点数不足");
                                break;
                            case 4:
                                if (gpnt < FormInfo.gpoint)
                                    throw new Exception("提交需要达到" + FormInfo.gpoint + "金币,您的金币不足");
                                break;
                            case 5:
                                if (ipnt < FormInfo.ipoint)
                                    throw new Exception("提交需要达到" + FormInfo.ipoint + "点数,您的点数不足");
                                break;
                            case 6:
                                if (gpnt < FormInfo.gpoint || ipnt < FormInfo.ipoint)
                                    throw new Exception("提交需要达到一定的金币和点数,您的金币或点数不足");
                                break;
                        }
                        if (FormInfo.groupnumber != string.Empty && FormInfo.groupnumber.IndexOf(grp + ",") < 0)
                        {
                            throw new Exception("只有指定的会员组才能提交,您不属于该会员组");
                        }
                    }
                }
                for (int i = 0; i < data.Length; i++)
                {
                    SQLConditionInfo info = data[i];
                    if (i > 0)
                    {
                        SqlF += ",";
                        SqlV += ",";
                    }
                    SqlF += info.name;
                    SqlV += "@" + info.name;
                    Param[i + 4] = new OleDbParameter("@" + info.name, info.value);
                }
                if (string.IsNullOrEmpty(Convert.ToString(Param[0].Value)))
                {
                    Param[0].Value = "";
                }
                if (string.IsNullOrEmpty(Convert.ToString(Param[1].Value)))
                {
                    Param[1].Value = "";
                }
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                OleDbTransaction tran = cn.BeginTransaction();
                try
                {
                    string Sql = "insert into [" + FormInfo.formtablename + "] (usernum,username,submit_ip,submit_time," + SqlF + ")";
                    Sql += " values (@usernum,@username,@submit_ip,@submit_time," + SqlV + ")";
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, Database.getNewParam(Param, Database.getSqlParam(Sql)));
                    if ((FormInfo.isdelpoint == 1 || FormInfo.isdelpoint == 2 || FormInfo.isdelpoint == 3)
                        && (FormInfo.ipoint > 0 || FormInfo.gpoint > 0))
                    {
                        if (FormInfo.isdelpoint == 1)
                            Sql = "update " + Pre + "sys_User set gPoint=gPoint-" + FormInfo.gpoint + " where UserNum='" + Global.Current.UserNum + "'";
                        else if (FormInfo.isdelpoint == 2)
                            Sql = "update " + Pre + "sys_User set iPoint=iPoint-" + FormInfo.ipoint + " where UserNum='" + Global.Current.UserNum + "'";
                        else if (FormInfo.isdelpoint == 3)
                            Sql = "update " + Pre + "sys_User set gPoint=gPoint-" + FormInfo.gpoint + ",iPoint=iPoint-" + FormInfo.ipoint + " where UserNum='" + Global.Current.UserNum + "'";
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, null);
                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        DataTable ICustomForm.GetSubmitData(int formid, out string formname, out string tablename)
        {
            OleDbConnection cn = new OleDbConnection(Database.FoosunConnectionString);
            cn.Open();
            try
            {
                CustomFormInfo FormInfo = GetInfo(cn, formid);
                formname = FormInfo.formname;
                tablename = FormInfo.formtablename;
                return DbHelper.ExecuteTable(cn, CommandType.Text, "select * from " + tablename, null);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        void ICustomForm.TruncateTable(int formid)
        {
            OleDbConnection cn = new OleDbConnection(Database.FoosunConnectionString);
            cn.Open();
            try
            {
                CustomFormInfo FormInfo = GetInfo(cn, formid);
                string tablename = FormInfo.formtablename;
                DbHelper.ExecuteTable(cn, CommandType.Text, "TRUNCATE TABLE " + tablename, null);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
    }
}
