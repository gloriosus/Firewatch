using System;
using System.Collections.Generic;
using System.Text;

namespace FirewatchDaemon
{
    public static class AllowableValues
    {
        public static MonthlyValues[] Temperature = new MonthlyValues[]
        {
            new MonthlyValues(1, -29.1F, -19.0F),
            new MonthlyValues(2, -25.5F, -13.0F),
            new MonthlyValues(3, -14.8F, -1.6F),
            new MonthlyValues(4, -1.9F, 10.1F),
            new MonthlyValues(5, 5.1F, 18.7F),
            new MonthlyValues(6, 11.5F, 24.4F),
            new MonthlyValues(7, 15.7F, 27.1F),
            new MonthlyValues(8, 14.1F, 24.6F),
            new MonthlyValues(9, 7.3F, 18.5F),
            new MonthlyValues(10, -2.0F, 8.9F),
            new MonthlyValues(11, -15.0F, -5.3F),
            new MonthlyValues(12, -25.8F, -16.7F)
        };

        public static MonthlyValues[] Dew = new MonthlyValues[]
        {
            new MonthlyValues(1, -28.1F, -18.5F),
            new MonthlyValues(2, -25.0F, -19.8F),
            new MonthlyValues(3, -22.1F, -7.4F),
            new MonthlyValues(4, -9.9F, 0.8F),
            new MonthlyValues(5, 1.0F, 11.3F),
            new MonthlyValues(6, 9.2F, 13.5F),
            new MonthlyValues(7, 18.9F, 21.4F),
            new MonthlyValues(8, 12.3F, 15.2F),
            new MonthlyValues(9, 8.1F, 13.8F),
            new MonthlyValues(10, -3.6F, 5.2F),
            new MonthlyValues(11, -10.8F, -3.5F),
            new MonthlyValues(12, -25.1F, -16.8F)
        };

        // Unnecessary
        /*
        public static MonthlyValues[] Precipitation = new MonthlyValues[]
        {
            new MonthlyValues("January", 3.0F, 10.0F),
            new MonthlyValues("February", 3.0F, 10.0F),
            new MonthlyValues("March", 9.0F, 16.0F),
            new MonthlyValues("April", 28.0F, 38.0F),
            new MonthlyValues("May", 49.0F, 57.0F),
            new MonthlyValues("June", 81.0F, 99.0F),
            new MonthlyValues("July", 101.0F, 140.0F),
            new MonthlyValues("August", 132.0F, 148.0F),
            new MonthlyValues("September", 77.0F, 93.0F),
            new MonthlyValues("October", 30.0F, 40.0F),
            new MonthlyValues("November", 13.0F, 20.0F),
            new MonthlyValues("December", 8.0F, 15.0F)
        };
        */
    }
}
