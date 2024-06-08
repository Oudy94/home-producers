using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IDashboardManager
    {
        Task<(int, int)> GetEntityStatisticsAsync();
        Task<(int, decimal)> GetOrderStatisticsAsync(DateTime startDate, DateTime endDate);
        Task<List<KeyValuePair<string, int>>> GetTopProductsListAsync(DateTime startDate, DateTime endDate);
        Task<List<KeyValuePair<DateTime, decimal>>> GetGrossRevenueAsync(DateTime startDate, DateTime endDate);
        Task<List<Product>> GetUnderStockProductsAsync();
    }
}
