using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Modules
{
    public class Group
    {
        public Group(string name)
        {
            if (!IsAllowed(name))
            {
                throw new IsuException("This group got wrong name");
            }
            else
            {
                GroupName = name;
                StudentsList = new List<Student>();
            }
        }

        public List<Student> StudentsList { get; set; }
        public string GroupName { get; }
        public int StudentsInGroup { get; set; }

        private static bool IsAllowed(string name)
        {
            const int groupNameLength = 5;
            const char courseNumberCheck0 = '0';
            const char courseNumberCheck5 = '5';
            const char facultySymbolM = 'M';
            const char facultySymbol3 = '3';
            int nameLength = name.Length;
            char letterSymbol = name[0];
            char numberSymbol = name[1];
            char courseNumber = name[2];

            return nameLength == groupNameLength && courseNumber is < courseNumberCheck5 and > courseNumberCheck0 && letterSymbol == facultySymbolM && numberSymbol == facultySymbol3;
        }
    }
}