using SQLite.Net.Attributes;

namespace openhabUWP.Database
{
    public interface ITable
    {
        int Id { get; set; }
    }


    [Table("setup")]
    public class Setup : ITable
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public string Url { get; set; }
        public string RemoteUrl { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public bool IgnoreSslCertificate { get; set; }
        public bool IgnoreSslHostnames { get; set; }
    }
}
