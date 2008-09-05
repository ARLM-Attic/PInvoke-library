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
using Foosun.Model;
using Foosun.DALFactory;

namespace Foosun.CMS
{
    public class Special
    {
        private ISpecial sc;
        private string str_dirDumm = Foosun.Config.UIConfig.dirDumm;
        private string str_rootpath = Foosun.Common.ServerInfo.GetRootPath();

        public Special()
        {
            sc = DataAccess.CreateSpecial();
            if (str_dirDumm != "" && str_dirDumm != null && str_dirDumm != string.Empty)
                str_dirDumm = "\\" + str_dirDumm;
        }

        public DataTable getChildList(string classid)
        {
            DataTable dt = sc.getChildList(classid);
            return dt;
        }

        public void Lock(string id)
        {
            sc.Lock(id);
        }

        public void UnLock(string id)
        {
            sc.UnLock(id);
        }

        public void PDel(string id)
        {
            sc.PDel(id);
        }

        public void PDels(string id)
        {
            DataTable dt = sc.getSpeacilInfo(id);
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
                dt.Clear();
                dt.Dispose();
            }
            sc.PDels(id);
        }

        public void PLock(string id)
        {
            sc.PLock(id);
        }

        public void PUnLock(string id)
        {
            sc.PUnLock(id);
        }

        public void RemoveNews(string specialID, string newsID)
        {
            sc.RemoveNews(specialID, newsID);
        } 

        public string getSpicaelNewsNum(string id)
        {
            string newsNum = sc.getSpicaelNewsNum(id);
            return newsNum;
        }

        public string Add(Foosun.Model.Special sci)
        {
            return sc.Add(sci);
        }

        public int Edit(Foosun.Model.Special sci)
        {
            DataTable dt = sc.getSpeacilInfo(sci.SpecialID);
            if (dt != null)
            {
                string savepath = dt.Rows[0]["SavePath"].ToString();
                string spename = dt.Rows[0]["specialEName"].ToString();
                string savedirpath = dt.Rows[0]["saveDirPath"].ToString();
                string filename = dt.Rows[0]["FileName"].ToString();
                string fileexname = dt.Rows[0]["FileEXName"].ToString();

                string FilePath = str_rootpath + str_dirDumm + savepath + "\\" + spename + "\\" + savedirpath + "\\" + filename + "." + fileexname;
                string DirPath = str_rootpath + str_dirDumm + savepath + "\\" + spename + "\\" + savedirpath;
                Foosun.Common.Public.DelFile(DirPath, FilePath);
                dt.Clear();
                dt.Dispose();
            }

            int resultl = sc.Edit(sci);
            return resultl;
        }

        public DataTable getSpeacilInfo(string id)
        {
            DataTable dt = sc.getSpeacilInfo(id);
            return dt;
        }

        public IDataReader ToTempletBind(string ParentID)
        {
            return sc.ToTempletBind(ParentID);
        }

        public void BindSPTemplet(string SpecialID, string Templet)
        {
            sc.BindSPTemplet(SpecialID, Templet);
        }
    }
}
