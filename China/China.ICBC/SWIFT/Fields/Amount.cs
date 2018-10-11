using System;
using China.ICBC.SWIFT.Fields.Common;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :32A: Сумма в указанной валюте, на указанную дату
    /// </summary>
    [Serializable]
    public class Amount : FieldBase
    {
        public Amount(decimal value, Enums.CurrencyCode currency = Enums.CurrencyCode.USD)
        {
            this.Value = value;
            this.Currency = currency;
        }

        /// <summary>
        /// SWIFT-поле
        /// </summary>
        public override string Field { get { return ":32A:"; } }

        /// <summary>
        /// Допустимая длина SWIFT-поля (строки)
        /// </summary>
        public override Int16 Leght { get { return 6 + 3 + 15; } }

        /// <summary>
        /// Сумма денег
        /// </summary>
        public decimal Value { get; private set; }

        /// <summary>
        /// Бизнес-дата: =текущая, если до (включительно) 15:00 Moscow Time, иначе =следующая
        /// </summary>
        public DateTime Date 
        {
            get
            {
                return DateTime.Now.TimeOfDay.Hours <= 15 ? DateTime.Now.Date : DateTime.Now.AddDays(1).Date;
            } 
        }

        /// <summary>
        /// Код валюты, три символа
        /// </summary>
        public Enums.CurrencyCode Currency { get; private set; }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат: 15 символов.
        /// первые 6 символов = бизнес-дата: =текущая, если до (включительно) 15:00 Moscow Time, иначе =следующая,
        /// далее 3 символа = код валюты,
        /// далее 6 символов = сумма, дробная часть после запятой, запятая обязательно 
        /// Пример :32A:100318USD5200,
        /// </summary>
        public override bool Check(out string result, out string message)
        {
            result = this.Date.ToString("yyMMdd") + this.Currency + ValueToString(this.Value);
            bool isChecked = (result.Length <= this.Leght);
            message = isChecked ? string.Empty : String.Format(CHECK_MESSAGE, this.GetType().Name, this.Leght, "");
            return isChecked;
        }

        /// <summary>
        /// Представление суммы в SWIFT-формате
        /// </summary>
        private string ValueToString(decimal value)
        {
            string valueString = Value.ToString().Replace('.', ',');

            if (!valueString.Contains(","))
            {
                valueString += ",";
            }

            return valueString;
        }

        /// <summary>
        /// Порядок вывода данного SWIFT-поля (строки)
        /// </summary>
        public override short DefaultOrder
        {
            get { return 3; }
        }

    }
}
