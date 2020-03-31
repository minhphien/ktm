using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.Models
{
    public class EmployeeFromKmsResponse
    {
        [JsonProperty("Items")]
        public IEnumerable<EmployeeFromKmsDto> KmsEmployeeDtos { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
