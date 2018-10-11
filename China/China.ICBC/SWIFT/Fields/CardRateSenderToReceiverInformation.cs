using System;
using China.ICBC.SWIFT.Fields.Common;
using China.ICBC.SWIFT.Utils;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :72: Информация отправителя получателю
    /// </summary>
    [Serializable]
    public class CardRateSenderToReceiverInformation : SenderToReceiverInformation
    {
        public CardRateSenderToReceiverInformation(PhoneData beneficiaryPhone, decimal rate, decimal amount)
        {
            this.BeneficiaryPhone = beneficiaryPhone;
            this.SwiftCode = new CardBranchSwiftCode().SwiftCode;
            this.CurrencyConversion = "/ACC/YJHMS";
            this.Rate = rate;
            this.Amount = amount;
        }

        /// <summary>
        /// SWIFT-код филиала ICBC/Moscow (поле "код банковской операции")
        /// </summary>
        public SwiftCode SwiftCode { get; private set; }

        public decimal Rate { get; private set; }
        public decimal Amount { get; private set; }
        public decimal AmountConverted { get { return Math.Round(Amount * Rate, 2); } }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат:
        /// Пример
        /// :72:/ACC/ 
        /// /PHONBEN/TEL:2352626556
        /// //ICBKCNBJAHI, ANHUI,
        /// //PROVINCIAL BRANCH
        /// -}
        /// </summary>D:\Work\Китай\China\China.ICBC\SWIFT\MessageC.cs
        public override bool Check(out string result, out string message)
        {
            result = Transliterator.Transliterate
                (
                    "/" + CurrencyConversion                    + Environment.NewLine +
                    "/PHONBEN/TEL:" + BeneficiaryPhone          + Environment.NewLine +
                    "//RATE FIXED AT " + Rate + " EQUIVALENT"   + Environment.NewLine + 
                    "//CNY " + AmountConverted                  + Environment.NewLine +
                    "//" + this.SwiftCode
                );
            bool isChecked = (result.Length <= this.Leght) && BeneficiaryPhone.CountryPhoneCode == Enums.CountryPhoneCode.CHN;
            message = isChecked ? string.Empty : String.Format(CHECK_MESSAGE, this.GetType().Name, this.Leght, "");
            return isChecked;
        }
    }
}
