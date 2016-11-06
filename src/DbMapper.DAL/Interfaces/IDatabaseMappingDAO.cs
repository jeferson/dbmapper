using DbMapper.BusinessObjects.DatabaseObjects;
using System.Collections.Generic;

namespace DbMapper.DAL.Interfaces
{
    public interface IDatabaseMappingDAO
    {
        IEnumerable<Schema> GetDatabaseSchemas();
        IEnumerable<Table> GetDatabaseTables();
    }
}
