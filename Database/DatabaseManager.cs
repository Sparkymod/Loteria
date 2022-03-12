using MySql.Data.MySqlClient;
using Serilog;
using SqlKata.Compilers;
using SqlKata.Execution;


namespace Loteria.Database
{
    public class DatabaseManager : IDisposable
    {
        public DatabaseLoto Loto { get; set; } = new DatabaseLoto();

        public readonly string ConnectionString = Settings.GetConnectionString();
        private readonly string ConnectionStringWithoutDB = Settings.GetConnectionStringWithoutDB();

        public bool DatabaseExists(string database)
        {
            try
            {
                dynamic? result = new QueryFactory(new MySqlConnection(ConnectionStringWithoutDB), new MySqlCompiler())
                    .Select($"SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{database}'")
                    .FirstOrDefault();

                return result != null;
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Error getting Database information: {ex.Message}");
                Dispose();
                return false;
            }
        }

        #region Diposing
        bool isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            isDisposed = true;
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed)
            {
                return;
            }
            if (isDisposing)
            {
                // release managed resource.
            }
            // release unmanaged resources.
        }
        #endregion
    }
}
