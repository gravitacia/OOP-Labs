using System;

namespace Banks
{
    public class DateProvider
    {
        public DateProvider(int d, int m, int y)
        {
            Day = d - 1;
            Month = m;
            Year = y;

            AddOneDay();
        }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public void AddOneDay()
        {
            Day++;
            if (Day >= 32)
            {
                Day = 1;
                Month++;
            }

            if (Month >= 13)
            {
                Month = 1;
                Year++;
            }

            if (Day == 31 && (Month == 4 || Month == 6 || Month == 9 || Month == 11))
            {
                Day = 1;
                Month++;
            }

            if (Day == 30 && Month == 2)
            {
                Day = 1;
                Month++;
            }

            if (Day == 29 && Month == 2 && (Year % 4) != 0)
            {
                Day = 1;
                Month++;
            }
        }

        public void Show()
        {
            Console.WriteLine(Day + "." + Month + "." + Year);
        }
    }
}