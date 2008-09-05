using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.CMS;
using Foosun.CMS.Common;
using System.IO;

namespace Foosun.Web.manage.Sys
{
    public partial class File_GetIn : Foosun.Web.UI.ManagePage
    {
        public File_GetIn()
        {
            Authority_Code = "Q020";
        }
        sys rd = new sys();
        rootPublic pd = new rootPublic();
        #region
        
        string FP = Foosun.Config.UIConfig.filePass;//从Web.config中读取文件密码信息
        public string str_dirMana = Foosun.Config.UIConfig.dirDumm;//获取用户虚拟路径
        #endregion
        #region 从配置文件读出文件管理路径
        public string fpath1 = Foosun.Config.UIConfig.filePath.Split(',')[0];//userfiles目录
        string str_FilePath = "";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (str_dirMana != "" && str_dirMana != null && str_dirMana != string.Empty)
                str_dirMana = "//" + str_dirMana;
            string FileID = Request.QueryString["Id"];
            Response.CacheControl = "no-cache"; //清除缓存

            #region 判断进入文件管理权限并输入密码正确才可以进入
            if (FileID != "" && FileID != null)
            {
                if (FP != FileID)
                {
                    pd.SaveUserAdminLogs(1, 1, UserNum, "文件管理登陆失败", "登陆密码错误.");
                    PageError("密码错误,请重新输入!", "File_Win_Login.aspx");
                }
            }
            else
            {
                pd.SaveUserAdminLogs(0, 1, UserNum, "文件管理登陆成功", "文件管理登陆成功.");
                Response.Redirect("File_Win_Login.aspx");
            }
            #endregion
            if (!IsPostBack) //判断页面是否重载
            {
                copyright.InnerHtml = CopyRight;  //获取版权信息
            }
            if (SiteID == "0")
            {
                str_FilePath = Server.MapPath(str_dirMana + "\\" + fpath1);
            }
            else
            {
                string _sitePath = str_dirMana + "\\" + Foosun.Config.UIConfig.dirSite + "\\" + SiteID;
                if (!Directory.Exists(Server.MapPath(_sitePath))) { Directory.CreateDirectory(Server.MapPath(_sitePath)); }
                str_FilePath = Server.MapPath(_sitePath);
            }

            string type = Request.Form["Type"];
            string Path = str_FilePath + Request.Form["Path"];
            string ParentPath = str_FilePath + Request.Form["ParentPath"]; //父级

            try
            {
                if (Path.IndexOf(str_FilePath, 0) == -1 || ParentPath.IndexOf(str_FilePath, 0) == -1)
                    Response.End();
            }
            catch { }

            #region 传递参数
            switch (type)
            {
                case "EidtDirName":       //修改文件夹名称
                    EidtDirName(Path);
                    break;
                case "EidtFileName":      //修改文件名称
                    EidtFileName(Path);
                    break;
                case "DelDir":            //删除文件夹
                    DelDir(Path);
                    break;
                case "DelFile":           //删除文件
                    DelFile(Path);
                    break;
                case "AddDir":
                    AddDir(Path);         //添加文件夹
                    break;
                case "MoveFileFolder":    //移动文件夹
                    MoveFileFolder(Path);
                    break;
                case "MoveFile":          //移动文件
                    MoveFile(Path);
                    break;
                case "clearFile":
                    string _DateNum = Request.Form["dateNums"];
                    clearFile(Path, _DateNum);
                    break;
                default:
                    break;
            }
            #endregion
            ShowFile(str_FilePath, Path, ParentPath);

        }

        /// <summary>
        /// 显示文件
        /// </summary>
        /// <param name="defaultpath"></param>
        /// <param name="path"></param>
        /// <param name="parentPath"></param>

        protected void ShowFile(string defaultpath, string path, string parentPath)
        {
            try
            {
                if (path != "" && path != null && path != string.Empty)
                {
                    defaultpath = path;
                }
                if (Directory.Exists(defaultpath) == false) //判断文件管理目录是否存在
                {
                    PageError("目录不存在", "");
                }
                filemanage_list.InnerHtml = FileManageList(defaultpath, parentPath);
            }
            catch { }
        }

        /// <summary>
        /// 文件管理页
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="ParPath"></param>
        /// <returns></returns>

