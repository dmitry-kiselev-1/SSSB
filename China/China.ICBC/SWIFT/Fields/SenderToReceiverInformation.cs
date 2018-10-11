using System;
using China.ICBC.SWIFT.Fields.Common;
using China.ICBC.SWIFT.Utils;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :72: Информация отправителя получателю
    /// </summary>
    [Serializable]
    public abstract class SenderToReceiverInformation : FieldBase
    {
        /// <summary>
        /// SWIFT-поле
        /// </summary>
        public override string Field { get { return ":72:"; } }

        /// <summary>
        /// Допустимая длина SWIFT-поля (строки) - в документации не указана
        /// </summary>
        public override Int16 Leght { get { return 512; } }

        /// <summary>
        /// Инструкция для конвертации валюты
        /// </summary>
        public string CurrencyConversion { get; protected set; }

        /// <summary>
        /// Телефон получателя
        /// </summary>
        public PhoneData BeneficiaryPhone { get; protected set; }

        /// <summary>
        /// Порядок вывода данного SWIFT-поля (строки)
        /// </summary>
        public override short DefaultOrder
        {
            get { return 10; }
        }
    }
}
