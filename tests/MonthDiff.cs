using System;
using Ed.Utilities.Date;
using Xunit;

namespace tests
{
    public class MonthDiffTester
    {
        [Fact]
        public void TestMothDiff()
        {
            Assert.True(new DateTime(2017,7,10).MonthDifference(new DateTime(2017,7,11))==0);
            Assert.True(new DateTime(2017,7,10).MonthDifference(new DateTime(2017,7,31))==0);
            Assert.True(new DateTime(2017,7,10).MonthDifference(new DateTime(2017,8,1))==-1);
            Assert.True(new DateTime(2017,7,10).MonthDifference(new DateTime(2017,8,10))==-1);
            Assert.True(new DateTime(2017,7,10).MonthDifference(new DateTime(2017,9,10))==-2);
            Assert.True(new DateTime(2017,7,10).MonthDifference(new DateTime(2016,8,1))==11);
            
        }
    }
}
