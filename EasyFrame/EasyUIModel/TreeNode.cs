using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyUIModel
{
    /*  树的节点类
       id：节点id，对载入远程数据很重要。
       text：显示在节点的文本。
       state：节点状态，'open' or 'closed'，默认为'open'。当设置为'closed'时，拥有子节点的节点将会从远程站点载入它们。
       checked：表明节点是否被选择。
       children：子节点，必须用数组定义。
    */
    public class TreeNode
    {
        public string id { get; set; }  //节点的id值
        public string text { get; set; }  //节点显示的名称
        public string state { get; set; }//节点的状态
        // 请在整个树转换成jsonString时,将字符串Checked换成checked，否则easyui不认
        public bool Checked { get; set; } //注意：转成JsonTreeString时，将"Checked"替换成"checked",否则选中效果出不来的
        public List<TreeNode> children { get; set; }  //集合属性，可以保存子节点

        public string sondata { set; get; }
    }


}
