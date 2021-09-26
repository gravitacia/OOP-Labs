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
       private int _studentsCounter = 100000;
       private int _studentsInGroup;

       public Group AddGroup(string name)
       {
           var group = new Group(name);
           _groups.Add(group);
           return group;
       }

       public Student AddStudent(Group group, string name)
       {
           var student = new Student(name, _studentsCounter++, group.GroupName);
           if (_studentsInGroup < 20)
           {
               foreach (Group curGroup in _groups.Where(curGroup => curGroup == @group))
               {
                   curGroup.StudentsList.Add(student);
               }

               _studentsInGroup++;
           }
           else
           {
               throw new IsuException("Too many students");
           }

           if (_studentsInGroup > 20)
           {
               _studentsInGroup = 0;
           }

           return student;
       }

       public Student GetStudent(int id)
       {
           if (id > _studentsCounter | id < 99999 && id > 0)
           {
               throw new Exception("WARNING! Student not found.");
           }
           else
           {
               return _groups.SelectMany(curGroup => curGroup.StudentsList).FirstOrDefault(student => student.Id == id);
           }
       }

       public Student FindStudent(string name)
       {
           return _groups.SelectMany(curGroup => curGroup.StudentsList).FirstOrDefault(curStudent => curStudent.Name == name);
       }

       public List<Student> FindStudents(string groupName)
       {
           return _groups.Where(curGroup => curGroup.GroupName == groupName).Select(curGroup => curGroup.StudentsList).FirstOrDefault();
       }

       public List<Student> FindStudents(CourseNumber courseNumber)
       {
           var allStudents = new List<Student>();
           foreach (Group curGroup in _groups.Where(curGroup => curGroup.GroupName[2] == courseNumber.Number))
           {
               allStudents.AddRange(curGroup.StudentsList);
           }

           return allStudents;
       }

       public Group FindGroup(string groupName)
       {
           return _groups.FirstOrDefault(curGroup => curGroup.GroupName == groupName);
       }

       public List<Group> FindGroups(CourseNumber courseNumber)
       {
           return _groups.Where(curGroup => curGroup.GroupName[2] == courseNumber.Number).ToList();
       }

       public void ChangeStudentGroup(Student student, Group newGroup)
       {
           foreach (Group curGroup in _groups)
           {
               curGroup.StudentsList.Remove(student);
               AddStudent(newGroup, student.Name);
           }
       }
    }
}