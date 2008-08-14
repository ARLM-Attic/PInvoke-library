using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public interface Affiliation
    {
        void CalculateDeductions(Paycheck pc);
    }
}
