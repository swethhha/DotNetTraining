using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EmployeeTracker.Core.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        T GetById(int id);
        List<T> GetAll();
    }
}

