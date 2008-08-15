using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll
{
    public static class Util
    {
        /// <summary>
        /// Determine whether the date is the last day of the month.
        /// </summary>
        public static bool IsLastDayOfMonth(this DateTime date)
        {
            return date.Day == DateTime.DaysInMonth(date.Year, date.Month);
        }

        /// <summary>
        /// Determines whether the date is between two dates.
        /// </summary>
        /// <param name="begin">The begin date</param>
        /// <param name="end">The end date</param>
        /// <returns>True if the date is between the two dates and end > begin. </returns>
        public static bool Between(this DateTime thisDate, DateTime begin, DateTime end)
        {
            return (thisDate >= begin) && (thisDate <= end);
        }

        public static int NumberOfFridayBetweenDates(DateTime begin, DateTime end)
        {
            int count = 0;

            DateTime temp = begin;
            do
            {
                if (temp.DayOfWeek == DayOfWeek.Friday)
                    count++;
            } while ((temp = temp.AddDays(+1)) <= end);

            return count;
        }
    }
}
