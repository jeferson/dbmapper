using DbMapper.BusinessObjects.DatabaseObjects;
using DbMapper.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;

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
    }
}
