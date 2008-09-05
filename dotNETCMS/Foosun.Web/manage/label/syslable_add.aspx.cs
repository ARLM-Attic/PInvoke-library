///************************************************************************************************************
///**********添加标签Code By DengXi****************************************************************************
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

public partial class manage_label_syslable_add : Foosun.Web.UI.ManagePage
{
    public manage_label_syslable_add()
    {
        Authority_Code = "T011";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            showInfo();
        }
    }

    /// <summary>
    /// 在前台显示分类列表
    /// </summary>
    /// <returns>在前台显示分类列表</returns>
    /// 编写时间2007-04-24   Code By DengXi

    protected void showInfo()
    {
        Foosun.CMS.Label lbc = new Foosun.CMS.Label();
        DataTable dt = lbc.GetLabelClassList();
        if (dt != null)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem itm = new ListItem();
                if (Request.QueryString["ClassID"] != ""&&Request.QueryString["ClassID"] != null)
                {
                    if (dt.Rows[i]["ClassID"].ToString() == Request.QueryString["ClassID"].ToString())
                    {
                        itm.Selected = true;
                    }
                }
                itm.Value = dt.Rows[i]["ClassID"].ToString();
                itm.Text = dt.Rows[i]["ClassName"].ToString();
                LabelClass.Items.Add(itm);
                itm = null;
            }
            dt.Clear();dt.Dispose();
        }
    }


    /// <summary>
    /// 保存标签
    /// </summary>
    /// <returns>保存标签</returns>
    /// 编写时间2007-04-24   Code By DengXi

    protected void Button1_Click(object sender, EventArgs e)
    {
        //bug修改增加提示 周峻平 2008-6-5
        if (LabelClass.Items.Count == 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "提示", "<script>if(confirm('您还没有任何标签分类!现在就添加吗?')==true){window.location.href='syslabelclass_add.aspx';}</script>");
        }
        else if (Page.IsValid)
        {
            int result = 0;

            Foosun.Model.LabelInfo lbc = new Foosun.Model.LabelInfo();
            string TmplabelName = this.LabelName.Text;
            string TmplabelName_1 = "{FS_" + TmplabelName.Replace("{", "").Replace("}", "").Replace("S_", "").Replace("Page_", "") + "}";
            if (TmplabelName.ToLower().IndexOf("free_") >=0)
            {
                PageError("为了和自由标签区别。自定义标签请不要填写free_等字符!", "");
            }
            if (TmplabelName_1.ToLower().IndexOf("fs_dynclass") >-1 || TmplabelName.ToLower().IndexOf("fs_dynspecial") >-1)
            {
                PageError("为了和动态标签区别。自定义标签请不要填写DynClass或Special等字符!", "");
            }
            lbc.Label_Name = TmplabelName_1;
            lbc.ClassID =  LabelClass.Text;
            lbc.isBack =  int.Parse(LabelBack.SelectedValue);
            lbc.Label_Content = this.ContentTextBox.Value.Replace(TmplabelName_1,"");
            lbc.Description = LabelDescription.Text;
            lbc.isSys = 0;
            lbc.isRecyle = 0;
            lbc.CreatTime = DateTime.Now;
            lbc.SiteID = SiteID;

            Foosun.CMS.Label labelc = new Foosun.CMS.Label();
            result = labelc.LabelAdd(lbc);

            if (result==1)
                PageRight("添加标签成功!", "SysLabel_List.aspx?ClassID=" + LabelClass.Text);
            else
                PageError("添加标签失败!", "");
        }
    }
}
