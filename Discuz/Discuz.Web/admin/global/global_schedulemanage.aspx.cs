using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Discuz.Forum;

using Discuz.Common;
using Discuz.Config;
using Discuz.Data;

namespace Discuz.Web.Admin
{
#if NET1
    public class global_schedulemanage : AdminPage
#else
    public partial class global_schedulemanage : AdminPage
#endif
    {
#if NET1
		protected System.Web.UI.HtmlControls.HtmlForm form1;

		protected Discuz.Control.RadioButtonList allowpassport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl passportbody;
		protected Discuz.Control.Button DelRec;
        protected System.Web.UI.WebControls.DataGrid DataGrid1;
#endif
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                DataTable dt = new DataTable();
                dt.Columns.Add("key");
                dt.Columns.Add("scheduletype");
                dt.Columns.Add("exetime");
                dt.Columns.Add("lastexecute");
                dt.Columns.Add("issystemevent");
                dt.Columns.Add("enable");
                Discuz.Config.Event[] events = ScheduleConfigs.GetConfig().Events;
                foreach (Discuz.Config.Event ev in events)
                {
                    DataRow dr = dt.NewRow();
                    dr["key"] = ev.Key;
                    dr["scheduletype"] = ev.ScheduleType;
                    if (ev.TimeOfDay != -1)
                    {
                        dr["exetime"] = "��ʱִ��:" + (ev.TimeOfDay / 60) + "ʱ" + (ev.TimeOfDay % 60) + "��";
                    }
                    else
                    {
                        dr["exetime"] = "����ִ��:" + ev.Minutes + "����";
                    }
                    DateTime lastExecute = DatabaseProvider.GetInstance().GetLastExecuteScheduledEventDateTime(ev.Key, Environment.MachineName);
                    if (lastExecute == DateTime.MinValue)
                    {
                        dr["lastexecute"] = "��δִ��";
                    }
                    else
                    {
                        dr["lastexecute"] = lastExecute.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    dr["issystemevent"] = ev.IsSystemEvent ? "ϵͳ��" : "��ϵͳ��";
                    dr["enable"] = ev.Enabled ? "����" : "����";
                    dt.Rows.Add(dr);
                }
                //DataGrid1.TableHeaderName = "�ƻ������б�";
                DataGrid1.DataSource = dt;
                DataGrid1.DataKeyField = "key";
                DataGrid1.DataBind();
               

                #region
                /*APIConfigInfo aci = APIConfigs.GetConfig();
                allowpassport.SelectedValue = aci.Enable ? "1" : "0";
                passportbody.Attributes.Add("style", "display:" + (aci.Enable ? "block" : "none"));
                allowpassport.Items[0].Attributes.Add("onclick", "setAllowPassport(1)");
                allowpassport.Items[1].Attributes.Add("onclick", "setAllowPassport(0)");
                ApplicationInfoCollection appColl = aci.AppCollection;
                DataTable dt = new DataTable();
                dt.Columns.Add("appname");
                dt.Columns.Add("appurl");
                dt.Columns.Add("apikey");
                dt.Columns.Add("secret");
                foreach (ApplicationInfo ai in appColl)
                {
                    DataRow dr = dt.NewRow();
                    dr["appname"] = ai.AppName;
                    dr["appurl"] = ai.AppUrl;
                    dr["apikey"] = ai.APIKey;
                    dr["secret"] = ai.Secret;
                    dt.Rows.Add(dr);
                }
                DataGrid1.TableHeaderName = "���ϳ����б�";
                DataGrid1.DataKeyField = "apikey";
                DataGrid1.DataSource = dt;
                DataGrid1.DataBind();*/
                #endregion
            //}
        }

        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "exec")
            {
                Discuz.Config.Event[] events = ScheduleConfigs.GetConfig().Events;
                foreach (Discuz.Config.Event ev in events)
                {
                    if (ev.Key == e.CommandArgument.ToString())
                    {
                        ((Discuz.Forum.ScheduledEvents.IEvent)Activator.CreateInstance(Type.GetType(ev.ScheduleType))).Execute(HttpContext.Current);
                        DatabaseProvider.GetInstance().SetLastExecuteScheduledEventDateTime(ev.Key, Environment.MachineName, DateTime.Now);
                        break;
                    }
                }
                //base.RegisterStartupScript("exec", "window.location.href=window.location;");
            }
        }


        public void DataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                e.Item.Attributes.Add("onmouseover", "this.className='mouseoverstyle'");
                e.Item.Attributes.Add("onmouseout", "this.className='mouseoutstyle'");
                e.Item.Style["cursor"] = "hand";
            }
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            DataGrid1.CssClass = "datalist";
            DataGrid1.ShowHeader = true;
            DataGrid1.AutoGenerateColumns = false;

            DataGrid1.SelectedItemStyle.CssClass = "datagridSelectedItem";
            DataGrid1.HeaderStyle.CssClass = "category";
            DataGrid1.AutoGenerateColumns = false;

            DataGrid1.BorderWidth = 1;
            DataGrid1.BorderStyle = BorderStyle.Solid;
            DataGrid1.BorderColor = System.Drawing.Color.FromArgb(234, 233, 225);

            DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid_ItemDataBound);
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion
          
    }
}
