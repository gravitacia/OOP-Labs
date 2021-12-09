using System;

namespace Banks.BankService
{
    public class BankTransactions
    {
        public BankTransactions(string tp, int acc, double amount, BankInformation bi)
        {
            Id = bi.AvailibleTransactionId.ToString();
            Type = tp;
            Amount = amount;
            if (tp == "TFR")
                throw new Exception("Not enough parameters for transfer transaction");
            FirstAcc = acc;
            IsCanceled = false;
        }

        public BankTransactions(string tp, int facc, int sacc, double amount, BankInformation bi)
        {
            Id = bi.AvailibleTransactionId.ToString();
            Type = tp;
            Amount = amount;
            if (tp != "TFR")
                throw new Exception("Too many parameters");
            FirstAcc = facc;
            SecAcc = sacc;
            IsCanceled = false;
        }

        public string Id { get; set; }
        public string Type { get; set; } // "RNT" - Replenishment "WWL" - Withdrawal "TFR" - Transfer
        public int FirstAcc { get; set; }
        public int SecAcc { get; set; }
        public double Amount { get; set; }
        public bool IsCanceled { get; set; }
    }
}