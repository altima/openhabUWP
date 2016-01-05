
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
        public OpenhabDatabase()
        {
            
        }

        public Setup UpdateSetup(Setup setup)
        {
            throw new System.NotImplementedException();
        }

        public Setup GetSetup()
        {
            throw new System.NotImplementedException();
        }
    }
}
