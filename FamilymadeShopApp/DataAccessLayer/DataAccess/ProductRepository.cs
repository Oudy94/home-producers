using DataAccessLayer.Interfaces;
using ModelLayer.Models;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccess
{
    public class ProductRepository : DatabaseHelper, IProductRepository
    {
        public ProductRepository() : base() { }

        public async Task AddProductAsyncDAL(Product product)
        {
            try
            {
                OpenConnection();

                string query = "INSERT INTO [product] (name, description, category, price, quantity, image, sales_count) VALUES (@Name, @Description, @Category, @Price, @Quantity, @Image, @SalesCount)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Category", product.Category);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Quantity", product.Stock);
                    cmd.Parameters.AddWithValue("@Image", product.Image);
                    cmd.Parameters.AddWithValue("@SalesCount", product.SalesCount);
                    await cmd.ExecuteNonQueryAsync();
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

        public Product GetProductByIdDAL(int id)
        {
            Product product = null;

            try
            {
                OpenConnection();

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
                                (Category)reader.GetInt32(reader.GetOrdinal("category")),
                                reader.GetDecimal(reader.GetOrdinal("price")),
                                reader.GetInt32(reader.GetOrdinal("quantity")),
                                reader.GetString(reader.GetOrdinal("image")),
                                reader.GetInt32(reader.GetOrdinal("sales_count"))
                            );
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

            return product;
        }

        public async Task<int> GetProductsCountAsyncDAL(string filterName, Category? filterCategory)
        {
            try
            {
                OpenConnection();

                string query = "SELECT COUNT(*) FROM product WHERE 1=1";

                if (!string.IsNullOrEmpty(filterName))
                {
                    query += " AND name LIKE @FilterName";
                }

                if (filterCategory != null)
                {
					query += " AND category = @FilterCategory";
				}

				using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    if (!string.IsNullOrEmpty(filterName))
                    {
                        cmd.Parameters.AddWithValue("@FilterName", "%" + filterName + "%");
					}

					if (filterCategory != null)
					{
						cmd.Parameters.AddWithValue("@FilterCategory", (int)filterCategory);
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

        public async Task<List<Product>> GetAllProductsAsyncDAL(int pageNumber, int pageSize, string filterName, Category? filterCategory)
        {
            try
            {
				OpenConnection();

                List<Product> products = new List<Product>();
				int offset = (pageNumber - 1) * pageSize;
				string query = "SELECT id, name, description, category, price, quantity, image, sales_count FROM [product] WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(filterName))
                {
                    query += " AND name LIKE @FilterName";
                }

                if (filterCategory != null)
                {
					query += " AND category = @FilterCategory";
				}

                query += " ORDER BY id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
					if (!string.IsNullOrWhiteSpace(filterName))
                    {
                        cmd.Parameters.AddWithValue("@FilterName", "%" + filterName + "%");
					}

					if (filterCategory != null)
                    {
                        cmd.Parameters.AddWithValue("@FilterCategory", filterCategory);
                    }

                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
							Product product = new Product
							(
								reader.GetInt32(reader.GetOrdinal("id")),
								reader.GetString(reader.GetOrdinal("name")),
								reader.GetString(reader.GetOrdinal("description")),
								(Category)reader.GetInt32(reader.GetOrdinal("category")),
								reader.GetDecimal(reader.GetOrdinal("price")),
								reader.GetInt32(reader.GetOrdinal("quantity")),
                                reader.GetString(reader.GetOrdinal("image")),
                                reader.GetInt32(reader.GetOrdinal("sales_count"))
							);
							products.Add(product);
						}
                    }
				}

                return products;
			}
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                CloseConnection();
            }
		}

		public async Task<bool> UpdateProductsAsyncDAL(List<Product> products)
		{
			if (products == null || products.Count == 0)
			{
				return false;
			}

			SqlTransaction? transaction = null;

            try
            {
                OpenConnection();

                transaction = connection.BeginTransaction();

                foreach (Product product in products)
                {
                    string updateQuery = "UPDATE [product] SET name = @Name, description = @Description, category = @Category, price = @Price, quantity = @Quantity, image = @Image, sales_count = @SalesCount WHERE id = @Id";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Description", product.Description);
                        command.Parameters.AddWithValue("@Category", (int)product.Category);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@Quantity", product.Stock);
                        command.Parameters.AddWithValue("@Image", product.Image);
                        command.Parameters.AddWithValue("@SalesCount", product.SalesCount);
                        command.Parameters.AddWithValue("@Id", product.Id);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                CloseConnection();
            }
		}

        public async Task<List<string>> GetProductsNamesAsyncDAL()
        {
            try
            {
                OpenConnection();

                List<string> productsNames = new List<string>();
                string query = "SELECT name FROM [product]";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            productsNames.Add(reader.GetString(reader.GetOrdinal("name")));
                        }
                    }
                }

                return productsNames;
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

        public async Task RemoveProductByIdAsyncDAL(int id)
        {
            try
            {
                OpenConnection();

                // Delete from order_product table
                string deleteOrderProductQuery = "DELETE FROM order_product WHERE product_id = @ProductId;";
                using (SqlCommand orderProductCommand = new SqlCommand(deleteOrderProductQuery, connection))
                {
                    orderProductCommand.Parameters.AddWithValue("@ProductId", id);
                    await orderProductCommand.ExecuteNonQueryAsync();
                }

                // Delete from cart table
                string deleteCartQuery = "DELETE FROM cart WHERE product_id = @ProductId;";
                using (SqlCommand cartCommand = new SqlCommand(deleteCartQuery, connection))
                {
                    cartCommand.Parameters.AddWithValue("@ProductId", id);
                    await cartCommand.ExecuteNonQueryAsync();
                }

                // Delete from product table
                string deleteProductQuery = "DELETE FROM product WHERE Id = @Id;";
                using (SqlCommand productCommand = new SqlCommand(deleteProductQuery, connection))
                {
                    productCommand.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = await productCommand.ExecuteNonQueryAsync();

                    if (rowsAffected <= 0)
                    {
                        throw new Exception("No product found with the specified ID.");
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
        }
    }
}