        protected string FileManageList(string dir, string ParPath)
        {
            DirectoryInfo[] ChildDirectory;                         //子目录集
            FileInfo[] NewFileInfo;                                 //当前所有文件

            DirectoryInfo FatherDirectory = new DirectoryInfo(dir); //当前目录

            ChildDirectory = FatherDirectory.GetDirectories("*.*"); //得到子目录集

            NewFileInfo = FatherDirectory.GetFiles();               //得到文件集，可以进行操作

            #region 获取目录以及文件列表

            string Str_TempFileStr;
            string Str_TrStart = "<tr class=\"TR_BG_list\">";
            string Str_TrEnd = "</tr>";
            string Str_TdStart = "<td class=\"list_link\" align=\"left\">";
            string Str_TdEnd = "</td>";
            string Str_TempParentstr;
            string TempParentPath = dir.Replace("\\", "\\\\");      //路径转移
            string Str_help = "<span class=\"helpstyle\" style=\"cursor:help;\" title=\"点击查看帮助\" onClick=\"Help('H_FileManage_0001',this)\">帮助</span>";//帮助提示
            string Str_help1 = "<span class=\"helpstyle\" style=\"cursor:help;\" title=\"点击查看帮助\" onClick=\"Help('H_FileManage_0002',this)\">帮助</span>";//帮助提示

            #endregion

            #region 取得当前所在目录
            if (ParPath == "" || ParPath == null || ParPath == string.Empty || ParPath == "undefined")
            {
                Str_TempParentstr = "当前目录:" + dir + Str_help;
            }
            else
            {
                string _str_TempletTF = "";
                if (SiteID == "0")
                {
                    _str_TempletTF = str_dirMana + "\\" + fpath1;
                }
                else
                {
                    _str_TempletTF = str_dirMana + "\\" + Foosun.Config.UIConfig.dirSite + "\\" + SiteID + "\\" + fpath1;
                }

                if (dir == Server.MapPath(_str_TempletTF))  //判断是否是文件根目录,如果是则不显示返回上级目录
                {
                    Str_TempParentstr = "当前目录:\\" + fpath1 + dir.Replace(str_FilePath, "");
                }
                else
                {
                    ParPath = ParPath.Replace("\\", "\\\\");
                    string Str_strpath = TempParentPath.Remove(TempParentPath.LastIndexOf("\\") - 1).Replace(str_FilePath.Replace("\\", "\\\\"), "");//获取当前目录的上级目录
                    Str_TempParentstr = "<a href=\"javascript:ListGo('" + Str_strpath.Replace(str_FilePath.Replace("\\", "\\\\"), "") + "','" + TempParentPath.Replace(str_FilePath, "") + "');\" class=\"list_link\" title=\"点击回到上级目录\">返回上级目录</a>   |   当前目录:\\" + fpath1 + dir.Replace(str_FilePath, "");
                }
            }
            OperateFile(TempParentPath.Replace(str_FilePath.Replace("\\", "\\\\"), "")); //调用显示创建目录,上传文件，移动，删除 函数

            #endregion

            #region 文件及文件夹列表页显示
            Str_TempFileStr = "<table border=\"0\" class=\"table\" width=\"98%\" cellpadding=\"5\" cellspacing=\"1\">";
            Str_TempFileStr = Str_TempFileStr + "<td class=\"list_link\" align=\"left\"colspan=\"5\">" + Str_TempParentstr + Str_TdEnd + Str_TrEnd;
            Str_TempFileStr = Str_TempFileStr + Str_TrStart;

            Str_TempFileStr = Str_TempFileStr + Str_TdStart + "名称" + Str_TdEnd;
            Str_TempFileStr = Str_TempFileStr + Str_TdStart + "类型" + Str_TdEnd;
            Str_TempFileStr = Str_TempFileStr + Str_TdStart + "大小(byte)" + Str_TdEnd;
            Str_TempFileStr = Str_TempFileStr + Str_TdStart + "最后修改时间" + Str_TdEnd;
            Str_TempFileStr = Str_TempFileStr + Str_TdStart + "操作" + Str_TdEnd;
            Str_TempFileStr = Str_TempFileStr + Str_TrEnd;
            #endregion

            #region 获取文件夹的目录信息
            TempParentPath = TempParentPath.Replace(str_FilePath.Replace("\\", "\\\\"), "");

            foreach (DirectoryInfo dirInfo in ChildDirectory)       //获取此级目录下的一级目录(文件夹)
            {
                Str_TempFileStr = Str_TempFileStr + Str_TrStart;

                string TempPath = dirInfo.FullName.Replace("\\", "\\\\");
                TempPath = TempPath.Replace(str_FilePath.Replace("\\", "\\\\"), "");

                Str_TempFileStr = Str_TempFileStr + "<td class=\"list_link\" align=\"left\"><img src=\"../../sysImages/FileIcon/folder.gif\" alt=\"点击进入下级目录\"><a href=\"javascript:ListGo('" + TempPath + "','" + TempParentPath + "');\" class=\"list_link\" title=\"点击进入下级目录\">" + dirInfo.Name.ToString() + "</a></td>";
                Str_TempFileStr = Str_TempFileStr + Str_TdStart + "文件夹</td>";
                Str_TempFileStr = Str_TempFileStr + Str_TdStart + "-" + Str_TdEnd;
                Str_TempFileStr = Str_TempFileStr + Str_TdStart + "<span style=\"font-size:10px\">" + dirInfo.LastWriteTime.ToString() + "</span>" + Str_TdEnd;
                Str_TempFileStr = Str_TempFileStr + Str_TdStart + "<a href=\"javascript:EditFolder('" + TempParentPath + "','" + dirInfo.Name + "')\" class=\"list_link\" title=\"点击为此文件夹更名\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/editname.gif\" border=\"0\" alt=\"为此项改名\" /></a><a href=\"javascript:MoveFileFolder('" + TempParentPath + "','" + dirInfo.Name + "')\" class=\"list_link\" title=\"移动此文件夹\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/remove1.gif\" border=\"0\" alt=\"转移此项\" /></a><a href=\"javascript:DelDir('" + TempPath + "')\" class=\"list_link\" title=\"点击删除此文件夹\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除此项\" />" + Str_TdEnd;
                Str_TempFileStr = Str_TempFileStr + Str_TrEnd;
            }
            #endregion

            #region 获取文件夹下文件的信息
            foreach (FileInfo DirFile in NewFileInfo)//获取此级目录下的所有文件(文件夹)
            {
                Str_TempFileStr = Str_TempFileStr + Str_TrStart;
                Str_TempFileStr = Str_TempFileStr + "<td class=\"list_link\" align=\"left\"><img src=\"../../sysImages/FileIcon/" + GetFileIco(DirFile.Extension.ToString()) + "\">" + DirFile.Name.ToString() + Str_TdEnd;
                Str_TempFileStr = Str_TempFileStr + Str_TdStart + DirFile.Extension.ToString() + "文件" + Str_TdEnd;
                Str_TempFileStr = Str_TempFileStr + Str_TdStart + DirFile.Length.ToString() + Str_TdEnd;
                Str_TempFileStr = Str_TempFileStr + Str_TdStart + "<span style=\"font-size:10px\">" + DirFile.LastWriteTime.ToString() + "</span>" + Str_TdEnd;
                string _paths = string.Empty;
                if (string.IsNullOrEmpty(str_dirMana))
                    _paths = Foosun.Common.ServerInfo.ServerPort;
                else
                    _paths = str_dirMana;
                if (!string.IsNullOrEmpty(_paths))
                {
                    _paths = ":" + _paths;
                }
                if ((DirFile.Extension.ToString() == ".asa" || DirFile.Extension.ToString() == ".css" || DirFile.Extension.ToString() == ".shtml" || DirFile.Extension.ToString() == ".shtm" || DirFile.Extension.ToString() == ".html" || DirFile.Extension.ToString() == ".htm" || DirFile.Extension.ToString() == ".asp" || DirFile.Extension.ToString() == ".aspx" || DirFile.Extension.ToString() == ".txt" || DirFile.Extension.ToString() == ".java" || DirFile.Extension.ToString() == ".cs" || DirFile.Extension.ToString() == ".xml" || DirFile.Extension.ToString() == ".asax"))
                {
                    Str_TempFileStr = Str_TempFileStr + Str_TdStart + "<a href=\"File_OnLine_Edit.aspx?dir=" + TempParentPath + "&filename=" + DirFile.Name + "\" class=\"list_link\" title=\"在线编辑\"><img src=\"../../sysImages/folder/Vedit.gif\" border=\"0\" alt=\"在线编辑\" /></a><a href=\"File_Txt_Edit.aspx?dir=" + TempParentPath + "&filename=" + DirFile.Name + "\" class=\"list_link\" title=\"文本编辑\"><img src=\"../../sysImages/folder/Tedit.gif\" border=\"0\" alt=\"文本编辑\" /></a><a href=\"javascript:MoveFile('" + TempParentPath + "','" + DirFile.Name + "')\" class=\"list_link\" title=\"移动此文件\"><img src=\"../../sysImages/folder/js.gif\" border=\"0\" alt=\"转移此项\" /></a><a href='http://" + Request.ServerVariables["Server_Name"] + _paths + "\\" + PathPre() + "\\" + DirFile.Name + "' class=\"list_link\" title=\"点击预览此文件\" target=\"_blank\"><img src=\"../../sysImages/folder/review.gif\" border=\"0\" alt=\"预览该文件\" /></a><a href=\"javascript:EditFile('" + TempParentPath + "','" + DirFile.Name + "')\" class=\"list_link\" title=\"点击为此文件更名\"><img src=\"../../sysImages/folder/re.gif\" border=\"0\" alt=\"为此项改名\" /></a><a href=\"javascript:DelFile('" + TempParentPath + "','" + DirFile.Name + "')\" class=\"list_link\" title=\"点击删除此文件\"><img src=\"../../sysImages/folder/del.gif\" border=\"0\" alt=\"删除此项\" /></a>" + Str_TdEnd;
                }
                else
                {
                    Str_TempFileStr = Str_TempFileStr + Str_TdStart + "<a href=\"javascript:MoveFile('" + TempParentPath + "','" + DirFile.Name + "')\" class=\"list_link\" title=\"移动此文件\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/remove1.gif\" border=\"0\" alt=\"转移此项\" /></a><a href='http://" + Request.ServerVariables["Server_Name"] + _paths + "\\" + PathPre() + "\\" + DirFile.Name + "' class=\"list_link\" title=\"点击预览此文件\" target=\"_blank\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/review.gif\" border=\"0\" alt=\"预览该文件\" /></a><a href=\"javascript:EditFile('" + TempParentPath + "','" + DirFile.Name + "')\" class=\"list_link\" title=\"点击为此文件更名\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/editname.gif\" border=\"0\" alt=\"为此项改名\" /></a><a href=\"javascript:DelFile('" + TempParentPath + "','" + DirFile.Name + "')\" class=\"list_link\" title=\"点击删除此文件\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除此项\" /></a>" + Str_help1 + Str_TdEnd;
                }
                Str_TempFileStr = Str_TempFileStr + Str_TrEnd;

            }
            Str_TempFileStr = Str_TempFileStr + "</table>";

            return Str_TempFileStr;
            #endregion
        }

