//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                     Forum:bbs.foosun.net                        ==
//==                     WebSite:www.foosun.net                      ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                   Tel:86-28-85098980/66026180                   ==
//==                   QQ:655071,MSN:ikoolls@gmail.com               ==
//==                   Email:Service@foosun.cn                       ==
//==                      Code By Simplet.Xie                        ==
//=====================================================================
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
using System.IO;

public partial class configuration_system_xml : Foosun.Web.UI.DialogPage
{
    public string Str_dirMana = Foosun.Config.UIConfig.dirDumm;//获取用户虚拟路径
    public string str_dirFile = "xml/label/";//获取文件目录
    public configuration_system_xml()
    {
        BrowserAuthor = EnumDialogAuthority.ForAdmin;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (str_dirFile == null || str_dirFile.Trim() == "")
        {
            try
            {
                Directory.CreateDirectory(Server.MapPath(Str_dirMana + "\\" + str_dirFile));
            }
            catch (Exception es)
            {
                PageError(es.ToString(), "");
            }
        }
        if (!IsPostBack)
        {
            Response.CacheControl = "no-cache";
            str_dirFile = str_dirFile + Foosun.Global.Current.SiteID;
        }
        string pathtype = Request.QueryString["pathtype"];
        string type = Request.QueryString["Type"];
        string filesPath = str_dirFile;
        string subdirpath = Request.QueryString["subdirpath"];
        string parentdirpath = Request.QueryString["parentdirpath"];
        string filetype = Request.QueryString["filetype"];
        string Path = Request.QueryString["Path"];
        switch (type)
        {
            case "EidtDirName":     //修改文件夹名称
                EidtDirName(Path);
                break;
            case "EidtFileName":    //修改文件名称
                EidtFileName(Path);
                break;
            case "DelDir":          //删除文件夹
                DelDir(Path);
                break;
            case "DelFile":          //删除文件
                DelFile(Path);
                break;
            case "AddDir":
                AddDir(Path);        //添加文件夹
                break;
            default:
                break;
        }

        ShowFile(filesPath, subdirpath, parentdirpath, filetype);
    }

    protected void ShowFile(string filesPath, string subdirpath, string parentdirpath, string filetype)
    {

        if (Str_dirMana.Trim() != "") { Str_dirMana = "\\" + Str_dirMana; }
        string filesPath1 = Server.MapPath(Str_dirMana + "\\" + filesPath + "");
        string subdirpath1 = Server.MapPath(Str_dirMana + "\\" + filesPath + "\\" + subdirpath + "");

        string parentdirpath1 = Server.MapPath(Str_dirMana + "\\" + filesPath + "\\" + parentdirpath + "");
        if (!Directory.Exists(filesPath1))
        {
            try
            {
                Directory.CreateDirectory(Server.MapPath(Str_dirMana + "\\" + str_dirFile));
            }
            catch (Exception ep)
            {
                PageError(ep.ToString(), "");
            }
        }

        if (!Directory.Exists(subdirpath1))
        {
            PageError("文件目录" + subdirpath + "不存在?。", "");
        }
        if (filetype == "" || filetype == null)
        {
            filetype = "xml/label/" + Foosun.Global.Current.SiteID;
        }
        File_List.InnerHtml = GetDirFile(subdirpath1, parentdirpath1, filetype, subdirpath, parentdirpath);
    }

