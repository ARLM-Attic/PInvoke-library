//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By Xi.Deng                         ==
//===========================================================
using System;
using System.Collections.Generic;
using System.Data;
using Foosun.DALFactory;

namespace Foosun.CMS
{
    public class Recyle
    {
        private IRecyle rc;
        private string str_dirDumm = Foosun.Config.UIConfig.dirDumm;
        private string str_dirSite = Foosun.Config.UIConfig.dirSite;
        private string str_rootpath = Foosun.Common.ServerInfo.GetRootPath();
        public Recyle()
        {
            rc = DataAccess.CreateRecyle();
            if (str_dirDumm != "" && str_dirDumm != null && str_dirDumm != string.Empty)
                str_dirDumm = "\\" + str_dirDumm;
        }
        public DataTable getList(string type)
        {
            DataTable dt = rc.getList(type);
            return dt;
        }
        public void RallNCList()
        {
            rc.RallNCList();
        }
        public void RallNList(string classid)
        {
            rc.RallNList(classid);
        }
        public void RallCList()
        {
            rc.RallCList();
        }
        public void RallSList()
        {
            rc.RallSList();
        }
        public void RallLCList()
        {
            rc.RallLCList();
        }
        public void RallLList(string classid)
        {
            rc.RallLList(classid);
        }
        public void RallStCList()
        {
            rc.RallStCList();
        }
        public void RallStList(string classid)
        {
            rc.RallStList(classid);
        }
        public void RallPSFList()
        {
            rc.RallPSFList();
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------
        public void DallNCList()
        {
            DataTable dt = rc.getNewsTable();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string tbname = dt.Rows[i][0].ToString();
                    DataTable dv = rc.getNewsClass(null);
                    if (dv != null)
                    {
                        for (int j = 0; j < dv.Rows.Count; j++)
                        {
                            string classid = dv.Rows[j]["ClassID"].ToString();
                            DataTable dc = rc.getNews(classid, tbname);
                            if (dc != null)
                            {
                                for (int k = 0; k < dc.Rows.Count; k++)
                                {
                                    string newsid = dc.Rows[k]["NewsID"].ToString();
                                    string savepath = dc.Rows[k]["SavePath"].ToString();
                                    string filename = dc.Rows[k]["FileName"].ToString();
                                    string fileexname = dc.Rows[k]["FileEXName"].ToString();

                                    string filepath = str_rootpath + str_dirDumm + savepath + "\\" + filename + "." + fileexname;

                                    Foosun.Common.Public.DelFile("", filepath);
                                    rc.raDComment(newsid, true);
                                }
                                dc.Clear(); dc.Dispose();

                                string dirpath = str_rootpath + str_dirDumm + dv.Rows[j]["SavePath"].ToString() + "\\" + dv.Rows[j]["SaveClassframe"].ToString();
                                Foosun.Common.Public.DelFile(dirpath, "");
                            }
                        }
                        dv.Clear(); dv.Dispose();  
                    }
                }
                dt.Clear(); dt.Dispose();
            }
            rc.DallNCList();
        }
        public void DallNList()
        {
            DataTable dt = rc.getNewsTable();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string tbname = dt.Rows[i][0].ToString();
                    DataTable dc = rc.getNews("",tbname);
                    if (dc != null)
                    {
                        for (int k = 0; k < dc.Rows.Count; k++)
                        {
                            string newsid = dc.Rows[k]["NewsID"].ToString();
                            string savepath = dc.Rows[k]["SavePath"].ToString();
                            string filename = dc.Rows[k]["FileName"].ToString();
                            string fileexname = dc.Rows[k]["FileEXName"].ToString();

                            string filepath = str_rootpath + str_dirDumm + savepath + "\\" + filename + "." + fileexname;

                            Foosun.Common.Public.DelFile("", filepath);
                            rc.raDComment(newsid, true);
                        }
                        dc.Clear(); dc.Dispose();
                    }
                }
            }
            rc.DallNList();
        }
        public void DallCList()
        {
            DataTable dt = rc.getSite(null);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string siteid = dt.Rows[i]["ChannelID"].ToString();
                    string siteename = dt.Rows[i]["EName"].ToString();
                    string sitepath = str_rootpath + str_dirDumm + "\\" + str_dirSite + "\\" + siteename;
                    Foosun.Common.Public.DelFile(sitepath, "");
                }
                dt.Clear();dt.Dispose();
            }
            rc.DallCList();
        }
        public void DallSList()
        {
            DataTable dt = rc.getSpeaciList(null);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string savepath = dt.Rows[i]["SavePath"].ToString();
                    string spename = dt.Rows[i]["specialEName"].ToString();
                    string savedirpath = dt.Rows[i]["saveDirPath"].ToString();
                    string filename = dt.Rows[i]["FileName"].ToString();
                    string fileexname = dt.Rows[i]["FileEXName"].ToString();

                    string FilePath = str_rootpath + str_dirDumm + savepath + "\\" + spename + "\\" + savedirpath + "\\" + filename + "." + fileexname;
                    string DirPath = str_rootpath + str_dirDumm + savepath + "\\" + spename + "\\" + savedirpath;
                    Foosun.Common.Public.DelFile(DirPath, FilePath);
                }
                dt.Clear();dt.Dispose();
            }
            rc.DallSList();
        }
        public void DallLCList()
        {
            rc.DallLCList();
        }
        public void DallLList()
        {
            rc.DallLList();
        }
        public void DallStCList()
        {
            rc.DallStCList();
        }
        public void DallStList()
        {
            rc.DallStList();
        }
        public void DallPSFList()
        {
            rc.DallPSFList();
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------
        public void PRNCList(string idstr)
        {
            rc.PRNCList(idstr);
        }
        public void PRNList(string classid, string idstr)
        {
            rc.PRNList(classid,idstr);
        }
        public void PRCList(string idstr)
        {
            rc.PRCList(idstr);
        }
        public void PRSList(string idstr)
        {
            rc.PRSList(idstr);
        }
        public void PRStCList(string idstr)
        {
            rc.PRStCList(idstr);
        }
        public void PRStList(string classid, string idstr)
        {
            rc.PRStList(classid,idstr);
        }
        public void PRLCList(string idstr)
        {
            rc.PRLCList(idstr);
        }
        public void PRLList(string classid, string idstr)
        {
            rc.PRLList(classid,idstr);
        }
        public void PRPSFList(string idstr)
        {
            rc.PRPSFList(idstr);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------
        public void PDNCList(string idstr)
        {
            DataTable dc = rc.getNewsClass(idstr);
            if (dc != null)
            {
                for (int i = 0; i < dc.Rows.Count; i++)
                {
                    string classid = dc.Rows[i]["ClassID"].ToString();
                    DataTable dt = rc.getNewsTable();
                    if (dt != null)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            string tbname = dt.Rows[j][0].ToString();
                            DataTable dv = rc.getNews(classid, tbname);
                            if (dv != null)
                            {
                                for (int k = 0; k < dv.Rows.Count; k++)
                                {
                                    string newsid = dv.Rows[k]["NewsID"].ToString();
                                    string filepath = str_rootpath + str_dirDumm + dv.Rows[k]["SavePath"].ToString() + "\\" + dv.Rows[k]["FileName"].ToString() + "." + dv.Rows[k]["FileEXName"].ToString();

                                    Foosun.Common.Public.DelFile("", filepath);
                                    rc.raDComment(newsid, true);
                                }
                                dv.Clear(); dv.Dispose();
                            }
                        }
                        dt.Clear(); dt.Dispose();
                    }
                    string dirPath = str_rootpath + str_dirDumm + dc.Rows[0]["SavePath"].ToString() + "\\" + dc.Rows[0]["SaveClassframe"].ToString();
                    Foosun.Common.Public.DelFile(dirPath, "");
                }
                dc.Clear(); dc.Dispose();
            }
            rc.PDNCList(idstr);
        }
        public void PDNList(string idstr)
        {
            DataTable dt = rc.getNewsTable();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string tbname = dt.Rows[i][0].ToString();
                    DataTable dv = rc.getNews(null, tbname);
                    if (dv != null)
                    {
                        for (int k = 0; k < dv.Rows.Count; k++)
                        {
                            string newsid = dv.Rows[k]["NewsID"].ToString();
                            string savepath = dv.Rows[k]["SavePath"].ToString();
                            string filename = dv.Rows[k]["FileName"].ToString();
                            string fileexname = dv.Rows[k]["FileEXName"].ToString();

                            string filepath = str_rootpath + str_dirDumm + savepath + "\\" + filename + "." + fileexname;

                            Foosun.Common.Public.DelFile("", filepath);
                            rc.raDComment(newsid, true);
                        }
                        dv.Clear(); dv.Dispose();
                    }
                }
                dt.Clear(); dt.Dispose();
            }
            rc.PDNList(idstr);
        }
        public void PDCList(string idstr)
        {
            DataTable dt = rc.getSite(idstr);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string siteid = dt.Rows[i]["ChannelID"].ToString();
                    string siteename = dt.Rows[i]["EName"].ToString();
                    string sitepath = str_rootpath + str_dirDumm + "\\" + str_dirSite + "\\" + siteename;
                    Foosun.Common.Public.DelFile(sitepath, "");
                }
                dt.Clear(); dt.Dispose();
            }
            rc.PDCList(idstr);
        }
        public void PDSList(string idstr)
        {
            DataTable dt = rc.getSpeaciList(idstr);
            if (dt != null)
            { 
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string savepath = dt.Rows[i]["SavePath"].ToString();
                    string specialEName = dt.Rows[i]["specialEName"].ToString();
                    string saveDirPath = dt.Rows[i]["saveDirPath"].ToString();
                    string filename = dt.Rows[i]["FileName"].ToString();
                    string fileexname = dt.Rows[i]["FileEXName"].ToString();

                    string FilePath = str_rootpath + str_dirDumm + savepath + "\\" + specialEName + "\\" + saveDirPath + "\\" + filename + "." + fileexname;
                    string DirPath = str_rootpath + str_dirDumm + savepath + "\\" + specialEName + "\\" + saveDirPath;
                    Foosun.Common.Public.DelFile(DirPath, FilePath);
                }
                dt.Clear();
                dt.Dispose();
            }
            rc.PDSList(idstr);
        }
        public void PDStCList(string idstr)
        {
            rc.PDStCList(idstr);
        }
        public void PDStList(string idstr)
        {
            rc.PDStList(idstr);
        }
        public void PDLCList(string idstr)
        {
            rc.PDLCList(idstr);
        }
        public void PDLList(string idstr)
        {
            rc.PDLList(idstr);
        }
        public void PDPSFList(string idstr)
        {
            rc.PDPSFList(idstr);
        }
    }
}
