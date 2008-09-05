using System;
using System.Xml;
using System.Text;
using System.Data;
using System.Data.Common;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;

namespace Discuz.Forum
{
	/// <summary>
	/// ��̳ģ�������
	/// </summary>
	public class Templates
	{
		/// <summary>
		/// ģ��˵���ṹ, ÿ��ģ��Ŀ¼�¾���ʹ��ָ���ṹ��xml�ļ���˵����ģ��Ļ�����Ϣ
		/// </summary>
		public struct TemplateAboutInfo
		{
			/// <summary>
			/// ģ������
			/// </summary>
			public string name;
			/// <summary>
			/// ����
			/// </summary>
			public string author;
			/// <summary>
			/// ��������
			/// </summary>
			public string createdate;
			/// <summary>
			/// ģ��汾
			/// </summary>
			public string ver;
			/// <summary>
			/// ģ�����õ���̳�汾
			/// </summary>
			public string fordntver;
			/// <summary>
			/// ��Ȩ����
			/// </summary>
			public string copyright;

		}


		private static object SynObject=new object();


		/// <summary>
		/// ��ģ������ļ��л��ģ�����ֵ����Ϣ
		/// </summary>
		/// <param name="templatename">ģ������ļ���)</param>
		/// <returns>ģ�������</returns>
		public static DataTable GetTemplateVariable1(string templatename)
		{
				
			///��ű�����Ϣ���ļ� templatevariable.xml�Ƿ����,�����ڷ��ؿձ�
			if (!System.IO.File.Exists(Utils.GetMapPath("../../templates/" +templatename+"/templatevariable.xml")))
			{
				return (DataTable) null;
			}
			else
			{
				using(DataSet ds=new DataSet())
				{
					ds.ReadXml(Utils.GetMapPath("../../templates/" +templatename+"/templatevariable.xml"));
					return ds.Tables[0];
				}
			}

		}


		
		/// <summary>
		/// ��ģ��˵���ļ��л��ģ��˵����Ϣ
		/// </summary>
		/// <param name="xmlPath">ģ��·��(�������ļ���)</param>
		/// <returns>ģ��˵����Ϣ</returns>
		public static TemplateAboutInfo GetTemplateAboutInfo(string xmlPath)
		{
			TemplateAboutInfo __aboutinfo = new TemplateAboutInfo();
			__aboutinfo.name = "";
			__aboutinfo.author = "";
			__aboutinfo.createdate = "";
			__aboutinfo.ver = "";
			__aboutinfo.fordntver = "";
			__aboutinfo.copyright = "";
			
			///��Ź�����Ϣ���ļ� about.xml�Ƿ����,�����ڷ��ؿմ�
			if (!System.IO.File.Exists(xmlPath + @"\about.xml"))
			{
				return __aboutinfo;

			}


			XmlDocument xml = new XmlDocument();

			xml.Load(xmlPath + @"\about.xml");

            try
            {
                XmlNode root = xml.SelectSingleNode("about");
                foreach (XmlNode n in root.ChildNodes)
                {
                    if (n.NodeType != XmlNodeType.Comment && n.Name.ToLower() == "template")
                    {
                        XmlAttribute name = n.Attributes["name"];
                        XmlAttribute author = n.Attributes["author"];
                        XmlAttribute createdate = n.Attributes["createdate"];
                        XmlAttribute ver = n.Attributes["ver"];
                        XmlAttribute fordntver = n.Attributes["fordntver"];
                        XmlAttribute copyright = n.Attributes["copyright"];

                        if (name != null)
                        {
                            __aboutinfo.name = name.Value.ToString();
                        }

                        if (author != null)
                        {
                            __aboutinfo.author = author.Value.ToString();
                        }

                        if (createdate != null)
                        {
                            __aboutinfo.createdate = createdate.Value.ToString();
                        }

                        if (ver != null)
                        {
                            __aboutinfo.ver = ver.Value.ToString();
                        }

                        if (fordntver != null)
                        {
                            __aboutinfo.fordntver = fordntver.Value.ToString();
                        }

                        if (copyright != null)
                        {
                            __aboutinfo.copyright = copyright.Value.ToString();
                        }


                    }
                }
            }
            catch
            {
                __aboutinfo = new TemplateAboutInfo();
            }
			return __aboutinfo;
		}
		
		/// <summary>
		/// ���ǰ̨��Ч��ģ���б�
		/// </summary>
		/// <returns>ģ���б�</returns>
		public static DataTable GetValidTemplateList()
		{
			lock(SynObject)
			{
				Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
				DataTable dt = cache.RetrieveObject("/Forum/TemplateList") as DataTable;
				if (dt == null)
				{
                    dt = DatabaseProvider.GetInstance().GetValidTemplateList();
                    cache.AddObject("/Forum/TemplateList", dt);
				}
				return dt;
			}
		}

	
		/// <summary>
		/// ���ǰ̨��Ч��ģ��ID�б�
		/// </summary>
		/// <returns>ģ��ID�б�</returns>
		public static string GetValidTemplateIDList()
		{
			lock(SynObject)
			{
				Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
				string templateidlist = cache.RetrieveObject("/Forum/TemplateIDList") as string;
				if (templateidlist == null)
				{
                    DataTable dt = DatabaseProvider.GetInstance().GetValidTemplateIDList();
                    StringBuilder sb = new StringBuilder();
					foreach(DataRow dr in dt.Rows)
					{
						sb.Append(",");
						sb.Append(dr[0].ToString());
					}
                   
                    try
                    {
                        if (!Utils.StrIsNullOrEmpty(sb.ToString()))
                        {
                            templateidlist = sb.ToString().Substring(1);
                        }
                        cache.AddObject("/Forum/TemplateIDList", templateidlist);
                    }
                    finally
                    {
                        dt.Dispose();
                    }
				}
	 			return templateidlist;
	     	}
		}

		/// <summary>
		/// ���ָ����ģ����Ϣ
		/// </summary>
		/// <param name="templateid">Ƥ��id</param>
		/// <returns></returns>
		public static TemplateInfo GetTemplateItem(int templateid)
		{
			if (templateid <=0)
			{
				return null;
			}

			TemplateInfo __templateinfo = new TemplateInfo();


			DataRow[] dr = GetValidTemplateList().Select("templateid = " + templateid.ToString());
			if (dr.Length>0)
			{
				__templateinfo.Templateid = Int16.Parse(dr[0]["templateid"].ToString());
				__templateinfo.Name = dr[0]["name"].ToString();
				__templateinfo.Directory = dr[0]["directory"].ToString();
				__templateinfo.Copyright = dr[0]["copyright"].ToString();
			}
			else
			{
				return null;
			}
			return __templateinfo;
		}

	}
}
