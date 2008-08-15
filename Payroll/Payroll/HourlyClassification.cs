using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class HourlyClassification : PaymentClassification
    {
        public HourlyClassification(double hourlyRate)
        {
            this.hourlyRate = hourlyRate;
        }

        private double hourlyRate;
        public double HourlyRate
        {
            get
            {
                return hourlyRate;
            }
        }

        #region TODO: redirect to database
        //TODO: Redirect to database

        private Dictionary<DateTime, TimeCard> timeCards = new Dictionary<DateTime,TimeCard>();

        public TimeCard GetTimeCard(DateTime date)
        {
            return timeCards[date];
        }

        internal void AddTimeCard(TimeCard card)
        {
            timeCards.Add(card.Date, card);
        }

        #endregion

        internal override double CalculatePay(Paycheck pc)
        {
            double grossPay = 0;

            foreach (TimeCard card in timeCards.Values)
            {
                if (card.Date.Between(pc.BeginDate, pc.Date))
                {
                    grossPay += PayTimeCard(card);
                }
            }

            return grossPay;
        }

        private double PayTimeCard(TimeCard card)
        {
            double overtimeRate = 1.5;

            TimeSpan dayHours = new TimeSpan(8, 0, 0);
            if (card.Time <= dayHours)
                return hourlyRate * card.Time.TotalHours;
            else if (card.Time < new TimeSpan(24, 0, 0))
                return hourlyRate * card.Time.TotalHours + card.Time.Add(-dayHours).TotalHours * (overtimeRate - 1);
            else
                throw new ArgumentException();            
        }
    }
}
