using AutoMapper;
using BankPro.Core.DTOs;
using BankPro.Core.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankPro.Application.Mapping
{
    public class BankProProfile : Profile
    {
        public BankProProfile()
        {
            // Customer mapping
            CreateMap<CustomerRequestDTO, Customer>();
            CreateMap<Customer, CustomerResponseDTO>();

            // Account mapping
            CreateMap<AccountRequestDTO, Account>();
            CreateMap<Account, AccountResponseDTO>();

            // Transaction mapping
            CreateMap<TransactionRequestDTO, Transaction>();
            CreateMap<Transaction, TransactionResponseDTO>();
        }
    }
}
