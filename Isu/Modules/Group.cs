using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Modules
{
    public class Group
    {
        private const int GroupNameLength = 5;
        private const char CourseNumberCheck0 = '0';
        private const char CourseNumberCheck5 = '5';
        private const char FacultySymbolM = 'M';
        private const char FacultySymbol3 = '3';
        private const int MaxStudentsInGroup = 20;
        private List<Student> _studentsList;
        public Group(string name)
        {
            if (!IsAllowed(name))
            {
                throw new IsuException("This group got wrong name");
            }
            else
            {
                GroupName = name;
                _studentsList = new List<Student>();
            }
        }

        public string GroupName { get; }

        public static void AddStudentToGroup(Group group, Student student)
        {
            if (group._studentsList.Count < MaxStudentsInGroup)
            {
                group._studentsList.Add(student);
            }
            else
            {
                throw new IsuException("Too many students");
            }
        }

        public static void RemoveStudentFromGroup(Group group, Student student)
        {
            group._studentsList.Remove(student);
        }

        public static Student GetStudentWithId(Group group, int id)
        {
            return @group._studentsList.FirstOrDefault(curStudent => curStudent.Id == id);
        }

        public static Student GetStudentWithName(Group group, string name)
        {
            return @group._studentsList.FirstOrDefault(curStudent => curStudent.Name == name);
        }

        public static List<Student> GetStudentsList(List<Group> group, string name)
        {
            foreach (Group groupName in @group.Where(groupName => groupName.GroupName == name))
            {
                return groupName._studentsList;
            }

            throw new IsuException("Group not found!");
        }

        public static List<Student> GetAllStudentFromGroup(Group @group)
        {
            return group._studentsList;
        }

        private static bool IsAllowed(string name)
        {
            int nameLength = name.Length;
            char letterSymbol = name[0];
            char numberSymbol = name[1];
            char courseNumber = name[2];

            return nameLength == GroupNameLength && courseNumber is < CourseNumberCheck5 and > CourseNumberCheck0 && letterSymbol == FacultySymbolM && numberSymbol == FacultySymbol3;
        }
    }
}