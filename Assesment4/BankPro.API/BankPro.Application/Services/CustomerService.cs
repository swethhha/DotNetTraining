using AutoMapper;
using BankPro.Core.DTOs;
using BankPro.Core.Entities;
using BankPro.Core.Interfaces;
using BankPro.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankPro.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        public async Task<Customer> AddCustomerAsync(CustomerRequestDTO dto)
        {
            var customer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email
            };

            // Save to repository (or in-memory storage)
            await _customerRepo.AddAsync(customer);

            return customer; // Has Id now
        }


        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerRepo.GetAllAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _customerRepo.GetByIdAsync(id);
        }

        public async Task UpdateCustomerAsync(int id, CustomerRequestDTO dto)
        {
            var existing = await _customerRepo.GetByIdAsync(id);
            if (existing != null)
            {
                _mapper.Map(dto, existing);
                await _customerRepo.UpdateAsync(existing);
            }
        }
    }
}
