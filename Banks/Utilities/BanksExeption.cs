using System;

namespace Banks.Utilities
{
    public class BanksExeption : Exception
    {
            public BanksExeption()
            {
            }

            public BanksExeption(string message)
                : base(message)
            {
            }

            public BanksExeption(string message, Exception innerException)
                : base(message, innerException)
            {
            }
        }
}