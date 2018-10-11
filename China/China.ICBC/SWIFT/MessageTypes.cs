using System;
using System.Collections.Generic;

namespace China.ICBC.SWIFT
{
    /// <summary>
    /// Содержит коллекцию всех возможных типов сообщения из документации (Description of the ICBCService)
    /// </summary>
    public static class MessageTypes
    {
        /// <summary>
        /// Коллекция всех возможных типов сообщения из документации (Description of the ICBCService)
        /// </summary>
        public static readonly List<MessageType> MessageTypeCollection = new List<MessageType>();

        static MessageTypes()
        {
            MessageTypeCollection.Add(
                new MessageType()
                {
                    Type = MessageType.MessageTypeCode.A,
                    Description = new MessageType.MessageDescription()
                    {
                        Rus = "Перевод, номинированный в USD/EUR, с зачислением на USD/EUR счет Получателя  по реквизитам счета Получателя либо по реквизитам дебетовой карты Получателя",
                        Eng = "Transfer in USD/EUR, with crediting the Beneficiary’s USD/EUR account or debit card"
                    }
                });

            MessageTypeCollection.Add(
                new MessageType()
                {
                    Type = MessageType.MessageTypeCode.B,
                    Description = new MessageType.MessageDescription()
                    {
                        Rus = "Перевод, номинированный в USD/EUR, без открытия счета Получателю (с выплатой наличных USD/EUR)",
                        Eng = "Transfer in USD/EUR, without opening an account for the Beneficiary (with cash withdrawal in USD/EUR)"
                    }
                });

            MessageTypeCollection.Add(
                new MessageType()
                {
                    Type = MessageType.MessageTypeCode.C,
                    Description = new MessageType.MessageDescription()
                    {
                        Rus = "Перевод, номинированный в USD, c конвертацией USD/CNY и зачислением средств на CNY счет Получателя-резидента Китая в любом банке Китая",
                        Eng = "Transfer in USD, with crediting the Beneficiary’s account in CNY in any Chinese bank"
                    }
                });

            MessageTypeCollection.Add(
                new MessageType()
                {
                    Type = MessageType.MessageTypeCode.D,
                    Description = new MessageType.MessageDescription()
                    {
                        Rus = "Перевод, номинированный в USD/EUR, без открытия счета Получателю (с выплатой наличных USD/EUR)",
                        Eng = "Transfer in USD/EUR, without opening an account for the Beneficiary (with cash withdrawal in USD/EUR)"
                    }
                });

            MessageTypeCollection.Add(
                new MessageType()
                {
                    Type = MessageType.MessageTypeCode.E,
                    Description = new MessageType.MessageDescription()
                    {
                        Rus = "Перевод, номинированный в USD/EUR, с зачислением на USD/EUR счет либо дебетовую карту Получателя  по реквизитам счета Получателя в банках Китая в соответствии со списком банков-участников системы CDFCPS",
                        Eng = "Transfer in USD/EUR, with crediting the Beneficiary’s USD/EUR account or debit card in to Chinese banks –participants of CDFCPS"
                    }
                });

        }
    }
}
