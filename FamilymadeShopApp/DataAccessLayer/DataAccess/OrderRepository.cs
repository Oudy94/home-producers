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

        public void AddOrderDAL(Order order)
        {
            try
            {
                OpenConnection();

                string query = "INSERT INTO [order] (customer_id, status, date, shipping_price, shipping_address, payment_method) VALUES (@CustomerId, @Status, @Date, @ShippingPrice, @ShippingAddress, @PaymentMethod); SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                    cmd.Parameters.AddWithValue("@Status", (int)order.Status);
                    cmd.Parameters.AddWithValue("@Date", order.Date);
                    cmd.Parameters.AddWithValue("@ShippingPrice", order.ShippingPrice);
                    cmd.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress.ToString());
                    cmd.Parameters.AddWithValue("@PaymentMethod", order.PaymentMethod);

                    int orderId = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (OrderProduct orderProduct in order.Products)
                    {
                        string queryProduct = "INSERT INTO [order_product] (order_id, product_id, quantity, product_price) VALUES (@OrderId, @ProductId, @Quantity, @ProductPrice)";

                        using (SqlCommand cmdProduct = new SqlCommand(queryProduct, connection))
                        {
                            cmdProduct.Parameters.AddWithValue("@OrderId", orderId);
                            cmdProduct.Parameters.AddWithValue("@ProductId", orderProduct.Product.Id);
                            cmdProduct.Parameters.AddWithValue("@Quantity", orderProduct.Quantity);
                            cmdProduct.Parameters.AddWithValue("@ProductPrice", orderProduct.Price);
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

        public List<Order> GetOrdersByUserIdDAL(int customerId)
        {
            List<Order> orders = new List<Order>();

            try
            {
                OpenConnection();

                string query = "SELECT o.id, o.Status, o.date, o.shipping_price, o.shipping_address, o.payment_method, o.transaction_fee, op.product_id, op.quantity, op.product_price, p.name " +
                               "FROM [Order] o " +
                               "INNER JOIN order_product op ON o.id = op.order_id " +
                               "INNER JOIN Product p ON op.product_id = p.id " +
                               "WHERE o.customer_id = @CustomerId";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

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
									customerId,
                                    (OrderStatus)reader.GetInt32(reader.GetOrdinal("status")),
                                    reader.GetDateTime(reader.GetOrdinal("date")),
                                    new List<OrderProduct>(),
                                    reader.GetDecimal(reader.GetOrdinal("shipping_price")),
                                    Address.Parse(reader.GetString(reader.GetOrdinal("shipping_address"))),
                                    reader.GetString(reader.GetOrdinal("payment_method")),
                                    reader.GetDecimal(reader.GetOrdinal("transaction_fee"))
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
                                reader.GetDecimal(reader.GetOrdinal("product_price"))
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

		public async Task<int> GetOrdersCountAsyncDAL(string filterName, OrderStatus? filterStatus)
		{
			try
			{
				OpenConnection();

				string query = "SELECT COUNT(*) FROM [order] WHERE 1=1";

				if (!string.IsNullOrEmpty(filterName))
				{
					query += " AND name LIKE @FilterName";
				}

				if (filterStatus != null)
				{
					query += " AND status = @FilterStatus";
				}

				using (SqlCommand cmd = new SqlCommand(query, connection))
				{
					if (!string.IsNullOrEmpty(filterName))
					{
						cmd.Parameters.AddWithValue("@FilterName", "%" + filterName + "%");
					}

					if (filterStatus != null)
					{
						cmd.Parameters.AddWithValue("@FilterStatus", (int)filterStatus);
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

		public async Task<List<Order>> GetOrdersAsyncDAL(int pageNumber, int pageSize, string filterName, OrderStatus? filterStatus)
		{
			try
			{
				OpenConnection();

				List<Order> orders = new List<Order>();
				int offset = (pageNumber - 1) * pageSize;
				string query = "SELECT id, customer_id, status, date, shipping_price, shipping_address, payment_method, transaction_fee FROM [order] WHERE 1=1";

				if (!string.IsNullOrWhiteSpace(filterName))
				{
					query += " AND name LIKE @FilterName";
				}

				if (filterStatus != null)
				{
					query += " AND status = @FilterStatus";
				}

				query += " ORDER BY id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

				using (SqlCommand cmd = new SqlCommand(query, connection))
				{
					if (!string.IsNullOrWhiteSpace(filterName))
					{
						cmd.Parameters.AddWithValue("@FilterName", "%" + filterName + "%");
					}

					if (filterStatus != null)
					{
						cmd.Parameters.AddWithValue("@FilterStatus", filterStatus);
					}

					cmd.Parameters.AddWithValue("@Offset", offset);
					cmd.Parameters.AddWithValue("@PageSize", pageSize);

					using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							Order order = new Order
							(
								reader.GetInt32(reader.GetOrdinal("id")),
								reader.GetInt32(reader.GetOrdinal("customer_id")),
								(OrderStatus)reader.GetInt32(reader.GetOrdinal("status")),
								reader.GetDateTime(reader.GetOrdinal("date")),
								new List<OrderProduct>(),
								reader.GetDecimal(reader.GetOrdinal("shipping_price")),
                                Address.Parse(reader.GetString(reader.GetOrdinal("shipping_address"))),
								reader.GetString(reader.GetOrdinal("payment_method")),
								reader.GetDecimal(reader.GetOrdinal("transaction_fee"))
							);
							orders.Add(order);
						}
					}
				}

				return orders;
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

		public async Task<bool> UpdateOrdersDAL(List<Order> orders)
		{
			if (orders == null || orders.Count == 0)
			{
				return false;
			}

			SqlTransaction? transaction = null;

			try
			{
				OpenConnection();

				transaction = connection.BeginTransaction();

				foreach (Order order in orders)
				{
					string updateQuery = "UPDATE [order] SET customer_id = @CustomerId, status = @Status, shipping_price = @ShippingPrice, shipping_address = @ShippingAddress WHERE id = @Id";

					using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
					{
						command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
						command.Parameters.AddWithValue("@Status", (int)order.Status);
						command.Parameters.AddWithValue("@ShippingPrice", order.ShippingPrice);
						command.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress.ToString());
						command.Parameters.AddWithValue("@Id", order.Id);

						await command.ExecuteNonQueryAsync();
					}
				}

				transaction.Commit();

				return true;
			}
			catch (Exception ex)
			{
				transaction?.Rollback();
				throw new Exception("Error updating order data.", ex);
			}
			finally
			{
				CloseConnection();
			}
		}

        public async Task<Order> GetOrderByIdAsyncDAL(int orderId)
        {
            Order order = null;

            try
            {
                OpenConnection();

                string query = "SELECT o.id, o.customer_id, o.Status, o.date, o.shipping_price, o.shipping_address, o.payment_method, o.transaction_fee, " +
                               "op.product_id, op.quantity, op.product_price, p.name " +
                               "FROM [Order] o " +
                               "INNER JOIN order_product op ON o.id = op.order_id " +
                               "INNER JOIN Product p ON op.product_id = p.id " +
                               "WHERE o.id = @OrderId";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            if (order == null)
                            {
                                order = new Order
                                (
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetInt32(reader.GetOrdinal("customer_id")),
                                    (OrderStatus)reader.GetInt32(reader.GetOrdinal("status")),
                                    reader.GetDateTime(reader.GetOrdinal("date")),
                                    new List<OrderProduct>(),
                                    reader.GetDecimal(reader.GetOrdinal("shipping_price")),
                                    Address.Parse(reader.GetString(reader.GetOrdinal("shipping_address"))),
                                    reader.GetString(reader.GetOrdinal("payment_method")),
                                    reader.GetDecimal(reader.GetOrdinal("transaction_fee"))
                                );
                            }

                            order.Products.Add(new OrderProduct
                            (
                                new Product
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("product_id")),
									Name = reader.GetString(reader.GetOrdinal("name")),
                                },
                                reader.GetInt32(reader.GetOrdinal("quantity")),
                                reader.GetDecimal(reader.GetOrdinal("product_price"))
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

            return order;
        }

	}
}