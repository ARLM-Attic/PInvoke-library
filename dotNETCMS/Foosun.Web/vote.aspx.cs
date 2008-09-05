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

namespace Foosun.Web
{
    public partial class vote : Foosun.Web.UI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string NewsID = Request.QueryString["NewsID"];
            Foosun.CMS.News news = new Foosun.CMS.News();
            string votelist = "";
            if (NewsID != string.Empty && NewsID != null)
            {
                DataTable dt = news.getvote(NewsID.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    string RandStr = Foosun.Common.Rand.Number(3);
                    votelist += "<form id=\"VoteForm" + RandStr + "\" name=\"VoteForm\" method=\"post\" action=\"" + Foosun.Publish.CommonData.getUrl() + "/vote.aspx\">";
                    string voteTitle = dt.Rows[0]["voteTitle"].ToString();
                    string voteContent = dt.Rows[0]["voteContent"].ToString();
                    string creattime = dt.Rows[0]["creattime"].ToString();
                    string isTimeOutTime = dt.Rows[0]["isTimeOutTime"].ToString();
                    int ismTF = int.Parse(dt.Rows[0]["ismTF"].ToString());
                    int isMember = int.Parse(dt.Rows[0]["isMember"].ToString());
                    string numStr="";
                    string items = "";
                    if (voteContent.Trim() != string.Empty && voteContent.IndexOf("\r\n") > -1)
                    {
                        string[] CARR = voteContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        for (int i = 0; i < CARR.Length; i++)
                        {
                            string[] CARR1 = null;
                            if (CARR[i].IndexOf("|") > -1)
                            {
                                CARR1 = CARR[i].Split('|');
                                items = CARR1[0];
                                numStr = "(" + CARR1[1] + ")";
                            }
                            else
                            {
                                items = CARR[i];
                                numStr = "";
                            }
                            if (ismTF == 1)
                            {
                                votelist += "<div><input type=\"checkbox\" value=\"" + items + "\" name=\"voteitem\" value=\"checkbox\" />" + items + numStr + "</div>\r";
                            }
                            else
                            {
                                votelist += "<div><input type=\"radio\" value=\"" + items + "\" name=\"voteitem\" value=\"checkbox\" />" + items + numStr + "</div>\r";
                            }
                        }
                    }
                    votelist += "<input type=\"button\" name=\"Submit\" onclick=\"javascript:votePost(this.form);\" value=\"投票\" /></form>\r";
                    votelist += "<script language=\"javascript\">\r";
                    votelist += "function votePost(obj)\r";
                    votelist += "{\r";
                    votelist += "    var r = obj.voteitem;\r";
                    votelist += "    var voteitemvalue = '';\r";
                    votelist += "    for(var i=0;i<r.length;i++)\r";
                    votelist += "    {\r";
                    votelist += "        if(r[i].checked)\r";
                    votelist += "           voteitemvalue=r[i].value;\r";
                    votelist += "    var Action='type=add&voteitem='+escape(voteitemvalue)+'';\r";
                    votelist += "alert(escape(voteitemvalue));\r";
                    votelist += "    var options={\r";
                    votelist += "                    method:'get',\r";
                    votelist += "                    parameters:Action,\r";
                    votelist += "                    onComplete:function(transport)\r";
                    votelist += "                    { ";
                    votelist += "                        var returnvalue=transport.responseText;\r";
                    votelist += "                        var arrreturnvalue=returnvalue.split('$$$');\r";
                    votelist += "                        if (arrreturnvalue[0]==\"ERR\")\r";
                    votelist += "                        {\r";
                    votelist += "                           alert(arrreturnvalue[1]);\r";
                    votelist += "                        }\r";
                    votelist += "                        else\r";
                    votelist += "                        {\r";
                    votelist += "                           alert('发表评论成功!');\r";
                    votelist += "                        }\r";
                    votelist += "                    }\r";
                    votelist += "                 };\r";
                    votelist += "     new  Ajax.Request('" + Foosun.Publish.CommonData.getUrl() + "/vote.aspx?no-cache='+Math.random(),options);\r";
                    votelist += "    }\r";
                    votelist += "}\r";
                    votelist += "</script>\r";
                    dt.Clear(); dt.Dispose();
                }
            }
            Response.Write(votelist);
        }
    }
}
