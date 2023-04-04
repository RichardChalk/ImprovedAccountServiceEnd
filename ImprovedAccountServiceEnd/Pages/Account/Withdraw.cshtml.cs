using ImprovedAccountServiceEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankAccountTransactionsEnd.Pages.Account
{
    [BindProperties]
    public class WithdrawModel : PageModel
    {
        private readonly IAccountService2 _accountService2;

        public WithdrawModel(IAccountService2 accountService2)
        {
            _accountService2 = accountService2;
        }

        [Range(100, 10000)]
        public decimal Amount { get; set; }

        public decimal Balance { get; set; }
        public void OnGet(int accountId)
        {
            Balance = _accountService2.GetAccount(accountId).Balance;
        }

        public IActionResult OnPost(int accountId)
        {
            var status = _accountService2.Withdraw(accountId, Amount);

            if (ModelState.IsValid)
            {
                if (status == ErrorCode.OK)
                {
                    return RedirectToPage("Index");
                }
            }

            if (status == ErrorCode.BalanceTooLow)
            {
                ModelState.AddModelError("Amount", "You don't have that much money!");
            }

            if (status == ErrorCode.IncorrectAmount)
            {
                ModelState.AddModelError("Amount", "Please enter a correct amount (100-10000)!");
            }

            return Page();
        }
    }
}
