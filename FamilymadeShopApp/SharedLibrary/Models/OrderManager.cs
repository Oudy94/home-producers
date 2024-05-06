﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary.Helpers;
using SharedLibrary.Interfaces;

namespace SharedLibrary.Models
{
    public class OrderManager: IManager<Order>
    {
        private readonly DatabaseHelper _dbHelper;

        public OrderManager()
        {
            this._dbHelper = new DatabaseHelper();
        }

        public void Add(Order order)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.AddOrderToDB(order);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }
        }

        public Order Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
