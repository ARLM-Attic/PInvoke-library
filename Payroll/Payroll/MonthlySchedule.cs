using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class MonthlySchedule : PaymentSchedule
    {
        #region PaymentSchedule Members

        public bool IsPayDay(DateTime date)
        {
            return IsLastDayOfMoth(date);
        }

        private bool IsLastDayOfMoth(DateTime date)
        {
            return date.Day == DateTime.DaysInMonth(date.Year,date.Month);
        }

        #endregion
    }
}