        /// <summary>
        /// 显示功能导航菜单
        /// </summary>

        protected void OperateFile(string path)
        {
            string Str_Addfiledir = "<table width=\"100%\" border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\" align=\"center\">";
            Str_Addfiledir = Str_Addfiledir + "<tr>";
            Str_Addfiledir = Str_Addfiledir + "<td style=\"padding-left:14px;\"><a href=\"File_GetIn.aspx?id=" + FP + "\" class=\"menulist\">管理首页</a>&nbsp;┊&nbsp;<a href=\"javascript:AddDir('" + path + "');\" class=\"menulist\">创建目录</a>&nbsp;┊&nbsp;<a href=\"javascript:UpFile('" + path + "');\" class=\"menulist\">上传文件</a>&nbsp;┊&nbsp;(<a href=\"javascript:getDateNum_del('" + path + "');\" class=\"menulist\">&nbsp;清空图片文件</a>，选择几天前的图片：<input type=\"Text\" name=\"dateNum\" value=\"\" class=\"form\" style=\"width:30px;\">)";
            Str_Addfiledir = Str_Addfiledir + "</td>";
            Str_Addfiledir = Str_Addfiledir + "</tr>";
            Str_Addfiledir = Str_Addfiledir + "</table>";
            operatefile.InnerHtml = Str_Addfiledir;
        }

