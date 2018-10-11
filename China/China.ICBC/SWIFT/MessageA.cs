using System;
using System.IO;
using China.ICBC.SWIFT.Fields;

namespace China.ICBC.SWIFT
{
    /// <summary>
    /// Перевод, номинированный в USD/EUR, с зачислением на USD/EUR счет Получателя  по реквизитам счета Получателя либо по реквизитам дебетовой карты Получателя
    /// </summary>
    [Serializable]
    //[Synchronization]
    public class MessageA : MT103
    {
        /// <summary>
        /// Тип сообщения
        /// </summary>
        public override MessageType MessageType { get { return MessageTypes.MessageTypeCollection.Find(t => t.Type == MessageType.MessageTypeCode.A); } }

        public MessageA(
            Amount amount,
            Sender sender,
            CardBeneficiary beneficiary,
            CardSenderToReceiverInformation senderToReceiverInformation)
            : base
                (
                    new TransactionNumber(),
                    amount,
                    new BankOperationCode(), 
                    sender,
                    new HeadSwiftCode(),
                    new CardBranchSwiftCode(),
                    beneficiary,
                    new CardPaymentInformation(),
                    new ExpensesDetail(),
                    senderToReceiverInformation)
        {}
    }
}
