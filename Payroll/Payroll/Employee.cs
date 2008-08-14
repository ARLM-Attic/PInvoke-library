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

        internal bool IsPayDay(DateTime date)
        {
            return this.schedule.IsPayDay(date);
        }

        internal Paycheck Payday(DateTime dateTime)
        {
            Paycheck pc = new Paycheck();

            this.classification.CalculatePay(pc);
            this.affiliation.CalculateDeductions(pc);

            this.Method.Pay(pc);

            return pc;
        }
    }
}
