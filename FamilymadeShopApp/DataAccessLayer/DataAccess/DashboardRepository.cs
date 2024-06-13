using DataAccessLayer.Interfaces;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccess
{
    public class DashboardRepository : DatabaseHelper, IDashboardRepository
    {
        public DashboardRepository() : base() { }

        public async Task<(int, int)> GetEntityStatisticsAsyncDAL()
        {
            int numberOfCustomers = 0;
            int numberOfProducts = 0;

            try
            {
                OpenConnection();

                string query = @"
                    SELECT 
                        (SELECT COUNT(id) FROM [user] WHERE role IS NULL) AS NumCustomers,
                        (SELECT COUNT(id) FROM product) AS NumProducts";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            numberOfCustomers = reader.GetInt32(0);
                            numberOfProducts = reader.GetInt32(1);
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

            return (numberOfCustomers, numberOfProducts);
        }

        public async Task<List<KeyValuePair<string, int>>> GetTopProductsListAsyncDAL(DateTime startDate, DateTime endDate)
        {
            var topProductsList = new List<KeyValuePair<string, int>>();
            try
            {
                OpenConnection();

                string query = @"
                    SELECT TOP 5 p.name, SUM(order_product.quantity) AS q
                    FROM order_product
                    INNER JOIN product p ON p.id = order_product.product_id
                    INNER JOIN [order] o ON o.id = order_product.order_id
                    WHERE o.date BETWEEN @StartDate AND @EndDate
                    GROUP BY p.name
                    ORDER BY q DESC;";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            topProductsList.Add(new KeyValuePair<string, int>(reader.GetString(0), reader.GetInt32(1)));
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

            return topProductsList;
        }

        public async Task<List<KeyValuePair<DateTime, decimal>>> GetGrossRevenueAsyncDAL(DateTime startDate, DateTime endDate)
        {
            var revenueData = new List<KeyValuePair<DateTime, decimal>>();

            try
            {
                OpenConnection();

                string query = @"
                SELECT CONVERT(date, o.date) AS date,
                       SUM(op.product_price * op.quantity) AS total_amount
                FROM [order] AS o
                JOIN order_product AS op ON o.id = op.order_id
                WHERE o.date BETWEEN @StartDate AND @EndDate
                GROUP BY CONVERT(date, o.date)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            revenueData.Add(new KeyValuePair<DateTime, decimal>(reader.GetDateTime(0), reader.GetDecimal(1)));
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

            return revenueData;
        }

        public async Task<(int, decimal)> GetOrderStatisticsAsyncDAL(DateTime startDate, DateTime endDate)
        {
            int numberOfOrders = 0;
            decimal totalAmount = 0;

            try
            {
                OpenConnection();

                string query = @"
                    SELECT 
                        COUNT(DISTINCT o.id) AS number_of_orders, 
                        ISNULL(SUM(op.product_price * op.quantity), 0) AS total_amount
                    FROM [order] AS o
                    LEFT JOIN order_product AS op ON o.id = op.order_id
                    WHERE o.date BETWEEN @StartDate AND @EndDate";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            numberOfOrders = reader.GetInt32(0);
                            totalAmount = reader.GetDecimal(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching order statistics: " + ex.Message, ex);
            }
            finally
            {
                CloseConnection();
            }

            return (numberOfOrders, totalAmount);
        }

        public async Task<List<Product>> GetUnderStockProductsAsyncDAL()
        {
            List<Product> products = new List<Product>();

            try
            {
                OpenConnection();

                string query = "SELECT * FROM product WHERE quantity <= 5;";



                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            Product product = new Product
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                Stock = reader.GetInt32(reader.GetOrdinal("quantity"))
                            };
                            products.Add(product);
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

            return products;
        }
    }
}
