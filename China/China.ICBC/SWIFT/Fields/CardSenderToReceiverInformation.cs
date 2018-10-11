using System;
using China.ICBC.SWIFT.Fields.Common;
using China.ICBC.SWIFT.Utils;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :72: Информация отправителя получателю
    /// </summary>
    [Serializable]
    public class CardSenderToReceiverInformation : SenderToReceiverInformation
    {
        public CardSenderToReceiverInformation(PhoneData beneficiaryPhone)
        {
            this.BeneficiaryPhone = beneficiaryPhone;
            this.SwiftCode = new CardBranchSwiftCode().SwiftCode;
            this.CurrencyConversion = "/ACC/";
        }

        /// <summary>
        /// SWIFT-код филиала ICBC/Moscow (поле "код банковской операции")
        /// </summary>
        public SwiftCode SwiftCode { get; private set; }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат:
        /// Пример
        /// :72:/ACC/ 
        /// /PHONBEN/TEL:2352626556
        /// //ICBKCNBJAHI, ANHUI,
        /// //PROVINCIAL BRANCH
        /// -}
        /// </summary>
        public override bool Check(out string result, out string message)
        {
            result = Transliterator.Transliterate
                (
                    CurrencyConversion + Environment.NewLine +
                    "/PHONBEN/TEL:" + BeneficiaryPhone + Environment.NewLine +
                    "//" + this.SwiftCode
                );
            bool isChecked = (result.Length <= this.Leght) && BeneficiaryPhone.CountryPhoneCode == Enums.CountryPhoneCode.CHN;
            message = isChecked ? string.Empty : String.Format(CHECK_MESSAGE, this.GetType().Name, this.Leght, "");
            return isChecked;
        }
    }
}
