using DbMapper.BusinessObjects;

namespace DbMapper.Core.Interfaces
{
    public interface IDatabaseMapper
    {
        DatabaseMap DatabaseMapping { get; }

        void Map();

        void MapSchemas();
        void MapTables();
    }
}
