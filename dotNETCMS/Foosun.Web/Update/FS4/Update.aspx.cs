using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.DALProfile;

namespace Foosun.Web.Update.FS4
{
    public partial class Update : System.Web.UI.Page
    {
        public static IDbBase Provider = new Foosun.AccessDAL.DbBase(); 
        public static bool IsAcc = Regex.Equals(ConfigurationManager.AppSettings["WebDAL"].ToString(), "Foosun.AccessDAL");
        public static string SourConnstr = string.Empty;
        public static string TagConnstr = Foosun.Config.DBConfig.CmsConString;
        public static readonly string Prefix = Foosun.Config.UIConfig.dataRe;
        public static int GisSQL = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Server.ScriptTimeout = 1000;
            string gSet = Request.QueryString["set"];
            if (gSet != null && gSet != string.Empty)
            {
                string gtype = Request.QueryString["type"];
                string GConnstr = Request.QueryString["connstr"];
                string MConnstr = Request.QueryString["mConnstr"];
                string isSQL = Request.QueryString["isSQL"];
                if (GConnstr != null && GConnstr != string.Empty)
                {
                    SourConnstr = GConnstr;
                    if (isSQL != "1")
                    {
                        GisSQL = 0;
                        if (gtype == "news" || gtype == "class" || gtype == "special" || gtype == "gen")
                        {
                            SourConnstr = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Server.MapPath(GConnstr) + ";Persist Security Info=True;";
                        }
                        else
                        {
                            SourConnstr = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Server.MapPath(MConnstr) + ";Persist Security Info=True;";
                        }
                    }
                    else
                    {
                        GisSQL = 1;
                    }
                    StatConvertTodotNETCMS(gtype, isSQL);
                }
                else
                {
                    Response.Write("请正确填写连接字符串或者ACCESS路径");
                    Response.End();
                }
            }
        }

