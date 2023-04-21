using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSharpTest
{
    [TestClass]
    public class WorkDayCalculatorTests
    {

        [TestMethod]
        public void TestNoWeekEnd()
        {
            DateTime startDate = new DateTime(2021, 12, 1);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(startDate.AddDays(count), result);
        }

        [TestMethod]
        public void TestNormalPath()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestNormalPathWithTwoWeekends()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 10;
            WeekEnd[] weekends = new WeekEnd[]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25)),
                new WeekEnd(new DateTime(2021, 4, 27), new DateTime(2021, 4, 28))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 5, 4)));
        }

        [TestMethod]
        public void TestOneDayOff()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;

            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 23))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(startDate.AddDays(count + 1), result);
        }

        [TestMethod]
        public void TestTwiceOnOneDayOff()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;

            WeekEnd[] weekends = new WeekEnd[]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 23)),
                new WeekEnd(new DateTime(2021, 4, 25), new DateTime(2021, 4, 25))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(startDate.AddDays(count+2), result);
        }

        [TestMethod]
        public void TestWeekendAfterEnd()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25)),
                new WeekEnd(new DateTime(2021, 4, 29), new DateTime(2021, 4, 29))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestReversedWeekends()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 25), new DateTime(2021, 4, 23))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestNoWorkDays()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 0;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 21)));
        }

        [TestMethod]
        public void TestWeekendsWithNoWorkDays()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 0;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 25), new DateTime(2021, 4, 23))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 21)));
        }

        [TestMethod]
        public void TestNegativeNumberOfWorkingDays()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = -5;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 26)));
        }

        [TestMethod]
        public void TestWeekendWithNegativeNumberOfWorkingDays()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = -5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }
    }
}
