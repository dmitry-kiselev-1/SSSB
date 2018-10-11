using System;
using System.Data.EntityClient;
using System.Linq;
using China.ICBC.DAL.ORM.ICBC;
using China.ICBC.SWIFT.Fields.Common;
using Enum = China.ICBC.SWIFT.Fields.Common.Enums;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :20: Идентификатор транзакции
    /// </summary>
    [Serializable]
    public class TransactionNumber : FieldBase
    {
        public TransactionNumber(Enum.PaymentSystemCode paymentSystem = Enum.PaymentSystemCode.ССС)
        {
            this.PaymentSystem = paymentSystem;
            this.Date = DateTime.Now;
        }

        /// <summary>
        /// SWIFT-поле
        /// </summary>
        public override string Field { get { return ":20:"; } }

        /// <summary>
        /// Допустимая длина SWIFT-поля (строки)
        /// </summary>
        public override Int16 Leght { get { return 15; } }

        /// <summary>
        /// Платёжная система
        /// </summary>
        public Enum.PaymentSystemCode PaymentSystem { get; private set; }

        /// <summary>
        /// Дата транзакции (текущая дата)
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Порядковый номер транзакции (в платёжной системе, в рамках текущей даты);
        /// корректируется после сохранения сообщения в БД.
        /// </summary>
        public int OrderNumber { get; set; }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат: 15 символов.
        /// первые 3 символа = платёжная система (ПС),
        /// далее 6 символов = дата транзакции (ДТ),
        /// далее 6 символов = порядковый номер транзакции в ПС, в рамках ДТ.
        /// Пример :20:CMT011231000001
        /// </summary>
        public override bool Check(out string result, out string message)
        {
            result = GetSwiftTransactionNumber();
            bool isChecked = (result.Length <= this.Leght);
            message = isChecked ? string.Empty : String.Format(CHECK_MESSAGE, this.GetType().Name, this.Leght, "");
            return isChecked;
        }

        /// <summary>
        /// Номер транзакции, совпадающий с номером выгружаемого SWIFT-файла;
        /// корректируется после сохранения сообщения в БД.
        /// </summary>
        public string GetSwiftTransactionNumber()
        {
            return PaymentSystem + Date.ToString("yyMMdd") + OrderNumber.ToString("000000"); ;
        }

        /// <summary>
        /// Порядок вывода данного SWIFT-поля (строки)
        /// </summary>
        public override short DefaultOrder
        {
            get { return 1; }
        }

    }
}
