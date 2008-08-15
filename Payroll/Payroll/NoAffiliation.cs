using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class NoAffiliation : Affiliation
    {
        #region Affiliation Members

        public double CalculateDeductions(Paycheck pc)
        {
            return 0.0;
        }

        #endregion
    }
}
