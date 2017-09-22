using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace HKShoppingManage.Common
{
    public class ConcurrentCache<Key, Value> where Value : class
    {
        private ConcurrentDictionary<Key, Value> _cache;
        private Func<Key, Value> _getData;
        private Func<Value, Key> _getKey;

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        /// <param name="getKey">从Value中获取Key</param>
        /// <param name="getData">传入根据Key从数据库获取Value的方法</param>
        public ConcurrentCache(Func<Value, Key> getKey, Func<Key, Value> getData)
        {
            _getData = getData;
            _getKey = getKey;

            _cache = new ConcurrentDictionary<Key, Value>();
        }

        /// <summary>
        /// 尝试根据Key获取Value，若不存在，则返回null
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Value TryGetOne(Key key)
        {
            Value rtnValue = null;
            bool isSuccess = _cache.TryGetValue(key, out rtnValue);
            return rtnValue;
        }

        /// <summary>
        /// 根据Key获取Value，若不存在，则访问数据库获取，若还是不存在，则返回null
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Value GetOne(Key key)
        {
            Value rtnValue = TryGetOne(key);
            if (rtnValue == null)
            {
                rtnValue = _getData(key);
                if (rtnValue != null)
                {
                    AddOrUpdate(rtnValue);
                }
            }
            return rtnValue;
        }

        /// <summary>
        /// 根据列表ID获取对应值
        /// </summary>
        /// <param name="func">委托获取列表方法</param>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public List<Value> GetList(Func<List<Key>, List<Value>> func, List<Key> ids)
        {
            List<Value> list = new List<Value>();
            List<Key> keyList = new List<Key>();

            foreach (var item in ids)
            {
                if (TryGetOne(item) == null)
                {
                    keyList.Add(item);
                }
            }

            try
            {
                list = func(keyList);
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        Key key = _getKey(item);
                        _cache[key] = item;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("获取列表缓存出错", ex);
            }
            return list;
        }

        /// <summary>
        /// 增加或更新缓存（建议使用SyncCache方法）
        /// </summary>
        /// <param name="v"></param>
        public void AddOrUpdate(Value value)
        {
            Key key = _getKey(value);
            if (_cache.ContainsKey(key))
            {
                Value temp = TryGetOne(key);
                _cache.TryUpdate(key, value, temp);
            }
            else
            {
                _cache.TryAdd(key, value);
            }
        }

        /// <summary>
        /// 批量插入缓存
        /// </summary>
        /// <param name="listV"></param>
        public void AddList(List<Value> listV)
        {
            if (listV != null && listV.Count > 0)
            {
                try
                {
                    foreach (var item in listV)
                    {
                        Key key = _getKey(item);
                        if (!_cache.ContainsKey(key))
                        {
                            _cache.TryAdd(key, item);
                        }
                        else
                        {
                            _cache[key] = item;
                        }
                    }

                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[批量插入缓存错误]", ex);
                }
            }
        }

        /// <summary>
        /// 根据Key移除缓存（建议使用SyncCache方法）
        /// </summary>
        /// <param name="key"></param>
        public void Remove(Key key)
        {
            Value tmpV = null;
            try
            {
                if (_cache.ContainsKey(key))
                {
                    _cache.TryRemove(key, out tmpV);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("移除缓存出错", ex);
            }
        }

        /// <summary>
        /// 根据传入方法初始化缓存列表(会清除原有缓存)
        /// </summary>
        /// <param name="func">获取列表方法</param>
        public void InitList(Func<List<Value>> func)
        {
            this._cache.Clear();
            var list = func();
            AddList(list);
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
        public void SyncCache(Key key, Value value, bool del)
        {
            if (del)
            {
                Remove(key);
            }
            else
            {
                AddOrUpdate(value);
            }
        }
    }
}
