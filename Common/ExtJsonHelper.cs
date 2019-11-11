using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ExtJsonHelper
    {
        /// <summary>
        /// 将JSON格式字符转换成对象
        /// </summary>
        /// <typeparam name="T">需要转换的对象类型</typeparam>
        /// <param name="target">JSON目标</param>
        /// <returns>转换结果</returns>
        public static T JsonDeserialize<T>(this string target)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(target)))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                    return (T)serializer.ReadObject(ms);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        #region 将对象转换为JSON格式的字符串

        /// <summary>
        /// 将对象转换为JSON格式的字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="target">对象目标</param>
        /// <returns>转换结果</returns>
        public static string JsonSerialize<T>(this T target)
        {
            try
            {
                T result = (T)target;

                DataContractJsonSerializer json = new DataContractJsonSerializer(result.GetType());

                using (MemoryStream stream = new MemoryStream())
                {
                    json.WriteObject(stream, result);
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
