using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HKShoppingManage.Model
{
    public class Album
    {
        /// <summary>
        /// 相册ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 相册名称
        /// </summary>
        [DisplayName("相册名称")]
        public string AlbumName { get; set; }
        /// <summary>
        /// 相册封面
        /// </summary>
        [DisplayName("相册封面")]
        public string CoverImg { get; set; }
        /// <summary>
        /// 相册相片数量
        /// </summary>
        [DisplayName("相片数量")]
        public int PhotoNum { get; set; }
        /// <summary>
        /// 相册描述
        /// </summary>
        [DisplayName("相册描述")]
        public string AlbumDesc { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [DisplayName("是否有效")]
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
