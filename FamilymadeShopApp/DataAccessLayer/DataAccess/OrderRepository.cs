using DataAccessLayer.Interfaces;
using ModelLayer.Models;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccess
{
    public class OrderRepository : DatabaseHelper, IOrderRepository
    {
        public OrderRepository() : base() { }

        public void AddOrderToDB(Order order)
        {
            try
            {
                OpenConnection();

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
            finally
            {
                CloseConnection();
            }
        }

        public List<Order> GetOrdersByUserIdFromDB(int id)
        {
            List<Order> orders = new List<Order>();

            try
            {
                OpenConnection();

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
                                    (OrderStatus)reader.GetInt32(reader.GetOrdinal("status")),
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
            finally
            {
                CloseConnection();
            }

            return orders;
        }
    }
}