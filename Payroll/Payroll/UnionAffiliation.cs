using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class UnionAffiliation : Affiliation
    {
        private Dictionary<DateTime, ServiceCharge> charges = new Dictionary<DateTime, ServiceCharge>();

        //TODO: Direct to Database?
        internal void AddServiceCharge(ServiceCharge charge)
        {
            charges.Add(charge.Date, charge);
        }

        public ServiceCharge GetServiceCharge(DateTime date)
        {
            return charges[date];
        }
    }
}
