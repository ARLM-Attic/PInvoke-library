using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class NoAffiliation : Affiliation
    {
        #region Affiliation Members

        public void CalculateDeductions(Paycheck pc)
        {
            pc.Deductions = 0.0;
        }

        #endregion
    }
}
