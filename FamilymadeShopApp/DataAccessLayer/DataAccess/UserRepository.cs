using DataAccessLayer.Interfaces;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

        public List<Customer> GetAllUsersFromDB()
        {
            try
            {
                OpenConnection();

                List<Customer> users = new List<Customer>();

                string query = "SELECT id, name, email, registration_date FROM [user];";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer user = new Customer
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                RegistrationDate = reader.GetDateTime(reader.GetOrdinal("registration_date")),
                            };

                            users.Add(user);
                        }
                    }
                }

                return users;
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
            try
            {
                OpenConnection();

                User user = AuthenticateUserFromDB(email, password);
                if (user != null && user is Customer)
                {
                    return new Customer
                    {
                        Id = user.Id,
                        Name = user.Name
                    };
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
            return null;
        }

        private User AuthenticateUserFromDB(string email, string password)
        {
            User user = null;

            try
            {
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
                                user = new Admin
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    Name = reader.GetString(reader.GetOrdinal("name")),
                                };
                            }
                            else
                            {
                                user = new Customer
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

            return user;
        }
    }
}
