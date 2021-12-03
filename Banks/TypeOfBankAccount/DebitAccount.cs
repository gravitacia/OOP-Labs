namespace Banks.TypeOfBankAccount
{
    public class DebitAccount : IBankAccount
    {
        public DebitAccount(int money, double percent)
        {
            Money = money;
            Percent = percent;
        }

        public double Percent { get; set; }

        public int Money { get; set; }

        public void TakeMoney(int money)
        {
            Money -= money;
        }
    }
}