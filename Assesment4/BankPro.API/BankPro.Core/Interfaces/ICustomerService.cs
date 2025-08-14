using BankPro.Core.DTOs;
using BankPro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPro.Core.Interfaces
{
   public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> GetByIdAsync(int id);
    Task<Customer> AddCustomerAsync(CustomerRequestDTO dto); // return created Customer
    Task UpdateCustomerAsync(int id, CustomerRequestDTO dto);
    Task DeleteCustomerAsync(int id);
}

}
