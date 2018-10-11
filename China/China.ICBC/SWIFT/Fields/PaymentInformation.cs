using System;
using China.ICBC.SWIFT.Fields.Common;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :70: Назначение платежа (константа)
    /// </summary>
    [Serializable]
    public abstract class PaymentInformation : FieldBase
    {
        /// <summary>
        /// SWIFT-поле
        /// </summary>
        public override string Field { get { return ":70:"; } }

        /// <summary>
        /// Порядок вывода данного SWIFT-поля (строки)
        /// </summary>
        public override short DefaultOrder
        {
            get { return 8; }
        }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат: константа
        /// Пример :70:LIVING EXPENSES
        /// </summary>
        public override bool Check(out string result, out string message)
        {
            result = this.Constant;
            bool isChecked = (result.Length <= this.Leght);
            message = isChecked ? string.Empty : String.Format(CHECK_MESSAGE, this.GetType().Name, this.Leght, "");
            return isChecked;
        }

        /// <summary>
        /// Допустимая длина SWIFT-поля (строки)
        /// </summary>
        public override Int16 Leght { get { return Convert.ToInt16(this.Constant.Length); } }
    }
}
