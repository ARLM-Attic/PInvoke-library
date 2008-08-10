using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public abstract class AddEmployee : Transaction
    {
        private int employeeId;

        private string name;

        private string address;

        protected AddEmployee(int id, string name, string address)
        {
            employeeId = id;
            this.name = name;
            this.address = address;
        }

        protected abstract PaymentClassification MakeClassification();

        protected abstract PaymentSchedule MakeSchedule();

        #region Transaction Members

        public void Execute()
        {
            Employee e = new Employee(employeeId, name, address);

            e.Classification = MakeClassification();
            e.Schedule = MakeSchedule();
            e.Method = new HoldMethod();

            PayrollDatabase.AddEmployee(employeeId, e);
        }

        #endregion
    }
}
