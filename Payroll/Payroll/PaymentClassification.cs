using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public abstract class PaymentClassification
    {
        internal abstract double CalculatePay(Paycheck pc);
    }
}
