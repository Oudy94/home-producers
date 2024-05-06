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
                cmd.Parameters.AddWithValue("@RegistrationDate", user.RegistrationDate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@ShippingAddress", user.ShippingAddress);
                cmd.Parameters.AddWithValue("@Role", DBNull.Value);
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

        public List<Product> GetProductsFromDB(string searchTerm = null, int pageNumber = 1)
        {
            List<Product> products = new List<Product>();
            int pageSize = 2;

            int offset = (pageNumber - 1) * pageSize;

            string query = "SELECT * FROM product";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += " WHERE name LIKE @searchTerm";
            }

            query += " ORDER BY id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                }

                cmd.Parameters.AddWithValue("@offset", offset);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);

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

        public int GetProductsCountFromDB(string searchTerm)
        {
            int count = 0;

            string query = "SELECT COUNT(*) FROM product";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += " WHERE name LIKE @searchTerm";
            }

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                }

                count = (int)cmd.ExecuteScalar();
            }

            return count;
        }

        public Customer AuthenticateCustomerFromDB(string email, string password)
        {
            try
            {
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
            return null;
        }

        public Admin AuthenticateAdminFromDB(string email, string password)
        {
            try
            {
                User user = AuthenticateUserFromDB(email, password);
                if (user != null && user is Admin)
                {
                    return new Admin
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

        public void AddCartToDB(Cart cart)
        {
            try
            {
                string deleteQuery = "DELETE FROM [cart] WHERE customer_id = @CustomerId;";
                using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection))
                {
                    deleteCmd.Parameters.AddWithValue("@CustomerId", cart.Customer.Id);
                    deleteCmd.ExecuteNonQuery();
                }

                foreach (var cartProduct in cart.CartProducts)
                {
                    int productId = cartProduct.ProductId;
                    int quantity = cartProduct.Quantity;
                    int customerId = cart.Customer.Id;

                    string insertQuery = "INSERT INTO [cart] (product_id, quantity, customer_id) VALUES (@ProductId, @Quantity, @CustomerId);";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@ProductId", productId);
                        insertCmd.Parameters.AddWithValue("@Quantity", quantity);
                        insertCmd.Parameters.AddWithValue("@CustomerId", customerId);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void AddProductToCartInDB(int productId, int quantity, int customerId)
        {
            try
            {
                // Check if the product already exists in the cart
                string queryCheck = "SELECT COUNT(*) FROM [cart] WHERE product_id = @ProductId AND customer_id = @CustomerId;";
                int existingCount = 0;
                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, connection))
                {
                    cmdCheck.Parameters.AddWithValue("@ProductId", productId);
                    cmdCheck.Parameters.AddWithValue("@CustomerId", customerId);
                    existingCount = (int)cmdCheck.ExecuteScalar();
                }

                if (existingCount > 0)
                {
                    // Product already exists, update the quantity
                    string queryUpdate = "UPDATE [cart] SET quantity = quantity + @Quantity WHERE product_id = @ProductId AND customer_id = @CustomerId;";
                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, connection))
                    {
                        cmdUpdate.Parameters.AddWithValue("@ProductId", productId);
                        cmdUpdate.Parameters.AddWithValue("@Quantity", quantity);
                        cmdUpdate.Parameters.AddWithValue("@CustomerId", customerId);
                        cmdUpdate.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Product does not exist, insert a new row
                    string queryInsert = "INSERT INTO [cart] (product_id, quantity, customer_id) VALUES (@ProductId, @Quantity, @CustomerId);";
                    using (SqlCommand cmdInsert = new SqlCommand(queryInsert, connection))
                    {
                        cmdInsert.Parameters.AddWithValue("@ProductId", productId);
                        cmdInsert.Parameters.AddWithValue("@Quantity", quantity);
                        cmdInsert.Parameters.AddWithValue("@CustomerId", customerId);
                        cmdInsert.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void AddOrderToDB(Order order)
        {
            try
            {
                string query = "INSERT INTO [order] (customer_id, status, date, shipping_price, shipping_address) VALUES (@CustomerId, @Status, @Date, @ShippingPrice, @ShippingAddress); SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                    cmd.Parameters.AddWithValue("@Status", (int)order.Status);
                    cmd.Parameters.AddWithValue("@Date", order.Date);
                    cmd.Parameters.AddWithValue("@ShippingPrice", order.ShippingPrice);
                    cmd.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress);

                    int orderId = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (OrderProduct orderProduct in order.Products)
                    {
                        string queryProduct = "INSERT INTO [order_product] (order_id, product_id, quantity, price) VALUES (@OrderId, @ProductId, @Quantity, @Price)";

                        using (SqlCommand cmdProduct = new SqlCommand(queryProduct, connection))
                        {
                            cmdProduct.Parameters.AddWithValue("@OrderId", orderId);
                            cmdProduct.Parameters.AddWithValue("@ProductId", orderProduct.Product.Id);
                            cmdProduct.Parameters.AddWithValue("@Quantity", orderProduct.Quantity);
                            cmdProduct.Parameters.AddWithValue("@Price", orderProduct.Price);
                            cmdProduct.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public Customer GetUserFromDB(int id)
        {
            Customer user = null;

            try
            {
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return user;
        }

        public List<Order> GetOrdersByUserIdFromDB(int id)
        {
            List<Order> orders = new List<Order>();

            try
            {
                string query = "SELECT o.id, o.Status, o.date, o.shipping_price, o.shipping_address, op.product_id , op.quantity, op.price, p.name " +
                               "FROM [Order] o " +
                               "INNER JOIN order_product op ON o.id = op.order_id " +
                               "INNER JOIN Product p ON op.product_id = p.id " +
                               "WHERE o.customer_id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int orderId = reader.GetInt32(reader.GetOrdinal("id"));
                            Order order = orders.FirstOrDefault(o => o.Id == orderId);
                            if (order == null)
                            {
                                order = new Order
                                (
                                    orderId,
                                    id,
                                    (OrderStatusEnum)reader.GetInt32(reader.GetOrdinal("status")),
                                    reader.GetDateTime(reader.GetOrdinal("date")),
                                    new List<OrderProduct>(),
                                    reader.GetDecimal(reader.GetOrdinal("shipping_price")),
                                    reader.GetString(reader.GetOrdinal("shipping_address"))
                                );

                                orders.Add(order);
                            }

                            order.Products.Add(new OrderProduct
                            (
                                new Product
                                {
                                    Name = reader.GetString(reader.GetOrdinal("name")),
                                },
                                reader.GetInt32(reader.GetOrdinal("quantity")),
                                reader.GetDecimal(reader.GetOrdinal("price"))
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return orders;

        }
    }
}
