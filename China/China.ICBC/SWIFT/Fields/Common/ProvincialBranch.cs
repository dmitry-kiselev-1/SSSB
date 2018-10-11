using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using China.ICBC.DAL.ORM.ICBC;

namespace China.ICBC.SWIFT.Fields.Common
{
    /// <summary>
    /// Филиал с адресом и SWIFT-кодом
    /// </summary>
    [Serializable]
    public struct ProvincialBranch
    {
        public ProvincialBranch(string name, string address, SwiftCode swift)
        {
            this.Name = name;
            this.Address = address;
            this.Swift = swift;
        }

        public readonly string Name;
        public readonly string Address;
        public readonly SwiftCode Swift;

        /// <summary>
        /// Возвращает коллекцию всех филиалов из БД
        /// </summary>
        public static IEnumerable<ProvincialBranch> GetAllProvincialBranch(string sqlConnectionString)
        {
            using (var db = MT103.GetDatabase(sqlConnectionString))
            {
                var result = db.China_ICBC_ProvincialBranch.ToList();

                return result.Select(r => new ProvincialBranch(r.Name, r.Address, new SwiftCode(r.SWIFT)));
            }
        }

        /// <summary>
        /// Возвращает филиал по его имени из БД
        /// </summary>
        public static ProvincialBranch GetProvincialBranch(string name, string sqlConnectionString)
        {
            var allBranches = GetAllProvincialBranch(sqlConnectionString);
            var branche = allBranches.First(r => r.Name == name);

            return branche;
        }
    }
}
