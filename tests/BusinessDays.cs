using System;
using Ed.Utilities.Date;
using Xunit;

namespace tests
{
    public class BusinessDaysTester
    {
        [Fact]
        public void TestHolidays()
        {
            Assert.True(new DateTime(2017,1,1).IsHoliday());
            Assert.True(new DateTime(2017,1,2).IsHoliday());
            Assert.False(new DateTime(2018,1,2).IsHoliday());
            Assert.True(new DateTime(2018,1,15).IsHoliday());
            Assert.True(new DateTime(2018,2,19).IsHoliday());
            Assert.True(new DateTime(2018,5,28).IsHoliday());
            Assert.True(new DateTime(2018,7,4).IsHoliday());
            Assert.True(new DateTime(2018,9,3).IsHoliday());
            Assert.True(new DateTime(2018,10,8).IsHoliday());
            Assert.True(new DateTime(2018,11,12).IsHoliday());
            Assert.True(new DateTime(2018,11,22).IsHoliday());
            Assert.True(new DateTime(2018,12,25).IsHoliday());

        }

        [Fact]
        public void TestNextBusinessDay(){
            Assert.Equal(new DateTime(2017,6,16).NextBusinessDay(), new DateTime(2017,6,19));
            Assert.Equal(new DateTime(2017,6,17).NextBusinessDay(), new DateTime(2017,6,19));
            Assert.Equal(new DateTime(2017,6,18).NextBusinessDay(), new DateTime(2017,6,19));
            Assert.Equal(new DateTime(2017,6,19).NextBusinessDay(), new DateTime(2017,6,20));
            Assert.Equal(new DateTime(2017,7,3).NextBusinessDay(), new DateTime(2017,7,5));
        }

        
        [Fact]
        public void TestCountBusinessDays(){
            Assert.Equal(DateExtensions.CountBusinessDays(new DateTime(2017,6,16), new DateTime(2017,6,17)),1);
            Assert.Equal(DateExtensions.CountBusinessDays(new DateTime(2017,6,16), new DateTime(2017,6,18)),1);
            Assert.Equal(DateExtensions.CountBusinessDays(new DateTime(2017,6,16), new DateTime(2017,6,19)),2);
        }
        [Fact]
        public void TestGetBusinessDays(){
            Assert.Equal(new DateTime(2017,6,16).GetNextBusinessDay(4), new DateTime(2017,6,21));
            Assert.Equal(new DateTime(2017,6,16).GetNextBusinessDay(5), new DateTime(2017,6,22));
            Assert.Equal(new DateTime(2017,6,17).GetNextBusinessDay(4), new DateTime(2017,6,22));
            Assert.Equal(new DateTime(2017,6,17).GetNextBusinessDay(5), new DateTime(2017,6,23));
        }
    }
}
