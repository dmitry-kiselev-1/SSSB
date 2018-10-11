using System;
using System.Collections.Generic;
using System.Data;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using China.ICBC.DAL.ORM.ICBC;
using China.ICBC.SWIFT.Fields;
using China.ICBC.SWIFT.Fields.Common;

namespace China.ICBC.SWIFT
{
    /// <summary>
    ///  SWIFT-сообщение MT103Message: единичный перевод средств клиента
    /// </summary>
    [Serializable]
    //[Synchronization]
    public abstract class MT103
    {
        /// <summary>
        /// Тип сообщения
        /// </summary>
        public abstract MessageType MessageType { get; }
        
        /// <summary>
        /// Коллекция полей сообщения
        /// </summary>
        public readonly SortedList<Int16, IField> Fields = new SortedList<Int16, IField>();

        /// <summary>
        /// Представляет TransactionNumber из коллекции Fields
        /// </summary>
        public TransactionNumber TransactionNumber {
            get
            {
                return (TransactionNumber)
                    this.Fields.First(f => f.Value.GetType().Name == typeof (TransactionNumber).Name).Value;
            }
        }

        /// <summary>
        /// Конструктор, заполняющий коллекцию полей Fields
        /// </summary>
        protected MT103(
            TransactionNumber transactionNumber,
            Amount amount, 
            BankOperationCode bankOperationCode,
            Sender sender,
            HeadSwiftCode headSwiftCode,
            BranchSwiftCode branchSwiftCode,
            Beneficiary beneficiary,
            PaymentInformation paymentInformation,
            ExpensesDetail expensesDetail,
            SenderToReceiverInformation senderToReceiverInformation)
        {
            Fields.Add(transactionNumber.DefaultOrder, transactionNumber);
            Fields.Add(amount.DefaultOrder, amount);
            Fields.Add(bankOperationCode.DefaultOrder, bankOperationCode);
            Fields.Add(sender.DefaultOrder, sender);
            Fields.Add(headSwiftCode.DefaultOrder, headSwiftCode);
            Fields.Add(branchSwiftCode.DefaultOrder, branchSwiftCode);
            Fields.Add(beneficiary.DefaultOrder, beneficiary);
            Fields.Add(paymentInformation.DefaultOrder, paymentInformation);
            Fields.Add(expensesDetail.DefaultOrder, expensesDetail);
            Fields.Add(senderToReceiverInformation.DefaultOrder, senderToReceiverInformation);
        }
        
        /// <summary>
        /// SWIFT-формат
        /// </summary>
        public string ToSwift()
        {
            // Проверка сообщения:
            string message;
            if (!Check(out message))
            {
                throw new FormatException(message);
            }

            string result =
                Fields.Aggregate(string.Empty,
                    (current, field) => current + (field.Value.ToSwift() + Environment.NewLine)) + "-}";

            return result;
        }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат
        /// </summary>
        /// <param name="message">Сообщение об ошибочном поле внутри сообщения</param>
        /// <returns>Корректно ли составлено сообщение</returns>
        public bool Check(out string message)
        {
            string fieldResult, fieldMessage = string.Empty;

            foreach (var field in Fields)
            {
                string result; 
                if (!field.Value.Check(out result, out message))
                {
                    return false;
                }
            }
            
            message = "Проверка сообщения успешно завершена";
            return true;

            /*
            if (Fields.OrderByDescending(f => f.Key).All(f => f.Value.Check(out fieldResult, out fieldMessage)))
            {
                message = string.Empty;
                return true;
            }
            message = fieldMessage;
            return false;
            */
        }

