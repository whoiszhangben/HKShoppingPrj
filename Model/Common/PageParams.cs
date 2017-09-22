using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HKShoppingManage.Model
{
    public class PageParams<T> where T : class
    {
        public PageParams()
        {
            SortOrder = true;
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public Expression<Func<T, bool>> Where { get; set; }
        /// <summary>
        /// 排序方式，false降序，true 升序
        /// </summary>
        public bool SortOrder { get; set; }
        /// <summary>
        /// 按哪个字段排序
        /// </summary>
        public string SortField { get; set; }
    }
}
