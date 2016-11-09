using DbMapper.BusinessObjects.DatabaseObjects;
using DbMapper.Core;
using DbMapper.Core.Interfaces;
using DbMapper.DAL.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        #region .: MapSchemaTablesRelationships Tests :. 

        [Test]
        public void MapSchemaTableRelationships_NoMappedSchemas_ThrowsInvalidOperationException()
        {
            // Arrange
            Table[] tables = new Table[]
            {
                new Table { TableObjectId = 1, TableObjectName = "Table 1", TableCreateDate = DateTime.Now, TableModifyDate = DateTime.Now, SchemaId = 1 },
                new Table { TableObjectId = 2, TableObjectName = "Table 2", TableCreateDate = DateTime.Now, TableModifyDate = DateTime.Now, SchemaId = 1 },
                new Table { TableObjectId = 3, TableObjectName = "Table 3", TableCreateDate = DateTime.Now, TableModifyDate = DateTime.Now, SchemaId = 1 },
                new Table { TableObjectId = 4, TableObjectName = "Table 4", TableCreateDate = DateTime.Now, TableModifyDate = DateTime.Now, SchemaId = 1 },
                new Table { TableObjectId = 5, TableObjectName = "Table 5", TableCreateDate = DateTime.Now, TableModifyDate = DateTime.Now, SchemaId = 1 },
            };

            IDatabaseMappingDAO dbMappingDAO = Substitute.For<IDatabaseMappingDAO>();
            dbMappingDAO.GetDatabaseSchemas().Returns(new List<Schema>());
            dbMappingDAO.GetDatabaseTables().Returns(tables);

            // Act
            IDatabaseMapper dbMapper = new DatabaseMapper(dbMappingDAO);
            dbMapper.MapSchemas();
            dbMapper.MapTables();

            // Assert
            Assert.Throws<InvalidOperationException>(() => { dbMapper.MapSchemaTableRelationships(); });
        }

        [Test]
        public void MapSchemaTableRelationships_NoMappedTables_ThrowsInvalidOperationException()
        {
            // Arrange
            Schema[] schemas = new Schema[]
            {
                new Schema { SchemaId = 1, SchemaName = "Schema 1" },
                new Schema { SchemaId = 2, SchemaName = "Schema 2" },
                new Schema { SchemaId = 3, SchemaName = "Schema 3" },
            };

            IDatabaseMappingDAO dbMappingDAO = Substitute.For<IDatabaseMappingDAO>();
            dbMappingDAO.GetDatabaseSchemas().Returns(schemas);
            dbMappingDAO.GetDatabaseTables().Returns(new List<Table>());

            // Act
            IDatabaseMapper dbMapper = new DatabaseMapper(dbMappingDAO);
            dbMapper.MapSchemas();
            dbMapper.MapTables();

            // Assert
            Assert.Throws<InvalidOperationException>(() => { dbMapper.MapSchemaTableRelationships(); });
        }

        [Test]
        public void MapSchemaTableRelationships_HasMatchingRelationships_MapRelationshipsCorrecly()
        {
            // Arrange
            Schema[] schemas = new Schema[]
            {
                new Schema { SchemaId =  1, SchemaName = "Schema 1" },
                new Schema { SchemaId =  2, SchemaName = "Schema 2" },
                new Schema { SchemaId =  3, SchemaName = "Schema 3" },
            };

            var tableCreateDate = DateTime.Now - TimeSpan.FromDays(-30);
            var tableModifyDate = DateTime.Now - TimeSpan.FromDays(-15);

            Table[] tables = new Table[]
            {
                new Table { TableObjectId = 1, TableObjectName = "Table 1", TableCreateDate = tableCreateDate, TableModifyDate = tableModifyDate, SchemaId = 1 },
                new Table { TableObjectId = 2, TableObjectName = "Table 2", TableCreateDate = tableCreateDate, TableModifyDate = tableModifyDate, SchemaId = 1 },
                new Table { TableObjectId = 3, TableObjectName = "Table 3", TableCreateDate = tableCreateDate, TableModifyDate = tableModifyDate, SchemaId = 2 },
                new Table { TableObjectId = 4, TableObjectName = "Table 4", TableCreateDate = tableCreateDate, TableModifyDate = tableModifyDate, SchemaId = 2 },
                new Table { TableObjectId = 5, TableObjectName = "Table 5", TableCreateDate = tableCreateDate, TableModifyDate = tableModifyDate, SchemaId = 3 },
                new Table { TableObjectId = 6, TableObjectName = "Table 6", TableCreateDate = tableCreateDate, TableModifyDate = tableModifyDate, SchemaId = 3 }
            };

            IDatabaseMappingDAO dbMappingDAO = Substitute.For<IDatabaseMappingDAO>();
            dbMappingDAO.GetDatabaseSchemas().Returns(schemas);
            dbMappingDAO.GetDatabaseTables().Returns(tables);

            // Act
            IDatabaseMapper dbMapper = new DatabaseMapper(dbMappingDAO);
            dbMapper.MapSchemas();
            dbMapper.MapTables();

            dbMapper.MapSchemaTableRelationships();

            // Asserts

            #region .: Asserts for schema 1 :.

            var schema1 = schemas.Single(s => s.SchemaId == 1);
            var table1 = tables.Single(t => t.TableObjectId == 1);
            var table2 = tables.Single(t => t.TableObjectId == 2);

            // Asserts that tables 1 and 2 points to schema1
            Assert.AreSame(schema1, table1.Schema);
            Assert.AreSame(schema1, table2.Schema);

            // Asserts that schema1 contains tables 1 and 2

            Assert.Contains(table1, schema1.Tables as ICollection);
            Assert.Contains(table2, schema1.Tables as ICollection);

            #endregion .: Asserts for schema 1 :.

            #region .: Asserts for schema 2 :.

            var schema2 = schemas.Single(s => s.SchemaId == 2);
            var table3 = tables.Single(t => t.TableObjectId == 3);
            var table4 = tables.Single(t => t.TableObjectId == 4);

            // Asserts that tables 3 and 4 points to schema2
            Assert.AreSame(schema2, table3.Schema);
            Assert.AreSame(schema2, table4.Schema);

            // Asserts that schema2 contains tables 3 and 4
            Assert.Contains(table3, schema2.Tables as ICollection);
            Assert.Contains(table4, schema2.Tables as ICollection);

            #endregion .: Asserts for schema 2 :.

            #region .: Asserts for schema 3 :.

            var schema3 = schemas.Single(s => s.SchemaId == 3);
            var table5 = tables.Single(t => t.TableObjectId == 5);
            var table6 = tables.Single(t => t.TableObjectId == 6);

            // Asserts that tables 5 and 6 points to schema3
            Assert.AreSame(schema3, table5.Schema);
            Assert.AreSame(schema3, table6.Schema);

            // Asserts that schema3 contains tables 5 and 6
            Assert.Contains(table5, schema3.Tables as ICollection);
            Assert.Contains(table6, schema3.Tables as ICollection);

            #endregion .: Asserts for schema 3 :.
        }

        #endregion .: MapSchemaTablesRelationships Tests :. 
    }
}
