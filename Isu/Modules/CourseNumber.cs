using System;

namespace Isu.Modules
{
    public class CourseNumber
    {
        public CourseNumber(char number)
        {
            Number = number;
            if (!IsAllowedCourse(number)) throw new Exception("This group got wrong course number");
        }

        public char Number { get; }

        private static bool IsAllowedCourse(char number)
        {
            return number is < '5' and > '0';
        }
    }
}