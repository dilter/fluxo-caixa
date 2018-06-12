using System;
using System.Globalization;

namespace Stone.Sdk.Extensions
{
    public static class StringExtensions
    {
        public static DateTime ToLocalDateTime(this string dateString)
        {
            return DateTime.Parse(dateString, new CultureInfo("pt-BR"));
        }
    }
}