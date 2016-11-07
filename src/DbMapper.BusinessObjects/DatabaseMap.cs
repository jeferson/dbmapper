using DbMapper.BusinessObjects.DatabaseObjects;

namespace DbMapper.BusinessObjects
{
    public class DatabaseMap
    {
        public Schema[] Schemas { get; set; }
        public Table[] Tables { get; set; }
    }
}
