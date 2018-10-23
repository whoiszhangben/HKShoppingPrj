using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HKShoppingManage.Model
{
    public class Profile
    {
        public Profile()
        {
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }
        /// <summary>
        /// 自动增长Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 档案编号
        /// </summary>
        [DisplayName("档案编号")]
        public string ProfileNo { get; set; }
        /// <summary>
        /// 员工名称
        /// </summary>
        [DisplayName("员工名称")]
        public string EmpName { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        [DisplayName("身份证号码")]
        public string EmpIDNo { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [DisplayName("电话号码")]
        public string EmpTelNo { get; set; }
        /// <summary>
        /// 是否离职
        /// </summary>
        [DisplayName("是否离职")]
        public bool IsDimissioned { get; set; }
        /// <summary>
        /// 员工资料值
        /// </summary>
        public int RelationVal { get; set; }
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
        /// <summary>
        /// 当前标识
        /// </summary>
        public int Flag { get; set; }
    }
}
