using BankAccountTransactionsEnd.Data;
using ImprovedAccountServiceEnd.Services;
public enum ErrorCode
{
    OK,
    BalanceTooLow,
    IncorrectAmount
}
namespace ImprovedAccountServiceEnd.Services
{
  
    public interface IAccountService2
    {
        Account GetAccount(int accountId);
        ErrorCode Withdraw(int accountId, decimal amount);
        ErrorCode Deposit(int accountId, decimal amount);
    }
}
