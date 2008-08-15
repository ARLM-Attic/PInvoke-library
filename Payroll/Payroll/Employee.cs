using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class Employee
    {
        public Employee(int id, string name, string address)
        {
            this.id = id;
            this.name = name;
            this.address = address;
        }

        private int id;
        public int Id
        {
            get { return this.id; }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private PaymentClassification classification;
        public PaymentClassification Classification
        {
            get
            {
                return classification;
            }
            set
            {
                classification = value ;
            }
        }

        private PaymentMethod method;
        public PaymentMethod Method
        {
            get
            {
                return method;
            }
            set
            {
                method = value;
            }
        }

        private PaymentSchedule schedule;
        public PaymentSchedule Schedule
        {
            get
            {
                return schedule;
            }
            set
            {
                schedule = value;
            }
        }

        private Affiliation affiliation = new NoAffiliation();
        public Affiliation Affiliation
        {
            get
            {
                return affiliation;
            }
            set
            {
                this.affiliation = value;
            }
        }

        public bool IsPayDay(DateTime date)
        {
            return this.schedule.IsPayDay(date);
        }

        internal Paycheck Payday(DateTime date)
        {
             DateTime beginDate = GetPayBeginDate(date);
            Paycheck pc = new Paycheck(date, beginDate);

            double grossPay = this.classification.CalculatePay(pc);
            double deductions =  this.affiliation.CalculateDeductions(pc);

            this.Method.Pay(pc);

            pc.Gross = grossPay;
            pc.Deductions = deductions;

            return pc;
        }

        private DateTime GetPayBeginDate(DateTime date)
        {
            return this.schedule.GetPayBeginDate(date);
        }
    }
}
