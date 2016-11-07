using DbMapper.BusinessObjects.DatabaseObjects;
using DbMapper.Core;
using DbMapper.Core.Interfaces;
using DbMapper.DAL.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace DbMapper.Tests.UnitTests.Core
{
    [TestFixture]
    [Category("UnitTests")]
    public class DatabaseMapperTests
    {
        [Test]
        public void MapSchemas_HasNoSchemas_ReturnEmptyList()
        {
            // Arrange
            IDatabaseMappingDAO dbMappingDAOStub = Substitute.For<IDatabaseMappingDAO>();
            dbMappingDAOStub.GetDatabaseSchemas().Returns(new List<Schema>());

            // Act
            IDatabaseMapper dbMapper = new DatabaseMapper(dbMappingDAOStub);
            dbMapper.MapSchemas();

            var dbMapping = dbMapper.DatabaseMapping;

            // Assert 
            Assert.IsEmpty(dbMapping.Schemas);
        }
    }
}
