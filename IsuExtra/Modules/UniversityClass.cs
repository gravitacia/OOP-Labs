using Isu.Modules;

namespace IsuExtra.Modules
{
    public class UniversityClass
    {
        public UniversityClass(Group groupName, string time, string teacherName, int audienceNumber)
        {
            GroupName = groupName.GroupName;
            Time = time;
            TeacherName = teacherName;
            AudienceNumber = audienceNumber;
        }

        public int AudienceNumber { get; set; }

        public string TeacherName { get; set; }

        public string Time { get; set; }

        public string GroupName { get; set; }
    }
}