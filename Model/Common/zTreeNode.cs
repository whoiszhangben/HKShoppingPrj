
namespace HKShoppingManage.Model
{
    /// <summary>
    /// zTree节点类
    /// </summary>
    public class zTreeNode
    {
        public zTreeNode()
        {
            open = true;
            sort = 0;
        }
        /// <summary>
        /// Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        public string pId { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool open { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int sort { get; set; }
    }
}
