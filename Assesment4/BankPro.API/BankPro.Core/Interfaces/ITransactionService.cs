using BankPro.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPro.Core.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionResponseDTO>> GetAllTransactionsAsync();
        Task<TransactionResponseDTO> GetTransactionByIdAsync(int id);
        Task<TransactionResponseDTO> AddTransactionAsync(TransactionRequestDTO transactionDto);
        Task<bool> UpdateTransactionAsync(int id, TransactionRequestDTO transactionDto);
        Task<bool> DeleteTransactionAsync(int id);
    }
}
