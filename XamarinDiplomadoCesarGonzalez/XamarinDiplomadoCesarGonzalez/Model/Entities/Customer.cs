using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinDiplomadoCesarGonzalez.Model.Entities
{
    [DataTable("Customers")]
    public class Customer
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("LastName")]
        public string LastName { get; set; }
        [JsonProperty("Address")]
        public string Address { get; set; }
        [JsonProperty("City")]
        public string City { get; set; }
        [JsonProperty("State")]
        public string State { get; set; }
        [JsonProperty("Country")]
        public string Country { get; set; }
        [JsonProperty("Birth")]
        public DateTime Birth { get; set; }
        [JsonProperty("Active")]
        public bool Active { get; set; }

        [Version]
        public string Version { get; set; }

    }
}
