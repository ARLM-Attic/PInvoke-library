using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.CMS;
using Foosun.Model;

namespace Foosun.Web.customform
{
    public partial class CustomFormSubmit : Foosun.Web.UI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["CustomFormID"] != null)
            {
                int cfid = int.Parse(Request.Form["CustomFormID"]);
                CustomForm cf = new CustomForm();
                CustomFormInfo FormInfo;
                IList<CustomFormItemInfo> list = cf.GetAllInfo(cfid, out FormInfo);
                int n = list.Count;
                SQLConditionInfo[] Param = new SQLConditionInfo[n];
                if (FormInfo.isdelpoint != 0)
                {
                    if(Foosun.Global.Current.IsTimeout())
                        PageError("只有会员才能提交,请先登录系统。", "/user/login.aspx");
                }
                if (FormInfo.showvalidatecode)
                {
                    //
                    if(Session["CheckCode"] == null)
                        PageError("校验码已超时。", "");
                    string CheckCode = Session["CheckCode"].ToString().ToUpper();
                    Session.Remove("CheckCode");
                    if (Request.Form["CFValidate"] == null)
                        PageError("校验码未填写。", "");
                    if(Request.Form["CFValidate"].ToUpper() != CheckCode)
                        PageError("校验码错误。", "");                    
                }
                for (int j = 0; j < n; j++)
                {
                    CustomFormItemInfo info = list[j];
                    string CtrName = "CF_" + info.fieldname;
                    if ((info.itemtype == EnumCstmFrmItemType.UploadFile))
                    {
                        if (Request.Files[CtrName] == null)
                        {
                            PageError("表单项不完整,缺少表单项:" + info.itemname + "。", "");
                        }
                    }
                    else
                    {
                        if (Request.Form[CtrName] == null)
                        {
                            PageError("表单项不完整,缺少表单项:" + info.itemname + "。", "");
                        }
                    }
                    if (Request.Form[CtrName]== string.Empty && info.isnotnull)
                    {
                        PageError(info.itemname + "必须填写(选择)。", "");
                    }
                    if (info.itemtype == EnumCstmFrmItemType.UploadFile)
                    {
                        HttpPostedFile upfl = Request.Files[CtrName];
                        if (!(upfl.FileName==""||upfl.ContentLength<=0))
                        {
                            
                            string path = FormInfo.accessorypath;
                            if ((FormInfo.accessorysize*1024) != 0 && upfl.ContentLength > (FormInfo.accessorysize*1024))
                            {
                                PageError(info.itemname + ",上传的文件大小超出限制!", "");
                            }
                            if (path == null || path.Trim() == string.Empty)
                            {
                                PageError(info.itemname + ",上传的文件的保存路径未指定!", "");
                            }
                            string phpath = Server.MapPath(path);
                            if (!Directory.Exists(phpath))
                                Directory.CreateDirectory(phpath);
                            phpath = phpath.TrimEnd('\\');
                            string flnm = upfl.FileName;
                            int tmpFEXint = flnm.LastIndexOf('.');
                            string TmpFEX = flnm.Substring((tmpFEXint));
                            string FileNames = Foosun.Common.Rand.Number(6);
                            int i = 1;
                            while (File.Exists(phpath + "\\" + FileNames + TmpFEX))
                            {
                                int pos = flnm.LastIndexOf('.');
                                flnm = flnm.Substring(0, pos) + i + flnm.Substring(pos);
                                i++;
                            }
                            upfl.SaveAs(phpath + "\\" + FileNames + TmpFEX);
                            Param[j] = new SQLConditionInfo(info.fieldname, phpath + "\\" + FileNames + TmpFEX);
                        }
                        else
                        {
                            if (info.isnotnull)
                                PageError(info.itemname + ",必须选择上传的文件。", "");
                            Param[j] = new SQLConditionInfo(info.fieldname, string.Empty);
                        }
                    }
                    else
                    {
                        object obj = DBNull.Value;
                        string req = Request.Form[CtrName].Trim();
                        if (req != string.Empty)
                        {
                            if (info.itemtype == EnumCstmFrmItemType.DateTime)
                                try
                                {
                                    obj = DateTime.Parse(Request.Form[CtrName]);
                                }
                                catch
                                {
                                    PageError("时间格式不正确。", "");
                                }
                            else if (info.itemtype == EnumCstmFrmItemType.Numberic)
                                obj = double.Parse(Request.Form[CtrName]);
                            else
                                obj = req;
                        }
                        Param[j] = new SQLConditionInfo(info.fieldname, obj);
                    }
                }
                cf.AddRecord(cfid, Param);
                PageRight("数据添加成功!", "");
            }
        }
    }
}
