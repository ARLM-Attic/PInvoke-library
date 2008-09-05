using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using System.Data.Common;
using Discuz.Forum;
using Discuz.Cache;

namespace Discuz.Web.Admin
{
    public class addtopictype : System.Web.UI.UserControl
    {
        public bool result = true;
        public int maxId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string typename = DNTRequest.GetString("typename");
            string typeorder = DNTRequest.GetString("typeorder");
            string typedescription = DNTRequest.GetString("typedescription");

            //����Ƿ���ͬ���������
            if (DatabaseProvider.GetInstance().IsExistTopicType(typename))
            {
                result = false;
                return;
            }

            //���ӷ��ൽdnt_topictypes,��д��־
            DatabaseProvider.GetInstance().AddTopicTypes(typename, int.Parse(typeorder), typedescription);
            maxId = DatabaseProvider.GetInstance().GetMaxTopicTypesId();
            //���·��໺��
            DNTCache cache = DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/TopicTypes");
            return;
        }
    }
}