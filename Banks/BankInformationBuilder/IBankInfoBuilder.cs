using System.Collections.Generic;
using Banks.BankService;

namespace Banks.BankInformationBuilder
{
    public interface IBankInfoBuilder
    {
        void Reset();
        void SetTrustLimit(double trustlimit);
        void SetDebitYearlyPercent(double debityearlypercent);
        void SetCreditLimit(double creditlimit);
        void SetCreditMonthlyComission(double creditmonthlycomission);
        void SetDepositYearlyPercent(List<DepositYearPercent> deposityearlypercent);
        BankInformation GetBankInfo();
    }
}