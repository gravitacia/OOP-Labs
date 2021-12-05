using System;

namespace Banks.Utilities
{
    internal class Date
    {
        private int _day;
        private int _month;
        private int _year;

        public Date(int d, int m, int y)
        {
            _day = d - 1;
            _month = m;
            _year = y;

            AddOneDay();
        }

        public Date(Date date)
        {
            _day = date._day;
            _month = date._month;
            _year = date._year;
        }

        public void AddOneDay()
        {
            _day++;
            if (_day >= 32)
            {
                _day = 1;
                _month++;
            }

            if (_month >= 13)
            {
                _month = 1;
                _year++;
            }

            if (_day == 31 && (_month == 4 || _month == 6 || _month == 9 || _month == 11))
            {
                _day = 1;
                _month++;
            }

            if (_day == 30 && _month == 2)
            {
                _day = 1;
                _month++;
            }

            if (_day == 29 && _month == 2 && (_year % 4) != 0)
            {
                _day = 1;
                _month++;
            }
        }

        public void Show()
        {
            Console.WriteLine(_day + "." + _month + "." + _year);
        }
    }
}