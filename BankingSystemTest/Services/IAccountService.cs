using BankingSystemTest.Areas.BankSystemAPI.Models;

namespace BankingSystemTest.Services
{
    public interface IAccountService
    {
        //
        // Summary:
        //      Returns a list of accounts linked to specified user ID
        IEnumerable<Account> GetAccounts(string userId);
        //
        // Summary:
        //      Creates a new account
        void Create(AccountDto accountDto);
        //
        // Summary:
        //      Provides deposit and withdraw operations for the account with validation
        void Update(string Id, double value);
        //
        // Summary:
        //      Deleted an account with specified ID
        void Delete(string Id);
    }
}
