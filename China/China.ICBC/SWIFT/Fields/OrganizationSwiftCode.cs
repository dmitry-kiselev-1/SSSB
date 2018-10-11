using System;
using China.ICBC.SWIFT.Fields.Common;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :52A: SWIFT-код финансовой организации
    /// </summary>
    [Serializable]
    public class HeadSwiftCode : FieldBase
    {
        public HeadSwiftCode()
        {
            this.SwiftCode = new SwiftCode("ICBKRUMMXXX");
        }

        /// <summary>
        /// SWIFT-поле
        /// </summary>
        public override string Field { get { return ":52A:"; } }

        /// <summary>
        /// Допустимая длина SWIFT-поля (строки)
        /// </summary>
        public override Int16 Leght { get { return SwiftCode.CODE_LENGTH; } }

        /// <summary>
        /// SWIFT-код ICBC/Moscow (поле "код банковской операции")
        /// </summary>
        public SwiftCode SwiftCode { get; private set; }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат: константа
        /// Пример :52A:ICBKRUMMXXX
        /// </summary>
        public override bool Check(out string result, out string message)
        {
            result = SwiftCode.Code;
            bool isChecked = (result.Length <= this.Leght);
            message = isChecked ? string.Empty : String.Format(CHECK_MESSAGE, this.GetType().Name, this.Leght, "");
            return isChecked;
        }
        
        /// <summary>
        /// Порядок вывода данного SWIFT-поля (строки)
        /// </summary>
        public override short DefaultOrder
        {
            get { return 5; }
        }
    }
}
