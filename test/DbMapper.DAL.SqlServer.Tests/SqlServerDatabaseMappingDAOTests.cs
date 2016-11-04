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
        [Test]
        public void GetDatabaseSchemas_OnlyDefaultSchema_ReturnsDefaultSchema()
        {
            IDatabaseContext dbContext = new SimpleDatabaseContext(@"Data Source=.\SQLExpress;Initial Catalog=DbMapperTestDatabase;Integrated Security=True;ApplicationIntent=ReadOnly;Application Name=DbMapper.DAL.SqlServer.Tests;");

            IDatabaseMappingDAO dbMappingDAO = new SqlServerDatabaseMappingDAO(dbContext);

            IEnumerable<Schema> schemas = dbMappingDAO.GetDatabaseSchemas();

            Assert.AreEqual(1, schemas.Count());
            Assert.AreEqual("dbo", schemas.Single().SchemaName);
        }
    }
}
