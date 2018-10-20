using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HKShoppingManage.Model
{
    public class Category
    {
        /// <summary>
        /// 类别编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 产品类别名称
        /// </summary>
        [DisplayName("类别名称")]
        public string Name { get; set; }
        /// <summary>
        /// 产品类别描述
        /// </summary>
        [DisplayName("类别描述")]
        public string Description { get; set; }
        /// <summary>
        /// 上级编号
        /// </summary>
        [DisplayName("父级类别")]
        public int? ParentCategoryId { get; set; }
        /// <summary>
        /// 类别层级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string Updator { get; set; }
    }
}
