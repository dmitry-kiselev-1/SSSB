using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using China.ICBC.SWIFT;

namespace China.ICBC.SWIFT
{
    /// <summary>
    /// Тип сообщения, совпадающий с пунктом описания сервиса из документации (Description of the ICBCService)
    /// </summary>
    public struct MessageType
    {
        /// <summary>
        /// Тип сообщения
        /// </summary>
        public MessageTypeCode Type { get; set; }

        /// <summary>
        /// Описание типа сообщения
        /// </summary>
        public MessageDescription Description { get; set; }

        /// <summary>
        /// Тип сообщения, совпадающий с пунктом описания сервиса из документации (Description of the ICBCService)
        /// </summary>
        public enum MessageTypeCode : byte { A, B, C, D, E }

        /// <summary>
        /// Описание типа сообщения
        /// </summary>
        public struct MessageDescription
        {
            public string Rus;
            public string Eng;
        }
    }
}
