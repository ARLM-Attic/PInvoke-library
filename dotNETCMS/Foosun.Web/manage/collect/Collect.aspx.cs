//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By JiangDong                       ==
//===========================================================
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
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Foosun.CMS.Collect;
using Foosun.DALFactory;

public partial class manage_collect_Collect : Foosun.Web.UI.ManagePage
{
    public manage_collect_Collect()
    {
        Authority_Code = "S011";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int num = 0, id = 0;
        if (Request.QueryString["id"] == null || Request.QueryString["num"] == null
            || Request.QueryString["id"].Trim().Equals("") || Request.QueryString["num"].Trim().Equals("")
            || Request.QueryString["norepeat"] == null)
        {
            PageError("参数无效", "");
        }
        id = int.Parse(Request.QueryString["id"]);
        num = int.Parse(Request.QueryString["num"]);
        //判断采集规则中栏目是否合法（wxh 2008-6-24）
        if (!IsExistClassID(id))
        {
            PageError("栏目不存在，请检查。  <a href=\"Collect_List.aspx\">返回管理", "");
        }
        bool norepeat = false;
        if (Request.QueryString["norepeat"] == "1")
            norepeat = true;
        if (id < 1 || num < 1)
            PageError("输入了无效的参数!", "");
        Collect cl = new Collect();
        cl.Collecting(id, num, norepeat);
        Response.Write("<br />釆集结束  <a href=\"Collect_List.aspx\">返回管理</a>");
        Response.End();
    }

    /// <summary>
    /// 判断采集规则中栏目是否合法（wxh 2008-6-24）
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool IsExistClassID(int id)
    {
        bool result = false;
        string classID = "";
        Foosun.CMS.Collect.Collect collect = new Foosun.CMS.Collect.Collect();
        DataTable dt = collect.GetSite(id);
        if (dt.Rows.Count>0) //获取栏目ID
        {
            classID = dt.Rows[0]["ClassID"].ToString();
        }
        if (!string.IsNullOrEmpty(classID))　//栏目ID是否在栏目中存在
        {
            //Foosun.AccessDAL.News news = new Foosun.AccessDAL.News();
            Foosun.CMS.News news = new Foosun.CMS.News();
            IDataReader dr = news.getClassInfo(classID, 0);
            if (dr.Read())
            { 
                result = true; 
            }
        }
        return result;
    }
}
