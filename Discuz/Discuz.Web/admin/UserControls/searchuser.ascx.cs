using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;
using System.Text;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using System.Data.Common;
namespace Discuz.Web.Admin
{
    public class searchuser : System.Web.UI.UserControl
    {
        public StringBuilder sb = new StringBuilder();

        public int menucount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //�����û�
            System.Data.DataSet dsSrc = new System.Data.DataSet();
            dsSrc.ReadXml(Page.Server.MapPath("xml/navmenu.config"));
            //�õ������˵����ȫ����
            DataRow toptabmenudr = dsSrc.Tables["toptabmenu"].Rows[0];

            string searchinfo = DNTRequest.GetString("searchinf");
            if (searchinfo != "")
            {

                IDataReader idr = DatabaseProvider.GetInstance().GetUserInfoByName(searchinfo);
                int count = 0;
                bool isexist = false;

                sb.Append("<table width=\"100%\" style=\"align:center\"><tr>");
                while(idr.Read())
                {
                    //���ҳ��Ӳ˵����е���ز˵�
                    isexist = true;

                    if (count >= 3)
                    {
                        count = 0;
                        sb.Append("</tr><tr>");
                    }
                    count++;//javascript:resetindexmenu('7','3','7,8','global/global_usergrid.aspx');
                    sb.Append("<td width=\"33%\" style=\"align:left\"><a href=\"#\" onclick=\"javascript:resetindexmenu('7','3','7,8','global/global_edituser.aspx?uid=" + idr["uid"] + "');\">" + idr["username"].ToString().ToString() + "</a></td>");
                }
                idr.Close();
                if (!isexist)
                {
                    sb.Append("û���ҵ���ƥ��Ľ��");
                }
                sb.Append("</tr></table>");
            }
            else
            { 
                sb.Append("��δ�����κ������ؼ���"); 
            }

            dsSrc.Dispose();
               
        }
    }
}