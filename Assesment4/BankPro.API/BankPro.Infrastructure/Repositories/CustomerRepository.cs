using BankPro.Core.Entities;
using BankPro.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankPro.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers = new();
        private int _nextId = 1;

        public async Task AddAsync(Customer entity)
        {
            await Task.Run(() =>
            {
                entity.Id = _nextId++;
                _customers.Add(entity);
            });
        }

        public async Task DeleteAsync(int id)
        {
            await Task.Run(() =>
            {
                var customer = _customers.FirstOrDefault(c => c.Id == id);
                if (customer != null) _customers.Remove(customer);
            });
        }

        public async Task<IEnumerable<Customer>> GetAllAsync() =>
            await Task.Run(() => _customers.AsEnumerable());

        public async Task<Customer> GetByIdAsync(int id) =>
            await Task.Run(() => _customers.FirstOrDefault(c => c.Id == id));

        public async Task UpdateAsync(Customer entity)
        {
            await Task.Run(() =>
            {
                var existing = _customers.FirstOrDefault(c => c.Id == entity.Id);
                if (existing != null)
                {
                    existing.Name = entity.Name;
                    existing.Email = entity.Email;
                }
            });
        }
    }
}
