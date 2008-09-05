using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Foosun.Model;
using Foosun.DALFactory;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
{
    public class Ads : DbBase, IAds
    {
        private string SiteID;
        public Ads()
        {
            SiteID = Foosun.Global.Current.SiteID;
        }

        /// <summary>
        /// 获得广告列表/分类列表
        /// </summary>
        /// <param name="ali"></param>
        /// <returns></returns>
        public DataTable list(Foosun.Model.AdsListInfo ali)
        {
            string tempselect = string.Empty;
            string str_Sql = "";
            switch (ali.type)
            {
                case "Ads":
                    switch (ali.showAdsType)
                    {
                        case "showadstype":
                            if (ali.adsType != null && ali.adsType != "" && ali.adsType != string.Empty && ali.adsType != "-1")
                                tempselect = "And adType=" + ali.adsType + "";
                            else
                                tempselect = "";

                            if (ali.showSiteID != null && ali.showSiteID != "" && ali.showSiteID != string.Empty)
                                tempselect += " And " + Pre + "ads.SiteID='" + ali.showSiteID + "'";
                            else
                                tempselect += " And " + Pre + "ads.SiteID='" + SiteID + "'";

                            str_Sql = "Select " + Pre + "ads.AdID," + Pre + "ads.adName," + Pre + "ads.adType," + Pre + "ads.creatTime," + Pre + "ads.CusID," + Pre + "ads.isLock,Cname From " + Pre + "ads left join " + Pre + "ads_class on " + Pre + "ads.ClassID=" + Pre + "ads_class.AcID Where" +
                                      " 1=1 " + tempselect + " Order By " + Pre + "ads.Id Desc";
                            break;
                        case "search":
                            if (SiteID == "0")
                                tempselect = "";
                            else
                                tempselect = " And SiteID='" + SiteID + "'";

                            switch (ali.searchType)
                            {
                                case "adsname":
                                    str_Sql = "Select " + Pre + "ads.AdID," + Pre + "ads.adName," + Pre + "ads.adType," + Pre + "ads.creatTime," + Pre + "ads.CusID," + Pre + "ads.isLock,Cname From " + Pre + "ads left join " + Pre + "ads_class on " + Pre + "ads.ClassID=" + Pre + "ads_class.AcID " +
                                              " Where " + Pre + "ads.adName Like '%" + ali.SearchKey + "%' " + tempselect + " Order By " + Pre + "ads.Id Desc";
                                    break;
                                case "user":
                                    str_Sql = "Select " + Pre + "ads.AdID," + Pre + "ads.adName," + Pre + "ads.adType," + Pre + "ads.creatTime," + Pre + "ads.CusID," + Pre + "ads.isLock,Cname From " + Pre + "ads left join " + Pre + "ads_class on " + Pre + "ads.ClassID=" + Pre + "ads_class.AcID " +
                                              " Where " + Pre + "ads.CusID In(Select UserNum From " + Pre + "sys_User Where UserName" +
                                              "  Like '%" + ali.SearchKey + "%') " + tempselect + " Order By " + Pre + "ads.Id Desc";
                                    break;
                                default:
                                    str_Sql = "Select " + Pre + "ads.AdID," + Pre + "ads.adName," + Pre + "ads.adType," + Pre + "ads.creatTime," + Pre + "ads.CusID," + Pre + "ads.isLock,Cname From " + Pre + "ads left join " + Pre + "ads_class on " + Pre + "ads.ClassID=" + Pre + "ads_class.AcID where" +
                                              " 1=1 " + tempselect + " Order By " + Pre + "ads.Id Desc";
                                    break;
                            }
                            break;
                        case "site":
                            if (ali.adsType != null && ali.adsType != "" && ali.adsType != string.Empty && ali.adsType != "-1")
                                tempselect = "And adType=" + ali.adsType + "";
                            else
                                tempselect = "";

                            if (ali.showSiteID != null && ali.showSiteID != "" && ali.showSiteID != string.Empty)
                                tempselect += " And SiteID='" + ali.showSiteID + "'";
                            else
                                tempselect += " And SiteID='" + SiteID + "'";

                            str_Sql = "Select " + Pre + "ads.AdID," + Pre + "ads.adName," + Pre + "ads.adType," + Pre + "ads.creatTime," + Pre + "ads.CusID," + Pre + "ads.isLock,Cname From " + Pre + "ads left join " + Pre + "ads_class on " + Pre + "ads.ClassID=" + Pre + "ads_class.AcID Where" +
                                      " 1=1 " + tempselect + " Order By " + Pre + "ads.Id Desc";
                            break;
                        default:
                            str_Sql = "Select " + Pre + "ads.AdID," + Pre + "ads.adName," + Pre + "ads.adType," + Pre + "ads.creatTime," + Pre + "ads.CusID," + Pre + "ads.isLock,Cname From " + Pre + "ads left join " + Pre + "ads_class on " + Pre + "ads.ClassID=" + Pre + "ads_class.AcID Where" +
                                      " " + Pre + "ads.SiteID='" + SiteID + "' Order By " + Pre + "ads.Id Desc";
                            break;
                    }
                    break;
                case "Class":
                    if (ali.showSiteID != null && ali.showSiteID != "" && ali.showSiteID != string.Empty)
                        tempselect = " And " + Pre + "ads_class.SiteID='" + ali.showSiteID + "'";
                    else
                        tempselect = " And " + Pre + "ads_class.SiteID='" + SiteID + "'";

                    str_Sql = "Select AcID,Cname,Adprice,creatTime From " + Pre + "ads_class Where ParentID='0' " +
                              "" + tempselect + " Order By Id Desc";
                    break;
                case "Stat":
                    if (ali.showSiteID != null && ali.showSiteID != "" && ali.showSiteID != string.Empty)
                        tempselect = " And " + Pre + "ads.SiteID='" + ali.showSiteID + "'";
                    else
                        tempselect = " And " + Pre + "ads.SiteID='" + SiteID + "'";

                    str_Sql = "Select AdID,adName,clickNum,showNum From " + Pre + "ads Where 1=1 " + tempselect + " Order By " + Pre + "ads.Id Desc";
                    break;
                case "ClassName":
                    str_Sql = "select " + Pre + "ads.AdID," + Pre + "ads.adName," + Pre + "ads.adType," + Pre + "ads.creatTime," + Pre + "ads.CusID," + Pre + "ads.isLock,Cname from " + Pre + "ads left join " + Pre + "ads_class on " + Pre + "ads.ClassID=" + Pre + "ads_class.AcID where Cname like '%" + ali.SearchKey + "%'";
                    break;
            }
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }

        /// <summary>
        /// 得到广告的子分类
        /// </summary>
        /// <param name="classid">父分类ID</param>
        /// <returns></returns>
        public DataTable childlist(string classid)
        {
            string str_Sql = "Select AcID,Cname,Adprice,creatTime From " + Pre + "ads_class Where" +
                             " SiteID='" + SiteID + "' And ParentID='" + classid + "'";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }

        /// <summary>
        /// 锁定广告
        /// </summary>
        /// <param name="id"></param>
        public void Lock(string id)
        {
            string str_Sql = "Update " + Pre + "ads Set isLock=1 Where AdID='" + id + "' And SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }

        /// <summary>
        /// 解锁广告
        /// </summary>
        /// <param name="id"></param>
        public void UnLock(string id)
        {
            string str_Sql = "Update " + Pre + "ads Set isLock=0 Where AdID='" + id + "' And SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        /// <summary>
        /// 删除所有的广告
        /// </summary>
        public void DelAllAds()
        {
            string str_Sql = "Delete From " + Pre + "adstxt Where AdID In (Select AdID From " + Pre + "ads Where SiteID='" + SiteID + "')";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
            string str_Sql1 = "Delete From " + Pre + "ads_stat Where AdID In (Select AdID From " + Pre + "ads Where SiteID='" + SiteID + "')";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql1, null);
            string str_Sql2 = "Delete From " + Pre + "ads Where SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql2, null);
        }
        /// <summary>
        /// 得到广告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable AdsDt(string id)
        {
            string str_Sql = "";
            if (id != null && id != "" && id != string.Empty)
                str_Sql = "Select AdID,ClassID From " + Pre + "ads Where SiteID='" + SiteID + "' And AdID In (" + id + ")";
            else
                str_Sql = "Select AdID,ClassID From " + Pre + "ads Where SiteID='" + SiteID + "'";

            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }
        /// <summary>
        /// 批量删除广告
        /// </summary>
        /// <param name="id"></param>
        public void DelPAds(string id)
        {
            OleDbConnection Conn = new OleDbConnection(Database.FoosunConnectionString);
            Conn.Open();
            OleDbTransaction tran = Conn.BeginTransaction();
            try
            {
                string str_Sql = "Delete From " + Pre + "adstxt Where AdID In (" + id + ")";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql, null);
                string str_Sql1 = "Delete From " + Pre + "ads_stat Where AdID In (" + id + ")";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql1, null);
                string str_Sql2 = "Delete From " + Pre + "ads Where SiteID='" + SiteID + "' And AdID In(" + id + ")";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql2, null);
                tran.Commit();
                Conn.Close();
            }
            catch (OleDbException e)
            {
                tran.Rollback();
                Conn.Close();
                throw e;
            }
        }

        /// <summary>
        /// 删除所有广告分类
        /// </summary>
        public void DelAllAdsClass()
        {
            string str_Sql = "Delete From " + Pre + "adstxt Where AdID In (Select AdID From " + Pre + "ads Where ClassID In(Select " +
                             "AcID From " + Pre + "ads_class Where SiteID='" + SiteID + "'))";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
            string str_Sql1 = "Delete From " + Pre + "ads_stat Where AdID In (Select AdID From " + Pre + "ads Where ClassID In(Select " +
                              "AcID From " + Pre + "ads_class Where SiteID='" + SiteID + "'))";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql1, null);
            string str_Sql2 = "Delete From " + Pre + "ads Where And ClassID In(Select " +
                              "AcID From " + Pre + "ads_class Where SiteID='" + SiteID + "')";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql2, null);

            string str_Sql3 = "Delete From " + Pre + "ads_class Where SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql3, null);

        }
        public DataTable adsClassDt(string classid)
        {
            string str_Sql = "";
            if (classid != null && classid != "" && classid != string.Empty)
            {
                Recyle rc = new Recyle();
                string idstr = rc.getIDStr(classid, "AcID,ParentID", "ads_class");
                str_Sql = "Select AcID From " + Pre + "ads_class Where SiteID='" + SiteID + "' And AcID in (" + idstr + ")";
            }
            else
                str_Sql = "Select AcID From " + Pre + "ads_class Where SiteID='" + SiteID + "'";

            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }

        /// <summary>
        /// 删除广告分类
        /// </summary>
        /// <param name="classid"></param>
        public void DelPAdsClass(string classid)
        {
            Recyle rc = new Recyle();
            string idstr = rc.getIDStr(classid, "AcID,ParentID", "ads_class");
            string str_Sql = "Delete From " + Pre + "adstxt Where AdID In (Select AdID From " + Pre + "ads Where ClassID In(" + idstr + "))";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
            string str_Sql1 = "Delete From " + Pre + "ads_stat Where AdID In (Select AdID From " + Pre + "ads Where ClassID In(" + idstr + "))";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql1, null);
            string str_Sql2 = "Delete From " + Pre + "ads Where ClassID In(" + idstr + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql2, null);
            string str_Sql3 = "Delete From " + Pre + "ads_class Where SiteID='" + SiteID + "' And AcID In(" + idstr + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql3, null);
        }

        /// <summary>
        /// 增加广告分类
        /// </summary>
        /// <param name="aci"></param>
        /// <returns></returns>
        public int AddClass(Foosun.Model.AdsClassInfo aci)
        {
            string checkSql = "";
            int recordCount = 0;
            string AcID = Foosun.Common.Rand.Number(12);
            while (true)
            {
                checkSql = "select count(*) from " + Pre + "ads_class where AcID='" + AcID + "'";
                recordCount = (int)DbHelper.ExecuteScalar(CommandType.Text, checkSql, null);
                if (recordCount < 1)
                    break;
                else
                    AcID = Foosun.Common.Rand.Number(12, true);
            }
            checkSql = "select count(*) from " + Pre + "ads_class where Cname='" + aci.Cname + "'";
            recordCount = (int)DbHelper.ExecuteScalar(CommandType.Text, checkSql, null);
            if (recordCount > 0)
            {
                throw new Exception("分类名称重复,请重新添加!");
            }
            string Sql = "insert into " + Pre + "ads_class (AcID,Cname,ParentID,creatTime,SiteID,Adprice";
            Sql += ") values ('" + AcID + "',";
            Sql += "@Cname,@ParentID,@creatTime,@SiteID,@Adprice)";
            OleDbParameter[] param = Database.getNewParam(GetClassParameters(aci), "Cname,ParentID,creatTime,SiteID,Adprice");
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }

        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <param name="aci"></param>
        /// <returns></returns>
        public int EditClass(Foosun.Model.AdsClassInfo aci)
        {
            string checkSql = "select count(*) from " + Pre + "ads_class Where AcID<>'" + aci.AcID + "' And Cname='" + aci.Cname + "'";
            int recordCount = (int)DbHelper.ExecuteScalar(CommandType.Text, checkSql, null);
            if (recordCount > 0)
            {
                throw new Exception("分类名称重复,请重新修改!");
            }
            string Sql = "Update " + Pre + "ads_class Set Cname=@Cname,Adprice=@Adprice Where AcID=@AcID";
            OleDbParameter[] param = Database.getNewParam(GetClassParameters(aci), "Cname,Adprice,AcID");
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }

        public DataTable getAdsClassInfo(string classid)
        {
            string str_Sql = "Select Cname,ParentID,Adprice From " + Pre + "ads_class Where AcID='" + classid + "'";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }

        public void statDelAll()
        {
            string str_Sql = "Delete From " + Pre + "ads_stat Where AdID In(Select AdID From " + Pre + "ads Where SiteID='" + SiteID + "')";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
            string str_Sql1 = "Update " + Pre + "ads Set ClickNum=0,ShowNum=0 Where SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql1, null);
        }

        public void statDel(string idstr)
        {
            string str_Sql = "Delete From " + Pre + "ads_stat Where AdID In(" + idstr + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
            string str_Sql1 = "Update " + Pre + "ads Set ClickNum=0,ShowNum=0 Where SiteID='" + SiteID + "' And  AdID In(" + idstr + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql1, null);
        }

        public DataTable getAdsClassList()
        {
            string str_Sql = "Select AcID,Cname,ParentID,Adprice From " + Pre + "ads_class Where SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }
        public DataTable getAdsList(string id)
        {
            string str_Sql = "";
            if (id != null && id != "" && id != string.Empty)
                str_Sql = "Select AdID,adName From " + Pre + "ads Where SiteID='" + SiteID + "' And isLock=0 And adType<>11 And AdID<>'" + id + "'";
            else
                str_Sql = "Select AdID,adName From " + Pre + "ads Where SiteID='" + SiteID + "' And isLock=0 And adType<>11";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }

        public string adsAdd(Foosun.Model.AdsInfo ai)
        {
            string AdID = "";
            string checkSql = "";
            int recordCount = 0;
            AdID = Foosun.Common.Rand.Number(15);
            while (true)
            {
                checkSql = "select count(*) from " + Pre + "ads where AdID='" + AdID + "'";
                recordCount = (int)DbHelper.ExecuteScalar(CommandType.Text, checkSql, null);
                if (recordCount < 1)
                    break;
                else
                    AdID = Foosun.Common.Rand.Number(15, true);
            }
            checkSql = "select count(*) from " + Pre + "ads where adName='" + ai.adName + "'";
            recordCount = (int)DbHelper.ExecuteScalar(CommandType.Text, checkSql, null);
            if (recordCount > 0)
            {
                throw new Exception("广告名称重复,请重新添加!");
            }
            string Sql = "Insert Into " + Pre + "ads(AdID,adName,ClassID,CusID,adType,leftPic,rightPic,leftSize,rightSize," +
                         "LinkURL,CycTF,CycAdID,CycSpeed,CycDic,ClickNum,ShowNum,CondiTF,maxShowClick,TimeOutDay,maxClick," +
                         "creatTime,AdTxtNum,isLock,SiteID) Values('" + AdID + "',@adName,@ClassID,@CusID,@adType,@leftPic," +
                         "@rightPic,@leftSize,@rightSize,@LinkURL,@CycTF,@CycAdID,@CycSpeed,@CycDic,@ClickNum,@ShowNum," +
                         "@CondiTF,@maxShowClick,@TimeOutDay,@maxClick,@creatTime,@AdTxtNum,@isLock,@SiteID)";
            if (ai.adType == 11)
            {
                string[] arr_Content = ai.AdTxtContent.Split(',');
                string[] arr_Css = ai.AdTxtCss.Split(',');
                string[] arr_Link = ai.AdTxtLink.Split(',');
                string str_txtSql = "";
                for (int i = 0; i < arr_Content.Length; i++)
                {
                    str_txtSql = "Insert Into " + Pre + "adstxt(AdID,AdTxt,AdCss,AdLink) Values('" + AdID + "','" + arr_Content[i].ToString() + "','" + arr_Css[i].ToString() + "','" + arr_Link[i].ToString() + "')";
                    DbHelper.ExecuteNonQuery(CommandType.Text, str_txtSql, null);
                }
            }
            OleDbParameter[] param = Database.getNewParam(GetAdsParameters(ai), "adName,ClassID,CusID,adType,leftPic,rightPic,leftSize,rightSize,LinkURL,CycTF,CycAdID,CycSpeed,CycDic,ClickNum,ShowNum,CondiTF,maxShowClick,TimeOutDay,maxClick,creatTime,AdTxtNum,isLock,SiteID");
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
            return AdID;
        }

        public int adsEdit(Foosun.Model.AdsInfo ai)
        {
            string checkSql = "";
            int recordCount = 0;
            checkSql = "select count(*) from " + Pre + "ads Where AdID<>'" + ai.AdID + "' And adName='" + ai.adName + "'";
            recordCount = (int)DbHelper.ExecuteScalar(CommandType.Text, checkSql, null);
            if (recordCount > 0)
            {
                throw new Exception("广告名称重复,请重新修改!");
            }

            string Sql = "";

            if (ai.adType == 11)
            {
                Sql = "Update " + Pre + "ads Set adName=@adName,ClassID=@ClassID,adType=@adType,leftPic='',rightPic=''," +
                      "leftSize='',rightSize='',LinkURL='',CondiTF=@CondiTF,maxShowClick=@maxShowClick,TimeOutDay=@TimeOutDay," +
                      "maxClick=@maxClick,AdTxtNum=@AdTxtNum,isLock=@isLock Where AdID=@AdID";

                string str_DeltxtSql = "Delete From " + Pre + "adstxt Where AdID='" + ai.AdID + "'";
                DbHelper.ExecuteNonQuery(CommandType.Text, str_DeltxtSql, null);

                string[] arr_Content = ai.AdTxtContent.Split(',');
                string[] arr_Css = ai.AdTxtCss.Split(',');
                string[] arr_Link = ai.AdTxtLink.Split(',');
                string str_txtSql = "";
                for (int i = 0; i < arr_Content.Length; i++)
                {
                    str_txtSql = "Insert Into " + Pre + "adstxt(AdID,AdTxt,AdCss,AdLink) Values('" + ai.AdID + "','" + arr_Content[i].ToString() + "','" + arr_Css[i].ToString() + "','" + arr_Link[i].ToString() + "')";
                    DbHelper.ExecuteNonQuery(CommandType.Text, str_txtSql, null);
                }
                OleDbParameter[] param = Database.getNewParam(GetAdsParameters(ai), "adName,ClassID,adType,CondiTF,maxShowClick,TimeOutDay,maxClick,AdTxtNum,isLock,AdID");
                return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
            }
            else
            {
                Sql = "Update " + Pre + "ads Set adName=@adName,ClassID=@ClassID,adType=@adType,leftPic=@leftPic," +
                      "rightPic=@rightPic,leftSize=@leftSize,rightSize=@rightSize,LinkURL=@LinkURL,CondiTF=@CondiTF," +
                      "maxShowClick=@maxShowClick,TimeOutDay=@TimeOutDay,maxClick=@maxClick,AdTxtNum=0,isLock=@isLock," +
                      "CycTF=@CycTF,CycAdID=@CycAdID,CycSpeed=@CycSpeed,CycDic=@CycDic Where AdID=@AdID";

                OleDbParameter[] param = Database.getNewParam(GetAdsParameters(ai), "adName,ClassID,adType,leftPic,rightPic,leftSize,rightSize,LinkURL,CondiTF,maxShowClick,TimeOutDay,maxClick,isLock,CycTF,CycAdID,CycSpeed,CycDic,AdID");
                return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
            }

            
        }

        public DataTable getAdsDomain()
        {
            string str_Sql = "";
            if (SiteID == "0")
                str_Sql = "Select SiteDomain From " + Pre + "sys_Param";
            else
                str_Sql = "Select [Domain] From " + Pre + "news_site Where ChannelID='" + SiteID + "'";

            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }

        public DataTable getAdsPicInfo(string col, string tbname, string id)
        {
            OleDbParameter param = new OleDbParameter("@AdID", id);
            string str_Sql = "Select " + col + " From " + Pre + tbname + " Where AdID=@AdID";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, param);
        }

        public DataTable getAdsInfo(string id)
        {
            string str_Sql = "Select AdID,adName,ClassID,CusID,adType,leftPic,rightPic,leftSize,rightSize,LinkURL,CycTF,CycAdID,CycSpeed,CycDic" +
                             ",CondiTF,maxShowClick,TimeOutDay,maxClick,creatTime,AdTxtNum,isLock From " + Pre +
                             "ads Where AdID='" + id + "'";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }

        public DataTable get24HourStat(string type, string id)
        {
            string str_Sql = "";
            if (type == "1")
            {
                str_Sql = "Select creatTime From " + Pre + "ads_stat Where AdID='" + id + "' And datediff('h',creatTime,Now())" +
                          " < 24 And datediff('h',creatTime,Now()) >=0";
            }
            else
            {
                str_Sql = "Select creatTime From " + Pre + "ads_stat Where AdID='" + id + "'";
            }
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }


        public DataTable getDayStat(string type, string id, string mday)
        {
            string str_Sql = "";
            if (type == "1")
            {
                str_Sql = "Select creatTime From " + Pre + "ads_stat Where AdID='" + id + "' And datediff('d',creatTime,Now()) " +
                          "<= " + mday + " And datediff('h',creatTime,Now()) >=0";
            }
            else
            {
                str_Sql = "Select creatTime From " + Pre + "ads_stat Where AdID='" + id + "'";
            }
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }

        public DataTable getMonthStat(string type, string id)
        {
            string str_Sql = "";
            if (type == "1")
            {
                str_Sql = "Select creatTime From " + Pre + "ads_stat Where AdID='" + id + "' And " +
                          "datediff('mm',creatTime,Now()) < 13 And datediff('h',creatTime,Now()) >=0";
            }
            else
            {
                str_Sql = "Select creatTime From " + Pre + "ads_stat Where AdID='" + id + "'";
            }
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }
        public DataTable getYearStat(string id)
        {
            string str_Sql = "Select creatTime From " + Pre + "ads_stat Where AdID='" + id + "' And datediff('yyyy',creatTime,Now())=datediff('yyyy',Now(),Now())"; ;
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }

        public DataTable getWeekStat(string type, string id)
        {
            string str_Sql = "";
            if (type == "1")
            {
                str_Sql = "Select creatTime From " + Pre + "ads_stat Where AdID='" + id + "' And datediff('ww',creatTime,Now()) < 8 And datediff('ww',creatTime,Now()) >=0";
            }
            else
            {
                str_Sql = "Select creatTime From " + Pre + "ads_stat Where AdID='" + id + "'";
            }
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }
        public DataTable getSourceStat(string id)
        {
            string str_Sql = "Select AdID,Address,Count(IP) as Ipnum from " + Pre + "ads_stat group by AdID,Address having AdID=" + id + " order by Count(IP) desc";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }

        public DataTable getDbNull()
        {
            string str_Sql = "Select ID,IP From " + Pre + "ads_stat Where Address is Null or Address=''";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }

        public void upStat(string adress, string id)
        {
            string str_Sql = "Update " + Pre + "ads_stat Set Address='" + adress + "' where ID='" + id + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }

        public void upShowNum(string id)
        {
            OleDbParameter param = new OleDbParameter("@AdID", id);
            string str_Sql = "Update " + Pre + "ads Set ShowNum=ShowNum+1 Where AdID=@AdID";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, param);
        }
        public void upClickNum(string id, string type)
        {
            OleDbParameter param = new OleDbParameter("@AdID", id);
            string str_Sql = "Update " + Pre + "ads Set ClickNum=ClickNum+1 Where AdID=@AdID";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, param);
        }
        public void addStat(string id, string ip)
        {
            OleDbParameter param = new OleDbParameter("@ID", id);
            string str_Sql = "Insert Into " + Pre + "ads_stat(AdID,IP,creatTime) Values(@ID,'" + ip + "','" + DateTime.Now.ToString() + "')";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, param);
        }
        public DataTable getClassAdprice(string classid)
        {
            string str_Sql = "Select Adprice From " + Pre + "ads_class Where AcID='" + classid + "'";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }
        public DataTable getuserG()
        {
            string str_Sql = "Select gPoint From " + Pre + "sys_User Where UserNum='" + Foosun.Global.Current.UserNum + "'";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }
        public void DelUserG(int Gnum)
        {
            string str_Sql = "Update " + Pre + "sys_User Set gPoint=gPoint-" + Gnum + " Where UserNum='" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }

        private OleDbParameter[] GetAdsParameters(Foosun.Model.AdsInfo ai)
        {
            OleDbParameter[] param = new OleDbParameter[24];
            param[0] = new OleDbParameter("@AdID", OleDbType.VarWChar, 15);
            param[0].Value = ai.AdID;
            param[1] = new OleDbParameter("@adName", OleDbType.VarWChar, 50);
            param[1].Value = ai.adName;
            param[2] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 12);
            param[2].Value = ai.ClassID;

            param[3] = new OleDbParameter("@CusID", OleDbType.VarWChar, 12);
            param[3].Value = ai.CusID;
            param[4] = new OleDbParameter("@adType", OleDbType.Integer, 1);
            param[4].Value = ai.adType;
            param[5] = new OleDbParameter("@leftPic", OleDbType.VarWChar, 200);
            param[5].Value = ai.leftPic;

            param[6] = new OleDbParameter("@rightPic", OleDbType.VarWChar, 200);
            param[6].Value = ai.rightPic;
            param[7] = new OleDbParameter("@leftSize", OleDbType.VarWChar, 12);
            param[7].Value = ai.leftSize;
            param[8] = new OleDbParameter("@rightSize", OleDbType.VarWChar, 12);
            param[8].Value = ai.rightSize;

            param[9] = new OleDbParameter("@CycTF", OleDbType.Integer, 1);
            param[9].Value = ai.CycTF;
            param[10] = new OleDbParameter("@CycAdID", OleDbType.VarWChar, 15);
            param[10].Value = ai.CycAdID;

            param[11] = new OleDbParameter("@CycSpeed", OleDbType.Integer, 4);
            param[11].Value = ai.CycSpeed;
            param[12] = new OleDbParameter("@CycDic", OleDbType.Integer, 1);
            param[12].Value = ai.CycDic;
            param[13] = new OleDbParameter("@ClickNum", OleDbType.Integer, 4);
            param[13].Value = ai.ClickNum;

            param[14] = new OleDbParameter("@ShowNum", OleDbType.Integer, 4);
            param[14].Value = ai.ShowNum;
            param[15] = new OleDbParameter("@CondiTF", OleDbType.Integer, 1);
            param[15].Value = ai.CondiTF;
            param[16] = new OleDbParameter("@maxShowClick", OleDbType.Integer, 4);
            param[16].Value = ai.maxShowClick;

            param[17] = new OleDbParameter("@TimeOutDay", OleDbType.Date, 8);
            param[17].Value = ai.TimeOutDay;
            param[18] = new OleDbParameter("@maxClick", OleDbType.Integer, 4);
            param[18].Value = ai.maxClick;
            param[19] = new OleDbParameter("@creatTime", OleDbType.Date, 8);
            param[19].Value = ai.creatTime;

            param[20] = new OleDbParameter("@AdTxtNum", OleDbType.Integer, 4);
            param[20].Value = ai.AdTxtNum;
            param[21] = new OleDbParameter("@isLock", OleDbType.Integer, 1);
            param[21].Value = ai.isLock;
            param[22] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
            param[22].Value = ai.SiteID;

            param[23] = new OleDbParameter("@LinkURL", OleDbType.VarWChar, 200);
            param[23].Value = ai.LinkURL;

            return param;
        }


        private OleDbParameter[] GetClassParameters(Foosun.Model.AdsClassInfo aci)
        {
            OleDbParameter[] param = new OleDbParameter[6];
            param[0] = new OleDbParameter("@AcID", OleDbType.VarWChar, 12);
            param[0].Value = aci.AcID;
            param[1] = new OleDbParameter("@Cname", OleDbType.VarWChar, 50);
            param[1].Value = aci.Cname;
            param[2] = new OleDbParameter("@ParentID", OleDbType.VarWChar, 12);
            param[2].Value = aci.ParentID;
            param[3] = new OleDbParameter("@creatTime", OleDbType.Date, 8);
            param[3].Value = aci.creatTime;
            param[4] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
            param[4].Value = aci.SiteID;
            param[5] = new OleDbParameter("@Adprice", OleDbType.Integer, 4);
            param[5].Value = aci.Adprice;
            return param;
        }
    }
}
