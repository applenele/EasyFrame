using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QX.Entity
{
    /// <summary>
    /// 用户权限表
    /// </summary>
    [Table("t_user_role")]
    public class UserRole
    {
        [Column("id")]
        public int ID { set; get; }

        /// <summary>
        ///  用户ID
        /// </summary>
        [Column("uid")]
        [ForeignKey("User")]
        public int UserID { set; get; }

        public virtual User User { set; get; }

        /// <summary>
        ///  角色ID
        /// </summary>
        [Column("rid")]
        [ForeignKey("Role")]
        public int RoleID { set; get; }

        public virtual Role Role { set; get; }

        [Column("isdelete")]
        public bool  IsDelete { set; get; }


        [Column("addtime")]
        public DateTime AddTime { set; get; }

        [NotMapped]
        public string AddTimeAsString
        {
            get { return AddTime.ToString(); }
        }
    }
}
