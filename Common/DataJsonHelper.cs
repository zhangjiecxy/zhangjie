using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// layerui需要返回的特定格式
    /// </summary>
   public class DataJsonHelper
    {
        /// <summary>
        /// 返回值：0（正常） 1（错误）
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public dynamic data { get; set; }
        /// <summary>
        /// 行数/数据总条数
        /// </summary>
        public int count { get; set; }
    }

    /// <summary>
    /// 异步请求返回结果
    /// </summary>
    public class HttpReturn
    {
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 返回值：0（正常） 1（错误）
        /// </summary>
        public int code { get; set; }
    }
}
