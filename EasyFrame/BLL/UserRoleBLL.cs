using IBLL;
using QX.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserRoleBLL:BaseBLL<UserRole>,IUserRoleBLL
    {
        public override void SetDAL()
        {
            idal = DBSession.GetUserRoleDAL;
        }
    }
}
