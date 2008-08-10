using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll
{
    public class DeleteEmployee : Transaction
    {
        public DeleteEmployee(int id)
        {
            this.id = id;
        }

        private int id;        

        #region Transaction Members

        public void Execute()
        {
            PayrollDatabase.DeleteEmployee(id);
        }

        #endregion
    }
}
