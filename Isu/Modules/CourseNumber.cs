using System;

namespace Isu.Modules
{
    public class CourseNumber
    {
        private const char CourseNumberCheck0 = '0';
        private const char CourseNumberCheck5 = '5';
        public CourseNumber(char number)
        {
            if (!IsAllowedCourse(number))
            {
                throw new Exception("This group got wrong course number");
            }
            else
            {
                Number = number;
            }
        }

        public char Number { get; }

        private static bool IsAllowedCourse(char number)
        {
            return number is < CourseNumberCheck5 and > CourseNumberCheck0;
        }
    }
}