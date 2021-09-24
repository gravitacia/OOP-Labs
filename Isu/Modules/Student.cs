namespace Isu.Modules
{
    public class Student
    {
        public Student(string name, int id, string group_name)
        {
            Name = name;
            Id = id;
            GroupName = group_name;
        }

        public string Name { get; }
        public int Id { get; }
        public string GroupName { get; set; }
    }
}