        /// <summary>
        /// 获取文件图标
        /// </summary>

        protected string GetFileIco(string type)
        {
            string Str_ImgPath;
            switch (type.ToLower())
            {
                case ".htm":
                    Str_ImgPath = "html.gif";
                    break;
                case ".html":
                    Str_ImgPath = "html.gif";
                    break;
                case ".shtm":
                    Str_ImgPath = "html.gif";
                    break;
                case ".shtml":
                    Str_ImgPath = "html.gif";
                    break;
                case ".aspx":
                    Str_ImgPath = "aspx.gif";
                    break;
                case ".cs":
                    Str_ImgPath = "c.gif";
                    break;
                case ".asp":
                    Str_ImgPath = "asp.gif";
                    break;
                case ".doc":
                    Str_ImgPath = "doc.gif";
                    break;
                case ".exe":
                    Str_ImgPath = "exe.gif";
                    break;
                case ".swf":
                    Str_ImgPath = "flash.gif";
                    break;
                case ".gif":
                    Str_ImgPath = "gif.gif";
                    break;
                case ".jpg":
                    Str_ImgPath = "jpg.gif";
                    break;
                case ".jpeg":
                    Str_ImgPath = "jpg.gif";
                    break;
                case ".js":
                    Str_ImgPath = "script.gif";
                    break;
                case ".txt":
                    Str_ImgPath = "txt.gif";
                    break;
                case ".xml":
                    Str_ImgPath = "xml.gif";
                    break;
                case ".zip":
                    Str_ImgPath = "zip.gif";
                    break;
                case ".rar":
                    Str_ImgPath = "zip.gif";
                    break;
                default:
                    Str_ImgPath = "unknown.gif";
                    break;
            }
            return Str_ImgPath;
        }

