using System.Text;

using Discuz.Space.Entities;
using Discuz.Config;

namespace Discuz.Space.Modules
{

	public class BlogModule : ModuleBase
	{


		protected StringBuilder output = new StringBuilder();

		public string modulename = "";

        //不带文件名的forumurl地址
        protected string forumurlnopage = "../";

        protected string forumurl = GeneralConfigs.GetConfig().Forumurl;

		public BlogModule()
		{
			output.Append("<script src=\"" + BaseConfigs.GetForumPath + "space/manage/js/AjaxHelper.js\" type=\"text/javascript\"></script>\r\n");

            if (forumurl.ToLower().IndexOf("http://") == 0)
            {
                //去掉http地址中的文件名称
                forumurlnopage = forumurl.Substring(0, forumurl.LastIndexOf('/')) + "/";
            }
            else
            {
                forumurl = "../" + forumurl;
            }

		}

		
		protected void IsScalableAndEditable()
		{
			
			if(this.Module.Uid != this.UserID)
			{
				//this.Scalable =  false;
				this.Editable = false;
			}
			else
			{
				//this.Scalable =  true;
				this.Editable = true;
			}

			
		}
	}
}
