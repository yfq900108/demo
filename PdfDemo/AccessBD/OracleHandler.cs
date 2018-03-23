using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
namespace AccessBD
{
    /// <summary>
    /// Oracle数据处理基础类
    /// </summary>
    public class OracleHandler
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        private OracleConnection dbConnection;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strConnection">数据库连接字符串</param>
        public OracleHandler(string strConnection)
        {
            dbConnection = new OracleConnection(strConnection);
        }

        /// <summary>
        /// 通过sql语句执行，新增，修改，删除
        /// </summary>
        /// <param name="sqlCommandText">sql语句</param>
        /// <param name="oraParams">参数列表</param>
        public void ExecuteUpdate(string sqlCommandText, List<OracleParameter> oraParams)
        {
            try
            {
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                }
                OracleCommand oracleCommand = dbConnection.CreateCommand();
                oracleCommand.CommandType = CommandType.Text;
                oracleCommand.CommandText = sqlCommandText;
                if (oraParams != null)
                {
                    foreach (OracleParameter current in oraParams)
                    {
                        oracleCommand.Parameters.Add(current);
                    }
                }
                oracleCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        /// <summary>
        /// 通过存储过程执行，新增，修改，删除
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="oraParams">参数列表</param>
        public void ExecuteUpdateSp(string spName, List<OracleParameter> oraParams)
        {
            try
            {

                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                }
                OracleCommand oracleCommand = dbConnection.CreateCommand();
                oracleCommand.CommandType = CommandType.StoredProcedure;
                oracleCommand.CommandText = spName;
                foreach (OracleParameter current in oraParams)
                {
                    oracleCommand.Parameters.Add(current);
                }
                oracleCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        /// <summary>
        /// 执行sql语句返回DataSet
        /// </summary>
        /// <param name="sqlCommandText">sql语句</param>
        /// <returns></returns>
        public DataSet ExecuteQuery(string sqlCommandText)
        {
            DataSet ds = new DataSet();
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(sqlCommandText, dbConnection);
            oracleDataAdapter.Fill(ds);
            dbConnection.Close();
            return ds;
        }

        /// <summary>
        /// 执行sql存储过程返回DataSet
        /// </summary>
        /// <param name="spName">sql存储过程</param>
        /// <returns></returns>
        public DataSet ExecuteQuerySP(string spName)
        {
            DataSet ds = new DataSet();
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(spName, dbConnection);
            oracleDataAdapter.Fill(ds);
            dbConnection.Close();
            return ds;
        }
        /// <summary>
        /// 通过存储过程执行，新增，修改，删除
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="oraParams">参数列表</param>
        public void ExecuteQuerySP(string spName, List<OracleParameter> oraParams)
        {
            try
            {

                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                }
                OracleCommand oracleCommand = dbConnection.CreateCommand();
                oracleCommand.CommandType = CommandType.StoredProcedure;
                oracleCommand.CommandText = spName;
                foreach (OracleParameter current in oraParams)
                {
                    oracleCommand.Parameters.Add(current);
                }
                OracleDataReader dd = oracleCommand.ExecuteReader();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConnection.Close();
            }
        }




        /// <summary>
        /// 执行带参数的sql语句返回DataSet
        /// </summary>
        /// <param name="sqlCommandText">sql语句</param>
        /// <param name="oraParams">参数列表</param>
        /// <returns></returns>
        public DataSet ExecuteQuery(string sqlCommandText, List<OracleParameter> oraParams)
        {
            DataSet ds = new DataSet();
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
            OracleCommand oracleCommand = dbConnection.CreateCommand();
            oracleCommand.CommandType = CommandType.Text;
            oracleCommand.CommandText = sqlCommandText;
            foreach (OracleParameter current in oraParams)
            {
                oracleCommand.Parameters.Add(current);
            }
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
            oracleDataAdapter.Fill(ds);
            dbConnection.Close();
            return ds;
        }

        /// <summary>
        /// 执行sql,返回第一行第一列对象
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public object ExecuteScalar(string strSql)
        {
            object objRet = new object();
            try
            {
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                }
                OracleCommand oracleCmd = new OracleCommand(strSql, dbConnection);
                objRet = oracleCmd.ExecuteScalar();

                if (objRet.ToString() == "" || objRet.ToString() == null)
                {
                    objRet = "NULL";
                }
                return objRet;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }

    /// <summary>
    /// 参数列表类
    /// </summary>
    public class OracleDbParameterList
    {
        private List<OracleParameter> oraParams;
        public OracleDbParameterList()
        {
            this.oraParams = new List<OracleParameter>();
        }
        /// <summary>
        /// 创建一参数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="oraType">参数类型</param>
        /// <param name="paramValue">参数值</param>
        public void Add(string paramName, OracleType oraType, object paramValue)
        {
            OracleParameter oracleParameter = new OracleParameter();
            oracleParameter.OracleType = oraType;
            oracleParameter.ParameterName = paramName;
            oracleParameter.Value = paramValue;
            this.oraParams.Add(oracleParameter);
        }

        /// <summary>
        /// 创建OutPut参数
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="oraType"></param>
        public void Add(string paramName, OracleType oraType)
        {
            OracleParameter oracleParameter = new OracleParameter();
            oracleParameter.OracleType = oraType;
            oracleParameter.ParameterName = paramName;
            oracleParameter.Direction = ParameterDirection.Output;
            oracleParameter.Size = 200;
            this.oraParams.Add(oracleParameter);
        }

        public List<OracleParameter> GetParameters()
        {
            return this.oraParams;
        }

        public void Clear()
        {
            this.oraParams.Clear();
        }
    }



    public class Helper
    {
        public Helper(string strConnection)
        {
            dbConnection = new OracleConnection(strConnection);
        }
        #region orcale  数据库操作


        /// <summary>
        /// 数据库连接
        /// </summary>
        OracleConnection dbConnection = null;


        /// <summary>
        /// 执行sql,返回第一行第一列对象
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public object ExecuteScalar(string strSql)
        {
            dbConnection.Open();
            OracleCommand oracleCmd = new OracleCommand(strSql, dbConnection);
            object objRet = oracleCmd.ExecuteScalar();
            dbConnection.Close();
            if (objRet.ToString() == "" || objRet.ToString() == null)
            {
                objRet = 0;
            }
            return objRet;
        }
        /// <summary>
        /// 执行Sql,更新数据
        /// </summary>
        /// <param name="strSql"></param>
        public void ExecuteNonQuery(string strSql)
        {
            dbConnection.Open();
            OracleCommand oracleCmd = new OracleCommand(strSql, dbConnection);
            int ss = oracleCmd.ExecuteNonQuery();
            int b = 0;
            b = ss;

            dbConnection.Close();
        }
        /// <summary>
        /// 执行数据库命令,返回查询结果集
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <returns>结果集</returns>
        public DataSet ExecuteDataSet(string strSQL)
        {
            DataSet ds = new DataSet();
            dbConnection.Open();
            OracleCommand oracleCmd = new OracleCommand(strSQL, dbConnection);
            OracleDataAdapter da = new OracleDataAdapter(oracleCmd);
            da.Fill(ds);
            dbConnection.Close();
            return ds;
        }
        #endregion
    }
}

