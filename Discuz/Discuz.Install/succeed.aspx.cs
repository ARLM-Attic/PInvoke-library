using System;
using Discuz.Install;

namespace Discuz.Install
{
    public class succeed : SetupPage
    {
        /// <summary>
        /// 是否显示插件安装链接
        /// </summary>
        public bool showInstallPluginLink = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //插件是否可以安装检测
                if (Discuz.Plugin.Space.SpacePluginProvider.GetInstance() != null ||
                    Discuz.Plugin.Album.AlbumPluginProvider.GetInstance() != null ||
                    Discuz.Plugin.Mall.MallPluginProvider.GetInstance() != null)
                {
                    showInstallPluginLink = true;
                    return;
                }
            }
        }

        
      
    }
}
