using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Foosun.Install
{
    public class Config
    {
        public static string title = null;
        public static string corpRight = null;
        public static string style = null;
        public static string logo = null;
        public static string producename = null;
        public static string regprotocol = null;

        static Config()
        {
            title = "Foosun dotNETCMS v1.0sp3(r)  安装程序";
            corpRight = "<div align=\"center\" style=\"position:relative ; padding-top:20px;font-size:14px; font-family: Arial\">" +
                        "<hr style=\"height:1; width:600; height:1; color:#CCCCCC\" />Powered by Foosun dotNETCMS 1.0_sp3(r) (.net Framework 2.x/3.x)" +
                        " <br />© 2002-2008 <a href=\"http://www.foosu.net\" target=\"_blank\"><b>Foosun Inc.</b></a></div>";
            style = "<LINK rev=\"stylesheet\" media=\"all\" href=\"css/styles.css\" type=\"text/css\" rel=\"stylesheet\">";
            logo = "<img border=\"0\" src=\"image/logo.jpg\">";
            producename = "Foosun dotNETCMS v1.0sp3(r)";
            regprotocol = "<p>版权所有 (c) 2002-2008，四川风讯科技发展有限公司<br />保留所有权利。" +
                          "<p>感谢您选择 Foosun dotNETCMS 产品。希望我们的努力能为您提供一个高效快速和功能强大的大型门户网站解决方案。" +
                          "<p>dotNETCMS 是 Foosun 基于微软最新的Web开发技术ASP.NET的新一代网站内容管理系统，以下简称dotNETCMS。" +
                          "<p>四川风讯科技发展有限公司为 Foosun dotNETCMS 产品的开发商，依法独立拥有 dotNETCMS 产品著作权（中国国家版权局" +
                          "著作权登记号 2004SR11453）。公司网址为 http://www.foosun.net，dotNETCMS 官方站为 http://www.foosun.net，dotNETCMS 官方讨论区为 http://bbs.foosun.net。" +
                          "<p>dotNETCMS 著作权已在中华人民共和国国家版权局注册，著作权受到法律和国际公约保护。使用者：无论个人或组织、盈利" +
                          "与否、用途如何（包括以学习和研究为目的），均需仔细阅读本协议，在理解、同意、并遵守本协议的全部条款后，" +
                          "方可开始使用 dotNETCMS 软件。<p>本授权协议适用且仅适用于 dotNETCMS 1.x 版本，四川风讯科技发展有限" +
                          "公司拥有对本授权协议的最终解释权。<ul type=\"I\"><p><li><b>协议许可的权利</b><ul type=\"1\">" +
                          "<li>您可以在完全遵守本最终用户授权协议的基础上，将本软件应用于非商业用途，而不必支付软件版权授权费用。" +
                          "<li>您可以在协议规定的约束和限制范围内修改 dotNETCMS 源代码(如果被提供的话)或界面风格以适应您的网站要求。" +
                          "<li>您拥有使用本软件构建的论坛中全部会员资料、文章及相关信息的所有权，并独立承担与文章内容的相关法律义务。" +
                          "<li>获得商业授权之后，您可以将本软件应用于商业用途，同时依据所购买的授权类型中确定的技术支持期限、技术支持" +
                          "方式和技术支持内容，自购买时刻起，在技术支持期限内拥有通过指定的方式获得指定范围内的技术支持服务。" +
                          "商业授权用户享有反映和提出意见的权力，相关意见将被作为首要考虑，但没有一定被采纳的承诺或保证。" +
                          "</ul><p><p><li><b>协议规定的约束和限制</b><ul type=\"1\"><li>未获商业授权之前，不得将本软件用于商" +
                          "业用途（包括但不限于企业网站、经营性网站、以营利为目或实现盈利的网站）。购买商业授权请登陆http://www.f" +
                          "oosun.net参考相关说明，也可以致电028-85098980了解详情。<li>不得对本软件或与之关联的商业授权进行" +
                          "出租、出售、抵押或发放子许可证。<li>无论如何，即无论用途如何、是否经过修改或美化、修改程度如何，" +
                          "只要使用 dotNETCMS 的整体或任何部分，未经书面许可，网站" +
                          "页面内的 Foosun、FoosunCMS、dotNETCMS、NewsCMS 和四川风讯科技发展有限公司下属网站（http://www.f" +
                          "oosun.net、http://www.newscms.com、http://www.newsidc.com） 的链接都必须保留，而" +
                          "不能清除或修改。<li>禁止在 dotNETCMS 的整体或任何部分基础上以发展任何派生版本、修改" +
                          "版本或第三方版本用于重新分发。<li>如果您未能遵守本协议的条款，您的授权将被终止，所被许可" +
                          "的权利将被收回，并承担相应法律责任。</ul><p><li><b>有限担保和免责声明</b>" +
                          "<ul type=\"1\"><li>本软件及所附带的文件是作为不提供任何明确的或隐含的赔偿或担保的形式提供的。" +
                          "<li>用户出于自愿而使用本软件，您必须了解使用本软件的风险，在尚未购买产品技术服务之前，我们不" +
                          "承诺提供任何形式的技术支持、使用担保，也不承担任何因使用本软件而产生问题的相关责任。<li>四" +
                          "川风讯科技发展有限公司不对使用本软件构建的网站中的文章或信息承担责任。</ul></ul><p>有关 dotNETCMS " +
                          "最终用户授权协议、商业授权与技术服务的详细内容，均由 dotNETCMS 官方网站独家提供。四川风讯科技发展" +
                          "有限公司拥有在不事先通知的情况下，修改授权协议和服务价目表的权力，修改后的协议或价目表对自改变之日" +
                          "起的新授权用户生效。<p>电子文本形式的授权协议如同双方书面签署的协议一样，具有完全的和等同的法律" +
                          "效力。您一旦开始安装 dotNETCMS，即被视为完全理解并接受本协议的各项条款，在享有上述条款授予的权" +
                          "力的同时，受到相关的约束和限制。协议许可范围以外的行为，将直接违反本授权协议并构成侵权，我们" +
                          "有权随时终止授权，责令停止损害，并保留追究相关责任的权力。";
        }

        public static string InitialSystemValidCheck(ref bool error)
        {
            error = false;
            StringBuilder sb = new StringBuilder();
            sb.Append("<table cellSpacing='0' cellPadding='0' width='90%' align='center' border='0' bgcolor='#666666' style='font-size:12px'>");

            HttpContext context = HttpContext.Current;

            string filename = null;
            if (context != null)
                filename = context.Server.MapPath("/Web.config");
            else
                filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Web.config");

            //系统BIN目录检查
            sb.Append(IISSystemBINCheck(ref error));

            //检查Web.config文件的有效性
            if (!Comm.FileExists(filename) && (!GetRootWebconfigPath()))
            {
                sb.Append("<tr style=\"height:15px\"><td bgcolor='#ffffff' width='5%'><img src='image/error.gif' width='16'" +
                          " height='16'></td><td bgcolor='#ffffff' width='95%'>系统配置文件 Web.config 没有放置正确, " +
                          "相关问题详见安装文档!</td></tr>");
                error = true;
            }
            else
            {
                sb.Append("<tr style=\"height:15px\"><td bgcolor='#ffffff' width='5%'><img src='image/ok.gif' " +
                          "width='16' height='16'></td><td bgcolor='#ffffff' width='95%'>系统配置文件 Web.config 验证通过!</td></tr>");
            }

            string path = HttpContext.Current.Server.MapPath("~/xml/sys/foosun.config");
            System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
            xdoc.Load(path);
            System.Xml.XmlElement root = xdoc.DocumentElement;
            string manageForderName = null;
            string templeForderName = null;
            System.Xml.XmlNodeList elemList = root.GetElementsByTagName("dirMana");
            if (elemList[0] != null)
                manageForderName = elemList[0].InnerText;
            else
                manageForderName = "manage";

            elemList = root.GetElementsByTagName("dirTemplet");
            if (elemList[0] != null)
                templeForderName = elemList[0].InnerText;
            else
                templeForderName = "Templets";

            //检查系统目录的有效性
            string folderstr = "comm,configuration,controls,database,editor,files,history,html,Install,jsfiles,logs," + manageForderName + "," +
                               "stat,survey,sysImages," + templeForderName + ",user,userfiles,www,xml";

            foreach (string foldler in folderstr.Split(','))
            {
                if (!SystemFolderCheck(foldler))
                {
                    sb.Append("<tr><td bgcolor='#ffffff' width='5%'><img src='image/error.gif' width='16' height='16'>" +
                              "</td><td bgcolor='#ffffff' width='95%'>对 " + foldler + " 目录没有写入和删除权限!</td></tr>");
                    error = true;
                }
                else
                {
                    sb.Append("<tr><td bgcolor='#ffffff' width='5%'><img src='image/ok.gif' width='16' height='16'></td>" +
                              "<td bgcolor='#ffffff' width='95%'>对 " + foldler + " 目录权限验证通过!</td></tr>");
                }
            }
            //if (!SerialiazeTest())
            //{
            //    sb.Append("<tr><td bgcolor='#ffffff' width='5%'><img src='image/error.gif' width='16' height='16'></td><td bgcolor='#ffffff' width='95%'>您没有对 " + Path.GetTempPath() + " 文件夹访问权限，详情参见安装文档.</td></tr>");
            //    error = true;
            //}
            //else
            //{
            //    sb.Append("<tr><td bgcolor='#ffffff' width='5%'><img src='image/ok.gif' width='16' height='16'></td><td bgcolor='#ffffff' width='95%'>反序列化验证通过！</td></tr>");
            //}
            sb.Append("</table>");
            return sb.ToString();
        }

        /// <summary>
        /// 检查BIN目录内DLL是否完整
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static string IISSystemBINCheck(ref bool error)
        {
            string binfolderpath = HttpRuntime.BinDirectory;

            string result = "";
            try
            {
                string[] assemblylist = new string[] { "Foosun.CMS.dll", "Foosun.Common.dll", "Foosun.Config.dll", "Foosun.Control.dll" ,
                                                    "Foosun.DALFactory.dll","Foosun.DALProfile.dll","Foosun.Global.dll","Foosun.Model.dll",
                                                    "Foosun.Publish.dll","Foosun.SQLServerDAL.dll","Foosun.Web.dll","Foosun.Web.UI.dll",
                                                    "Intelligencia.UrlRewriter.dll","Interop.ADODB.dll","Interop.JRO.dll",
                                                    "Ajax.dll","CBMD5.dll"};
                foreach (string assembly in assemblylist)
                {
                    if (!File.Exists(binfolderpath + assembly))
                    {
                        result += "<tr><td bgcolor='#ffffff' width='5%'><img src='image/error.gif' width='16' height='16'>" +
                                  "</td><td bgcolor='#ffffff' width='95%'>" + assembly + " 文件放置不正确<br/>请将所有的dll文件复制" +
                                  "到目录 " + binfolderpath + " 中.</td></tr>";
                        error = true;
                    }
                    else
                    {
                        result += "<tr><td bgcolor='#ffffff' width='5%'><img src='image/ok.gif' width='16' height='16'>" +
                                  "</td><td bgcolor='#ffffff' width='95%'>" + assembly + " 文件放置正确.</td></tr>";
                    }
                }
            }
            catch
            {
                result += "<tr><td bgcolor='#ffffff' width='5%'><img src='image/error.gif' width='16' height='16'>" +
                          "</td><td bgcolor='#ffffff' width='95%'>请将所有的dll文件复制到目录 " + binfolderpath + " 中.</td></tr>";
                error = true;
            }
            return result;
        }

        /// <summary>
        /// 检查Web.config
        /// </summary>
        /// <returns></returns>
        public static bool GetRootWebconfigPath()
        {
            try
            {
                HttpContext context = HttpContext.Current;
                string webconfigpath = Path.Combine(context.Request.PhysicalApplicationPath, "web.config");

                //如果文件不存在退出
                if (!Comm.FileExists(webconfigpath))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool SystemFolderCheck(string foldername)
        {
            string physicsPath = Comm.GetMapPath(@"..\" + foldername);
            try
            {
                using (FileStream fs = new FileStream(physicsPath + "\\a.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    fs.Close();
                }

                System.IO.File.Delete(physicsPath + "\\a.txt");

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}