using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DBA
{
    /// <summary>
    /// SqlHepler类
    /// </summary>
    public class SqlHelper
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        /// <summary>
        /// 执行查询语句，返回动态类型
        /// </summary>
        /// <param name="sql">Sql语句</param>
        public static List<dynamic> Query(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    var list = conn.Query<dynamic>(sql).AsList();
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// 执行带参数的查询语句，返回动态类型
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="pars">参数</param>
        public static List<dynamic> Query(string sql, object pars)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    var list = conn.Query<dynamic>(sql, pars).AsList();
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// 执行存储过程，返回动态类型
        /// </summary>
        /// <param name="sql">Sql语句</param>
        public static List<dynamic> QueryProc(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    var list = conn.Query<dynamic>(sql, commandType: CommandType.StoredProcedure).AsList();
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// 执行带参数的存储过程，返回动态类型
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="pars">参数</param>
        public static List<dynamic> QueryProc(string sql, object pars)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    var list = conn.Query<dynamic>(sql, pars, commandType: CommandType.StoredProcedure).AsList();
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// 执行sql语句，返回自增id
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="pars">参数</param>
        public static int ExecuteScalar(string sql, object pars)
        {
            int maxid = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    maxid = Convert.ToInt32(conn.ExecuteScalar(sql, pars));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    conn.Close();
                }
            }

            return maxid;
        }

        /// <summary>
        /// 执行sql语句，带事务操作
        /// </summary>
        /// <param name="sqls">sqls</param>
        /// <param name="pars">参数</param>
        public static int ExecuteTran(List<string> sqls, object pars)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    foreach (string sql in sqls)
                    {
                        count += conn.Execute(sql, pars, transaction);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    transaction.Dispose();
                    conn.Close();
                }
            }

            return count;
        }

        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="sql">SQL语句</param>
        ///// <param name="pars">参数</param>
        ///// <returns>bool</returns>
        //public static bool Delete(string sql,object pars)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            int count = conn.Execute(sql, pars);
        //            return count > 0 ? true : false;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message, ex);
        //        }
        //    }
        //}

        ///// <summary>
        ///// 分页查询
        ///// </summary>
        ///// <param name="querySql">数据查询的SQL</param>
        ///// <param name="countSql">总页数的SQL</param>
        ///// <param name="pars">参数</param>
        ///// <param name="TotalCount">总页数</param>
        //public static List<dynamic> QueryPagination(string querySql, string countSql, object pars, out int TotalCount)
        //{
        //    TotalCount = 0;
        //    using (MySqlConnection conn = new MySqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            var list = conn.Query<dynamic>(querySql, pars).AsList();
        //            TotalCount = conn.QuerySingleOrDefault<Int32>(countSql, pars);
        //            return list;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message, ex);
        //        }
        //    }
        //}

        //public static bool Add(string sql, object pars)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            int count = conn.Execute(sql, pars);
        //            return count > 0 ? true : false;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message, ex);
        //        }
        //    }
        //}
    }
}
