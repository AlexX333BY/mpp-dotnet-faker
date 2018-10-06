using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class DateTimeValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            /* generated values are limited according to DateTime limitations */
            Random random = new Random();
            int year = random.Next(1, 10000);
            int month = random.Next(1, 13);
            int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
            int hour = random.Next(0, 24);
            int minute = random.Next(0, 60);
            int second = random.Next(0, 60);
            int millisecond = random.Next(0, 1000);

            return new DateTime(year, month, day, hour, minute, second, millisecond);
        }

        public DateTimeValueGenerator()
        {
            GeneratedType = typeof(DateTime);
        }
    }
}
