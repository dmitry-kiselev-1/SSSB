using System;
using System.IO;
using China.ICBC.SWIFT.Fields;

namespace China.ICBC.SWIFT
{
    /// <summary>
    /// Перевод, номинированный в USD/EUR, без открытия счета Получателю (с выплатой наличных USD/EUR)
    /// </summary>
    [Serializable]
    //[Synchronization]
    public class MessageB : MT103
    {
        /// <summary>
        /// Тип сообщения
        /// </summary>
        public override MessageType MessageType { get { return MessageTypes.MessageTypeCollection.Find(t => t.Type == MessageType.MessageTypeCode.B); } }

        public MessageB(
            Amount amount,
            Sender sender,
            CashBranchSwiftCode branchSwiftCode,
            CashBeneficiary beneficiary,
            CashSenderToReceiverInformation senderToReceiverInformation)
            : base
                (
                    new TransactionNumber(), 
                    amount,
                    new BankOperationCode(), 
                    sender,
                    new HeadSwiftCode(), 
                    branchSwiftCode,
                    beneficiary,
                    new CashPaymentInformation(),
                    new ExpensesDetail(),
                    senderToReceiverInformation)
        {}
    }
}
