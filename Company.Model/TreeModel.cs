using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Model
{
  public  class TreeModel
    {
        public string id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool spread { get; set; }
        /// <summary>
        /// 跳转路径
        /// </summary>
        public string href { get; set; }
        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<TreeModel> children { get; set; }
    }
}
