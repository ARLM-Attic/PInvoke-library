using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class SalariedClassification : PaymentClassification
    {
        public SalariedClassification(double salary)
        {
            this.salary = salary;
        }

        private double salary;
        public double Salary
        {
            get
            {
                return salary;
            }
        }

        internal override double CalculatePay(Paycheck pc)
        {
            return this.salary;
        }
    }
}
