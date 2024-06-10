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
    public class CartRepository : DatabaseHelper, ICartRepository
    {
        public CartRepository() : base() { }

        public void AddCartDAL(Cart cart)
        {
            try
            {
                OpenConnection();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string deleteQuery = "DELETE FROM [cart] WHERE customer_id = @CustomerId;";
                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection, transaction))
                        {
                            deleteCmd.Parameters.AddWithValue("@CustomerId", cart.Customer.Id);
                            deleteCmd.ExecuteNonQuery();
                        }

                        string insertQuery = "INSERT INTO [cart] (product_id, quantity, customer_id) VALUES (@ProductId, @Quantity, @CustomerId);";
                        foreach (var cartProduct in cart.CartProducts)
                        {
                            int productId = cartProduct.ProductId;
                            int quantity = cartProduct.Quantity;
                            int customerId = cart.Customer.Id;

                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection, transaction))
                            {
                                insertCmd.Parameters.AddWithValue("@ProductId", productId);
                                insertCmd.Parameters.AddWithValue("@Quantity", quantity);
                                insertCmd.Parameters.AddWithValue("@CustomerId", customerId);
                                insertCmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("An error occurred while processing the cart. Transaction rolled back.", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the cart.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void AddProductToCartDAL(int customerId, int productId, int quantity)
        {
            try
            {
                OpenConnection();

                string query = @"
                    MERGE INTO [cart] AS target
                    USING (SELECT @ProductId AS ProductId, @CustomerId AS CustomerId) AS source
                    ON (target.product_id = source.ProductId AND target.customer_id = source.CustomerId)
                    WHEN MATCHED THEN
                        UPDATE SET quantity = target.quantity + @Quantity
                    WHEN NOT MATCHED THEN
                        INSERT (product_id, quantity, customer_id) 
                        VALUES (@ProductId, @Quantity, @CustomerId);";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
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
        
        public void RemoveCartByCustomerIdDAL(int customerId)
        {
            try
            {
                OpenConnection();

                string query = "DELETE FROM[cart] WHERE customer_id = @CustomerId; ";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
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

        public Cart GetCartByCustomerIdDAL(int customerId)
        {
            Cart cart = null;

            try
            {
                OpenConnection();

                string query = @"
                    SELECT c.id, c.customer_id, c.product_id, c.quantity, p.name AS product_name, p.price
                    FROM cart c
                    JOIN product p ON c.product_id = p.id
                    WHERE c.customer_id = @CustomerId;";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cart = new Cart
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Customer = new Customer { Id = customerId },
                                CartProducts = new List<CartProduct>()
                            };

                            do
                            {
                                CartProduct cartProduct = new CartProduct
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("product_id")),
                                    ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                                    Name = reader.GetString(reader.GetOrdinal("product_name")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("price"))
                                };
                                cart.CartProducts.Add(cartProduct);
                            } while (reader.Read());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the customer's cart.", ex);
            }
            finally
            {
                CloseConnection();
            }

            return cart;
        }

        public void UpdateProductQuantityInCartDAL(int customerId, int productId, int newQuantity)
        {
            try
            {
                OpenConnection();

                string updateQuery = @"
                    UPDATE cart
                    SET quantity = @NewQuantity
                    WHERE customer_id = @CustomerId AND product_id = @ProductId;";

                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@NewQuantity", newQuantity);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("The product does not exist in the customer's cart.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while changing the product quantity in the cart.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

    }
}