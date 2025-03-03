using Microsoft.Data.SqlClient;

namespace _04_DB_01_SQL_connection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PR2B;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insQuery = "INSERT INTO Cars(Id, RegPlat, Brand, Model, Purchased) VALUES (@Id, @RegPlat, @Brand, @Model, @Purchased)";

                SqlCommand insCommand = new SqlCommand(insQuery, connection);
                insCommand.Parameters.AddWithValue("@Id", 3);
                insCommand.Parameters.AddWithValue("@RegPlat", "3E33333");
                insCommand.Parameters.AddWithValue("@Brand", "Škoda");
                insCommand.Parameters.AddWithValue("@Model", "Karoq");
                insCommand.Parameters.AddWithValue("@Purchased", DateTime.Now);

                insCommand.ExecuteNonQuery();


                string query = "SELECT * FROM Cars";

                using (SqlCommand command = new SqlCommand(query, connection)) 
                {
                    using (SqlDataReader reader = command.ExecuteReader()) 
                    {
                        while (reader.Read()) 
                        {
                            int id = reader.GetInt32(0);
                            string RegPlat = reader.GetString(1);
                            string Brand = (string)reader["Brand"];
                            string Model = (string)reader["Model"];
                            DateTime purchased = reader.GetDateTime(4);
                        }
                    }
                }
            }
        }
    }
}
