using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class DirectMethod : PaymentMethod
    {
        public DirectMethod(string bank, string account)
        {
            this.bank = bank;
            this.account = account;
        }

        private string bank;
        public string Bank
        {
            get
            {
                return bank;
            }
        }

        private string account;
        public string Account
        {
            get
            {
                return account;
            }
        }
    }
}
