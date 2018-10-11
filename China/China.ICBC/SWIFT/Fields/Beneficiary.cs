using System;
using China.ICBC.SWIFT.Fields.Common;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :59: Получатель платежа
    /// </summary>
    [Serializable]
    public abstract class Beneficiary : FieldBase
    {
        protected Beneficiary(
            NameWithHieroglyph name1, 
            NameWithHieroglyph name2, 
            NameWithHieroglyph name3,
            NameWithHieroglyph name4,
            AddressData address
            )
        {
            this.Name1 = name1;
            this.Name2 = name2;
            this.Name3 = name3;
            this.Name4 = name4;
            this.Address = address;
        }

        /// <summary>
        /// SWIFT-поле
        /// </summary>
        public override string Field { get { return ":59:"; } }

        /// <summary>
        /// Допустимая длина SWIFT-поля (строки)
        /// </summary>
        public override Int16 Leght { get { return 175; } }

        /// <summary>
        /// Имя первое
        /// </summary>
        public NameWithHieroglyph Name1 { get; private set; }

        /// <summary>
        /// Имя второе
        /// </summary>
        public NameWithHieroglyph Name2 { get; private set; }

        /// <summary>
        /// Имя третье
        /// </summary>
        public NameWithHieroglyph Name3 { get; private set; }

        /// <summary>
        /// Имя четвёртое
        /// </summary>
        public NameWithHieroglyph Name4 { get; private set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public AddressData Address { get; private set; }

        /// <summary>
        /// Вывод китайского имени вместе с кодами иероглифов (chinese commercial code, 3*4=12 цифр),
        /// в виде "Имя Фамилия Отчество (0015 0012 0018)"
        /// </summary>
        /// <returns></returns>
        public string FullName()
        {

            string fullName =
                (String.IsNullOrWhiteSpace(Name1.Name + " ") ? string.Empty : Name1.Name + " ") +
                (String.IsNullOrWhiteSpace(Name2.Name + " ") ? string.Empty : Name2.Name + " ") +
                (String.IsNullOrWhiteSpace(Name3.Name + " ") ? string.Empty : Name3.Name + " ") +
                (String.IsNullOrWhiteSpace(Name4.Name + " ") ? string.Empty : Name4.Name);

            string fullHieroglyph =
                (String.IsNullOrWhiteSpace(Name1.Hieroglyph + " ") ? string.Empty : Name1.Hieroglyph + " ") +
                (String.IsNullOrWhiteSpace(Name2.Hieroglyph + " ") ? string.Empty : Name2.Hieroglyph + " ") +
                (String.IsNullOrWhiteSpace(Name3.Hieroglyph + " ") ? string.Empty : Name3.Hieroglyph + " ") +
                (String.IsNullOrWhiteSpace(Name4.Hieroglyph + " ") ? string.Empty : Name4.Hieroglyph);

            return fullName + (string.IsNullOrEmpty(fullHieroglyph) ? string.Empty : " (" + fullHieroglyph + ")");
        }

        /// <summary>
        /// Порядок вывода данного SWIFT-поля (строки)
        /// </summary>
        public override short DefaultOrder
        {
            get { return 7; }
        }
    }
}
