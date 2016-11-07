using DbMapper.BusinessObjects.DatabaseObjects;
using DbMapper.DAL.Interfaces;
using DbMapper.DAL.SqlServer.DAO;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DbMapper.DAL.SqlServer.Tests
{
    [TestFixture]
    public class SqlServerDatabaseMappingDAOTests
    {
        private IDatabaseContext _dbContext;
        private IDatabaseMappingDAO _dbMappingDAO;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _dbContext = new SimpleDatabaseContext(@"Data Source=.\SQLExpress;Initial Catalog=DbMapperTestDatabase;Integrated Security=True;ApplicationIntent=ReadOnly;Application Name=DbMapper.DAL.SqlServer.Tests;");
            _dbMappingDAO = new SqlServerDatabaseMappingDAO(_dbContext);
        }

        [Test]
        public void GetDatabaseSchemas_OnlyDefaultSchema_ReturnsDefaultSchema()
        {
            IEnumerable<Schema> schemas = _dbMappingDAO.GetDatabaseSchemas();

            Assert.AreEqual(1, schemas.Count());
            Assert.AreEqual("dbo", schemas.Single().SchemaName);
        }

        [Test]
        public void GetDatabaseTables_ExistingTables_ReturnsExistingTables()
        {
            IEnumerable<Table> tables = _dbMappingDAO.GetDatabaseTables();
            
            Assert.Greater(tables.Count(), 0);
        }
    }
}
