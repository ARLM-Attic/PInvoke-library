using System;
using System.Data;
using System.Data.Common;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;

namespace Discuz.Forum
{
    /// <summary>
    /// TopicStats ��ժҪ˵����
    /// </summary>
    public class TopicStats
    {
        private TopicStats()
        {
        }

#if NET1
        static TopicViewCollection queuedStatsList = null;
#else
        static TopicViewCollection<TopicView> queuedStatsList = null;
#endif

        static int queuedAllowCount = 20;

        static TopicStats()
        {
            if (GeneralConfigs.GetConfig().TopicQueueStats == 1)
            {
                SetQueueCount();
            }

        }


        /// <summary>
        /// ׷�������
        /// </summary>
        /// <param name="tv">�������������</param>
        /// <returns>�ɹ�����true</returns>
        public static bool Track(TopicView tv)
        {
            if (tv == null)
            {
                return false;
            }
            if (queuedStatsList == null)
            {
                SetQueueCount();
            }
            if (GeneralConfigs.GetConfig().TopicQueueStats == 1)
            {
                return AddQuedStats(tv);
            }
            else
            {
                return TrackTopic(tv);
            }
        }

        /// <summary>
        /// ׷�������
        /// </summary>
        /// <param name="tid">����id</param>
        /// <param name="viewcount">�����</param>
        /// <returns>�ɹ�����true</returns>
        public static bool Track(int tid, int viewcount)
        {
            if (tid < 1 || viewcount < 1)
                return false;
            TopicView tv = new TopicView();
            tv.TopicID = tid;
            tv.ViewCount = viewcount;
            return Track(tv);
        }


        #region Data

        /// <summary>
        /// �������������
        /// </summary>
        /// <param name="tid">����id</param>
        /// <param name="viewcount">�����</param>
        /// <returns>�ɹ�����1�����򷵻�0</returns>
        public static int UpdateTopicViewCount(int tid, int viewcount)
        {
            return DatabaseProvider.GetInstance().UpdateTopicViewCount(tid, viewcount);
        }

        /// <summary>
        /// ׷������
        /// </summary>
        /// <param name="tv">�����������</param>
        /// <returns>�ɹ�����true</returns>
        public static bool TrackTopic(TopicView tv)
        {
            return UpdateTopicViewCount(tv.TopicID, tv.ViewCount) == 1;
        }


        #endregion


        #region �������뷽��

        /// <summary>
        /// ���ö��г���
        /// </summary>
        public static void SetQueueCount()
        {
            if (GeneralConfigs.GetConfig().TopicQueueStatsCount > 20 && GeneralConfigs.GetConfig().TopicQueueStatsCount <= 1000)
            {
                queuedAllowCount = GeneralConfigs.GetConfig().TopicQueueStatsCount;
            }

            if (queuedStatsList == null)
            {
#if NET1
                queuedStatsList = new TopicViewCollection();
#else
                queuedStatsList = new TopicViewCollection<TopicView>();
#endif
            }

        }

        /// <summary>
        /// ������У��������������������
        /// </summary>
        /// <param name="save"></param>
        /// <returns></returns>
        public static bool ClearQueue(bool save)
        {
            lock (queuedStatsList.SyncRoot)
            {
                if (save)
                {
                    TopicView[] eva = new TopicView[queuedStatsList.Count];
                    queuedStatsList.CopyTo(eva, 0);

#if NET1
                    ClearTrackTopicQueue(new TopicViewCollection(eva));
#else
                    ClearTrackTopicQueue(new TopicViewCollection<TopicView>(eva));
#endif

                }
                queuedStatsList.Clear();
            }
            return true;
        }

