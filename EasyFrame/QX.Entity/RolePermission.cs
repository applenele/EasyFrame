using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QX.Entity
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    [Table("t_role_permission")]
    public class RolePermission
    {
        [Column("id")]
        public int ID { set; get; }

        [Column("rid")]
        [ForeignKey("Role")]
        public int RoleID { set; get; }

        public virtual Role Role { set; get; }

        [Column("pid")]
        [ForeignKey("Permission")]
        public int PermissionID { set; get; }

        public virtual Permission Permission { set; get; }

        [Column("IsDelete")]
        public bool IsDelete { set; get; }

        [Column("addtime")]
        public DateTime AddTime { set; get; }

        [NotMapped]
        public string AddTimeAsString
        {
            get { return AddTime.ToString(); }
        }
    }
}