    protected string GetDirFile(string dir, string ParPath, string FileType, string subdirpath, string parentdirpath)
    {
        if (Str_dirMana == null)
        {
            Str_dirMana = "/" + Str_dirMana;
        }
        DirectoryInfo[] ChildDirectory;                         //子目录集
        FileInfo[] NewFileInfo;                                 //当前所有文件
        DirectoryInfo FatherDirectory = new DirectoryInfo(dir); //当前目录
        ChildDirectory = FatherDirectory.GetDirectories("*.*"); //得到子目录集

        NewFileInfo = FatherDirectory.GetFiles();               //得到文件集，可以进行操作
        string Str_TempFileStr;
        string Str_TrStart = "<tr class=\"TR_BG_list\">";
        string Str_TrEnd = "</tr>";
        string Str_TempParentstr;
        string TempParentPath = dir.Replace("\\", "\\\\");      //路径转意
        if (ParPath == "" || ParPath == null || ParPath == string.Empty || ParPath == "undefined")
        {
            Str_TempParentstr = "当前目录:" + dir;
        }
        else
        {
            if (dir == Server.MapPath(Str_dirMana + "\\" + str_dirFile + "\\"))      //判断是否是模板目录,如果是则不显示返回上级目录
            {
                Str_TempParentstr = "<a  class=\"list_link\" href=\"javascript:AddDir('" + TempParentPath + Request.QueryString["subdirpath"] + "');\">创建目录</a>┇当前:" + dir;
            }
            else
            {
                ParPath = ParPath.Replace("\\", "\\\\");
                string Str_strpath = TempParentPath.Remove(TempParentPath.LastIndexOf("\\") - 1);//获取当前目录的上级目录
                Str_TempParentstr = "<a href=\"" + Str_dirMana + "/configuration/system/xml.aspx?FileType=" + FileType + "&subdirpath=" + parentdirpath + "&pathtype=" + Request.QueryString["pathtype"] + "\" class=\"list_link\" title=\"点击回到上级目录\">回上级</a>┇<a href=\"javascript:UpFile('" + TempParentPath + Request.QueryString["subdirpath"] + "');\" class=\"list_link\">上传</a>┇<a  class=\"list_link\" href=\"javascript:AddDir('" + TempParentPath + Request.QueryString["subdirpath"] + "');\">创建目录</a>┇当前:" + dir;
            }
        }
        Str_TempFileStr = "<table border=\"0\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">";
        Str_TempFileStr = Str_TempFileStr + Str_TrStart + "<td align=\"left\" colspan=\"5\">" + Str_TempParentstr + Str_TrEnd;
        Str_TempFileStr = Str_TempFileStr + "</table>";
        Str_TempFileStr = Str_TempFileStr + "<table border=\"0\" class=\"table\" width=\"100%\" cellpadding=\"2\" cellspacing=\"1\">";
        Str_TempFileStr = Str_TempFileStr + Str_TrStart + "<td class=\"list_link\">地址：<input class=\"form\" type=\"text\" id=\"sUrl\" name=\"sUrl\" style=\"width:60%\" />&nbsp;<input type=\"button\" class=\"form\" name=\"Submit\" value=\"选择此文件\" onclick=\"ReturnValue(document.Templetslist.sUrl.value);\" /></td>" + Str_TrEnd;

        string str_dirinfo = "";
        string str_dirinfo1 = "";
        string str_fileinfo = "";
        string str_fileinfo1 = "";
        foreach (DirectoryInfo dirInfo in ChildDirectory)       //获取此级目录下的一级目录
        {
            string TempPath = dirInfo.FullName.Replace("\\", "\\\\");

            str_dirinfo = str_dirinfo + TempPath + ",";
            str_dirinfo1 = str_dirinfo1 + dirInfo.Name.ToString() + ",";
            str_fileinfo = str_fileinfo + "!@#" + ",";
            str_fileinfo1 = str_fileinfo1 + "!@#" + ",";
        }
        str_dirFile = "/" + str_dirFile;

        foreach (FileInfo DirFile in NewFileInfo)                    //获取此级目录下的所有文件
        {
            if (SelectFile(DirFile.Extension, FileType))                       //传入文件后缀名,判断是否是被显示的文件类型,默认显示html,htm,css
            {
                str_dirinfo = str_dirinfo + "!@#" + ",";
                str_dirinfo1 = str_dirinfo1 + DirFile.Name.ToString() + ",";
                str_fileinfo = str_fileinfo + DirFile.Extension.ToLower() + ",";
                str_fileinfo1 = str_fileinfo1 + DirFile.Length.ToString() + ",";
            }
        }

        str_dirinfo = Foosun.Common.Public.Lost(str_dirinfo);
        str_dirinfo1 = Foosun.Common.Public.Lost(str_dirinfo1);
        str_fileinfo = Foosun.Common.Public.Lost(str_fileinfo);
        str_fileinfo1 = Foosun.Common.Public.Lost(str_fileinfo1);


        string[] arr_dirinfo = str_dirinfo.Split(',');
        string[] arr_dirinfo1 = str_dirinfo1.Split(',');
        string[] arr_fileinfo = str_fileinfo.Split(',');
        string[] arr_fileinfo1 = str_fileinfo1.Split(',');

        string curPage = Request.QueryString["page"];    //当前页码
        int pageSize = 20, page = 0;                     //每页显示数

        if (curPage == "" || curPage == null || curPage == string.Empty) { page = 1; }
        else
        {
            try { page = int.Parse(curPage); }
            catch
            {
                page = 0;
            }
        }
        int i, j;
        int Cnt = arr_dirinfo.Length;

        int pageCount = Cnt / pageSize;
        if (Cnt % pageSize != 0) { pageCount++; }
        if (page > pageCount) { page = pageCount; }
        if (page < 1) { page = 1; }

        for (i = (page - 1) * pageSize, j = 1; i < Cnt && j <= pageSize; i++, j++)
        {
            if (arr_dirinfo[i].ToString() != "!@#")
            {
                Str_TempFileStr = Str_TempFileStr + Str_TrStart;
                Str_TempFileStr = Str_TempFileStr + "<td class=\"list_link\" align=\"left\"><a href=\"javascript:DelDir('" + arr_dirinfo[i].ToString() + "')\"><img  src=../../sysimages/folder/del.gif border=\"0\" alt=\"删除\" /></a><img src=\"../../sysImages/FileIcon/folder.gif\" alt=\"点击进入下级目录\">&nbsp;<a href=\"" + Str_dirMana + "/configuration/system/xml.aspx?FileType=" + FileType + "&pathtype=" + Request.QueryString["pathtype"] + "&subdirpath=" + (subdirpath + "/" + arr_dirinfo1[i].ToString()).ToString().Replace("//", "/") + "&parentdirpath=" + Request.QueryString["subdirpath"] + "\"\" target=\"select_main\" class=\"list_link\" title=\"点击进入下级目录\">" + arr_dirinfo1[i].ToString() + "</a></td>";
                Str_TempFileStr = Str_TempFileStr + Str_TrEnd;
            }
            else
            {
                Str_TempFileStr = Str_TempFileStr + Str_TrStart;
                Str_TempFileStr = Str_TempFileStr + "<td class=\"list_link\" align=\"left\"><a href=\"javascript:DelFile('" + TempParentPath + "','" + arr_dirinfo1[i].ToString() + "')\"><img src=../../sysimages/folder/del.gif border=\"0\"  alt=\"删除\" /></a><img src=\"../../sysImages/FileIcon/" + GetFileIco(arr_fileinfo[i].ToString()) + "\">&nbsp;<a class=\"list_link\" href=\"javascript:sFiles('" + str_dirFile + Request.QueryString["subdirpath"] + "/" + arr_dirinfo1[i].ToString() + "');\" ondblclick=\"ReturnValue(document.Templetslist.sUrl.value);\">" + arr_dirinfo1[i].ToString() + "</a></td>";
                Str_TempFileStr = Str_TempFileStr + Str_TrEnd;
            }
        }
        string url = "xml.aspx?Path=" + Request.QueryString["Path"] + "&FileType=" + Request.QueryString["FileType"] + "&pathtype=" + Request.QueryString["pathtype"] + "&subdirpath=" + Request.QueryString["subdirpath"] + "&parentdirpath=" + Request.QueryString["parentdirpath"] + "&page=";
        Str_TempFileStr = Str_TempFileStr + Str_TrStart;
        Str_TempFileStr = Str_TempFileStr + "<td class=\"list_link\" align=\"right\" colspan=\"2\">" + ShowPage(page, pageSize, Cnt, url, pageCount) + "</td>";
        Str_TempFileStr = Str_TempFileStr + Str_TrEnd;
        Str_TempFileStr = Str_TempFileStr + "</table>";
        return Str_TempFileStr;
    }
    string PathPre()
    {
        string path_ = Request.Form["Path"];
        if (path_ != null)
        {
            int i, j;
            i = path_.LastIndexOf(str_dirFile);
            j = path_.Length - i;
            path_ = path_.Substring(i, j);
        }
        else
        {
            path_ = str_dirFile;
        }
        return path_;
    }

