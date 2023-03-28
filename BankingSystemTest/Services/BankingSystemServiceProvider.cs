namespace BankingSystemTest.Services
{
    //
    // Summary:
    //      Provides asscess to the singelton services. Could be used as a factory
    public static class BankingSystemServiceProvider
    {
        private static IAuthenticationService _authenticationService;
        private static IAccountService _accountService;
        public static IAuthenticationService AuthenticationService 
        {
            get
            {
                if(_authenticationService == null)
                {
                    _authenticationService = new AuthenticationService();
                }
                return _authenticationService;
            }
        }
        public static IAccountService AccountService
        {
            get
            {
                if (_accountService == null)
                {
                    _accountService = new AccountService();
                }
                return _accountService;
            }
        }
    }
}
