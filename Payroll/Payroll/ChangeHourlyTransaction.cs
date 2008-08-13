using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class ChangeHourlyTransaction : ChangeClassificationTransaction
    {
        public ChangeHourlyTransaction(int empid, double hourlyRate)
            : base(empid)
        {
            this.hourlyRate = hourlyRate;
        }

        private double hourlyRate;

        protected override PaymentClassification MakeClassification()
        {
            return new HourlyClassification(this.hourlyRate);
        }

        protected override PaymentSchedule MakeSchedule()
        {
            return new WeeklySchedule();
        }
    }
}
