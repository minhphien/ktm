using Newtonsoft.Json;

namespace KMS.Product.Ktm.Dto
{
    public partial class BotProfileDto
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("icons")]
        public IconsDto Icons { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("team_id")]
        public string TeamId { get; set; }

        [JsonProperty("updated")]
        public long Updated { get; set; }
    }
}