        /// <summary>
        /// Сохраняет транзакцию в БД (и актуализирует её номер, в соответствии с БД);
        /// не позволит сохранить одну и ту же транзакцию в БД более одного раза.
        /// </summary>
        /// <param name="send">Создавать ли MT103-файл, или отложить этот процесс и ограничиться только сохранением в БД</param>
        /// <param name="outputDirectory">Директория для создания файла</param>
        /// <param name="sqlConnectionString">Сторока подключения к БД</param>
        public void Save(string sqlConnectionString, bool send = false, DirectoryInfo outputDirectory = null)
        {
            // Проверка сообщения:
            string message;
            if (!Check(out message))
            {
                throw new FormatException(message);
            }

            // Предотвращение повторного сохранения одной и той же транзакции в БД:
            if (this.TransactionNumber.OrderNumber != 0)
            {
                return;
            }

            // Сохранение объекта в бинарном виде в БД, для возможности восстановления из БД в дальнейшем:
            byte[] messageObject;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, this);
                messageObject = memoryStream.GetBuffer();
            }

            using (var db = GetDatabase(sqlConnectionString))
            {
                // Сохранение текущей транзакции в БД:
                var newTransaction = db.spChina_ICBC_Transactions_Insert
                    (
                        paymentSystem: this.TransactionNumber.PaymentSystem.ToString(),
                        dateTime: this.TransactionNumber.Date,
                        messageType: this.MessageType.Type.ToString(),
                        messageSwift: this.ToSwift(),
                        messageObject: messageObject    
                    ).Single();

                // Установка номера текущей транзакции:
                this.TransactionNumber.OrderNumber = newTransaction.OrderNumber;
            }

