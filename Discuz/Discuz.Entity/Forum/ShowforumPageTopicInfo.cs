using System;

namespace Discuz.Entity
{
	/// <summary>
	/// IndexPageForumInfo ��ժҪ˵����
	/// </summary>
	public class ShowforumPageTopicInfo : TopicInfo
	{
		private string m_folder;

		private string m_topictypename;

		public string Folder
		{
			get { return m_folder;}
			set { m_folder = value;}
		}

		public string Topictypename
		{
			get { return m_topictypename;}
			set { m_topictypename = value;}
		}
	}
}
