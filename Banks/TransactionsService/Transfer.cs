using System;
using Banks.AccountType;
using Banks.BankService;
using Banks.Utilities;

namespace Banks.TransactionsService
{
    public class Transfer : ITransaction
    {
        public void ProcessTransaction(BankTransactions bankTransaction)
        {
            bankTransaction.FirstAcc.Withdraw(bankTransaction.Amount);
            bankTransaction.SecAcc.Replenish(bankTransaction.Amount);
        }

        public void Cancel(BankTransactions bankTransaction)
        {
            bool t = bankTransaction.SecAcc.Withdraw(bankTransaction.Amount);
            if (t)
            {
                bankTransaction.FirstAcc.Replenish(bankTransaction.Amount);
                bankTransaction.IsCanceled = true;
            }
            else
            {
                throw new BanksExeption("Transaction cannot be canceled");
            }
        }
    }
}