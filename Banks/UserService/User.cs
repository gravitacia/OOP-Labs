namespace Banks.UserService
{
    public class User
    {
        public User()
        {
        }

        public string Passport { get; set; }

        public string Address { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public int UId { get; set; }

        public bool Istrustful()
        {
            if (Address == string.Empty || Passport == string.Empty)
                return false;
            else
                return true;
        }
    }
}
