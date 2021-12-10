using System.Collections.Generic;
using Banks.BankService;

namespace Banks.BankInformationBuilder
{
    public class BankInfoBuilder : IBankInfoBuilder
    {
        private BankInformation _bankInformation = new BankInformation();

        public BankInfoBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._bankInformation = new BankInformation();
        }

        public void SetTrustLimit(double trustlimit)
        {
            this._bankInformation.TrustLimit = trustlimit;
        }

        public void SetDebitYearlyPercent(double debityearlypercent)
        {
            this._bankInformation.DebitYearlyPercent = debityearlypercent;
        }

        public void SetCreditLimit(double creditlimit)
        {
            this._bankInformation.CreditLimit = creditlimit;
        }

        public void SetCreditMonthlyComission(double creditmonthlycomission)
        {
            this._bankInformation.CreditMonthlyComission = creditmonthlycomission;
        }

        public void SetDepositYearlyPercent(List<DepositYearPercent> deposityearlypercent)
        {
            this._bankInformation.DepositYearlyPercent = deposityearlypercent;
        }

        public BankInformation GetBankInfo()
        {
            BankInformation result = this._bankInformation;
            this.Reset();
            return result;
        }
    }
}