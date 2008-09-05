using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

using Discuz.Common;
using Discuz.Entity;
using Discuz.Forum;
using Discuz.Data;
using Button = Discuz.Control.Button;
using DataGrid = Discuz.Control.DataGrid;
using DropDownList = Discuz.Control.DropDownList;
using Discuz.Config;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// �������
    /// </summary>
     
#if NET1
    public class auditpost : AdminPage
#else
    public partial class auditpost : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.DropDownList postlist;
        protected Discuz.Control.Button SelectPass;
        protected Discuz.Control.Button SelectDelete;
        protected Discuz.Control.DataGrid DataGrid1;
        protected Discuz.Web.Admin.ajaxpostinfo AjaxPostInfo1;

        protected System.Web.UI.WebControls.Literal msg;
        #endregion
#endif

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (DatabaseProvider.GetInstance().GetCurrentPostTableRecordCount(int.Parse(postlist.SelectedValue)) == 0)
                {
                    msg.Visible = true;
                }
                BindData();
            }
        }

        public void BindData()
        {
            #region ���������
            DataGrid1.AllowCustomPaging = false;
            DataGrid1.TableHeaderName = "��������б�";
            DataGrid1.DataKeyField = "pid";
            DataGrid1.BindData(DatabaseProvider.GetInstance().GetUnauditPostSQL(int.Parse(postlist.SelectedValue)));
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

        private void postslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        public void initPostTable()
        {
            #region ��ʼ���ֱ�ؼ�

            postlist.AutoPostBack = true;

            //DataTable dt = DbHelper.ExecuteDataset("SELECT ID FROM [" + BaseConfigs.GetTablePrefix + "tablelist] Order BY ID ASC").Tables[0];
            DataTable dt = DatabaseProvider.GetInstance().GetDetachTableId();
            postlist.Items.Clear();
            foreach (DataRow r in dt.Rows)
            {
                postlist.Items.Add(new ListItem(BaseConfigs.GetTablePrefix + "posts" + r[0].ToString(), r[0].ToString()));
            }
            postlist.DataBind();
            postlist.SelectedValue = Posts.GetPostTableID();

            #endregion
        }

        private void SelectPass_Click(object sender, EventArgs e)
        {
            #region ��ѡ�е���������Ϊͨ�����

            string pidlist = DNTRequest.GetString("pid");
            if (this.CheckCookie())
            {
                if (pidlist != "")
                {
                    UpdateUserCredits(int.Parse(postlist.SelectedValue), pidlist);
                    DatabaseProvider.GetInstance().PassPost(int.Parse(postlist.SelectedValue), pidlist);
                    base.RegisterStartupScript( "PAGE", "window.location.href='forum_auditpost.aspx';");
                }
                else
                {
                    base.RegisterStartupScript( "", "<script>alert('��δѡ���κ�ѡ��');window.location.href='forum_auditpost.aspx';</script>");
                }
            }

            #endregion
        }

        /// <summary>
        /// �����û�����
        /// </summary>
        /// <param name="postTableId">�ֱ�id</param>
        /// <param name="pidlist">ͨ��������ӵ�Pid�б�</param>
        private void UpdateUserCredits(int postTableId, string pidlist)
        {
            string[] pidarray = pidlist.Split(',');
            float[] values = null;
            ForumInfo forum = null;
            PostInfo post = null;
            int fid = -1;
            foreach (string pid in pidarray)
            {
                post = Posts.GetPostInfo(postTableId, int.Parse(pid));  //��ȡ������Ϣ
                if (fid != post.Fid)    //����һ���͵�ǰ���ⲻ��һ�������ʱ�����¶�ȡ���Ļ�������
                {
                    fid = post.Fid;
                    forum = Forums.GetForumInfo(fid);
                    if (!forum.Replycredits.Equals(""))
                    {
                        int index = 0;
                        float tempval = 0;
                        values = new float[8];
                        foreach (string ext in Utils.SplitString(forum.Replycredits, ","))
                        {

                            if (index == 0)
                            {
                                if (!ext.Equals("True"))
                                {
                                    values = null;
                                    break;
                                }
                                index++;
                                continue;
                            }
                            tempval = Utils.StrToFloat(ext, 0.0f);
                            values[index - 1] = tempval;
                            index++;
                            if (index > 8)
                            {
                                break;
                            }
                        }
                    }
                }

                if (values != null)
                {
                    ///ʹ�ð���ڻ���
                    UserCredits.UpdateUserCreditsByPosts(post.Posterid, values);
                }
                else
                {
                    ///ʹ��Ĭ�ϻ���
                    UserCredits.UpdateUserCreditsByPosts(post.Posterid);
                }
            }
        }

        private void SelectDelete_Click(object sender, EventArgs e)
        {
            #region ��ѡ�е����ӽ���ɾ��

            if (this.CheckCookie())
            {
                if (DNTRequest.GetString("pid") != "")
                {
                    DataTable dt = new DataTable();
                    foreach (string pid in DNTRequest.GetString("pid").Split(','))
                    {
                        if (pid.Trim() != "")
                        {
                            //dt = DbHelper.ExecuteDataset("SELECT TOP 1 * [layer],[tid]  FROM [" + BaseConfigs.GetTablePrefix + "posts" + postlist.SelectedValue + "] WHERE [pid]=" + pid).Tables[0];
                            dt = DatabaseProvider.GetInstance().GetPostLayer(int.Parse(postlist.SelectedValue), int.Parse(pid));
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["layer"].ToString().Trim() == "0")
                                {
                                    TopicAdmins.DeleteTopics(dt.Rows[0]["tid"].ToString(), false);
                                }
                                else
                                {
                                    Posts.DeletePost(postlist.SelectedValue, Convert.ToInt32(pid), false,false);
                                }
                            }
                        }
                    }
                    base.RegisterStartupScript( "PAGE", "window.location.href='forum_auditpost.aspx';");
                }
                else
                {
                    base.RegisterStartupScript( "", "<script>alert('��δѡ���κ�ѡ��');window.location.href='forum_auditpost.aspx';</script>");
                }
            }

            #endregion
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }


        private void InitializeComponent()
        {
            this.postlist.SelectedIndexChanged += new EventHandler(this.postslist_SelectedIndexChanged);
            this.SelectPass.Click += new EventHandler(this.SelectPass_Click);
            this.SelectDelete.Click += new EventHandler(this.SelectDelete_Click);

            this.Load += new EventHandler(this.Page_Load);

            DataGrid1.DataKeyField = "pid";
            DataGrid1.TableHeaderName = "��������б�";
            DataGrid1.ColumnSpan = 7;

            initPostTable();
        }

        #endregion

    }
}