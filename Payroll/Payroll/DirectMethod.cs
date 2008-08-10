using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class DirectMethod : PaymentMethod
    {
        public string Bank
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public string Account
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
