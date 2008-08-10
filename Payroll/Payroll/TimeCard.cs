using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class TimeCard
    {
        public TimeCard(DateTime date, TimeSpan time)
        {
            this.date = date;
            this.time = time;
        }

        private DateTime date;
        public DateTime Date { get { return this.date; } }

        private TimeSpan time;
        public TimeSpan Time { get { return this.time; } }
    }
}
