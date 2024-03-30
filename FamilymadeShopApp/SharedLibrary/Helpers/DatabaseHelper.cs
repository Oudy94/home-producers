using SharedLibrary.Enums;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Helpers
{
    public class DatabaseHelper
    {
        private readonly string connectionString = "Server=mssqlstud.fhict.local;Database=dbi534217_familymade;User Id=dbi534217_familymade;Password=123456;";
        private SqlConnection connection;

        public DatabaseHelper()
        {
            connection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error opening database connection: {ex.Message}");
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error closing database connection: {ex.Message}");
            }
        }

		public void AddUserToDB(Customer user)
		{
			string query = "INSERT INTO [user] (name, email, password, registration_date, shipping_address, role) VALUES (@Name, @Email, @Password, @RegistrationDate, @ShippingAddress, @Role)";
			using (SqlCommand cmd = new SqlCommand(query, connection))
			{
				cmd.Parameters.AddWithValue("@Name", user.Name);
				cmd.Parameters.AddWithValue("@Email", user.Email);
				cmd.Parameters.AddWithValue("@Password", user.Password);
				cmd.Parameters.AddWithValue("@RegistrationDate", user.RegistrationDate.ToUniversalTime()); // Convert to UTC
				cmd.Parameters.AddWithValue("@ShippingAddress", user.ShippingAddress);
				cmd.Parameters.AddWithValue("@Role", 0); // Ensure this matches the role ID datatype in your database
				cmd.ExecuteNonQuery();
			}
		}

		public Product GetProductByIdFromDB(int id)
		{
			Product product = null;

			string query = "SELECT * FROM product WHERE id = @Id";

			using (SqlCommand cmd = new SqlCommand(query, connection))
			{
				cmd.Parameters.AddWithValue("@Id", id);

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						product = new Product
						(
							reader.GetInt32(reader.GetOrdinal("id")),
							reader.GetString(reader.GetOrdinal("name")),
							reader.GetString(reader.GetOrdinal("description")),
							(CategoryEnum)reader.GetInt32(reader.GetOrdinal("category")) - 1,
							reader.GetDecimal(reader.GetOrdinal("price")),
							reader.GetInt32(reader.GetOrdinal("quantity")),
							new List<string> { "", "", "" },
							reader.GetInt32(reader.GetOrdinal("sales_count"))
						);
					}
				}
			}

			return product;
		}

		public List<Product> GetProductsFromDB()
		{
			List<Product> products = new List<Product>();

			string query = "SELECT * FROM product";

			using (SqlCommand cmd = new SqlCommand(query, connection))
			{
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Product product = new Product
						(
							reader.GetInt32(reader.GetOrdinal("id")),
							reader.GetString(reader.GetOrdinal("name")),
							reader.GetString(reader.GetOrdinal("description")),
							(CategoryEnum)reader.GetInt32(reader.GetOrdinal("category")) - 1,
							reader.GetDecimal(reader.GetOrdinal("price")),
							reader.GetInt32(reader.GetOrdinal("quantity")),
							new List<string> { "", "", "" },
							reader.GetInt32(reader.GetOrdinal("sales_count"))
						);
						products.Add(product);
					}
				}
			}

			return products;
		}
	}
}
