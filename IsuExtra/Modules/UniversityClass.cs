using Isu.Modules;

namespace IsuExtra.Modules
{
    public class UniversityClass
    {
        public UniversityClass(Group groupName, string time, string teacherName, int audienceNumber, string day)
        {
            GroupName = groupName.GroupName;
            Time = time;
            TeacherName = teacherName;
            AudienceNumber = audienceNumber;
            Day = day;
        }

        public string Day { get; set; }
        public int AudienceNumber { get; set; }

        public string TeacherName { get; set; }

        public string Time { get; set; }

        public string GroupName { get; set; }
    }
}