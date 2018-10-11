using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using China.ICBC.DAL.ORM.ICBC;
using China.ICBC.SWIFT.Fields.Common;

namespace China.ICBC.SWIFT.Fields
{
    /// <summary>
    /// :57A: SWIFT-код финансовой организации
    /// Отправитель называет провинцию, банк вставляет SWIFT code провинции 
    /// </summary>
    [Serializable]
    public class CashBranchSwiftCode : BranchSwiftCode
    {
        /// <summary>
        /// SWIFT-код филиала
        /// </summary>
        /// <param name="provincialBranch">Не константа для выплат наличными в ICBC - требуется предварительно выбрать из справочника</param>
        public CashBranchSwiftCode(ProvincialBranch provincialBranch)
        {
            this.ProvincialBranch = provincialBranch;
        }

        /// <summary>
        /// Имя филиала ICBC (name of ICBC province branch)
        /// </summary>
        public ProvincialBranch ProvincialBranch { get; private set; }

        /// <summary>
        /// Проверка возможности конвертации в SWIFT-формат: константа
        /// Пример :57A:ICBKRUMMXXX
        /// </summary>
        public override bool Check(out string result, out string message)
        {
            result = this.ProvincialBranch.Swift.Code;
            bool isChecked = (result.Length <= this.Leght);
            message = isChecked ? string.Empty : String.Format(CHECK_MESSAGE, this.GetType().Name, this.Leght, "");
            return isChecked;
        }
    }
}
