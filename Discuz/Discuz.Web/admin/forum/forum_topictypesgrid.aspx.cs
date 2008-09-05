using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections;

using Discuz.Common;
using Discuz.Forum;
using Button = Discuz.Control.Button;
using RadioButtonList = Discuz.Control.RadioButtonList;
using TextBox = Discuz.Control.TextBox;
using Discuz.Config;
using Discuz.Data;

using Discuz.Cache;

namespace Discuz.Web.Admin
{

#if NET1
    public class topictypesgrid : AdminPage
#else
    public partial class topictypesgrid : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.Button delButton;
        protected Discuz.Control.DataGrid DataGrid1;
        protected Discuz.Control.TextBox typename;
        protected Discuz.Control.TextBox displayorder;
        protected Discuz.Control.TextBox description;
        protected Discuz.Control.Button AddNewRec;
		protected Discuz.Control.Button SaveTopicType;
        #endregion
#endif
        private DataTable ForumNameIncludeTopicType;
        private void Page_Load(object sender, System.EventArgs e)
        {
            ForumNameIncludeTopicType = DatabaseProvider.GetInstance().GetForumNameIncludeTopicType();
            // �ڴ˴������û������Գ�ʼ��ҳ��
            if (!Page.IsPostBack)
            {
                BindData();	//���������
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public void BindData()
        {
            #region ������
            DataGrid1.AllowCustomPaging = false;
            DataGrid1.TableHeaderName = "�������";
            DataGrid1.BindData(DatabaseProvider.GetInstance().GetTopicTypes());
            #endregion
        }

        /// <summary>
        /// ��ҳ
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.LoadCurrentPageIndex(e.NewPageIndex);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Sort_Grid(Object sender, DataGridSortCommandEventArgs e)
        {
            DataGrid1.Sort = e.SortExpression.ToString();
        }

        private int GetDisplayOrder(string topicTypeName, DataTable topicTypes)
        {
            #region ������������
            foreach (DataRow dr in topicTypes.Rows)
            {
                if (dr["name"].ToString().Trim() == topicTypeName.Trim())
                {
                    return int.Parse(dr["displayorder"].ToString());
                }
            }
            return -1;
            #endregion
        }

        private string GetTopicTypeString(string topicTypes, string topicName)
        {
            #region ���������Ƿ�������
            foreach (string type in topicTypes.Split('|'))
            {
                if (type.IndexOf("," + topicName.Trim() + ",") != -1)
                    return type;
            }
            return "";
            #endregion
        }


        /// <summary>
        /// �������������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddNewRec_Click(object sender, EventArgs e)
        {
            #region �������������
            //��������Ƿ�Ϸ�
            if (!CheckValue(typename.Text, displayorder.Text, description.Text)) return;

            //����Ƿ���ͬ���������
            if(DatabaseProvider.GetInstance().IsExistTopicType(typename.Text))
            {
                base.RegisterStartupScript( "", "<script>alert('���ݿ����Ѵ�����ͬ�������������');window.location.href='forum_topictypesgrid.aspx';</script>");
                return;
            }

            //���ӷ��ൽdnt_topictypes,��д��־
            DatabaseProvider.GetInstance().AddTopicTypes(typename.Text, int.Parse(displayorder.Text), description.Text);
            AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "����������", "����������,����Ϊ:" + typename.Text);

            //���·��໺��
            DNTCache cache = DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/TopicTypes");
            base.RegisterStartupScript("", "<script>window.location.href='forum_topictypesgrid.aspx';</script>");
            return;
            #endregion
        }

        /// <summary>
        /// ɾ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void delButton_Click(object sender, EventArgs e)
        {
            #region ɾ���������
            if (this.CheckCookie())
            {
                if (DNTRequest.GetString("id") != "")
                {
                    //ȡ��Ҫɾ����ID�б��ԡ������ָ�
                    string idlist = DNTRequest.GetString("id");

                    //���ø��°��ķ���
                    DeleteForumTypes(idlist);

                    //����������(dnt_topictypes)��ɾ����Ӧ�ķ��ಢд��־
                    DatabaseProvider.GetInstance().DeleteTopicTypesByTypeidlist(idlist);
                    AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "ɾ���������", "ɾ���������,IDΪ:" + DNTRequest.GetString("id").Replace("0 ", ""));

                    //����������໺��
                    DNTCache cache = DNTCache.GetCacheService();
                    cache.RemoveObject("/Forum/TopicTypes");
                    Response.Redirect("forum_topictypesgrid.aspx");
                }
                else
                {
                    base.RegisterStartupScript( "", "<script>alert('��δѡ���κ�ѡ��');window.location.href='forum_attachtypesgrid.aspx';</script>");
                }
            }
            #endregion
        }

