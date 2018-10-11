using System;
using System.Linq;
using China.ICBC.SWIFT;
using China.ICBC.SWIFT.Fields.Common;
using China.ICBC.SWIFT.Utils;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :50K: Данные отправителя
    /// </summary>
    [Serializable]
    public class Sender : FieldBase
    {
        public Sender(string name, PassportData passport, AddressData address)
        {
            this.Name = name;
            this.Passport = passport;
            this.Address = address;
        }

        /// <summary>
        /// SWIFT-поле
        /// </summary>
        public override string Field { get { return ":50K:"; } }

        /// <summary>
        /// Допустимая длина SWIFT-поля (строки)
        /// </summary>
        public override Int16 Leght { get { return 140; } }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Паспорт
        /// </summary>
        public PassportData Passport { get; private set; }

        /// <summary>
        /// Адрес регистрации
        /// </summary>
        public AddressData Address { get; private set; }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат: не более 140 символов суммарно по всем полям
        /// Пример:
        /// :50K://Wisel Andrea
        /// Yun Ding str Puskin bld. zip:
        /// twn.Aleksandro-Nevsky RUS
        /// </summary>
        public override bool Check(out string result, out string message)
        {
            result =
                Transliterator.Transliterate
                (
                    "//" + Name + Environment.NewLine +
                    Passport + Environment.NewLine +
                    Address
                );
            bool isChecked = (result.Length <= this.Leght);
            message = isChecked ? string.Empty : String.Format(CHECK_MESSAGE, this.GetType().Name, this.Leght, "");
            return isChecked;
        }
        /// <summary>
        /// Порядок вывода данного SWIFT-поля (строки)
        /// </summary>
        public override short DefaultOrder
        {
            get { return 4; }
        }

    }
}
