using System;
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
            AllStudents = new List<Student>();

            if (!IsAllowed(name)) throw new IsuException("This group got wrong name");
        }

        public List<Student> StudentsList { get; set; }
        public List<Student> AllStudents { get; set; }

        public string GroupName { get; }
        private bool IsAllowed(string name)
        {
            return name.Length == 5 && name[2] < '5' && name[2] > '0';
        }
    }
}