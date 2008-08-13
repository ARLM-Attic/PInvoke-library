using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class ChangeMailTransaction: ChangeMethodTransaction
    {
        public ChangeMailTransaction(int empid, string address)
            :base(empid)
        {
            this.address = address;
        }

        private string address;

        protected override PaymentMethod MakeMethod()
        {
            return new MailMethod(address);
        }
    }
}
