using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SFMForFraudTransactions.ViewModels
{
    /// <summary>
    /// Custom Class to Validate Date Inputs
    /// </summary>
    public class ValidDate : ValidationAttribute
    {
        /// <summary>
        /// Works with the object passed as parameter to ensure it is a valid DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Valid</returns>
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value), "d MMM yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTime);

            return isValid;
        }
    }
}
