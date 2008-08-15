using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class UnionAffiliation : Affiliation
    {
        public UnionAffiliation(int memberId, double dues)
        {
            this.memberId = memberId;
            this.dues = dues;
        }

        private int memberId;
        public int MemberId { get { return memberId; } }

        private double dues;
        public double Dues { get { return dues; } }

        #region
        //TODO: Direct to Database?
        private Dictionary<DateTime, ServiceCharge> charges = new Dictionary<DateTime, ServiceCharge>();

        internal void AddServiceCharge(ServiceCharge charge)
        {
            charges.Add(charge.Date, charge);
        }

        public ServiceCharge GetServiceCharge(DateTime date)
        {
            return charges[date];
        }
        #endregion

        #region Affiliation Members

        public double CalculateDeductions(Paycheck pc)
        {
            double deductions = 0;

            int numberOfFriday = Util.NumberOfFridayBetweenDates(pc.BeginDate, pc.Date);
            deductions += numberOfFriday * dues;

            foreach (ServiceCharge charge in charges.Values)
            {
                if (charge.Date.Between(pc.BeginDate, pc.Date))
                    deductions += charge.Amount;
            }

            return deductions;
        }

        #endregion
    }
}
