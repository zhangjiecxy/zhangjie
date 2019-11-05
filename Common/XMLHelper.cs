using LitJson;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Common
{
    public class XMLHelper
    {
        /// <summary>
        /// 用字典来存储XML数据
        /// </summary>
        private SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();

        /// <summary>
        /// 设置某个字段的值
        /// </summary>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        public void SetValue(string key, object value)
        {
            m_values[key] = value;
        }
        /// <summary>
        /// 获取某个字段的值
        /// </summary>
        /// <param name="key">字段名</param>
        /// <returns>字段值</returns>
        public object GetValue(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            return o;
        }
        /// <summary>
        /// 判断某个字段是否已设置
        /// </summary>
        /// <param name="key">字段名</param>
        /// <returns>若字段key已被设置，则返回true，否则返回false</returns>
        public bool IsSet(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            if (null != o)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 转换成XML
        /// </summary>
        /// <returns>XML字符串</returns>
        public string ToXml()
        {
            if (0 == m_values.Count)
            {
                return "";
            }
            string xml = "<xml>";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                if (pair.Value != null)
                {
                    if (pair.Value.GetType() == typeof(int))
                    {
                        xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                    }
                    else if (pair.Value.GetType() == typeof(string))
                    {
                        xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                    }
                    else//除了string和int类型不能含有其他数据类型
                    {
                        throw new Exception("XML字典的字段数据类型错误!");
                    }
                }
            }
            xml += "</xml>";
            return xml;
        }
        /// <summary>
        /// Dictionary格式化成Json
        /// </summary>
        /// <returns>json串数据</returns>
        public string ToJson()
        {
            string jsonStr = JsonMapper.ToJson(m_values);
            return jsonStr;
        }
        /// <summary>
        /// 将xml转为Dictionary对象并返回对象内部的数据
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>经转换得到的Dictionary</returns>
        public SortedDictionary<string, object> FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new Exception("将空的xml串转换为Dictionary对象不合法!");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                m_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }
            return m_values;
        }
    }
}
