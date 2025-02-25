﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viikkkvv_bot.Data
{
    class DBConnection
    {
        OleDbConnection connection;

        public async Task CreateConnection()
        {
            //connection = new OleDbConnection(@"Provider = SQLOLEDB.1; Persist Security Info = False; Trusted_Connection = Yes; User ID = " + Login + "; Password = " + Pass + "; Initial Catalog = SnegirSoft_Prod; Data Source = " + DB);
            connection = new OleDbConnection(@"Provider=MSOLEDBSQL.1;Data Source=HOME-PC\SQLEXPRESS;Trusted_Connection=Yes;Persist Security Info=False");

            try
            {
                await connection.OpenAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB error: " + ex.Message);
            }
        }
        //GetData
        public async Task<List<object>> SendCommandRequest(string request)
        {
            OleDbCommand command = new OleDbCommand(request, connection);

            List<object> dbData = new List<object>(); //store data from db here

            try
            {
                OleDbDataReader reader = (OleDbDataReader)await command.ExecuteReaderAsync(); //read data from db

                while (reader.Read())
                {
                    object[] row = new object[reader.FieldCount]; //create row
                    reader.GetValues(row);
                    dbData.Add(row);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB error: " + ex.Message);
            }

            command.Dispose();

            return dbData;
        }
        //Set data
        public async Task SendCommandSet(string request)
        {
            OleDbCommand command = new OleDbCommand(request, connection);
            OleDbTransaction transaction = connection.BeginTransaction();

            try
            {
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB error: " + ex.Message);
            }

            command.Dispose();
        }
        public async Task SendCommandDelete(string request)
        {
            OleDbCommand command = new OleDbCommand(request, connection);
            OleDbTransaction transaction = connection.BeginTransaction();

            try
            {
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB error: " + ex.Message);
            }

            command.Dispose();
        }

        public void CloseConnection()
        {
            if (connection?.State == ConnectionState.Open)
                connection.Close();
        }
    }
}
