using QX.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class vPermission
    {
        public vPermission() { }
        public vPermission(Permission p)
        {
            this.ID = p.ID;
            this.PName = p.PName;
            this.FID = p.ParentID;
            this.PAreaName = p.PAreaName;
            this.PActionName = p.PActionName;
            this.PControllerName = p.PActionName;
            this.PFormMethod = p.PFormMethod;
            this.PURL = p.PURL;
            this.Remark = p.Remark;
            this.IsDelete = p.IsDelete;
            this.IsShow = p.IsShow;
            this.AddTime = p.AddTime.ToString();
        }

        public int ID { set; get; }

        public string PName { set; get; }

        public int? FID { set; get; }

        public string PAreaName { set; get; }

        public string PControllerName { set; get; }

        public string PActionName { set; get; }

        public int? PFormMethod { set; get; }

        public string PURL { set; get; }

        public bool IsShow { set; get; }

        public bool IsDelete { set; get; }

        public string Remark { set; get; }

        public string AddTime { set; get; }

    }
}
