using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IManager<T>
    {
        void Add(T obj);
        T Get(int id);
        List<T> GetAll();
    }
}
