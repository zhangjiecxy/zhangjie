using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Model
{
    /// <summary>
    /// layui树-导航
    /// </summary>
  public  class TreeModel:Menu
    {
        public string id { get; set; }                
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool spread { get; set; }        
        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<TreeModel> children { get; set; }

        
    }

    /// <summary>
    /// boostrap树-树配置
    /// </summary>
    public class TreeBost
    {
        public string id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public string pid { get; set; }  
        /// <summary>
        /// 路径
        /// </summary>
        public string href { get; set; }
        /// <summary>
        /// 图片class
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? seq { get; set; }
        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<TreeBost> nodes { get; set; }
        
    }
}
