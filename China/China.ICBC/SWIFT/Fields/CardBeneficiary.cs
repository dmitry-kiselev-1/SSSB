using System;
using China.ICBC.SWIFT.Fields;
using China.ICBC.SWIFT.Fields.Common;
using China.ICBC.SWIFT.Utils;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// Получатель платежа, при выплате наличными
    /// </summary>
    [Serializable]
    public class CardBeneficiary : Beneficiary
    {
        /// <summary>
        /// Номер счёта или карты - 19 или 16 символов (при варианте выплаты наличными должен оставаться пустым)
        /// </summary>
        public string AccountNumber { get; private set; }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат: не более 35 символов на каждое поле.
        /// Пример:
        /// :59:/9558820402002075269
        /// Pei Diu(0015 0012)
        /// str.Str bld. zip: twn.Guangzhou C
        /// ID 230206197101290216
        /// </summary>
        public override bool Check(out string result, out string message)
        {
            result = Transliterator.Transliterate
                (
                    "/" + AccountNumber + Environment.NewLine +
                    base.FullName()     + Environment.NewLine +
                    Address
                );
            bool isChecked = (result.Length <= this.Leght) && 
                (this.AccountNumber.Length == 16 || 
                 this.AccountNumber.Length == 19);
            message = isChecked
                ? string.Empty
                : String.Format(CHECK_MESSAGE, this.GetType().Name, this.Leght,
                    "Номер счёта в Китае должен быть равен 16 либо 19 символов");
            return isChecked;
        }

        /// <summary>
        /// Получатель перевода (на карту или счёт)
        /// </summary>
        public CardBeneficiary(
            string accountNumber,
            NameWithHieroglyph name1,
            NameWithHieroglyph name2,
            NameWithHieroglyph name3,
            NameWithHieroglyph name4,
            AddressData address
            ) : base
                (
                name1,
                name2,
                name3,
                name4,
                address
                )
        {
            this.AccountNumber = accountNumber;
        }
    }
}
