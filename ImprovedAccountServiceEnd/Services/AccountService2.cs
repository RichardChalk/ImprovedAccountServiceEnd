using BankAccountTransactionsEnd.Data;
using SkysFormsDemo.Data;
using System.Diagnostics.Metrics;

namespace ImprovedAccountServiceEnd.Services
{
    public class AccountService2 : IAccountService2
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountService2(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ErrorCode Withdraw(int accountId, decimal amount)
        {
            var accountDb = _dbContext.Accounts.First(a=>a.Id == accountId);
            
            if (accountDb.Balance < amount)
            {
                return ErrorCode.BalanceTooLow;
            }

            if (amount < 100 || amount > 10000)
            {
                return ErrorCode.IncorrectAmount;
            }


            // Här skulle man tex. skapa en ny databas entitet som heter "Transaction"
            // ... och fyller den med info här...
            // tex. Date, Amount, Current Balance, Account number etc.
            accountDb.Balance -= amount;
            _dbContext.SaveChanges();
            return ErrorCode.OK;
        }

        public ErrorCode Deposit(int accountId, decimal amount)
        {
            var accountDb = _dbContext.Accounts.First(a => a.Id == accountId);

            if (amount < 100 || amount > 10000)
            {
                return ErrorCode.IncorrectAmount;
            }

            // Här skulle man tex. skapa en ny databas entitet som heter "Transaction"
            // ... och fyller den med info här...
            // tex. Date, Amount, Current Balance, Account number etc.
            accountDb.Balance += amount;
            _dbContext.SaveChanges();
            return ErrorCode.OK;

        }
        public Account GetAccount(int accountId)
        {
            return _dbContext.Accounts.First(a => a.Id == accountId);
        }
    }
}
