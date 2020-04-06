using Newtonsoft.Json;
using System;

namespace KMS.Product.Ktm.Api.Models.Events
{
    public partial class Icons
    {
        [JsonProperty("image_36")]
        public Uri Image36 { get; set; }

        [JsonProperty("image_48")]
        public Uri Image48 { get; set; }

        [JsonProperty("image_72")]
        public Uri Image72 { get; set; }
    }
}