        /// <summary>
        /// �������Ƿ�Ϸ�
        /// </summary>
        /// <param name="typename">�����������</param>
        /// <param name="displayorder">�������</param>
        /// <param name="description">����</param>
        /// <returns>�Ϸ�����treu�����򷵻�false</returns>
        private bool CheckValue(string typename, string displayorder, string description)
        {
            #region �������Ƿ�Ϸ�
            if (typename == "" || typename.Length > 100 )
            {
                base.RegisterStartupScript("", "<script>alert('����������Ʋ���Ϊ��');window.location.href='forum_topictypesgrid.aspx';</script>");
                return false;
            }

            if ((displayorder == "") || (Convert.ToInt32(displayorder) < 0))
            {
                base.RegisterStartupScript("", "<script>alert('��ʾ˳����Ϊ�� ');window.location.href='forum_topictypesgrid.aspx';</script>");
                return false;
            }

            if (description.Length > 500)
            {
                base.RegisterStartupScript("", "<script>alert('�������ܳ���500����');window.location.href='forum_topictypesgrid.aspx';</script>");
                return false;
            }
            if( typename.IndexOf("|") > 0 )
            {
                base.RegisterStartupScript("", "<script>alert('���ܺ��зǷ��ַ� | ');window.location.href='forum_topictypesgrid.aspx';</script>");
                return false;
            }
            return true;
            #endregion
        }

        /// <summary>
        /// ǰ̨�󶨡�������̳���ķ���
        /// </summary>
        /// <param name="id">��������ID</param>
        /// <returns>���ع�����̳�������ַ���</returns>
        public string LinkForum(string id)
        {
            #region �����������󶨵���̳����
            string str = "";

            //����ÿ����̳���
            foreach (DataRow dr in ForumNameIncludeTopicType.Rows)
            {
                //����������б��ü������|���п�
                foreach (string type in dr["topictypes"].ToString().Split('|'))
                {
                    //��������ID��Id+","��������Ϊ�˷���ƥ�䣬��Ϊ����ID�ڰ���б�����ÿһ��Ŀ�ʼ���������0����˵���ҵ��ˣ�С��0��ʾδ�ҵ�������0��ʾ��������ID��
                    if (type.IndexOf(id + ",") == 0)
                    {
                        //�γ��ַ���
                        str += "<a href='/showforum.aspx?forumid=" + dr["fid"] + "&typeid=" + id + "&search=1' target='_blank'>" + dr["name"].ToString().Trim() + "</a>";
                        str += "[<a href='forum_editforums.aspx?fid=" + dr["fid"] + "&tabindex=4'>�༭</a>],";
                        //ÿһ���������ֻ�ܴ�����һ������У��ҵ���Ͳ��������²��ң�������������飬���Ų�����һ���
                        break;
                    }
                }
            }

            //�����str��Ϊ��˵���а���������ID�İ�飬����ȥ������һ����,��
            if (str != "")
                return str.Substring(0, str.Length - 1);
            else
                return "";
            #endregion
        }

        /// <summary>
        /// ɾ������е��������
        /// </summary>
        /// <param name="idlist">Ҫɾ����������ID�б�</param>
        private void DeleteForumTypes(string idlist)
        {
            #region ɾ����ѡ���������
            //ȡ��ID������
            string[] ids = idlist.Split(',');

            //ȡ���������Ļ���
#if NET1            
            System.Collections.SortedList __topictypearray = new SortedList();
#else
            Discuz.Common.Generic.SortedList<int, object> __topictypearray = new Discuz.Common.Generic.SortedList<int, object>();
#endif
            __topictypearray = Caches.GetTopicTypeArray();

            //ȡ�ð���fid,topictypes�ֶ�
            DataTable dt = DatabaseProvider.GetInstance().GetForumTopicType();

            //����ÿһ�����
            foreach (DataRow dr in dt.Rows)
            {
                //���������������ֶ�Ϊ�գ�topictypes==""����������һ��
                if (dr["topictypes"].ToString() == "") continue;

                string topictypes = dr["topictypes"].ToString();
                //����ÿһ��Ҫɾ����ID
                foreach (string id in ids)
                {
                    //��ɾ����IDƴ����Ӧ�ĸ�ʽ���󣬽�ԭ�����޳������γ�һ���µ����������ֶ�
                    topictypes = topictypes.Replace(id + "," + __topictypearray[Int32.Parse(id)].ToString() + ",0|", "");
                    topictypes = topictypes.Replace(id + "," + __topictypearray[Int32.Parse(id)].ToString() + ",1|", "");
                    //�������б�dnt_topics����typeidΪ��ǰҪɾ����Id����Ϊ0
                    DatabaseProvider.GetInstance().ClearTopicTopicType(int.Parse(id));
                }
                //���޳���Ҫɾ��������ID�������б�ֵ�������ݿ�
                DatabaseProvider.GetInstance().UpdateTopicTypeForForum(topictypes, int.Parse(dr["fid"].ToString()));
            }
            #endregion
        }

