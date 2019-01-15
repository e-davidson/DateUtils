using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ed.Utilities.Enumerable;
 

namespace Ed.Utilities.Date
{

    public static class DateExtensions
    {
        public static DateTime FirstDayInMonth(this DateTime date) => new DateTime(date.Year,date.Month,date.Day);
        public static DateTime LastDayOfMonth(this DateTime date) => date.FirstDayInMonth().AddMonths(1).AddDays(-1);


        public static DateTime Tomorrow(this DateTime date) => date.AddDays(1);

        public static DateTime GetNextBusinessDay(this DateTime date, int numberOfBusinessDays)
            => InfiniteEnumerable.Inifinite( date , NextBusinessDay,date.IsBusinessDay()).Take(numberOfBusinessDays).Last();

        public static DateTime NextBusinessDay(this DateTime date)
         => date.Tomorrow().DateIterator().First(IsBusinessDay);
          
        public static IEnumerable<DateTime> DateIterator(this DateTime date) => InfiniteEnumerable.Inifinite(date,Tomorrow,true);

        public static int CountBusinessDays(DateTime startDate ,DateTime endDate)
             =>      InfiniteEnumerable.Inifinite( startDate,Tomorrow,true, (date)=> date > endDate,false  ).Where(Date=> Date.IsBusinessDay() ).Count();

        public static bool IsWeekend(this DateTime date)
            => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

        public static bool IsBusinessDay(this DateTime date ) => !date.IsWeekend() && !date.IsHoliday();
        public static bool NotBusinessDay(this DateTime date) => date.IsWeekend() || date.IsHoliday();

        static List<(int Month, int Day)> FixedHolidays = new List<(int Month, int Day)>{
            (1,1),   //new years
            (7,4),   //independence day
            (11,11), //veterans day
            (12,25)  //Christmas
        }; 

        static List<(int Month,DayOfWeek DayOfWeek,int WeekNumberInMonth )> FloatingHolidays =
         new List<(int Month,DayOfWeek Weekday,int WeekNumberInMonth )>
         {
            (1,DayOfWeek.Monday,3),    //Martin Luther King Day
            (2,DayOfWeek.Monday,3),    //Presidents Day
                                       //Memorial Day is an exception
            (9,DayOfWeek.Monday,1),    //Labor Day
            (10,DayOfWeek.Monday,2),   //Columbus Day
            (11,DayOfWeek.Thursday,4), //Thanksgiving
        };

        public static bool IsFixedHoliday(this DateTime testDate)
             => FixedHolidays.Any(fixedHoliday=> 
                fixedHoliday.Month == testDate.Month &&
                fixedHoliday.Day == testDate.Day
             );

        public static bool IsFloatingHoliday(this DateTime testDate)
             => FloatingHolidays.Any(floatingHoliday=> 
                floatingHoliday.Month == testDate.Month && 
                floatingHoliday.DayOfWeek == testDate.DayOfWeek && 
                floatingHoliday.WeekNumberInMonth == testDate.WeekdayNumberInMonth()
             );
        
        public static bool IsFixedHolidayWeekend(this DateTime testDate) 
            => (testDate.DayOfWeek == DayOfWeek.Monday && testDate.AddDays(-1).IsFixedHoliday()) || (
                testDate.DayOfWeek == DayOfWeek.Friday && testDate.AddDays(1).IsFixedHoliday());
        
        public static bool IsMemorialDay(this DateTime testDate)
            => testDate.Month == 5 &&
               testDate.DayOfWeek == DayOfWeek.Monday &&
               testDate.Day> 24;
        public static bool IsHoliday(this DateTime testDate)
            =>  testDate.IsFixedHoliday()        ||
                testDate.IsFloatingHoliday()     ||
                testDate.IsFixedHolidayWeekend() ||
                testDate.IsMemorialDay();

        public static int WeekdayNumberInMonth(this DateTime testDate) => (testDate.Day - 1) / 7 + 1;

        public static int MonthDifference(this DateTime date, DateTime difference ) 
            => (date.Month - difference.Month)    + 12 * (date.Year - difference.Year);
        
        
    }
}
