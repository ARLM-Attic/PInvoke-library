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

namespace Foosun.Web.manage.news
{
    public partial class special_templet : Foosun.Web.UI.ManagePage
    {
        Special rd = new Special();
        protected void Page_Load(object sender, EventArgs e)
        {
            ClassRender(this.splist, "0", 0);
        }

        private void ClassRender(ListBox lst, string PID, int Layer)
        {
            IDataReader dts = rd.ToTempletBind(PID);
            while (dts.Read())
            {
                    ListItem it = new ListItem();
                    string stxt = "";
                    it.Value = dts["SpecialID"].ToString();
                    if (Layer > 0)
                        stxt = "┝";
                    for (int i = 1; i < Layer; i++)
                    {
                        stxt += "┉";
                    }
                    it.Text = stxt + dts["SpecialCName"].ToString();
                    lst.Items.Add(it);
                    ClassRender(lst, dts["SpecialID"].ToString(), Layer + 1);
            }
            dts.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string templet = this.templet.Text;
            string spid = string.Empty;
            if (templet == string.Empty)
            {
                PageError("请选择模板!", "special_templet.aspx");
            }
            if (this.splist.SelectedValue.Trim().Equals(""))
            {
                PageError("请选择要移动到的专题!", "News_List.aspx");
                return;
            }

            for (int i = 0; i < this.splist.Items.Count; i++)
            {
                if (this.splist.Items[i].Selected == true)
                {
                    if (i > 0) spid += ",";
                    spid += "'" + this.splist.Items[i].Value + "'";
                }
            }

            rd.BindSPTemplet(spid, templet);
            PageRight("捆绑专题模板成功", "Special_List.aspx", true);
        }
    }
}
