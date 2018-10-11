using System;
using China.ICBC.SWIFT.Fields.Common;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :57A: SWIFT-код финансовой организации
    /// </summary>
    [Serializable]
    public class CardRateBranchSwiftCode : BranchSwiftCode
    {
        /// <summary>
        /// SWIFT-код филиала ICBC/Moscow (поле "код банковской операции")
        /// </summary>
        public SwiftCode SwiftCode {get { return new SwiftCode("ICBKCNBJSHI"); } }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат: константа
        /// Пример :57A:ICBKCNBJSHI
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
