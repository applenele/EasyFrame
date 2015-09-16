using QX.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class vDepartment
    {
        public vDepartment() { }

        public vDepartment(Department department)
        {
            this.ID = department.ID;
            this.DepartmentName = department.DepartmentName;
            this.Remark = department.Remark;
            this.IsDelete = department.IsDelete;
            this.AddTime = department.AddTime.ToString();
        }

        public int ID { set; get; }

        public string DepartmentName { set; get; }

        public string Remark { set; get; }

        public bool IsDelete { set; get; }

        public string AddTime { set; get; }

    }
}
