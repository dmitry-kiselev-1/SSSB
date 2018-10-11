using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace China.ICBC.SWIFT.Fields.Common
{
    /// <summary>
    /// Составляющие паспорта для SWIFT-формата
    /// </summary>
    [Serializable]
    public struct PassportData
    {
        public readonly string Series;
        public readonly string Number;

        public PassportData(string series, string number, Enums.Country country)
        {
            bool isChecked;
            string result = series + number;
            
            switch (country)
            {
                case Enums.Country.CHN:
                    isChecked = (result.Length == 15 || result.Length == 18);
                    if (! isChecked)
                        throw new FormatException("Номер паспорта в Китае должен быть длиной 15 либо 18 символов.");
                    break;
                case Enums.Country.RUS:
                    isChecked = (result.Length == 10);;
                    if (!isChecked)
                        throw new FormatException("Номер паспорта в России должен быть длиной 10 символов.");
                    break;
                case Enums.Country.DoNotCheck:
                    break;
                default:
                    throw new FormatException("Неизвестный код страны в адресе.");
            }

            this.Series = series;
            this.Number = number;
        }

        public override string ToString()
        {
            return Series + Number;
        }
    }
}
