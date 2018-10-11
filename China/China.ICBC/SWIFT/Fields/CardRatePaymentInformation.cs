using System;
using China.ICBC.SWIFT.Fields.Common;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :70: Назначение платежа (константа)
    /// </summary>
    [Serializable]
    public class CardRatePaymentInformation : PaymentInformation
    {
        override protected string Constant { get { return "Overseas payment" + "\r\n" + "LIVING EXPENSES"; } }
    }
}
