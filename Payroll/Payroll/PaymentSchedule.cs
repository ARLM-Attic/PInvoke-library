﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public interface PaymentSchedule
    {
        bool IsPayDay(DateTime date);

        DateTime GetPayBeginDate(DateTime date);
    }
}
