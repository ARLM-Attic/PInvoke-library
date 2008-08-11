using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class ServiceCharge
    {
        public ServiceCharge(DateTime date, double amount)
        {
            this.date = date;
            this.amount = amount;
        }

        private DateTime date;
        public DateTime Date { get { return this.date; } }

        private double amount;
        public double Amount { get { return this.amount; } }
    }
}
