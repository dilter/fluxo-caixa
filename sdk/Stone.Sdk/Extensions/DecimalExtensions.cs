using System.Globalization;

namespace Stone.Sdk.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToCurrencyString(this decimal value)
        {
            var formatInfo = new NumberFormatInfo
            {
                NegativeSign = "-",
                CurrencyDecimalSeparator = ",",
                CurrencyGroupSeparator = ".",
                CurrencySymbol = "R$"
            };
            return string.Format(formatInfo, "{0:c}", value);
        }
        
        public static bool TryParseFromRealCurrency(this string number, out decimal value)
        {
            var formatInfo = new NumberFormatInfo
            {
                NegativeSign = "-",
                CurrencyDecimalSeparator = ",",
                CurrencyGroupSeparator = ".",
                CurrencySymbol = "R$"
            };
            return decimal.TryParse(number, NumberStyles.Currency, formatInfo, out value);
        }
        
    }
}