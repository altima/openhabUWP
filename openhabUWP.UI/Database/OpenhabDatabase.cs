using System;
using Lex.Db;
using System.IO;
using System.Linq;
using Windows.Storage;

namespace openhabUWP.Database
{
    public interface IOpenhabDatabase
    {
        int GetNextSetupId();
        Setup UpdateSetup(Setup setup);
        Setup GetSetup();
    }

    public class OpenhabDatabase : IOpenhabDatabase, IDisposable
    {
        private DbInstance _db;
        public OpenhabDatabase()
        {
            Init();
        }

        private void Init()
        {
            if (_db == null)
            {
                //cerate
                _db = new DbInstance("openhab.database", ApplicationData.Current.LocalFolder);
                //setup
                _db.Map<Setup>().Automap(i => i.Id);
                //start
                _db.Initialize();
            }
        }

        public Setup UpdateSetup(Setup setup)
        {
            if (setup.Id == 0)
            {
                setup.Id = GetNextSetupId();
            }
            _db.Table<Setup>().Save(setup);
            return setup;
        }

        public Setup GetSetup()
        {
            return _db.Table<Setup>().FirstOrDefault() ?? new Setup();
        }

        public int GetNextSetupId()
        {
            var table = _db.Table<Setup>();
            if (table != null && table.Any())
            {
                return _db.Table<Setup>().Select(s => s.Id).Max() + 1;
            }
            return 1;
        }

        private bool _isDisposed;
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                _db.Dispose();
                _db = null;
            }
        }
    }
}
