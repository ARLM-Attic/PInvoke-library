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
            return date.IsLastDayOfMonth();
        }

 

        #endregion

        #region PaymentSchedule Members


        public DateTime GetPayBeginDate(DateTime date)
        {
            return date.AddMonths(-1).AddDays(1);
        }

        #endregion
    }
}
