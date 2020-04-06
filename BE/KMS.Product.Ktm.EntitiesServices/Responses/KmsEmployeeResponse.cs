using KMS.Product.Ktm.EntitiesServices.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.EntitiesServices.Responses
{
    public class KmsEmployeeResponse
    {
        /// <summary>
        /// Map Items in JSON response to KmsEmployeeDTOs when deserializing
        /// </summary>
        [JsonProperty("Items")]
        public IEnumerable<KmsEmployeeDTO> KmsEmployeeDTOs { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }
    }
}