        public void StatConvertTodotNETCMS(string gtype, string isSQL)
        {
            IDataReader dr = null;
            int i = 0;
            int m = 0;
            string sql = string.Empty;

            switch (gtype)
            {
                case "news":
                    sql = "select * from FS_NS_News order by id asc";
                    if (GisSQL != 1)
                    {
                        OleDbConnection con = new OleDbConnection(SourConnstr);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand(sql, con);
                        dr = cmd.ExecuteReader();
                    }
                    else
                    {
                        dr = SqlHelper.ExecuteReader(SourConnstr, CommandType.Text, sql, null);
                    }
                    while (dr.Read())
                    {
                        #region 转换开头
                        string NewsID = dr["NewsID"].ToString();
                        try
                        {
                            if (NewsID.Length > 12)
                            {
                                NewsID = NewsID.Substring(0, 12);
                            }
                            int NewsType = 0;//IsURL
                            if (dr["IsURL"].ToString() == "1")
                            {
                                NewsType = 2;
                            }
                            if (dr["isPicNews"].ToString() == "1")
                            {
                                NewsType = 1;
                            }
                            string ClassID = dr["ClassID"].ToString();
                            if (ClassID.Length > 12)
                            {
                                ClassID = ClassID.Substring(0, 12);
                            }
                            string NewsProperty = "0,0,0,0,0,0,0,0";
                            DateTime CreatTime = DateTime.Now;
                            if (Foosun.Common.Input.IsDate(dr["addtime"].ToString()))
                            {
                                CreatTime = DateTime.Parse(dr["addtime"].ToString());
                            }
                            int ContentPicTF = 0;
                            if (dr["IsAdPic"].ToString() == "1")
                            {
                                ContentPicTF = 1;
                            }
                            string CheckStat = "0|0|0|0";
                            int isLock = 0;
                            if (dr["isLock"].ToString() == "1")
                            {
                                CheckStat = "1|1|0|0";
                                isLock = 1;
                            }
                            int isRecyle = 0;
                            if (dr["isRecyle"].ToString() == "1")
                            {
                                isRecyle = 1;
                            }

                            if (IsAcc)  //4.0sql转换到.net access
                            {
                                OleDbParameter[] param = new OleDbParameter[2];
                                //param[0] = new OleDbParameter("@Content", OleDbType.LongVarWChar,300000);
                                //param[0].Value = dr["Content"].ToString();
                                param[0] = new OleDbParameter("@NewsID", OleDbType.LongVarChar, 12);
                                param[0].Value = NewsID;
                                param[1] = new OleDbParameter("@ClassID", OleDbType.LongVarChar, 12);
                                param[1].Value = ClassID;

                                string insertsql = "insert into " + Prefix + "News(NewsID,NewsType,OrderID,NewsTitle,sNewsTitle,TitleColor,TitleITF,TitleBTF,CommLinkTF,SubNewsTF,URLaddress,PicURL,SPicURL,ClassID";
                                insertsql += ",SpecialID,Author,Souce,Tags,NewsProperty,NewsPicTopline,Templet,Content,Metakeywords,Metadesc,naviContent,Click,CreatTime,EditTime,SavePath,FileName,FileEXName";
                                insertsql += ",isDelPoint,Gpoint,iPoint,GroupNumber,ContentPicTF,ContentPicURL,ContentPicSize,CommTF,DiscussTF,TopNum,VoteTF,CheckStat,isLock,isRecyle,SiteID,DataLib,DefineID,isVoteTF";
                                insertsql += ",Editor,isHtml,isConstr,isFiles,vURL";
                                insertsql += ") values (";
                                insertsql += "@NewsID," + NewsType + "," + Convert.ToInt32(dr["PopId"].ToString()) + ",'" + dr["NewsTitle"].ToString() + "','" + dr["CurtTitle"].ToString() + "','" + dr["TitleColor"].ToString() + "'," + Convert.ToInt16(dr["TitleItalic"].ToString()) + "," + Convert.ToInt16(dr["titleBorder"].ToString()) + "," + Convert.ToInt16(dr["isShowReview"].ToString()) + ",0,'" + dr["URLAddress"].ToString() + "','" + dr["NewsPicFile"].ToString() + "','" + dr["NewsSmallPicFile"].ToString() + "',@ClassID";
                                insertsql += ",'0','" + dr["Author"].ToString() + "','" + dr["Source"].ToString() + "','" + (dr["Keywords"].ToString()).Replace(",", "|") + "','" + NewsProperty + "',0,'" + dr["Templet"].ToString() + "','" + dr["Content"].ToString() + "','','','" + dr["NewsNaviContent"].ToString() + "'," + Convert.ToInt32(dr["Hits"].ToString()) + ",'" + CreatTime + "','" + CreatTime + "','" + dr["SaveNewsPath"].ToString() + "','" + dr["FileName"].ToString() + "','." + dr["FileExtName"].ToString() + "'";
                                insertsql += "," + Convert.ToInt32(dr["isPop"].ToString()) + ",0,0,''," + ContentPicTF + ",'" + dr["AdPicAdress"].ToString() + "','" + (dr["AdPicWH"].ToString()).Replace(",", "|") + "',1,0,0,0,'" + CheckStat + "'," + isLock + "," + isRecyle + ",'0','" + Prefix + "news',0,0";
                                insertsql += ",'" + dr["Editor"].ToString() + "',0,0,0,''";
                                insertsql += ")";
                                SqlHelper.ExecuteNonQuery(TagConnstr, CommandType.Text, insertsql, param);
                            }
                            else
                            {
                                SqlParameter[] param = new SqlParameter[3];
                                param[0] = new SqlParameter("@Content", SqlDbType.NText);
                                param[0].Value = dr["Content"].ToString();
                                param[1] = new SqlParameter("@NewsID", SqlDbType.NVarChar, 12);
                                param[1].Value = NewsID;
                                param[2] = new SqlParameter("@ClassID", SqlDbType.NVarChar, 12);
                                param[2].Value = ClassID;

                                string insertsql = "insert into " + Prefix + "News(NewsID,NewsType,OrderID,NewsTitle,sNewsTitle,TitleColor,TitleITF,TitleBTF,CommLinkTF,SubNewsTF,URLaddress,PicURL,SPicURL,ClassID";
                                insertsql += ",SpecialID,Author,Souce,Tags,NewsProperty,NewsPicTopline,Templet,Content,Metakeywords,Metadesc,naviContent,Click,CreatTime,EditTime,SavePath,FileName,FileEXName";
                                insertsql += ",isDelPoint,Gpoint,iPoint,GroupNumber,ContentPicTF,ContentPicURL,ContentPicSize,CommTF,DiscussTF,TopNum,VoteTF,CheckStat,isLock,isRecyle,SiteID,DataLib,DefineID,isVoteTF";
                                insertsql += ",Editor,isHtml,isConstr,isFiles,vURL";
                                insertsql += ") values (";
                                insertsql += "@NewsID," + NewsType + "," + Convert.ToInt32(dr["PopId"].ToString()) + ",'" + dr["NewsTitle"].ToString() + "','" + dr["CurtTitle"].ToString() + "','" + dr["TitleColor"].ToString() + "'," + Convert.ToInt16(dr["TitleItalic"].ToString()) + "," + Convert.ToInt16(dr["titleBorder"].ToString()) + "," + Convert.ToInt16(dr["isShowReview"].ToString()) + ",0,'" + dr["URLAddress"].ToString() + "','" + dr["NewsPicFile"].ToString() + "','" + dr["NewsSmallPicFile"].ToString() + "',@ClassID";
                                insertsql += ",'0','" + dr["Author"].ToString() + "','" + dr["Source"].ToString() + "','" + (dr["Keywords"].ToString()).Replace(",", "|") + "','" + NewsProperty + "',0,'" + dr["Templet"].ToString() + "',@Content,'','','" + dr["NewsNaviContent"].ToString() + "'," + Convert.ToInt32(dr["Hits"].ToString()) + ",'" + CreatTime + "','" + CreatTime + "','" + dr["SaveNewsPath"].ToString() + "','" + dr["FileName"].ToString() + "','." + dr["FileExtName"].ToString() + "'";
                                insertsql += "," + Convert.ToInt32(dr["isPop"].ToString()) + ",0,0,''," + ContentPicTF + ",'" + dr["AdPicAdress"].ToString() + "','" + (dr["AdPicWH"].ToString()).Replace(",", "|") + "',1,0,0,0,'" + CheckStat + "'," + isLock + "," + isRecyle + ",'0','" + Prefix + "news',0,0";
                                insertsql += ",'" + dr["Editor"].ToString() + "',0,0,0,''";
                                insertsql += ")";
                                SqlHelper.ExecuteNonQuery(TagConnstr, CommandType.Text, insertsql, param);
                            }
                            i++;
                        }
                        catch(Exception ex)
                        {
                            m++;
                            Foosun.Common.Public.saveConvertLogFiles("新闻ID：" + dr["NewsID"].ToString(), ex.ToString());
                        }
                        #endregion
                    }
                    Response.Write("√ 成功转换新闻" + i + "条,转换失败" + m + "条.");
                    Response.End();
                    dr.Close();
                    //con.Close();
                    break;
                case "class":
                    sql = "select * from FS_NS_NewsClass order by id asc";
                    if (GisSQL != 1)
                    {
                        OleDbConnection con = new OleDbConnection(SourConnstr);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand(sql, con);
                        dr = cmd.ExecuteReader();
                    }
                    else
                    {
                        dr = SqlHelper.ExecuteReader(SourConnstr, CommandType.Text, sql, null);
                    }
                    while (dr.Read())
                    {
                        string ClassID = dr["ClassID"].ToString();
                        try
                        {
                            if (ClassID.Length > 12)
                            {
                                ClassID = ClassID.Substring(0, 12);
                            }

                            string ParentID = dr["ParentID"].ToString();
                            if (ParentID.Length > 12)
                            {
                                ParentID = ParentID.Substring(0, 12);
                            }
                            int Checkint = 0;
                            DateTime Addtime = DateTime.Now;
                            if (Foosun.Common.Input.IsDate(dr["Addtime"].ToString()))
                            {
                                Addtime = DateTime.Parse(dr["Addtime"].ToString());
                            }

                            if (IsAcc)  //4.0sql转换到.net access
                            {
                                OleDbParameter[] param = new OleDbParameter[2];
                                param[0] = new OleDbParameter("@ClassID", OleDbType.LongVarChar, 12);
                                param[0].Value = ClassID;
                                param[1] = new OleDbParameter("@ParentID", OleDbType.LongVarChar, 12);
                                param[1].Value = ParentID;

                                string insertsql = "insert into " + Prefix + "news_Class(ClassID,ClassCName,ClassEName,ParentID,IsURL,OrderID,URLaddress,[Domain],ClassTemplet,ReadNewsTemplet,SavePath,SaveClassframe,Checkint,ClassSaveRule";
                                insertsql += ",ClassIndexRule,NewsSavePath,NewsFileRule,PicDirPath,ContentPicTF,ContentPICurl,ContentPicSize,InHitoryDay,DataLib,SiteID,NaviShowtf,NaviPIC,NaviContent,MetaKeywords,MetaDescript,isDelPoint,Gpoint";
                                insertsql += ",iPoint,GroupNumber,FileName,isLock,isRecyle,NaviPosition,NewsPosition,isComm,Defineworkey,CreatTime,isPage,PageContent,ModelID,isunHTML";
                                insertsql += ") values (";
                                insertsql += "@ClassID,'" + dr["ClassName"].ToString() + "','" + dr["ClassEName"].ToString() + "',@ParentID," + Convert.ToInt32(dr["IsURL"].ToString()) + "," + Convert.ToInt32(dr["OrderID"].ToString()) + ",'" + dr["UrlAddress"].ToString() + "','" + dr["Domain"].ToString() + "','" + dr["Templet"].ToString() + "','" + dr["NewsTemplet"].ToString() + "','" + dr["SavePath"].ToString() + "','',0,'" + dr["ClassEName"].ToString() + "/index.html'";
                                insertsql += ",'{@year04}-{@month}/{@day}','{@year04}{@month}','{@year04}-{@month}-{@day}-{@hour}-{@minute}',''," + Convert.ToInt32(dr["IsAdPic"].ToString()) + ",'" + dr["AdPicAdress"].ToString() + "','" + (dr["AdPicWH"].ToString()).Replace(",", "|") + "'," + Convert.ToInt32(dr["Oldtime"].ToString()) + ",'" + Prefix + "news','0'," + Convert.ToInt32(dr["isShow"].ToString()) + ",'" + dr["ClassNaviPic"].ToString() + "','" + dr["ClassNaviContent"].ToString() + "','" + dr["ClassKeywords"].ToString() + "','" + dr["Classdescription"].ToString() + "'," + Convert.ToInt32(dr["isPop"].ToString()) + ",0";
                                insertsql += ",0,'','." + dr["FileExtName"].ToString() + "',0," + Convert.ToInt32(dr["ReycleTF"].ToString()) + ",'','',1,'','" + Addtime + "',0,'','',0";
                                insertsql += ")";
                                SqlHelper.ExecuteNonQuery(TagConnstr, CommandType.Text, insertsql, param);
                            }
                            else
                            {
                                SqlParameter[] param = new SqlParameter[2];
                                param[0] = new SqlParameter("@ClassID", SqlDbType.NVarChar, 12);
                                param[0].Value = ClassID;
                                param[1] = new SqlParameter("@ParentID", SqlDbType.NVarChar, 12);
                                param[1].Value = ParentID;

                                string insertsql = "insert into " + Prefix + "news_Class(ClassID,ClassCName,ClassEName,ParentID,IsURL,OrderID,URLaddress,[Domain],ClassTemplet,ReadNewsTemplet,SavePath,SaveClassframe,Checkint,ClassSaveRule";
                                insertsql += ",ClassIndexRule,NewsSavePath,NewsFileRule,PicDirPath,ContentPicTF,ContentPICurl,ContentPicSize,InHitoryDay,DataLib,SiteID,NaviShowtf,NaviPIC,NaviContent,MetaKeywords,MetaDescript,isDelPoint,Gpoint";
                                insertsql += ",iPoint,GroupNumber,FileName,isLock,isRecyle,NaviPosition,NewsPosition,isComm,Defineworkey,CreatTime,isPage,PageContent,ModelID,isunHTML";
                                insertsql += ") values (";
                                insertsql += "@ClassID,'" + dr["ClassName"].ToString() + "','" + dr["ClassEName"].ToString() + "',@ParentID," + Convert.ToInt32(dr["IsURL"].ToString()) + "," + Convert.ToInt32(dr["OrderID"].ToString()) + ",'" + dr["UrlAddress"].ToString() + "','" + dr["Domain"].ToString() + "','" + dr["Templet"].ToString() + "','" + dr["NewsTemplet"].ToString() + "','" + dr["SavePath"].ToString() + "','',0,'" + dr["ClassEName"].ToString() + "/index.html'";
                                insertsql += ",'{@year04}-{@month}/{@day}','{@year04}{@month}','{@year04}-{@month}-{@day}-{@hour}-{@minute}',''," + Convert.ToInt32(dr["IsAdPic"].ToString()) + ",'" + dr["AdPicAdress"].ToString() + "','" + (dr["AdPicWH"].ToString()).Replace(",", "|") + "'," + Convert.ToInt32(dr["Oldtime"].ToString()) + ",'" + Prefix + "news','0'," + Convert.ToInt32(dr["isShow"].ToString()) + ",'" + dr["ClassNaviPic"].ToString() + "','" + dr["ClassNaviContent"].ToString() + "','" + dr["ClassKeywords"].ToString() + "','" + dr["Classdescription"].ToString() + "'," + Convert.ToInt32(dr["isPop"].ToString()) + ",0";
                                insertsql += ",0,'','." + dr["FileExtName"].ToString() + "',0," + Convert.ToInt32(dr["ReycleTF"].ToString()) + ",'','',1,'','" + Addtime + "',0,'','',0";
                                insertsql += ")";
                                SqlHelper.ExecuteNonQuery(TagConnstr, CommandType.Text, insertsql, param);
                            }
                            i++;
                        }
                        catch (Exception ex)
                        {
                            m++;
                            Foosun.Common.Public.saveConvertLogFiles("栏目ID：" + dr["ClassID"].ToString(), ex.ToString());
                        }
                    }
                    dr.Close();
                    Response.Write("√ 成功转换新闻栏目" + i + "条,转换失败" + m + "条.如果存在此栏目，将转换失败");
                    Response.End();
                    break;

                case "special":
                    sql = "select * from FS_NS_Special order by SpecialID asc";
                    if (GisSQL != 1)
                    {
                        OleDbConnection con = new OleDbConnection(SourConnstr);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand(sql, con);
                        dr = cmd.ExecuteReader();
                    }
                    else
                    {
                        dr = SqlHelper.ExecuteReader(SourConnstr, CommandType.Text, sql, null);
                    }
                    while (dr.Read())
                    {
                        string SpecialID = dr["SpecialID"].ToString();
                        try
                        {
                            if (SpecialID.Length > 12)
                            {
                                SpecialID = SpecialID.Substring(0, 12);
                            }
                            string ParentID = dr["ParentID"].ToString();
                            if (ParentID.Length > 12)
                            {
                                ParentID = ParentID.Substring(0, 11);
                            }
                            int Checkint = 0;
                            DateTime Addtime = DateTime.Now;
                            if (Foosun.Common.Input.IsDate(dr["Addtime"].ToString()))
                            {
                                Addtime = DateTime.Parse(dr["Addtime"].ToString());
                            }
                            if (IsAcc)  //4.0sql转换到.net access
                            {
                                OleDbParameter[] param = new OleDbParameter[2];
                                param[0] = new OleDbParameter("@SpecialID", OleDbType.LongVarChar, 12);
                                param[0].Value = SpecialID;
                                param[1] = new OleDbParameter("@ParentID", OleDbType.LongVarChar, 12);
                                param[1].Value = ParentID;
                                string insertsql = "insert into " + Prefix + "news_special(SpecialID,SpecialCName,specialEName,ParentID,[Domain],isDelPoint,Gpoint,[iPoint],GroupNumber,saveDirPath,SavePath,FileName,FileEXName,NaviPicURL";
                                insertsql += ",NaviContent,SiteID,Templet,isLock,isRecyle,CreatTime,NaviPosition,ModelID";
                                insertsql += ") values (";
                                insertsql += "@SpecialID,'" + dr["SpecialCName"].ToString() + "','" + dr["SpecialEName"].ToString() + "',@ParentID,'',0,0,0,'','','" + dr["SavePath"].ToString() + "','" + dr["SpecialEName"].ToString() + "','." + dr["ExtName"].ToString() + "',''";
                                insertsql += ",'" + dr["SpecialContent"].ToString() + "','0','" + dr["Templet"].ToString() + "'," + Convert.ToInt32(dr["isLock"].ToString()) + ",0,'" + Addtime + "','','0'";
                                insertsql += ")";
                                SqlHelper.ExecuteNonQuery(TagConnstr, CommandType.Text, insertsql, param);
                            }
                            else
                            {
                                SqlParameter[] param = new SqlParameter[2];
                                param[0] = new SqlParameter("@SpecialID", SqlDbType.NVarChar, 12);
                                param[0].Value = SpecialID;
                                param[1] = new SqlParameter("@ParentID", SqlDbType.NVarChar, 12);
                                param[1].Value = ParentID;
                                string insertsql = "insert into " + Prefix + "news_special(SpecialID,SpecialCName,specialEName,ParentID,[Domain],isDelPoint,Gpoint,[iPoint],GroupNumber,saveDirPath,SavePath,FileName,FileEXName,NaviPicURL";
                                insertsql += ",NaviContent,SiteID,Templet,isLock,isRecyle,CreatTime,NaviPosition,ModelID";
                                insertsql += ") values (";
                                insertsql += "@SpecialID,'" + dr["SpecialCName"].ToString() + "','" + dr["SpecialEName"].ToString() + "',@ParentID,'',0,0,0,'','','" + dr["SavePath"].ToString() + "','" + dr["SpecialEName"].ToString() + "','." + dr["ExtName"].ToString() + "',''";
                                insertsql += ",'" + dr["SpecialContent"].ToString() + "','0','" + dr["Templet"].ToString() + "'," + Convert.ToInt32(dr["isLock"].ToString()) + ",0,'" + Addtime + "','','0'";
                                insertsql += ")";
                                SqlHelper.ExecuteNonQuery(TagConnstr, CommandType.Text, insertsql, param);
                            }
                            i++;
                        }
                        catch (Exception ex)
                        {
                            m++;
                            Foosun.Common.Public.saveConvertLogFiles("专题ID：" + SpecialID, ex.ToString());
                        }
                    }
                    dr.Close();
                    Response.Write("√ 成功转换新闻专题" + i + "条,转换失败" + m + "条");
                    Response.End();
                    break;
                case "user":
                    sql = "select * from FS_ME_Users order by UserID asc";
                    if (GisSQL != 1)
                    {
                        OleDbConnection con = new OleDbConnection(SourConnstr);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand(sql, con);
                        dr = cmd.ExecuteReader();
                    }
                    else
                    {
                        dr = SqlHelper.ExecuteReader(SourConnstr, CommandType.Text, sql, null);
                    }
                    while (dr.Read())
                    {
                        string UserNumber = dr["UserNumber"].ToString();
                        try
                        {
                            if (UserNumber.Length > 12)
                            {
                                UserNumber = UserNumber.Substring(0, 12);
                            }
                            int Checkint = 0;
                            DateTime BothYear = DateTime.Now;
                            if (Foosun.Common.Input.IsDate(dr["BothYear"].ToString()))
                            {
                                BothYear = DateTime.Parse(dr["BothYear"].ToString());
                            }

                            DateTime RegTime = DateTime.Now;
                            if (Foosun.Common.Input.IsDate(dr["RegTime"].ToString()))
                            {
                                RegTime = DateTime.Parse(dr["RegTime"].ToString());
                            }
                            DateTime LastLoginTime = DateTime.Now;
                            if (Foosun.Common.Input.IsDate(dr["LastLoginTime"].ToString()))
                            {
                                LastLoginTime = DateTime.Parse(dr["LastLoginTime"].ToString());
                            }
                            //if (IsAcc)  //4.0sql转换到.net access
                            //{
                            //    string insertsql = "insert into " + Prefix + "sys_User(UserNum,UserName,UserPassword,NickName,RealName,isAdmin,UserGroupNumber,PassQuestion,PassKey,CertType,CertNumber,Email,mobile,Sex";
                            //    insertsql += ",birthday,Userinfo,UserFace,userFacesize,marriage,iPoint,gPoint,cPoint,ePoint,aPoint,isLock,RegTime,LastLoginTime,OnlineTime,OnlineTF,LoginNumber,FriendClass";
                            //    insertsql += ",LoginLimtNumber,LastIP,SiteID,Addfriend,isOpen,ParmConstrNum,isIDcard,IDcardFiles,Addfriendbs,EmailATF,EmailCode,isMobile,BindTF,MobileCode";
                            //    insertsql += ") values (";
                            //    insertsql += "'" + UserNumber + "','" + dr["UserName"].ToString() + "','" + dr["UserPassword"].ToString() + "','" + dr["NickName"].ToString() + "','" + dr["RealName"].ToString() + "',0,'00000000001','" + dr["PassQuestion"].ToString() + "','" + dr["PassAnswer"].ToString() + "',0,'','" + dr["Email"].ToString() + "','" + dr["Mobile"].ToString() + "'," + Convert.ToInt32(dr["Sex"].ToString()) + "";
                            //    insertsql += ",'" + BothYear + "','','" + dr["HeadPic"].ToString() + "','" + dr["HeadPicSize"].ToString().Replace(",", "|") + "'," + Convert.ToInt32(dr["IsMarray"].ToString()) + "," + Convert.ToInt32(dr["Integral"].ToString()) + "," + Convert.ToDouble(dr["FS_Money"].ToString()) + ",0,0,0," + Convert.ToInt32(dr["isLock"].ToString()) + ",'" + RegTime + "','" + LastLoginTime + "',0,0," + Convert.ToInt32(dr["LoginNum"].ToString()) + ",''";
                            //    insertsql += ",0,'" + dr["LastLoginIP"].ToString() + "','0',0," + Convert.ToInt32(dr["isOpen"].ToString()) + ",0,0,'','',0,'',0,0,''";
                            //    insertsql += ")";
                            //    SqlHelper.ExecuteNonQuery(TagConnstr, CommandType.Text, insertsql, null);
                            //}
                            //else
                            //{

                            //}                                 
                            string insertsql = "insert into " + Prefix + "sys_User(UserNum,UserName,UserPassword,NickName,RealName,isAdmin,UserGroupNumber,PassQuestion,PassKey,CertType,CertNumber,Email,mobile,Sex";
                            insertsql += ",birthday,Userinfo,UserFace,userFacesize,marriage,iPoint,gPoint,cPoint,ePoint,aPoint,isLock,RegTime,LastLoginTime,OnlineTime,OnlineTF,LoginNumber,FriendClass";
                            insertsql += ",LoginLimtNumber,LastIP,SiteID,Addfriend,isOpen,ParmConstrNum,isIDcard,IDcardFiles,Addfriendbs,EmailATF,EmailCode,isMobile,BindTF,MobileCode";
                            insertsql += ") values (";
                            insertsql += "'" + UserNumber + "','" + dr["UserName"].ToString() + "','" + dr["UserPassword"].ToString() + "','" + dr["NickName"].ToString() + "','" + dr["RealName"].ToString() + "',0,'00000000001','" + dr["PassQuestion"].ToString() + "','" + dr["PassAnswer"].ToString() + "',0,'','" + dr["Email"].ToString() + "','" + dr["Mobile"].ToString() + "'," + Convert.ToInt32(dr["Sex"].ToString()) + "";
                            insertsql += ",'" + BothYear + "','','" + dr["HeadPic"].ToString() + "','" + dr["HeadPicSize"].ToString().Replace(",", "|") + "'," + Convert.ToInt32(dr["IsMarray"].ToString()) + "," + Convert.ToInt32(dr["Integral"].ToString()) + "," + Convert.ToDouble(dr["FS_Money"].ToString()) + ",0,0,0," + Convert.ToInt32(dr["isLock"].ToString()) + ",'" + RegTime + "','" + LastLoginTime + "',0,0," + Convert.ToInt32(dr["LoginNum"].ToString()) + ",''";
                            insertsql += ",0,'" + dr["LastLoginIP"].ToString() + "','0',0," + Convert.ToInt32(dr["isOpen"].ToString()) + ",0,0,'','',0,'',0,0,''";
                            insertsql += ")";
                            SqlHelper.ExecuteNonQuery(TagConnstr, CommandType.Text, insertsql, null);
                            i++;
                        }
                        catch (Exception ex)
                        {
                            m++;
                            Foosun.Common.Public.saveConvertLogFiles("用户ID：" + UserNumber, ex.ToString());
                        }
                    }
                    dr.Close();
                    Response.Write("√ 成功转换会员" + i + "条,转换失败" + m + "条");
                    Response.End();
                    break;
                case "gen":
                    //1关键，2来源，3作者，4内部
                    sql = "select * from FS_NS_General order by GID asc";
                    if (GisSQL != 1)
                    {
                        OleDbConnection con = new OleDbConnection(SourConnstr);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand(sql, con);
                        dr = cmd.ExecuteReader();
                    }
                    else
                    {
                        dr = SqlHelper.ExecuteReader(SourConnstr, CommandType.Text, sql, null);
                    }
                    while (dr.Read())
                    {
                        string ID = dr["GID"].ToString();
                        try
                        {
                            //0表示关键字，1表示作者，2表示来源，3表示内部连接
                            int G_Type = 0;
                            switch (dr["G_Type"].ToString())
                            {
                                case "1":
                                    G_Type = 0;
                                    break;
                                case "2":
                                    G_Type = 2;
                                    break;
                                case "3":
                                    G_Type = 1;
                                    break;
                                case "4":
                                    G_Type = 3;
                                    break;
                            }
                            string insertsql = "insert into " + Prefix + "news_Gen(Cname,gType,URL,EmailURL,isLock,SiteID";
                            insertsql += ") values (";
                            insertsql += "'" + dr["G_Name"].ToString() + "'," + G_Type + ",'" + dr["G_URL"].ToString() + "','" + dr["G_Email"].ToString() + "'," + Convert.ToInt32(dr["isLock"].ToString()) + ",'0'";
                            insertsql += ")";
                            i++;
                            SqlHelper.ExecuteNonQuery(TagConnstr, CommandType.Text, insertsql, null);
                        }
                        catch (Exception ex)
                        {
                            m++;
                            Foosun.Common.Public.saveConvertLogFiles("常规ID：" + ID, ex.ToString());
                        }
                    }
                    dr.Close();
                    Response.Write("√ 成功转换常规管理" + i + "条,转换失败" + m + "条");
                    Response.End();
                    break;

            }
        }

