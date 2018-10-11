using System;
using China.ICBC.SWIFT.Fields.Common;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :71A: Детали расходов (константа)
    /// </summary>
    [Serializable]
    public class ExpensesDetail : FieldBase
    {
        /// <summary>
        /// SWIFT-поле
        /// </summary>
        public override string Field { get { return ":71A:"; } }

        /// <summary>
        /// Допустимая длина SWIFT-поля (строки)
        /// </summary>
        public override Int16 Leght { get { return 3; } }

        /// <summary>
        /// Код банковской операции
        /// </summary>
        public string Code { get { return "OUR"; } }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат: константа
        /// Пример :71A:SHA
        /// </summary>
        public override bool Check(out string result, out string message)
        {
            result = Code;
            bool isChecked = (result.Length <= this.Leght);
            message = isChecked ? string.Empty : String.Format(CHECK_MESSAGE, this.GetType().Name, this.Leght, "");
            return isChecked;
        }

        /// <summary>
        /// Порядок вывода данного SWIFT-поля (строки)
        /// </summary>
        public override short DefaultOrder
        {
            get { return 9; }
        }
    }
}
