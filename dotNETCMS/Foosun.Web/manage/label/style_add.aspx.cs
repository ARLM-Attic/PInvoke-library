///************************************************************************************************************
///**********添加样式Code By DengXi****************************************************************************
///************************************************************************************************************
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
using System.IO;
using System.Xml;

public partial class manage_label_style_add : Foosun.Web.UI.ManagePage
{
    public manage_label_style_add()
    {
        Authority_Code = "T018";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";                        //设置页面无缓存
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;        //获取版权信息
            string _dirdumm = Foosun.Config.UIConfig.dirDumm;
            if (_dirdumm.Trim() != ""){ _dirdumm = "/" + _dirdumm; }
            style_base.InnerHtml = Foosun.Common.Public.getxmlstylelist("styleContent", _dirdumm + "/xml/cuslabeStyle/cstylebase.xml");
            style_class.InnerHtml = Foosun.Common.Public.getxmlstylelist("styleContent1", _dirdumm + "/xml/cuslabeStyle/cstyleclass.xml");
            style_special.InnerHtml = Foosun.Common.Public.getxmlstylelist("DropDownList2", _dirdumm + "/xml/cuslabeStyle/cstylespecial.xml");
            showInfo();
            getDefine();
        }
    }

    /// <summary>
    /// 在前台显示分类列表,以及样式列表
    /// </summary>
    /// <returns>在前台显示分类列表,以及样式列表</returns>
    /// 编写时间2007-04-20   Code By DengXi

    protected void showInfo()
    {
        //Foosun.CMS.Style.Style stClass = new Foosun.CMS.Style.Style();
        //DataTable dt = stClass.styleClassList();
        //if (dt != null)
        //{
        //    styleClass.DataTextField = "Sname";
        //    if (Request.QueryString["ClassID"] == dt.Rows[0]["ClassID"])
        //    {
        //        styleClass.Selected = true;
        //    }
        //    styleClass.DataValueField ="ClassID";
        //    styleClass.DataSource = dt;
        //    styleClass.DataBind();
        //    dt.Clear();
        //    dt.Dispose();
        //}
        //ListItem itm = new ListItem();
        //itm.Value = "";
        //itm.Text = "请选择分类";
        //styleClass.Items.Insert(0,itm);
        //itm = null;

        Foosun.CMS.Style.Style stClass = new Foosun.CMS.Style.Style();
        DataTable dt = stClass.styleClassList();
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem itm = new ListItem();
                if (dt.Rows[i]["ClassID"].ToString() == Request.QueryString["ClassID"])
                {
                    itm.Selected = true;
                }
                itm.Value = dt.Rows[i]["ClassID"].ToString();
                itm.Text = dt.Rows[i]["Sname"].ToString();
                styleClass.Items.Add(itm);
                itm = null;
            }
            dt.Clear();
            dt.Dispose();
        }
        ListItem itm1 = new ListItem();
        itm1.Value = "";
        itm1.Text = "请选择分类";
        styleClass.Items.Insert(0, itm1);
        itm1 = null; 


    }
    /// <summary>
    /// 获得自定义字段列表
    /// </summary>

    protected void getDefine()
    {
        Foosun.CMS.Style.Style stClass = new Foosun.CMS.Style.Style();
        DataTable dt = stClass.styledefine();

        if (dt != null)
        {
            define.DataTextField = "defineCname";
            define.DataValueField = "defineColumns";
            define.DataSource = dt;
            define.DataBind();
            dt.Clear();
            dt.Dispose();
        }
        ListItem itm = new ListItem();
        itm.Value = "";
        itm.Text = "自定义字段";
        define.Items.Insert(0, itm);
        itm = null;
        
    }


    /// <summary>
    /// 保存样式
    /// </summary>
    /// <returns>保存样式</returns>
    /// 编写时间2007-04-20   Code By DengXi

    protected void Button1_Click(object sender, EventArgs e)
    {
        //bug修改增加提示 周峻平 2008-6-5
        if (this.styleClass.Items.Count == 1)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "提示", "<script>if(confirm('您还没有任何样式分类!现在就添加吗?')==true){window.location.href='styleclass_add.aspx';}</script>");
        }
        else if(this.styleClass.SelectedIndex == 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "提示", "<script>alert('请选择分类');</script>");
        }
        else if (Page.IsValid)
        {
            int result = 0;
            Foosun.Model.StyleInfo stClass = new Foosun.Model.StyleInfo();
            stClass.StyleName = styleName.Text;
            stClass.ClassID = styleClass.Text;
            string StContent = ContentTextBox.Value;
            if (StContent.ToLower().IndexOf("<p>") > -1 && StContent.IndexOf("</p>") > -1)
            {
                StContent = Foosun.Common.Input.RemovePor(StContent);
            }
            stClass.Content = StContent;
            stClass.Description = Description.Text;
            stClass.CreatTime = DateTime.Now;
            stClass.isRecyle = 0;
            Foosun.CMS.Style.Style style_Class = new Foosun.CMS.Style.Style();
            
            result = style_Class.styleAdd(stClass);
            if (result == 1)
            {
                PageRight("添加样式成功!", "style.aspx");
            }
            else
            {
                PageError("添加样式失败!", "");
            }
        }
    }
}
