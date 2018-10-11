using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace China.ICBC.SWIFT.Fields.Common
{
    /// <summary>
    /// Составляющие адреса для SWIFT-формата
    /// </summary>
    [Serializable]
    public struct AddressData
    {
        public readonly string Building;
        public readonly string Street;
        public readonly string City;
        public readonly string Country;

        public AddressData(string building, string street, string city, string country)
        {
            this.Building = building;
            this.Street = street;
            this.City = city;
            this.Country = country;
        }

        /// <summary>
        /// Представление в строгом порядке: Building, Street, City, Country
        /// </summary>
        public override string ToString()
        {
            return
                (String.IsNullOrEmpty(Building) ? String.Empty : Building + ", ") +
                (String.IsNullOrEmpty(Street) ? String.Empty : Street + ", ") +
                (String.IsNullOrEmpty(City) ? String.Empty : City + ", ") +
                (String.IsNullOrEmpty(Building + Street + City) ? String.Empty: Country.ToString());
        }
    }
}
