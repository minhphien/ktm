using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Dto
{
    public class KmsTeamDto
    {
        /// <summary>
        /// Map ClientName in JSON response to TeamName when deserializing
        /// </summary>
        [JsonProperty("ClientName")]
       public string TeamName { get; set; }    
    }
}
