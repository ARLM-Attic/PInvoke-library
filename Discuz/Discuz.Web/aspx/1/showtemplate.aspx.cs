using System;
using System.Web;
using System.Data;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;

namespace Discuz.Web
{
    /// <summary>
    /// 模板列表选择
    /// </summary>
    public class showtemplate : PageBase
    {
        /// <summary>
        /// 可用模板列表
        /// </summary>
        public DataTable templatelist;
        protected override void ShowPage()
        {
            pagetitle = "选择模板";

            if (userid == -1 && config.Guestcachepagetimeout > 0)
            {
                AddErrLine("当前的系统设置不允许游客选择模板");
                return;
            }

            int templateid = DNTRequest.GetInt("templateid", 0);
            if (templateid > 0)
            {
                if (!System.IO.Directory.Exists(Utils.GetMapPath("../" + templateid)))
                {
                    AddErrLine("您所选择的模板不存在！");
                    return;
                }
                string strtemplateid = Templates.GetValidTemplateIDList();
                if (!Utils.InArray(templateid.ToString(), strtemplateid))
                {
                    templateid = config.Templateid;
                }
                Utils.WriteCookie(Utils.GetTemplateCookieName(), templateid.ToString(), 999999);

                string rurl = ForumUtils.GetReUrl();
                SetUrl(rurl.IndexOf("logout.aspx") > -1 || rurl.IndexOf("showtemplate.aspx") > -1 ? "index.aspx" : rurl);
                AddMsgLine("切换模板成功, 返回切换模板前页面");
                SetMetaRefresh();
                SetShowBackLink(false);
            }
            else
            {
                templatelist = Templates.GetValidTemplateList();
                if ((DNTRequest.GetUrlReferrer() == "") || (DNTRequest.GetUrlReferrer().IndexOf("showtemplate") > -1))
                {
                    ForumUtils.WriteCookie("reurl", "index.aspx");
                }
                else
                {
                    ForumUtils.WriteCookie("reurl", DNTRequest.GetUrlReferrer());
                }
            }
        }
    }
}