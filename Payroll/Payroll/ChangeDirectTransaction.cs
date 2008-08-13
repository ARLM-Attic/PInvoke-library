using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class ChangeDirectTransaction : ChangeMethodTransaction
    {
        public ChangeDirectTransaction(int empid, string bank, string account)
            :base(empid)
        {
            this.bank = bank;
            this.account = account;
        }

        private string bank;
        private string account;

        protected override PaymentMethod MakeMethod()
        {
            return new DirectMethod(this.bank, this.account);
        }
    }
}
