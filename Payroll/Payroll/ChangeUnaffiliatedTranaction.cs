using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class ChangeUnaffiliatedTranaction : ChangeAffilicationTransaction
    {
        public ChangeUnaffiliatedTranaction(int empid) : base(empid) { }

        protected override Affiliation MakeAffiliation()
        {
            return new NoAffiliation();
        }
    }
}
