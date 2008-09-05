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


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ���ֲ��Ա༭
    /// </summary>

#if NET1
    public class allowparticipatescore : AdminPage
#else
    public partial class allowparticipatescore : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.DataGrid DataGrid1;
        #endregion
#endif


        #region �ؼ�����

        protected Button SetAvailable;

        #endregion

        protected DataTable templateDT = new DataTable("templateDT");


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (DNTRequest.GetString("groupid") != "")
                {
                    BindData();
                }
                else
                {
                    Response.Write("<script>history.go(-1);</script>");
                    Response.End();
                }
            }
        }

        public void BindData()
        {
            #region ������

            DataGrid1.AllowCustomPaging = false;
            DataGrid1.TableHeaderName = "�������ַ�Χ�б�";
            DataGrid1.DataSource = LoadDataInfo();
            DataGrid1.DataBind();

            #endregion
        }

        protected void Sort_Grid(Object sender, DataGridSortCommandEventArgs e)
        {
            DataGrid1.Sort = e.SortExpression.ToString();
        }


        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.LoadCurrentPageIndex(e.NewPageIndex);
            BindData();
        }

        protected void DataGrid_Edit(Object sender, DataGridCommandEventArgs E)
        {
            if (ViewState["validrow"].ToString().IndexOf("," + E.Item.ItemIndex + ",") >= 0)
            {
                DataGrid1.EditItemIndex = (int)E.Item.ItemIndex;
                DataGrid1.DataSource = LoadDataInfo();
                DataGrid1.DataBind();
            }
            else
            {
                base.RegisterStartupScript( "", "<script>alert('����ʧ��,�����޸ĵĻ���������Ч��,��������뿴ע��!');window.location.href='global_allowparticipatescore.aspx?pagename=" + DNTRequest.GetString("pagename") + "&groupid=" + DNTRequest.GetString("groupid") + "';</script>");
                return;
            }
        }

        protected void DataGrid_Cancel(Object sender, DataGridCommandEventArgs E)
        {
            DataGrid1.EditItemIndex = -1;
            DataGrid1.DataSource = LoadDataInfo();
            DataGrid1.DataBind();
        }

        private DataTable LoadDataInfo()
        {
            #region ����������Ϣ

            DataTable dt = DatabaseProvider.GetInstance().GetRaterangeByGroupid(DNTRequest.GetInt("groupid", 0));
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString().Trim() == "")
                {
                    return RemoveEmptyRows(NewParticipateScore());
                }
                else
                {
                    return RemoveEmptyRows(GroupParticipateScore(dt.Rows[0]["raterange"].ToString().Trim()));
                }
            }
            else
            {
                return (DataTable)null;
            }

            #endregion
        }

        private DataTable RemoveEmptyRows(DataTable dt)
        {
            DataRow[] drs = dt.Select("ScoreName=''");
            foreach (DataRow dr in drs)
            {
                dt.Rows.Remove(dr);
            }
            return dt;
        }

        public DataTable NewParticipateScore()
        {
            #region ��ʼ����װ��Ĭ������

            templateDT.Columns.Clear();
            templateDT.Columns.Add("id", Type.GetType("System.Int32"));
            templateDT.Columns.Add("available", Type.GetType("System.Boolean"));
            templateDT.Columns.Add("ScoreCode", Type.GetType("System.String"));
            templateDT.Columns.Add("ScoreName", Type.GetType("System.String"));
            templateDT.Columns.Add("Min", Type.GetType("System.String"));
            templateDT.Columns.Add("Max", Type.GetType("System.String"));
            templateDT.Columns.Add("MaxInDay", Type.GetType("System.String"));

            for (int rowcount = 0; rowcount < 8; rowcount++)
            {
                DataRow dr = templateDT.NewRow();
                dr["id"] = rowcount + 1;
                dr["available"] = false;
                dr["ScoreCode"] = "extcredits" + Convert.ToString(rowcount + 1);
                dr["ScoreName"] = "";
                dr["Min"] = "";
                dr["Max"] = "";
                dr["MaxInDay"] = "";
                templateDT.Rows.Add(dr);
            }
            DataRow scoresetname = Scoresets.GetScoreSet().Rows[0];
            string validrow = "";

            for (int count = 0; count < 8; count++)
            {
                if ((scoresetname[count + 2].ToString().Trim() != "") && (scoresetname[count + 2].ToString().Trim() != "0"))
                {
                    templateDT.Rows[count]["ScoreName"] = scoresetname[count + 2].ToString().Trim();

                    validrow = validrow + "," + count;
                }

                if (IsValidScoreName(count + 1))
                {
                    validrow = validrow + "," + count;
                }
            }
            ViewState["validrow"] = validrow + ",";
            return templateDT;

            #endregion
        }

        public bool IsValidScoreName(int scoreid)
        {
            #region �Ƿ�����Ч�Ļ�������

            bool isvalid = false;

            foreach (DataRow dr in Scoresets.GetScoreSet().Rows)
            {
                if ((dr["id"].ToString() != "1") && (dr["id"].ToString() != "2"))
                {
                    if (dr[scoreid + 1].ToString().Trim() != "0")
                    {
                        isvalid = true;
                        break;
                    }
                }
            }
            return isvalid;

            #endregion
        }

        public DataTable GroupParticipateScore(string raterange)
        {
            #region �����ݿ��еļ�¼������װ���Ĭ������

            NewParticipateScore();

            int i = 0;
            foreach (string raterangestr in raterange.Split('|'))
            {
                if (raterangestr.Trim() != "")
                {
                    string[] scoredata = raterangestr.Split(',');
                    if (scoredata[1].Trim() == "True")
                    {
                        templateDT.Rows[i]["available"] = true;
                    }
                    templateDT.Rows[i]["Min"] = scoredata[4].Trim();
                    templateDT.Rows[i]["Max"] = scoredata[5].Trim();
                    templateDT.Rows[i]["MaxInDay"] = scoredata[6].Trim();
                }
                i++;
            }
            return templateDT;

            #endregion
        }


        protected void DataGrid_Update(Object sender, DataGridCommandEventArgs E)
        {
            #region �༭��صĻ���������Ϣ

            string id = DataGrid1.DataKeys[(int)E.Item.ItemIndex].ToString();
            bool available = ((CheckBox)E.Item.FindControl("available")).Checked;
            string Min = ((TextBox)E.Item.Cells[5].Controls[0]).Text.Trim();
            string Max = ((TextBox)E.Item.Cells[6].Controls[0]).Text.Trim();
            string MaxInDay = ((TextBox)E.Item.Cells[7].Controls[0]).Text.Trim();

            LoadDataInfo();
            int count = Convert.ToInt16(id) - 1;
            if (available)
            {
                templateDT.Rows[count]["available"] = true;
            }
            else
            {
                templateDT.Rows[count]["available"] = false;
            }

            if (Min == "" || Max == "" || MaxInDay == "")
            {
                base.RegisterStartupScript( "", "<script>alert('���ֵ���Сֵ,���ֵ�Լ�24Сʱ�������������Ϊ��.');window.location.href='global_allowparticipatescore.aspx?pagename=" + DNTRequest.GetString("pagename") + "&groupid=" + DNTRequest.GetString("groupid") + "';</script>");
                return;
            }

            if ((Min != "" && !Utils.IsNumeric(Min.Replace("-", ""))) || (Max != "" && !Utils.IsNumeric(Max.Replace("-", ""))) || (MaxInDay != "" && !Utils.IsNumeric(MaxInDay.Replace("-", ""))))
            {
                base.RegisterStartupScript( "", "<script>alert('��������ݱ���������.');window.location.href='global_allowparticipatescore.aspx?pagename=" + DNTRequest.GetString("pagename") + "&groupid=" + DNTRequest.GetString("groupid") + "';</script>");
                return;
            }

            if (Convert.ToInt16(Utils.SBCCaseToNumberic(Min)) >= Convert.ToInt16(Utils.SBCCaseToNumberic(Max)))
            {
                base.RegisterStartupScript( "", "<script>alert('���ֵ���Сֵ����С���������ֵ.');window.location.href='global_allowparticipatescore.aspx?pagename=" + DNTRequest.GetString("pagename") + "&groupid=" + DNTRequest.GetString("groupid") + "';</script>");
                return;
            }

            templateDT.Rows[count]["Min"] = Convert.ToInt16(Utils.SBCCaseToNumberic(Min));
            templateDT.Rows[count]["Max"] = Convert.ToInt16(Utils.SBCCaseToNumberic(Max));
            templateDT.Rows[count]["MaxInDay"] = Convert.ToInt16(Utils.SBCCaseToNumberic(MaxInDay));

            try
            {
                WriteScoreInf(templateDT);
                DataGrid1.EditItemIndex = -1;
                DataGrid1.DataSource = LoadDataInfo();
                DataGrid1.DataBind();
                base.RegisterStartupScript( "PAGE", "window.location.href='global_allowparticipatescore.aspx?pagename=" + DNTRequest.GetString("pagename") + "&groupid=" + DNTRequest.GetString("groupid") + "';");
            }
            catch
            {
                base.RegisterStartupScript( "", "<script>alert('�޷��������ݿ�.');window.location.href='global_allowparticipatescore.aspx?pagename=" + DNTRequest.GetString("pagename") + "&groupid=" + DNTRequest.GetString("groupid") + "';</script>");
                return;
            }

            #endregion
        }


        public void WriteScoreInf(DataTable dt)
        {
            #region �����ݿ���д����������ַ�Χ����

            string scorecontent = "";
            foreach (DataRow dr in dt.Rows)
            {
                scorecontent += dr["id"].ToString() + ",";
                scorecontent += dr["available"].ToString() + ",";
                scorecontent += dr["ScoreCode"].ToString() + ",";
                scorecontent += dr["ScoreName"].ToString() + ",";
                scorecontent += dr["Min"].ToString() + ",";
                scorecontent += dr["Max"].ToString() + ",";
                scorecontent += dr["MaxInDay"].ToString() + "|";
            }
            DatabaseProvider.GetInstance().UpdateRaterangeByGroupid(scorecontent.Substring(0, scorecontent.Length - 1), DNTRequest.GetInt("groupid",0));
            templateDT.Clear();

            AdminCaches.ReSetUserGroupList();

            #endregion
        }


        public bool GetAvailable(string available)
        {
            if (available == "True") return true;
            else return false;
        }

        public string GetImgLink(string available)
        {
            if (available == "True")
            {
                return "<div align=center><img src=../images/OK.gif /></div>";
            }
            else
            {
                return "<div align=center><img src=../images/Cancel.gif /></div>";
            }
        }

        private void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            #region ���ݰ���ʾ���ȿ���

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                TextBox t = (TextBox)e.Item.Cells[5].Controls[0];
                t.Attributes.Add("maxlength", "3");
                t.Attributes.Add("size", "4");

                t = (TextBox)e.Item.Cells[6].Controls[0];
                t.Attributes.Add("maxlength", "2");
                t.Attributes.Add("size", "3");

                t = (TextBox)e.Item.Cells[7].Controls[0];
                t.Attributes.Add("maxlength", "4");
                t.Attributes.Add("size", "4");
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
            this.DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid_ItemDataBound);

            DataGrid1.LoadEditColumn();
        }

        #endregion
    }
}