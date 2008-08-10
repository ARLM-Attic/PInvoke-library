using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class AddHourlyEmployee : AddEmployee
    {
        public AddHourlyEmployee(int id, string name, string address, double hourlyRate)
            :base(id,name,address)
        {
            this.hourlyRate = hourlyRate;
        }

        private double hourlyRate;


        protected override PaymentClassification MakeClassification()
        {
            return new HourlyClassification(hourlyRate);
        }

        protected override PaymentSchedule MakeSchedule()
        {
            return new WeeklySchedule();
        }
    }
}
