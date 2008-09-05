///************************************************************************************************************
///**********修改标签Code By DengXi****************************************************************************
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


public partial class manage_label_syslabel_edit : Foosun.Web.UI.ManagePage
{
    public manage_label_syslabel_edit()
    {
        Authority_Code = "T012";
    }
    public string str_tempClassID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";                        //设置页面无缓存
        if (!IsPostBack)
        {
            
            GetLabelInfo();
        }
    }


    /// <summary>
    /// 读出当前标签数据并在前台显示出来
    /// </summary>
    /// <returns>读出当前标签数据并在前台显示出来</returns>
    /// 编写时间2007-04-24   Code By DengXi

    protected void GetLabelInfo()
    {
        string str_ID = Foosun.Common.Input.checkID(Request.QueryString["LabelID"]);
        LabelID.Value = str_ID;
        Foosun.CMS.Label lbc = new Foosun.CMS.Label();
        DataTable dt = lbc.GetLabelInfo(str_ID);
        if (dt != null)
        {
            LabelClass.Text = dt.Rows[0]["ClassID"].ToString();
            str_tempClassID = dt.Rows[0]["ClassID"].ToString();
            getClassInfo(str_tempClassID);
            if (str_tempClassID == "99999999")
            {
                LabelName.ReadOnly = true;
                Button1.Enabled = false;
                LabelDescription.ReadOnly = true;
            }
            string tLabelName= dt.Rows[0]["Label_Name"].ToString();
            LabelName.Text = tLabelName.Replace("{FS_", "").Replace("}", "");
            LabelBack.Text = dt.Rows[0]["isBack"].ToString();
            ContentTextBox.Value = dt.Rows[0]["Label_Content"].ToString();
            LabelDescription.Text = dt.Rows[0]["Description"].ToString();
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
        string str_showstr = "";
        if (ClassID != "99999999")
        {
            Foosun.CMS.Label lbc = new Foosun.CMS.Label();
            DataTable dt = lbc.GetLabelClassList();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListItem itm = new ListItem();
                    if (dt.Rows[i]["ClassID"].ToString() == ClassID)
                        itm.Selected = true;
                    itm.Value = dt.Rows[i]["ClassID"].ToString();
                    itm.Text = dt.Rows[i]["ClassName"].ToString();
                    LabelClass.Items.Add(itm);
                    itm = null;
                }
                dt.Clear();
                dt.Dispose();
            }
            str_showstr = "请选择分类";
        }
        else
        {
            str_showstr = "系统内置";
        }
        ListItem itm1 = new ListItem();
        itm1.Value = "";
        itm1.Text = str_showstr;
        LabelClass.Items.Insert(0, itm1);
        itm1 = null;
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
            Foosun.Model.LabelInfo lbc = new Foosun.Model.LabelInfo();
            lbc.LabelID = Foosun.Common.Input.checkID(Request.Form["LabelID"]);
            string TmplabelName = Request.Form["LabelName"];
            string TmplabelName_1 = "{FS_" + TmplabelName.Replace("{", "").Replace("}", "").Replace("S_", "").Replace("ClassD_", "").Replace("SpecialD_", "").Replace("Page_", "") + "}";
            if (TmplabelName.ToLower().IndexOf("free_") >= 0)
            {
                PageError("为了和自由标签区别。自定义标签请不要填写free_等字符!", "");
            }
            lbc.Label_Name = TmplabelName_1;
            lbc.ClassID = Request.Form["LabelClass"];
            lbc.isBack = int.Parse(Request.Form["LabelBack"]);
            lbc.Label_Content = ContentTextBox.Value.Replace(TmplabelName_1, "");
            lbc.Description = Request.Form["LabelDescription"];
            lbc.CreatTime = DateTime.Now;
            lbc.SiteID = SiteID;

            Foosun.CMS.Label labelc = new Foosun.CMS.Label();
            result = labelc.LabelEdit(lbc);
            if (result==1)
                PageRight("修改标签成功!", "SysLabel_List.aspx?ClassID=" + Request.Form["LabelClass"]);
            else
                PageError("修改标签失败!", "");
        }
    }


    /// <summary>
    /// 判断是否是系统内置,执行JS
    /// </summary>
    /// <returns>判断是否是系统内置,执行JS</returns>
    /// Code By DengXi
    protected void showJs()
    {
        if (str_tempClassID == "99999999")
        {
            Response.Write("<script language=\"javascript\">document.getElementById(\"Tr_Content\").style.display=\"none\";document.getElementById(\"Tr_LabelBack\").style.display=\"none\";</script>");
        }
    }
}
