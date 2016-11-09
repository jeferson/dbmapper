using System.Collections.Generic;

namespace DbMapper.BusinessObjects.DatabaseObjects
{
    public class Schema
    {
        public int SchemaId { get; set; }
        public string SchemaName { get; set; }

        public ICollection<Table> Tables { get; set; } = new List<Table>();
    }
}
