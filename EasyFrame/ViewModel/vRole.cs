using BLL;
using QX.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class vRole
    {
        public vRole() { }

        public vRole(Role role)
        {
            IBLL.IDepartmentBLL bll = DI.SpringHelper.GetObject<IBLL.IBLLSessionFactory>("BLLSessionFactory").GetBLLSession().GetDepartmentBLL;
           this.ID=role.ID;
           this.RoleName = role.RoleName;
           this.Remark = role.Remark;
           this.DepartmentID = role.DepartmentID;
           this.Department = bll.GetListBy(d => d.ID == this.DepartmentID).SingleOrDefault();
           this.IsDelete = role.IsDelete;
           this.IsShow = role.IsShow;
           this.AddTime = role.AddTime.ToString();
        }


        public int ID { set; get; }

        public int DepartmentID { set; get; }

        public Department Department { set; get; }

        public string RoleName { set; get; }

        public string Remark { set; get; }

        public bool IsShow { set; get; }

        public bool IsDelete { set; get; }

        public string  AddTime { set; get; }
    }
}
