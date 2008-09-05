//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By Y.xiaoBin                       ==
//===========================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Foosun.CMS.Common
{
   public class UpLoad
    {
        private System.Web.HttpPostedFile postedFile = null;
        private string savePath = "";
        private string extension = "";
        private int fileLength = 0;

        /// <summary>
        /// 显示该组件使用的参数信息
        /// </summary>
        public System.Web.HttpPostedFile PostedFile
        {
            get
            {
                return postedFile;
            }
            set
            {
                postedFile = value;
            }
        }
        public string SavePath
        {
            get
            {
                if (savePath != "") return savePath;
                return "c:\\";
            }
            set
            {
                savePath = value;
            }
        }
        public int FileLength
        {
            get
            {
                if (fileLength != 0) return fileLength;
                return 1024;
            }
            set
            {
                fileLength = value * 1024;
            }
        }

        public string Extension
        {
            get
            {
                if (extension != "")
                    return extension;
                return "txt";
            }
            set
            {
                extension = value;
            }
        }
        public string PathToName(string path)
        {
            int pos = path.LastIndexOf(@"\");
            return path.Substring(pos + 1);
        }

       public string Upload(int _num, int isAdmin)
       {
           return Upload(_num, isAdmin, "");
       }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
       public string Upload(int _num,int isAdmin,string fileType)
        {
            if (PostedFile != null)
            {
                int getFileLent=0;
                try
                {
                    //此处得到会员所在的会员组的上传信息
                    if (isAdmin != 1)
                    {
                        rootPublic pd = new rootPublic();
                        DataTable dt = pd.getGroupUpInfo(Foosun.Global.Current.UserNum);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Extension = dt.Rows[0]["upfileType"].ToString();
                            getFileLent = int.Parse(dt.Rows[0]["upfileSize"].ToString()) * 1024;
                        }
                    }
                    else{getFileLent = FileLength;}
                    string fileName = PathToName(PostedFile.FileName);
                    string lastFileName = "";
                    switch (fileType)
                    {
                        case "0":
                            lastFileName = fileName;
                            break;
                        case "1":
                            lastFileName = "副件" + fileName;
                            break;
                        case "2":
                            lastFileName = "1" + fileName;
                            break;
                        case "3":
                            lastFileName = string.Format("{0}{1}{2}{3}{4}{5}.{6}", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString(), DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), Extension);
                            break;
                        default:
                            lastFileName = fileName;
                            break;
                    }
                    string _fileName = "";
                    string[] Exten = Extension.Split(',');
                    if (Exten.Length == 0){return "你未设置上传文件类型,系统不允许进行下一步操作!$0";}
                    else
                    {
                        for (int i = 0; i < Exten.Length; i++)
                        {
                            if (fileName.ToLower().EndsWith(Exten[i].ToLower()))
                            {
                                if (PostedFile.ContentLength > getFileLent) return "上传文件限制大小:" + getFileLent / 1024 + "kb！$0";
                                 //时间：2008-07-23    修改者：吴静岚
                                //实现水印、缩图 开始
                                string IsFileex = SavePath + @"\" + lastFileName;
                              
                                if (!Directory.Exists(SavePath)) { Directory.CreateDirectory(SavePath); }
                                if (_num == 1)
                                {
                                    string _Randstr = Foosun.Common.Rand.Number(6);
                                    string _tmps = DateTime.Now.Month + DateTime.Now.Day + "-" + _Randstr + "-" + fileName;
                                    if (fileType.Equals("7"))
                                    {
                                        _tmps = "small" + DateTime.Now.Month + DateTime.Now.Day + "-" + _Randstr + "-" + fileName;
                                    }
                                    //--结束 wjl>
                                    if (File.Exists(IsFileex))
                                    {
                                        postedFile.SaveAs(SavePath + @"\" + _tmps);
                                        _fileName = _tmps;
                                        return _fileName + "$1";
                                    }
                                    else
                                    {
                                        PostedFile.SaveAs(IsFileex);
                                        _fileName = fileName;
                                        return _fileName + "$1";
                                    }
                                }
                                else
                                {
                                    PostedFile.SaveAs(IsFileex);
                                    _fileName = fileName;
                                    return _fileName + "$1";
                                }
                            }
                        }
                        return "只允许上传" + Extension + " 文件!$0";
                    }
                }
                catch (System.Exception exc)
                {
                    return exc.Message + "$0";
                }
            }
            else
            {
                return "上文件失败!$0";
            }
        }

    }
}