        public abstract class SqlHelper
        {


            //数据库表通用前缀
            // Hashtable to store cached parameters
            private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

            /// <summary>
            /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
            /// using the provided parameters.
            /// </summary>
            /// <remarks>
            /// e.g.:  
            ///  int result = ExecuteNonQuery( CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
            /// </remarks>
            /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
            /// <param name="commandText">the stored procedure name or T-SQL command</param>
            /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
            /// <returns>an int representing the number of rows affected by the command</returns>
            public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
            {
                return ExecuteNonQuery(SourConnstr, cmdType, cmdText, commandParameters);
            }

            /// <summary>
            /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
            /// using the provided parameters.
            /// </summary>
            /// <remarks>
            /// e.g.:  
            ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
            /// </remarks>
            /// <param name="connectionString">a valid connection string for a SqlConnection</param>
            /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
            /// <param name="commandText">the stored procedure name or T-SQL command</param>
            /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
            /// <returns>an int representing the number of rows affected by the command</returns> 
            public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
            {

                SqlCommand cmd = new SqlCommand();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
            }

            /// <summary>
            /// access
            /// </summary>
            /// <param name="connectionString"></param>
            /// <param name="cmdType"></param>
            /// <param name="cmdText"></param>
            /// <param name="commandParameters"></param>
            /// <returns></returns>
            public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
            {

                //SqlCommand cmd = new SqlCommand();

                //using (SqlConnection conn = new SqlConnection(connectionString))
                //{
                //    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //    int val = cmd.ExecuteNonQuery();
                //    cmd.Parameters.Clear();
                //    return val;
                //}

                DbCommand cmd = Provider.CreateCommand();
                using (DbConnection connection = Provider.CreateConnection())
                {
                    try
                    {
                        connection.ConnectionString = connectionString;
                        PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                        int val = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        //DbHelper.Conn = connection;
                        return val;
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Dispose();
                    }
                }
            }

