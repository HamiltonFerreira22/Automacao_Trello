using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SecondTrello
{
    class SQLConnect
    {

        public SqlConnection Conn;
        public bool Connected = false;

        public SQLConnect(string connectionString)
        {
            try
            {
                Conn = new SqlConnection(connectionString);
                OpenConn();
                Connected = ConnOpened();
                CloseConn();
            }
            catch (Exception ex)
            {
                Connected = false;
                //throw new Exception("Unexpected error creating Connector (" + ex.Message.ToString() + ")");
            }
        }

        public void OpenConn()
        {
            if (!ConnOpened())
            {
                Conn.Open();
            }
        }

        public void CloseConn()
        {
            if (ConnOpened())
            {
                Conn.Close();
            }
        }

        public bool ConnOpened()
        {
            return Conn.State == ConnectionState.Open;
        }

        public string getPassword(string user)
        {
            string password = "";
            if (Connected)
            {
                OpenConn();
                if (ConnOpened())
                {   //Query DB
                    string queryText = "select senha from usuarios where login = '" + user + "'";

                    SqlCommand cmdSQLSERVER = Conn.CreateCommand();
                    cmdSQLSERVER.CommandText = queryText;
                    SqlDataReader drSQLSERVER = cmdSQLSERVER.ExecuteReader();

                    if (drSQLSERVER.HasRows)
                    {
                        while (drSQLSERVER.Read())
                        {
                            //password = drSQLSERVER[0].ToString();
                            password = drSQLSERVER["SENHA"].ToString();
                        }
                    }
                    CloseConn();
                }
            }
            return password;
        }
    }
}
