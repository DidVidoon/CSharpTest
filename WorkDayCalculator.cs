using System;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            DateTime resultDate = startDate.Date;

            if (dayCount == 0)
                return resultDate;

            if (dayCount < 0)
                dayCount *= -1;

            for (int i = 0; i < dayCount; i++)
            {
                if (weekEnds != null)
                {
                    foreach (WeekEnd weekEnd in weekEnds)
                    {
                        while (weekEnd.StartDate < weekEnd.EndDate && resultDate >= weekEnd.StartDate && resultDate < weekEnd.EndDate)
                            resultDate = resultDate.AddDays(1);
                        while (weekEnd.StartDate > weekEnd.EndDate && resultDate < weekEnd.StartDate && resultDate >= weekEnd.EndDate)
                            resultDate = resultDate.AddDays(1);
                        if (weekEnd.StartDate == resultDate && weekEnd.EndDate == resultDate)
                            resultDate = resultDate.AddDays(1);
                    }
                }

                resultDate = resultDate.AddDays(1);
            }

            return resultDate;
        }
    }
}
