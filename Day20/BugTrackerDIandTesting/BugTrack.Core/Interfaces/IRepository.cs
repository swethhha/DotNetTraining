﻿namespace BugTrack.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // Sync
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        T? GetById(int id);
        IEnumerable<T> GetAll();
    }
}
