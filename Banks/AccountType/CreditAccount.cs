using Banks.BankService;

namespace Banks.AccountType
{
    public class CreditAccount : AccountReference
    {
        private double creditLimit;
        private DateProvider lastComissionWithdrawal;
        private double comissionWithdrawal;
        private bool accountWasNegative;

        public CreditAccount(string clientid, double money, DateProvider today, bool trust, BankInformation bankinfo)
        {
            AccType = "Credit";
            Id = bankinfo.AvailibleAccountId;
            ClientId = clientid;
            Money = money;
            Trustful = trust;
            TrustLimit = bankinfo.TrustLimit;
            creditLimit = bankinfo.CreditLimit;
            comissionWithdrawal = bankinfo.CreditMonthlyComission;
            lastComissionWithdrawal = new DateProvider(today.Day, today.Month, today.Year);
            accountWasNegative = false;
        }

        public override void Replenish(double amount)
        {
            Money += amount;
        }

        public override bool Withdraw(double amount, DateProvider today)
        {
            if (Money - amount < creditLimit || (!Trustful && amount > TrustLimit))
            {
                return false;
            }
            else
            {
                Money -= amount;
                if (Money < 0)
                    accountWasNegative = true;

                return true;
            }
        }

        public override void ReNew(DateProvider today)
        {
            if (today.Year > lastComissionWithdrawal.Year ||
                (today.Year == lastComissionWithdrawal.Year && today.Month > lastComissionWithdrawal.Month))
            {
                if (accountWasNegative)
                    Money -= comissionWithdrawal;

                accountWasNegative = false;
                lastComissionWithdrawal = new DateProvider(today.Day, today.Month, today.Year);
            }

            if (Money < 0)
                accountWasNegative = true;
        }
    }
}