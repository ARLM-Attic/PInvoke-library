using System;
using System.Data;
using System.Web.UI;
using System.Xml;
using System.Web.UI.WebControls;

using Discuz.Control;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Data;
using Discuz.Entity;
using Discuz.Common;
using Discuz.Common.Xml;

using Discuz.Aggregation;

namespace Discuz.Web.Admin
{
    /// <summary>
    /// ����Ҫ��˵����� 
    /// </summary>
#if NET1
    public class forumaggset : AdminPage
#else
    public partial class forumaggset : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����

		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected Discuz.Control.DropDownList showtype;
		protected Discuz.Control.TextBox topnumber;
		protected Discuz.Control.Button SaveTopicDisplay;
		protected Discuz.Control.DropDownList tablelist;
		protected Discuz.Control.DropDownTreeList forumid;
		protected Discuz.Control.TextBox poster;
		protected Discuz.Control.TextBox title;
		protected Discuz.Control.Calendar postdatetimeStart;
		protected Discuz.Control.Calendar postdatetimeEnd;
		protected Discuz.Control.Button SearchTopicAudit;
		protected Discuz.Web.Admin.ajaxtopicinfo AjaxTopicInfo1;
		protected System.Web.UI.WebControls.Literal forumlist;
		protected Discuz.Control.Button SaveTopic;
		protected Discuz.Control.Hint Hint1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden forumtopicstatus;
        #endregion
#endif

        private string configPath;

        protected void Page_Load(object sender, EventArgs e)
        {
            //forumid.BuildTree(DatabaseProvider.GetInstance().GetForumsTree());
            forumid.BuildTree(DatabaseProvider.GetInstance().GetOpenForumListSql());
            if (!IsPostBack)
            {
                LoadWebSiteConfig();
                LoadPostTableList();
            }
        }

        private void LoadPostTableList()
        {
            #region װ�������б�
            DataRowCollection drlist = DatabaseProvider.GetInstance().GetDatechTableIds();
            foreach (DataRow dr in drlist)
            {
                tablelist.Items.Add(new ListItem(BaseConfigs.GetTablePrefix + "posts" + dr["id"].ToString(), dr["id"].ToString()));
            }
            tablelist.SelectedIndex = tablelist.Items.Count - 1;
            #endregion
        }

