using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace China.ICBC.SWIFT.Fields.Common
{
    /// <summary>
    /// Составляющие телефонного номера: код страны и номер
    /// Китайский телефон должен быть 13-15 символов в длину.
    /// </summary>
    [Serializable]
    public struct PhoneData
    {
        public readonly Enums.CountryPhoneCode CountryPhoneCode;
        public readonly string Number;

        /// <summary>
        /// Создаёт объект и выполняет его проверку
        /// </summary>
        public PhoneData(Enums.CountryPhoneCode countryPhoneCode, string number)
        {
            bool isChecked;
            string result = "+" + Convert.ToInt16(countryPhoneCode) + number;

            switch (countryPhoneCode)
            {
                case Enums.CountryPhoneCode.CHN:
                    // +86 123 456 7890
                    isChecked = (result.Length >= (10 + 3) && result.Length <= (12 + 3));
                    if (! isChecked) 
                        throw new FormatException("Телефонный номер в Китае должен быть длиной от 10 до 12 знаков.");
                    break;
                case Enums.CountryPhoneCode.RUS:
                    // +7 123 456 7890
                    isChecked = (result.Length == (10 + 2));
                    if (! isChecked)
                        throw new FormatException("Телефонный номер в России должен быть длиной в 10 знаков.");
                    break;
                default:
                    throw new FormatException("Неизвестный код страны в телефонном номере.");
            }

            this.CountryPhoneCode = countryPhoneCode;
            this.Number = number;
        }

        /// <summary>
        /// Выводит телефон в требуемом формате
        /// </summary>
        public override string ToString()
        {
            return "+" + Convert.ToInt16(CountryPhoneCode) + Number;
        }
    }
}
