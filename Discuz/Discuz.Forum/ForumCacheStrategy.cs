using System;
using System.Web.Caching;

using Discuz.Common;
using Discuz.Data;

namespace Discuz.Forum
{
	/// <summary>
	/// ForumCacheStrategy ����
	/// ���Ƶ���̳���������, ��ʵ�ֵ���ʱ�����ú���������
	/// </summary>
    public class ForumCacheStrategy : Discuz.Cache.ICacheStrategy
	{

		protected static volatile System.Web.Caching.Cache webCacheforfocus = null;

		private int _timeOut = 20; // Ĭ�ϻ�������Ϊ20����

		/// <summary>
		/// ���캯��
		/// </summary>
		static ForumCacheStrategy()
		{
            webCacheforfocus = Discuz.Cache.DefaultCacheStrategy.GetWebCacheObj;
		}

		
		
		//���õ������ʱ��[��λ:����]
		virtual public int TimeOut
		{
			set { _timeOut = (value<100) ? value : 20; }
			get { return (_timeOut<100) ? _timeOut : 20; }
		}


		
		/// <summary>
		/// ���뵱ǰ���󵽻�����
		/// </summary>
		/// <param name="objId">key for the object</param>
		/// <param name="o">object</param>
		public void AddObject(string objId, object o)
		{
		
			if (objId == null || objId.Length == 0 || o == null) 
			    return;

            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);

            webCacheforfocus.Insert(objId, o, null, DateTime.Now.AddMinutes(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callBack);
		}


		public void AddObjectWith(string objId, object o)
		{
			if (objId == null || objId.Length == 0 || o == null) 
			    return ;
			
			webCacheforfocus.Insert(objId,o,null,System.DateTime.Now.AddHours(TimeOut),	System.Web.Caching.Cache.NoSlidingExpiration);
		}



		/// <summary>
		/// ���뵱ǰ���󵽻�����,��������ļ���������
		/// </summary>
		/// <param name="objId">key for the object</param>
		/// <param name="o">object</param>
		/// <param name="files">���ӵ�·���ļ�</param>
		public void AddObjectWithFileChange(string objId, object o, string[] files)
		{
			if (objId == null || objId.Length == 0 || o == null)
			    return;
		
			CacheDependency dep = new CacheDependency(files, DateTime.Now);

			webCacheforfocus.Insert(objId,o,dep,System.DateTime.Now.AddHours(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration);
		}
	


		/// <summary>
		/// ���뵱ǰ���󵽻�����,��ʹ��������
		/// </summary>
		/// <param name="objId">key for the object</param>
		/// <param name="o">object</param>
		/// <param name="dependKey">���ӵ�·���ļ�</param>
		public void AddObjectWithDepend(string objId, object o, string[] dependKey)
		{
			if (objId == null || objId.Length == 0 || o == null) 
			    return;
			CacheDependency dep = new CacheDependency(null, dependKey, DateTime.Now);
			webCacheforfocus.Insert(objId,o,dep,System.DateTime.Now.AddMinutes(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration);
		}
	
		/// <summary>
		/// ɾ���������
		/// </summary>
		/// <param name="objId">����Ĺؼ���</param>
		public void RemoveObject(string objId)
		{
			if (objId == null || objId.Length == 0)
			    return;
			webCacheforfocus.Remove(objId);
		}


		/// <summary>
		/// ����һ��ָ���Ķ���
		/// </summary>
		/// <param name="objId">����Ĺؼ���</param>
		/// <returns>����</returns>
		public object RetrieveObject(string objId)
		{
			if (objId == null || objId.Length == 0)
			    return null;
			object o = webCacheforfocus.Get(objId);
			return webCacheforfocus.Get(objId);
		}


        //�����ص�ί�е�һ��ʵ��
        public void onRemove(string key, object val, CacheItemRemovedReason reason)
        {

            switch (reason)
            {
               
                case CacheItemRemovedReason.Expired:
                    {
                        break;
                    }
               default: break;
            }

        }

	}


    /// <summary>
    /// RssCacheStrategy ����
    /// Rss���������, ��ʵ�ֵ���ʱ����������
    /// </summary>
    /// </summary>
    public class RssCacheStrategy : ForumCacheStrategy
    {
        private int _timeOut = 60; // Ĭ�ϻ�������Ϊ60����

        //���õ������ʱ��[��λ��������] 
        override public int TimeOut
        {
            set { _timeOut = (value > 0 && value < 9999) ? value : 60; }
            get { return (_timeOut > 0 && _timeOut < 9999) ? _timeOut : 60; }
        }
    }
    
    /// <summary>
    /// SitemapCacheStrategy ����
    /// Sitemap���������, ��ʵ�ֵ���ʱ����������
    /// </summary>
    /// </summary>
    public class SitemapCacheStrategy : ForumCacheStrategy
    {
        private int _timeOut = 12*60; // Ĭ�ϻ�������Ϊ60����

        //���õ������ʱ��[��λ��������] 
        override public int TimeOut
        {
            set { _timeOut = (value > 0 && value < 9999) ? value : 12*60; }
            get { return (_timeOut > 0 && _timeOut < 9999) ? _timeOut : 12*60; }
        }
    }



}
