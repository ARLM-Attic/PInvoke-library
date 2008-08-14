using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll
{
    public class Paycheck
    {
        public double Gross { get; set; }
        public double  Deductions { get; set; }

        public double Net { get { return Gross - Deductions; } }

        public string GetField(string name)
        {
            throw new NotImplementedException();
        }
    }
}
