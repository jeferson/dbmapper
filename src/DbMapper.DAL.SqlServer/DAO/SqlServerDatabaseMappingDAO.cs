using DbMapper.BusinessObjects.DatabaseObjects;
using DbMapper.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace DbMapper.DAL.SqlServer.DAO
{
    public class SqlServerDatabaseMappingDAO : IDatabaseMappingDAO
    {
        private IDatabaseContext _dbContext;

        public SqlServerDatabaseMappingDAO(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Schema> GetDatabaseSchemas()
        {
            IList<Schema> schemas = new List<Schema>();

            using (SqlConnection connection = new SqlConnection(_dbContext.ConnectionString))
            using (SqlCommand command = new SqlCommand(Resources.SelectAllDatabaseSchemas, connection))
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int schemaId = (int)reader["schema_id"];
                        string schemaName = (string)reader["schema_name"];

                        Schema schema = new Schema { SchemaId = schemaId, SchemaName = schemaName };
                        schemas.Add(schema);
                    }
                }
            }

            return schemas;
        }

        public IEnumerable<Table> GetDatabaseTables()
        {
            IList<Table> tables = new List<Table>();

            using (SqlConnection connection = new SqlConnection(_dbContext.ConnectionString))
            using (SqlCommand command = new SqlCommand(Resources.SelectAllDatabaseTables, connection))
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        int tableObjectId = (int)reader["table_object_id"];
                        string tableObjectName = (string)reader["table_object_name"];
                        DateTime tableCreateDate = (DateTime)reader["table_create_date"];
                        DateTime tableModifyDate = (DateTime)reader["table_modify_date"];
                        int schemaId = (int)reader["schema_object_id"];

                        Table table = new Table
                        {
                            TableObjectId = tableObjectId,
                            TableObjectName = tableObjectName,
                            TableCreateDate = tableCreateDate,
                            TableModifyDate = tableModifyDate,
                            SchemaId = schemaId
                        };

                        tables.Add(table);
                    }
                }
            }

            return tables;
        }
    }
}
