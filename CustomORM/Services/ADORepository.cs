using CustomORM.Interfaces;
using CustomORM.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Data;

namespace CustomORM.Services
{
    internal class ADORepository : IRepository //Consider using transactions
    {
        private AttributeHelper _helper;
        private string _connectionString;
        private Dictionary<string, string> _tablesAndPrimaryKeys = new Dictionary<string, string>();

        internal ADORepository(string connectionString)
        {
            _helper = new AttributeHelper();
            _connectionString = connectionString;
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "SELECT tabcons.TABLE_NAME AS TableName, colcons.COLUMN_NAME AS PrimaryKeyColumn FROM " +
                                "INFORMATION_SCHEMA.TABLE_CONSTRAINTS tabcons JOIN " +
                                "INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE colcons " +
                                "ON colcons.Table_Name = tabcons.Table_Name AND tabcons.Constraint_Name = colcons.Constraint_Name " +
                                "WHERE tabcons.Constraint_Type = 'PRIMARY KEY';";
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                sqlCommand.Connection = connection;
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    _tablesAndPrimaryKeys.Add(reader.GetString(0), reader.GetString(1));
                }
                connection.Close();
            }
        }

        void IRepository.Create(DBObject dBObject)
        {
            SqlCommand command = new SqlCommand();
            StringBuilder commandBuilder = new StringBuilder();
            commandBuilder.Append("INSERT INTO " + dBObject.TableName + " (");

            int count = dBObject.RowValues.Count;
            for (int i = 0; i < count; i++)
            {
                commandBuilder.Append(dBObject.ColumnNames[i]);
                if (i != count - 1)
                    commandBuilder.Append(", ");
            };
            commandBuilder.Append(")  VALUES( ");
            for (int i = 0; i < count; i++)
            {
                command.Parameters.Add(new SqlParameter("@" + dBObject.ColumnNames[i] + "value", dBObject.ColumnDataTypes[i])
                { Value = dBObject.RowValues[i] });

                commandBuilder.Append("@" + dBObject.ColumnNames[i] + "value");
                if (i != count - 1)
                    commandBuilder.Append(", ");
            };
            commandBuilder.Append(");");
            command.CommandText = commandBuilder.ToString();

            int result;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                command.Connection = connection;
                connection.Open();
                result = command.ExecuteNonQuery();
                connection.Close();
            }

        }
        void IRepository.Update(DBObject dBObject)
        {   
            SqlCommand command = new SqlCommand();
            StringBuilder commandBuilder = new StringBuilder();
            commandBuilder.Append("UPDATE " + dBObject.TableName + " SET ");
            int count = dBObject.RowValues.Count;
            for (int i = 0; i < count; i++)
            {
                command.Parameters.Add(new SqlParameter("@" + dBObject.ColumnNames[i] + "value", dBObject.ColumnDataTypes[i])
                { Value = dBObject.RowValues[i] });

                commandBuilder.Append(dBObject.ColumnNames[i] + " = @" + dBObject.ColumnNames[i] + "value");
                if (i != count - 1)
                    commandBuilder.Append(", ");
            };
            command.Parameters.AddWithValue("@id", dBObject.PrimaryKey);
            commandBuilder.Append(" WHERE " + _tablesAndPrimaryKeys[dBObject.TableName] + "= @id");
            command.CommandText = commandBuilder.ToString();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
        void IRepository.Delete(object id, string tablename)
        {

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "DELETE FROM " + tablename + " WHERE " + _tablesAndPrimaryKeys[tablename] + "= @id";
            
            int result;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                command.Connection = connection;
                connection.Open();
                result = command.ExecuteNonQuery();
                connection.Close();
            }
            if (result != 1)
                throw new Exception("Database error inserting");
        }
        DBObject IRepository.Get(object id, string tableName)
        {
            var dbo = new DBObject();
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "SELECT * FROM " + tableName + " WHERE " + _tablesAndPrimaryKeys[tableName] + "= @id";

            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                command.Connection = connection;
                connection.Open();
                reader = command.ExecuteReader();
                int count = reader.FieldCount;
                reader.Read();
                for (int i = 0; i < count; i++)
                {
                    dbo.Add(reader.GetValue(i), reader.GetColumnSchema()[i].ColumnName, _helper.ParseToSqlDbType(reader.GetColumnSchema()[i].DataTypeName));
                }
                connection.Close();
            }
            dbo.TableName = tableName;
            return dbo;
        }
        List<DBObject> IRepository.GetAll(string tableName)
        {
            List<DBObject> dboList = new List<DBObject>();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM " + tableName;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                command.Connection = connection;
                connection.Open();
                var reader = command.ExecuteReader();
                int count = reader.FieldCount;
                while (reader.Read())
                {
                    var dbo = new DBObject();
                    dbo.TableName = tableName;
                    for (int i = 0; i < count; i++)
                    {
                        dbo.Add(reader.GetValue(i), reader.GetColumnSchema()[i].ColumnName, _helper.ParseToSqlDbType(reader.GetColumnSchema()[i].DataTypeName));
                    }
                    dboList.Add(dbo);
                }
                connection.Close();
            }
            return dboList;
        }
        List<DBObject> IRepository.GetAllWithForeignKey(string firstTableName, object foreignKeyValue, string secondTableName, bool toMany)
        {
            List<DBObject> dboList = new List<DBObject>();
            SqlCommand command = new SqlCommand();
            string foreignkey;
            if (toMany)
            {
                foreignkey = _tablesAndPrimaryKeys[firstTableName];
            }
            else
            { 
                foreignkey = _tablesAndPrimaryKeys[secondTableName];
            }
            command.Parameters.AddWithValue("@foreignKeyValue", foreignKeyValue);
            command.CommandText = "SELECT " + secondTableName + ".* FROM " + firstTableName +
                " JOIN " + secondTableName + " ON " + firstTableName + "." + foreignkey + " = " + secondTableName + "." + foreignkey + 
                " WHERE " + secondTableName + "." + foreignkey + " = @foreignKeyValue";
            //Need to fix situation where foreign key column name in second table is different
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                command.Connection = connection;
                connection.Open();
                var reader = command.ExecuteReader();
                int count = reader.FieldCount;
                while (reader.Read())
                {
                    var dbo = new DBObject();
                    dbo.TableName = secondTableName;
                    for (int i = 0; i < count; i++)
                    {
                        dbo.Add(reader.GetValue(i), reader.GetColumnSchema()[i].ColumnName, _helper.ParseToSqlDbType(reader.GetColumnSchema()[i].DataTypeName));
                    }
                    dboList.Add(dbo);
                }
                connection.Close();
            }
            return dboList;
        }
        bool IRepository.Exists(DBObject dBObject)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", dBObject.PrimaryKey);
            command.CommandText = "SELECT * FROM " + dBObject.TableName + " WHERE " + _tablesAndPrimaryKeys[dBObject.TableName] + "= @id";
            bool hasRows = false;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                command.Connection = connection;
                connection.Open();
                hasRows = command.ExecuteReader().HasRows;
                connection.Close();
            }
            return hasRows;
        }
    }
}
