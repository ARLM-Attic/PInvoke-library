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

public partial class manage_publish_psf_add : Foosun.Web.UI.ManagePage
{
    Psframe rd = new Psframe();
    rootPublic pd = new rootPublic();
    public string Str_CreatTime;//创建时间
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.CacheControl = "no-cache";                        //设置页面无缓存

            //LoginInfo.CheckPop("权限代码", "0", "1", "9");             //权限代码
            copyright.InnerHtml = CopyRight;
        }
        #region createtime
        Str_CreatTime = System.DateTime.Now.ToString();
        this.CreatTime.Text = Str_CreatTime;
        #endregion
    }

    /// <summary>
    /// 新增接点
    /// </summary>
    /// Code By ChenZhaohui

    #region save psf
    protected void SavePsf_ServerClick(object sender, EventArgs e)
    {

        if (Page.IsValid == true)                               //判断页面是否通过验证
        {
            #region 取得添加中的表单信息
            string Str_psfName = Foosun.Common.Input.Filter(this.psfName.Text.Trim());//接点名
            #region 复选框
            int isSubb = 0, isAlll = 0;
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
                isAlll = 1;
            }
            else
            {
                isAlll = 0;
            }
            #endregion


            #region checknum
            string Str_psfID = Foosun.Common.Rand.Number(12);
            //check: string Str_psfID = Foosun.Common.Rand.Number(12);
            //    if (rd.IsExitPSFID(Str_psfID) != 0)
            //        goto check;
            #endregion

            #region 取目录值
            string Str_LocalDir = Request.Form["LocalDir"];//本地目录
            string Str_RemoteDir = Request.Form["RemoteDir"];//远程目录
            //Response.Write(Str_LocalDir + "<br>" + Str_RemoteDir);
            //Response.End();
            #region 判断
            if (Str_LocalDir == null || Str_LocalDir == "" || Str_LocalDir == string.Empty && Str_RemoteDir == null || Str_RemoteDir == "" || Str_RemoteDir == string.Empty)
            {
                PageError("抱歉，本地目录或是远程目录不能为空", "psf.aspx");
            }
            //检查是否有已经存在的名称
            DataTable dt = rd.getTitleRecord(Str_psfName);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    PageError("对不起，该接点名称已经存在", "psf.aspx");
                }
                dt.Clear(); dt.Dispose();
            }
            #endregion
            #endregion
            #endregion
            #region 为构造函数中赋予数据
            Foosun.Model.PSF uc = new Foosun.Model.PSF();
            uc.psfID = Str_psfID;
            uc.psfName = Str_psfName;
            uc.LocalDir = Str_LocalDir;
            uc.RemoteDir = Str_RemoteDir;
            uc.isSub = isSubb;
            uc.isRecyle = 0;
            uc.CreatTime = DateTime.Parse(Str_CreatTime.ToString());
            uc.SiteID = SiteID;
            uc.isAll = isAlll;
            #endregion
            #region updata
            rd.InsertPSF(uc);
            pd.SaveUserAdminLogs(1, 1, UserNum, "新增PSF", "新增成功.psfID:" + Str_psfID + "");
            PageRight("添加成功", "psf.aspx");
            #endregion
        }
    }
    #endregion
}