        /// <summary>
        /// �����������ͳ�ƶ���
        /// </summary>
        /// <param name="tv">�������������</param>
        /// <returns>�ɹ�����true</returns>
        public static bool AddQuedStats(TopicView tv)
        {
            if (tv == null)
                return false;
            if (queuedAllowCount != GeneralConfigs.GetConfig().TopicQueueStatsCount || queuedStatsList == null)
            {
                SetQueueCount();
            }

            //Check for the limit
            if (queuedStatsList.ViewCount >= queuedAllowCount || queuedStatsList.Count >= 5)
            {
                //aquire the lock 
                lock (queuedStatsList.SyncRoot)
                {
                    //make sure the pool queue was not cleared during a wait for the lock
                    if (queuedStatsList.ViewCount >= queuedAllowCount || queuedStatsList.Count >= 5)
                    {
                        TopicView[] tva = new TopicView[queuedStatsList.Count];
                        queuedStatsList.CopyTo(tva, 0);

#if NET1
                        ClearTrackTopicQueue(new TopicViewCollection(tva));
#else
                        ClearTrackTopicQueue(new TopicViewCollection<TopicView>(tva));
#endif

                        queuedStatsList.Clear();
                        queuedStatsList.ViewCount = 0;

                    }
                }
            }

            bool inArray = false;
            foreach (TopicView curtv in queuedStatsList)
            {
                if (curtv.TopicID == tv.TopicID)
                {
                    curtv.ViewCount = curtv.ViewCount + tv.ViewCount;
                    inArray = true;
                    break;
                }
            }

            if (!inArray)
                queuedStatsList.Add(tv);
            queuedStatsList.ViewCount = queuedStatsList.ViewCount + 1;
            return true;
        }

        /// <summary>
        /// ���׷������Ķ���
        /// </summary>
        /// <param name="tvc">�����������</param>
        /// <returns>�ɹ�����true</returns>
#if NET1
        private static bool ClearTrackTopicQueue(TopicViewCollection tvc)
#else
        private static bool ClearTrackTopicQueue(TopicViewCollection<TopicView> tvc)
#endif
        {
            ProcessStats ps = new ProcessStats(tvc);
            ps.Enqueue();

            return true;
        }


        /// <summary>
        /// ׷������
        /// </summary>
        /// <param name="tvc">�����������</param>
        /// <returns>�ɹ�����true</returns>
#if NET1
        public static bool TrackTopic(TopicViewCollection tvc)
#else
        public static bool TrackTopic(TopicViewCollection<TopicView> tvc)
#endif
        {
            if (tvc == null)
                return false;

            foreach (TopicView tv in tvc)
            {
                UpdateTopicViewCount(tv.TopicID, tv.ViewCount);
            }
            return true;
        }

        private class ProcessStats
        {
#if NET1
            public ProcessStats(TopicViewCollection tvc)
#else
            public ProcessStats(TopicViewCollection<TopicView> tvc)
#endif
            {
                _tvc = tvc;
            }
#if NET1
            protected TopicViewCollection _tvc;
#else
            protected TopicViewCollection<TopicView> _tvc;
#endif

            /// <summary>
            /// ִ��ͳ�Ʋ���
            /// </summary>
            public void Enqueue()
            {
                ManagedThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Process));
            }

            /// <summary>
            /// ����ǰ����
            /// </summary>
            /// <param name="state"></param>
            private void Process(object state)
            {
                TopicStats.TrackTopic(this._tvc);
            }
        }

#if NET1
#else
        public class TopicViewCollection<T> : Discuz.Common.Generic.List<T> where T : TopicView
        {
            public TopicViewCollection() : base() { }

            public TopicViewCollection(System.Collections.Generic.IEnumerable<T> collection) : base(collection) { }

            public TopicViewCollection(int capacity) : base(capacity) { }

            private int _viewCount = 0;

            public int ViewCount
            {
                get
                {
                    return _viewCount;
                }
                set
                {
                    _viewCount = value;
                }
            }
        }
#endif
        #endregion


        public static int GetStoredTopicViewCount(int tid)
        {
            foreach (TopicView curtv in queuedStatsList)
            {
                if (curtv.TopicID == tid)
                {
                    return curtv.ViewCount;
                }
            }
            return 0;
        }

    }
}
