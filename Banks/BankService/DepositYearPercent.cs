namespace Banks.BankService
{
    public class DepositYearPercent
    {
        public DepositYearPercent(double neededamount, double appropriatepercent)
        {
                NeededAmount = neededamount;
                AppropriatePercent = appropriatepercent;
        }

        public double AppropriatePercent { get; set; }

        public double NeededAmount { get; set; }
    }
}