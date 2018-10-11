using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace China.ICBC.SWIFT.Fields.Common
{
    public class Enums
    {
        /// <summary>
        /// Трёх-символьный код валюты
        /// </summary>
        public enum CurrencyCode : byte { RUR, USD, EUR }

        /// <summary>
        /// Код страны
        /// </summary>
        public enum Country : byte { RUS, CHN, DoNotCheck }

        /// <summary>
        /// Телефонный код страны
        /// </summary>
        public enum CountryPhoneCode : byte { RUS = 7, CHN = 86 }

        /// <summary>
        /// Трёх-символьный код платёжной системы (аббревиатура нашего банка, присвоенная системой ICBC)
        /// </summary>
        public enum PaymentSystemCode : byte { ССС }

    }
}
