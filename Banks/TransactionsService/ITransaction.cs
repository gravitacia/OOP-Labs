using Banks.AccountType;
using Banks.BankService;

namespace Banks.TransactionsService
{
    public interface ITransaction
    {
        public void ProcessTransaction(BankTransactions bankTransaction);

        public void Cancel(BankTransactions bankTransaction);
    }
}