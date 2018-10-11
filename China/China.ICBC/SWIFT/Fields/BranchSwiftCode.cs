using System;
using China.ICBC.SWIFT.Fields.Common;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :57A: SWIFT-код финансовой организации
    /// </summary>
    [Serializable]
    public abstract class BranchSwiftCode : FieldBase
    {
        /// <summary>
        /// SWIFT-поле
        /// </summary>
        public override string Field { get { return ":57A:"; } }

        /// <summary>
        /// Допустимая длина SWIFT-поля (строки)
        /// </summary>
        public override Int16 Leght { get { return SwiftCode.CODE_LENGTH; } }

        /// <summary>
        /// Порядок вывода данного SWIFT-поля (строки)
        /// </summary>
        public override short DefaultOrder
        {
            get { return 6; }
        }
    }
}
