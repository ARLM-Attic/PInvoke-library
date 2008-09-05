using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

using Discuz.Common;
using Discuz.Forum;
using Button = Discuz.Control.Button;
using DataGrid = Discuz.Control.DataGrid;
using TextBox = Discuz.Control.TextBox;
using Discuz.Config;
using Discuz.Data;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// �ֱ�����
    /// </summary>
    
#if NET1
    public class detachtable : AdminPage
#else
    public partial class detachtable : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.Button StartFullIndex;
        protected Discuz.Control.DataGrid DataGrid1;
        protected Discuz.Control.TextBox detachtabledescription;
        protected Discuz.Control.Button SaveInfo;
        protected Discuz.Control.Hint Hint1;
        protected Discuz.Web.Admin.pageinfo info1;
        protected Discuz.Web.Admin.pageinfo info2;
        #endregion
#endif

        private readonly bool IsFullTextSearchEnabled = DbHelper.Provider.IsFullTextSearchEnabled();

        public string currentpost_tablename = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            currentpost_tablename = Posts.GetPostTableName();
            info2.Text = "ϵͳ��ǰʹ�õ����ӷֱ���: <b>" + currentpost_tablename + "</b>";
            if (!Page.IsPostBack)
            {
                if (!IsFullTextSearchEnabled)
                {

                    StartFullIndex.Visible = false;
                    DataGrid1.Columns[0].Visible = false;  
                    
                }
                 BindData();
                detachtabledescription.AddAttributes("maxlength", "50");
                int currentpostsnum = DatabaseProvider.GetInstance().GetPostCount(currentpost_tablename) / 10000;
                SaveInfo.Attributes.Add("onclick", "if(!confirm('��Ŀǰ��������������" + (currentpostsnum + 1) + "��,Ҫ�������ӷֱ���?')){return false;}");
            }
        }

        public void BindData()
        {
           
            DataGrid1.DataSource = buildGridData();
            DataGrid1.DataBind();
        }

        protected void Sort_Grid(Object sender, DataGridSortCommandEventArgs e)
        {
            DataView dv = new DataView(buildGridData());
            dv.Sort = e.SortExpression.ToString();
            DataGrid1.DataSource = dv;
            DataGrid1.DataBind();
        }

        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            BindData();
        }

        protected void DataGrid_Edit(Object sender, DataGridCommandEventArgs E)
        {
            DataGrid1.EditItemIndex = E.Item.ItemIndex;
            BindData();
        }

        protected void DataGrid_Cancel(Object sender, DataGridCommandEventArgs E)
        {
            DataGrid1.EditItemIndex = -1;
            BindData();
        }

        private DataTable buildGridData()
        {
            return DatabaseProvider.GetInstance().GetPostTableList();
        }

        protected void DataGrid_Update(Object sender, DataGridCommandEventArgs E)
        {
            #region ����ָ���ķֱ���Ϣ

            int fid = Utils.StrToInt(DataGrid1.DataKeys[E.Item.ItemIndex].ToString(), 0);
            string description = ((System.Web.UI.WebControls.TextBox)E.Item.Cells[4].Controls[0]).Text;

            try
            {
                DatabaseProvider.GetInstance().UpdateDetachTable(fid, description);
                base.RegisterStartupScript( "", "<script>window.location.href='global_detachtable.aspx';</script>");
            }
            catch
            {
                base.RegisterStartupScript( "", "<script>alert('�޷��������ݿ�.');window.location.href='global_detachtable.aspx';</script>");
                return;
            }

            #endregion
        }

        private void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            #region �������ݰ󶨵ĳ���

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                System.Web.UI.WebControls.TextBox t = (System.Web.UI.WebControls.TextBox)e.Item.Cells[4].Controls[0];
                t.Attributes.Add("maxlength", "50");
                t.Attributes.Add("size", "20");
            }


            if (e.Item.ItemType == ListItemType.Item)
            {
                if (e.Item.Cells[2].Text.ToString().Length > 40)
                {
                    e.Item.Cells[2].Text = e.Item.Cells[2].Text.Substring(0, 40) + "��";
                }
            }

            #endregion
        }

        private void StartFullIndex_Click(object sender, EventArgs e)
        {
            #region ��ʼ����������

            if (this.CheckCookie())
            {
                string DbName = DatabaseProvider.GetInstance().GetDbName();

                if (DNTRequest.GetString("id") != "")
                {
                    try
                    {
                        DatabaseProvider.GetInstance().StartFullIndex(DbName);                        
                        aysncallback = new delegateCreateOrFillText(StarFillIndexWithPostid);
                        AsyncCallback myCallBack = new AsyncCallback(CallBack);
                        aysncallback.BeginInvoke(DbName, DNTRequest.GetString("id"), myCallBack, this.username); //
                        base.LoadRegisterStartupScript("PAGE", "window.location.href='global_detachtable.aspx';");
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message.Replace("'", " ");
                        message = message.Replace("\\", "/");
                        message = message.Replace("\r\n", "\\r\\n");
                        message = message.Replace("\r", "\\r");
                        message = message.Replace("\n", "\\n");
                        base.RegisterStartupScript( "", "<script>alert('" + message + "');</script>");
                    }
                }
                else
                {
                    base.RegisterStartupScript( "", "<script>alert('��δѡ���κ�ѡ��');window.location.href='global_detachtable.aspx';</script>");
                }
            }

            #endregion
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            #region ���ֱ����ִ�к��������Ӧ�Ļ�������

            if (CreateDetachTable(detachtabledescription.Text))
            {
                AdminCaches.ReSetLastPostTableName();
                AdminCaches.ReSetAllPostTableName();
                base.RegisterStartupScript( "PAGE",  "window.location.href='global_detachtable.aspx';");
            }

            #endregion
        }

        public bool CreateDetachTable(string description)
        {
            #region �����ֱ�

            try
            {
                string tableprefix = BaseConfigs.GetTablePrefix;

                string currentdbprefix = tableprefix + "posts"; //��ǰ���ݱ���ʹ�õ�ǰ�

                //ȡ����ǰ�������ID�ļ�¼������
                int tablelistmaxid = DatabaseProvider.GetInstance().GetMaxTableListId();

                if (tablelistmaxid > 213) //��ֵ�������ܴ���213
                {
                    base.RegisterStartupScript( "", "<script>alert('��ֵ�������ܴ���213,��ǰ���ֵΪ" + tablelistmaxid + "!');window.location.href='global_detachtable.aspx';</script>");
                    return false;
                }
                //���µ�ǰ�������ID�ļ�¼�õ�������Сtid�ֶ�		
                if (tablelistmaxid > 0)
                {
                    DatabaseProvider.GetInstance().UpdateMinMaxField(currentdbprefix + tablelistmaxid, tablelistmaxid);
                }

                string tablename = currentdbprefix + (tablelistmaxid + 1);

                try
                {
                    //������Ӧ��ȫ������
                     DatabaseProvider.GetInstance().CreatePostTableAndIndex(tablename);
                }
                catch (Exception ex)
                {
                    string message = ex.Message.Replace("'", " ");
                    message = message.Replace("\\", "/");
                    message = message.Replace("\r\n", "\\r\\n");
                    message = message.Replace("\r", "\\r");
                    message = message.Replace("\n", "\\n");
                    base.RegisterStartupScript( "", "<script>alert('" + message + "');</script>");
                }
                finally
                {
                    if (tablelistmaxid > 0)
                    {
                        DatabaseProvider.GetInstance().AddPostTableToTableList(description, DatabaseProvider.GetInstance().GetMaxPostTableTid(currentdbprefix + tablelistmaxid), 0);
                    }
                    else
                    {
                        DatabaseProvider.GetInstance().AddPostTableToTableList(description, DatabaseProvider.GetInstance().GetMaxPostTableTid(currentdbprefix), 0);
                    }
                    //�����洢����
                    if (DbHelper.Provider.IsStoreProc())
                    {
                        CreateStoreProc(tablelistmaxid);
                    }
                    AdminCaches.ReSetPostTableInfo();
                }
                return true;
            }
            catch
            {
                return false;
            }

            #endregion
        }

        private void CreateStoreProc(int tablelistmaxid)
        {
            if (DbHelper.Provider.IsStoreProc())
                DatabaseProvider.GetInstance().CreateStoreProc(tablelistmaxid + 1);
        }

        public string DisplayTid(string mintid, string maxtid)
        {
            #region ��ʾ��ǰ�ֱ�������TID�ķ�Χ

            if (maxtid == "0")
            {
                DataTable dt = DatabaseProvider.GetInstance().GetMaxTid();
                if (dt.Rows.Count > 0)
                {
                    return mintid + " �� " + (dt.Rows[0][0].ToString() == "" ? mintid : dt.Rows[0][0].ToString());
                }
                else
                {
                    return mintid + " �� " + mintid;
                }
            }
            else
            {
                return mintid + " �� " + maxtid;
            }

            #endregion
        }

        public string CurrentPostsCount(string postsid)
        {
            #region �õ���ǰ�ֱ��������

            try
            {
                DataTable dt = DatabaseProvider.GetInstance().GetPostCountFromIndex(postsid);
                if (dt.Rows.Count > 0)
                    return dt.Rows[0][0].ToString();
                else
                    return "0";
            }
            catch
            {
                DataTable dt = DatabaseProvider.GetInstance().GetPostCountTable(postsid);
                if (dt.Rows.Count > 0)
                    return dt.Rows[0][0].ToString();
                else
                    return "0";
            }

            #endregion
        }

        #region �첽�����������������Ĵ���

        private delegate bool delegateCreateOrFillText(string DbName, string postidlist);

        //�첽�����������������Ĵ���
        private delegateCreateOrFillText aysncallback;


        public void CallBack(IAsyncResult e)
        {
            aysncallback.EndInvoke(e);
        }

        //private static AdminDbIndexOp __admindbindexop = new AdminDbIndexOp();

        public bool StarFillIndexWithPostid(string DbName, string postidlist)
        {
            try
            {
                foreach (string postid in postidlist.Split(','))
                {
                    DatabaseProvider.GetInstance().CreateORFillIndex(DbName, postid);
                }
                return true;
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("'", " ");
                message = message.Replace("\\", "/");
                message = message.Replace("\r\n", "\\r\\n");
                message = message.Replace("\r", "\\r");
                message = message.Replace("\n", "\\n");
                return false;
            }
        }

        #endregion

        #region Web ������������ɵĴ���

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.DataGrid1.EditCommand += new DataGridCommandEventHandler(this.DataGrid_Edit);
            this.DataGrid1.CancelCommand += new DataGridCommandEventHandler(DataGrid_Cancel);
            this.DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid_ItemDataBound);
            this.DataGrid1.UpdateCommand += new DataGridCommandEventHandler(this.DataGrid_Update);
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
            this.StartFullIndex.Click += new EventHandler(this.StartFullIndex_Click);

            this.Load += new EventHandler(this.Page_Load);

            DataGrid1.LoadEditColumn();
            DataGrid1.DataKeyField = "id";
            DataGrid1.TableHeaderName = "���ӷֱ��б�";
            DataGrid1.ColumnSpan = 4;
            DataGrid1.SaveDSViewState = true;

        }

        #endregion
    }
}
