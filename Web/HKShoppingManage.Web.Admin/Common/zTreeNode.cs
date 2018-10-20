using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HKShoppingManage.Web.Admin
{
    public class zTreeNode
    {
        public zTreeNode()
        {
            children = new List<zTreeNode>();
        }
        public int id { get; set; }
        public int? pId { get; set; }
        public string name { get; set; }
        public bool open { get; set; }
        public List<zTreeNode> children { get; set; }
    }
}