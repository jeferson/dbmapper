using System;

namespace DbMapper.BusinessObjects.DatabaseObjects
{
    public class Table
    {
        public int TableObjectId { get; set; }
        public string TableObjectName { get; set; }
        public DateTime TableCreateDate { get; set; }
        public DateTime TableModifyDate { get; set; }

        public int SchemaId { get; set; }
        public Schema Schema { get; set; }
    }
}
