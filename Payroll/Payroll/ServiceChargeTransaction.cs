using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll
{
    public class ServiceChargeTransaction:Transaction
    {
        private int memberId;
        private DateTime date;
        private double amount;

        public ServiceChargeTransaction(int memberId, DateTime date, double amount)
        {
            this.memberId = memberId;
            this.date = date;
            this.amount = amount;
        }

        #region Transaction Members

        public void Execute()
        {
            Employee e = PayrollDatabase.GetUnionMember(memberId);

            if (e == null)
                throw new Exception();

            UnionAffiliation affiliation = e.Affiliation as UnionAffiliation;
            if (affiliation == null)
                throw new Exception();

            ServiceCharge charge = new ServiceCharge(date, amount);
            affiliation.AddServiceCharge(charge);
        }

        #endregion
    }
}
