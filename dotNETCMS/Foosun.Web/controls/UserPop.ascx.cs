using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class controls_UserPop : System.Web.UI.UserControl
{
    private string[] arrstr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Foosun.CMS.Common.rootPublic rp = new Foosun.CMS.Common.rootPublic();
            IDataReader rd = rp.GetGroupList();
            while (rd.Read())
            {
                ListItem it = new ListItem();
                it.Value = rd["GroupNumber"].ToString();
                it.Text = rd["GroupName"].ToString();
                if (arrstr != null && arrstr.Length > 0)
                {
                    foreach (string s in arrstr)
                    {
                        if (s == it.Value)
                        {
                            it.Selected = true;
                            break;
                        }
                    }
                }
                this.LstAuthorityGroup.Items.Add(it);
            }
            rd.Close();
        }
    }

    /// <summary>
    ///设置或取得权限的类型：0:表示都可以查看;1:扣出金币;2:扣除积分;3:扣除金币和积分;4:要达到金币;5:达到积分;6:要达到金币和积分.
    /// </summary>
    public int AuthorityType
    {
        get
        {
            return int.Parse(this.DdlAuthorityType.SelectedValue);
        }
        set
        {
            this.DdlAuthorityType.SelectedValue = value.ToString();
        }
    }

    /// <summary>
    /// 设置或取得积分
    /// </summary>
    public int Point
    {
        get
        {
            if (this.TxtAuthorityPoint.Text.Trim().Equals(""))
                return 0;
            else
                return int.Parse(this.TxtAuthorityPoint.Text.Trim());
        }
        set
        {
            this.TxtAuthorityPoint.Text = value.ToString();
        }
    }

   /// <summary>
   /// 设置或取得金币数
   /// </summary>
    public int Gold
    {
        get
        {
            if (this.TxtAuthorityGold.Text.Trim().Equals(""))
                return 0;
            else
                return int.Parse(this.TxtAuthorityGold.Text.Trim());
        }
        set
        {
            this.TxtAuthorityGold.Text = value.ToString();
        }
    }

    /// <summary>
    /// 设置或取得会员组
    /// </summary>
    public string[] MemberGroup
    {
        get 
        {
            string rtstr = "";
            int i = 0;
            foreach (ListItem it in this.LstAuthorityGroup.Items)
            {
                if (it.Selected)
                {
                    if (i > 0) rtstr += ",";
                    rtstr += it.Value;
                    i++;
                }
            }
            return rtstr.Split(',');
        }
        set 
        {
            arrstr = value;
        }
    }
}
