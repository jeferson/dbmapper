using DbMapper.BusinessObjects;
using DbMapper.Core.Interfaces;
using DbMapper.DAL.Interfaces;
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

        public void MapSchemas()
        {
            DatabaseMapping.Schemas = _dbMappingDAO.GetDatabaseSchemas().ToArray();
        }
    }
}
