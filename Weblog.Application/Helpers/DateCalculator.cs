using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Helpers
{
    public static class DateCalculator
    {
        public static string ToShamsi(this DateTimeOffset dateTimeOffset)
        {
            if (dateTimeOffset.UtcTicks <= 0)
            {
                return "";
            }
            DateTime dateTime = dateTimeOffset.DateTime;
            PersianCalendar persianCalendar = new PersianCalendar();
            int year = persianCalendar.GetYear(dateTime);
            int month = persianCalendar.GetMonth(dateTime);
            int day = persianCalendar.GetDayOfMonth(dateTime);
            int hour = persianCalendar.GetHour(dateTime);
            int minute = persianCalendar.GetMinute(dateTime);
            int second = persianCalendar.GetSecond(dateTime);

            return $"{year}/{month}/{day} {hour}:{minute}:{second}";
        }
    }
}