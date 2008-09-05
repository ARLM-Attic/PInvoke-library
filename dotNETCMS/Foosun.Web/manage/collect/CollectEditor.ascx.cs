//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
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

public partial class manage_collect_CollectEditor : System.Web.UI.UserControl
{
    private int MaxTextLength;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.TxtEditor.Attributes.Add("onchange", "CheckLength(this," + MaxTextLength + ")");
        //onchange = "CheckLength(this,'<%Response.Write(MaxTextLength);%>')";
    }
    public string Text
    {
        set
        {
            this.TxtEditor.Text = value;
        }
        get
        {
            return this.TxtEditor.Text;
        }
    }

    public string[] SetTag
    {
        set
        {
            string[] _tag = value;
            if (_tag.Length > 0)
                this.LblTag.Visible = true;
            foreach (string s in _tag)
            {
                if (!s.Trim().Equals(""))
                {
                    Label lbt = new Label();
                    lbt.Text = s;
                    lbt.Style.Add("cursor", "hand");
                    lbt.Style.Add("margin-left", "10px");
                    lbt.Attributes.Add("onclick", "addTag(this,'" + s + "')");
                    this.PnlMenu.Controls.Add(lbt);
                }
            }
        }
    }

    public int SetTextAreaRows
    {
        set { this.TxtEditor.Rows = value;}
    }
    public int SetMaxLength
    {
        set { MaxTextLength = value;}
    }
}
