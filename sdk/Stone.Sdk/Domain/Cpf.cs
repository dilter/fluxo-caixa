using System;
using System.Text.RegularExpressions;

namespace Stone.Sdk.Domain
{
    public struct Cpf
    {
        //  Constructos
        public Cpf(string value)
            : this()
        {
            this.Value = value;
            this.IsValid();
        }
                
        //  Properties
        public readonly string Value;
        
        public static Cpf Empty => new Cpf(null);

        //  Operators
        public static implicit operator Cpf(string value)
        {
            return new Cpf(value);
        }

        //  Methods
        public bool CheckMask(string cpf)
        {
            return Regex.IsMatch(cpf, @"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)");
        }

        private bool CheckNumber(string cpf)
        {
            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            var tempCpf = cpf.Substring(0, 9);
            var soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            var resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            var digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto;
            return cpf.EndsWith(digito);
        }

        public void IsValid()
        {
            if (!this.CheckMask(this.Value)) throw new FormatException("Formato inválido do Cpf");
            if (CheckNumber(this.Value)) return;
            throw new InvalidCastException("Cpf Inválido");
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}