///************************************************************************************************************
///**********广告统计Code By DengXi****************************************************************************
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
using System.Data.OleDb;

public partial class manage_advertisement_ad_stat : Foosun.Web.UI.ManagePage
{
    public manage_advertisement_ad_stat()
    {
        Authority_Code = "S006";
    }
    public string str_adsID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
        str_adsID = Foosun.Common.Input.checkID(Request.QueryString["adsID"]);
        string type=Request.QueryString["st"];
        switch (type)
        {
            case "hour":
                DivStat.InnerHtml = get24HourStat("1") + get24HourStat("0");
                break;
            case "day":
                DivStat.InnerHtml = getDayStat("1") + getDayStat("0");
                break;
            case "week":
                DivStat.InnerHtml = getWeekStat("1") + getWeekStat("0");
                break;
            case "month":
                DivStat.InnerHtml = getMonthStat("1") + getMonthStat("0");
                break;
            case "year":
                DivStat.InnerHtml = getYearStat();
                break;
            case "source":
                DivStat.InnerHtml = getSourceStat();
                break;
            default:
                DivStat.InnerHtml = get24HourStat("1") + get24HourStat("0");
                break;
        }
        getCororight(); 
    }

    /// <summary>
    /// 获取最近24小时统计
    /// </summary>
    /// <param name="type">当type值为1时,返回最近24小时统计,否则返回所有的24小时统计</param>
    /// <returns>返回最近24小时统计</returns>
    /// Code By DengXi

    protected string get24HourStat(string type)
    {
        int int_MaxVCount = 0;
        string str_VCount = "";
        int int_Chour = int.Parse(Convert.ToDateTime(System.DateTime.Now).ToString("HH"));
        string str_temp = "";

        for (int i = 0; i <= 23; i++)
        {
            str_VCount = str_VCount + "0" + ",";
        }
        
        str_VCount = Foosun.Common.Input.CutComma(str_VCount);
        string [] arr_VCount = str_VCount.Split(',');

        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        DataTable dt = ac.get24HourStat(type, str_adsID);

        if (dt != null)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                string str_Vtime = dt.Rows[j]["creatTime"].ToString();
                string str_Vhour = Convert.ToDateTime(str_Vtime).ToString("HH");
                for (int k = 0; k <= 23; k++)
                {
                    if (k == int.Parse(str_Vhour))
                    {
                        int int_tempstr = int.Parse(arr_VCount[k]) + 1;
                        arr_VCount[k] = int_tempstr.ToString();
                    }
                }
            }
            for (int l = 0; l <= 23; l++)
            {
                if (int.Parse(arr_VCount[l].ToString()) >= int_MaxVCount)
                {
                    int_MaxVCount = int.Parse(arr_VCount[l]);
                }
            }
        }
        string str_Vsize = "";
        for (int m = 0; m <= 23; m++)
        {
            if (int_MaxVCount != 0)
                str_Vsize = str_Vsize + (100 * int.Parse(arr_VCount[m]) / int_MaxVCount) + ",";
            else
                str_Vsize = str_Vsize + "0" + ",";
        }
        str_Vsize = Foosun.Common.Input.CutComma(str_Vsize);
        string [] arr_Vsize = str_Vsize.Split(',');

        string strhour1 = "100%";
        string strhour2 = "75%";
        string strhour3 = "50%";
        string strhour4 = "25%";
        string strhourName = "访问量24小时分配图表";

        if (type == "1")
        {
            strhour1 = int_MaxVCount + "次";

            if (int_MaxVCount > 3)
            {
                strhour2 = Math.Round(int_MaxVCount * 0.75, 0) + "次";
            }
            else if (int_MaxVCount > 1)
                strhour2 = (int_MaxVCount - 1) + "次";
            else
                strhour2 = "&nbsp;";

            if (int_MaxVCount > 3)
                strhour3 = Math.Round(int_MaxVCount * 0.5, 0) + "次";
            else if (int_MaxVCount > 2)
                strhour3 = (int_MaxVCount - 2) + "次";
            else
                strhour3 = "&nbsp;";

            if (int_MaxVCount > 3)
                strhour4 = Math.Round(int_MaxVCount * 0.25, 0) + "次";
            else
                strhour4 = "&nbsp;";

            strhourName = "最近24小时统计图表";
        }
        str_temp += "<table border=\"0\" align=\"center\" cellpadding=\"2\" width=\"98%\">";
        str_temp += "<tr><td align=\"left\"><img src=\"../../sysImages/folder/stat.gif\" border=\"0\" />" + strhourName + "</td>";
        str_temp += "</tr><tr><td align=\"center\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        str_temp += "<tr><td><table border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour1 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour2 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour3 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour4 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >0次</td></tr>";
        str_temp += " </table></td>";
        str_temp += " <td valign=\"bottom\">";

        if (type == "1")
        {
            str_temp += "<table align=\"center\" border=\"0\">";
            str_temp += "<tr valign=\"bottom\" >";
            for (int n = int_Chour + 1; n <= 23; n++)
            {
                str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"" + arr_Vsize[n] + "\" border=\"0\"><br />" + n + "</td>";
            }
            for (int o = 0; o <= int_Chour; o++)
            {
                str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"" + arr_Vsize[o] + "\" border=\"0\"><br />" + o + "</td>";
            }
        }
        else
        {
            str_temp += "<table align=\"center\" border=\"0\">";
            str_temp += "<tr valign=\"bottom\" >";
            str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"100\" border=\"0\"><br /> 总</td>";
            for (int p = 0; p <= 23; p++)
            {
                str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"" + arr_Vsize[p] + "\" border=\"0\"><br />" + p + "</td>";
            }
        }
        str_temp += "<td>单位(点)</td></tr></table>";
        str_temp += "</td></tr></table></td></tr></table>";
        return str_temp;
    }


    /// <summary>
    /// 获取日统计
    /// </summary>
    /// <param name="type">当type值为1时,返回当月日统计,否则返回所有的日统计</param>
    /// <returns>返回日统计信息</returns>
    /// Code By DengXi

    
    protected string getDayStat(string type)
    {
        int int_MaxVCount = 0;
        string str_VCount = "";
        int int_Cday = int.Parse(Convert.ToDateTime(System.DateTime.Now).ToString("dd"));
        string str_tempMD="";
        if (int.Parse(Convert.ToDateTime(System.DateTime.Now).ToString("mm")) == 1)
        {
            str_tempMD = getDayNum(int.Parse(Convert.ToDateTime(System.DateTime.Now).ToString("yy"))-1,12);
        }
        else
        {
            str_tempMD = getDayNum(int.Parse(Convert.ToDateTime(System.DateTime.Now).ToString("yy")),int.Parse(Convert.ToDateTime(System.DateTime.Now).ToString("mm"))-1);
        }
        string str_temp = "";
        for (int i = 1; i <= 32; i++)
        {
            str_VCount = str_VCount + "0" + ",";
        }
        str_VCount = Foosun.Common.Input.CutComma(str_VCount);
        string[] arr_VCount = str_VCount.Split(',');

        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        DataTable dt = ac.getDayStat(type, str_adsID, str_tempMD);


        if (dt != null)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                string str_Vtime = dt.Rows[j]["creatTime"].ToString();
                int str_Vday = int.Parse(Convert.ToDateTime(str_Vtime).ToString("dd"));
                for (int k = 0; k <= int.Parse(str_tempMD); k++)
                {
                    if (k == str_Vday)
                    {
                        int int_tempstr = int.Parse(arr_VCount[k].ToString()) + 1;
                        arr_VCount[k] = int_tempstr.ToString();
                    }
                }
                for (int l = 0; l <= int.Parse(str_tempMD); l++)
                {
                    if (int.Parse(arr_VCount[l].ToString()) >= int_MaxVCount)
                    {
                        int_MaxVCount = int.Parse(arr_VCount[l]);
                    }
                }
            }
        }

        string str_Vsize = "";
        for (int m = 0; m < 31; m++)
        {
            if (int_MaxVCount != 0)
                str_Vsize = str_Vsize + (100 * int.Parse(arr_VCount[m]) / int_MaxVCount) + ",";
            else
                str_Vsize = str_Vsize + "1" + ",";
        }
        str_Vsize = Foosun.Common.Input.CutComma(str_Vsize);
        string[] arr_Vsize = str_Vsize.Split(',');

        string strhour1 = "100%";
        string strhour2 = "75%";
        string strhour3 = "50%";
        string strhour4 = "25%";
        string strhourName = "访问量月统计分配图表";

        if (type == "1")
        {
            strhour1 = int_MaxVCount + "次";

            if (int_MaxVCount > 3)
            {
                strhour2 = Math.Round(int_MaxVCount * 0.75, 0) + "次";
            }
            else if (int_MaxVCount > 1)
                strhour2 = (int_MaxVCount - 1) + "次";
            else
                strhour2 = "&nbsp;";

            if (int_MaxVCount > 3)
                strhour3 = Math.Round(int_MaxVCount * 0.5, 0) + "次";
            else if (int_MaxVCount > 2)
                strhour3 = (int_MaxVCount - 2) + "次";
            else
                strhour3 = "&nbsp;";

            if (int_MaxVCount > 3)
                strhour4 = Math.Round(int_MaxVCount * 0.25, 0) + "次";
            else
                strhour4 = "&nbsp;";
            strhourName = "访问量31天分配图表";
        }
        str_temp += "<table border=\"0\" align=\"center\" cellpadding=\"2\" width=\"98%\">";
        str_temp += "<tr><td align=\"left\"><img src=\"../../sysImages/folder/stat.gif\" border=\"0\" />" + strhourName + "</td>";
        str_temp += "</tr><tr><td align=\"center\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        str_temp += "<tr><td><table border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour1 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour2 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour3 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour4 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >0次</td></tr>";
        str_temp += " </table></td>";
        str_temp += " <td valign=\"bottom\">";
        if (type == "1")
        {
            str_temp += "<table align=\"center\" border=\"0\">";
            str_temp += "<tr valign=\"bottom\" >";
            for (int n = int_Cday + 1; n < int.Parse(str_tempMD) + 1; n++)
            {
                str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"" + arr_Vsize[n] + "\" border=\"0\"><br />" + n + "</td>";
            }
            for (int o = 1; o <= int_Cday; o++)
            {
                str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"" + arr_Vsize[o] + "\" border=\"0\"><br />" + o + "</td>";
            }
        }
        else
        {
            str_temp += "<table align=\"center\" border=\"0\">";
            str_temp += "<tr valign=\"bottom\" >";
            str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"100\" border=\"0\"><br /> 总</td>";
            for (int p = 1; p <= int.Parse(str_tempMD); p++)
            {
                str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"" + arr_Vsize[p] + "\" border=\"0\"><br />" + p + "</td>";
            }
        }
        str_temp += "<td>单位(点)</td></tr></table>";
        str_temp += "</td></tr></table></td></tr></table>";
        return str_temp;
    }


    /// <summary>
    /// 获取版权信息
    /// </summary>
    /// <returns>返回版权信息</returns>
    /// Code By DengXi

    protected void getCororight()
    {
        DivCorpright.InnerHtml = "<table width=\"100%\" border=\"0\" cellpadding=\"8\" cellspacing=\"0\" class=\"copyright_bg\" style=\"height: 76px\" align=\"center\"><tr><td align=\"center\">" + CopyRight + "</td></tr></table>";
    }


    /// <summary>
    /// 取得当月的天数
    /// </summary>
    /// <param name="yearstr">年</param>
    /// <param name="daystr">月</param>
    /// <returns>返回当月天数</returns>
    /// Code By DengXi

    protected string getDayNum(int yearstr, int daystr)
    {
        string tempbigmonth = "1, 3, 5, 7, 8, 10, 12";
        string[] arr_month = tempbigmonth.Split(',');

        string daynum = "30";
        int temp = yearstr / 4;
        bool tf = true;
        bool tf1 = true;

        if (yearstr != (temp * 4))
            tf = false;
        for (int i = 0; i < arr_month.Length; i++)
        {
            if (daystr == int.Parse(arr_month[i].ToString()))
                break;
            else
                tf1 = false;
        }
        if (tf1)
        {
            daynum = "31";
        }
        else
        {
            if (daystr == 2)
            {
                if (tf)
                    daynum = "29";
                else
                    daynum = "28";
            }
            else
                daynum = "30";
        }
        return daynum;
    }


    /// <summary>
    /// 获取月统计
    /// </summary>
    /// <param name="type">当type值为1时,返回当年月统计,否则返回所有的月统计</param>
    /// <returns>返回月统计信息</returns>
    /// Code By DengXi


    protected string getMonthStat(string type)
    {
        int int_MaxVCount = 0;
        string str_VCount = "";
        int int_CMonth = int.Parse(Convert.ToDateTime(System.DateTime.Now).ToString("MM"));
        string str_temp = "";

        for (int i = 0; i <= 12; i++)
        {
            str_VCount = str_VCount + "0" + ",";
        }
        str_VCount = Foosun.Common.Input.CutComma(str_VCount);
        string[] arr_VCount = str_VCount.Split(',');

        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        DataTable dt = ac.getMonthStat(type, str_adsID);

        if (dt != null)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                string str_Vtime = dt.Rows[j]["creatTime"].ToString();
                string str_Vmonth = Convert.ToDateTime(str_Vtime).ToString("MM");
                for (int k = 0; k <= 12; k++)
                {
                    if (k == int.Parse(str_Vmonth))
                    {
                        int int_tempstr = int.Parse(arr_VCount[k]) + 1;
                        arr_VCount[k] = int_tempstr.ToString();
                    }
                }
            }
            for (int l = 0; l <= 12; l++)
            {
                if (int.Parse(arr_VCount[l].ToString()) >= int_MaxVCount)
                {
                    int_MaxVCount = int.Parse(arr_VCount[l]);
                }
            }
        }
        string str_Vsize = "";

        for (int m = 0; m <= 12; m++)
        {
            if (int_MaxVCount != 0)
                str_Vsize = str_Vsize + (100 * int.Parse(arr_VCount[m]) / int_MaxVCount) + ",";
            else
                str_Vsize = str_Vsize + "0" + ",";
        }
        str_Vsize = Foosun.Common.Input.CutComma(str_Vsize);
        string[] arr_Vsize = str_Vsize.Split(',');

        string strhour1 = "100%";
        string strhour2 = "75%";
        string strhour3 = "50%";
        string strhour4 = "25%";
        string strhourName = "访问量月分配图表";

        if (type == "1")
        {
            strhour1 = int_MaxVCount + "次";

            if (int_MaxVCount > 3)
            {
                strhour2 = Math.Round(int_MaxVCount * 0.75, 0) + "次";
            }
            else if (int_MaxVCount > 1)
                strhour2 = (int_MaxVCount - 1) + "次";
            else
                strhour2 = "&nbsp;";

            if (int_MaxVCount > 3)
                strhour3 = Math.Round(int_MaxVCount * 0.5, 0) + "次";
            else if (int_MaxVCount > 2)
                strhour3 = (int_MaxVCount - 2) + "次";
            else
                strhour3 = "&nbsp;";

            if (int_MaxVCount > 3)
                strhour4 = Math.Round(int_MaxVCount * 0.25, 0) + "次";
            else
                strhour4 = "&nbsp;";

            strhourName = "当月统计图表";
        }
        str_temp += "<table border=\"0\" align=\"center\" cellpadding=\"2\" width=\"98%\">";
        str_temp += "<tr><td align=\"left\"><img src=\"../../sysImages/folder/stat.gif\" border=\"0\" />" + strhourName + "</td>";
        str_temp += "</tr><tr><td align=\"center\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        str_temp += "<tr><td><table border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour1 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour2 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour3 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour4 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >0次</td></tr>";
        str_temp += " </table></td>";
        str_temp += " <td valign=\"bottom\">";

        if (type == "1")
        {
            str_temp += "<table align=\"center\" border=\"0\">";
            str_temp += "<tr valign=\"bottom\" >";

            for (int n = int_CMonth + 1; n <= 12; n++)
            {
                str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"" + arr_Vsize[n] + "\" border=\"0\"><br />" + n + "</td>";
            }
            for (int o = 1; o <= int_CMonth; o++)
            {
                str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"" + arr_Vsize[o] + "\" border=\"0\"><br />" + o + "</td>";
            }
        }
        else
        {
            str_temp += "<table align=\"center\" border=\"0\">";
            str_temp += "<tr valign=\"bottom\" >";
            str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"100\" border=\"0\"><br /> 总</td>";
            for (int p = 1; p <= 12; p++)
            {
                str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"" + arr_Vsize[p] + "\" border=\"0\"><br />" + p + "</td>";
            }
        }
        str_temp += "<td>单位(点)</td></tr></table>";
        str_temp += "</td></tr></table></td></tr></table>";
        return str_temp;
    }


    /// <summary>
    /// 获取年统计
    /// </summary>
    /// <returns>返回年统计信息</returns>
    /// Code By DengXi

    protected string getYearStat()
    {
        int int_MaxVCount = 0;
        int int_CMonth = int.Parse(Convert.ToDateTime(System.DateTime.Now).ToString("MM"));

        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        DataTable dt = ac.getYearStat(str_adsID);

        if (dt != null)
        {
            int_MaxVCount = dt.Rows.Count;
        }
        //string strhour1 = "100%";
        //string strhour2 = "75%";
        //string strhour3 = "50%";
        //string strhour4 = "25%";
        string strhourName = "当年统计图表";
        string str_temp = "";
        str_temp += "<table border=\"0\" align=\"center\" cellpadding=\"2\" width=\"98%\">";
        str_temp += "<tr><td align=\"left\"><img src=\"../../sysImages/folder/stat.gif\" border=\"0\" />" + strhourName + "</td>";
        str_temp += "</tr><tr><td align=\"center\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        str_temp += "<tr><td><table border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >100%</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >75%</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >50%</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >25%</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >0次</td></tr>";
        str_temp += " </table></td>";
        str_temp += " <td valign=\"bottom\">";

        str_temp += "<table align=\"center\" border=\"0\">";
        str_temp += "<tr valign=\"bottom\" >";
        str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"100\" border=\"0\"><br /> 总</td>";
        str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"" + int_MaxVCount + "\" border=\"0\"><br />" + int_MaxVCount + "</td>";
        str_temp += "<td>单位(点)</td></tr></table>";
        str_temp += "</td></tr></table></td></tr></table>";
        return str_temp;    
    }

    /// <summary>
    /// 获取周统计
    /// </summary>
    /// <param name="type">当type值为1时,返回当周统计,否则返回所有的周统计</param>
    /// <returns>返回周统计信息</returns>
    /// Code By DengXi

    protected string getWeekStat(string type)
    {
        int int_MaxVCount = 0;
        string str_VCount = "";
        int int_temp1 = int.Parse(Convert.ToDateTime(System.DateTime.Now).ToString("yyyy"));
        int int_temp2 = int.Parse(Convert.ToDateTime(System.DateTime.Now).ToString("mm"));
        int int_temp3 = int.Parse(Convert.ToDateTime(System.DateTime.Now).ToString("dd"));
        int int_Cweek = CaculateWeekDay(int_temp1, int_temp2, int_temp3);
        string str_temp = "";
        for (int i = 0; i <= 7; i++)
        {
            str_VCount = str_VCount + "0" + ",";
        }
        str_VCount = Foosun.Common.Input.CutComma(str_VCount);
        string[] arr_VCount = str_VCount.Split(',');

        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        DataTable dt = ac.getWeekStat(type, str_adsID);

        if (dt != null)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                string str_Vtime = dt.Rows[j]["creatTime"].ToString();
                int int_temp4 = int.Parse(Convert.ToDateTime(str_Vtime).ToString("yyyy"));
                int int_temp5 = int.Parse(Convert.ToDateTime(str_Vtime).ToString("mm"));
                int int_temp6 = int.Parse(Convert.ToDateTime(str_Vtime).ToString("dd"));
                int str_Vweek = CaculateWeekDay(int_temp4,int_temp5,int_temp6);
                for (int k = 0; k <= 7; k++)
                {
                    if (k == str_Vweek)
                    {
                        int int_tempstr = int.Parse(arr_VCount[k]) + 1;
                        arr_VCount[k] = int_tempstr.ToString();
                    }
                }
            }
            for (int l = 0; l <= 7; l++)
            {
                if (int.Parse(arr_VCount[l].ToString()) >= int_MaxVCount)
                {
                    int_MaxVCount = int.Parse(arr_VCount[l]);
                }
            }
        }
        string str_Vsize = "";

        for (int m = 0; m <= 7; m++)
        {
            if (int_MaxVCount != 0)
                str_Vsize = str_Vsize + (100 * int.Parse(arr_VCount[m]) / int_MaxVCount) + ",";
            else
                str_Vsize = str_Vsize + "0" + ",";
        }
        str_Vsize = Foosun.Common.Input.CutComma(str_Vsize);
        string[] arr_Vsize = str_Vsize.Split(',');

        string strhour1 = "100%";
        string strhour2 = "75%";
        string strhour3 = "50%";
        string strhour4 = "25%";
        string strhourName = "访问量周分配图表";

        if (type == "1")
        {
            strhour1 = int_MaxVCount + "次";

            if (int_MaxVCount > 3)
            {
                strhour2 = Math.Round(int_MaxVCount * 0.75, 0) + "次";
            }
            else if (int_MaxVCount > 1)
                strhour2 = (int_MaxVCount - 1) + "次";
            else
                strhour2 = "&nbsp;";

            if (int_MaxVCount > 3)
                strhour3 = Math.Round(int_MaxVCount * 0.5, 0) + "次";
            else if (int_MaxVCount > 2)
                strhour3 = (int_MaxVCount - 2) + "次";
            else
                strhour3 = "&nbsp;";

            if (int_MaxVCount > 3)
                strhour4 = Math.Round(int_MaxVCount * 0.25, 0) + "次";
            else
                strhour4 = "&nbsp;";

            strhourName = "当周统计图表";
        }
        str_temp += "<table border=\"0\" align=\"center\" cellpadding=\"2\" width=\"98%\">";
        str_temp += "<tr><td align=\"left\"><img src=\"../../sysImages/folder/stat.gif\" border=\"0\" />" + strhourName + "</td>";
        str_temp += "</tr><tr><td align=\"center\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        str_temp += "<tr><td><table border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour1 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour2 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour3 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >" + strhour4 + "</td></tr>";
        str_temp += "<tr><td height=\"25\" valign=\"top\" align=\"right\" >0次</td></tr>";
        str_temp += " </table></td>";
        str_temp += " <td valign=\"bottom\">";

        if (type == "1")
        {
            str_temp += "<table align=\"center\" border=\"0\">";
            str_temp += "<tr valign=\"bottom\" >";

            for (int n = int_Cweek + 1; n <= 7; n++)
            {
                str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"" + arr_Vsize[n] + "\" border=\"0\"><br />" + n + "</td>";
            }
            for (int o = 1; o <= int_Cweek; o++)
            {
                str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"" + arr_Vsize[o] + "\" border=\"0\"><br />" + o + "</td>";
            }
        }
        else
        {
            str_temp += "<table align=\"center\" border=\"0\">";
            str_temp += "<tr valign=\"bottom\" >";
            str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"100\" border=\"0\"><br /> 总</td>";
            for (int p = 1; p <= 7; p++)
            {
                str_temp += "<td width=\"15\" align=\"center\" background=\"../../sysImages/StatIcon/tu_back.gif\"><img src=\"../../sysImages/StatIcon/tu.gif\" width=\"15\" height=\"" + arr_Vsize[p] + "\" border=\"0\"><br />" + p + "</td>";
            }
        }
        str_temp += "<td>单位(点)</td></tr></table>";
        str_temp += "</td></tr></table></td></tr></table>";
        return str_temp;
    }

    /// <summary>
    /// 获取是周几
    /// </summary>
    /// <param name="y">年</param>
    /// <param name="m">月</param>
    /// <param name="d">日</param>
    /// <returns>返回周几</returns>
    /// Code By DengXi

    int CaculateWeekDay(int y, int m, int d)
    {
        if (m == 1) m = 13;
        if (m == 2) m = 14;
        int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7;
        int weekstr = 1;
        switch (week)
        {
            case 1: weekstr = 1; break;
            case 2: weekstr = 2; break;
            case 3: weekstr = 3; break;
            case 4: weekstr = 4; break;
            case 5: weekstr = 5; break;
            case 6: weekstr = 6; break;
            case 7: weekstr = 7; break;
        }
        return weekstr;
    }


    /// <summary>
    /// 来源统计
    /// </summary>
    /// <returns>返回来源统计</returns>
    /// Code By DengXi

    protected string getSourceStat()
    {
        updb();
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        DataTable dt = ac.getSourceStat(str_adsID);

        string str_Tempstr = "";
        string curPage = Request.QueryString["page"];    //当前页码
        int pageSize = 20, page = 0;                     //每页显示数

        if (curPage == "" || curPage == null || curPage == string.Empty) { page = 1; }
        else
        {
            try { page = int.Parse(curPage); }
            catch (Exception e)
            {
                PageError("参数错误！<li>" + e + "</li>", "");
            }
        }
        str_Tempstr += "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"4\" cellspacing=\"1\" bgcolor=\"#FFFFFF\" class=\"table\">";
        str_Tempstr += "<tr class=\"TR_BG\">";
        str_Tempstr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">地区</td>";
        str_Tempstr += "<td align=\"left\" valign=\"middle\" class=\"sys_topBg\">点击次数</td>";
        str_Tempstr += "</tr>";
        int Cnt = 0;
        int pageCount = 0;
        if (dt != null)
        {
            int i, j;
            Cnt = dt.Rows.Count;

            //获得当前分页数-----------------------------------------------------
            pageCount = Cnt / pageSize;
            if (Cnt % pageSize != 0) { pageCount++; }

            if (page > pageCount) { page = pageCount; }
            if (page < 1) { page = 1; }

            for (i = (page - 1) * pageSize, j = 1; i < Cnt && j <= pageSize; i++, j++)
            {
                str_Tempstr += "<tr class=\"TR_BG_list\">";
                str_Tempstr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + dt.Rows[i]["Address"].ToString() + "</td>";
                str_Tempstr += "<td align=\"left\" valign=\"middle\" height=\"20\">" + dt.Rows[i]["Ipnum"].ToString() + "</td>";
                str_Tempstr += "</tr>";
            }
        }
        string url = "ad_stat.aspx?st=source&adsID=" + str_adsID + "&page=";
        str_Tempstr += "<tr class=\"TR_BG_list\" align=\"right\"><td colspan=\"2\">" + ShowPage(page, pageSize, Cnt, url, pageCount) + "</td></tr>";
        str_Tempstr += "</table>";

        return str_Tempstr;
    }

    /// <summary>
    /// 取得分页
    /// </summary>
    /// <param name="page">当前页</param>
    /// <param name="pageSize">一页显示多少条</param>
    /// <param name="Cnt">总记录数</param>
    /// <param name="url">链接地址</param>
    /// <param name="pageCount">分页总数</param>
    /// <returns>返回分页</returns>
    /// Code By DengXi

    protected string ShowPage(int page, int pageSize, int Cnt, string url, int pageCount)
    {
        string urlstr = "共" + Cnt.ToString() + "条记录,共" + pageCount.ToString() + "页,当前第" + page.ToString() + "页   ";
        urlstr = urlstr + "<a href=\"" + url + "1)\" title=\"首页\" class=\"list_link\">首页</a> ";
        if ((page - 1) < 1)
            urlstr = urlstr + " <a href=\"" + url + "1)\" title=\"上一页\" class=\"list_link\">上一页</a> ";
        else
            urlstr = urlstr + " <a href=\"" + url + (page - 1) + "\" title=\"上一页\" class=\"list_link\">上一页</a> ";
        if ((page + 1) < pageCount)
            urlstr = urlstr + " <a href=\"" + url + (page + 1) + "\" title=\"下一页\" class=\"list_link\">下一页</a> ";
        else
            urlstr = urlstr + " <a href=\"" + url + pageCount + "\" title=\"下一页\" class=\"list_link\">下一页</a> ";
        urlstr = urlstr + " <a href=\"" + url + pageCount + "\" title=\"尾页\" class=\"list_link\">尾页</a> ";
        return urlstr;
    }

    /// <summary>
    /// 获取IP地址对应的实际地址
    /// </summary>
    /// <param name="ip">IP地址</param>
    /// <returns>返回IP地址对应的实际地址</returns>
    /// Code By DengXi

    protected string Get_Address(string ip)
    { 
        int num=0;
        string [] arr_ip = ip.Split('.');
        string str_address = "";
        int int_temp1 = int.Parse(arr_ip[0].ToString()) * 256 * 256 * 256;
        int int_temp2 = int.Parse(arr_ip[1].ToString()) * 256 * 256;
        int int_temp3 = int.Parse(arr_ip[2].ToString()) * 256;
        int int_temp4 = int.Parse(arr_ip[3].ToString()) - 1;


        num = int_temp1 + int_temp2 + int_temp3 + int_temp4;
        string sql = "select Country from Address where StarIP <=" + num + " and EndIP >=" + num + "";
        DataTable dt = CipDb(sql);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
                str_address = dt.Rows[0][0].ToString();
            dt.Clear();
            dt.Dispose();
        }
        return str_address;
    }

    /// <summary>
    /// 联接IP地址数据库并且执行SQL语句
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <returns>返回数据表</returns>
    /// Code By DengXi

    protected DataTable CipDb(string sql)
    {
        OleDbConnection conn = new OleDbConnection();
        string str_dirMana = Foosun.Config.UIConfig.dirDumm;//获取用户虚拟路径
        if (str_dirMana != "" && str_dirMana != null && str_dirMana != string.Empty)
        {
            str_dirMana = "//" + str_dirMana;
        }
        conn.ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;data source=" + Server.MapPath(str_dirMana + "/database/AddressIp.mdb") + "";
        try
        {
            conn.Open();
        }
        catch (OleDbException e)
        {
            PageError("打开IP地址数据库失败!失败原因:" + e.ToString(), "");
        }
        OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
        DataTable dt =null;
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        try
        {
            dt = ds.Tables["table"];
        }
        catch (Exception ee)
        {
            PageError("未知错误!错误原因:" + ee.ToString(), "");
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return dt;
    }


    /// <summary>
    /// 写地区进数据库
    /// </summary>
    /// <returns>写地区进数据库</returns>
    /// Code By DengXi

    protected void updb()
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        DataTable dt = ac.getDbNull() ;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string stradress = Get_Address(dt.Rows[i]["IP"].ToString());
                    ac.upStat(stradress, dt.Rows[i]["ID"].ToString());
                }
            }
            dt.Clear();
            dt.Dispose();
        }    
    }
}

