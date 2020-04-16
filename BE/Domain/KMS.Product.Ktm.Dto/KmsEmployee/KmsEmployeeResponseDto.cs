using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Dto.KmsEmployee
{
    public class KmsEmployeeResponseDto
    {
        /// <summary>
        /// Map Items in JSON response to KmsEmployeeDTOs when deserializing
        /// </summary>
        [JsonProperty("Items")]
        public IEnumerable<KmsEmployeeDto> KmsEmployees { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }
    }
}
