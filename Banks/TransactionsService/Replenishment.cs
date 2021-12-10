using System;
using Banks.AccountType;
using Banks.BankService;
using Banks.Utilities;

namespace Banks.TransactionsService
{
    public class Replenishment : ITransaction
    {
        public void ProcessTransaction(BankTransactions bankTransaction)
        {
            bankTransaction.FirstAcc.Replenish(bankTransaction.Amount);
        }

        public void Cancel(BankTransactions bankTransaction)
        {
            bool t = bankTransaction.FirstAcc.Withdraw(bankTransaction.Amount);
            if (t)
                bankTransaction.IsCanceled = true;
            else
                throw new BanksExeption("Transaction cannot be canceled");
        }
    }
}