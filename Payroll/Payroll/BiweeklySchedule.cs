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

        #endregion
    }
}
