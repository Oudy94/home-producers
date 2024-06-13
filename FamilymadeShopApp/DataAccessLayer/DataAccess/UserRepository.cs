using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using ModelLayer.Models;
using SharedLayer.Enums;
using SharedLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccessLayer.DataAccess
{
	public class UserRepository : DatabaseHelper, IUserRepository
	{
		public UserRepository() : base() { }

		public void AddCustomerDAL(Customer user)
		{
			try
			{
				OpenConnection();

				if (IsEmailAlreadyExists(user.Email))
				{
					throw new ExistingEmailException("Email already exists. Please choose a different email.");
				}

				string query = @"
				INSERT INTO [user] (name, email, password, registration_date, role) 
				VALUES (@Name, @Email, @Password, @RegistrationDate, @Role)";

				using (SqlCommand cmd = new SqlCommand(query, connection))
				{
					cmd.Parameters.AddWithValue("@Name", user.Name);
					cmd.Parameters.AddWithValue("@Email", user.Email);
					cmd.Parameters.AddWithValue("@Password", user.Password);
					cmd.Parameters.AddWithValue("@RegistrationDate", user.RegistrationDate.ToUniversalTime());
					cmd.Parameters.AddWithValue("@Role", DBNull.Value);

					cmd.ExecuteNonQuery();
				}
			}
			catch (ExistingEmailException ex)
			{
				throw new ExistingEmailException(ex.Message, ex);
			}
			catch (Exception ex)
			{
				throw new Exception("Error adding customer.", ex);
			}
			finally
			{
				CloseConnection();
			}
		}

		private bool IsEmailAlreadyExists(string email)
		{
			string query = "SELECT COUNT(*) FROM [user] WHERE email = @Email";

			using (SqlCommand cmd = new SqlCommand(query, connection))
			{
				cmd.Parameters.AddWithValue("@Email", email);
				int count = (int)cmd.ExecuteScalar();
				return count > 0;
			}
		}

		public void AddAdminDAL(Admin admin)
		{
			try
			{
				OpenConnection();

				string query = "INSERT INTO [user] (name, email, password, registration_date, role) VALUES (@Name, @Email, @Password, @RegistrationDate, @Role)";
				using (SqlCommand cmd = new SqlCommand(query, connection))
				{
					cmd.Parameters.AddWithValue("@Name", admin.Name);
					cmd.Parameters.AddWithValue("@Email", admin.Email);
					cmd.Parameters.AddWithValue("@Password", admin.Password);
					cmd.Parameters.AddWithValue("@RegistrationDate", admin.RegistrationDate.ToUniversalTime());
					cmd.Parameters.AddWithValue("@Role", admin.Role);
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

		public Customer GetCustomerByIdDAL(int id)
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
								RegistrationDate = reader.GetDateTime(reader.GetOrdinal("registration_date"))
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

		public async Task<List<Customer>> GetAllCustomersAsyncDAL(string filterName, int pageNumber, int pageSize)
		{
			try
			{
				OpenConnection();

				List<Customer> customers = new List<Customer>();
				int offset = (pageNumber - 1) * pageSize;

				string query = "SELECT id, name, email, registration_date, role FROM [user] WHERE role IS NULL";

				if (!string.IsNullOrEmpty(filterName))
				{
					query += " AND name LIKE @FilterName";
				}

				query += " ORDER BY id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

				using (SqlCommand cmd = new SqlCommand(query, connection))
				{
					if (!string.IsNullOrEmpty(filterName))
					{
						cmd.Parameters.AddWithValue("@FilterName", "%" + filterName + "%");
					}
					cmd.Parameters.AddWithValue("@Offset", offset);
					cmd.Parameters.AddWithValue("@PageSize", pageSize);

					using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							Customer customer = new Customer
							{
								Id = reader.GetInt32(reader.GetOrdinal("id")),
								Name = reader.GetString(reader.GetOrdinal("name")),
								Email = reader.GetString(reader.GetOrdinal("email")),
								RegistrationDate = reader.GetDateTime(reader.GetOrdinal("registration_date"))
							};
							customers.Add(customer);
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

		public async Task<List<Admin>> GetAllAdminsAsyncDAL(string filterName, int pageNumber, int pageSize)
		{
			try
			{
				OpenConnection();

				List<Admin> admins = new List<Admin>();
				int offset = (pageNumber - 1) * pageSize;

				string query = "SELECT id, name, email, registration_date, role FROM [user] WHERE role IS NOT NULL";

				if (!string.IsNullOrEmpty(filterName))
				{
					query += " AND name LIKE @FilterName";
				}

				query += " ORDER BY id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

				using (SqlCommand cmd = new SqlCommand(query, connection))
				{
					if (!string.IsNullOrEmpty(filterName))
					{
						cmd.Parameters.AddWithValue("@FilterName", "%" + filterName + "%");
					}
					cmd.Parameters.AddWithValue("@Offset", offset);
					cmd.Parameters.AddWithValue("@PageSize", pageSize);

					using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
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

		public async Task<int> GetAdminCountAsyncDAL(string filterName)
		{
			try
			{
				OpenConnection();

				string query = "SELECT COUNT(*) FROM [user] WHERE role IS NOT NULL";

				if (!string.IsNullOrEmpty(filterName))
				{
					query += " AND name LIKE @FilterName";
				}

				using (SqlCommand cmd = new SqlCommand(query, connection))
				{
					if (!string.IsNullOrEmpty(filterName))
					{
						cmd.Parameters.AddWithValue("@FilterName", "%" + filterName + "%");
					}

					return (int)await cmd.ExecuteScalarAsync();
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

		public async Task<int> GetCustomerCountAsyncDAL(string filterName)
		{
			try
			{
				OpenConnection();

				string query = "SELECT COUNT(*) FROM [user] WHERE role IS NULL";

				if (!string.IsNullOrEmpty(filterName))
				{
					query += " AND name LIKE @FilterName";
				}

				using (SqlCommand cmd = new SqlCommand(query, connection))
				{
					if (!string.IsNullOrEmpty(filterName))
					{
						cmd.Parameters.AddWithValue("@FilterName", "%" + filterName + "%");
					}

					return (int)await cmd.ExecuteScalarAsync();
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

		public Customer GetCustomerByCredentialsDAL(string email, string password)
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

		public Admin GetAdminByCredentialsDAL(string email, string password)
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

		public async Task<bool> UpdateAdminsAsyncDAL(List<Admin> admins)
		{
			if (admins == null || admins.Count == 0)
			{
				return false;
			}

			SqlTransaction transaction = null;

			try
			{
				OpenConnection();

				transaction = connection.BeginTransaction();

				foreach (Admin admin in admins)
				{
					string updateQuery = "UPDATE [user] SET name = @Name, email = @Email, role = @Role WHERE id = @Id";

					using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
					{
						command.Parameters.AddWithValue("@Name", admin.Name);
						command.Parameters.AddWithValue("@Email", admin.Email);
						command.Parameters.AddWithValue("@Role", admin.Role);
						command.Parameters.AddWithValue("@Id", admin.Id);

						await command.ExecuteNonQueryAsync();
					}
				}

				transaction.Commit();

				return true;
			}
			catch (Exception ex)
			{
				transaction?.Rollback();
				throw new Exception("Error updating admin data.", ex);
			}
			finally
			{
				CloseConnection();
			}
		}

		public async Task<bool> UpdateCustomersAsyncDAL(List<Customer> customers)
		{
			if (customers == null || customers.Count == 0)
			{
				return false;
			}

			SqlTransaction transaction = null;

			try
			{
				OpenConnection();

				transaction = connection.BeginTransaction();

				foreach (Customer customer in customers)
				{
					string updateQuery = "UPDATE [user] SET name = @Name, email = @Email  WHERE id = @Id";

					using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
					{
						command.Parameters.AddWithValue("@Name", customer.Name);
						command.Parameters.AddWithValue("@Email", customer.Email);
						command.Parameters.AddWithValue("@Id", customer.Id);

						await command.ExecuteNonQueryAsync();
					}
				}

				transaction.Commit();

				return true;
			}
			catch (Exception ex)
			{
				transaction?.Rollback();
				throw new Exception("Error updating customer data.", ex);
			}
			finally
			{
				CloseConnection();
			}
		}

		public async Task RemoveUserByIdAsyncDAL(int id)
		{
			try
			{
				OpenConnection();

				string query = "DELETE FROM [user] WHERE Id = @Id";

				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Id", id);
					int rowsAffected = await command.ExecuteNonQueryAsync();

					if (rowsAffected <= 0)
					{
						throw new Exception("No user found with the specified ID.");
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error removing user", ex);
			}
			finally
			{
				CloseConnection();
			}
		}

		public async Task AddPersonalPictureAsyncDAL(int userId, IFormFile personalPic)
		{
			try
			{
				using (MemoryStream ms = new MemoryStream())
				{
					personalPic.CopyTo(ms);
					byte[] data = ms.ToArray();

					OpenConnection();

					// Check if the record exists for the user
					string checkSql = "SELECT COUNT(*) FROM [profile_picture] WHERE user_id = @UserID;";
					using (SqlCommand checkCmd = new SqlCommand(checkSql, connection))
					{
						checkCmd.Parameters.AddWithValue("@UserID", userId);
						int count = (int)checkCmd.ExecuteScalar();

						if (count > 0)
						{
							// Update the existing record
							string updateSql = "UPDATE [profile_picture] SET name = @Name, data = @Data, content_type = @ContentType WHERE user_id = @UserID;";
							using (SqlCommand updateCmd = new SqlCommand(updateSql, connection))
							{
								updateCmd.Parameters.AddWithValue("@UserID", userId);
								updateCmd.Parameters.AddWithValue("@Name", personalPic.FileName);
								updateCmd.Parameters.AddWithValue("@Data", data);
								updateCmd.Parameters.AddWithValue("@ContentType", personalPic.ContentType);
								await updateCmd.ExecuteNonQueryAsync();
							}
						}
						else
						{
							// Insert a new record
							string insertSql = "INSERT INTO [profile_picture] (user_id, name, data, content_type) VALUES (@UserID, @Name, @Data, @ContentType);";
							using (SqlCommand insertCmd = new SqlCommand(insertSql, connection))
							{
								insertCmd.Parameters.AddWithValue("@UserID", userId);
								insertCmd.Parameters.AddWithValue("@Name", personalPic.FileName);
								insertCmd.Parameters.AddWithValue("@Data", data);
								insertCmd.Parameters.AddWithValue("@ContentType", personalPic.ContentType);
								await insertCmd.ExecuteNonQueryAsync();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to update profile picture.", ex);
			}
		}

		public async Task<byte[]> GetPersonalPictureAsyncDAL(int userId)
		{
			try
			{
				OpenConnection();

				string selectSql = "SELECT name, data, content_type FROM [profile_picture] WHERE user_id = @UserID;";
				using (SqlCommand cmd = new SqlCommand(selectSql, connection))
				{
					cmd.Parameters.AddWithValue("@UserID", userId);

					using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
					{
						if (await reader.ReadAsync())
						{
							return (byte[])reader["data"];
						}
					}
				}

				return null;
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to retrieve profile picture.", ex);
			}
		}
	}
}