        private void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            #region ���ñ༭����
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ((System.Web.UI.WebControls.TextBox)e.Item.Cells[1].Controls[0]).Width = 150;
                ((System.Web.UI.WebControls.TextBox)e.Item.Cells[2].Controls[0]).Width = 30;
                ((System.Web.UI.WebControls.TextBox)e.Item.Cells[3].Controls[0]).Width = 250;
            }
            #endregion
        }

        private void SaveTopicType_Click(object sender, EventArgs e)
        {
            #region �����������༭
            //������ȡ�༭�еĸ���ֵ
            int rowid = 0;
            bool error = false;
            foreach (object o in DataGrid1.GetKeyIDArray())
            {
                string id = o.ToString();
                string name = DataGrid1.GetControlValue(rowid, "name");
                string displayorder = DataGrid1.GetControlValue(rowid, "displayorder");
                string description = DataGrid1.GetControlValue(rowid, "description");

                //�������ĺϷ���
                //if (!CheckValue(name, displayorder, description)) return;

                //�ж������������Ƿ�����Ҫ���µ�����
                if (!CheckValue(name, displayorder, description) || DatabaseProvider.GetInstance().IsExistTopicType(name, int.Parse(id)))
                {
                    //base.RegisterStartupScript("", "<script>alert('���ݿ����Ѵ�����ͬ�������������');window.location.href='forum_topictypesgrid.aspx';</script>");
                    //return;
                    error = true;
                    continue;
                }

                //ȡ���������Ļ���
#if NET1
            System.Collections.SortedList __topictypearray = new SortedList();
#else
                Discuz.Common.Generic.SortedList<int, object> __topictypearray = new Discuz.Common.Generic.SortedList<int, object>();
#endif
                __topictypearray = Caches.GetTopicTypeArray();

                DataTable dt = DatabaseProvider.GetInstance().GetExistTopicTypeOfForum();
                DataTable topicTypes = DbHelper.ExecuteDataset(DatabaseProvider.GetInstance().GetTopicTypes()).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    //����������dnt_forumfields���topictypes�ֶ�
                    string topictypes = dr["topictypes"].ToString();
                    if (topictypes.Trim() == "")    //��������б�Ϊ���򲻴���
                        continue;
                    string oldTopicType = GetTopicTypeString(topictypes, __topictypearray[Int32.Parse(id)].ToString().Trim()); //��ȡ�޸�����ǰ�ľ������б�
                    if (oldTopicType == "")    //��������б��в�������ǰҪ�޸ĵ����⣬�򲻴���
                        continue;
                    string newTopicType = oldTopicType.Replace("," + __topictypearray[Int32.Parse(id)].ToString().Trim() + ",", "," + name + ",");
                    topictypes = topictypes.Replace(oldTopicType + "|", ""); //���ɵ������б����̳�����б���ɾ��
                    ArrayList topictypesal = new ArrayList();
                    foreach (string topictype in topictypes.Split('|'))
                    {
                        if (topictype != "")
                            topictypesal.Add(topictype);
                    }
                    bool isInsert = false;
                    for (int i = 0; i < topictypesal.Count; i++)
                    {
                        int curDisplayOrder = GetDisplayOrder(topictypesal[i].ToString().Split(',')[1], topicTypes);
                        if (curDisplayOrder > int.Parse(displayorder))
                        {
                            topictypesal.Insert(i, newTopicType);
                            isInsert = true;
                            break;
                        }
                    }
                    if (!isInsert)
                    {
                        topictypesal.Add(newTopicType);
                    }
                    topictypes = "";
                    foreach (object t in topictypesal)
                    {
                        topictypes += t.ToString() + "|";
                    }
                    DatabaseProvider.GetInstance().UpdateTopicTypeForForum(topictypes, int.Parse(dr["fid"].ToString()));
                    Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/TopicTypesOption" + dr["fid"].ToString());
                    Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/TopicTypesLink" + dr["fid"].ToString());
                }

                //������������(dnt_topictypes)
                DatabaseProvider.GetInstance().UpdateTopicTypes(name, int.Parse(displayorder), description, int.Parse(id));
                rowid++;
            }

            //���»���
            DNTCache cache = DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/TopicTypes");
            if(error)
                base.RegisterStartupScript("", "<script>alert('���ݿ����Ѵ�����ͬ������������ƻ�Ϊ�գ��ü�¼���ܱ����£�');window.location.href='forum_topictypesgrid.aspx';</script>");
            else
                base.RegisterStartupScript("PAGE", "window.location.href='forum_topictypesgrid.aspx';");
            return;
            #endregion
        }

        #region Web ������������ɵĴ���
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.AddNewRec.Click += new EventHandler(this.AddNewRec_Click);
            this.delButton.Click += new EventHandler(this.delButton_Click);
            this.DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid_ItemDataBound);
            this.SaveTopicType.Click += new EventHandler(this.SaveTopicType_Click);
            DataGrid1.ColumnSpan = 5;
        }
        #endregion
    }
}
