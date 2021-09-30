using System;

namespace Isu.Modules
{
    public class CourseNumber
    {
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
            const char courseNumberCheck0 = '0';
            const char courseNumberCheck5 = '5';

            return number is < courseNumberCheck5 and > courseNumberCheck0;
        }
    }
}