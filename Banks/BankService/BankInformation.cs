using System.Collections.Generic;

namespace Banks.BankService
{
    public class BankInformation
    {
        public BankInformation(double trustlimit, double debityearlypercent, double creditlimit, double creditmonthlycomission, List<DYP> deposityearlypercent)
            {
                AvailibleClientId = 0;
                AvailibleAccountId = 0;
                AvailibleTransactionId = 0;
                TrustLimit = trustlimit;
                DebitYearlyPercent = debityearlypercent;
                CreditLimit = creditlimit;
                CreditMonthlyComission = creditmonthlycomission;
                DepositYearlyPercent = deposityearlypercent;
            }

        public int AvailibleClientId { get; set; }
        public int AvailibleAccountId { get; set; }
        public int AvailibleTransactionId { get; set; }
        public double TrustLimit { get; set; }
        public double DebitYearlyPercent { get; set; }
        public double CreditLimit { get; set; }
        public double CreditMonthlyComission { get; set; }
        public List<DYP> DepositYearlyPercent { get; set; }
    }
}