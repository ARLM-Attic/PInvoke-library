using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public abstract class PaymentClassification
    {
        internal abstract void CalculatePay(Paycheck pc);
    }
}
