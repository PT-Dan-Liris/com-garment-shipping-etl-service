using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Com.Garment.Shipping.ETL.Service.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class Sku
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tier")]
        public string Tier { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class CurrentSku
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tier")]
        public string Tier { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class Properties
    {
        [JsonProperty("collation")]
        public string Collation { get; set; }

        [JsonProperty("maxSizeBytes")]
        public long MaxSizeBytes { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("databaseId")]
        public string DatabaseId { get; set; }

        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("currentServiceObjectiveName")]
        public string CurrentServiceObjectiveName { get; set; }

        [JsonProperty("requestedServiceObjectiveName")]
        public string RequestedServiceObjectiveName { get; set; }

        [JsonProperty("defaultSecondaryLocation")]
        public string DefaultSecondaryLocation { get; set; }

        [JsonProperty("catalogCollation")]
        public string CatalogCollation { get; set; }

        [JsonProperty("readScale")]
        public string ReadScale { get; set; }

        [JsonProperty("currentSku")]
        public CurrentSku CurrentSku { get; set; }

        [JsonProperty("storageAccountType")]
        public string StorageAccountType { get; set; }

        [JsonProperty("maintenanceConfigurationId")]
        public string MaintenanceConfigurationId { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class DWHViewModel
    {
        [JsonProperty("sku")]
        public Sku Sku { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}