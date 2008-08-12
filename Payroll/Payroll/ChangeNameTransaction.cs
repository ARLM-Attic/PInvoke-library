using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class ChangeNameTransaction : ChangeEmployeeTransaction
    {        
        public ChangeNameTransaction(int empid, string newName)
            : base(empid)
        {
            this.newName = newName;
        }

        private string newName;

        protected override void Change(Employee e)
        {
            e.Name = newName;
        } 
    }
}
