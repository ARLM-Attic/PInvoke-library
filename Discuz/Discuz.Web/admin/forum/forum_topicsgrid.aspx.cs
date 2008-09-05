using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

using Discuz.Control;
using Discuz.Common;
using Discuz.Forum;
using Button = Discuz.Control.Button;
using DataGrid = Discuz.Control.DataGrid;
using DropDownList = Discuz.Control.DropDownList;
using Discuz.Config;
using Discuz.Data;
using Discuz.Entity;



namespace Discuz.Web.Admin
{
    /// <summary>
    /// ���������б�
    /// </summary>

#if NET1
    public class topicsgrid : AdminPage
#else
    public partial class topicsgrid : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.DropDownTreeList forumid;
        protected System.Web.UI.HtmlControls.HtmlInputCheckBox nodeletepostnum;
        protected Discuz.Control.Button SetTopicInfo;
        protected Discuz.Control.DataGrid DataGrid1;
        protected Discuz.Control.DropDownList typeid;
        #endregion
#endif

        #region �ؼ�����

        protected CheckBox chkConfirmInsert;
        protected CheckBox chkConfirmUpdate;
        protected CheckBox chkConfirmDelete;

        #endregion

        public string condition = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["topicswhere"] != null)
                {
                    condition = Session["topicswhere"].ToString();
                }
                else
                {
                    Response.Redirect("forum_seachtopic.aspx");
                    return;
                }
                BindData();
            }
            //�����ݵ��ؼ�
            forumid.BuildTree(DatabaseProvider.GetInstance().GetForumsTree());
        }

        public void BindData()
        {
            #region
            DataGrid1.AllowCustomPaging = false;
            DataGrid1.BindData("Select * From " + BaseConfigs.GetTablePrefix + "topics WHERE " + condition);
            #endregion
        }

        protected void Sort_Grid(Object sender, DataGridSortCommandEventArgs e)
        {
            DataGrid1.Sort = e.SortExpression.ToString();
        }


        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.LoadCurrentPageIndex(e.NewPageIndex);
        }

        private void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            #region ���ݰ���ʾ���ȿ���

            if (e.Item.Cells[2].Text.ToString().Length > 15)
            {
                e.Item.Cells[2].Text = e.Item.Cells[2].Text.Substring(0, 15) + "��";
            }

            #endregion
        }


        private void SetTopicInfo_Click(object sender, EventArgs e)
        {
            #region ��ѡ���Ĳ�����������

            if (this.CheckCookie())
            {
                if (DNTRequest.GetString("tid") != "")
                {
                    string tidlist = DNTRequest.GetString("tid");
                    switch (DNTRequest.GetString("operation"))
                    {
                        case "moveforum":
                            {
                                if (forumid.SelectedValue != "0")
                                { //���ҳ���ǰ�����б���������FID
                                    //foreach (DataRow olddr in DbHelper.ExecuteDataset("SELECT distinct [fid] From [" + BaseConfigs.GetTablePrefix + "topics] WHERE [tid] IN(" + tidlist + ")").Tables[0].Rows)
                                    foreach (DataRow olddr in DatabaseProvider.GetInstance().GetTopicFidByTid(tidlist).Rows)
                                    {
                                        string oldtidlist = "0";
                                        //��FID���б�Ϊ�����г��ڵ�ǰFID�µ������б�
                                        //foreach (DataRow mydr in DbHelper.ExecuteDataset("SELECT [tid] From [" + BaseConfigs.GetTablePrefix + "topics] WHERE [tid] IN(" + tidlist + ") AND [fid]=" + olddr["fid"]).Tables[0].Rows)
                                        foreach (DataRow mydr in DatabaseProvider.GetInstance().GetTopicTidByFid(tidlist, int.Parse(olddr["fid"].ToString())).Rows)
                                        {
                                            oldtidlist += "," + mydr["tid"].ToString();
                                        }
                                        //����ǰ̨��������
                                        TopicAdmins.MoveTopics(oldtidlist, Convert.ToInt16(forumid.SelectedValue), Convert.ToInt16(olddr["fid"].ToString()));
                                    }
                                    AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "�����ƶ�����", "����ID:" + tidlist + " <br />Ŀ����̳fid:" + forumid.SelectedValue);

                                }
                                break;
                            }
                        case "movetype":
                            {
                                if (typeid.SelectedValue != "0")
                                {
                                    AdminTopics.SetTypeid(tidlist, Convert.ToInt16(typeid.SelectedValue));
                                    AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "�����޸���������", "����ID:" + tidlist + " <br />����tid:" + typeid.SelectedValue);
                                }
                                break;
                            }
                        case "delete":
                            {
                                if (nodeletepostnum.Checked)
                                {
                                    TopicAdmins.DeleteTopics(tidlist, 0, false);
                                }
                                else
                                {
                                    TopicAdmins.DeleteTopics(tidlist, 1, false);
                                }
                                Attachments.UpdateTopicAttachment(tidlist);
                                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "����ɾ������", "����ID:" + tidlist);
                                break;
                            }
                        case "displayorder":
                            {
                                AdminTopics.SetDisplayorder(tidlist, Convert.ToInt16(DNTRequest.GetString("displayorder_level")));
                                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "�����ö�����", "����ID:" + tidlist + "<br /> �ö���Ϊ:" + DNTRequest.GetString("displayorder_level"));
                                break;
                            }
                        case "adddigest":
                            {
                                TopicAdmins.SetDigest(DNTRequest.GetString("tid").Replace("0 ", ""), (short)Convert.ToInt16(DNTRequest.GetString("digest_level")));
                                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "�����Ӿ�����", "����ID:" + tidlist + "<br /> �Ӿ���Ϊ:" + DNTRequest.GetString("digest_level"));
                                break;
                            }
                        case "deleteattach":
                            {
                                AdminTopicOperations.DeleteAttachmentByTid(DNTRequest.GetString("tid").Replace("0 ", ""));
                                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "ɾ�������еĸ���", "����ID:" + tidlist);
                                break;
                            }
                    }
                    base.RegisterStartupScript("PAGE", "window.location.href='forum_topicsgrid.aspx';");
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('��ѡ����Ӧ������!');window.location.href='forum_topicsgrid.aspx';</script>");
                }
            }

            #endregion
        }


        public string BoolStr(string closed)
        {
            if (closed == "1")
            {
                return "<div align=center><img src=../images/OK.gif /></div>";
            }
            else
            {
                return "<div align=center><img src=../images/Cancel.gif /></div>";
            }
        }

        public string GetPostLink(string tid, string replies)
        {
            return "<a href=forum_postgrid.aspx?tid=" + tid + ">" + replies + "</a>";

        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SetTopicInfo.Click += new EventHandler(this.SetTopicInfo_Click);
            DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid_ItemDataBound);
            this.Load += new EventHandler(this.Page_Load);

            DataGrid1.TableHeaderName = "�����б�";
            DataGrid1.ColumnSpan = 11;
            DataGrid1.DataKeyField = "tid";
        }

        #endregion


    }
}