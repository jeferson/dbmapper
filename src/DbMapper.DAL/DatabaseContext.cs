using DbMapper.DAL.Interfaces;

namespace DbMapper.DAL
{
    /// <summary>
    /// This class wrap database connection information 
    /// </summary>
    public class DatabaseContext : IDatabaseContext
    {
        /// <summary>
        /// Connection string used to connect to database server
        /// </summary>
        public string ConnectionString { get; private set; }

        public DatabaseContext(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
