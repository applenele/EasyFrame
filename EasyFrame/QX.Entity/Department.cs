using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QX.Entity
{
    [Table("t_department")]
    public class Department
    {
        [Column("id")]
        public int ID { set; get; }

        [Column("departmentname")]
        public string DepartmentName { set; get; }

        [Column("remark")]
        public string Remark { set; get; }

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
