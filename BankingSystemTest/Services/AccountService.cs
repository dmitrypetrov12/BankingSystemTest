using BankingSystemTest.Areas.BankSystemAPI.Models;

namespace BankingSystemTest.Services
{
    //
    // Summary:
    //      Instance of the IAccountService with in-memory data
    public class AccountService : IAccountService
    {
        private List<Account> _accounts;
        public AccountService() 
        {
            //Default data
            _accounts = new List<Account>() { new Account() { Id = "0000", Name = "My Account", Balance = 100, UserId = "testUser" } };
        }
        public IEnumerable<Account> GetAccounts(string userId)
        {
            return _accounts.Where(a => a.UserId == userId);
        }
        public void Create(AccountDto accountDto)
        {
            var account = new Account() { Id = Guid.NewGuid().ToString(), Name = accountDto.AccountName, UserId = accountDto.UserId };
            account.Balance = 100; //initial balance
            _accounts.Add(account);
        }
        public void Delete(string Id)
        {
            var account = _accounts.FirstOrDefault(a => a.Id == Id);
            if(account == null)
            {
                throw new AccountNotFoundException();
            }
            _accounts.Remove(account);
        }
        public void Update(string Id, double value)
        {
            var account = _accounts.FirstOrDefault(a => a.Id == Id);
            if (account == null)
            {
                throw new AccountNotFoundException();
            }

            //validate request
            if(value > 10000) 
            {
                throw new CreditLimitException();
            }
            if (value < 0)
            {
                if((-value*100/account.Balance) > 90)
                {
                    throw new DebitLimitException();
                }
                if(account.Balance + value < 100)
                {
                    throw new BalanceLimitException();
                }
            }
            account.Balance += value;
        }
    }
}
