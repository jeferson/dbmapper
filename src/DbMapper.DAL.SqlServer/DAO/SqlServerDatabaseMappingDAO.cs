using DbMapper.BusinessObjects.DatabaseObjects;
using DbMapper.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using DbMapper.DAL.SqlServer.DAOMappers;

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
            return GetDatabaseObjects<Schema>(Resources.SelectAllDatabaseSchemas);
        }

        public IEnumerable<Table> GetDatabaseTables()
        {
            return GetDatabaseObjects<Table>(Resources.SelectAllDatabaseTables);
        }

        private IEnumerable<T> GetDatabaseObjects<T>(string sqlCommandText)
            where T : new()
        {
            IList<T> objs = new List<T>();

            using (SqlConnection connection = new SqlConnection(_dbContext.ConnectionString))
            using (SqlCommand command = new SqlCommand(sqlCommandText, connection))
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                        objs.Add(DefaultMapper.Map<T>(reader));
                }
            }

            return objs;
        }
    }
}
