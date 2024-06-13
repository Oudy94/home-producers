using BusinessLogicLayer.Interfaces;
using DataAccessLayer.DataAccess;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class DashboardManager: IDashboardManager
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly MemoryCache _cache;

        public DashboardManager(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public async Task<(int, int)> GetEntityStatisticsAsync()
        {
            if (_cache.TryGetValue("EntityStatistics", out (int, int) cachedData))
            {
                return cachedData;
            }

            try
            {
                var data = await _dashboardRepository.GetEntityStatisticsAsyncDAL();
                _cache.Set("EntityStatistics", data, TimeSpan.FromMinutes(10));
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<(int, decimal)> GetOrderStatisticsAsync(DateTime startDate, DateTime endDate)
        {
            string cacheKey = $"OrderStatistics_{startDate.ToString("yyyyMMdd")}_{endDate.ToString("yyyyMMdd")}";

            if (_cache.TryGetValue(cacheKey, out (int, decimal) cachedData))
            {
                return cachedData;
            }

            try
            {
                var data = await _dashboardRepository.GetOrderStatisticsAsyncDAL(startDate, endDate);
                _cache.Set(cacheKey, data, TimeSpan.FromMinutes(10));
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<KeyValuePair<string, int>>> GetTopProductsListAsync(DateTime startDate, DateTime endDate)
        {
            string cacheKey = $"TopProductsList_{startDate.ToString("yyyyMMdd")}_{endDate.ToString("yyyyMMdd")}";

            if (_cache.TryGetValue(cacheKey, out List<KeyValuePair<string, int>> cachedData))
            {
                return cachedData;
            }

            try
            {
                var data = await _dashboardRepository.GetTopProductsListAsyncDAL(startDate, endDate);
                _cache.Set(cacheKey, data, TimeSpan.FromMinutes(10));
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<KeyValuePair<DateTime, decimal>>> GetGrossRevenueAsync(DateTime startDate, DateTime endDate)
        {
            string cacheKey = $"GrossRevenue_{startDate.ToString("yyyyMMdd")}_{endDate.ToString("yyyyMMdd")}";

            if (_cache.TryGetValue(cacheKey, out List<KeyValuePair<DateTime, decimal>> cachedData))
            {
                return cachedData;
            }

            try
            {
                var data = await _dashboardRepository.GetGrossRevenueAsyncDAL(startDate, endDate);
                _cache.Set(cacheKey, data, TimeSpan.FromMinutes(10));
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<Product>> GetUnderStockProductsAsync()
        {
            if (_cache.TryGetValue("UnderStockProducts", out List<Product> cachedData))
            {
                return cachedData;
            }

            try
            {
                var data = await _dashboardRepository.GetUnderStockProductsAsyncDAL();
                _cache.Set("UnderStockProducts", data, TimeSpan.FromMinutes(10));
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
