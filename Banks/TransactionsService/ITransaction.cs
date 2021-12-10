using Banks.AccountType;
using Banks.BankService;

namespace Banks.TransactionsService
{
    public interface ITransaction
    {
        public abstract void ProcessTransaction(BankTransactions bankTransaction);

        public abstract void Cancel(BankTransactions bankTransaction);
    }
}