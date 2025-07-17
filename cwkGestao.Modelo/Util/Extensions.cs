using System;
using System.Linq;

namespace cwkGestao.Modelo.Util
{
    public static class Extensions
    {
        public static int ToInt(this string value, int valueDefault = 0)
        {
            value = value.OnlyNumbers();
            int.TryParse(value, out var result);
            return result != 0 ? result : valueDefault;
        }

        public static decimal ToDecimal(this string value, decimal valueDefault = 0)
        {
            value = value.Replace("R$", "").Replace("%", "").Replace(" ", "");
            decimal.TryParse(value, out var result);
            return result != 0 ? result : valueDefault;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            value = value.TrimCk();
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !value.IsNullOrEmpty();
        }

        public static string OnlyNumbers(this string value)
        {    
            return (value ?? "").Trim().IsNullOrEmpty()
                ? string.Empty
                : new string(value.Where(char.IsDigit).ToArray());
        }

        public static string TrimCk(this string value)
        {
            return value?.Trim() ?? string.Empty;
        }

        public static int NotZeroOrLower(this int value)
        {
            return value <= 0 ? 1 : value;
        }

        public static int DaysInMonth(this DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }
    }
}