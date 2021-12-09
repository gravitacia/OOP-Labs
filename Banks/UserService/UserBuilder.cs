using System;

namespace Banks.UserService
{
    public class UserBuilder : IUserBuilder
    {
        private User _user = new UserService.User();
        public UserBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._user = new UserService.User();
        }

        public void SetName(string name)
        {
            this._user.Name = name;
        }

        public void SetSurname(string surname)
        {
            this._user.Surname = surname;
        }

        public void SetAdress(string adress)
        {
            this._user.Address = adress;
        }

        public void SetPassport(string passport)
        {
            this._user.Passport = passport;
        }

        public User GetUser()
        {
            var rnd = new Random();
            User result = this._user;
            this._user.UId = rnd.Next();
            this.Reset();
            return result;
        }
    }
}