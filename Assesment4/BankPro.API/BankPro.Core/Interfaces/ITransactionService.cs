using BankPro.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankPro.Core.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionResponseDTO>> GetAllTransactionsAsync();
        Task<TransactionResponseDTO> GetTransactionByIdAsync(int id);


        Task<IEnumerable<TransactionResponseDTO>> GetTransactionsByTypeAsync(string type);
        Task<IEnumerable<TransactionResponseDTO>> GetTransactionsByDateRangeAsync(DateTime from, DateTime to);
    }
}
