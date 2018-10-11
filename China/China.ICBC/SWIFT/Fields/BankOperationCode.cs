using System;
using China.ICBC.SWIFT.Fields.Common;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :23B: Код банковской операции (константа)
    /// </summary>
    [Serializable]
    public class BankOperationCode : FieldBase
    {
        /// <summary>
        /// SWIFT-поле
        /// </summary>
        public override string Field { get { return ":23B:"; } }

        /// <summary>
        /// Порядок вывода данного SWIFT-поля (строки)
        /// </summary>
        public override short DefaultOrder
        {
            get { return 2; }
        }

        override protected string Constant { get { return "CRED"; } }

        /// <summary>
        /// Допустимая длина SWIFT-поля (строки)
        /// </summary>
        public override Int16 Leght { get { return Convert.ToInt16(this.Constant.Length); } }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат: константа
        /// Пример :23B:CRED
        /// </summary>
        public override bool Check(out string result, out string message)
        {
            result = this.Constant;
            bool isChecked = (result.Length <= this.Leght);
            message = isChecked ? string.Empty : String.Format(CHECK_MESSAGE, this.GetType().Name, this.Leght, "");
            return isChecked;
        }
    }
}
