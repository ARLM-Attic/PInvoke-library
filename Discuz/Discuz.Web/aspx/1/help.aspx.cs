using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Discuz.Config;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Common;

namespace Discuz.Web
{
    public class help : PageBase
    {
        #region ҳ�����
        /// <summary>
        /// �����б�
        /// </summary>
        public ArrayList helplist;

        #region DLL�ļ��İ汾��Ϣ
      
        public string dllver_discuzaggregation = "";
        public string dllver_discuzcache = "";
        public string dllver_discuzcommon = "";
        public string dllver_discuzconfig = "";
        public string dllver_discuzcontrol = "";
        public string dllver_discuzdata = "";
        public string dllver_discuzdatasqlserver = "";
        public string dllver_discuzdataaccess = "";
        public string dllver_discuzdatamysql = "";
        public string dllver_discuzentity = "";
        public string dllver_discuzforum = "";
        public string dllver_discuzplugin = "";
        public string dllver_discuzpluginsysmail = "";
        public string dllver_discuzspace = "";
        public string dllver_discuzweb = "";
        public string dllver_discuzwebui = "";

        #endregion

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public string dbtype = BaseConfigs.GetDbType;
        
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string assemblyproductname = Utils.GetAssemblyProductName();
        
        /// <summary>
        /// ��Ȩ
        /// </summary>
        public string Assemblycopyright = Utils.GetAssemblyCopyright();

        /// <summary>
        /// ��ʾ�汾��Ϣ
        /// </summary>
        public int showversion = DNTRequest.GetInt("version", 0);
        #endregion

        protected override void ShowPage()
        {
            pagetitle = "����";

            int helpid = DNTRequest.GetInt("hid", 0);
            if (helpid > 0)
            {
                helplist = Helps.GetHelpList(helpid);
            }
            else
            {
                helplist = Helps.GetHelpList();
            }
            if (helplist == null)
            {
                AddErrLine("û����Ϣ�ɶ�ȡ��");
                return;
            }

            if (showversion == 1)
            {
                string filepath = HttpRuntime.BinDirectory;
                dllver_discuzaggregation = LoadDllVersion(filepath + "Discuz.Aggregation.dll");
                dllver_discuzcache = LoadDllVersion(filepath + "Discuz.Cache.dll");
                dllver_discuzcommon = LoadDllVersion(filepath + "Discuz.Common.dll");
                dllver_discuzconfig = LoadDllVersion(filepath + "Discuz.Config.dll");
                dllver_discuzcontrol = LoadDllVersion(filepath + "Discuz.Control.dll");
                dllver_discuzdata = LoadDllVersion(filepath + "Discuz.Data.dll");
                dllver_discuzdatasqlserver = LoadDllVersion(filepath + "Discuz.Data.SqlServer.dll");
                dllver_discuzdataaccess = LoadDllVersion(filepath + "Discuz.Data.Access.dll");
                dllver_discuzdatamysql = LoadDllVersion(filepath + "Discuz.Data.MySql.dll");
                dllver_discuzentity = LoadDllVersion(filepath + "Discuz.Entity.dll");
                dllver_discuzforum = LoadDllVersion(filepath + "Discuz.Forum.dll");
                dllver_discuzplugin = LoadDllVersion(filepath + "Discuz.Plugin.dll");
                dllver_discuzpluginsysmail = LoadDllVersion(filepath + "Discuz.Plugin.SysMail.dll");
                dllver_discuzspace = LoadDllVersion(filepath + "Discuz.Space.dll");
                dllver_discuzweb = LoadDllVersion(filepath + "Discuz.Web.dll");
                dllver_discuzwebui = LoadDllVersion(filepath + "Discuz.Web.UI.dll");
            }
        }

        /// <summary>
        /// ��ȡָ��DLL�ļ��İ汾��Ϣ
        /// </summary>
        /// <param name="fullfilename"></param>
        /// <returns></returns>
        private string LoadDllVersion(string fullfilename)
        {
            try
            {
                FileVersionInfo AssemblyFileVersion = FileVersionInfo.GetVersionInfo(fullfilename);
                return string.Format("{0}.{1}.{2}.{3}", AssemblyFileVersion.FileMajorPart, AssemblyFileVersion.FileMinorPart, AssemblyFileVersion.FileBuildPart, AssemblyFileVersion.FilePrivatePart);
            }
            catch
            {
                return "δ�ܼ���dll���dll�ļ�������!";
            }
        }
    }
}