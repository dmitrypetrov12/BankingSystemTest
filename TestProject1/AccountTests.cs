using BankingSystemTest;
using BankingSystemTest.Areas.BankSystemAPI.Models;
using BankingSystemTest.Services;

namespace TestProject1
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void DeleteNotFount()
        {
            BankingSystemServiceProvider.AccountService.Delete("");
        }
        
        [TestMethod]
        public void CreateAccount()
        {
            AccountDto accountDto = new AccountDto() { AccountName = "AccountName", UserId = "UserId" };
            BankingSystemServiceProvider.AccountService.Create(accountDto);

            var accounts = BankingSystemServiceProvider.AccountService.GetAccounts("UserId");
            Assert.IsNotNull(accounts);
            var newAccount = accounts.FirstOrDefault((a) => a.Name == "AccountName");
            Assert.IsNotNull(newAccount);
        }

        [TestMethod]
        public void CreditDebit()
        {
            AccountDto accountDto = new AccountDto() { AccountName = "AccountName1", UserId = "UserId1" };
            BankingSystemServiceProvider.AccountService.Create(accountDto);
            var account = BankingSystemServiceProvider.AccountService.GetAccounts("UserId1").FirstOrDefault((a) => a.Name == "AccountName1");
            Assert.AreEqual(100, account.Balance);

            BankingSystemServiceProvider.AccountService.Update(account.Id, 2000);
            account = BankingSystemServiceProvider.AccountService.GetAccounts("UserId1").FirstOrDefault((a) => a.Name == "AccountName1");
            Assert.AreEqual(2100, account.Balance);

            BankingSystemServiceProvider.AccountService.Update(account.Id, -1000);
            account = BankingSystemServiceProvider.AccountService.GetAccounts("UserId1").FirstOrDefault((a) => a.Name == "AccountName1");
            Assert.AreEqual(1100, account.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(CreditLimitException))]
        public void CreditLimit()
        {
            AccountDto accountDto = new AccountDto() { AccountName = "AccountName2", UserId = "UserId2" };
            BankingSystemServiceProvider.AccountService.Create(accountDto);
            var account = BankingSystemServiceProvider.AccountService.GetAccounts("UserId2").FirstOrDefault((a) => a.Name == "AccountName2");
            Assert.AreEqual(100, account.Balance);

            BankingSystemServiceProvider.AccountService.Update(account.Id, 10001);
        }

        [TestMethod]
        [ExpectedException(typeof(DebitLimitException))]
        public void DebitLimit()
        {
            AccountDto accountDto = new AccountDto() { AccountName = "AccountName3", UserId = "UserId3" };
            BankingSystemServiceProvider.AccountService.Create(accountDto);
            var account = BankingSystemServiceProvider.AccountService.GetAccounts("UserId3").FirstOrDefault((a) => a.Name == "AccountName3");
            Assert.AreEqual(100, account.Balance);

            BankingSystemServiceProvider.AccountService.Update(account.Id, -91);
        }
    }
}