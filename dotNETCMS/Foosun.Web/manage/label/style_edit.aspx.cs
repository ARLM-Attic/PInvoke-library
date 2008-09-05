///************************************************************************************************************
///**********修改样式Code By DengXi****************************************************************************
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


public partial class manage_label_style_edit : Foosun.Web.UI.ManagePage
{
    public manage_label_style_edit()
    {
        Authority_Code = "T018";
    }
    public string UDir = "\\Content";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";                        //设置页面无缓存
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;        //获取版权信息
            string _dirdumm = Foosun.Config.UIConfig.dirDumm;
            if (_dirdumm.Trim() != "") { _dirdumm = "/" + _dirdumm; }
            style_base.InnerHtml = Foosun.Common.Public.getxmlstylelist("styleContent", _dirdumm + "/xml/cuslabeStyle/cstylebase.xml");
            style_class.InnerHtml = Foosun.Common.Public.getxmlstylelist("styleContent1", _dirdumm + "/xml/cuslabeStyle/cstyleclass.xml");
            style_special.InnerHtml = Foosun.Common.Public.getxmlstylelist("DropDownList2", _dirdumm + "/xml/cuslabeStyle/cstylespecial.xml");
            GetStyleInfo();
            getDefine();
        }
    }

    /// <summary>
    /// 读出当前样式数据并在前台显示出来
    /// </summary>
    /// <returns>读出当前样式数据并在前台显示出来</returns>
    /// 编写时间2007-04-21   Code By DengXi

    protected void GetStyleInfo()
    {
        string str_ID = Request.QueryString["styleID"];
        styleID.Value = str_ID;
        Foosun.CMS.Style.Style stClass = new Foosun.CMS.Style.Style();
        DataTable dt = stClass.getstyleInfo(str_ID);
        if (dt != null)
        {
            styleClass.Text = dt.Rows[0]["ClassID"].ToString();
            getClassInfo(dt.Rows[0]["ClassID"].ToString());
            styleName.Text = dt.Rows[0]["StyleName"].ToString();
            ContentTextBox.Value = dt.Rows[0]["Content"].ToString();
            Description.Text = dt.Rows[0]["Description"].ToString();
 
            dt.Clear();
            dt.Dispose();
        }
        else
        {
            PageError("参数传递错误!", "");
        }
    }

    /// <summary>
    /// 取得分类列表
    /// </summary>
    /// <param name="ClassID">当前样式选中的栏目</param>
    /// <returns>在前台显示分类列表</returns>
    /// 编写时间2007-04-21   Code By DengXi

    protected void getClassInfo(string ClassID)
    {
        Foosun.CMS.Style.Style stClass = new Foosun.CMS.Style.Style();
        DataTable dt = stClass.styleClassList();
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem itm = new ListItem();
                if (dt.Rows[i]["ClassID"].ToString() == ClassID)
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
    /// 保存修改
    /// </summary>
    /// <returns>保存修改</returns>
    /// 编写时间2007-04-21   Code By DengXi

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int result = 0;
            Foosun.Model.StyleInfo stClass = new Foosun.Model.StyleInfo();
            stClass.styleID = Foosun.Common.Input.Filter(Request.Form["styleID"]);
            stClass.StyleName = Request.Form["styleName"];
            stClass.ClassID = Request.Form["styleClass"];
            string StContent = ContentTextBox.Value;
            if (StContent.ToLower().IndexOf("<p>") > -1 && StContent.IndexOf("</p>") > -1)
            {
                StContent = Foosun.Common.Input.RemovePor(StContent);
            }
            stClass.Content = StContent;
            stClass.Description = Request.Form["Description"];
            stClass.CreatTime = DateTime.Now;

            Foosun.CMS.Style.Style styleClass = new Foosun.CMS.Style.Style();
            result = styleClass.styleEdit(stClass);
            
            if (result==1)
                PageRight("修改样式成功!", "style.aspx?ClassID=" + Request.Form["styleClass"]);
            else
                PageError("修改样式失败!", "");
        }
    }
}
