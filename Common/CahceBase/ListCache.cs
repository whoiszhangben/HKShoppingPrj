using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System;

namespace HKShoppingManage.Common
{
    public class ListCache<T>
    {
        private List<T> cacheList;
        private ReaderWriterLockSlim readWriteLock;

        public ListCache(List<T> list)
        {
            this.readWriteLock = new ReaderWriterLockSlim();
            this.cacheList = new List<T>();

            if (list != null && list.Count > 0)
            {
                this.cacheList.AddRange(list);
            }
        }

        public void Add(T model)
        {
            try
            {
                this.readWriteLock.EnterWriteLock();
                if (!this.cacheList.Contains(model))
                {
                    this.cacheList.Add(model);
                }
            }
            finally
            {
                this.readWriteLock.ExitWriteLock();
            }
        }

        public IEnumerable<T> Where(Func<T, bool> predicate)
        {
            return this.cacheList.Where(predicate);
        }

        public List<T> GetList()
        {
            return this.cacheList;
        }


    }
}