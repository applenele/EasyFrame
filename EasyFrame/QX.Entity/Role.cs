using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QX.Entity
{
    [Table("t_role")]
    public class Role
    {
        [Column("id")]
        public int ID { set; get; }

        [Column("department_id")]
        [ForeignKey("Department")]
        public int DepartmentID { set; get; }

        public virtual Department Department { set; get; }

        [Column("role_name")]
        public string RoleName { set; get; }

        [Column("remark")]
        public string Remark { set; get; }

        [Column("isshow")]
        public bool IsShow { set; get; }

        [Column("isdelete")]
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
