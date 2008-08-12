using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class ChangeSalariedTransaction : ChangeClassificationTransaction
    {
        public ChangeSalariedTransaction(int empid, double salary)
            : base(empid)
        {
            this.salary = salary;
        }

        private double salary;

        protected override PaymentClassification MakeClassification()
        {
            return new SalariedClassification(this.salary);
        }

        protected override PaymentSchedule MakeSchedule()
        {
            return new MonthlySchedule();
        }
    }
}
