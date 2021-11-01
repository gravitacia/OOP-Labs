using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Isu;
using Isu.Modules;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Modules;
using IsuExtra.Services;
using IsuExtra.Tools;

namespace IsuExtra.Tools
{
    public interface IIsuExtraService
    {
        UltimateCourse AddUltimateCourse(string courseName);
        Student AddStudentToCourse(Student student, string courseName, StudyGroup studyGroup, Schedule schedule);
        void RemoveStudentFromCourse(Student student, string courseName, string streamName);
        Schedule AddGroupSchedule(Group group, Schedule schedule);
        Schedule GetGroupSchedule(Group group);
        List<Student> GetStudentsFromStream(string courseName, string streamName);
        void RemoveGroupSchedule(Group group);
        string GetStudentFromStudyGroup(StudyGroup studyGroup, string studentName);
        bool ScheduleValidation(Schedule ultimateSchedule, Schedule simpSchedule);
        bool AppoinmentValidation(StudyStream studyStream);
        StudyStream AddStudyStream(string courseName, string studyStreamName, int size);
        void AddStudyGroup(StudyStream studyStream, StudyGroup studyGroup);
    }
}