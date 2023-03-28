namespace BankingSystemTest
{
    public class AuthenticationException: Exception
    {
        public AuthenticationException(string message)
            :base(message) { }
    }

    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException()
            : base("Account not found") { }
    }

    public class CreditLimitException : Exception
    {
        public CreditLimitException()
            : base("A user cannot deposit more than $10,000 in a single transaction.") { }
    }
    public class DebitLimitException : Exception
    {
        public DebitLimitException()
            : base("A user cannot withdraw more than 90% of their total balance from an account in a single transaction.") { }
    }
    public class BalanceLimitException : Exception
    {
        public BalanceLimitException()
            : base("An account cannot have less than $100 at any time in an account.") { }
    }
}
