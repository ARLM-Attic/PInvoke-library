using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public abstract class ChangeEmployeeTransaction : Transaction
    {
        private int empid;

        protected ChangeEmployeeTransaction(int empid)
        {
            this.empid = empid;
        }

        #region Transaction Members

        public void Execute()
        {
            Employee e = PayrollDatabase.GetEmployee(empid);
            Change(e);
        }

        #endregion

        protected abstract void Change(Employee e);
    }
}
