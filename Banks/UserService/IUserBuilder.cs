namespace Banks.UserService
{
    public interface IUserBuilder
    {
        void Reset();
        void SetName(string name);
        void SetSurname(string surname);
        void SetAdress(string adress);
        void SetPassport(string passport);
        User GetUser();
    }
}