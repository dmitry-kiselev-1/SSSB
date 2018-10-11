using System;
using China.ICBC.SWIFT.Fields.Common;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :70: Назначение платежа (константа)
    /// </summary>
    [Serializable]
    public class CashPaymentInformation : PaymentInformation
    {
        override protected string Constant { get { return "LIVING EXPENSES" + "\r\n" + "NO ACCOUNT, PAY CASH"; } }
    }
}
