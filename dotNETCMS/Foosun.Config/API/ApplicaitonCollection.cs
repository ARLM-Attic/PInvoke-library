using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Foosun.Config.API
{

    /// <summary>
    /// 整合的应用集合
    /// </summary>
    [Serializable]
    public class ApplicaitonCollection : CollectionBase
    {

        public ApplicaitonCollection()
        {
        }

        public ApplicaitonCollection(ApplicaitonCollection value)
        {
            this.AddRange(value);
        }

        public ApplicaitonCollection(ApplicationInfo[] value)
        {
            this.AddRange(value);
        }

        /// <summary>
        /// 添加一个实用
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(ApplicationInfo value)
        {
            return base.List.Add(value);
        }

        /// <summary>
        /// 添加应用
        /// </summary>
        /// <param name="value"></param>
        public void AddRange(ApplicationInfo[] value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                this.Add(value[i]);
            }
        }

        public void AddRange(ApplicaitonCollection value)
        {
            for (int i = 0; i < value.Count; i++)
            {
                this.Add((ApplicationInfo)value.List[i]);
            }
        }
        /// <summary>
        /// 检查集合中是否包含某应用
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(ApplicationInfo value)
        {
            return base.List.Contains(value);
        }

        public void CopyTo(ApplicationInfo[] array, int index)
        {
            base.List.CopyTo(array, index);
        }

        public new ApplicationCollectionEnumerator GetEnumerator()
        {
            return new ApplicationCollectionEnumerator(this);
        }

        public int IndexOf(ApplicationInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ApplicationInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ApplicationInfo value)
        {
            base.List.Remove(value);
        }

        // Properties
        public ApplicationInfo this[int index]
        {
            get
            {
                return (ApplicationInfo)base.List[index];
            }
        }

        
        public class ApplicationCollectionEnumerator : IEnumerator
        {
           
            private IEnumerator _enumerator;
            private IEnumerable _temp;

           
            public ApplicationCollectionEnumerator(ApplicaitonCollection mappings)
            {
                this._temp = mappings;
                this._enumerator = this._temp.GetEnumerator();
            }

            public bool MoveNext()
            {
                return this._enumerator.MoveNext();
            }

            public void Reset()
            {
                this._enumerator.Reset();
            }

            bool IEnumerator.MoveNext()
            {
                return this._enumerator.MoveNext();
            }

            void IEnumerator.Reset()
            {
                this._enumerator.Reset();
            }

           
            public ApplicationInfo Current
            {
                get
                {
                    return (ApplicationInfo)this._enumerator.Current;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this._enumerator.Current;
                }
            }
        }
    }


}
