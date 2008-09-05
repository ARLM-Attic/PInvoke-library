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
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.SessionState;
using Foosun.CMS;

public partial class stat_stat : Foosun.Web.UI.BasePage
{
    Stat sta = new Stat();
    #region 定义public参数变量
    public static DataView dv;//公用视图
    public string str_fs_url;//前台地址
    public string code;//前台显示样式(滚动,图标,文本)

    #region 统计参数
    public string str_fs_page;//定义被访问页参数变量
    public string str_fs_ip;//定义IP参数变量
    public string str_fs_come;//定义来路统计参数变量

    public int str_fs_year;//年 
    public int str_fs_month;//月
    public int str_fs_day;//日
    public int str_fs_hour;//时
    public int str_fs_week;//周
    public string str_fs_time;//具体统计时间

    public string str_fs_soft="Other";//浏览器(默认为Other)
    public string str_fs_Os = "Other";//操作系统(默认为Other)
    public int str_fs_width = 800;//屏幕宽度(默认宽度为800)

    public string str_fs_country = "未知区域";//地区(大)
    public string str_fs_city = "";//(小)

    public int str_fs_online_person;//在线人数
    public int str_fs_IPtimeResh;//IP防刷新时间
    public string str_fs_sysenglish_name;//站点英文名
    public string fs_isipcheck;//IP防刷新开启？
    public string fs_isonline;//在线统计开启？

    public int str_fs_today;//今日统计数
    public int str_fs_yesterday;//昨日统计数
    public string str_fs_content_data;//综合统计时间
    public int str_fs_all;//总数
    public int str_fs_heigh;//最高数
    public string str_fs_heightime;//最高时的时间

    public int str_fs_user_visit;//用户本人的访问量
    public string str_fs_outstr = string.Empty;//输出字符串

    public string isDataBase = string.Empty;//是否是独立的数据库
    public string OnlyBaseConn = string.Empty;//独立数据库连接路径
    public string fs_stat_pram = string.Empty;//统计参数表
    public string fs_stat_info = string.Empty;//统计信息表
    public string fs_stat_content = string.Empty;//统计综合表
    public string fs_stat_class = string.Empty;//统计分类表
    #endregion
    #endregion