        /// <summary>
        /// 取得当前路径
        /// </summary>

        string PathPre()
        {
            string path_ = str_FilePath + Request.Form["Path"];
            if (path_ != null)
            {
                int i, j;
                i = path_.LastIndexOf("" + fpath1 + "");
                j = path_.Length - i;
                path_ = path_.Substring(i, j);
            }
            else
            {
                path_ = "" + fpath1 + "";
            }
            return path_;
        }

        /// <summary>
        /// 修改文件夹名称
        /// </summary>

        protected void EidtDirName(string path)
        {
            string Str_OldName = Request.Form["OldFileName"];
            string Str_NewName = Request.Form["NewFileName"];
            if (Directory.Exists(path + "\\" + Str_OldName))
            {
                if (Str_OldName == "" || Str_OldName == null || Str_OldName == string.Empty || Str_NewName == "" || Str_NewName == null || Str_NewName == string.Empty)
                {
                    pd.SaveUserAdminLogs(1, 1, UserNum, "文件夹名称修改失败", "参数传递错误.");
                    PageError("参数传递错误！", "File_GetIn.aspx?id=" + FP + "");
                }
                else
                {
                    if (Str_OldName == Str_NewName)
                    {
                        pd.SaveUserAdminLogs(1, 1, UserNum, "文件夹名称修改失败", "名称相同，更换无效.");
                        PageError("抱歉，名称相同，更换无效", "File_GetIn.aspx?id=" + FP + "");
                    }
                    else
                    {
                        Directory.Move(path + "\\" + Str_OldName, path + "\\" + Str_NewName);
                        pd.SaveUserAdminLogs(1, 1, UserNum, "文件夹名称修改成功", "更改文件夹名成功.");
                        PageRight("更改文件夹名成功！", "File_GetIn.aspx?id=" + FP + "");
                    }
                }
            }
            else
            {
                pd.SaveUserAdminLogs(1, 1, UserNum, "文件夹名称修改失败", "参数传递错误.");
                PageError("参数传递错误！", "File_GetIn.aspx?id=" + FP + "");
            }
        }

        /// <summary>
        /// 修改文件名称
        /// </summary>

