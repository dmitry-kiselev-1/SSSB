using System;
using China.ICBC.SWIFT.Fields.Common;
using China.ICBC.SWIFT.Utils;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :72: Информация отправителя получателю
    /// </summary>
    [Serializable]
    public class CashSenderToReceiverInformation : SenderToReceiverInformation
    {
        public CashSenderToReceiverInformation(PhoneData beneficiaryPhone, ProvincialBranch provincialBranch)
        {
            this.BeneficiaryPhone = beneficiaryPhone;
            this.ProvincialBranch = provincialBranch;
            this.CurrencyConversion = "/ACC/";
        }

        /// <summary>
        /// Имя филиала ICBC (name of ICBC province branch)
        /// </summary>
        public ProvincialBranch ProvincialBranch { get; private set; }

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
                    "//" + ProvincialBranch.Swift + ", " + ProvincialBranch.Name
                );
            bool isChecked = (result.Length <= this.Leght) && BeneficiaryPhone.CountryPhoneCode == Enums.CountryPhoneCode.CHN; 
            message = isChecked ? string.Empty : String.Format(CHECK_MESSAGE, this.GetType().Name, this.Leght, "");
            return isChecked;
        }
    }
}