            /// <summary>
            /// Execute a SqlCommand (that returns no resultset) against an existing database connection 
            /// using the provided parameters.
            /// </summary>
            /// <remarks>
            /// e.g.:  
            ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
            /// </remarks>
            /// <param name="conn">an existing database connection</param>
            /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
            /// <param name="commandText">the stored procedure name or T-SQL command</param>
            /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
            /// <returns>an int representing the number of rows affected by the command</returns>
            public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }

            /// <summary>
            /// Execute a SqlCommand (that returns no resultset) using an existing SQL Transaction 
            /// using the provided parameters.
            /// </summary>
            /// <remarks>
            /// e.g.:  
            ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
            /// </remarks>
            /// <param name="trans">an existing sql transaction</param>
            /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
            /// <param name="commandText">the stored procedure name or T-SQL command</param>
            /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
            /// <returns>an int representing the number of rows affected by the command</returns>
            public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }

            /// <summary>
            /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
            /// using the provided parameters.
            /// </summary>
            /// <remarks>
            /// e.g.:  
            ///  SqlDataReader r = ExecuteReader(CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
            /// </remarks>
            /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
            /// <param name="commandText">the stored procedure name or T-SQL command</param>
            /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
            /// <returns>A SqlDataReader containing the results</returns>
            public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
            {
                return ExecuteReader(SourConnstr, cmdType, cmdText, commandParameters);
            }

            /// <summary>
            /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
            /// using the provided parameters.
            /// </summary>
            /// <remarks>
            /// e.g.:  
            ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
            /// </remarks>
            /// <param name="connectionString">a valid connection string for a SqlConnection</param>
            /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
            /// <param name="commandText">the stored procedure name or T-SQL command</param>
            /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
            /// <returns>A SqlDataReader containing the results</returns>
            public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
            {

                SqlCommand cmd = new SqlCommand();
                SqlConnection conn = new SqlConnection(connectionString);

                // we use a try/catch here because if the method throws an exception we want to 
                // close the connection throw code, because no datareader will exist, hence the 
                // commandBehaviour.CloseConnection will not work
                try
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    cmd.Parameters.Clear();
                    return rdr;
                }
                catch
                {
                    conn.Close();
                    throw;
                }
            }

            /// <summary>
            /// 执行对默认数据库有自定义排序的分页的查询
            /// </summary>
            /// <param name="connectionString">连接字符串
            /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
            /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
            /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
            /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
            /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
            /// <param name="PageIndex">当前页的页码</param>
            /// <param name="PageSize">每页记录数</param>
            /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
            /// <param name="PageCount">输出参数，返回查询的总页数</param>
            /// <returns>返回查询结果</returns>
            public static SqlDataReader ExecuteReaderPage(string connectionString, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string GroupClause, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SqlParameter[] commandParameters)
            {
                SqlCommand cmd = new SqlCommand();
                SqlConnection conn = new SqlConnection(connectionString);
                try
                {
                    conn.Open();

                    RecordCount = 0;
                    PageCount = 0;
                    if (PageSize <= 0)
                    {
                        PageSize = 10;
                    }
                    string SqlCount = "select count(*) from " + SqlTablesAndWhere;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SqlCount;
                    if (commandParameters != null)
                    {
                        foreach (SqlParameter parm in commandParameters)
                            cmd.Parameters.Add(parm);
                    }
                    RecordCount = (int)cmd.ExecuteScalar();
                    if (RecordCount % PageSize == 0)
                    {
                        PageCount = RecordCount / PageSize;
                    }
                    else
                    {
                        PageCount = RecordCount / PageSize + 1;
                    }
                    if (PageIndex > PageCount)
                        PageIndex = PageCount;
                    if (PageIndex < 1)
                        PageIndex = 1;
                    string Sql = null;
                    if (PageIndex == 1)
                    {
                        Sql = "select top " + PageSize + " " + SqlAllFields + " from " + SqlTablesAndWhere + " " + GroupClause + " " + OrderFields;
                    }
                    else
                    {
                        Sql = "select top " + PageSize + " " + SqlAllFields + " from ";
                        if (SqlTablesAndWhere.ToLower().IndexOf(" where ") > 0)
                        {
                            string _where = Regex.Replace(SqlTablesAndWhere, @"\ where\ ", " where (", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                            Sql += _where + ") and (";
                        }
                        else
                        {
                            Sql += SqlTablesAndWhere + " where (";
                        }
                        Sql += IndexField + " not in (select top " + (PageIndex - 1) * PageSize + " " + IndexField + " from " + SqlTablesAndWhere + " " + OrderFields;
                        Sql += ")) " + GroupClause + " " + OrderFields;
                    }
                    cmd.CommandText = Sql;
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    cmd.Parameters.Clear();
                    return rdr;
                }
                catch
                {
                    conn.Close();
                    throw;
                }
            }

            /// <summary>
            /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
            /// using the provided parameters.
            /// </summary>
            /// <remarks>
            /// e.g.:  
            ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
            /// </remarks>
            /// <param name="connectionString">a valid connection string for a SqlConnection</param>
            /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
            /// <param name="commandText">the stored procedure name or T-SQL command</param>
            /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
            /// <returns>A SqlDataReader containing the results</returns>
            public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
            {
                SqlCommand cmd = new SqlCommand();
                // we use a try/catch here because if the method throws an exception we want to 
                // close the connection throw code, because no datareader will exist, hence the 
                // commandBehaviour.CloseConnection will not work
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return rdr;
            }

            /// <summary>
            /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
            /// using the provided parameters.
            /// </summary>
            /// <remarks>
            /// e.g.:  
            ///  Object obj = ExecuteScalar(CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
            /// </remarks>
            /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
            /// <param name="commandText">the stored procedure name or T-SQL command</param>
            /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
            /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
            public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
            {
                return ExecuteScalar(SourConnstr, cmdType, cmdText, commandParameters);
            }

            /// <summary>
            /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
            /// using the provided parameters.
            /// </summary>
            /// <remarks>
            /// e.g.:  
            ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
            /// </remarks>
            /// <param name="connectionString">a valid connection string for a SqlConnection</param>
            /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
            /// <param name="commandText">the stored procedure name or T-SQL command</param>
            /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
            /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
            public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
            {
                SqlCommand cmd = new SqlCommand();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return val;
                }
            }

            /// <summary>
            /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
            /// using the provided parameters.
            /// </summary>
            /// <remarks>
            /// e.g.:  
            ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
            /// </remarks>
            /// <param name="conn">an existing database connection</param>
            /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
            /// <param name="commandText">the stored procedure name or T-SQL command</param>
            /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
            /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
            public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
            {

                SqlCommand cmd = new SqlCommand();

                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }

            /// <summary>
            /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
            /// using the provided parameters.
            /// </summary>
            /// <remarks>
            /// e.g.:  
            ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
            /// </remarks>
            /// <param name="trans">an existing sql transaction</param>
            /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
            /// <param name="commandText">the stored procedure name or T-SQL command</param>
            /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
            /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
            public static object ExecuteScalar(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
            {
                SqlCommand cmd = new SqlCommand();

                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }

            /// <summary>
            /// Execute a SqlCommand that returns the DataTable object against the database specified in the connection string 
            /// using the provided parameters.
            /// </summary>
            /// <remarks>
            /// e.g.:  
            ///  DataTable tb = ExecuteTable( CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
            /// </remarks>
            /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
            /// <param name="commandText">the stored procedure name or T-SQL command</param>
            /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
            /// <returns>A DataTable containing the results</returns>
            public static DataTable ExecuteTable(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
            {
                return ExecuteTable(SourConnstr, cmdType, cmdText, commandParameters);
            }

            /// <summary>
            /// Execute a SqlCommand that returns the DataTable object against the database specified in the connection string 
            /// using the provided parameters.
            /// </summary>
            /// <remarks>
            /// e.g.:  
            ///  DataTable tb = ExecuteTable(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
            /// </remarks>
            /// <param name="connectionString">a valid connection string for a SqlConnection</param>
            /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
            /// <param name="commandText">the stored procedure name or T-SQL command</param>
            /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
            /// <returns>A DataTable containing the results</returns>
            public static DataTable ExecuteTable(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
            {
                SqlCommand cmd = new SqlCommand();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                    SqlDataAdapter ap = new SqlDataAdapter();
                    ap.SelectCommand = cmd;
                    DataSet st = new DataSet();
                    ap.Fill(st, "Result");
                    cmd.Parameters.Clear();
                    return st.Tables["Result"];
                }
            }

            /// <summary>
            /// Execute a SqlCommand that returns the DataTable object against an existing database connection 
            /// using the provided parameters.
            /// </summary>
            /// <remarks>
            /// e.g.:  
            ///  DataTable tb = ExecuteTable(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
            /// </remarks>
            /// <param name="conn">an existing database connection</param>
            /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
            /// <param name="commandText">the stored procedure name or T-SQL command</param>
            /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
            /// <returns>A DataTable containing the results </returns>
            public static DataTable ExecuteTable(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
            {

                SqlCommand cmd = new SqlCommand();

                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter ap = new SqlDataAdapter();
                ap.SelectCommand = cmd;
                DataSet st = new DataSet();
                ap.Fill(st, "Result");
                cmd.Parameters.Clear();
                return st.Tables["Result"];
            }

            /// <summary>
            /// 执行对默认数据库有自定义排序的分页的查询
            /// </summary>
            /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
            /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
            /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
            /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
            /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
            /// <param name="PageIndex">当前页的页码</param>
            /// <param name="PageSize">每页记录数</param>
            /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
            /// <param name="PageCount">输出参数，返回查询的总页数</param>
            /// <returns>返回查询结果</returns>
            public static DataTable ExecutePage(string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SqlParameter[] commandParameters)
            {
                return ExecutePage(SourConnstr, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out  RecordCount, out  PageCount, commandParameters);
            }

            /// <summary>
            /// 执行有自定义排序的分页的查询
            /// </summary>
            /// <param name="connectionString">SQL数据库连接字符串</param>
            /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
            /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
            /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
            /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
            /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
            /// <param name="PageIndex">当前页的页码</param>
            /// <param name="PageSize">每页记录数</param>
            /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
            /// <param name="PageCount">输出参数，返回查询的总页数</param>
            /// <returns>返回查询结果</returns>
            public static DataTable ExecutePage(string connectionString, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SqlParameter[] commandParameters)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return ExecutePage(connection, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, commandParameters);
                }
            }

            /// <summary>
            /// 执行有自定义排序的分页的查询
            /// </summary>
            /// <param name="connection">SQL数据库连接对象</param>
            /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
            /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
            /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
            /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
            /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
            /// <param name="PageIndex">当前页的页码</param>
            /// <param name="PageSize">每页记录数</param>
            /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
            /// <param name="PageCount">输出参数，返回查询的总页数</param>
            /// <returns>返回查询结果</returns>
            public static DataTable ExecutePage(SqlConnection connection, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SqlParameter[] commandParameters)
            {
                RecordCount = 0;
                PageCount = 0;
                if (PageSize <= 0)
                {
                    PageSize = 10;
                }
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                string SqlCount = "select count(*) from " + SqlTablesAndWhere;
                SqlCommand cmd = new SqlCommand(SqlCount, connection);
                if (commandParameters != null)
                {
                    foreach (SqlParameter parm in commandParameters)
                        cmd.Parameters.Add(parm);
                }
                RecordCount = (int)cmd.ExecuteScalar();
                if (RecordCount % PageSize == 0)
                {
                    PageCount = RecordCount / PageSize;
                }
                else
                {
                    PageCount = RecordCount / PageSize + 1;
                }
                if (PageIndex > PageCount)
                    PageIndex = PageCount;
                if (PageIndex < 1)
                    PageIndex = 1;
                string Sql = null;
                if (PageIndex == 1)
                {
                    Sql = "select top " + PageSize + " " + SqlAllFields + " from " + SqlTablesAndWhere + " " + OrderFields;
                }
                else
                {
                    Sql = "select top " + PageSize + " " + SqlAllFields + " from ";
                    if (SqlTablesAndWhere.ToLower().IndexOf(" where ") > 0)
                    {
                        string _where = Regex.Replace(SqlTablesAndWhere, @"\ where\ ", " where (", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                        Sql += _where + ") and (";
                    }
                    else
                    {
                        Sql += SqlTablesAndWhere + " where (";
                    }
                    Sql += IndexField + " not in (select top " + (PageIndex - 1) * PageSize + " " + IndexField + " from " + SqlTablesAndWhere + " " + OrderFields;
                    Sql += ")) " + OrderFields;
                }
                cmd.CommandText = Sql;
                SqlDataAdapter ap = new SqlDataAdapter();
                ap.SelectCommand = cmd;
                DataSet st = new DataSet();
                ap.Fill(st, "PageResult");
                cmd.Parameters.Clear();
                return st.Tables["PageResult"];
            }

            /// <summary>
            /// add parameter array to the cache
            /// </summary>
            /// <param name="cacheKey">Key to the parameter cache</param>
            /// <param name="cmdParms">an array of SqlParamters to be cached</param>
            public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
            {
                parmCache[cacheKey] = commandParameters;
            }

            /// <summary>
            /// Retrieve cached parameters
            /// </summary>
            /// <param name="cacheKey">key used to lookup parameters</param>
            /// <returns>Cached SqlParamters array</returns>
            public static SqlParameter[] GetCachedParameters(string cacheKey)
            {
                SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

                if (cachedParms == null)
                    return null;

                SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

                for (int i = 0, j = cachedParms.Length; i < j; i++)
                    clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

                return clonedParms;
            }

            /// <summary>
            /// Prepare a command for execution
            /// </summary>
            /// <param name="cmd">SqlCommand object</param>
            /// <param name="conn">SqlConnection object</param>
            /// <param name="trans">SqlTransaction object</param>
            /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
            /// <param name="cmdText">Command text, e.g. Select * from Products</param>
            /// <param name="cmdParms">SqlParameters to use in the command</param>
            private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
            {

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                cmd.Connection = conn;
                cmd.CommandText = cmdText;

                if (trans != null)
                    cmd.Transaction = trans;

                cmd.CommandType = cmdType;

                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                        cmd.Parameters.Add(parm);
                }
            }

            /// <summary>
            /// access
            /// </summary>
            /// <param name="cmd"></param>
            /// <param name="conn"></param>
            /// <param name="trans"></param>
            /// <param name="cmdType"></param>
            /// <param name="cmdText"></param>
            /// <param name="cmdParms"></param>
            private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
            {

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                cmd.Connection = conn;
                cmd.CommandText = cmdText;

                if (trans != null)
                    cmd.Transaction = trans;

                cmd.CommandType = cmdType;
                //cmd.CommandTimeout = Timeout;
                if (cmdParms != null)
                {
                    foreach (DbParameter parm in cmdParms)
                        if (parm != null)
                            cmd.Parameters.Add(parm);
                }
            }

        }
    }
}
