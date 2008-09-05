using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;

using Discuz.Common;
using Discuz.Data;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Entity;


namespace Discuz.Web.Admin
{

    /// <summary>
    ///	ajax ��ȡ������Ϣ
    /// </summary>
    public class ajaxtopicinfo : System.Web.UI.UserControl
    {
        public DataTable dt;
        public string pagelink;
        public int currentpage = 0;
        public string postname;
        string tablelist;
        int forumid;
        string posterlist;
        string keylist;
        string startdate;
        string enddate;
        //ҳ���С
        public int pagesize = 16;

        public ajaxtopicinfo()
        {
            //��ȡ��ѯ��Ϣ
            forumid = DNTRequest.GetInt("_ctl0", 0);
            posterlist = DNTRequest.GetString("poster");
            keylist = DNTRequest.GetString("title");
            startdate = DNTRequest.GetString("postdatetimeStart:postdatetimeStart");
            enddate = DNTRequest.GetString("postdatetimeEnd:postdatetimeEnd");
            currentpage = DNTRequest.GetInt("currentpage", 1);
            tablelist = DNTRequest.GetString("tablelist");
            if (tablelist == "")
                tablelist = DatabaseProvider.GetInstance().GetMaxTableListId().ToString();
            postname = BaseConfigs.GetTablePrefix + "posts" + tablelist;
            //��ȡ��ǰҳ��
            if (DNTRequest.GetInt("postnumber", 0) > 0)
            {
                pagesize = DNTRequest.GetInt("postnumber", 0);
            }
            //��ȡ��ҳ��
            int recordcount = DatabaseProvider.GetInstance().GetTopicListCountByCondition(postname,forumid, posterlist, keylist, startdate, enddate);
            dt = DatabaseProvider.GetInstance().GetTopicListByCondition(postname,forumid, posterlist, keylist, startdate, enddate, 10, currentpage);
            pagelink = AjaxPagination(recordcount, 10, currentpage);
            
        }

        //// <summary>
        /// ��ҳ����
        /// </summary>
        /// <param name="recordcount">�ܼ�¼��</param>
        /// <param name="pagesize">ÿҳ��¼��</param>
        /// <param name="currentpage">��ǰҳ��</param>
        public string AjaxPagination(int recordcount, int pagesize, int currentpage)
        {
            if (DNTRequest.GetInt("postnumber", 0) > 0)
            {
                return AjaxPagination(recordcount, pagesize, currentpage, "../usercontrols/ajaxtopicinfo.ascx", "tablelist=" + tablelist + "&_ctl0=" + forumid + "&poster=" + posterlist + "&title=" + keylist + "&postdatetimeStart:postdatetimeStart=" + startdate + "&postdatetimeEnd:postdatetimeEnd=" + enddate + "&postnumber=" + DNTRequest.GetInt("postnumber", 0), "topiclistgrid");
            }
            else
            {
                return AjaxPagination(recordcount, pagesize, currentpage, "../usercontrols/ajaxtopicinfo.ascx", "tablelist=" + tablelist + "&_ctl0=" + forumid + "&poster=" + posterlist + "&title=" + keylist + "&postdatetimeStart:postdatetimeStart=" + startdate + "&postdatetimeEnd:postdatetimeEnd=" + enddate, "topiclistgrid");
            }
        }
        /// <summary>
        /// ��ҳ����
        /// </summary>
        /// <param name="recordcount">�ܼ�¼��</param>
        /// <param name="pagesize">ÿҳ��¼��</param>
        /// <param name="currentpage">��ǰҳ��</param>
        public string AjaxPagination(int recordcount, int pagesize, int currentpage, string usercontrolname, string paramstr, string divname)
        {
            int allcurrentpage = 0;
            int next = 0;
            int pre = 0;
            int startcount = 0;
            int endcount = 0;
            string currentpagestr = "<BR />";

            if (currentpage < 1)
            {
                currentpage = 1;
            }

            //������ҳ��
            if (pagesize != 0)
            {
                allcurrentpage = (recordcount / pagesize);
                allcurrentpage = ((recordcount % pagesize) != 0 ? allcurrentpage + 1 : allcurrentpage);
                allcurrentpage = (allcurrentpage == 0 ? 1 : allcurrentpage);
            }
            next = currentpage + 1;
            pre = currentpage - 1;

            //�м�ҳ��ʼ���
            startcount = (currentpage + 5) > allcurrentpage ? allcurrentpage - 9 : currentpage - 4;

            //�м�ҳ��ֹ���
            endcount = currentpage < 5 ? 10 : currentpage + 5;

            //Ϊ�˱��������ʱ������������������С��1�ʹ����1��ʼ
            if (startcount < 1)
            {
                startcount = 1;
            }

            //ҳ��+5�Ŀ����Ծͻ�������������Ŵ�����ҳ�룬��ô��Ҫ���������ҳ����֮��
            if (allcurrentpage < endcount)
            {
                endcount = allcurrentpage;
            }

            if (startcount > 1)
            {
                currentpagestr += currentpage > 1 ? "&nbsp;&nbsp;<a href=\"###\"  onclick=\"javascript:AjaxHelper.Updater('" + usercontrolname + "','" + divname + "', 'load=true&" + paramstr + "&currentpage=" + pre + "');\" title=\"��һҳ\">��һҳ</a>" : "";
            }

            //��ҳ��������1ʱ, ����ʾҳ��
            if (endcount > 1)
            {
                //�м�ҳ����, �������ʱ�临�Ӷȣ���С�ռ临�Ӷ�
                for (int i = startcount; i <= endcount; i++)
                {
                    currentpagestr += currentpage == i ? "&nbsp;" + i + "" : "&nbsp;<a href=\"###\"  onclick=\"javascript:AjaxHelper.Updater('" + usercontrolname + "','" + divname + "', 'load=true&" + paramstr + "&currentpage=" + i + "');\">" + i + "</a>";
                }
            }

            if (endcount < allcurrentpage)
            {
                currentpagestr += currentpage != allcurrentpage ? "&nbsp;&nbsp;<a href=\"###\" onclick=\"javascript:AjaxHelper.Updater('" + usercontrolname + "','" + divname + "', 'load=true&" + paramstr + "&currentpage=" + next + "');\" title=\"��һҳ\">��һҳ</a>&nbsp;&nbsp;" : "";
            }

            if (endcount > 1)
            {
                currentpagestr += "&nbsp; &nbsp; &nbsp; &nbsp;";
            }

            currentpagestr += "�� " + allcurrentpage + " ҳ, ��ǰ�� " + currentpage + " ҳ, �� " + recordcount + " ����¼";

            return currentpagestr;

        }
    }
}