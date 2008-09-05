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

public partial class manage_Sys_table_list : Foosun.Web.UI.ManagePage
{
    sys rd = new sys();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
            tableName.Text = "fs_d_" + Foosun.Common.Rand.Number(5);
            if (Foosun.Config.verConfig.PublicType != "1"){ PageError("您的版本不允许创建新闻表!", ""); }
        }
    }
    protected void shortCutsubmit(object sender, EventArgs e)
    {
        string TableName = Request.Form["TableName"];
        DataTable dt1 = rd.GetTableRecord();
        if (dt1.Rows.Count > 10)
        {
            PageError("操作错误：您最多允许复制10个表", "");
        }
        else
        {
            if (TableName.IndexOf("fs_d_") == -1)
            {
                PageError("操作错误：复制的表必须以fs_d_开头", "");
            }
            DataTable dt = rd.GetTableExsit(TableName);
            if (dt.Rows.Count > 0)
            {
                PageError("操作错误：表名已经存在", "");
            }
            else
            {
                //开始复制新闻表
                rd.CreatTableData(TableName);
                rd.InsertTableLab(TableName);
                PageRight("复制新闻表成功。", "table_manage.aspx");  
            }
        }
    }
}
