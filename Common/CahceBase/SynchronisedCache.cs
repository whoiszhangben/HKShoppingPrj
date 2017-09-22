using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HKShoppingManage.Common
{
    public class SynchronisedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private Dictionary<TKey, TValue> innerDict;
        private ReaderWriterLockSlim readWriteLock;

        public SynchronisedDictionary(Func<TValue, TKey> getKey, List<TValue> list)
        {
            this.readWriteLock = new ReaderWriterLockSlim();
            this.innerDict = new Dictionary<TKey, TValue>();

            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    this.innerDict.Add(getKey(item), item);
                }
            }
        }

        /// <summary>
        /// 通过KeyValuePair新增缓存（建议使用SyncCache方法）
        /// </summary>
        /// <param name="item">KeyValuePair键值对</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            using (new AcquireWriteLock(this.readWriteLock))
            {
                this.innerDict[item.Key] = item.Value;
            }
        }

        /// <summary>
        /// 根据Key，Value新增缓存（建议使用SyncCache方法）
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        public void Add(TKey key, TValue value)
        {
            using (new AcquireWriteLock(this.readWriteLock))
            {
                this.innerDict[key] = value;
            }
        }

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public void Clear()
        {
            using (new AcquireWriteLock(this.readWriteLock))
            {
                this.innerDict.Clear();
            }
        }

        /// <summary>
        /// 是否包含指定元素
        /// </summary>
        /// <param name="item">KeyValuePair键值对</param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            using (new AcquireReadLock(this.readWriteLock))
            {
                return this.innerDict.Contains<KeyValuePair<TKey, TValue>>(item);
            }
        }

        /// <summary>
        /// 是否包含指定的键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            using (new AcquireReadLock(this.readWriteLock))
            {
                return this.innerDict.ContainsKey(key);
            }
        }

        /// <summary>
        /// copy到指定Array中
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            using (new AcquireReadLock(this.readWriteLock))
            {
                this.innerDict.ToArray<KeyValuePair<TKey, TValue>>().CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// 获取枚举数
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            using (new AcquireReadLock(this.readWriteLock))
            {
                return this.innerDict.GetEnumerator();
            }
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            using (new AcquireReadLock(this.readWriteLock))
            {
                return this.innerDict.GetEnumerator();
            }
        }

        /// <summary>
        /// 移除缓存（建议使用SyncCache方法）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            bool isRemoved;
            using (new AcquireWriteLock(this.readWriteLock))
            {
                isRemoved = this.innerDict.Remove(key);
            }
            return isRemoved;
        }

        /// <summary>
        /// 移除缓存（建议使用SyncCache方法）
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            using (new AcquireWriteLock(this.readWriteLock))
            {
                return this.innerDict.Remove(item.Key);
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            using (new AcquireReadLock(this.readWriteLock))
            {
                return this.innerDict.TryGetValue(key, out value);
            }
        }

        public int Count
        {
            get
            {
                using (new AcquireReadLock(this.readWriteLock))
                {
                    return this.innerDict.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                using (new AcquireReadLock(this.readWriteLock))
                {
                    return this.innerDict[key];
                }
            }
            set
            {
                using (new AcquireWriteLock(this.readWriteLock))
                {
                    this.innerDict[key] = value;
                }
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                using (new AcquireReadLock(this.readWriteLock))
                {
                    return this.innerDict.Keys;
                }
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                using (new AcquireReadLock(this.readWriteLock))
                {
                    return this.innerDict.Values;
                }
            }
        }

        /// <summary>
        /// 同步缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="del">是否删除</param>
        /// <remarks>
        /// 新增:SyncCache(Id,Value,false)
        /// 修改:SyncCache(Id,Value,false)
        /// 删除:SyncCache(Id,null,true)
        /// </remarks>
        public void SyncCache(TKey key, TValue value, bool del)
        {
            if (del)
            {
                Remove(key);
            }
            else
            {
                this[key] = value;
            }
        }

    }

    public class SynchronisedList<TValue> : IList<TValue>
    {
        private List<TValue> innerList;
        private ReaderWriterLockSlim readWriteLock;

        public SynchronisedList(IEnumerable<TValue> list)
        {
            innerList = new List<TValue>();
            readWriteLock = new ReaderWriterLockSlim();
            if (list != null && list.Count() > 0)
            {
                this.innerList.AddRange(list);
            }
        }

        public int IndexOf(TValue item)
        {
            using (new AcquireReadLock(this.readWriteLock))
            {
                return innerList.IndexOf(item);
            }
        }

        public void Insert(int index, TValue item)
        {
            using (new AcquireWriteLock(this.readWriteLock))
            {
                innerList.Insert(index, item);
            }
        }

        public void RemoveAt(int index)
        {
            using (new AcquireWriteLock(this.readWriteLock))
            {
                innerList.RemoveAt(index);
            }
        }

        public TValue this[int index]
        {
            get
            {
                using (new AcquireReadLock(this.readWriteLock))
                {
                    return innerList[index];
                }
            }
            set
            {
                using (new AcquireWriteLock(this.readWriteLock))
                {
                    innerList[index] = value;
                }
            }
        }

        public void Add(TValue item)
        {
            using (new AcquireWriteLock(this.readWriteLock))
            {
                innerList.Add(item);
            }
        }

        public void Clear()
        {
            using (new AcquireWriteLock(this.readWriteLock))
            {
                innerList.Clear();
            }
        }

        public bool Contains(TValue item)
        {
            using (new AcquireReadLock(this.readWriteLock))
            {
                return innerList.Contains(item);
            }
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            using (new AcquireReadLock(this.readWriteLock))
            {
                innerList.CopyTo(array, arrayIndex);
            }
        }

        public int Count
        {
            get
            {
                using (new AcquireReadLock(this.readWriteLock))
                {
                    return innerList.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove(TValue item)
        {
            using (new AcquireWriteLock(this.readWriteLock))
            {
                return innerList.Remove(item);
            }
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            using (new AcquireReadLock(this.readWriteLock))
            {
                return innerList.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            using (new AcquireReadLock(this.readWriteLock))
            {
                return innerList.GetEnumerator();
            }
        }
    }

    class AcquireReadLock : IDisposable
    {
        private ReaderWriterLockSlim rwLock;
        private bool disposedValue;

        public AcquireReadLock(ReaderWriterLockSlim rwLock)
        {
            this.rwLock = new ReaderWriterLockSlim();
            this.disposedValue = false;
            this.rwLock = rwLock;
            this.rwLock.EnterReadLock();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue && disposing)
            {
                this.rwLock.ExitReadLock();
            }
            this.disposedValue = true;
        }
    }

    class AcquireWriteLock : IDisposable
    {
        private ReaderWriterLockSlim rwLock;
        private bool disposedValue;

        public AcquireWriteLock(ReaderWriterLockSlim rwLock)
        {
            this.rwLock = new ReaderWriterLockSlim();
            this.disposedValue = false;
            this.rwLock = rwLock;
            this.rwLock.EnterWriteLock();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue && disposing)
            {
                this.rwLock.ExitWriteLock();
            }
            this.disposedValue = true;
        }
    }
}