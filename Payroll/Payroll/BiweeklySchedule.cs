using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class BiweeklySchedule : PaymentSchedule
    {
        #region PaymentSchedule Members

        public bool IsPayDay(DateTime date)
        {
            throw new NotImplementedException();
        }

        public DateTime GetPayBeginDate(DateTime date)
        {
            return date.AddDays(-13);
        }

        #endregion
    }
}
