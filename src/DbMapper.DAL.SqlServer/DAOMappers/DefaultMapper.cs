using DbMapper.BusinessObjects.DatabaseObjects;
using System;
using System.Data.SqlClient;

namespace DbMapper.DAL.SqlServer.DAOMappers
{
    internal static class DefaultMapper
    {
        internal static T Map<T>(SqlDataReader reader)
            where T : new ()
        {
            var objType = typeof(T);

            if (objType == typeof(Schema))
            {
                Schema schema = MapSchema(reader);
                return (T)Convert.ChangeType(schema, objType);
            }
            else if (objType == typeof(Table))
            {
                var table = MapTable(reader);
                return (T)Convert.ChangeType(table, objType);
            }

            throw new InvalidOperationException(
                String.Format("No entity-relational modal mapper found for type {0}", objType.Name));
        }

        private static Schema MapSchema(SqlDataReader reader)
        {
            int schemaId = (int)reader["schema_id"];
            string schemaName = (string)reader["schema_name"];

            return new Schema { SchemaId = schemaId, SchemaName = schemaName };
        }

        private static Table MapTable(SqlDataReader reader)
        {
            int tableObjectId = (int)reader["table_object_id"];
            string tableObjectName = (string)reader["table_object_name"];
            DateTime tableCreateDate = (DateTime)reader["table_create_date"];
            DateTime tableModifyDate = (DateTime)reader["table_modify_date"];
            int schemaId = (int)reader["schema_object_id"];

            return new Table
            {
                TableId = tableObjectId,
                TableName = tableObjectName,
                TableCreateDate = tableCreateDate,
                TableModifyDate = tableModifyDate,
                SchemaId = schemaId
            };
        }
    }
}
