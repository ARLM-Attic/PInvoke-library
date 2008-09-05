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

public partial class configuration_system_selectPath : Foosun.Web.UI.DialogPage
{
    public configuration_system_selectPath()
    {
        BrowserAuthor = EnumDialogAuthority.Publicity;
    }
    private string str_dirMana = Foosun.Config.UIConfig.dirDumm;
    private string str_dirFile = Foosun.Config.UIConfig.dirHtml;  //获取图片或者文件路径
    private string str_FilePath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        if (str_dirMana != "" && str_dirMana != null && str_dirMana != string.Empty)//判断虚拟路径是否为空,如果不是则加上//
            str_dirMana = "//" + str_dirMana;
        string type = Request.Form["Type"];
        if (Foosun.Global.Current.SiteID.Trim() == "0")
        {
            str_FilePath = Server.MapPath(str_dirMana + "\\" + str_dirFile);
        }
        else
        {
            string _sitePath = str_dirMana + "\\" + Foosun.Config.UIConfig.dirSite + "\\" + Foosun.Global.Current.SiteID.Trim() + "\\" + str_dirFile;
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
            case "DelDir":          //删除文件夹
                DelDir(Path);
                break;
            case "AddDir":
                AddDir(Path);        //添加文件夹
                break;
            default:
                break;
        }
        ShowFile(str_FilePath, Path, ParentPath);
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
            PageError("目录不存在", "selectpath.aspx");
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
        string str_selectpath = "";
        if (ParPath == "" || ParPath == null || ParPath == string.Empty || ParPath == "undefined")
        {
            Str_TempParentstr = "当前目录:" + dir;
        }
        else
        {
            string _str_dirFileTF = "";
            if (Foosun.Global.Current.SiteID.Trim() == "0")
            {
                _str_dirFileTF = str_dirMana + "\\" + str_dirFile;
            }
            else
            {
                _str_dirFileTF = str_dirMana + "\\" + Foosun.Config.UIConfig.dirSite + "\\" + Foosun.Global.Current.SiteID.Trim() + "\\" + str_dirFile;
            }
            if (dir == Server.MapPath(_str_dirFileTF))      //判断是否是模板目录,如果是则不显示返回上级目录
            {
                Str_TempParentstr = "当前目录:" + _str_dirFileTF.Replace("\\", "/");
                str_selectpath = _str_dirFileTF.Replace("\\", "/");
            }
            else
            {
                ParPath = ParPath.Replace("\\", "\\\\");
                string Str_strpath = TempParentPath.Remove(TempParentPath.LastIndexOf("\\") - 1).Replace(str_FilePath.Replace("\\", "\\\\"), "");//获取当前目录的上级目录
                string str_thispath = "";
                if (str_dirMana != null && str_dirMana != "")
                    str_thispath = Server.MapPath(str_dirMana);
                else
                    str_thispath = Server.MapPath("/");
                Str_TempParentstr = "<a href=\"javascript:ListGo('" + Str_strpath.Replace(str_FilePath.Replace("\\", "\\\\"), "") + "','" + TempParentPath.Replace(str_FilePath.Replace("\\", "\\\\"), "") + "');\" class=\"list_link\" title=\"点击回到上级目录\">返回上级目录</a>  |  当前目录:/" + dir.Replace(str_thispath, "").Replace("\\", "/");
                str_selectpath = "/" + dir.Replace(str_thispath, "").Replace("\\", "/");
            }
        }
        ShowAddfiledir(TempParentPath.Replace(str_FilePath.Replace("\\", "\\\\"), "")); //调用显示创建目录,导入文件函数
        str_TempFileStr = "<div style=\"padding-left:10px;\">" + Str_TempParentstr + "</div>";
        str_TempFileStr += "<div style=\"padding-left:10px;\">地址：<input type=\"text\" id=\"sUrl\" name=\"sUrl\" style=\"width:60%\" value=\"" + str_selectpath + "\" />&nbsp;<input type=\"button\" class=\"form\" name=\"Submit\" value=\"选择此目录\" onclick=\"ReturnValue(document.Templetslist.sUrl.value);\" /></div>";

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
            str_TempFileStr += " <img src=\"../../sysImages/FileIcon/folder.gif\" alt=\"点击进入下级目录\" border=\"0\"><a href=\"javascript:javascript:ListGo('" + TempPath + "','" + TempParentPath + "');\" class=\"list_link\" ondblclick=\"\" title=\"点击进入下级目录\">" + dirInfo.Name.ToString() + "</a></td>";
            str_TempFileStr += str_TrEnd;
        }
        str_TempFileStr += "</table>";
        return str_TempFileStr;
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
            PageError("参数传递错误!", "selectPath.aspx");

        Foosun.CMS.Templet.Templet tpClass = new Foosun.CMS.Templet.Templet();
        int result = tpClass.EidtName(path, str_OldName, str_NewName, 0);
        if (result == 1)
            PageRight("更改文件夹名成功！", "selectPath.aspx");
        else
            PageError("参数传递错误！", "selectPath.aspx");
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
            PageRight("删除文件夹成功!", "selectPath.aspx");
        else
            PageError("参数错误!", "selectPath.aspx");
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
            PageRight("添加文件夹成功!", "selectPath.aspx");
        else
            PageError("未知错误!", "selectPath.aspx");
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
        str_Addfiledir += "<a href=\"javascript:AddDir('" + path + "');\" class=\"topnavichar\"><span style=\"color:red;\">创建目录</span></a>";
        str_Addfiledir += "</span>";
        addfiledir.InnerHtml = str_Addfiledir;
    }
}
