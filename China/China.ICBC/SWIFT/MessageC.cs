using System;
using System.IO;
using China.ICBC.SWIFT.Fields;

namespace China.ICBC.SWIFT
{
    /// <summary>
    /// Перевод, номинированный в USD, c конвертацией USD/CNY и зачислением средств на CNY счет Получателя-резидента Китая в банке ICBC Ltd;
    /// </summary>
    [Serializable]
    //[Synchronization]
    public class MessageC : MT103
    {
        /// <summary>
        /// Тип сообщения
        /// </summary>
        public override MessageType MessageType { get { return MessageTypes.MessageTypeCollection.Find(t => t.Type == MessageType.MessageTypeCode.C); } }

        public MessageC(
            Amount amount,
            Sender sender,
            CardBeneficiary beneficiary,
            CardRateSenderToReceiverInformation senderToReceiverInformation)
            : base
                (
                    new TransactionNumber(),
                    amount,
                    new BankOperationCode(),
                    sender,
                    new HeadSwiftCode(), 
                    new CardRateBranchSwiftCode(),
                    beneficiary,
                    new CardRatePaymentInformation(),
                    new ExpensesDetail(),
                    senderToReceiverInformation)
        {}
    }
}
