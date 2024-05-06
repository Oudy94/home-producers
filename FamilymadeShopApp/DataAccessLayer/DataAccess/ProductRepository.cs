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
    public class ProductRepository : DatabaseHelper, IProductRepository
    {
        public ProductRepository() : base() { }

        public Product GetProductByIdFromDB(int id)
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
                                (Category)reader.GetInt32(reader.GetOrdinal("category")) - 1,
                                reader.GetDecimal(reader.GetOrdinal("price")),
                                reader.GetInt32(reader.GetOrdinal("quantity")),
                                new List<string> { "", "", "" },
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

        public List<Product> GetProductsFromDB(string searchTerm = null, int pageNumber = 1)
        {
            List<Product> products = new List<Product>();
            int pageSize = 4;

            int offset = (pageNumber - 1) * pageSize;

            try
            {
                OpenConnection();

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
                                (Category)reader.GetInt32(reader.GetOrdinal("category")) - 1,
                                reader.GetDecimal(reader.GetOrdinal("price")),
                                reader.GetInt32(reader.GetOrdinal("quantity")),
                                new List<string> { "", "", "" },
                                reader.GetInt32(reader.GetOrdinal("sales_count"))
                            );
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

        public int GetProductsCountFromDB(string searchTerm)
        {
            int count = 0;

            try
            {
                OpenConnection();

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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                CloseConnection();
            }

            return count;
        }
    }
}
