using MySql.Data.MySqlClient;

namespace SaborFit.DAOs
{
    public class ConnectionFactory
    {
        public static MySqlConnection Build()
        {
            return new MySqlConnection("Server=localhost;Database=SaborFit;Uid=root;Pwd=root;");
        }
    }
}
