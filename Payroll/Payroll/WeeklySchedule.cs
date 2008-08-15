using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class WeeklySchedule : PaymentSchedule
    {
        #region PaymentSchedule Members

        public bool IsPayDay(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Friday;
        }

        public DateTime GetPayBeginDate(DateTime date)
        {
            return date.AddDays(-6);
        }

        #endregion
    }
}
