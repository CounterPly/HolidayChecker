using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using OutSystems.HubEdition.RuntimePlatform;
using OutSystems.RuntimePublic.Db;

namespace OutSystems.NssHolidayChecker {

	public class CssHolidayChecker: IssHolidayChecker {

        /// <summary>
        /// C#/.NET Extension method to test whether or not a given business day is an observed US Holiday.
        ///Supported Holidays:
        /// 
        /// Fixed Holidays
        /// New Year's Day (January 1)
        /// Independence Day (July 4)
        /// Veterans Day (November 11)
        /// Christmas Day (December 25)
        /// 
        /// 
        /// Variable Holidays
        /// Martin Luther King Day (third Monday in January)
        /// President's Day (third Monday in February)
        /// Memorial Day  (last Monday in May)
        /// Labor Day (first Monday in September)
        /// Columbus Day (second Monday in October)
        /// Thanksgiving Day (fourth Thursday in November)
        /// 
        /// 
        /// Rule-based so it works for any year.
        /// </summary>
        /// <param name="ssdateToCheck"></param>
        /// <param name="ssresult"></param>
        public void MssIsHoliday(DateTime ssdateToCheck, bool ssresult)
        {
            ssresult = false;
            ssresult = IsHoliday(ssdateToCheck);
        } // MssIsHoliday

        private static bool IsHoliday(DateTime date)
        {
            var holidays = new HashSet<DateTime>();
            var year = date.Year;

            // New Year's Day - fixed date
            holidays.Add(AdjustForWeekendHoliday(new DateTime(year, 1, 1).Date));

            // Martin Luther King Day - 3rd Monday in January
            var mlkDay = new DateTime(year, 1, 21);
            var dayOfWeek = mlkDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                mlkDay = mlkDay.AddDays(-1);
                dayOfWeek = mlkDay.DayOfWeek;
            }
            holidays.Add(mlkDay.Date);

            // President's Day - 3rd Monday in February
            var presDay = new DateTime(year, 2, 21);
            dayOfWeek = presDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                presDay = presDay.AddDays(-1);
                dayOfWeek = presDay.DayOfWeek;
            }
            holidays.Add(presDay.Date);

            // Memorial Day - last Monday in May
            var memorialDay = new DateTime(year, 5, 31);
            dayOfWeek = memorialDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                memorialDay = memorialDay.AddDays(-1);
                dayOfWeek = memorialDay.DayOfWeek;
            }
            holidays.Add(memorialDay.Date);

            // Independence Day - fixed date 7/4
            holidays.Add(AdjustForWeekendHoliday(new DateTime(year, 7, 4).Date));

            // Labor Day - 1st Monday in September
            var laborDay = new DateTime(year, 9, 1);
            dayOfWeek = laborDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                laborDay = laborDay.AddDays(1);
                dayOfWeek = laborDay.DayOfWeek;
            }
            holidays.Add(laborDay.Date);

            // Columbus Day - 2nd Monday in October
            var columbusday = new DateTime(year, 10, 14);
            dayOfWeek = columbusday.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                columbusday = columbusday.AddDays(-1);
                dayOfWeek = columbusday.DayOfWeek;
            }
            holidays.Add(columbusday.Date);

            // Veterans Day - fixed date
            holidays.Add(AdjustForWeekendHoliday(new DateTime(year, 11, 11).Date));

            // Thanksgiving Day - 4th Thursday in November
            var thanksgiving = (
                from day in Enumerable.Range(1, 30)
                where new DateTime(year, 11, day).DayOfWeek == DayOfWeek.Thursday
                select day).ElementAt(3);
            var thanksgivingDay = new DateTime(year, 11, thanksgiving);
            holidays.Add(thanksgivingDay.Date);

            // Christmas Day - fixed date
            holidays.Add(AdjustForWeekendHoliday(new DateTime(year, 12, 25).Date));
            return holidays.Contains(date);
        }

        private static DateTime AdjustForWeekendHoliday(DateTime holiday)
        {
            switch (holiday.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return holiday.AddDays(-1);
                case DayOfWeek.Sunday:
                    return holiday.AddDays(1);
                default:
                    return holiday;
            }
        }

    } // CssHolidayChecker

} // OutSystems.NssHolidayChecker

