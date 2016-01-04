
using System.IO;
using SQLite.Net;

namespace openhabUWP.Database
{
    public interface IOpenhabDatabase
    {
        Setup UpdateSetup(Setup setup);
        Setup GetSetup();
    }

    public class OpenhabDatabase : IOpenhabDatabase
    {
        public string DbFile = "openhab.db";
        public string DbPath => Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, DbFile);

        public OpenhabDatabase()
        {
            CreateOrUpdate();
        }

        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DbPath);
        }

        private void CreateOrUpdate()
        {
            using (var conn = GetConnection())
            {
                conn.CreateTable<Setup>();
            }
        }

        public Setup UpdateSetup(Setup setup)
        {
            using (var conn = GetConnection())
            {
                conn.InsertOrReplace(setup);
            }
            return setup;
        }

        public Setup GetSetup()
        {
            using (var conn = GetConnection())
            {
                return conn.Table<Setup>().FirstOrDefault() ?? new Setup();
            }
        }
    }
}
