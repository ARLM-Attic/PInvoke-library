using System;

namespace Discuz.Forum
{
	/// <summary>
	/// AdminTopicOperationFactory ��ժҪ˵����
	/// ��̨���������
	/// </summary>
	public class AdminTopicOperations : Discuz.Forum.TopicAdmins
	{
		public AdminTopicOperations()
		{
		}

		public static int DeleteAttachmentByTid(string topicidlist)
		{
			return Discuz.Forum.Attachments.DeleteAttachmentByTid(topicidlist);
		}
	}
}
