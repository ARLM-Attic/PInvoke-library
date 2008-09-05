using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace Foosun.Web.Install
{
    public partial class finishinstall : Foosun.Web.UI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string[] arr_file ={ "Index.aspx", "step1.aspx", "step2.aspx", "step3.aspx", "step4.aspx", "step_End.aspx" };
                for (int i = 0; i < arr_file.Length; i++)
                {
                    string s_filepath = Server.MapPath(arr_file[i].ToString());
                    if (File.Exists(s_filepath))
                    {
                        File.Delete(s_filepath);
                    }
                    if (Directory.Exists(Server.MapPath("~/install/SQL")))
                    {
                        Directory.Delete(Server.MapPath("~/install/SQL"));
                    }
                }
            }
            catch
            {
                Response.Redirect("../" + Foosun.Config.UIConfig.dirMana + "/login.aspx?");
            }
            Response.Redirect("../" + Foosun.Config.UIConfig.dirMana + "/login.aspx?");
        }
    }
}
