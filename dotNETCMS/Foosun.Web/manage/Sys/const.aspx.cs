using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using Foosun.Common;

public partial class manage_Sys_const : Foosun.Web.UI.ManagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.CacheControl = "no-cache";         //设置页面无缓存
        string str = Foosun.Config.UIConfig.constPass;
        string Idinfo = "";
        if (Session["tb_"] == null)
        {
            Idinfo ="";          
        }
        else
        {
            Idinfo = Session["tb_"].ToString();
        }
        Session["tb_"] = null;
        if (!IsPostBack)
        {
            if (SiteID != "0") { PageError("您没有配置文件的权限", ""); }
            if (Idinfo != "" && Idinfo != String.Empty && Idinfo != null)
            {
                if (str == Idinfo)
                {
                    StartPage();
                }
                else
                {
                    PageError("密码错误,请重新输入!", "windlogin.aspx");
                }
            }
            else
            {
                Response.Redirect("windlogin.aspx");
            }
        }
    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    void StartPage()
    {
        dirMana.Text = Foosun.Config.UIConfig.dirMana;
        dirTemplet.Text = Foosun.Config.UIConfig.dirTemplet;
        dirDumm.Text = Foosun.Config.UIConfig.dirDumm;
        dirFile.Text = Foosun.Config.UIConfig.dirFile;
        filePass.Text = Foosun.CMS.FSSecurity.FDESEncrypt(Foosun.Config.UIConfig.filePass, 0);
        constPass.Text = Foosun.CMS.FSSecurity.FDESEncrypt(Foosun.Config.UIConfig.constPass, 0);
        protRand.Text = Foosun.Config.UIConfig.protRand;
        filePath.Text = Foosun.Config.UIConfig.filePath;
        dirPige.Text = Foosun.Config.UIConfig.dirPige;
        sqlConnData.Text = Foosun.Config.UIConfig.sqlConnData;
        manner.Text = Foosun.Config.UIConfig.CssPath();
        //检测是否开启密码保护功能
        protPass.Text = Foosun.Config.UIConfig.protPass;
        //检测统计是否采用独立数据库
        CheckStat(int.Parse(Foosun.Config.UIConfig.indeData));
    }

    /// <summary>
    /// 设置RadioButtonList属性
    /// </summary>
    /// <param name="pram">Xml文件字节</param>
    /// <param name="sChar">控件类型</param>
    void CheckValue(int pram,RadioButtonList sChar)
    {
        switch (pram)
        {
            case 0:
                sChar.Items[1].Selected = true;
                break;
            case 1:
                sChar.Items[0].Selected = true;
                break;
            default:
                sChar.Items[1].Selected = true;
                break;
        }
    }

    /// <summary>
    /// 提交数据处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_const_Click(object sender, EventArgs e)
    {
        #region 检测web.config是否只读
        if (Public.constReadOnly(0,"xml/sys/foosun.config"))
            Public.constReadOnly(2, "xml/sys/foosun.config");
        #endregion

        #region 更新配置文件
        foreach (Control ctl in this.Controls[0].Controls)
        {
            switch (ctl.GetType().Name)
            {
                case "TextBox":
                    {
                        TextBox tb = new TextBox();
                        tb = (TextBox)this.FindControl(ctl.ID);
                        if (tb.ID == "filePass")
                        {
                            tb.Text =  Foosun.CMS.FSSecurity.FDESEncrypt(tb.Text, 1);
                        }

                        if (tb.ID == "constPass")
                        {
                            tb.Text = Foosun.CMS.FSSecurity.FDESEncrypt(tb.Text, 1);
                        }
                        Public.SaveXmlConfig(tb.ID, tb.Text.ToString().Trim(), "xml/sys/foosun.config");
                        break;
                    }
                case "RadioButtonList":
                    {
                        RadioButtonList rbl = new RadioButtonList();
                        rbl = (RadioButtonList)this.FindControl(ctl.ID);
                        Public.SaveXmlConfig(rbl.ID, rbl.Text.ToString().Trim(), "xml/sys/foosun.config");
                        break;
                    }
                default: 
                    break;
            }
        }
        #endregion
        Public.constReadOnly(1, "xml/sys/foosun.config");
        StartPage();
        //刷新
        Foosun.Config.UIConfig.RefurbishCatch();
        PageRight("配置文件更新成功！", "const.aspx?ID=" + Foosun.CMS.FSSecurity.FDESEncrypt(this.constPass.Text,1) + "");
        
    }

    /// <summary>
    /// 判断统计是否是独立数据库选中状态
    /// </summary>
    /// <param name="pram"></param>
    void CheckStat(int pram)
    {
        switch (pram)
        {
            case 1:
                stat1.Checked = true;
                stat0.Checked = false;
                break;
            case 0:
                stat1.Checked = false;
                stat0.Checked = true;
                break;
        }
    }

    /// <summary>
    /// 回调参数
    /// </summary>
    protected void showjs()
    {
        int stat_pram = int.Parse(Foosun.Config.UIConfig.indeData);
        Response.Write("<script language=\"javascript\">Change("+ stat_pram +");</script>");
    }

}
