using Banks.AccountType;
using Banks.BankService;

namespace Banks.TransactionsService
{
    public class Withdrawal : ITransaction
    {
        public void ProcessTransaction(BankTransactions bankTransaction)
        {
            bankTransaction.FirstAcc.Withdraw(bankTransaction.Amount);
        }

        public void Cancel(BankTransactions bankTransaction)
        {
            bankTransaction.FirstAcc.Replenish(bankTransaction.Amount);
            bankTransaction.IsCanceled = true;
        }
    }
}