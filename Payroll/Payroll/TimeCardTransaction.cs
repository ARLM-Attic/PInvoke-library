using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll
{
   public  class TimeCardTransaction : Transaction
    {
       public TimeCardTransaction(int empid, DateTime date, TimeSpan time)
       {
           this.empid = empid;
           this.date = date;
           this.time = time;
       }

       private int empid;
       private DateTime date;
       private TimeSpan time;

       #region Transaction Members

       public void Execute()
       {
           Employee e = PayrollDatabase.GetEmployee(empid);

           HourlyClassification c = e.Classification as HourlyClassification;

           TimeCard card = new TimeCard(this.date, this.time);
           c.AddTimeCard(card);
       }

       #endregion
    }
}
