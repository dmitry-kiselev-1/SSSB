using System;
using China.ICBC.SWIFT.Fields.Common;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :57A: SWIFT-код финансовой организации
    /// </summary>
    [Serializable]
    public class CardBranchSwiftCode : BranchSwiftCode
    {
        /// <summary>
        /// SWIFT-код филиала ICBC/Moscow (поле "код банковской операции")
        /// </summary>
        public SwiftCode SwiftCode { get { return new SwiftCode("ICBKCNBJXXX"); } }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат: константа
        /// Пример :52A:ICBKRUMMXXX
        /// </summary>
        public override bool Check(out string result, out string message)
        {
            result = this.SwiftCode.Code;
            bool isChecked = (result.Length <= this.Leght);
            message = isChecked ? string.Empty : String.Format(CHECK_MESSAGE, this.GetType().Name, this.Leght, "");
            return isChecked;
        }
    }
}