        protected void EidtFileName(string path)
        {
            string Str_OldName = Request.Form["OldFileName"];
            string Str_NewName = Request.Form["NewFileName"];
            if (File.Exists(path + "\\" + Str_OldName))
            {
                if (Str_OldName == "" || Str_OldName == null || Str_OldName == string.Empty || Str_NewName == "" || Str_NewName == null || Str_NewName == string.Empty)
                {
                    pd.SaveUserAdminLogs(1, 1, UserNum, "文件名称修改失败", "参数传递错误.");
                    PageError("参数传递错误！", "File_GetIn.aspx?id=" + FP + "");
                }
                else
                {
                    File.Move(path + "\\" + Str_OldName, path + "\\" + Str_NewName);
                    pd.SaveUserAdminLogs(1, 1, UserNum, "文件名称修改成功", "修改成功.");
                    PageRight("更改文件名成功！", "File_GetIn.aspx?id=" + FP + "");
                }
            }
            else
            {
                pd.SaveUserAdminLogs(1, 1, UserNum, "文件名称修改失败", "参数传递错误.");
                PageError("参数传递错误！", "File_GetIn.aspx?id=" + FP + "");
            }
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>

        protected void DelDir(string path)
        {
            if (Directory.Exists(path))                 //判断此文件夹是否存在
            {
                try
                {
                    Directory.Delete(path, true);
                    pd.SaveUserAdminLogs(1, 1, UserNum, "删除文件夹", "删除文件夹成功.");
                    PageRight("删除文件夹成功!", "File_GetIn.aspx?id=" + FP + "");
                }
                catch (IOException e)
                {
                    PageError(e.ToString(), "");
                }
            }
            else
            {
                pd.SaveUserAdminLogs(1, 1, UserNum, "删除文件夹失败", "参数错误.");
                PageError("参数错误!", "File_GetIn.aspx?id=" + FP + "");
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>

        protected void DelFile(string path)
        {
            string Str_FileName = Request.Form["filename"];
            if (File.Exists(path + "\\" + Str_FileName))                 //判断此文件是否存在
            {
                FileInfo fso = new FileInfo(path + "\\" + Str_FileName);
                try
                {
                    fso.Delete();
                }
                catch (Exception e)
                {
                    PageError(e.ToString(), "");
                }
                pd.SaveUserAdminLogs(1, 1, UserNum, "删除文件", "删除文件成功.");
                PageRight("删除文件成功!", "File_GetIn.aspx?id=" + FP + "");
            }
            else
            {
                pd.SaveUserAdminLogs(1, 1, UserNum, "删除文件", "参数错误.");
                PageError("参数错误!", "File_GetIn.aspx?id=" + FP + "");
            }
        }

        /// <summary>
        /// 清空图片
        /// </summary>
        /// <param name="_Path"></param>
        /// <param name="_DateNum"></param>
        protected void clearFile(string _Path, string _DateNum)
        {
            if (!Foosun.Common.Input.IsInteger(_DateNum))
            {
                PageError("请输入正整数!", "File_GetIn.aspx?id=" + FP + "");
            }
            FileInfo[] NewFileInfo;                                 //当前所有文件
            DirectoryInfo FatherDirectory = new DirectoryInfo(_Path); //当前目录
            NewFileInfo = FatherDirectory.GetFiles();               //得到文件集，可以进行操作
            string Str_FileName = "";
            foreach (FileInfo DirFile in NewFileInfo)//获取此级目录下的所有文件(文件夹)
            {
                DateTime d1 = Convert.ToDateTime(System.DateTime.Now);
                DateTime d2 = Convert.ToDateTime(DirFile.LastWriteTime);
                TimeSpan s = d1 - d2;
                double ss = s.TotalDays;
                if (ss >= int.Parse(_DateNum))
                {
                    Str_FileName = DirFile.Name.ToString();
                    if (File.Exists(_Path + "\\" + Str_FileName))                 //判断此文件是否存在
                    {
                        FileInfo fso = new FileInfo(_Path + "\\" + Str_FileName);
                        if (DirFile.Extension.ToString() == ".jpg" || DirFile.Extension.ToString() == ".jpeg" || DirFile.Extension.ToString() == ".gif" || DirFile.Extension.ToString() == ".ico" || DirFile.Extension.ToString() == ".png" || DirFile.Extension.ToString() == ".bmp" || DirFile.Extension.ToString() == ".swf")
                        {
                            fso.Delete();
                        }
                    }
                }
            }
            PageRight("" + _DateNum + " 天前的图片文件清空成功!", "File_GetIn.aspx?id=" + FP + "");
        }

        /// <summary>
        /// 添加文件夹
        /// </summary>

        protected void AddDir(string path)
        {
            string Str_DirName = Request.Form["filename"];
            if (Directory.Exists(path + "\\" + Str_DirName) == false)        //判断此文件夹是否已存在
            {
                try
                {
                    Directory.CreateDirectory(path + "\\" + Str_DirName.Replace(".", ""));
                }
                catch (Exception e)
                {
                    PageError(e.ToString(), "");
                }
                pd.SaveUserAdminLogs(1, 1, UserNum, "添加文件夹", "添加文件夹成功.");
                PageRight("添加文件夹成功!", "File_GetIn.aspx?id=" + FP + "");
            }
            else
            {
                pd.SaveUserAdminLogs(1, 1, UserNum, "添加文件夹失败", "此文件夹已存在.");
                PageError("此文件夹已存在!", "File_GetIn.aspx?id=" + FP + "");
            }
        }

        /// <summary>
        /// 移动文件夹
        /// </summary>
        /// Code By ChenZhaoHui

        protected void MoveFileFolder(string path)
        {
            string Str_OldName = Request.Form["OldFileName"];
            string Str_NewName = Request.Form["NewFileName"];
            if (Directory.Exists(path + "\\" + Str_NewName + "\\" + Str_OldName) == false) //判断在目的文件夹中是否已存在该要转移的源文件夹,若不存在，则继续下一步
            {
                if (Str_OldName == "" || Str_OldName == null || Str_OldName == string.Empty || Str_NewName == "" || Str_NewName == null || Str_NewName == string.Empty)
                {
                    pd.SaveUserAdminLogs(1, 1, UserNum, "移动文件夹", "警告!参数传递错误.");
                    PageError("警告!参数传递错误!", "File_GetIn.aspx?id=" + FP + "");
                }
                else
                {
                    if (Directory.Exists(path + "\\" + Str_NewName + "\\") == false)//判断路径是否正确
                    {
                        pd.SaveUserAdminLogs(1, 1, UserNum, "移动文件夹", "警告!路径错误.");
                        PageError("路径错误!" + "<br>" + "请您确保您的文件夹路径正确!", "File_GetIn.aspx?id=" + FP + "");
                    }
                    else
                    {
                        Directory.Move(path + "\\" + Str_OldName, path + "\\" + Str_NewName + "\\" + Str_OldName);//开始转移文件夹
                        pd.SaveUserAdminLogs(1, 1, UserNum, "移动文件夹", "恭喜!转移文件夹成功.");
                        PageRight("恭喜!转移文件夹成功!", "File_GetIn.aspx?id=" + FP + "");
                    }
                }
            }
            else
            {
                pd.SaveUserAdminLogs(1, 1, UserNum, "移动文件夹", "此文件夹已存在，不能转移");
                PageError("此文件夹已存在，不能转移!" + "<br>" + "或请您选择其他的文件夹进行转移!", "File_GetIn.aspx?id=" + FP + "");
            }

        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// Code By ChenZhaoHui

        protected void MoveFile(string path)
        {
            string Str_OldName = Request.Form["OldFileName"];
            string Str_NewName = Request.Form["NewFileName"];
            if (File.Exists(path + "\\" + Str_NewName + "\\" + Str_OldName) == false) //判断在目的文件夹中是否已存在该要转移的源文件,若不存在，则继续下一步
            {
                if (Str_OldName == "" || Str_OldName == null || Str_OldName == string.Empty || Str_NewName == "" || Str_NewName == null || Str_NewName == string.Empty)
                {
                    pd.SaveUserAdminLogs(1, 1, UserNum, "移动文件夹", "警告!参数传递错误");
                    PageError("警告!参数传递错误!", "File_GetIn.aspx?id=" + FP + "");
                }
                else
                {
                    if (Directory.Exists(path + "\\" + Str_NewName + "\\") == false)//判断路径是否正确
                    {
                        pd.SaveUserAdminLogs(1, 1, UserNum, "移动文件夹", "警告!路径错误");
                        PageError("路径错误!" + "<br>" + "请您确保您的文件夹路径正确!", "File_GetIn.aspx?id=" + FP + "");
                    }
                    else
                    {
                        File.Move(path + "\\" + Str_OldName, path + "\\" + Str_NewName + "\\" + Str_OldName);//开始转移文件
                        pd.SaveUserAdminLogs(1, 1, UserNum, "移动文件夹", "恭喜!转移文件成功");
                        PageRight("恭喜!转移文件成功!", "File_GetIn.aspx?id=" + FP + "");
                    }
                }
            }
            else
            {
                pd.SaveUserAdminLogs(1, 1, UserNum, "移动文件夹", "此文件已在目的文件夹中存在，不能转移");
                PageError("此文件已在目的文件夹中存在，不能转移!" + "<br>" + "或请您选择其他的文件夹进行转移!", "");
            }

        }
    }
}
