using EasyUIModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QX.Entity
{
    [Table("t_permission")]
    public class Permission
    {
        [Column("id")]
        public int ID { set; get; }

        /// <summary>
        /// 权限的上一级
        /// </summary>
        [Column("parent_id")]
        [ForeignKey("Father")]
        public int? ParentID { set; get; }

        public virtual Permission Father { set; get; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Column("pname")]
        public string PName { set; get; }

        /// <summary>
        ///  权限的域名称
        /// </summary>
        [Column("permission_area_name")]
        public string PAreaName { set; get; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        [Column("permission_controller_name")]
        public string PControllerName { set; get; }

        /// <summary>
        /// 权限action名称
        /// </summary>
        [Column("permisson_action_name")]
        public string PActionName { set; get; }

        /// <summary>
        /// 权限form方法
        /// </summary>
        [Column("permisson_form_method")]
        public int? PFormMethod { set; get; }

        [NotMapped]
        public EFormMethod FormMethod
        {
            set { PFormMethod = (int)value; }
            get { return (EFormMethod)PFormMethod; }
        }

        /// <summary>
        /// 权限的url
        /// </summary>
        [Column("purl")]
        public string PURL { set; get; }

        [Column("isshow")]
        public bool IsShow { set; get; }

        [Column("isdelete")]
        public bool IsDelete { set; get; }

        [Column("remark")]
        public string Remark { set; get; }

        [Column("addtime")]
        public DateTime AddTime { set; get; }

        [NotMapped]
        public string AddTimeAsString
        {
            get { return AddTime.ToString(); }
        }

        public virtual ICollection<Permission> children   //  子节点
        {
            get;
            set;
        }


        #region 2.0 将当前组织 对象 转成 树节点对象 +TreeNode ToNode()
        /// <summary>
        /// 将当前组织 对象 转成 树节点对象
        /// </summary>
        /// <returns></returns>
        public TreeNode ToNode()
        {
            TreeNode node = new TreeNode()
            {
                id = this.ID.ToString(),
                text = this.PName,
                state = "open",
                Checked = false,
                sondata="",
                children = new List<TreeNode>()
            };
            return node;
        }
        #endregion




        #region 3.0 递归组织集合 创建 树节点集合
        /// <summary>
        /// 递归组织集合 创建 树节点集合
        /// </summary>
        /// <param name="listPer">组织集合</param>
        /// <param name="listNodes">节点集合</param>
        /// <param name="pid">节点父id</param>
        public static void LoadTreeNode(List<Permission> listPer, List<TreeNode> listNodes, string pid)
        {
            foreach (var permission in listPer)
            {
                //如果组织父id=参数
                if (permission.ParentID.Equals(pid) || permission.ParentID.ToString().Equals(pid))
                {
                    //将 组织转成 树节点
                    TreeNode node = permission.ToNode();
                    //将节点 加入到 树节点集合
                    listNodes.Add(node);
                    //递归 为这个新创建的 树节点找 子节点
                    LoadTreeNode(listPer, node.children, node.id);
                }
            }
        }
        #endregion




        #region 4.0 将 组织集合 转成 树节点集合 +List<MODEL.EasyUIModel.TreeNode> ToTreeNodes(List<Ou_Permission> listPer)
        /// <summary>
        /// 将 组织集合 转成 树节点集合
        /// </summary>
        /// <param name="listPer"></param>
        /// <returns></returns>
        public static List<TreeNode> ToTreeNodes(List<Permission> listPer)
        {
            List<TreeNode> listNodes = new List<TreeNode>();
            //生成 树节点时，根据 pid=null的根节点 来生成
            LoadTreeNode(listPer, listNodes, null);

            //if (listCurrentPermissions.Count > 0)
            //listNodes[0].Checked = false;

            //SetFathersUnchecked(listNodes, argPId);

            return listNodes;
        }
        #endregion

        public static void SetFathersUnchecked(List<TreeNode> listNodes, string pid)
        {
            //叶子节点，则取消父节点的勾选状态，让其变成不确定状态 (否则会自动勾选父下的所有节点而显示不正确)
            foreach (var node in listNodes)
            {
                if (node.children.Count > 0)
                {
                    SetFathersUnchecked(node.children, node.id);

                    if (node.children.Exists(c => c.Checked)//如果节点A下有勾选的子节点，则节点A取消勾选(界面上会自动变成不确定状态)

           || node.children.TrueForAll(c => !c.Checked))//都未勾选，则父取消勾选
                        node.Checked = false;
                }
                else
                {
                    //叶子节点
                }
            }
        }
    }

    public enum EFormMethod { POST, GET }
}
