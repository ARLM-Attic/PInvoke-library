using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

using Discuz.Common;
using Discuz.Forum;
using Button = Discuz.Control.Button;
using DataGrid = Discuz.Control.DataGrid;
using Discuz.Config;
using Discuz.Data;

using Discuz.Entity;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ����б�
    /// </summary>
    
#if NET1
    public class advsgrid : AdminPage
#else
    public partial class advsgrid : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.Button DelAds;
        protected Discuz.Control.Button SetAvailable;
        protected Discuz.Control.Button SetUnAvailable;
        protected Discuz.Control.DataGrid DataGrid1;
        #endregion
#endif


        #region �ؼ�����


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        public void BindData()
        {
            DataGrid1.AllowCustomPaging = false;
            DataGrid1.BindData(DatabaseProvider.GetInstance().GetAdvertisements());
        }

        protected void Sort_Grid(Object sender, DataGridSortCommandEventArgs e)
        {
            DataGrid1.Sort = e.SortExpression.ToString();
        }

        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.LoadCurrentPageIndex(e.NewPageIndex);
        }


        private void DelAds_Click(object sender, EventArgs e)
        {
            #region ɾ��ָ���Ĺ��

            if (this.CheckCookie())
            {
                if (DNTRequest.GetString("advid") != "")
                {
                    //DatabaseProvider.GetInstance().DeleteAnnouncements(DNTRequest.GetString("advid"));
                    DatabaseProvider.GetInstance().DeleteAdvertisement(DNTRequest.GetString("advid"));

                    Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/Advertisements");

                    base.RegisterStartupScript( "PAGE", "window.location.href='global_advsgrid.aspx';");
                }
                else
                {
                    base.RegisterStartupScript( "", "<script>alert('��δѡ���κ�ѡ��');window.location.href='global_advsgrid.aspx';</script>");
                }
            }

            #endregion
        }

        public string BoolStr(string closed)
        {
            #region ����Ƿ���ЧͼƬ,����ǰ̨��

            if (closed == "1")
            {
                return "<div align=center><img src=../images/OK.gif /></div>";
            }
            else
            {
                return "<div align=center><img src=../images/Cancel.gif /></div>";
            }

            #endregion
        }

        public string ParameterType(string parameters)
        {
            return parameters.Split('|')[0];
        }

        public string TargetsType(string targets)
        {
            #region �����Ͷ�ŷ�Χ�ı�ʶ��ת��Ϊ����

            string result = ""; //���Ͷ�ŷ�Χ�ı�ʶ��
            if (targets.IndexOf("ȫ��") >= 0) return "ȫ��";
            else
            {
                if (targets.IndexOf("��ҳ") >= 0)
                {
                    result = "��ҳ,";
                    targets = targets.Replace("��ҳ,", "");
                }
            }

            if (targets.Trim() != "��ҳ")
            {
                foreach (DataRow dr in DatabaseProvider.GetInstance().GetTargetsForumName(targets))
                {
                    result += dr["name"].ToString() + ",";
                }
            }
            return result.Length > 0 ? result.Substring(0, result.Length - 1) : "";

            #endregion
        }


        private void SetUnAvailable_Click(object sender, EventArgs e)
        {
            #region ���ù���Ϊ��Ч״̬

            if (this.CheckCookie())
            {
                if (DNTRequest.GetString("advid") != "")
                {
                    DatabaseProvider.GetInstance().UpdateAdvertisementAvailable(DNTRequest.GetString("advid"), 0);
                    Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/Advertisements");

                    base.RegisterStartupScript( "PAGE",  "window.location.href='global_advsgrid.aspx';");
                }
                else
                {
                    base.RegisterStartupScript( "", "<script>alert('��δѡ���κ�ѡ��');window.location.href='global_advsgrid.aspx';</script>");
                }
            }

            #endregion
        }


        private void SetAvailable_Click(object sender, EventArgs e)
        {
            #region ���ù���Ϊ��Ч״̬

            if (this.CheckCookie())
            {
                if (DNTRequest.GetString("advid") != "")
                {
                    DatabaseProvider.GetInstance().UpdateAdvertisementAvailable(DNTRequest.GetString("advid"), 1);
                    Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/Advertisements");

                    base.RegisterStartupScript( "PAGE", "window.location.href='global_advsgrid.aspx';");
                }
                else
                {
                    base.RegisterStartupScript( "", "<script>alert('��δѡ���κ�ѡ��');window.location.href='global_advsgrid.aspx';</script>");
                }
            }

            #endregion
        }


        public string GetAdType(string adtype)
        {
            #region �õ��������

            string result = "";
            switch (adtype)
            {
                case "0":
                    result = "ͷ��������";
                    break;
                case "1":
                    result = "β��������";
                    break;
                case "2":
                    result = "ҳ�����ֹ��";
                    break;
                case "3":
                    result = "���ڹ��";
                    break;
                case "4":
                    result = "�������";
                    break;
                case "5":
                    result = "�������";
                    break;
                case "6":
                    result = "Silverlightý����";
                    break;
                case "7":
                    result = "����ͨ�����";
                    break;
                case "8":
                    result = "�������";
                    break;
                case "9":
                    result = "���ٷ������Ϸ����";
                    break;
                case "10":
                    result = "���ٱ༭���������";
                    break;
                default:
                    result = "δ֪";
                    break;
            }
            return result;

            #endregion
        }

        #region Web Form Designer generated code

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SetAvailable.Click += new EventHandler(this.SetAvailable_Click);
            this.SetUnAvailable.Click += new EventHandler(this.SetUnAvailable_Click);
            this.DelAds.Click += new EventHandler(this.DelAds_Click);

            this.Load += new EventHandler(this.Page_Load);

            #region ������

            DataGrid1.TableHeaderName = "����б�";
            DataGrid1.DataKeyField = "advid";
            DataGrid1.ColumnSpan = 12;

            #endregion
        }

        #endregion
    }
}