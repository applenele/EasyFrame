using QX.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DepartmentBLL:BaseBLL<Department> ,IBLL.IDepartmentBLL
    {
        public override void SetDAL()
        {
            idal = DBSession.GetDepartmentDAL;
        }
    }
}
