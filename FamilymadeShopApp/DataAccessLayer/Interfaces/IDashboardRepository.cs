using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IDashboardRepository
    {
        Task<(int, int)> GetEntityStatisticsAsyncDAL();
        Task<List<KeyValuePair<string, int>>> GetTopProductsListAsyncDAL(DateTime startDate, DateTime endDate);
        Task<List<KeyValuePair<DateTime, decimal>>> GetGrossRevenueAsyncDAL(DateTime startDate, DateTime endDate);
        Task<(int, decimal)> GetOrderStatisticsAsyncDAL(DateTime startDate, DateTime endDate);
        Task<List<Product>> GetUnderStockProductsAsyncDAL();
    }
}
