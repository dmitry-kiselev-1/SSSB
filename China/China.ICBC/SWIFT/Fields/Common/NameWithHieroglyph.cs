using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Linq;
using China.ICBC.DAL.ORM.ICBC;

namespace China.ICBC.SWIFT.Fields.Common
{
    /// <summary>
    /// Составляющие ФИО, вместе с кодом иероглифа (например, имя и соответствующий код иероглифа)
    /// </summary>
    [Serializable]
    public class NameWithHieroglyph
    {
        /// <summary>
        /// Имя, английские символы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Иероглиф, один символ
        /// </summary>
        public string Hieroglyph { get; set; }

        /// <summary>
        /// Код иероглифа, четыре символа (цифры с ведущими нулями)
        /// </summary>
        public string HieroglyphCode { get; set; }

        public NameWithHieroglyph(string name)
            : this(name, string.Empty, string.Empty)
        {}
        
        public NameWithHieroglyph(string name, string hieroglyph, string hieroglyphCode)
        {
            this.Name = name;
            this.Hieroglyph = hieroglyph;
            this.HieroglyphCode = hieroglyphCode;
        }

        /// <summary>
        /// Удостоверяет, что код иероглифа найден
        /// </summary>
        public bool IsHieroglyphCodeFound()
        {
            return HieroglyphCode.Length == 4;
        }

        /// <summary>
        /// Возвращает варианты иероглифов с кодами, для заданного имени
        /// </summary>
        public static List<NameWithHieroglyph> GetHieroglyphList(string sqlConnectionString, string name)
        {
            using (var db = MT103.GetDatabase(sqlConnectionString))
            {
                name = name.ToLower();

                var result = from r in db.China_ICBC_Hieroglyphs.Where(r => r.Name == name).ToList() 
                             select new NameWithHieroglyph(r.Name, r.Hieroglyph, r.HieroglyphCode);
                
                return result.ToList();
            }
        }
    }
}
