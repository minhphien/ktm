using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Dto
{
    public class KmsEmployeeDto
    {
        /// <summary>
        /// Map EmployeeCode in JSON response to EmployeeBadgeId when deserializing
        /// </summary>
        [JsonProperty("EmployeeCode")]
        public string EmployeeBadgeId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// Map JoinDate in JSON response to JoinedDate when deserializing
        /// </summary>
        [JsonProperty("JoinDate")]
        public DateTime JoinedDate { get; set; }

        /// <summary>
        /// Map ClientName in JSON response to CurrentTeam when deserializing
        /// </summary>
        [JsonProperty("ClientName")]
        public string CurrentTeam { get; set; }
    }
}
