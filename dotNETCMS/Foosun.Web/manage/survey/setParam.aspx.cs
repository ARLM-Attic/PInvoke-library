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


public partial class Manage_Survey_setParam : Foosun.Web.UI.ManagePage
{
    public Manage_Survey_setParam()
    {
        Authority_Code="S005";
    }
    rootPublic rd = new rootPublic();
    Survey sur = new Survey();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache"; //清除缓存
        if (!IsPostBack)                                               //判断页面是否重载
        {
            //判断用户是否登录
            copyright.InnerHtml = CopyRight;             //获取版权信息
            if (SiteID != "0")
            {
                PageError("没有权限", "");
            }
            ParamStartLoad();                                   //载入初始参数设置页面数据
        }
    }

    /// <summary>
    /// 初始参数设置信息
    /// </summary>
    ///code by chenzhaohui 

    void ParamStartLoad()
    {
        DataTable dt = sur.sel_5();
        if (dt.Rows.Count > 0)
        {
            //投票参数设置
            IPtime.Text = dt.Rows[0]["IPtime"].ToString();
            IsReg.Text = dt.Rows[0]["IsReg"].ToString();
            IpLimit.Value = dt.Rows[0]["IpLimit"].ToString();
        }
    }

    /// <summary>
    /// 保存参数设置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    protected void SavePram_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            //取得投票参数设置添加中的表单信息
            string Str_IPtime = Foosun.Common.Input.Filter(this.IPtime.Text);//Ip时间间隔
            string Str_IsReg = Foosun.Common.Input.Filter(this.IsReg.Text);//是否需要注册?
            string Str_IpLimit = Foosun.Common.Input.Filter(this.IpLimit.Value);//IP段
            #region 判断
            if (Str_IPtime == null || Str_IPtime == string.Empty)
            {
                PageError("对不起，请填写完整", "setParam.aspx");
            }
            #endregion
            //载入数据-刷新页面
            if (sur.Update_Str_InSqls(Str_IPtime, Str_IsReg, Str_IpLimit, SiteID) != 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "投票系统参数设置", "问卷调查系统参数设置成功");
                PageRight("问卷调查系统参数设置成功", "setParam.aspx");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "投票系统参数设置", "意外错误");
                PageError("意外错误：未知错误", "shortcut_list.aspx");
            }

        }
    }
}
