using Banks.Utilities;

namespace Banks.AccountType
{
    public abstract class AccountReference
    {
        public double Money { get; set; }
        public int Id { get; set; }
        public EnumAccountTypes AccType { get; set; }
        public string ClientId { get; set; }
        public double TrustLimit { get; set; }
        public bool Trustful { get; set; }

        public abstract void ReNew(DateProvider today);
        public abstract void Replenish(double amount);
        public abstract bool Withdraw(double amount, DateProvider today);
        public bool Withdraw(double amount) // In Case of Cancelation
        {
            if (Money >= amount)
            {
                Money -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}