    protected bool SelectFile(string Extension, string str_char)
    {
        bool value = false;
        switch (Extension.ToLower())
        {
            case ".xml":
                value = true;
                break;
        }
        return value;
    }

    protected string GetFileIco(string type)
    {
        string Str_ImgPath;
        switch (type.ToLower())
        {
            case ".xml":
                Str_ImgPath = "xml.gif";
                break;
           default:
                Str_ImgPath = "unknown.gif";
                break;
        }
        return Str_ImgPath;
    }

    //=================================================
    protected void EidtDirName(string path)
    {
        string Str_OldName = Request.QueryString["OldFileName"];
        string Str_NewName = Request.QueryString["NewFileName"];
        if (Directory.Exists(path + "\\" + Str_OldName))
        {
            if (Str_OldName == "" || Str_OldName == null || Str_OldName == string.Empty || Str_NewName == "" || Str_NewName == null || Str_NewName == string.Empty)
            {
                PageError("参数传递错误！", "");
            }
            else
            {
                try
                {
                    string rdir = path + "\\" + Str_OldName;
                    string rpath = path + "\\";
                    Directory.Move(rdir.Replace("\\\\", "\\"), rpath.Replace("\\\\", "\\") + Str_NewName.Replace(".", ""));
                }
                catch (Exception e)
                {
                    PageError(e.ToString(), "");
                }
                PageRight("更改文件夹名成功！", "00");
            }
        }
        else
        {
            PageError("参数传递错误！", "00");
        }
    }

