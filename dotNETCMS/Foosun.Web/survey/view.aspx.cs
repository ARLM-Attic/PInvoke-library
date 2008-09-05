///************************************************************************************************************
///**********投票系统Code By ChenZhaoHui****************************************************************************
///************************************************************************************************************
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
using System.ComponentModel;
using System.Drawing;
using System.Web.SessionState;
using Foosun.CMS;
using Foosun.CMS.Common;

public partial class vote_vview : Foosun.Web.UI.BasePage
{
    Survey sur = new Survey();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache"; //清除缓存
        if (!IsPostBack)                                               //判断页面是否重载
        {
          //                                         //判断用户是否登录
            copyright.InnerHtml = CopyRight;            //获取版权信息
        }
        showResult.InnerHtml = ShowResultt();
    }

    string ShowResultt()
    {
        //输出最终结果
        int tid = int.Parse(Request.QueryString["TID"]);
        decimal nowNum = 0;//当前票数(真实票数)
        decimal baseNum = 0;//基本票数(添加的时候的票数)
        decimal CountNum = 0;//总票数(相应ID的投票总数'包括真实票数和真实票数')
        string color = "";//显示颜色
        string titlename = "";//主题名
        string strlist = "";
        if (tid <= 0)
        {
            //PageError("参数错误", "");
            return "";
        }
        else
        {
            //取得主题名
            DataTable dtt = new DataTable();
            dtt = sur.sel_18(tid);

            #region titlename
            if (dtt != null)
            {
                if (dtt.Rows.Count > 0)
                {
                    titlename = dtt.Rows[0]["Title"].ToString();
                }
                else
                {
                    PageError("抱歉，参数错误", "");
                }
            }
            else
            {
                PageError("抱歉，参数错误", "");
            }
            #endregion
            DataTable dt = sur.sel_19(tid);

            strlist = "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"table\">\n";
            strlist += "<tr class=\"TR_BG_list\"><td width=100% class=\"list_link\" colspan=\"3\">主题：" + titlename + "</td></tr>\n";
            strlist += "</table>\n";

            #region 循环
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int iid = int.Parse(dt.Rows[i][0].ToString());//选项ID
                //查询相应ID的投票数(为投票的真实票数，除去了后台添加时赋给的投票数)

                baseNum = decimal.Parse(dt.Rows[i]["VoteCount"].ToString());//取得基本票数的值

                if (dt != null)//当前选项存在与否？(存在)
                {
                    if (sur.sel_20(tid, iid) != 0)//当前选项存在且有投票事件
                    {
                        nowNum = sur.sel_20(tid, iid);//当前票数(真实票数)
                       // CountNum = baseNum + nowNum;//取得总的投票数
                        CountNum = baseNum + sur.sel_21(tid);
                    }
                    else//存在选项，但是没有投票事件
                    {
                        nowNum = 0;//当前票数(真实票数)
                        //CountNum = baseNum + nowNum;//取得总的投票数
                        CountNum = baseNum + sur.sel_21(tid);
                    }
                }
                else//选项不存在
                {
                    nowNum = 0;//当前票数(真实票数)
                    baseNum = 0;//基本票数(添加的时候的票数)
                    CountNum = baseNum + nowNum;//取得总的投票数

                }

                color = dt.Rows[i]["DisColor"].ToString();//取得选项的颜色(仅限于文字颜色)

                string ItemName = dt.Rows[i]["ItemName"].ToString();//得到选项名称
                decimal decPercent = GetPercent(nowNum, CountNum) * 100;//得到百分比

                string strPercent = decPercent.ToString();//将百分比转为字符型

                if (strPercent.Length > 6)//如果百分比结果长度超过5位
                {
                    strPercent = strPercent.Substring(0, 6);//将百分比的余数截短为“00.000”
                }
                strlist += "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\">\n";
                strlist += "<tr class=\"TR_BG_list\"><td width=100 class=\"list_link\"><font color=#" + color + "><a title=\"所属主题:" + titlename +"\">" + ItemName + "</a></font></td><td width=50 class=\"list_link\"><a title=\"主题:" + titlename + "--选项:" + ItemName + "\"><font color=#" + color + ">" + nowNum.ToString() + "票</font></a></td><td class=\"list_link\"><img src=/sysImages/VoteIcon/vote.gif height=18 width=" + strPercent + "% alt='投票百分率为" + strPercent + "% \n" + CopyRight + "'>" + strPercent + "%</td></tr>\n";
                strlist += "</table>";
            }
            #endregion
            //Response.Write(strlist);
            return strlist;
        }
    }

    //百分比
    //nowNum：当前选项本身的票数；CountNum：所有的票数
    decimal GetPercent(decimal nowNum, decimal CountNum)
    {
        if (CountNum == 0)//如果总票数是零
        {
            CountNum++;//加一，避免除0出错
        }
        //计算百分数
        decimal decPercent = nowNum / CountNum;
        return decPercent;
    }

}
