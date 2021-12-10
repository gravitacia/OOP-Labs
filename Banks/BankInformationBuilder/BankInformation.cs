using System.Collections.Generic;
using Banks.BankService;

namespace Banks.BankInformationBuilder
{
    public class BankInformation
    {
        public BankInformation()
            {
                AvailibleClientId = 0;
                AvailibleAccountId = 0;
                AvailibleTransactionId = 0;
            }

        public int AvailibleClientId { get; set; }
        public int AvailibleAccountId { get; set; }
        public int AvailibleTransactionId { get; set; }
        public double TrustLimit { get; set; }
        public double DebitYearlyPercent { get; set; }
        public double CreditLimit { get; set; }
        public double CreditMonthlyComission { get; set; }
        public List<DepositYearPercent> DepositYearlyPercent { get; set; }
    }
}