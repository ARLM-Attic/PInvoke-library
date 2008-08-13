using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public abstract class ChangeMethodTransaction : ChangeEmployeeTransaction
    {
        protected ChangeMethodTransaction(int empid) : base(empid) { }

        protected override void Change(Employee e)
        {
            e.Method = MakeMethod();
        }

        protected abstract PaymentMethod MakeMethod();
    }
}
