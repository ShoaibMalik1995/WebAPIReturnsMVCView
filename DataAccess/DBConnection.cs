using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class DBConnection
    {
        private SqlConnection m_SqlConnection;
        private SqlCommand m_SqlCommand;
        private SqlTransaction m_Transaction;
        private bool m_BeginTransaction = false;
        /// <summary>
        /// Constructor
        /// </summary>
        public DBConnection()
        {
            m_SqlConnection = new SqlConnection(ConfigurationManager.AppSettings["constr"].ToString());
            m_SqlCommand = new SqlCommand();
            m_SqlCommand.Connection = m_SqlConnection;
        }

        public DBConnection(string conn)
        {
            //m_SqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[1].ToString());
            m_SqlConnection = new SqlConnection(conn);
            m_SqlCommand = new SqlCommand();
            m_SqlCommand.Connection = m_SqlConnection;
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenConnection()
        {
            if (m_BeginTransaction)
                return;

            if (m_SqlConnection.State == ConnectionState.Open)
                m_SqlConnection.Close();

            m_SqlConnection.Open();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CloseConnection()
        {
            if (m_BeginTransaction)
                return;

            if (m_SqlConnection.State == ConnectionState.Open)
                m_SqlConnection.Close();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool BeginTransaction()
        {
            OpenConnection();
            m_Transaction = m_SqlConnection.BeginTransaction();
            m_SqlCommand.Transaction = m_Transaction;
            m_BeginTransaction = true;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CommitTransaction()
        {
            m_Transaction.Commit();
            m_BeginTransaction = false;
            CloseConnection();
        }

        /// <summary>
        /// 
        /// </summary>
        public void RollbackTransaction()
        {
            m_Transaction.Rollback();
            m_BeginTransaction = false;
            CloseConnection();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_StrQuery"></param>
        /// <param name="p_Params"></param>
        /// <returns></returns>
        public object GetSingleRecord(string p_StrQuery, CommandType p_CommandType, ParamCollection p_Params)
        {
            try
            {

                m_SqlCommand.CommandText = p_StrQuery;
                m_SqlCommand.CommandType = p_CommandType;
                m_SqlCommand.Parameters.Clear();

                if (p_Params != null && p_Params.Count > 0)
                {
                    foreach (SqlParameter l_Parm in p_Params)
                    {
                        m_SqlCommand.Parameters.Add(l_Parm);
                    }
                }

                this.OpenConnection();
                return m_SqlCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_StrQuery"></param>
        /// <returns></returns>
        public object GetSingleRecord(string p_StrQuery, CommandType p_CommandType)
        {
            try
            {
                m_SqlCommand.CommandText = p_StrQuery;
                m_SqlCommand.CommandType = p_CommandType;

                this.OpenConnection();
                return m_SqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_StrQuery"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string p_StrQuery, CommandType p_CommandType)
        {
            try
            {
                m_SqlCommand.CommandText = p_StrQuery;
                m_SqlCommand.CommandType = p_CommandType;
                m_SqlCommand.Parameters.Clear();
                m_SqlCommand.CommandTimeout = 0;
                this.OpenConnection();

                SqlDataAdapter l_Adaper = new SqlDataAdapter();
                DataTable l_Table = new DataTable();

                l_Adaper.SelectCommand = m_SqlCommand;
                l_Adaper.Fill(l_Table);

                return l_Table;

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_StrQuery"></param>
        /// <param name="p_Params"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string p_StrQuery, CommandType p_CommandType, ParamCollection p_Params)
        {
            try
            {
                m_SqlCommand.CommandText = p_StrQuery;
                m_SqlCommand.Parameters.Clear();
                m_SqlCommand.CommandType = p_CommandType;
                m_SqlCommand.CommandTimeout = 0;
                if (p_Params != null && p_Params.Count > 0)
                {
                    foreach (SqlParameter l_Parm in p_Params)
                    {
                        m_SqlCommand.Parameters.Add(l_Parm);
                    }
                }

                this.OpenConnection();

                SqlDataAdapter l_Adaper = new SqlDataAdapter();
                DataTable l_Table = new DataTable();

                l_Adaper.SelectCommand = m_SqlCommand;
                l_Adaper.Fill(l_Table);

                return l_Table;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_StrQuery"></param>
        /// <param name="p_Params"></param>
        /// <returns></returns>
        public DataSet GetDatSet(string p_StrQuery, CommandType p_CommandType, ParamCollection p_Params)
        {
            try
            {
                m_SqlCommand.CommandText = p_StrQuery;
                m_SqlCommand.Parameters.Clear();
                m_SqlCommand.CommandType = p_CommandType;
                m_SqlCommand.CommandTimeout = 0;
                if (p_Params != null && p_Params.Count > 0)
                {
                    foreach (SqlParameter l_Parm in p_Params)
                    {
                        m_SqlCommand.Parameters.Add(l_Parm);
                    }
                }

                if (!m_BeginTransaction)
                    this.OpenConnection();

                SqlDataAdapter l_Adaper = new SqlDataAdapter();
                DataSet l_DataSet = new DataSet();

                l_Adaper.SelectCommand = m_SqlCommand;
                l_Adaper.Fill(l_DataSet);

                return l_DataSet;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_StrQuery"></param>
        /// <returns></returns>
        public DataSet GetDatSet(string p_StrQuery, CommandType p_CommandType)
        {
            try
            {
                m_SqlCommand.Parameters.Clear();
                m_SqlCommand.CommandText = p_StrQuery;
                m_SqlCommand.CommandType = p_CommandType;
                m_SqlCommand.CommandTimeout = 0;
                this.OpenConnection();

                SqlDataAdapter l_Adaper = new SqlDataAdapter();
                DataSet l_DataSet = new DataSet();

                l_Adaper.SelectCommand = m_SqlCommand;
                l_Adaper.Fill(l_DataSet);

                return l_DataSet;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.CloseConnection();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_StrQuery"></param>
        /// <param name="p_Params"></param>
        /// <returns></returns>
        public int ExecuteQuery(string p_StrQuery, CommandType p_CommandType, ParamCollection p_Params)
        {
            try
            {
                m_SqlCommand.CommandText = p_StrQuery;
                m_SqlCommand.Parameters.Clear();
                m_SqlCommand.CommandType = p_CommandType;
                m_SqlCommand.CommandTimeout = 0;
                if (p_Params != null && p_Params.Count > 0)
                {
                    foreach (SqlParameter l_Parm in p_Params)
                    {
                        m_SqlCommand.Parameters.Add(l_Parm);
                    }
                }

                this.OpenConnection();
                return m_SqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                this.CloseConnection();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_StrQuery"></param>
        /// <returns></returns>
        public int ExecuteQuery(string p_StrQuery, CommandType p_CommandType)
        {
            try
            {
                m_SqlCommand.CommandText = p_StrQuery;
                m_SqlCommand.CommandType = p_CommandType;
                m_SqlCommand.CommandTimeout = 0;
                this.OpenConnection();
                return m_SqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                this.CloseConnection();
            }
        }
    }

    public class CustomDataSet : DataSet
    {
        public override SchemaSerializationMode SchemaSerializationMode
        {
            get
            {
                return base.SchemaSerializationMode;
            }
            set
            {
                base.SchemaSerializationMode = value;
            }
        }
    }
}
