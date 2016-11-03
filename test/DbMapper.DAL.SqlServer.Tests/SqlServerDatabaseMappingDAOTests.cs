using DbMapper.BusinessObjects.DatabaseObjects;
using DbMapper.DAL.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMapper.DAL.SqlServer.Tests
{
    [TestFixture]
    public class SqlServerDatabaseMappingDAOTests
    {
        [Test]
        public void GetDatabaseSchemas_OnlyDefaultSchema_ReturnsDefaultSchema()
        {
            IDatabaseContext dbContext = new SimpleDatabaseContext(@"Data Source=.\SQLExpress;Initial Catalog=DbMapperTestDatabase;Integrated Security=True;ApplicationIntent=ReadOnly;Application Name=DbMapper.DAL.SqlServer.Tests;");

            IDatabaseMappingDAO dbMappingDAO = new SqlServerDatabaseMappingDAO();

            IEnumerable<Schema> schemas = dbMappingDAO.GetDatabaseSchemas();

            Assert.AreEqual(schemas.Count(), 1);
            Assert.AreEqual(schemas.Single().SchemaName, "dbo");
        }
    }
}
