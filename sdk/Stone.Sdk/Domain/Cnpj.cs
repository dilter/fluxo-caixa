using System;
using System.Text.RegularExpressions;

namespace Stone.Sdk.Domain
{
    public class Cnpj
    {
        public Cnpj()
        {
            
        }        
        //  Constructors
        public Cnpj(string value)            
        {
            this.Value = value;
            this.IsValid();
        }
        
        //  Fields
        public string Value { get; set; }

        //  Operators
        public static implicit operator Cnpj(string value)
        {
            return new Cnpj(value);
        }

        //  Properties
        public static Cnpj Empty => new Cnpj(null);

        //  Methods
        private bool CheckMask(string value)
        {
            return Regex.IsMatch(value, @"(^(\d{2}.\d{3}.\d{3}/\d{4}-\d{2})|(\d{14})$)");
        }

        private string RemoveMask(string value)
        {
            return value.Replace(".", "").Replace("-", "").Replace("/", "");
        }

        private bool IsValid()
        {
            if (this.CheckMask(this.Value))
            {
                if (this.CheckNumber(this.Value))
                    return true;

                throw new InvalidCastException("Invalid Cnpj");
            }
            throw new FormatException("Invalid Format Cnpj");
        }

        private bool CheckNumber(string value)
        {
            var m1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var m2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            value = value.Trim();
            value = this.RemoveMask(value);

            if (value.Length != 14)
                return false;

            var temp = value.Substring(0, 12);
            var sum = 0;

            for (var i = 0; i < 12; i++)
                sum += int.Parse(temp[i].ToString()) * m1[i];

            var leftOver = (sum % 11);

            if (leftOver < 2)
                leftOver = 0;
            else
                leftOver = 11 - leftOver;

            var digit = leftOver.ToString();

            temp = temp + digit;
            sum = 0;

            for (var i = 0; i < 13; i++)
                sum += int.Parse(temp[i].ToString()) * m2[i];

            leftOver = (sum % 11);

            if (leftOver < 2)
                leftOver = 0;
            else
                leftOver = 11 - leftOver;
            digit = digit + leftOver;

            return value.EndsWith(digit);
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}