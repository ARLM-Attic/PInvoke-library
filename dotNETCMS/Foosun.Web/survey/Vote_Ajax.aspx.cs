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

public partial class survey_Vote_Ajax : Foosun.Web.UI.BasePage
{
    Survey sur = new Survey();
    protected void Page_Load(object sender, EventArgs e)
    {
        int tid = int.Parse(Request["TID"]);
        string strvip = Request.ServerVariables["REMOTE_ADDR"].ToString();   //取得投票者的ip信息值
        string strvtime = DateTime.Now.ToString();//具体时间
        string IID = Request["Items"];
        if (IID == null)
        {
            Response.Write("您没有选择选项");
            Response.End();
        }
        IID = IID.Replace(" ", "");
        string[] TmpArr = IID.Split(',');
        string othercontent = Request["ItemsInput"];

        DataTable dt_sys = new DataTable();
        dt_sys = sur.sel_16();
        if (dt_sys==null||dt_sys.Rows.Count <= 0)
        {
            Response.Write("后台投票参数设置不正确");
            Response.End();
        }
        int IPtime = int.Parse(dt_sys.Rows[0]["IPtime"].ToString());//IP限制时间;
        int IsReg = int.Parse(dt_sys.Rows[0]["IsReg"].ToString());//是否注册;
        string IpLimit = dt_sys.Rows[0]["IpLimit"].ToString();//IP地址限制;
        DateTime nowtime = DateTime.Now;
        DateTime timesy = nowtime;
        DataTable dtp = sur.sel_17(tid);
        if (IsReg == 1 && Foosun.Global.Current.IsTimeout())//是否注册判断
        {
            Response.Write("对不起，您需要注册后才能投票");
            Response.End();
        }
        else
        {
            string VuserNum = "";
            if (!Foosun.Global.Current.IsTimeout())
            {
                VuserNum = Foosun.Global.Current.UserNum;
            }
            else
            {
                VuserNum = "guest";
            }
            if (sur.sel_IP() == 1)
            {
                Response.Write("抱歉，您得IP被限制不能投票");
                Response.End();
            }
            else 
            {
                if (dtp.Rows.Count > 0)
                {
                    timesy = DateTime.Parse(dtp.Rows[0]["VoteTime"].ToString());
                    TimeSpan st = nowtime - timesy;
                    if (st.Minutes < IPtime && dtp.Rows[0]["VoteIp"].ToString() == strvip && dtp.Rows[0]["UserNumber"].ToString() == VuserNum)
                    {
                        Response.Write("抱歉，请等待" + IPtime + "分钟后再进行投票");
                        Response.End();
                    }
                    else
                    {
                        for (int i = 0; i < TmpArr.Length; i++)
                        {
                            if (sur.Add_1(TmpArr[i], tid, othercontent, strvip, strvtime, VuserNum) == 0)
                            {
                                Response.Write("投票错误");
                                Response.End();
                            }
                        }
                        Response.Write("感谢您的投票");
                        Response.End();
                    }
                }
                else
                {
                    for (int i = 0; i < TmpArr.Length; i++)
                    {
                        if (sur.Add_1(TmpArr[i], tid, othercontent, strvip, strvtime, VuserNum) == 0)
                        {
                            Response.Write("投票错误");
                            Response.End();
                        }
                    }
                    Response.Write("感谢您的投票");
                    Response.End();
                }
            }
        }
    }
}