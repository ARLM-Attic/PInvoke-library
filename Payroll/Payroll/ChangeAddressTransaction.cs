using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class ChangeAddressTransaction : ChangeEmployeeTransaction
    {
        public ChangeAddressTransaction(int empid, string newAddress)
            : base(empid)
        {
            this.newAddress = newAddress;
        }

        private string newAddress;

        protected override void Change(Employee e)
        {
            e.Address = newAddress;
        }
    }
}
