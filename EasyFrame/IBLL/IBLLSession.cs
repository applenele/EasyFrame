using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IBLLSession
    {
        IUserBLL GetUserBLL { set; get; }
    }
}
