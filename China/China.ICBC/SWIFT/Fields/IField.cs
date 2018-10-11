using System;

namespace China.ICBC.SWIFT.Fields.Common
{
    public interface IField
    {
        /// <summary>
        /// SWIFT-формат
        /// </summary>
        string ToSwift();

        /// <summary>
        /// SWIFT-поле
        /// </summary>
        string Field { get; }

        /// <summary>
        /// Допустимая длина SWIFT-поля (строки)
        /// </summary>
        Int16 Leght { get; }

        /// <summary>
        /// Порядок вывода данного SWIFT-поля (строки)
        /// </summary>
        Int16 DefaultOrder { get; }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат
        /// </summary>
        bool Check(out string result, out string message);
    }
}
