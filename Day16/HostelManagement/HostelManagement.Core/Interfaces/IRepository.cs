﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelManagement.Core.Interfaces
{
    public interface IRepository <T> where T : class
    {
      public void Add(T entity);
        public void Update(T entity);
        public void Delete(int id);
        public T? GetById(int id);
        List<T> GetAll();


    }
}
