using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Modules;
using IsuExtra.Modules;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuExtraService : IIsuExtraService
    {
        private List<UltimateCourse> _courses = new List<UltimateCourse>();
        private Dictionary<string, Schedule> _schedules = new Dictionary<string, Schedule>();

        public UltimateCourse AddUltimateCourse(string courseName)
        {
            if (_courses.Exists(curCourse => curCourse.Name.Equals(courseName)))
            {
                throw new Exception("Warning! This Ultimate course already exist!");
            }

            var course = new UltimateCourse(courseName, courseName[0]);
            _courses.Add(course);

            return course;
        }

        public Student AddStudentToCourse(Student student, string courseName, StudyGroup studyGroup, Schedule schedule)
        {
            UltimateCourse course = _courses.FirstOrDefault(curCourse => curCourse.Name.Equals(courseName));

            if (course != null) course.AddStudentToUltimateCourse(student, studyGroup, schedule);
            return student;
        }

        public void RemoveStudentFromCourse(Student student, string courseName, string streamName)
        {
            UltimateCourse course = _courses.FirstOrDefault(curCourse => curCourse.Name.Equals(courseName));

            if (course != null) course.RemoveStudent(student, course.GetStream(streamName));
        }

        public Schedule AddGroupSchedule(Group group, Schedule schedule)
        {
            if (_schedules.ContainsKey(group.GroupName))
            {
                throw new Exception("Schedule of this group is already exists!");
            }

            _schedules.Add(group.GroupName, schedule);
            return _schedules[group.GroupName];
        }

        public Schedule GetGroupSchedule(Group group)
        {
            if (!_schedules.ContainsKey(group.GroupName))
            {
                throw new Exception("Warning! Schedules doesn't exist!");
            }

            return _schedules[group.GroupName];
        }

        public List<Student> GetStudentsFromStream(string courseName, string streamName)
        {
            UltimateCourse course = _courses.FirstOrDefault(curCourse => curCourse.Name.Equals(courseName));

            if (course == null) throw new Exception("Warning!");
            StudyStream stream = course.GetStream(streamName);

            return stream.GetStudentsFromStream();
        }

        public void RemoveGroupSchedule(Group group)
        {
            bool result = _schedules.Remove(group.GroupName);
            if (!result)
            {
                throw new Exception("Schedule of this group doesn't exists!");
            }
        }

        public string GetStudentFromStudyGroup(StudyGroup studyGroup, string studentName)
        {
            foreach (Student curStudent in studyGroup.RefGroup.GetStudentsList().Where(curStudent => curStudent.Name == studentName))
            {
                return curStudent.Name;
            }

            throw new Exception("Student not found!");
        }

        public bool ScheduleValidation(Schedule ultimateSchedule, Schedule simpSchedule)
        {
            if (ultimateSchedule.GetClassesList()
                .SelectMany(
                    curClass => simpSchedule.GetClassesList(),
                    (curClass, curSimpClass) => new { curClass, curSimpClass })
                .Where(@t => @t.curSimpClass.Time != @t.curClass.Time)
                .Select(@t => @t.curClass).Any())
            {
                return true;
            }

            throw new Exception("You cant choose this Ultimate Course!");
        }

        public bool AppoinmentValidation(StudyStream studyStream)
        {
            if (studyStream.GetStudentsFromStream().Any(curStudent => studyStream.GetStudentsFromStream().Contains(curStudent)))
            {
                return true;
            }

            throw new Exception("Students don't exist in this course!");
        }

        public StudyStream AddStudyStream(string courseName, string studyStreamName, int size)
        {
            foreach (UltimateCourse curCourse in _courses.Where(curCourse => curCourse.Name == courseName))
            {
                return curCourse.AddStudyStream(studyStreamName, size);
            }

            throw new Exception("Warning! This stream is already exist!");
        }

        public void AddStudyGroup(StudyStream studyStream, StudyGroup studyGroup)
        {
            studyStream.AddStudyGroup(studyGroup);
        }
    }
}