using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll
{
    public class SalesReceiptTransaction : Transaction
    {
        public SalesReceiptTransaction(int empid, DateTime date, double amount)
        {
            this.empid = empid;
            this.date = date;
            this.amount = amount;
        }

        private int empid;
        private DateTime date;
        private double amount;

        #region Transaction Members

        public void Execute()
        {
            Employee e = PayrollDatabase.GetEmployee(this.empid);

            CommissionedClassification c = e.Classification as CommissionedClassification;

            SalesReceipt receipt = new SalesReceipt(this.date, this.amount);

            c.AddSalesReceipt(receipt);
        }

        #endregion
    }
}
