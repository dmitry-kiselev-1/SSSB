using System;
using China.ICBC.SWIFT.Fields.Common;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// Общий для всех полей базовый класс
    /// </summary>
    [Serializable]
    public abstract class FieldBase : IField
    {
        /// <summary>
        /// Общий для всех полей формат сообщения об ошибке
        /// </summary>
        protected const string CHECK_MESSAGE = "Ошибка при формировании поля {0}. Допустимо не более {1} символов. {2}";

        /// <summary>
        /// В случае, если значением поля является константа, содержит её.
        /// </summary>
        protected virtual string Constant {get { return string.Empty; }}

        /// <summary>
        /// SWIFT-поле
        /// </summary>
        public abstract string Field { get; }

        /// <summary>
        /// Допустимая длина SWIFT-поля (строки)
        /// </summary>
        public abstract short Leght { get; }

        /// <summary>
        /// Порядок вывода данного SWIFT-поля (строки)
        /// </summary>
        public abstract short DefaultOrder { get; }

        /// <summary>
        /// Общее для всех полей сообщение об ошибке
        /// </summary>
        protected void RaiseException(string message)
        {
            throw new FormatException(message);
        }

        /// <summary>
        /// Преобразует экземпляр дочернего класса в SWIFT-формат
        /// </summary>
        public string ToSwift()
        {
            string result, message;

            if (!Check(out result, out message))
            {
                RaiseException(message);
            }

            return Field + result;
        }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат: константа
        /// </summary>
        /// <param name="result">SWIFT-формат</param>
        /// <param name="message">Сообщение о причине ошибки</param>
        /// <returns>Выполнена ли проверка</returns>
        public abstract bool Check(out string result, out string message);
    }
}

