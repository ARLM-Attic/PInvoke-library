using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;

using Discuz.Common;
using Discuz.Data;

namespace Discuz.Forum
{
	/// <summary>
	/// ��̳ģ��������
	/// </summary>
	public class ForumPageTemplate : PageTemplate
	{

		/// <summary>
		/// �����������
		/// </summary>
		/// <param name="skinName">Ƥ����</param>
		/// <param name="strTemplate">ģ������</param>
		/// <returns></returns>
		public override string ReplaceSpecialTemplate(string forumpath,string skinName,string strTemplate)
		{
			Regex r;
			Match m;
		
			StringBuilder sb = new StringBuilder();
			sb.Append(strTemplate);
    		r = new Regex(@"({([^\[\]/\{\}='\s]+)})", RegexOptions.IgnoreCase|RegexOptions.Multiline|RegexOptions.Compiled);
			for (m = r.Match(strTemplate); m.Success; m = m.NextMatch()) 
			{
				if (m.Groups[0].ToString() == "{forumversion}")
				{
					sb = sb.Replace(m.Groups[0].ToString(), Utils.GetAssemblyVersion());
				}
				else if (m.Groups[0].ToString() == "{forumproductname}")
				{
					sb = sb.Replace(m.Groups[0].ToString(), Utils.GetAssemblyProductName());
				}
			}

			foreach(DataRow dr in GetTemplateVarList(forumpath,skinName).Rows)
			{
				sb = sb.Replace(dr["variablename"].ToString().Trim(), dr["variablevalue"].ToString().Trim());
			}
			return sb.ToString();
		}


		/// <summary>
		/// ��ȡģ������
		/// </summary>
		/// <param name="skinName">Ƥ����</param>
		/// <param name="templateName">ģ����</param>
		/// <param name="nest">Ƕ�״���</param>
		/// <param name="templateid">Ƥ��id</param>
		/// <returns></returns>
		public override string GetTemplate(string forumpath,string skinName, string templateName, int nest,int templateid)
		{
			return base.GetTemplate(forumpath,skinName,templateName,nest,templateid);
		}

		/// <summary>
		/// ���ģ������б�
		/// </summary>
		/// <param name="skinName">Ƥ����</param>
		/// <returns></returns>
		public static DataTable GetTemplateVarList(string forumpath,string skinName)
		{
			Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            DataTable dt = cache.RetrieveSingleObject("/Forum/" + skinName + "/TemplateVariable") as DataTable;

			if(dt != null)
			{
				return dt;
			}
			else
			{
				DataSet dsSrc = new DataSet("template");
				string[] filename = new string[1] {Utils.GetMapPath(forumpath + "templates/" + skinName + "/templatevariable.xml")};
				
				if (Utils.FileExists(filename[0]))
				{
					dsSrc.ReadXml(filename[0]);

                    if (dsSrc.Tables.Count == 0)
                    {
                        DataTable templatevariable = new DataTable("TemplateVariable");
                        templatevariable.Columns.Add("id", System.Type.GetType("System.Int32"));
                        templatevariable.Columns.Add("variablename", System.Type.GetType("System.String"));
                        templatevariable.Columns.Add("variablevalue", System.Type.GetType("System.String"));
                        dsSrc.Tables.Add(templatevariable);
                    }
				}
				else
				{
					DataTable templatevariable = new DataTable("TemplateVariable");
					templatevariable.Columns.Add("id", System.Type.GetType("System.Int32"));
					templatevariable.Columns.Add("variablename", System.Type.GetType("System.String"));
					templatevariable.Columns.Add("variablevalue", System.Type.GetType("System.String"));
					dsSrc.Tables.Add(templatevariable);
				}

                cache.AddSingleObject("/Forum/" + skinName + "/TemplateVariable", dsSrc.Tables[0], filename);
				return dsSrc.Tables[0];
			}
		}

		
	}
}
