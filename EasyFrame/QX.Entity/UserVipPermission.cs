using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QX.Entity
{
    /// <summary>
    ///  用户特权实体
    /// </summary>
    [Table("t_user_vip_permission")]
    public class UserVipPermission
    {
        [Column("id")]
        public int ID { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Column("uid")]
        [ForeignKey("User")]
        public int UserID { set; get; }

        public virtual User User { set; get; }

        /// <summary>
        ///  权限ID
        /// </summary>
        [Column("pid")]
        [ForeignKey("Permission")]
        public int PermissionID { set; get; }

        public virtual Permission Permission { set; get; }

        [Column("isDelete")]
        public bool IsDelete { set; get; }

        [Column("remark")]
        public string Remark { set; get; }

        [Column("addtime")]
        public DateTime AddTime { set; get; }
    }
}
