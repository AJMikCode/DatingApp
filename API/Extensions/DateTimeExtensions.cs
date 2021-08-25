using System;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob)
        {
            //  Find DateTime of today
            var today = DateTime.Today;
            //  Finds difference between DateTime of today and when you were born
            var age = today.Year - dob.Year;
            //  Accounts for birthday of the year whether it hacs happened or not.
            if(dob.Date > today.AddYears(-age)) age--;
            //  Return after changes made from last statement.
            return age;
        }
    }
}