    #region 取得服务器变量集合
    System.Collections.Specialized.NameValueCollection ServerVariables = System.Web.HttpContext.Current.Request.ServerVariables;
    #endregion
    /// <summary>
    /// 页面载入时即开始进行条件统计
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    private void Page_Load(object sender, System.EventArgs e)
    {
        Response.CacheControl = "no-cache"; //设置页面无缓存
        if (!IsPostBack){}

        #region 网页立即超时，防止漏统计
        Response.Expires = 0;
        #endregion

        /// <summary>
        /// 下面为开始进行各项数据的统计
        /// </summary>
        /// code by chenzhaohui
        
        #region 下面为开始进行各项数据的统计

        #region 获取页面来源（页面地址）
        if (ServerVariables["HTTP_REFERER"] != null)
        {
            str_fs_page = ServerVariables["HTTP_REFERER"].ToString();
        }
        else
        {
            str_fs_page = string.Empty;
        }
        #endregion

        #region 取指定长度信息
        if (str_fs_page != string.Empty && str_fs_page.Substring(str_fs_page.Length - 1, 1) == "/") str_fs_page = str_fs_page.Substring(0, str_fs_page.Length - 1);
        #endregion

        #region 从参数表里读出数据
        DataTable dt = sta.sel_stat_Param(); 
        dv = dt.DefaultView;
        str_fs_sysenglish_name = dv[0].Row["SystemNameE"].ToString();//站点英文名称
        str_fs_IPtimeResh = int.Parse(dv[0].Row["ipTime"].ToString());//IP防刷新时间
        fs_isipcheck = dv[0].Row["ipCheck"].ToString();//IP防刷新开启?
        fs_isonline = dv[0].Row["isOnlinestat"].ToString();//在线统计开启?

        #endregion

        #region 判断页面是否可以进行统计
        bool fs_ok_stat = true;
        if (fs_ok_stat)
        {
            /// <summary>
            /// 记录相关数据
            /// </summary>
            /// code by chenzhaohui

            #region 记录相关数据

            #region 取得访问者的ip信息值
            str_fs_ip = Request.ServerVariables["REMOTE_ADDR"];
            #endregion

            #region 从mystat里取得传递过来的来路值
            if (Request.QueryString["come"] != null)
            {
                str_fs_come = Request.QueryString["come"].ToString();
            }
            else
            {
                str_fs_come = string.Empty;
            }
            if (str_fs_come != string.Empty && str_fs_come.Substring(str_fs_come.Length - 1, 1) == "/") str_fs_come = str_fs_come.Substring(0, str_fs_come.Length - 1);
            #endregion

            #region 时间----记录各种访问时间值(年，月，日，小时，具体访问时间[2007-3-30]，周)

            str_fs_year = int.Parse(DateTime.Now.AddHours(0).Year.ToString());//年
            str_fs_month = int.Parse(DateTime.Now.AddHours(0).Month.ToString());//月
            str_fs_day = int.Parse(DateTime.Now.AddHours(0).Day.ToString());//日
            str_fs_hour = int.Parse(DateTime.Now.AddHours(0).Hour.ToString());//时
            str_fs_week = (int)DateTime.Now.AddHours(0).DayOfWeek;//周
            str_fs_time = DateTime.Now.AddHours(0).ToString();//具体时间

            #endregion

            #region 需要传递用户HTTP设备的变量,如:你使用的IE浏览器版本,还有你的机器名和操作系统......

            string useragent = ServerVariables["HTTP_USER_AGENT"].ToString();

            #endregion

            #region 将各类浏览器放入数组进行传值管理等

            string[,] arvsoft = new string[,] { { "NetCaptor", "NetCaptor" }, { "MSIE 6", "MSIE 6.x" }, { "MSIE 5", "MSIE 5.x" }, { "MSIE 4", "MSIE 4.x" }, { "Netscape", "Netscape" }, { "Opera", "Opera" } };
            #endregion
            
            #region 获取浏览器的值
            for (int i = 0; i < 6; i++)
            {
                if (useragent.IndexOf(arvsoft[i, 0].ToString()) > 0)
                {
                    str_fs_soft = arvsoft[i, 1];
                    break;
                }
            }
            #endregion

            #region 将各类操作系统放入数组进行传值管理等
            string[,] arvos = new string[,] { { "Windows NT 5.0", "Win2k" }, { "Windows NT 5.1", "WinXP" }, { "Windows NT 5.2", "Win2k3" }, { "Windows NT", "WinNT" }, { "Windows 9", "Win9x" }, { "unix", "类Unix" }, { "linux", "类Unix" }, { "SunOS", "类Unix" }, { "BSD", "类Unix" }, { "Mac", "Mac" } };
            #endregion
            #region 获取操作系统的值
            for (int i = 0; i < 10; i++)
            {
                if (useragent.IndexOf(arvos[i, 0].ToString()) > 0)
                {
                    str_fs_Os = arvos[i, 1];
                    break;
                }
            }
            #endregion

            #region 屏幕宽度screen.width

            #region 取得参数传递传过来的值screenwidth
            if (Request.QueryString["width"] != null)
            {
                str_fs_width = int.Parse(Request.QueryString["width"].ToString());
            }
            #endregion
            #endregion

            /// <summary>
            /// 访问者所在地区,从数据表读取IP信息
            /// </summary>
            /// code by chenzhaohui

            #region 访问者所在地区,从数据表读取IP信息

                DataView dv1 = new DataView();
                long str_fs_ipnow = str_fs_GetIP(str_fs_ip);              
                string Sqll = "";
                Sqll = "select * from Address where StarIP<=" + str_fs_ipnow + " and EndIP>=" + str_fs_ipnow + "";
                DataTable dt_stat = stat_AcDb(Sqll);

                if (dt_stat != null && dt_stat.Rows.Count > 0)
                {
                    str_fs_country = dt_stat.Rows[0]["Country"].ToString();
                    str_fs_city = dt_stat.Rows[0]["City"].ToString();
                }
                
                #region 执行数据操作语句，返回更新数据,提交对数据的更改
                #endregion              
            #endregion

            #endregion

            /// <summary>
            /// 检查是否属于刷新
            /// </summary>
            /// code by chenzhaohui

            #region 检查是否属于刷新
            if (fs_isipcheck == "1")//防刷新处于开启状态
            {
                Response.AddHeader("Refresh", "" + str_fs_IPtimeResh + ""); 
            }
            #endregion

            #region 统计在线人数(根据IP数来统计)
            DateTime newtime = DateTime.Now.AddHours(0).AddMinutes(0);

            #region 执行数据，返回更新数据
            DataTable dtp = sta.sel_vip_1(newtime);
            dv1 = dtp.DefaultView;
            dv1.Table.AcceptChanges();

            #region 取得在线人数
            str_fs_online_person = dv1.Count;
            #endregion

            #endregion

            dv1.Dispose();//释放资源
            #endregion


            #region 读写详细数据库,往统计表里写数据(如果在线统计开启，则可以写数据，否则不能写数据进数据库)
            if (fs_isonline == "1")//在线统计开启状态
            {
                string statid = "";
                if (Request.QueryString["statid"] != null)
                {
                    statid = Request.QueryString["statid"];//取得传递过来的统计ID号
                }
                Foosun.Model.stat_Info info = new Foosun.Model.stat_Info();

                info.vyear = str_fs_year;
                info.vmonth = str_fs_month;
                info.vday = str_fs_day;
                info.vhour = str_fs_hour;
                info.vtime = str_fs_time;
                info.vweek = str_fs_week;
                info.vip=str_fs_ip;
                info.vwhere = str_fs_country;
                info.vwheref = str_fs_city;
                info.vcome = str_fs_come;
                info.vpage = str_fs_page;
                info.vsoft = str_fs_soft;
                info.vOS = str_fs_Os;
                info.vwidth = str_fs_width;
                info.classid = statid;
                info.SiteID = Foosun.Global.Current.SiteID;
                //info.SiteID = "0";
                #region 执行数据操作语句，返回更新数据
                sta.Add(info);
                #endregion
            }
           
            #endregion

            #region 读写数据库,该数据库为整站的综合统计信息
            if (fs_isonline == "1")
            {
                string statidz = Request.QueryString["statid"];
                DataTable dtsa = sta.sel_stat_content(statidz);
                dv1 = dtsa.DefaultView;//先查询表中是否有数据存在，否则就对相应的ID号的综合统计信息进行数据增加
                dv1.Table.AcceptChanges();//提交对数据的更改
                if (dv1.Count == 0)
                {
                    dv1.Dispose();//释放资源
                    string vdatee = DateTime.Now.AddHours(0).Date.ToShortDateString();
                    string starttimee = DateTime.Now.AddHours(0).ToString();
                    string highttimee = DateTime.Now.AddHours(0).Date.ToShortDateString();

                    #region 向数据表中插入的初始数据
                    sta.Add_1(vdatee, starttimee, highttimee, statidz, Foosun.Global.Current.SiteID);//数据语句，返回数据表中
                    #endregion
                }
                else
                {
                    str_fs_today = int.Parse(dv1[0].Row["today"].ToString());
                    str_fs_yesterday = int.Parse(dv1[0].Row["yesterday"].ToString());
                    str_fs_content_data = dv1[0].Row["vdate"].ToString();
                    str_fs_all = int.Parse(dv1[0].Row["vtop"].ToString());
                    str_fs_heigh = int.Parse(dv1[0].Row["vhigh"].ToString());
                    str_fs_heightime = dv1[0].Row["vhightime"].ToString();
                    string strclassid = dv1[0].Row["classid"].ToString();
                    
                    dv1.Dispose();//释放资源

                    #region 更新综合表数据

                    int intdatecha = (int)(DateTime.Now.AddHours(0).Subtract(DateTime.Parse(str_fs_content_data)).TotalDays);
                    switch (intdatecha)
                    {
                        //上条记录是今天的
                        case 0:
                            str_fs_today += 1;//今天+1
                            break;

                        //上条记录是昨天的
                        case 1:
                            str_fs_yesterday = str_fs_today;//今天值->昨天
                            str_fs_today = 1;
                            str_fs_content_data = DateTime.Now.AddHours(0).Date.ToShortDateString();
                            break;

                        //上条记录是若干天前的
                        default:
                            str_fs_yesterday = 0;
                            str_fs_today = 1;
                            str_fs_content_data = DateTime.Now.AddHours(0).Date.ToShortDateString();
                            break;
                    }

                    if (str_fs_today > str_fs_heigh)
                    {
                        str_fs_heigh = str_fs_today;
                        str_fs_heightime = DateTime.Now.AddHours(0).Date.ToShortDateString();
                    }

                    str_fs_all += 1;

                    #endregion

                    #region 对指定的数据表进行更新

                    sta.Update(str_fs_today, str_fs_yesterday, str_fs_content_data, str_fs_all, str_fs_heigh, str_fs_heightime, strclassid, Foosun.Global.Current.SiteID, strclassid); 
                    #endregion

                }

            }

            #region 读出数据(此数据为统计系统统计出来的数据的一个综合统计，如:今日统计，最高统计，本月的等等)
            string statidz1 = Request.QueryString["statid"];
            DataTable dte = sta.sel_stat_content(statidz1);
            dv1 = dte.DefaultView;

            int str_fs_today1 = int.Parse(dv1[0].Row["today"].ToString());
            int str_fs_yesterday1 = int.Parse(dv1[0].Row["yesterday"].ToString());
            int str_fs_all1 = int.Parse(dv1[0].Row["vtop"].ToString());
            int inthigh1 = int.Parse(dv1[0].Row["vhigh"].ToString());
            string strhightime1 = DateTime.Parse(dv1[0].Row["vhightime"].ToString()).Date.ToShortDateString();

            dv1.Dispose();//释放资源

            #endregion

            #region 读写COOKIE，得到用户浏览量

            //bug修改  周峻平 2008-6-3
            System.Web.HttpCookie readcookie1 = Request.Cookies["str_fs_old"];
            if (readcookie1 != null && readcookie1.Value != null)
            {
                str_fs_user_visit = int.Parse(readcookie1.Value.ToString()) + 1;
            }
            else
            {
                str_fs_user_visit = 1;
            }

            HttpCookie cookie = new HttpCookie("str_fs_old");
            cookie.Value = str_fs_user_visit.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);

            #endregion

            #endregion

            /// <summary>
            /// 前台调用
            /// </summary>
            /// code by chenzhaohui

            #region 前台调用程序及图像文件路径-从stat.aspx中传递参数style,url等，调用该统计
           
            code = Request.QueryString["code"].ToString();

            string Str_dirMana = Foosun.Config.UIConfig.dirDumm;//虚拟目录
            
            if (Str_dirMana != "" && Str_dirMana != null && Str_dirMana != string.Empty)//判断虚拟路径是否为空
            {
                Str_dirMana = @"/" + Str_dirMana;
            }
            else
            {
                Str_dirMana = "";
            }
            #region 取得当前域名地址
            str_fs_url = "http://" + ServerVariables["HTTP_HOST"].ToString() + str_fs_url + Str_dirMana;
            #endregion

            /// <summary>
            /// 根据传递的参数得出统计的样式(marquee,pic,text)
            /// </summary>
            /// <param name="code">前台样式</param>
            /// code by chenzhaohui

            #region 输出
            switch (code)
            {
                case "1":		//LOGO滚动样式的统计方式，滚动方式显示统计到的相关信息
                    str_fs_outstr = "<marquee loop='-1' behavior='alternate' scrollDelay='1' scrollAmount='3' style='font-size: 12px; line-height=15px' onMouseOut='this.start();' onMouseOver='this.stop();'>";
                    str_fs_outstr += "<font face='Arial, Verdana, san-serif' color='#407526'>总量: " + str_fs_all1 + "&nbsp;最高访问量: " + inthigh1 + "&nbsp;最高访问量日期: " + strhightime1 + "&nbsp;今日访问: " + str_fs_today1 + " &nbsp;昨日访问: " + str_fs_yesterday1 + " &nbsp;您的访问量: " + str_fs_user_visit;
                    if (int.Parse(dv[0].Row["isOnlinestat"].ToString()) == 1) str_fs_outstr += " &nbsp;在线人数: " + str_fs_online_person;
                    str_fs_outstr += "</font></marquee>";
                    break;

                case "2":		//ICON显示一小图标，鼠标放上去自动显示出系统统计到的相关信息
                    str_fs_outstr = "<a href='http://www.foosun.net' title='" + dv[0].Row["SystemName"].ToString() + "\\n总量: " + str_fs_all1 + "\\n最高访问量: " + inthigh1 + "\\n最高访问量日期: " + strhightime1 + "\\n今日访问: " + str_fs_today1 + "\\n昨日访问: " + str_fs_yesterday1 + "\\n您的访问量: " + str_fs_user_visit;
                    if (int.Parse(dv[0].Row["isOnlinestat"].ToString()) == 1) str_fs_outstr += "\\n在线人数: " + str_fs_online_person;
                    str_fs_outstr += "' target='_blank'><img border='0' src='" + str_fs_url + "/sysImages/folder/stat.gif'></a>";
                    break;

                case "0":		//TEXT文本方式显示总数，统计方式比较单一简单。
                    str_fs_outstr = str_fs_all1.ToString();
                    str_fs_outstr = "总量:" + str_fs_all1.ToString() + ",最高日期:" + strhightime1 + ",今日:" + str_fs_today1 + ",昨日:" + str_fs_yesterday1 + ",您的访问量:" + str_fs_user_visit + "";
                    break;
                default:        //默认
                    str_fs_outstr = "<a href='http://www.foosun.net' title='" + dv[0].Row["SystemName"].ToString() + "\\n总量: " + str_fs_all1 + "\\n最高访问量: " + inthigh1 + "\\n最高访问量日期: " + strhightime1 + "\\n今日访问: " + str_fs_today1 + "\\n昨日访问: " + str_fs_yesterday1 + "\\n您的访问量: " + str_fs_user_visit;
                    if (int.Parse(dv[0].Row["isOnlinestat"].ToString()) == 1) str_fs_outstr += "\\n在线人数: " + str_fs_online_person;
                    str_fs_outstr += "' target='_blank'><img border='0' src='" + str_fs_url + "/sysImages/folder/stat.gif'></a>";
                    break;
            }

            //输出,统计数据
            //Response.Expires = DateAdd("n", str_fs_IPtimeResh, now());
            Response.Write("document" + "." + "write(\"" + str_fs_outstr + "\")");
            #endregion
            #endregion
        }
        #endregion

        else	//检查是否非法调用
        {
            PageError("意外错误：未知错误", "shortcut_list.aspx");
        }
        #endregion

    }

    /// <summary>
    /// 取IP
    /// </summary>
    /// <param name="fs_ip"></param>
    /// <returns></returns>

    public long str_fs_GetIP(string fs_ip)
    {
        string[] fs_streachip = fs_ip.Split('.');
        long str_fs_intip = 0;
        for (int i = 0; i < 4; i++)
        {
            str_fs_intip += (long)(int.Parse(fs_streachip[i]) * System.Math.Pow(256, 3 - i));
        }
        return str_fs_intip;
    }

    /// <summary>
    /// 连接IP地址数据库并执行SQL语句
    /// </summary>
    /// <param name="sql">SQL语句</param>
    /// <returns>返回数据表</returns>

    protected DataTable stat_AcDb(string sql)
    {
        OleDbConnection conn = new OleDbConnection();
        string str_dirMana = Foosun.Config.UIConfig.dirDumm;//获取用户虚拟路径
        if (str_dirMana != "" && str_dirMana != null && str_dirMana != string.Empty)
        {
            str_dirMana = @"/" + str_dirMana;
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
        DataTable dt = null;
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
}
