using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Modules
{
    public class Group
    {
        public Group(string name)
        {
            GroupName = name;
            StudentsList = new List<Student>();

            if (!IsAllowed(name)) throw new IsuException("This group got wrong name");
        }

        public List<Student> StudentsList { get; set; }

        public string GroupName { get; }
        private static bool IsAllowed(string name)
        {
            int groupNameLength = name.Length;
            return groupNameLength == 5 && name[2] < '5' && name[2] > '0' && name[0] == 'M' && name[1] == '3';
        }
    }
}