using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class MailMethod : PaymentMethod
    {
        public MailMethod(string address)
        {
            this.address = address;
        }

        private string address;
        public string Address
        {
            get
            {
                return address;
            }
        }
    }
}
