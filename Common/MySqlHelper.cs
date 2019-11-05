using MySql.Data.MySqlClient;
using System;

namespace Common
{
    public class MySqlHelper
    {
        private static string connectionString = "server=localhost;user id=root;password=fd_123456;database=YKTManager"; //根据自己的设置

        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="sql">DML语句</param>
        /// <param name="para">参数列表</param>
        /// <param name="err">错误信息</param>
        /// <returns>受影响的行数</returns>
        public static int Execute(string sql, MySqlParameter[] para, ref string err)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    try
                    {
                        comm.Parameters.AddRange(para);
                        int ir = comm.ExecuteNonQuery();
                        return ir;
                    }
                    catch (Exception e)
                    {
                        err = e.Message;
                        return 0;
                    }
                }
            }
        }
        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="sql">DML语句</param>
        /// <param name="para">参数列表</param>
        /// <param name="err">错误信息</param>
        /// <returns>受影响的行数</returns>
        public static int Execute(string sql, ref string err)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    try
                    {
                        conn.Open();
                        int ir = comm.ExecuteNonQuery();
                        return ir;
                    }
                    catch (Exception e)
                    {
                        err = e.Message;
                        return 0;
                    }
                }
            }
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        //public static int ExecuteSql(string SQLString, params MySqlParameter[] cmdParms)
        //{
        //using (MySqlConnection connection = new MySqlConnection(connectionString))
        //{
        //    using (MySqlCommand cmd = new MySqlCommand())
        //    {
        //        try
        //        {
        //            PrepareCommand(cmd, connection, null, SQLString, cmdParms);
        //            int rows = cmd.ExecuteNonQuery();
        //            cmd.Parameters.Clear();
        //            return rows;
        //        }
        //        catch (MySql.Data.MySqlClient.MySqlException e)
        //        {
        //            throw e;
        //        }
        //    }
        //}
        //}
    }
}
