using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QX.Entity
{
    [Table("t_user")]
    public class User
    {
        [Column("id")]
        public int ID { set; get; }

        [Column("department_id")]
        [ForeignKey("Department")]
        public int? DepartmentID { set; get; }

        public virtual Department Department { set; get; }

        [Column("username")]
        public string Username { set; get; }

        [Column("password")]
        public string Password { set; get; }

        [Column("addtime")]
        public DateTime AddTime { set; get; }

        [NotMapped]
        public string AddTimeAsString
        {
            get { return AddTime.ToString(); }
        }

        [Column("isdelete")]
        public bool IsDelete { set; get; }




        [Column("remark")]
        public string Remark { set; get; }

        /// <summary>
        ///  性别
        /// </summary>
        [Column("gender")]
        public int Gender { set; get; }

        [NotMapped]
        public EGender EGender
        {
            set { Gender = (int)value; }
            get { return (EGender)Gender; }
        }


    }

    public enum EGender { 男, 女 }
}
