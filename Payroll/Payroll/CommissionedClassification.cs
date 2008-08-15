using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class CommissionedClassification : PaymentClassification
    {
        public CommissionedClassification(double salary, double commissionRate)
        {
            this.salary = salary;
            this.commissionRate = commissionRate;
        }

        private double salary;
        public double Salary
        {
            get
            {
                return salary;
            }
        }

        private double commissionRate;
        public double CommissionRate
        {
            get
            {
                return commissionRate;
            }
        }

        #region 
        //TODO: Redirect to database
        private Dictionary<DateTime, SalesReceipt> receipts = new Dictionary<DateTime, SalesReceipt>();

        public void AddSalesReceipt(SalesReceipt receipt)
        {
            receipts.Add(receipt.Date, receipt);
        }

        public SalesReceipt GetSalesReceipt(DateTime date)
        {
            return receipts[date];
        }

        #endregion

        internal override double CalculatePay(Paycheck pc)
        {
            throw new NotImplementedException();
        }
    }
}
