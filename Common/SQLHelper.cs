using System;
using System.Data;
using System.Data.SqlClient;

namespace Common
{
    public class SQLHelper
    {
        //链接数据库字符串
        //private static string strConnString = "server = .;uid = sa;pwd = fd_123456;database = YKTManager";
        private static string strConnString = GetConfigValue("strConnString");

        /// <summary>
        /// 获取配置文件中键对应的值
        /// </summary>
        /// <param name="key">配置文件中的健名称</param>
        /// <returns></returns>
        public static string GetConfigValue(string key)
        {
            return System.Configuration.ConfigurationSettings.AppSettings[key];
        }
        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="para">参数列表</param>
        /// <returns>DataTable</returns>
        public static DataTable getTable(string sql, SqlParameter[] para, ref string err)
        {
            #region 
            SqlConnection conn = new SqlConnection(strConnString);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (para != null)
                {
                    cmd.Parameters.AddRange(para);
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
            #endregion
        }
        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="sql">DML语句</param>
        /// <param name="para">参数列表</param>
        /// <param name="err">错误信息</param>
        /// <returns>受影响的行数</returns>
        public static int Execute(string sql, SqlParameter[] para, ref string err)
        {
            #region 
            SqlConnection conn = new SqlConnection(strConnString);
            int ir = -1;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (para != null)
                {
                    cmd.Parameters.AddRange(para);
                }
                ir = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return ir;
            #endregion
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public static int ExecuteProcedure(string sql, SqlParameter[] para, CommandType type, ref string err)
        {
            #region 
            SqlConnection conn = new SqlConnection(strConnString);
            int IR = -1;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = type;
                if (para != null)
                {
                    cmd.Parameters.AddRange(para);
                }
                IR = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return IR;
            #endregion
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string sql, SqlParameter[] para, ref string err)
        {
            #region MyRegion
            SqlConnection conn = new SqlConnection(strConnString);
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (para != null)
                {
                    cmd.Parameters.AddRange(para);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return ds;
            #endregion
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            #region MyRegion
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {


                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        #endregion
    }
}
