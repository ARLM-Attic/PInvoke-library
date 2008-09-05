//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                     Forum:bbs.foosun.net                        ==
//==                     WebSite:www.foosun.net                      ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                   Tel:86-28-85098980/66026180                   ==
//==                   QQ:655071,MSN:ikoolls@gmail.com               ==
//==                   Email:Service@foosun.cn                       ==
//==                      Code By WangZhenjiang                      ==
//=====================================================================
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
public partial class user_arealist_cadd : Foosun.Web.UI.ManagePage
{
    public user_arealist_cadd()
    {
        Authority_Code = "U032";
    }
    Arealist ali = new Arealist();
    private DataTable TbClass = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.CacheControl = "no-cache";
            
            TbClass = ali.sel_3();
            if (TbClass != null)
            {
                ClassRender("0", 0);
            }
            string pname = Foosun.Common.Input.Filter(Request.QueryString["Cid"].ToString());
            for (int r = 0; r < this.DropDownList1.Items.Count; r++)
            {
                if (this.DropDownList1.Items[r].Value == pname)
                {
                    this.DropDownList1.Items[r].Selected = true;
                }
            }
        }
    }

    private void ClassRender(string PID, int Layer)
    {
        DataRow[] row = TbClass.Select("Pid='" + PID + "'");
        if (row.Length < 1)
        { 
            return; 
        }
        else
        {
            foreach (DataRow r in row)
            {
                ListItem it = new ListItem();
                it.Value = r["Cid"].ToString();
                string stxt = "";
                for (int i = 0; i < Layer; i++)
                {
                    if (i == 0)
                        stxt = "├";
                    else
                        stxt += "─";
                }
                it.Text = stxt + r["cityName"].ToString();
                this.DropDownList1.Items.Add(it);
                ClassRender(r["Cid"].ToString(), Layer + 1);
            }
        }
    }

    protected void but1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            rootPublic rd = new rootPublic();
            string cityName = Foosun.Common.Input.Filter(Request.Form["cityName"].ToString());
            string Cid = Foosun.Common.Rand.Number(12);
            string OrderID = this.OrderID.Text;
            if (!Foosun.Common.Input.IsInteger(OrderID))
            {
                PageError("排序号请用0-100的数字。数字越大，越靠前。", "arealist.aspx");
            }
            DateTime creatTime = DateTime.Now;
            string Pid = this.DropDownList1.SelectedValue;

            DataTable dt = ali.sel_4();
            int cutb = dt.Rows.Count;
            string Cids = "";
            if (cutb > 0)
            {
                Cids = dt.Rows[0]["Cid"].ToString();
            }

            if (Cids != Cid)
            {
                if (ali.Add_1(Pid, Cid, cityName, creatTime, int.Parse(OrderID)) == 0)
                    {
                        rd.SaveUserAdminLogs(1, 1, UserNum, "添加小类", "添加失败");
                        PageError("添加错误", "arealist.aspx");
                    }
                    else
                    {
                        rd.SaveUserAdminLogs(1, 1, UserNum, "添加小类", "添加成功"); ;
                        PageRight("添加成功", "arealist.aspx");
                    }
            }
            else
            {
                PageError("对不起建立失败有可能是编号重复", "arealist.aspx");
            }
            
        }
    }

}

