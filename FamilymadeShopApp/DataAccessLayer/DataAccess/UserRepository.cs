using DataAccessLayer.Interfaces;
using ModelLayer.Models;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccess
{
    public class UserRepository : DatabaseHelper, IUserRepository
    {
        public UserRepository() : base() { }

        public void AddUserToDB(Customer user)
        {
            try
            {
                OpenConnection();

                string query = "INSERT INTO [user] (name, email, password, registration_date, shipping_address, role) VALUES (@Name, @Email, @Password, @RegistrationDate, @ShippingAddress, @Role)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@RegistrationDate", user.RegistrationDate.ToUniversalTime());
                    cmd.Parameters.AddWithValue("@ShippingAddress", user.ShippingAddress);
                    cmd.Parameters.AddWithValue("@Role", DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public Customer GetUserFromDB(int id)
        {
            try
            {
                OpenConnection();

                Customer user = null;

                string query = "SELECT name, email, registration_date FROM [user] WHERE id = @Id;";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new Customer
                            {
                                Id = id,
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                RegistrationDate = reader.GetDateTime(reader.GetOrdinal("registration_date")),
                            };
                        }
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                CloseConnection();
            }
        }

		public async Task<List<Customer>> GetCustomersFromDBAsync()
		{
			try
			{
				OpenConnection();

				List<Customer> customers = new List<Customer>();

				string query = "SELECT id, name, email, registration_date, shipping_address, role FROM [user];";
				using (SqlCommand cmd = new SqlCommand(query, connection))
				{
					using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							if (reader.IsDBNull(reader.GetOrdinal("role")))
							{
								Customer customer = new Customer
								{
									Id = reader.GetInt32(reader.GetOrdinal("id")),
									Name = reader.GetString(reader.GetOrdinal("name")),
									Email = reader.GetString(reader.GetOrdinal("email")),
									RegistrationDate = reader.GetDateTime(reader.GetOrdinal("registration_date")),
									ShippingAddress = reader.GetString(reader.GetOrdinal("shipping_address")),
								};
								customers.Add(customer);
							}
						}
					}
				}

				return customers;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
			finally
			{
				CloseConnection();
			}
		}

		public async Task<List<Admin>> GetAdminsFromDBAsync()
		{
			try
			{
				OpenConnection();

				List<Admin> admins = new List<Admin>();

				string query = "SELECT id, name, email, registration_date, role FROM [user];";
				using (SqlCommand cmd = new SqlCommand(query, connection))
				{
					using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							if (!reader.IsDBNull(reader.GetOrdinal("role")))
							{
								Admin admin = new Admin
								{
									Id = reader.GetInt32(reader.GetOrdinal("id")),
									Name = reader.GetString(reader.GetOrdinal("name")),
									Email = reader.GetString(reader.GetOrdinal("email")),
									RegistrationDate = reader.GetDateTime(reader.GetOrdinal("registration_date")),
									Role = (Role)reader.GetInt32(reader.GetOrdinal("role")),
								};
								admins.Add(admin);
							}
						}
					}
				}

				return admins;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
			finally
			{
				CloseConnection();
			}
		}

		public Customer AuthenticateCustomerFromDB(string email, string password)
        {
			Customer customer = null;

			try
            {
                OpenConnection();

				string query = "SELECT id, name, role FROM [user] WHERE email = @Email AND password = @Password;";
				using (SqlCommand cmd = new SqlCommand(query, connection))
				{
					cmd.Parameters.AddWithValue("@Email", email);
					cmd.Parameters.AddWithValue("@Password", password);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							if (reader.IsDBNull(reader.GetOrdinal("role")))
							{
								customer = new Customer
								{
									Id = reader.GetInt32(reader.GetOrdinal("id")),
									Name = reader.GetString(reader.GetOrdinal("name")),
								};
							}
						}
					}
				}
			}
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                CloseConnection();
            }

            return customer;
        }

		public Admin AuthenticateAdminFromDB(string email, string password)
		{
			Admin admin = null;

			try
			{
				OpenConnection();

				string query = "SELECT id, name, role FROM [user] WHERE email = @Email AND password = @Password;";
				using (SqlCommand cmd = new SqlCommand(query, connection))
				{
					cmd.Parameters.AddWithValue("@Email", email);
					cmd.Parameters.AddWithValue("@Password", password);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							if (!reader.IsDBNull(reader.GetOrdinal("role")))
							{
								admin = new Admin
								{
									Id = reader.GetInt32(reader.GetOrdinal("id")),
									Name = reader.GetString(reader.GetOrdinal("name")),
									Role = (Role)reader.GetInt32(reader.GetOrdinal("role")),
								};
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
			finally
			{
				CloseConnection();
			}

			return admin;
		}

		public List<Customer> GetCustomersFromDB()
		{
			throw new NotImplementedException();
		}
	}
}
