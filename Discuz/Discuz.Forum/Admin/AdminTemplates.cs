using System;
using System.IO;
using System.Data;
using System.Data.Common;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;

namespace Discuz.Forum
{
	/// <summary>
	/// ��̨ģ�������
	/// </summary>
	public class AdminTemplates : Templates
	{

		/// <summary>
		/// ����µ�ģ����
		/// </summary>
		/// <param name="templateName">ģ������</param>
		/// <param name="directory">ģ���ļ�����Ŀ¼</param>
		/// <param name="copyright">ģ���Ȩ����</param>
		/// <returns>ģ��id</returns>
		public static int CreateTemplateItem(string templateName, string directory, string copyright)
		{
            return DatabaseProvider.GetInstance().AddTemplate(templateName, directory, copyright);	
        }



		/// <summary>
		/// ɾ��ָ����ģ����
		/// </summary>
		/// <param name="templateid">ģ��id</param>
		public static void DeleteTemplateItem(int templateid)
		{
            DatabaseProvider.GetInstance().DeleteTemplateItem(templateid);
		}



		/// <summary>
		/// ɾ��ָ����ģ�����б�,
		/// </summary>
		/// <param name="templateidlist">��ʽΪ�� 1,2,3</param>
		public static void DeleteTemplateItem(string templateidlist)
		{
            DatabaseProvider.GetInstance().DeleteTemplateItem(templateidlist);
		}



		/// <summary>
		/// ���������ģ��Ŀ¼�µ�ģ���б�(��:��Ŀ¼����)
		/// </summary>
		/// <param name="templatePath">ģ������·��</param>
		/// <example>GetAllTemplateList(Utils.GetMapPath(@"..\..\templates\"))</example>
		/// <returns>ģ���б�</returns>
		public static DataTable GetAllTemplateList(string templatePath)
		{
            DataTable dt = DatabaseProvider.GetInstance().GetAllTemplateList(templatePath);

			dt.Columns.Add("valid", Type.GetType("System.Int16"));
			foreach (DataRow dr in dt.Rows)
			{
				dr["valid"] = 1;
			}

			DirectoryInfo dirinfo = new DirectoryInfo(templatePath);

            int count = DatabaseProvider.GetInstance().GetMaxTemplateId() + 1;
			foreach (DirectoryInfo dir in dirinfo.GetDirectories())
			{
				if (dir != null)
				{
					bool itemexist = false;
					foreach (DataRow dr in dt.Rows)
					{
						if (dr["directory"].ToString() == dir.Name)
						{
							itemexist = true;
							break;
						}
					}
					if (!itemexist)
					{
						DataRow dr = dt.NewRow();
						// ��Ŀ¼��
						dr["templateid"] = count.ToString();
						dr["directory"] = dir.Name;
						// �Ƿ���ǰ̨��Чģ��
						dr["valid"] = 0;

						TemplateAboutInfo __aboutinfo = GetTemplateAboutInfo(dir.FullName);
						// ģ������
						dr["name"] = __aboutinfo.name;
						// ����
						dr["author"] = __aboutinfo.author;
						// ��������
						dr["createdate"] = __aboutinfo.createdate;
						// ģ��汾
						dr["ver"] = __aboutinfo.ver;
						// ���õ���̳�汾
						dr["fordntver"] = __aboutinfo.fordntver;
						// ��Ȩ
						dr["copyright"] = __aboutinfo.copyright;
						dt.Rows.Add(dr);
						count++;
					}

				}
			}

			dt.AcceptChanges();

			return dt;
		}


	}
}