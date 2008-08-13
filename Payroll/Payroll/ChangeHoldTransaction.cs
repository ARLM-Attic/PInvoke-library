using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class ChangeHoldTransaction: ChangeMethodTransaction
    {
        public ChangeHoldTransaction(int empid) : base(empid) { }

        protected override PaymentMethod MakeMethod()
        {
            return new HoldMethod();
        }
    }
}
