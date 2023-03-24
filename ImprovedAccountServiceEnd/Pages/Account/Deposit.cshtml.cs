using BankAccountTransactionsEnd.Services;
using ImprovedAccountServiceEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankAccountTransactionsEnd.Pages.Account
{
    [BindProperties]
    public class DepositModel : PageModel
    {
        private readonly IAccountService2 _accountService2;

        public DepositModel(IAccountService2 accountService2)
        {
            _accountService2 = accountService2;
        }
        
        [Range(100,10000)]
        public decimal Amount { get; set; }
        public DateTime DepositDate { get; set; }

        [Required(ErrorMessage = "You forgot to write a comment!")]
        [MinLength(5, ErrorMessage = "Comments must be at least 5 characters long")]
        [MaxLength(250, ErrorMessage = "OK, thats just too many words")]
        public string Comment { get; set; }
        
        public void OnGet(int accountId)
        {
            DepositDate = DateTime.Now.AddHours(1);
        }

        public IActionResult OnPost(int accountId)
        {
            var status = _accountService2.Deposit(accountId, Amount, Comment);

            if (ModelState.IsValid)
            {
                if (status == ErrorCode.OK)
                {
                    return RedirectToPage("Index");
                }

                if (status == ErrorCode.IncorrectAmount)
                {
                    ModelState.AddModelError("Amount", "Please enter a correct amount (100-10000)!");
                }

                if (status == ErrorCode.CommentEmpty)
                {
                    ModelState.AddModelError("Comment", "Please enter a comment");
                }

            }
            return Page();
        }

    }
}
