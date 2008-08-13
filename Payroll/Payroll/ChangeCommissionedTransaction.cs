using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class ChangeCommissionedTransaction : ChangeClassificationTransaction
    {
        public ChangeCommissionedTransaction(int empid, double salary, double commissionRate)
            : base(empid)
        {
            this.salary = salary;
            this.commissionRate = commissionRate;
        }

        private double salary;
        private double commissionRate;

        protected override PaymentClassification MakeClassification()
        {
            return new CommissionedClassification(this.salary, this.commissionRate);
        }

        protected override PaymentSchedule MakeSchedule()
        {
            return new BiweeklySchedule();
        }
    }
}
