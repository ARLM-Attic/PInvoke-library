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

public partial class configuration_system_selectuserpic : Foosun.Web.UI.DialogPage
{
    public configuration_system_selectuserpic()
    {
        BrowserAuthor = EnumDialogAuthority.ForAdmin | EnumDialogAuthority.ForPerson;
    }
    private string str_dirMana = Foosun.Config.UIConfig.dirDumm;
    private string str_dirSite = Foosun.Config.UIConfig.dirSite;
    private string str_dirUserFile = Foosun.Config.UIConfig.UserdirFile;
    private string str_dirFile = "";  //获取图片或者文件路径
    private string str_FilePath = "";
    bool tf = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        str_dirFile = str_dirUserFile + "\\" + Foosun.Global.Current.UserNum;
        Response.CacheControl = "no-cache";
        if (str_dirMana != "" && str_dirMana != null && str_dirMana != string.Empty)//判断虚拟路径是否为空,如果不是则加上//
            str_dirMana = "//" + str_dirMana;

        if (Request.QueryString["FileType"] == "user_Hpic")
        {
            getDefaultHead();
        }
        else
        {
            if (Request.QueryString["FileType"] == "user_pic")
                tf = true;

            string type = Request.Form["Type"];
            if (Foosun.Global.Current.SiteID == "0")
            {
                str_FilePath = Server.MapPath(str_dirMana + "\\" + str_dirFile);
            }
            else
            {
                string _sitePath = str_dirMana + "\\" + str_dirSite + "\\" + str_dirFile;
                if (!Directory.Exists(Server.MapPath(_sitePath))) { Directory.CreateDirectory(Server.MapPath(_sitePath)); }
                str_FilePath = Server.MapPath(_sitePath);
            }
            string Path = str_FilePath + Request.Form["Path"];
            string ParentPath = str_FilePath + Request.Form["ParentPath"]; //父级
            try
            {
                if (Path.IndexOf(str_FilePath, 0) == -1 || ParentPath.IndexOf(str_FilePath, 0) == -1)
                    Response.End();
            }
            catch { }

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

            ShowFile(str_FilePath, Path, ParentPath);
        }
    }

    /// <summary>
    /// 显示文件列表
    /// </summary>
    /// <param name="defaultpath">默认路径</param>
    /// <param name="path">当前路径</param>
    /// <param name="parentPath">父目录路径</param>
    /// <returns>显示文件列表</returns>
    /// Code By DengXi

    protected void ShowFile(string defaultpath, string path, string parentPath)
    {

        if (path != "" && path != null && path != string.Empty)
        {
            defaultpath = path;
        }
        if (Directory.Exists(defaultpath) == false)            //判断模板目录是否存在
        {
            //PageError(defaultpath+"目录不存在", "selectuserpic.aspx");
            //如果不存在则新建一个目录
            Directory.CreateDirectory(defaultpath);
        }
        File_List.InnerHtml = GetDirFile(defaultpath, parentPath);
    }

    /// <summary>
    /// 显示文件列表
    /// </summary>
    /// <param name="dir">当前路径</param>
    /// <param name="ParPath">父目录路径</param>
    /// <returns>显示文件列表</returns>
    /// Code By DengXi

    protected string GetDirFile(string dir, string ParPath)
    {
        DirectoryInfo[] ChildDirectory;                         //子目录集
        FileInfo[] NewFileInfo;                                 //当前所有文件

        DirectoryInfo FatherDirectory = new DirectoryInfo(dir); //当前目录

        ChildDirectory = FatherDirectory.GetDirectories("*.*"); //得到子目录集

        NewFileInfo = FatherDirectory.GetFiles();               //得到文件集，可以进行操作
        //-----------获取目录以及文件列表
        string str_TempFileStr;
        string str_TrStart = "<tr class=\"TR_BG_list\" onmouseover=\"javascript:overColor(this);\" onmouseout=\"javascript:outColor(this);\">";
        string str_TrEnd = "</tr>";
        string str_TdStart = "<td class=\"list_link\" align=\"left\">";
        string str_TdEnd = "</td>";
        string Str_TempParentstr;
        string TempParentPath = dir.Replace("\\", "\\\\");      //路径转意

        //------------取得当前所在目录
        if (ParPath == "" || ParPath == null || ParPath == string.Empty || ParPath == "undefined")
        {
            Str_TempParentstr = "当前目录:" + dir;
        }
        else
        {
            string _str_dirFileTF = "";
            if (Foosun.Global.Current.SiteID == "0")
            {
                _str_dirFileTF = str_dirMana + "\\" + str_dirFile;
            }
            else
            {
                _str_dirFileTF = str_dirMana + "\\" + str_dirSite + "\\" + str_dirFile;
            }
            if (dir == Server.MapPath(_str_dirFileTF))      //判断是否是模板目录,如果是则不显示返回上级目录
            {
                Str_TempParentstr = "当前目录:" + _str_dirFileTF.Replace("\\", "/");
            }
            else
            {
                string str_thispath = "";
                if (str_dirMana != null && str_dirMana != "")
                    str_thispath = Server.MapPath(str_dirMana);
                else
                    str_thispath = Server.MapPath("/");


                ParPath = ParPath.Replace("\\", "\\\\");
                string Str_strpath = TempParentPath.Remove(TempParentPath.LastIndexOf("\\") - 1).Replace(str_FilePath.Replace("\\", "\\\\"), "");//获取当前目录的上级目录
                Str_TempParentstr = "<a href=\"javascript:ListGo('" + Str_strpath.Replace(str_FilePath.Replace("\\", "\\\\"), "") + "','" + TempParentPath.Replace(str_FilePath.Replace("\\", "\\\\"), "") + "');\" class=\"list_link\" title=\"点击回到上级目录\">返回上级目录</a>   |   当前目录:/" + dir.Replace(str_thispath, "").Replace("\\", "/");
            }
        }
        ShowAddfiledir(TempParentPath.Replace(str_FilePath.Replace("\\", "\\\\"), "")); //调用显示创建目录,导入文件函数
        str_TempFileStr = "<div style=\"padding-left:10px;\">" + Str_TempParentstr + "</div>";
        str_TempFileStr += "<div style=\"padding-left:10px;\">地址：<input type=\"text\" id=\"sUrl\" name=\"sUrl\" style=\"width:60%\" />&nbsp;<input type=\"button\" class=\"form\" name=\"Submit\" value=\"选择此文件\" onclick=\"ReturnValue(document.Templetslist.sUrl.value);\" /></div>";

        str_TempFileStr += "<table border=\"0\" class=\"table\" width=\"100%\" cellpadding=\"5\" cellspacing=\"1\">";
        //---------------获取目录信息
        TempParentPath = TempParentPath.Replace(str_FilePath.Replace("\\", "\\\\"), "");

        string str_parpath = TempParentPath.Replace("\\\\", "/");

        foreach (DirectoryInfo dirInfo in ChildDirectory)       //获取此级目录下的一级目录
        {
            str_TempFileStr += str_TrStart;
            string TempPath = dirInfo.FullName.Replace("\\", "\\\\");

            TempPath = TempPath.Replace(str_FilePath.Replace("\\", "\\\\"), "");

            str_TempFileStr += str_TdStart + "<a href=\"javascript:EditFolder('" + TempParentPath + "','" + dirInfo.Name + "');\" class=\"list_link\"><img src=\"../../sysimages/folder/re.gif\" border=\"0\" alt=\"改名\" /></a><a href=\"javascript:DelDir('" + TempPath + "');\" class=\"list_link\"><img src=\"../../sysimages/folder/del.gif\" border=\"0\" alt=\"删除\" /></a> ";
            str_TempFileStr += " <img src=\"../../sysImages/FileIcon/folder.gif\" alt=\"点击进入下级目录\" border=\"0\"><a href=\"javascript:ListGo('" + TempPath + "','" + TempParentPath + "');\" class=\"list_link\" title=\"点击进入下级目录\">" + dirInfo.Name.ToString() + "</a></td>";
            str_TempFileStr += str_TrEnd;
        }

        //--------------获取文件信息
        foreach (FileInfo DirFile in NewFileInfo)                    //获取此级目录下的所有文件
        {
            if (SelectFile(DirFile.Extension))                       //传入文件后缀名,判断是否是被显示的文件类型,默认显示html,htm,css
            {
                string str_replace = "{@UserdirFile}";

                string str_picadress = str_dirMana + "/" + str_replace + "/" + Foosun.Global.Current.UserNum + "/" + str_parpath + "/" + DirFile.Name;
                str_picadress = str_picadress.Replace("//", "/");

                string str_picshowadress = str_dirMana + "/" + str_dirFile + "/" + str_parpath + "/" + DirFile.Name;
                str_picshowadress = Foosun.Publish.CommonData.getUrl() + str_picshowadress.Replace("//", "/");
                str_picshowadress = str_picshowadress.Replace("\\", "/");
                string str_showpic = "";
                if (tf == true)
                    str_showpic = "onmouseover=\"javascript:ShowDivPic(this,'" + str_picshowadress + "','" + DirFile.Extension + "'," + DirFile.Length + ");\" onmouseout=\"javascript:hiddDivPic();\"";

                str_TempFileStr += str_TrStart;
                str_TempFileStr += str_TdStart + "<a href=\"javascript:EditFile('" + TempParentPath + "','" + DirFile.Name + "')\" class=\"list_link\"><img src=\"../../sysimages/folder/re.gif\" border=\"0\" alt=\"改名\" /></a><a href=\"javascript:DelFile('" + TempParentPath + "','" + DirFile.Name + "')\" class=\"list_link\"><img src=\"../../sysimages/folder/del.gif\" border=\"0\" alt=\"删除\" /></a> ";
                str_TempFileStr += " <img src=\"../../sysImages/FileIcon/" + GetFileIco(DirFile.Extension.ToString()) + "\"><a class=\"list_link\" href=\"javascript:sFiles('" + str_picadress + "');\" " + str_showpic + "  ondblclick=\"ReturnValue(document.Templetslist.sUrl.value);\">" + DirFile.Name.ToString() + str_TdEnd;
                str_TempFileStr += str_TrEnd;
            }
        }

        str_TempFileStr += "</table>";
        return str_TempFileStr;
    }

    protected void getDefaultHead()
    {
        tf = true;
        FileInfo[] NewFileInfo;                                 //当前所有文件

        DirectoryInfo FatherDirectory = new DirectoryInfo(Server.MapPath(str_dirMana + "/sysImages/user/face")); //当前目录
        NewFileInfo = FatherDirectory.GetFiles();               //得到文件集，可以进行操作
        //--------------获取文件信息
        string str_TempFileStr = "<input type=\"hidden\" id=\"sUrl\" name=\"sUrl\" style=\"width:60%\" />" +
                                 "<table border=\"0\" class=\"table\" width=\"100%\" cellpadding=\"5\" cellspacing=\"1\">";
        foreach (FileInfo DirFile in NewFileInfo) 
        {
            if (SelectFile(DirFile.Extension))
            {
                string str_picshowadress = Foosun.Publish.CommonData.getUrl() + "/sysImages/user/face/" + DirFile.Name;
                string str_showpic = "onmouseover=\"javascript:ShowDivPic(this,'" + str_picshowadress + "','" + DirFile.Extension + "'," + DirFile.Length + ");\" onmouseout=\"javascript:hiddDivPic();\"";

                str_TempFileStr += "<tr class=\"TR_BG_list\" onmouseover=\"javascript:overColor(this);\" onmouseout=\"javascript:outColor(this);\">";
                str_TempFileStr += "<td class=\"list_link\" align=\"left\"><img src=\"../../sysImages/FileIcon/" + GetFileIco(DirFile.Extension.ToString()) + "\"><a class=\"list_link\" href=\"javascript:ReturnValue('" + str_dirMana + "/sysImages/user/face/" + DirFile.Name + "');\" " + str_showpic + ">" + DirFile.Name.ToString() + "</td>";
                str_TempFileStr += "</tr>";
            }
        }
        str_TempFileStr += "</table>";
        File_List.InnerHtml = str_TempFileStr;
    }


    /// <summary>
    /// 获取上级目录
    /// </summary>
    /// <returns>获取上级目录</returns>
    /// Code By DengXi

    string PathPre()
    {
        Foosun.CMS.Templet.Templet tpClass = new Foosun.CMS.Templet.Templet();
        string str_path = tpClass.PathPre(str_FilePath + Request.Form["Path"], str_dirFile);
        return str_path;
    }

    /// <summary>
    /// 判断文件后缀名,选取要列举出来的文件
    /// </summary>
    /// <param name="Extension">文件后缀名</param>
    /// <returns>如果是所列举的类型,则返回true,否则为false</returns>
    /// Code By DengXi

    protected bool SelectFile(string Extension)
    {
        bool value = false;
        if (tf == false)
        {
            switch (Extension.ToLower())
            {
                case ".htm":
                    value = true;
                    break;
                case ".html":
                    value = true;
                    break;
                case ".shtm":
                    value = true;
                    break;
                case ".shtml":
                    value = true;
                    break;
                case ".txt":
                    value = true;
                    break;
                case ".aspx":
                    value = true;
                    break;
                case ".asp":
                    value = true;
                    break;
                case ".rar":
                    value = true;
                    break;
                case ".zip":
                    value = true;
                    break;
                case ".exe":
                    value = true;
                    break;
                case ".doc":
                    value = true;
                    break;
                case ".rm":
                    value = true;
                    break;
                case ".rmvb":
                    value = true;
                    break;
                case ".mp3":
                    value = true;
                    break;
                case ".wma":
                    value = true;
                    break;
                case ".asf":
                    value = true;
                    break;
                case ".flv":
                    value = true;
                    break;
                case ".avi":
                    value = true;
                    break;
                case ".mpg":
                    value = true;
                    break;
                case ".wmv":
                    value = true;
                    break;
                case ".swf":
                    value = true;
                    break;
                default:
                    value = false;
                    break;
            }
        }
        else
        {
            switch (Extension.ToLower())
            {
                case ".gif":
                    value = true;
                    break;
                case ".jpg":
                    value = true;
                    break;
                case ".jpeg":
                    value = true;
                    break;
                case ".ico":
                    value = true;
                    break;
                case ".png":
                    value = true;
                    break;
                case ".bmp":
                    value = true;
                    break;
                case ".swf":
                    value = true;
                    break;
                case ".fla":
                    value = true;
                    break;
                case ".psd":
                    value = true;
                    break;
                case ".tif":
                    value = true;
                    break;
                case ".flv":
                    value = true;
                    break;
                default:
                    value = false;
                    break;
            }
        }
        return value;
    }

    /// <summary>
    /// 获取文件图标
    /// </summary>
    /// <param name="type">文件后缀名</param>
    /// <returns>返回与文件后缀名相匹配的ICO图标</returns>
    /// Code By DengXi

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
    /// 修改文件夹名称
    /// </summary>
    /// <param name="path">文件夹路径</param>
    /// <returns>修改文件夹名称</returns>
    /// Code By DengXi


    protected void EidtDirName(string path)
    {
        string str_OldName = Request.Form["OldFileName"];
        string str_NewName = Request.Form["NewFileName"];
        if (str_OldName == "" || str_OldName == null || str_OldName == string.Empty || str_NewName == "" || str_NewName == null || str_NewName == string.Empty)
            PageError("参数传递错误!", "selectuserpic.aspx?FileType=" + Request.QueryString["FileType"]);

        Foosun.CMS.Templet.Templet tpClass = new Foosun.CMS.Templet.Templet();
        int result = tpClass.EidtName(path, str_OldName, str_NewName, 0);
        if (result == 1)
            PageRight("更改文件夹名成功！", "selectuserpic.aspx?FileType=" + Request.QueryString["FileType"]);
        else
            PageError("参数传递错误！", "selectuserpic.aspx?FileType=" + Request.QueryString["FileType"]);
    }

    /// <summary>
    /// 修改文件名称
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>修改文件名称</returns>
    /// Code By DengXi

    protected void EidtFileName(string path)
    {
        string str_OldName = Request.Form["OldFileName"];
        string str_NewName = Request.Form["NewFileName"];

        if (str_OldName == "" || str_OldName == null || str_OldName == string.Empty || str_NewName == "" || str_NewName == null || str_NewName == string.Empty)
            PageError("参数传递错误!", "selectuserpic.aspx?FileType=" + Request.QueryString["FileType"]);

        Foosun.CMS.Templet.Templet tpClass = new Foosun.CMS.Templet.Templet();
        int result = tpClass.EidtName(path, str_OldName, str_NewName, 1);
        if (result == 1)
            PageRight("更改文件名成功！", "selectuserpic.aspx?FileType=" + Request.QueryString["FileType"]);
        else
            PageError("参数传递错误！", "selectuserpic.aspx?FileType=" + Request.QueryString["FileType"]);
    }

    /// <summary>
    /// 删除文件夹
    /// </summary>
    /// <param name="path">文件夹路径</param>
    /// <returns>删除文件夹</returns>
    /// Code By DengXi

    protected void DelDir(string path)
    {
        int result = 0;
        Foosun.CMS.Templet.Templet tpClass = new Foosun.CMS.Templet.Templet();
        result = tpClass.Del(path, "", 0);
        if (result == 1)
            PageRight("删除文件夹成功!", "selectuserpic.aspx?FileType=" + Request.QueryString["FileType"]);
        else
            PageError("参数错误!", "selectuserpic.aspx?FileType=" + Request.QueryString["FileType"]);
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>删除文件</returns>
    /// Code By DengXi

    protected void DelFile(string path)
    {
        string str_FileName = Request.Form["filename"];

        int result = 0;
        Foosun.CMS.Templet.Templet tpClass = new Foosun.CMS.Templet.Templet();
        result = tpClass.Del(path, str_FileName, 1);
        if (result == 1)
            PageRight("删除文件成功!", "selectuserpic.aspx?FileType=" + Request.QueryString["FileType"]);
        else
            PageError("参数错误!", "selectuserpic.aspx?FileType=" + Request.QueryString["FileType"]);
    }

    /// <summary>
    /// 添加文件夹
    /// </summary>
    /// <param name="path">文件夹路径</param>
    /// <returns>添加文件夹</returns>
    /// Code By DengXi


    protected void AddDir(string path)
    {
        string str_DirName = Request.Form["filename"];

        int result = 0;
        Foosun.CMS.Templet.Templet tpClass = new Foosun.CMS.Templet.Templet();
        result = tpClass.AddDir(path, str_DirName);
        if (result == 1)
            PageRight("添加文件夹成功!", "selectuserpic.aspx?FileType=" + Request.QueryString["FileType"]);
        else
            PageError("未知错误!", "selectuserpic.aspx?FileType=" + Request.QueryString["FileType"]);
    }

    /// <summary>
    /// 显示导入文件,创建目录
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>返回操作区域</returns>
    /// Code By DengXi

    protected void ShowAddfiledir(string path)
    {
        string str_Addfiledir = "<span style=\"padding-left:10px;\">";
        str_Addfiledir += "<a href=\"javascript:AddDir('" + path + "');\" class=\"topnavichar\">创建目录</a>&nbsp;&nbsp;<a href=\"javascript:UpFile('" + path + "','" + Request.Form["ParentPath"] + "');\"  class=\"topnavichar\"><span style=\"color:red;\">上传文件</span></a>";
        str_Addfiledir += "</span>";
        addfiledir.InnerHtml = str_Addfiledir;
    }
}
