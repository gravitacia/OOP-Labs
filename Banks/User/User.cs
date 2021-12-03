using System.Collections.Generic;

namespace Banks.User
{
    public class User
    {
        private string _id;
        private string _name;
        private string _surname;
        private string _address;
        private string _passport;

        public User(string name, string surname, string address, string passport, BankInformation bankInfo)
        {
            _id = bankInfo.AvailibleClientId.ToString();
            _name = name;
            _surname = surname;
            _address = address;
            _passport = passport;
        }

        public bool IsTrustful()
        {
            if (_address == string.Empty || _passport == string.Empty)
                return false;
            else
                return true;
        }
    }
}
