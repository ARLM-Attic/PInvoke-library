using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public abstract class ChangeAffilicationTransaction : ChangeEmployeeTransaction
    {
        protected ChangeAffilicationTransaction(int empid) : base(empid) { }

        protected override void Change(Employee e)
        {
            UnionAffiliation ua = e.Affiliation as UnionAffiliation;
            if (ua != null)
            {
                // TODO remove union member! It's bad to depend on UnionAffiliation here...
            }

            e.Affiliation = MakeAffiliation();
        }

        protected abstract Affiliation MakeAffiliation(); 
    }
}
