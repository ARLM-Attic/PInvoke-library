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
    }
}
