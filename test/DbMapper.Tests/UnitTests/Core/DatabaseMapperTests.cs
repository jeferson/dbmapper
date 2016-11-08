using DbMapper.BusinessObjects.DatabaseObjects;
using DbMapper.Core;
using DbMapper.Core.Interfaces;
using DbMapper.DAL.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DbMapper.Tests.UnitTests.Core
{
    [TestFixture]
    [Category("UnitTests")]
    public class DatabaseMapperTests
    {

        #region .: MapSchemas Tests :. 

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
    
        [Test]
        public void MapSchemas_HasSchemas_ReturnsNonEmptyList()
        {
            // Arrange
            var schema = new Schema { SchemaId = 1, SchemaName = "dbo" };

            IDatabaseMappingDAO dbMappingDAOStub = Substitute.For<IDatabaseMappingDAO>();
            dbMappingDAOStub.GetDatabaseSchemas().Returns(new List<Schema> { schema });

            // Act
            IDatabaseMapper dbMapper = new DatabaseMapper(dbMappingDAOStub);
            dbMapper.MapSchemas();

            var dbMapping = dbMapper.DatabaseMapping;

            // Assert
            Assert.IsNotEmpty(dbMapping.Schemas);
        }

        #endregion .: MapSchemas Tests :. 

        #region .: MapTables Tests :.

        [Test]
        public void MapTables_HasNoTables_ReturnsEmptyList()
        {
            // Arrange
            IDatabaseMappingDAO dbMappingDAOStub = Substitute.For<IDatabaseMappingDAO>();
            dbMappingDAOStub.GetDatabaseTables().Returns(new List<Table>());

            // Act
            IDatabaseMapper dbMapper = new DatabaseMapper(dbMappingDAOStub);
            dbMapper.MapTables();

            var dbMapping = dbMapper.DatabaseMapping;

            // Assert
            Assert.IsEmpty(dbMapping.Tables);
        }

        [Test]
        public void MapTables_HasTables_ReturnsNonEmptyList()
        {
            // Arrange
            var table = new Table
            {
                TableObjectId = 1000,
                TableObjectName = "TableName",
                TableCreateDate = DateTime.Today - TimeSpan.FromDays(-30),
                TableModifyDate = DateTime.Today - TimeSpan.FromDays(-15),
                SchemaId = 1
            };

            IDatabaseMappingDAO dbMappingDAOStub = Substitute.For<IDatabaseMappingDAO>();
            dbMappingDAOStub.GetDatabaseTables().Returns(new List<Table> { table });

            // Act
            IDatabaseMapper dbMapper = new DatabaseMapper(dbMappingDAOStub);
            dbMapper.MapTables();

            var dbMapping = dbMapper.DatabaseMapping;

            // Assert
            Assert.IsNotEmpty(dbMapping.Tables);
        }

        #endregion .: MapTables Tests :.
    }
}
