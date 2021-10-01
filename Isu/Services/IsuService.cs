using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Modules;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly List<Group> _groups = new List<Group>();
        private int _studentsIsuNumber = 100000;

        public Group AddGroup(string name)
       {
           var group = new Group(name);
           _groups.Add(group);
           return group;
       }

        public Student AddStudent(Group group, string name)
       {
           var student = new Student(name, _studentsIsuNumber++, group.GroupName);
           foreach (Group curGroup in _groups)
           {
               if (curGroup == @group)
               {
                   curGroup.AddStudentToGroup(student);
               }
           }

           return student;
       }

        public Student GetStudent(int id)
       {
           const int wrongStudentIdMax = 99999;
           if (!(id <= _studentsIsuNumber | id > wrongStudentIdMax))
               throw new Exception("WARNING! Student doesn't exist.");

           foreach (Student student in _groups.Select(curGroup => curGroup.GetStudentWithId(id)).Where(student => student != null))
           {
               return student;
           }

           throw new Exception("WARNING! Student doesn't exist.");
       }

        public Student FindStudent(string name)
        {
            foreach (Student student in _groups.Select(curGroup => curGroup.GetStudentWithName(name)).Where(student => student != null))
            {
                return student;
            }

            throw new IsuException("Student not found!");
        }

        public List<Student> FindStudents(string groupName)
        {
            foreach (Group curGroup in _groups.Where(curGroup => curGroup.GroupName == groupName))
            {
                return curGroup.GetStudentsList();
            }

            throw new IsuException("Students list not found");
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
           {
               var allStudents = new List<Student>();
               foreach (Group curGroup in _groups.Where(curGroup => curGroup.GroupName[2] == courseNumber.Number))
               {
                   allStudents.AddRange(curGroup.GetAllStudentFromGroup());
               }

               return allStudents;
           }

        public Group FindGroup(string groupName)
        {
            foreach (Group curGroup in _groups.Where(curGroup => curGroup.GroupName == groupName))
            {
                return curGroup;
            }

            throw new IsuException();
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
           {
               return _groups.Where(curGroup => curGroup.GroupName[2] == courseNumber.Number).ToList();
           }

        public void ChangeStudentGroup(Student student, Group newGroup)
           {
               foreach (Group curGroup in _groups)
               {
                   if (student.GroupName == curGroup.GroupName)
                   {
                       curGroup.RemoveStudentFromGroup(student);
                   }

                   AddStudent(newGroup, student.Name);
               }
           }
    }
}