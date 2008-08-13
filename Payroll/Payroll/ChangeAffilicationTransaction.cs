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
            RecordMembership(e);
            e.Affiliation = MakeAffiliation();
        }

        protected virtual void RecordMembership(Employee e)
        {
            // TODO: It's bad to depend on UnionAffiliation here... 
            // Maybe I can move this to PayrollDatabase
            UnionAffiliation ua = e.Affiliation as UnionAffiliation;
            if (ua != null)
            {
                int memberId = ua.MemberId;
                PayrollDatabase.RemoveUnionMember(memberId);
            }
        }

        protected abstract Affiliation MakeAffiliation(); 
    }
}
