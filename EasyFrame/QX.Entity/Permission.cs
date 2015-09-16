using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QX.Entity
{
    [Table("t_permission")]
    public class Permission
    {
        [Column("id")]
        public int ID { set; get; }

        /// <summary>
        /// 权限的上一级
        /// </summary>
        [Column("parent_id")]
        [ForeignKey("Father")]
        public int? ParentID { set; get; }

        public virtual Permission Father { set; get; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Column("pname")]
        public string PName { set; get; }

        /// <summary>
        ///  权限的域名称
        /// </summary>
        [Column("permission_area_name")]
        public string PAreaName { set; get; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        [Column("permission_controller_name")]
        public string PControllerName { set; get; }

        /// <summary>
        /// 权限action名称
        /// </summary>
        [Column("permisson_action_name")]
        public string PActionName { set; get; }

        /// <summary>
        /// 权限form方法
        /// </summary>
        [Column("permisson_form_method")]
        public int? PFormMethod { set; get; }

        [NotMapped]
        public EFormMethod FormMethod
        {
            set { PFormMethod = (int)value; }
            get { return (EFormMethod)PFormMethod; }
        }

        /// <summary>
        /// 权限的url
        /// </summary>
        [Column("purl")]
        public string PURL { set; get; }

        [Column("isshow")]
        public bool IsShow { set; get; }

        [Column("isdelete")]
        public bool IsDelete { set; get; }

        [Column("remark")]
        public string Remark { set; get; }

        [Column("addtime")]
        public DateTime AddTime { set; get; }

        [NotMapped]
        public string AddTimeAsString
        {
            get { return AddTime.ToString(); }
        }
    }

    public enum EFormMethod { POST, GET }
}
