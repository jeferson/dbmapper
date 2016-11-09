using DbMapper.BusinessObjects;
using DbMapper.BusinessObjects.DatabaseObjects;
using DbMapper.Core.Interfaces;
using DbMapper.DAL.Interfaces;
using System;
using System.Linq;

namespace DbMapper.Core
{
    public class DatabaseMapper : IDatabaseMapper
    {
        public DatabaseMap DatabaseMapping { get; private set; } = new DatabaseMap();

        private IDatabaseMappingDAO _dbMappingDAO;

        public DatabaseMapper(IDatabaseMappingDAO dbMappingDAO)
        {
            _dbMappingDAO = dbMappingDAO;
        }

        public void Map()
        {
            MapSchemas();
            MapTables();
        }

        public void MapSchemas()
        {
            DatabaseMapping.Schemas = _dbMappingDAO.GetDatabaseSchemas().ToArray();
        }

        public void MapTables()
        {
            DatabaseMapping.Tables = _dbMappingDAO.GetDatabaseTables().ToArray();
        }

        public void MapSchemaTableRelationships()
        {
            if (DatabaseMapping.Schemas.Length <= 0) throw new InvalidOperationException("No schemas mapped");
            if (DatabaseMapping.Tables.Length <= 0) throw new InvalidOperationException("No tables mapped");
            
            DatabaseMapping.Tables.Select(
                table =>
                {
                    var schema = DatabaseMapping.Schemas.Single(s => s.SchemaId == table.SchemaId);

                    // Map Schema to Table.Schema reference
                    table.Schema = schema;

                    // Add table into Schema.
                    schema.Tables.Add(table);

                    return (Table)null;
                }
            ).ToArray();
        }
    }
}
