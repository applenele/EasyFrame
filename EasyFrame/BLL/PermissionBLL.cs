using IBLL;
using QX.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PermissionBLL:BaseBLL<Permission> , IPermissionBLL
    {
        public override void SetDAL()
        {
            idal = DBSession.GetPermissionDAL;
        }
    }
}
