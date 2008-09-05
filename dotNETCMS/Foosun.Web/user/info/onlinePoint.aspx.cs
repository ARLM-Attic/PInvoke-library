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
using System.Globalization;
using cn.com.chinabank.CBSecurity;
using System.Net;
using System.IO;
using System.Text;

public partial class user_info_onlinePoint : Foosun.Web.UI.UserPage
{
    Foosun.CMS.UserMisc rd = new Foosun.CMS.UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
        div_1.Visible = true;
        div_2.Visible = false;
        string pointnumber = Request.QueryString["pointNumber"];
        if (pointnumber != null && pointnumber != "")
        {
            pointNumber_1.InnerHtml = Request.QueryString["pointNumber"].ToString();
            this.getpointNumber.Value = Request.QueryString["pointNumber"].ToString();
            string _tmp = Foosun.Common.Rand.Number(12);
            orderNumber_1.InnerHtml = _tmp;
            this.getorderNumber.Value = _tmp;
            //这里输入ISP商信息，准备开始支付
            div_1.Visible = false;
            div_2.Visible = true;
            DataTable dt = rd.getOnlinePay();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string _ispname = "";
                    switch (dt.Rows[0]["onpayType"].ToString())
                    {
                        case "0":
                            _ispname = "支付宝支付(www.alipay.com)";
                            break;
                        case "1":
                            _ispname = "网银支付(www.chinabank.com.cn)";
                            break;
                        case "2":
                            _ispname = "云网支付(www.cncard.net)";
                            break;
                    }
                    ispName.InnerHtml = "<font color=\"red\">" + _ispname +"</font>";
                    this.v_md5info.Value = "";
                    this.v_mid.Value = dt.Rows[0]["o_userName"].ToString();
                    this.v_oid.Value = _tmp;
                    this.v_amount.Value = pointnumber.ToString();
                    this.v_moneytype.Value = "CNY";
                    string _urls = dt.Rows[0]["o_returnurl"].ToString(); ;
                    this.v_url.Value = _urls;
                    string key = dt.Rows[0]["o_key"].ToString();
                    string _text = pointnumber.ToString() + "CNY" + _tmp + dt.Rows[0]["o_userName"].ToString() + _urls + key; // 拼凑加密串
                    this.v_md5info.Value = MD5Util.getMD5(_text);
                    this.postUrl.Value = dt.Rows[0]["o_sendurl"].ToString();
                }
            }

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int pointNumber = int.Parse(this.pointNumber.Text);
            Response.Redirect("onlinePoint.aspx?pointNumber=" + pointNumber + "&s=0&dec=在线银行冲值");
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string _md5 = this.v_md5info.Value;
            string _v_mid = this.v_mid.Value;
            string _v_oid = this.v_oid.Value;
            string _v_amount = this.v_amount.Value;
            string _v_moneytype = this.v_moneytype.Value;
            string _urls =  this.v_url.Value;
            Encoding encoding = Encoding.GetEncoding("GB2312");

            string postData = "v_md5info=" + _md5;
            string strUrl = this.postUrl.Value;
            postData += ("&v_mid=" + _v_mid);
            postData += ("&v_oid=" + _v_oid);
            postData += ("&v_amount=" + _v_amount);
            postData += ("&v_moneytype=" + _v_moneytype);
            postData += ("&v_url=" + _urls);
            byte[] data = encoding.GetBytes(postData);

            // 准备请求...
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(strUrl);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();
            // 发送数据
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            Response.Redirect(this.postUrl.Value);
        }
    }
}
