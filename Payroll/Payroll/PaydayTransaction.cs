using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll
{
   public class PaydayTransaction:Transaction
    {
        public PaydayTransaction(DateTime date)
        {
            this.date = date;
        }

        private DateTime date;
        private Dictionary<int, Paycheck> paychecks = new Dictionary<int, Paycheck>();

        public Paycheck GetPaycheck(int empid)
        {
            throw new NotImplementedException();
        }

        #region Transaction Members

        public void Execute()
        {
            foreach (Employee e in PayrollDatabase.GetEmployees())
            {
                if (e.IsPayDay(date))
                {
                    Paycheck pc = e.Payday(date);
                    paychecks.Add(e.Id, pc);
                }
            }
        }

        #endregion
    }
}
