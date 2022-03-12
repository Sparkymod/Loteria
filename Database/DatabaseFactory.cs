using MySql.Data.MySqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace Loteria.Database
{
    public abstract class DatabaseFactory
    {
        protected string? TableName;

        protected static QueryFactory QueryFactory
        {
            get
            {
                using DatabaseManager manager = new();
                using MySqlConnection connection = new(manager.ConnectionString);
                QueryFactory queryFactory = new(connection, new MySqlCompiler());
                return queryFactory;
            }
        }

        public DatabaseFactory(string tableName)
        {
            TableName = tableName;
        }
    }
}
