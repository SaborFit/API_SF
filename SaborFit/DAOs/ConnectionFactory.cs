using MySql.Data.MySqlClient;

namespace SaborFit.DAOs
{
    public class ConnectionFactory
    {
        public static MySqlConnection Build()
        {
            return new MySqlConnection("Server=saborfit-etec-pw.mysql.database.azure.com;Database=SaborFit;Uid=saborfit;Pwd=S3nhadific1l;");
        }
    }
}
