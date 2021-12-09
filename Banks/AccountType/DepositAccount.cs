using System;
using Banks.BankService;

namespace Banks.AccountType
{
    public class DepositAccount : AccountReference
    {
        private DateProvider withdrawalLimitedDue;
        private DateProvider _lastMonthlyPercentCharge;
        private DateProvider _lastDailyPercentCharge;
        private double yearlyPercent;
        private double _percentPay;

        public DepositAccount(string clientid, double money, DateProvider today, bool trust, BankInformation bankinfo, DateProvider wlimit)
        {
            AccType = "Deposit";
            Id = bankinfo.AvailibleAccountId;
            ClientId = clientid;
            Money = money;
            Trustful = trust;
            withdrawalLimitedDue = new DateProvider(wlimit.Day, wlimit.Month, wlimit.Year);
            _lastMonthlyPercentCharge = new DateProvider(DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);
            _lastDailyPercentCharge = new DateProvider(DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);
            TrustLimit = bankinfo.TrustLimit;
            _percentPay = 0;

            int i = 0;
            while (i < bankinfo.DepositYearlyPercent.Count)
            {
                if (bankinfo.DepositYearlyPercent[i].NeededAmount <= money)
                    yearlyPercent = bankinfo.DepositYearlyPercent[i].AppropriatePercent;
                else
                    break;
                i++;
            }
        }

        public override void Replenish(double amount)
        {
            Money += amount;
        }

        public override bool Withdraw(double amount, DateProvider today)
        {
            if (withdrawalLimitedDue.Day > today.Day || Money < amount || (!Trustful && amount > TrustLimit))
            {
                return false;
            }
            else
            {
                Money -= amount;
                return true;
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

            if (today.Equals(_lastDailyPercentCharge)) return;
            _percentPay += (yearlyPercent / 36500) * Money;
            _lastDailyPercentCharge = new DateProvider(DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);
        }
    }
}