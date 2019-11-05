using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 实现Base64加密解密
    /// </summary>
    public sealed class Base64Helper
    {
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="codeName">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string EncodeBase64(Encoding encode, string source)
        {
            string str = "";
            byte[] bytes = encode.GetBytes(source);
            try
            {
                str = Convert.ToBase64String(bytes);
            }
            catch
            {
                str = source;
            }
            return str;
        }

        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string EncodeBase64(string source)
        {
            return EncodeBase64(Encoding.UTF8, source);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="codeName">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(Encoding encode, string result)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }

        /// <summary>
        /// Base64解密，采用utf8编码方式解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(string result)
        {
            return DecodeBase64(Encoding.UTF8, result);
        }

        /// <summary>
        /// 图片上传 Base64解码
        /// </summary>
        /// <param name="dataURL">Base64数据</param>
        /// <param name="path">保存路径</param>
        /// <param name="imgName">图片名字</param>
        /// <returns>返回一个相对路径</returns>

        public static string DecodeBase64ToImage(string base64, string path, string imgName)
        {

            string filename = "";//声明一个string类型的相对路径

            System.Drawing.Bitmap bitmap = null;//定义一个Bitmap对象，接收转换完成的图片

            try//会有异常抛出，try，catch一下
            {

                byte[] arr = Convert.FromBase64String(base64);//将纯净资源Base64转换成等效的8位无符号整形数组

                System.IO.MemoryStream ms = new System.IO.MemoryStream(arr);//转换成无法调整大小的MemoryStream对象
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);//将MemoryStream对象转换成Bitmap对象
                ms.Close();//关闭当前流，并释放所有与之关联的资源
                bitmap = bmp;
                filename = path + "/Knowledge_" + imgName + ".png";//所要保存的相对路径及名字
                string tmpRootDir = AppDomain.CurrentDomain.BaseDirectory; //获取程序根目录 
                string imagesurl2 = tmpRootDir + filename.Replace(@"/", @"\"); //转换成绝对路径 
                bitmap.Save(imagesurl2, System.Drawing.Imaging.ImageFormat.Png);//保存到服务器路径
            }
            catch (Exception)
            {
            }
            return filename;//返回相对路径
        }
    }
}
