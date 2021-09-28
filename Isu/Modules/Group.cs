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
        private static bool IsAllowed(string name)
        {
            int groupNameLength = name.Length;
            char letterSymbol = name[0];
            char numberSymbol = name[1];
            char courseNumber = name[2];

            return groupNameLength == 5 && courseNumber < '5' && courseNumber > '0' && letterSymbol == 'M' && numberSymbol == '3';
        }
    }
}