        /// <summary>
        /// װ��WebSite��Ϣ
        /// </summary>
       private void LoadWebSiteConfig()
       {
           #region װ��������Ϣ
           XmlDocumentExtender doc = new XmlDocumentExtender();
            doc.Load(configPath);
            XmlNodeList topiclistNode = doc.SelectNodes("/Aggregationinfo/Aggregationdata/Websiteaggregationdata/Topiclist/Topic");
            XmlNodeList website_spacelistnode = doc.SelectNodes("/Aggregationinfo/Aggregationpage/Website/Forum/Topiclist/Topic");
            XmlNodeInnerTextVisitor topicvisitor = new XmlNodeInnerTextVisitor();
            XmlNodeInnerTextVisitor pagetopicvisitor = new XmlNodeInnerTextVisitor();
            forumlist.Text = "";
            int i = 0;
            foreach (XmlNode topic in topiclistNode)
            {
                topicvisitor.SetNode(topic);
                bool isCheck = false;
                foreach (XmlNode index in website_spacelistnode)
                {
                    pagetopicvisitor.SetNode(index);
                    if (topicvisitor["topicid"].ToString() == pagetopicvisitor["topicid"].ToString())
                    {
                        isCheck = true;
                        break;
                    }
                }
                forumlist.Text += "<div class='mo' id='m" + i + "' flag='f" + i + "'><h1><input type='checkbox' name='tid' " + (isCheck ? "checked" : "") + " value='" + topicvisitor["topicid"] + "'>" + topicvisitor["title"] + "</h1></div>\n";
                i++;
            }
            topnumber.Text = doc.GetSingleNodeValue(doc.SelectNodes("/Aggregationinfo/Aggregationpage/Website/Forum")[0], "Bbs/Topnumber");
            showtype.SelectedValue = doc.GetSingleNodeValue(doc.SelectNodes("/Aggregationinfo/Aggregationpage/Website/Forum")[0], "Bbs/Showtype");
           #endregion
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveTopic_Click(object sender, EventArgs e)
        {
            #region ������Ϣ
            string tidlist = DNTRequest.GetString("forumtopicstatus");
            //��δѡ������ʱ�����������ѡ��
            if (tidlist == "")
            {
                XmlDocumentExtender doc = new XmlDocumentExtender();
                doc.Load(configPath);
                doc.RemoveNodeAndChildNode("/Aggregationinfo/Aggregationdata/Websiteaggregationdata/Topiclist");
                doc.RemoveNodeAndChildNode("/Aggregationinfo/Aggregationpage/Website/Forum/Topiclist");
                doc.Save(configPath);
                Response.Redirect("aggregation_editforumaggset.aspx");
                return;
            }
            else
            {
                //�õ���ѡ��������Ϣ
                DataTable dt = DatabaseProvider.GetInstance().GetTopicListByTidlist(tablelist.SelectedValue, tidlist);
            
                XmlDocumentExtender doc = new XmlDocumentExtender();
                doc.Load(configPath);
                //�����ǰѡ��
                XmlNode topiclistnode = doc.InitializeNode("/Aggregationinfo/Aggregationdata/Websiteaggregationdata/Topiclist");
                XmlNode websitetopiclistnode = doc.InitializeNode("/Aggregationinfo/Aggregationpage/Website/Forum/Topiclist");

                tidlist = DNTRequest.GetString("tid");
                foreach (DataRow dr in dt.Rows)
                {
                    //����Topic�ڵ�
                    XmlElement topic = doc.CreateElement("Topic");
                    doc.AppendChildElementByDataRow(ref topic, dt.Columns, dr, "tid,message");
                    doc.AppendChildElementByNameValue(ref topic, "topicid", dr["tid"].ToString());
                    string tempubbstr = UBB.ClearUBB(dr["message"].ToString());
                    if (tempubbstr.Length > 200)
                        tempubbstr = tempubbstr.Substring(0, 200) + "...";

                    doc.AppendChildElementByNameValue(ref topic, "shortdescription", tempubbstr, true);
                    doc.AppendChildElementByNameValue(ref topic, "fulldescription", UBB.ClearUBB(dr["message"].ToString()), true);
                    topiclistnode.AppendChild(topic);


                    if (("," + tidlist + ",").IndexOf("," + dr["tid"].ToString() + ",") >= 0)
                    {
                        websitetopiclistnode.AppendChild(topic.Clone());
                    }
                }                          
                doc.Save(configPath);
                AggregationFacade.BaseAggregation.ClearAllDataBind();
                Response.Redirect("aggregation_editforumaggset.aspx");
            }
            #endregion
        }

        /// <summary>
        /// ����������ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveTopicDisplay_Click(object sender, EventArgs e)
        {
            #region ����������ʾ
            XmlDocumentExtender doc = new XmlDocumentExtender();
            doc.Load(configPath);
            //doc.RemoveNodeAndChildNode("/Aggregationinfo/Aggregationpage/Website/Forum/Bbs");
            doc.InitializeNode("/Aggregationinfo/Aggregationpage/Website/Forum");

            XmlElement BBS = doc.CreateElement("Bbs");
            doc.AppendChildElementByNameValue(ref BBS, "Topnumber", topnumber.Text, false);
            doc.AppendChildElementByNameValue(ref BBS, "Showtype", showtype.SelectedValue, false);
            doc.SelectSingleNode("/Aggregationinfo/Aggregationpage/Website/Forum").AppendChild(BBS);
            doc.Save(configPath);
            #endregion
        }

        #region Web ������������ɵĴ���

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveTopic.Click += new EventHandler(this.SaveTopic_Click);
            this.SaveTopic.ValidateForm = true;
            configPath = Server.MapPath(BaseConfigs.GetForumPath + "config/aggregation.config");
        }

        #endregion

    }
}