    protected void EidtFileName(string path)
    {
        string Str_OldName = Request.QueryString["OldFileName"];
        string Str_NewName = Request.QueryString["NewFileName"];
        if (File.Exists(path + "\\" + Str_OldName))
        {
            if (Str_OldName == "" || Str_OldName == null || Str_OldName == string.Empty || Str_NewName == "" || Str_NewName == null || Str_NewName == string.Empty)
            {
                PageError("参数传递错误！", "00");
            }
            else
            {
                try
                {
                    File.Move(path + "\\" + Str_OldName, path + "\\" + Str_NewName);
                }

                catch (Exception es)
                {
                    PageError("错误：" + es.ToString() + "！", "");
                }
                PageRight("更改文件名成功！", "00");
            }
        }
        else
        {
            PageError("参数传递错误.可能是您的文件名有敏感字符！", "00");
        }
    }
    //----------------------------------------------修改文件名称结束-----------------------------------------------------

    //----------------------------------------------删除文件夹-----------------------------------------------------------
    protected void DelDir(string path)
    {
        if (Directory.Exists(path))                 //判断此文件夹是否存在
        {
            try
            {
                Directory.Delete(path, true);
                PageRight("删除文件夹成功!", "00");
            }
            catch (IOException e)
            {
                PageError(e.ToString(), "00");
            }
        }
        else
        {
            PageError("参数错误!", "00");
        }
    }
    //----------------------------------------------删除文件夹结束-------------------------------------------------------
    //----------------------------------------------删除文件-----------------------------------------------------------
    protected void DelFile(string path)
    {
        string Str_FileName = Request.QueryString["filename"];
        if (File.Exists(path + "\\" + Str_FileName))                 //判断此文件是否存在
        {
            FileInfo fso = new FileInfo(path + "\\" + Str_FileName);
            try
            {
                fso.Delete();
            }
            catch (Exception e)
            {
                PageError(e.ToString(), "00");
            }
            PageRight("删除文件成功!", "00");
        }
        else
        {
            PageError("参数错误!", "00");
        }
    }
    //----------------------------------------------删除文件结束-------------------------------------------------------
    //----------------------------------------------添加文件夹---------------------------------------------------------
    protected void AddDir(string path)
    {
        string Str_DirName = Request.QueryString["NewFileName"];
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
            PageRight("添加文件夹成功!", "00");
        }
        else
        {
            PageError("此文件夹已存在!", "00");
        }
    }
    //----------------------------------------------添加文件夹结束-----------------------------------------------------
    protected string ShowPage(int page, int pageSize, int Cnt, string url, int pageCount)
    {
        string urlstr = "共" + Cnt.ToString() + "条记录,共" + pageCount.ToString() + "页,当前第" + page.ToString() + "页   ";
        urlstr = urlstr + "<a href=\"" + url + "1\" title=\"首页\" class=\"list_link\">首页</a> ";
        if ((page - 1) < 1)
        {
            urlstr = urlstr + " <a href=\"" + url + "1\" title=\"上一页\" class=\"list_link\">上一页</a> ";
        }
        else
        {
            urlstr = urlstr + " <a href=\"" + url + (page - 1) + "\" title=\"上一页\" class=\"list_link\">上一页</a> ";
        }
        if ((page + 1) < pageCount)
        {
            urlstr = urlstr + " <a href=\"" + url + (page + 1) + "\" title=\"下一页\" class=\"list_link\">下一页</a> ";
        }
        else
        {
            urlstr = urlstr + " <a href=\"" + url + pageCount + "\" title=\"下一页\" class=\"list_link\">下一页</a> ";
        }
        urlstr = urlstr + " <a href=\"" + url + pageCount + "\" title=\"尾页\" class=\"list_link\">尾页</a> ";
        return urlstr;
    }
}
