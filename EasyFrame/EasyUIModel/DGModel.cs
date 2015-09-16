using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyUIModel
{
    /// <summary>
    /// DataGrid 返回时的Model
    /// </summary>
    public class DGModel<T> where  T:class
    {
        public int total { set; get; }
        public List<T> rows { set; get; }
    }
}
