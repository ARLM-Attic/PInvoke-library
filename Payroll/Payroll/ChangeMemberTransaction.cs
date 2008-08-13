using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class ChangeMemberTransaction : ChangeAffilicationTransaction
    {
        public ChangeMemberTransaction(int empid, int memberId, double dues)
            :base(empid)
        {
            this.memberId = memberId;
            this.dues = dues;
        }

        private int memberId;
        private double dues;

        protected override Affiliation MakeAffiliation()
        {
            return new UnionAffiliation(memberId, dues);
        }

        protected override void RecordMembership(Employee e)
        {
            base.RecordMembership(e);

            PayrollDatabase.AddUnionMember(memberId, e);
        }
    }
}
