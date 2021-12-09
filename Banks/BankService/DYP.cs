namespace Banks
{
    public class DYP
    {
        public DYP(double neededamount, double appropriatepercent)
        {
                NeededAmount = neededamount;
                AppropriatePercent = appropriatepercent;
        }

        public double AppropriatePercent { get; set; }

        public double NeededAmount { get; set; }
    }
}