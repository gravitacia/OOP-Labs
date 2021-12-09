using System;
using Banks.BankService;

namespace Banks.AccountType
{
    public class DebitAccount : AccountReference
    {
        private DateProvider _lastMonthlyPercentCharge;
        private DateProvider _lastDailyPercentCharge;
        private double _yearlyPercent;
        private double _percentPay;

        public DebitAccount(string clientid, double money, DateProvider today, bool trust, BankInformation bankinfo)
        {
            AccType = "Debit";
            Id = bankinfo.AvailibleAccountId;
            ClientId = clientid;
            _percentPay = 0;
            Money = money;
            Trustful = trust;
            _lastMonthlyPercentCharge = new DateProvider(DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);
            _lastDailyPercentCharge = new DateProvider(DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);
            _yearlyPercent = bankinfo.DebitYearlyPercent;
            TrustLimit = bankinfo.TrustLimit;
        }

        public override void Replenish(double amount)
        {
            Money += amount;
        }

        public override bool Withdraw(double amount, DateProvider today)
        {
            if (Money < amount && (Trustful || amount <= TrustLimit))
            {
                Money -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void ReNew(DateProvider today)
        {
            if (today.Year > _lastMonthlyPercentCharge.Year ||
                (today.Year == _lastMonthlyPercentCharge.Year && today.Month > _lastMonthlyPercentCharge.Month))
            {
                _lastMonthlyPercentCharge = new DateProvider(DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);
                Replenish(_percentPay);
                _percentPay = 0;
            }

            if (!today.Equals(_lastDailyPercentCharge))
            {
                _percentPay += (_yearlyPercent / 36500) * Money;
                _lastDailyPercentCharge = new DateProvider(DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);
            }
        }
    }
}