using System;
using China.ICBC.SWIFT.Fields.Common;
using China.ICBC.SWIFT.Utils;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// Получатель платежа, при выплате на карту (счёт)
    /// </summary>
    [Serializable]
    public class CashBeneficiary : Beneficiary
    {
        /// <summary>
        /// Паспорт (нужен только при варианте выплаты наличными, иначе - должен оставаться пустым)
        /// </summary>
        public PassportData Passport { get; private set; }

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
                    "/"             + Environment.NewLine +
                    base.FullName() + Environment.NewLine +
                    Address         + Environment.NewLine +
                    "ID "           + Passport
                );
            bool isChecked = (result.Length <= this.Leght);
            message = isChecked
                ? string.Empty
                : String.Format(CHECK_MESSAGE, this.GetType().Name, this.Leght, "");
            return isChecked;
        }

        /// <summary>
        /// Получатель перевода (наличными)
        /// </summary>
        public CashBeneficiary(
            NameWithHieroglyph name1,
            NameWithHieroglyph name2,
            NameWithHieroglyph name3,
            NameWithHieroglyph name4,
            AddressData address,
            PassportData passport
            ) : base
                (
                name1,
                name2,
                name3,
                name4,
                address
                )
        {
            this.Passport = passport;
        }

    }
}
