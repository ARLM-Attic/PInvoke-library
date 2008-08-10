using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class AddCommissionedEmployee : AddEmployee
    {
        public AddCommissionedEmployee(int id, string name, string address, double salary, double commissionRate)
            : base(id, name, address)
        {
            this.salary = salary;
            this.commissionRate = commissionRate;
        }

        private double salary;
        private double commissionRate;


        protected override PaymentClassification MakeClassification()
        {
            return new CommissionedClassification(salary, commissionRate);
        }

        protected override PaymentSchedule MakeSchedule()
        {
            return new BiweeklySchedule();
        }
    }
}
