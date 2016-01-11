using Lex.Db;
using System.IO;

namespace openhabUWP.Database
{
    public interface IOpenhabDatabase
    {
        Setup UpdateSetup(Setup setup);
        Setup GetSetup();
    }

    public class OpenhabDatabase : IOpenhabDatabase
    {
        private DbInstance _instance;
        public OpenhabDatabase()
        {
            _instance = new DbInstance("/openhab.database");
        }

        public Setup UpdateSetup(Setup setup)
        {
            return setup;
        }

        public Setup GetSetup()
        {
            return _instance.LoadByKey<Setup>("singelSetup") ?? new Setup();
        }
    }
}
