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

namespace Discuz.Web.Admin
{
#if NET1
    public class global_schedulesetting : AdminPage
#else
    public partial class global_schedulesetting : AdminPage
#endif
    {
#if NET1
		protected System.Web.UI.HtmlControls.HtmlForm form1;

		protected Discuz.Control.TextBox appname;
		protected Discuz.Control.TextBox appurl;
		protected Discuz.Control.TextBox callbackurl;
		protected Discuz.Web.Admin.TextareaResize ipaddresses;
		protected System.Web.UI.HtmlControls.HtmlInputHidden apikeyhidd;
		protected Discuz.Control.Button savepassportinfo;
		protected Discuz.Control.Hint hint1;
#endif
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                type1.Attributes.Add("onclick", "changetimespan(this.value)");
                type2.Attributes.Add("onclick", "changetimespan(this.value)");
                for (int i = 0; i < 24; i++)
                {
                    hour.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
                }
                for (int i = 0; i < 60; i++)
                {
                    minute.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
                }
                if (DNTRequest.GetString("keyid") != "")
                {
                    ScheduleConfigInfo sci = ScheduleConfigs.GetConfig();
                    foreach (Discuz.Config.Event ev1 in sci.Events)
                    {
                        if (ev1.Key == DNTRequest.GetString("keyid"))
                        {
                            oldkey.Value = ev1.Key;
                            key.Text = ev1.Key;
                            key.Enabled = !ev1.IsSystemEvent;
                            scheduletype.Text = ev1.ScheduleType;
                            scheduletype.Enabled = !ev1.IsSystemEvent;
                            timeserval.HintInfo = "����ִ��ʱ����,��Сֵ:" + sci.TimerMinutesInterval + "����.�������ֵС����Сֵ,��ȡ��Сֵ";
                            if (ev1.TimeOfDay != -1)
                            {
                                int _hour = ev1.TimeOfDay / 60;
                                int _minute = ev1.TimeOfDay % 60;
                                type1.Checked = true;
                                hour.SelectedValue = _hour.ToString();
                                minute.SelectedValue = _minute.ToString();
                                hour.Enabled = true;
                                minute.Enabled = true;
                                timeserval.Enabled = false;
                            }
                            else
                            {
                                type2.Checked = true;
                                timeserval.Text = ev1.Minutes.ToString();
                                hour.Enabled = false;
                                minute.Enabled = false;
                                timeserval.Enabled = true;
                            }
                            if (!ev1.IsSystemEvent)
                            {
                                eventenabletr.Visible = true;
                                if (ev1.Enabled)
                                    eventenable.Items[0].Selected = true;
                                else
                                    eventenable.Items[1].Selected = true;
                            }
                        }
                    }
                }

                #region
                /*string apikey = DNTRequest.GetString("apikey");
                if (apikey != "")
                {
                    APIConfigInfo aci = APIConfigs.GetConfig();
                    foreach (ApplicationInfo ai in aci.AppCollection)
                    {
                        if (ai.APIKey == apikey)
                        {
                            appname.Text = ai.AppName;
                            appurl.Text = ai.AppUrl;
                            callbackurl.Text = ai.CallbackUrl;
                            ipaddresses.Text = ai.IPAddresses;
                            break;
                        }
                    }
                }
                apikeyhidd.Value = apikey;*/
                #endregion
            }
        }

        protected void savepassportinfo_Click(object sender, EventArgs e)
        {
            ScheduleConfigInfo sci = ScheduleConfigs.GetConfig();
            if (key.Text.Trim() == "")
            {
                base.RegisterStartupScript("PAGE", "alert('�ƻ��������Ʋ���Ϊ��!');");
                return;
            }
            if (scheduletype.Text.Trim() == "")
            {
                base.RegisterStartupScript("PAGE", "alert('�ƻ��������Ͳ���Ϊ��!');");
                return;
            }
            if (type2.Checked && (timeserval.Text == "" || !Utils.IsNumeric(timeserval.Text)))
            {
                base.RegisterStartupScript("PAGE", "alert('����ִ��ʱ�����Ϊ��ֵ!');");
                return;
            }
            if (DNTRequest.GetString("keyid") == "")
            {
                foreach (Discuz.Config.Event ev1 in sci.Events)
                {
                    if (ev1.Key == key.Text.Trim())
                    {
                        base.RegisterStartupScript("PAGE", "alert('�ƻ����������Ѿ�����!');");
                        return;
                    }
                }
                Discuz.Config.Event ev = new Discuz.Config.Event();
                ev.Key = key.Text;
                ev.Enabled = true;
                ev.IsSystemEvent = false;
                ev.ScheduleType = scheduletype.Text.Trim() ;
                if (type1.Checked)
                {
                    ev.TimeOfDay = int.Parse(hour.Text) * 60 + int.Parse(minute.Text);
                    ev.Minutes = sci.TimerMinutesInterval;
                }
                else
                {
                    ev.Minutes = int.Parse(timeserval.Text.Trim());
                    ev.TimeOfDay = -1;
                }
                Discuz.Config.Event[] es = new Discuz.Config.Event[sci.Events.Length + 1];
                for (int i = 0; i < sci.Events.Length; i++)
                {
                    es[i] = sci.Events[i];
                }
                es[es.Length - 1] = ev;
                sci.Events = es;
            }
            else
            {
                foreach (Discuz.Config.Event ev1 in sci.Events)
                {
                    if (key.Text.Trim() != oldkey.Value && ev1.Key == key.Text.Trim())
                    {
                        base.RegisterStartupScript("PAGE", "alert('�ƻ����������Ѿ�����!');");
                        return;
                    }
                }
                foreach (Discuz.Config.Event ev1 in sci.Events)
                {
                    if (ev1.Key == oldkey.Value)
                    {
                        ev1.Key = key.Text.Trim();
                        ev1.ScheduleType = scheduletype.Text.Trim();
                        if (type1.Checked)
                        {
                            ev1.TimeOfDay = int.Parse(hour.Text) * 60 + int.Parse(minute.Text);
                            ev1.Minutes = sci.TimerMinutesInterval;
                        }
                        else
                        {
                            if (int.Parse(timeserval.Text.Trim()) < sci.TimerMinutesInterval)
                                ev1.Minutes = sci.TimerMinutesInterval;
                            else
                                ev1.Minutes = int.Parse(timeserval.Text.Trim());
                            ev1.TimeOfDay = -1;
                        }
                        if (!ev1.IsSystemEvent)
                        {
                            if (eventenable.Items[0].Selected)
                                ev1.Enabled = true;
                            else
                                ev1.Enabled = false;
                        }
                        break;
                    }
                }
            }
            ScheduleConfigs.SaveConfig(sci);
            Response.Redirect("global_schedulemanage.aspx");
            #region
            /*if (appname.Text.Trim() == "")
            {
                base.RegisterStartupScript("PAGE", "alert('���ϳ������Ʋ���Ϊ��!');");
                return;
            }
            if (appurl.Text.Trim() == "")
            {
                base.RegisterStartupScript("PAGE", "alert('���ϳ��� Url ��ַ����Ϊ��!');");
                return;
            }
            if (callbackurl.Text.Trim() == "")
            {
                base.RegisterStartupScript("PAGE", "alert('��¼��ɺ󷵻ص�ַ����Ϊ��!');");
                return;
            }
            if (ipaddresses.Text.Trim() != "")
            {
                foreach (string ip in ipaddresses.Text.Replace("\r\n","").Replace(" ","").Split(','))
                {
                    if (!Utils.IsIP(ip))
                    {
                        base.RegisterStartupScript("PAGE", "alert('IP��ַ��ʽ����!');");
                        return;
                    }
                }
            }
            if (apikeyhidd.Value == "") //����
            {
                ApplicationInfo ai = new ApplicationInfo();
                ai.AppName = appname.Text;
                ai.AppUrl = appurl.Text;
                ai.APIKey = Utils.MD5(System.Guid.NewGuid().ToString());
                ai.Secret = Utils.MD5(System.Guid.NewGuid().ToString());
                ai.CallbackUrl = callbackurl.Text;
                ai.IPAddresses = ipaddresses.Text.Replace("\r\n","").Replace(" ","");
                APIConfigInfo aci = APIConfigs.GetConfig();
                if (aci.AppCollection == null)
                    aci.AppCollection = new ApplicationInfoCollection();
                aci.AppCollection.Add(ai);
                APIConfigs.SaveConfig(aci);
            }
            else   //�޸�
            {
                APIConfigInfo aci = APIConfigs.GetConfig();
                foreach (ApplicationInfo ai in aci.AppCollection)
                {
                    if (ai.APIKey == apikeyhidd.Value)
                    {
                        ai.AppName = appname.Text;
                        ai.AppUrl = appurl.Text;
                        ai.CallbackUrl = callbackurl.Text;
                        ai.IPAddresses = ipaddresses.Text.Replace("\r\n","").Replace(" ","");
                        break;
                    }
                }
                APIConfigs.SaveConfig(aci);
            }
            Response.Redirect("global_passportmanage.aspx");*/
            #endregion
        }
    }
}