            // Создать файл:
            if (send && outputDirectory != null && outputDirectory.Exists)
            {
                Send(sqlConnectionString, outputDirectory);
            }
        }

        /// <summary>
        /// Асинхронно сохраняет транзакцию в файл и ставит об этом отметку в БД (в случае успеха).
        /// </summary>
        /// <param name="sqlConnectionString">Сторока подключения к БД</param>
        private void Send(string sqlConnectionString, DirectoryInfo outputDirectory)
        {
            new TaskFactory()
                // Формирование файла:
                .StartNew(() =>
                {
                    string path = Path.Combine(outputDirectory.FullName, this.TransactionNumber.GetSwiftTransactionNumber() + ".txt");
                    FileInfo file = new FileInfo(path);

                    if (file.Exists)
                    {
                        throw new Exception("Попытка сохранения транзакции в существующий файл " + file.Name);
                    }
                    File.WriteAllText(file.FullName, this.ToSwift(), new UTF8Encoding());
                })
                // Запись в базу в случае успеха:
                .ContinueWith(task =>
                {
                    if (task.Exception == null)
                    {
                        using (var db = GetDatabase(sqlConnectionString))
                        {
                            // Отметка об успешной отправке файла:
                            string paymentSystemEnum = this.TransactionNumber.PaymentSystem.ToString();
                            db.China_ICBC_Transactions
                                .Single(r =>
                                    r.PaymentSystem == paymentSystemEnum &&
                                    r.Date == this.TransactionNumber.Date.Date &&
                                    r.OrderNumber == this.TransactionNumber.OrderNumber)
                                .IsFileSent = true;
                            db.SaveChanges();
                        }
                    }
                })
                .Wait();
        }

        /// <summary>
        /// Извлекает порцию готовых сообщений из БД и отправляет их в платёжную систему.
        /// </summary>
        /// <param name="sqlConnectionString">Сторока подключения к БД</param>
        /// <param name="outputDirectory">Директория, в которую необходимо отправить файл</param>
        /// <param name="messageCount">Количество сообщений, извлекаемых из БД для отправки</param>
        /// <param name="sendDelayInHour">Задержка отправки файла, в часах</param>
        public static void Send(string sqlConnectionString, DirectoryInfo outputDirectory, int messageCount, int sendDelayInHour)
        {
            if (!outputDirectory.Exists)
                throw new IOException("Возникла проблема доступа к директории " + outputDirectory);

            IList<China_ICBC_Transactions> messages;

            using (var db = GetDatabase(sqlConnectionString))
            //using (var db = new ICBC_Entities(new EntityConnection("name=ORM")))
            {
                //if (!String.IsNullOrEmpty(sqlConnectionString))
                //    ((EntityConnection)db.Connection).StoreConnection.ConnectionString = sqlConnectionString;

                messages = db.China_ICBC_Transactions
                    .Where(r => !r.IsFileSent && (DateTime.Now.Hour - r.Time.Hours) > sendDelayInHour)
                    .Take(messageCount).ToList();

                // При отсутсвии новых сообщений обработка прекращается:
                if (!messages.Any()) return;
            }
            
            // Выгрузка файлов несколькими потоками:
            Parallel.ForEach(messages, message =>
            {
                string path = Path.Combine(outputDirectory.FullName, message.SwiftTransactionNumber + ".txt");
                FileInfo file = new FileInfo(path);

                if (file.Exists)
                {
                    throw new Exception("Попытка сохранения транзакции в существующий файл " + file.Name);
                }

                File.WriteAllText(file.FullName, message.MessageSwift, new UTF8Encoding());

                using (var db = GetDatabase(sqlConnectionString))
                {
                    db.China_ICBC_Transactions.Single(r => 
                        r.PaymentSystem     == message.PaymentSystem    &&
                        r.Date              == message.Date             &&
                        r.OrderNumber       == message.OrderNumber
                        ).IsFileSent = true;

                    db.SaveChanges();
                }
            });
        }

        /// <summary>
        /// Извлекает заданное сообщение из БД и отправляет его в платёжную систему.
        /// </summary>
        /// <param name="outputDirectory">Директория, в которую необходимо отправить файл</param>
        /// <param name="swiftTransactionNumber">Идентификатор сообщения, которое необходимо отправить (пример: "ССС 14 01 18 001003")</param>
        public static void Send(string sqlConnectionString, DirectoryInfo outputDirectory, string swiftTransactionNumber)
        {
            if (!outputDirectory.Exists)
                throw new IOException("Возникла проблема доступа к директории " + outputDirectory);

            // Извлечение составляющих swiftTransactionNumber для поиска по кластерному индексу в БД:
            string paymentSystem = swiftTransactionNumber.Substring(0, 3);
            DateTime date = new DateTime( 
                Convert.ToInt32("20" + swiftTransactionNumber.Substring(3, 2)),
                Convert.ToInt32(swiftTransactionNumber.Substring(5, 2)),
                Convert.ToInt32(swiftTransactionNumber.Substring(7, 2)));
            int orderNumber = Convert.ToInt32(swiftTransactionNumber.Substring(9));

            // Извлечение записи о сообщении из БД:
            China_ICBC_Transactions messageFromDatabase;
            using (var db = GetDatabase(sqlConnectionString))
            {
                messageFromDatabase = db.China_ICBC_Transactions
                    .Single(r => 
                        r.PaymentSystem == paymentSystem &&
                        r.Date == date && 
                        r.OrderNumber == orderNumber);
            }

            // Восстановление (десериализация) экземпляра сообщения:
            MT103 messageObject;
            using (var memoryStream = new MemoryStream(messageFromDatabase.MessageObject))
            {
                var binaryFormatter = new BinaryFormatter();
                messageObject = (MT103)binaryFormatter.Deserialize(memoryStream);
            }

            // Корректировка номера транзакции восстановленного экземпляра сообщения:
            messageObject.TransactionNumber.OrderNumber = orderNumber;

            // Вызов метода восстановленного экзепляра сообщения:
            messageObject.Send(sqlConnectionString, outputDirectory);
        }

        /// <summary>
        /// Возвращает объект для доступа к БД
        /// </summary>
        internal static ICBC_Entities GetDatabase(string sqlConnectionString, string entityName = "DAL.ORM.ICBC.ICBC")
        {
            var entityBuilder = new EntityConnectionStringBuilder()
            {
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = sqlConnectionString,
                Metadata = "res://*/" + entityName + ".csdl|res://*/" + entityName + ".ssdl|res://*/" + entityName + ".msl"
            };

            return new ICBC_Entities(entityBuilder.ToString());
        }
    }
}
