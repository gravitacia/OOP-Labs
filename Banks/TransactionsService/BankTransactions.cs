using System;
using Banks.AccountType;
using Banks.BankInformationBuilder;

namespace Banks.TransactionsService
{
    public class BankTransactions
    {
        public BankTransactions(ITransaction tp, AccountReference acc, double amount, BankInformation bi)
        {
            Id = bi.AvailibleTransactionId.ToString();
            Type = tp;
            Amount = amount;
            FirstAcc = acc;
            IsCanceled = false;
        }

        public BankTransactions(ITransaction tp, AccountReference facc, AccountReference sacc, double amount, BankInformation bi)
        {
            Id = bi.AvailibleTransactionId.ToString();
            Type = tp;
            Amount = amount;
            FirstAcc = facc;
            SecAcc = sacc;
            IsCanceled = false;
        }

        public string Id { get; set; }
        public ITransaction Type { get; set; } // "RNT" - Replenishment "WWL" - Withdrawal "TFR" - Transfer
        public AccountReference FirstAcc { get; set; }
        public AccountReference SecAcc { get; set; }
        public double Amount { get; set; }
        public bool IsCanceled { get; set; }
    }
}