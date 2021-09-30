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
           if (group.StudentsInGroup < 20)
           {
               foreach (Group curGroup in _groups.Where(curGroup => curGroup == @group))
               {
                   curGroup.StudentsList.Add(student);
               }

               group.StudentsInGroup++;
           }
           else
           {
               throw new IsuException("Too many students");
           }

           if (group.StudentsInGroup > 20)
           {
               group.StudentsInGroup = 0;
           }

           return student;
       }

       public Student GetStudent(int id)
       {
           const int wrongStudentIdMin = 0;
           const int wrongStudentIdMax = 99999;
           if (id > _studentsIsuNumber | id < wrongStudentIdMax && id > wrongStudentIdMin)
           {
               throw new Exception("WARNING! Wrong Id!");
           }

           foreach (Student student in from curGroup in _groups from student in curGroup.StudentsList where student.Id == id select student)
           {
               return student;
           }

           throw new Exception("WARNING! Student not found.");
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