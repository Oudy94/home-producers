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
            finally
            {
                CloseConnection();
            }
        }

        public void AddProductToCartDAL(int productId, int quantity, int customerId)
        {
            try
            {
                OpenConnection();

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
            finally
            {
                CloseConnection();
            }
        }
    }
}