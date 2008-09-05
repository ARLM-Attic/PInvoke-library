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

public partial class manage_publish_psf_edit : Foosun.Web.UI.ManagePage
{
    Psframe rd = new Psframe();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.CacheControl = "no-cache";                        //设置页面无缓存

            //LoginInfo.CheckPop("权限代码", "0", "1", "9");             //权限代码
            copyright.InnerHtml = CopyRight;
            Start();                                                        //初始修改数据
        }
    }
    /// <summary>
    /// 初始化修改接点信息
    /// </summary>
    /// Code By ChenZhaohui

    #region start
    protected void Start()
    {
        string psfID = Request.QueryString["psfid"];
        int txtnum = 0;
        if (psfID == null || psfID == "" || psfID == string.Empty)
        {
            PageError("参数错误", "psf.aspx");
        }
        else
        {
            DataTable dt = rd.getPSFParam(psfID);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.psfName.Text = dt.Rows[0]["psfName"].ToString();
                if (dt.Rows[0]["isSub"].ToString() == "1")
                {
                    this.isSub.Checked = true;
                }
                else
                {
                    this.isSub.Checked = false;
                }

                if (dt.Rows[0]["isAll"].ToString() == "1")
                {
                    this.isAll.Checked = true;
                }
                else
                {
                    this.isAll.Checked = false;
                }

                this.CreatTime.Text = dt.Rows[0]["CreatTime"].ToString();

                #region

                string[] Arr_StringLocal = dt.Rows[0]["LocalDir"].ToString().Split(',');
                string[] Arr_StringRemot = dt.Rows[0]["RemoteDir"].ToString().Split(',');
                if (Arr_StringLocal.Length != Arr_StringRemot.Length)
                {
                    PageError("抱歉，参数错误", "psf.aspx");
                }
                else
                {
                    string str_Temp = "";
                    bool tf = false;
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < Arr_StringLocal.Length; i++)
                            {
                                txtnum = dt.Rows.Count;
                                if (i == 0)
                                {
                                    str_Temp = str_Temp + "<div id=\"default\" style=\"margin-bottom:1px;\"> 本地目录 <input name=\"LocalDir\" type=\"text\" style=\"width:130px;\" maxlength=\"200\" value=\"" + Arr_StringLocal[i] + "\" class=\"form\"  id=\"LocalDir\"/> ";
                                    str_Temp = str_Temp + "远程服务器目录 <input name=\"RemoteDir\" type=\"text\" id=\"RemoteDir\" value=\"" + Arr_StringRemot[i] + "\" style=\"width:130px;\" maxlength=\"100\" class=\"form\" /> ";
                                    str_Temp = str_Temp + "<span class=\"helpstyle\" style=\"cursor:help;\" title=\"点击显示帮助\" onclick=\"Help('H_psf_0006',this)\">帮助</span>&nbsp;&nbsp;<font color=\"red\">(<a href=\"javascript:psf_add()\" class=\"list_link\"><font color=\"red\"><strong>点击添加</strong></font></a>)</font></div><div id=\"temp\">";
                                }
                                else
                                {
                                    str_Temp = str_Temp + "<div id=\"" + psfID + "\"> 本地目录 <input name=\"LocalDir\" type=\"text\" style=\"width:130px;\" maxlength=\"200\" value=\"" + Arr_StringLocal[i] + "\" class=\"form\" id=\"LocalDir\"/> ";
                                    str_Temp = str_Temp + "远程服务器目录 <input name=\"RemoteDir\" type=\"text\" id=\"RemoteDir\" value=\"" + Arr_StringRemot[i] + "\" style=\"width:130px;\" maxlength=\"100\" class=\"form\" /> ";
                                    str_Temp = str_Temp + "<a href=\"#\" onclick='psf_delete(this.parentNode)' class=\"list_link\">删除</a>(<font color=\"red\">*</font>)</div>";
                                }
                            }
                            str_Temp = str_Temp + "</div>";
                            tf = false;
                        }
                        else
                        {
                            tf = true;
                        }
                        dt.Clear();
                        dt.Dispose();
                    }
                    else
                    {
                        tf = true;
                    }
                    if (tf == true)
                    {
                        str_Temp = str_Temp + "<div id=\"default\" style=\"margin-bottom:1px;\"> 本地目录 <input name=\"LocalDir\" type=\"text\" style=\"width:130px;\" maxlength=\"200\" value=\"\" class=\"form\" id=\"LocalDir\"/> ";
                        str_Temp = str_Temp + "远程服务器目录 <input name=\"RemoteDir\" type=\"text\" id=\"RemoteDir\" value=\"\" style=\"width:130px;\" maxlength=\"100\" class=\"form\" /> ";
                        str_Temp = str_Temp + "<span class=\"helpstyle\" style=\"cursor:help;\" title=\"点击显示帮助\" onclick=\"Help('H_psf_0006',this)\">帮助</span>&nbsp;&nbsp;<font color=\"red\">(<a href=\"javascript:psf_add()\" class=\"list_link\"><font color=\"red\"><strong>点击添加</strong></font></a>)</font><span id=\"spanAdTxtNum\"></span></div><div id=\"temp\"></div>";
                    }
                    DivadTxt.InnerHtml = str_Temp;
                }
                #endregion
            }
            else
            {
                PageError("参数错误", "psf.aspx");
            }
        }
    }
    #endregion

    /// <summary>
    /// 保存对修改接点的事件
    /// </summary>
    /// Code By ChenZhaohui

    #region save
    protected void SavePsf_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            string psfID = Request.QueryString["psfID"];
            if (psfID == null || psfID == "" || psfID == string.Empty)
            {
                PageError("参数错误", "psf.aspx");
            }
            else
            {

                #region 取得添加中的表单信息

                string Str_psfName = this.psfName.Text.Trim();//接点名
                #region 复选框
                int isSubb = 0, isalll = 0;
                if (isSub.Checked)
                {
                    isSubb = 1;
                }
                else
                {
                    isSubb = 0;
                }
                if (isAll.Checked)
                {
                    isalll = 1;
                }
                else
                {
                    isalll = 0;
                }
                #endregion

                string Str_CreatTime = this.CreatTime.Text.Trim();//创建时间

                #region 取目录值
                string Str_LocalDir = Request.Form["LocalDir"];//本地目录
                string Str_RemoteDir = Request.Form["RemoteDir"];//远程目录
                //Response.Write(Str_LocalDir + "<br>" + Str_RemoteDir);
                //Response.End();
                if (Str_LocalDir == null || Str_LocalDir == "" || Str_LocalDir == string.Empty && Str_RemoteDir == null || Str_RemoteDir == "" || Str_RemoteDir == string.Empty)
                {
                    PageError("抱歉，本地目录或是远程目录不能为空", "psf.aspx");
                }
                #endregion
                #endregion
                #region updata

                Foosun.Model.PSF uc = new Foosun.Model.PSF();
                uc.psfID = psfID;
                uc.psfName = Str_psfName;
                uc.LocalDir = Str_LocalDir;
                uc.RemoteDir = Str_RemoteDir;
                uc.isSub = isSubb;
                uc.isRecyle = 0;
                uc.isAll = isalll;
                if (rd.UpdatePSF(uc) != 0)
                {
                    pd.SaveUserAdminLogs(1, 1, UserNum, "修改PSF", "修改成功.psfID:" + psfID + "");
                    PageRight("修改成功", "psf.aspx");
                }
                else
                {
                    pd.SaveUserAdminLogs(1, 1, UserNum, "修改PSF", "修改失败.psfID:" + psfID + "");
                    PageRight("修改失败", "psf.aspx");
                }
                #endregion
            }
        }
    }
    #endregion
}
