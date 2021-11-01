using NUnit.Framework;
using Isu.Modules;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Modules;
using IsuExtra.Services;
using IsuExtra.Tools;


namespace IsuExtra.Tests
{
    public class IsuExtraTest
    {
        private IIsuExtraService _isuExtraService;
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuExtraService = new IsuExtraService();
            _isuService = new IsuService();
        }

        [Test]
        public void AddUltimateCourseToIsu()
        {
            UltimateCourse ultimateCourse = _isuExtraService.AddUltimateCourse("R3510");
            
            Assert.Contains(ultimateCourse.Name, new[] {"R3510"});
        }

        [Test]
        public void AddStudentToUltimateCourse_UltimateCourseHasStudent()
        {
            UltimateCourse ultimateCourse = _isuExtraService.AddUltimateCourse("Cool");
            
            var studentGroup = new Group("M3210");
            Student student1 = _isuService.AddStudent(studentGroup, "AK");
            
            var ultimateGroup = new Group("K1111");
            var universityClass = new UniversityClass(ultimateGroup, "8:20-9:50", "AA", 302);
            var ultimateSchedule = new Schedule();
            
            ultimateSchedule.AddClassToSchedule(universityClass);
            var studyGroup = new StudyGroup(ultimateGroup, ultimateSchedule, 20);
            StudyStream studyStream = _isuExtraService.AddStudyStream(ultimateCourse.Name, "KKKK", 20);


            _isuExtraService.AddStudyGroup(studyStream, studyGroup);

            _isuExtraService.AddStudentToCourse(student1, "Cool", studyGroup, ultimateSchedule);

            
            Assert.Contains(_isuExtraService.GetStudentFromStudyGroup(studyStream.FindStudyGroup(ultimateGroup.GroupName), "AK"), new[] {"AK"});
        }

        [Test]
        public void RemoveStudentFromUltimateCourse()
        {
            UltimateCourse ultimateCourse = _isuExtraService.AddUltimateCourse("Cool");
            
            var studentGroup = new Group("M3210");
            Student student1 = _isuService.AddStudent(studentGroup, "AK");
            var ultimateGroup = new Group("K1111");
            var universityClass = new UniversityClass(ultimateGroup, "8:20-9:50", "AA", 302);
            var ultimateSchedule = new Schedule();
            
            ultimateSchedule.AddClassToSchedule(universityClass);
            var studyGroup = new StudyGroup(ultimateGroup, ultimateSchedule, 20);
            StudyStream studyStream = _isuExtraService.AddStudyStream(ultimateCourse.Name, "KKKK", 20);


            _isuExtraService.AddStudyGroup(studyStream, studyGroup);

            _isuExtraService.AddStudentToCourse(student1, "Cool", studyGroup, ultimateSchedule);
            _isuExtraService.RemoveStudentFromCourse(student1, ultimateCourse.Name, studyStream.Name);
            
            Assert.True(!(_isuExtraService.GetStudentsFromStream(ultimateCourse.Name, studyStream.Name)).Contains(student1));
            
        }

        [Test]
        public void ScheduleValidation_ThereIsNoIntersection()
        {
            var ultimateGroup = new Group("K1111");
            
            var universityClass = new UniversityClass(ultimateGroup, "8:20-9:50", "AA", 302);
            var universitySimpClass = new UniversityClass(ultimateGroup, "10:00-11:30", "AA", 302);
            var ultimateSchedule = new Schedule();
            var simpSchedule = new Schedule();
            
            ultimateSchedule.AddClassToSchedule(universityClass);
            simpSchedule.AddClassToSchedule(universitySimpClass);
            
            Assert.True(_isuExtraService.ScheduleValidation(ultimateSchedule, simpSchedule));
        }

        [Test]
        public void StudentsWithoutAnAppointment()
        {
            UltimateCourse ultimateCourse = _isuExtraService.AddUltimateCourse("Cool");
            
            var studentGroup = new Group("M3210");
            Student student1 = _isuService.AddStudent(studentGroup, "AK");
            Student student2 = _isuService.AddStudent(studentGroup, "BA");
            Student student3 = _isuService.AddStudent(studentGroup, "AB");
            Student student4 = _isuService.AddStudent(studentGroup, "AS");
            
            var ultimateGroup = new Group("K1111");
            var universityClass = new UniversityClass(ultimateGroup, "8:20-9:50", "AA", 302);
            var ultimateSchedule = new Schedule();
            
            ultimateSchedule.AddClassToSchedule(universityClass);
            var studyGroup = new StudyGroup(ultimateGroup, ultimateSchedule, 20);
            StudyStream studyStream = _isuExtraService.AddStudyStream(ultimateCourse.Name, "KKKK", 20);


            _isuExtraService.AddStudyGroup(studyStream, studyGroup);

            _isuExtraService.AddStudentToCourse(student1, "Cool", studyGroup, ultimateSchedule);
            _isuExtraService.AddStudentToCourse(student2, "Cool", studyGroup, ultimateSchedule);
            _isuExtraService.AddStudentToCourse(student3, "Cool", studyGroup, ultimateSchedule);
            _isuExtraService.AddStudentToCourse(student4, "Cool", studyGroup, ultimateSchedule);
            Assert.True(_isuExtraService.AppoinmentValidation(studyStream));

        }
    }
}