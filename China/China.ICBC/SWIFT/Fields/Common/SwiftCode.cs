using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace China.ICBC.SWIFT.Fields.Common
{
    /// <summary>
    /// Представляет SWIFT-код
    /// </summary>
    [Serializable]
    public struct SwiftCode
    {
        /// <summary>
        /// SWIFT-код
        /// </summary>
        public readonly string Code;

        /// <summary>
        /// Допустимая длина SWIFT-кода
        /// </summary>
        public const int CODE_LENGTH = 11;

        /// <summary>
        /// Создаёт SWIFT-код и проверяет его корректность
        /// </summary>
        /// <param name="code">Код</param>
        public SwiftCode(string code)
        {
            if (code.Length != CODE_LENGTH)
            {
                throw new FormatException("Длина SWIFT-кода должна быть равна " + CODE_LENGTH + " символам");
            }

            this.Code = code;
        }

        public override string ToString()
        {
            return this.Code;
        }
    }
}
