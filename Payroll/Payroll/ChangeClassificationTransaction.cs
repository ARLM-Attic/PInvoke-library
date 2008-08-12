using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public abstract class ChangeClassificationTransaction : ChangeEmployeeTransaction
    {
        protected ChangeClassificationTransaction(int empid) : base(empid) { }
        protected override void Change(Employee e)
        {
            e.Classification = MakeClassification();
            e.Schedule = MakeSchedule();
        }

        protected abstract PaymentClassification MakeClassification();
        protected abstract PaymentSchedule MakeSchedule();
    }
}
