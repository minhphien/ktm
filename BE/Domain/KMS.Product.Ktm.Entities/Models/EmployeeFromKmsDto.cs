using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.Models
{
    public class EmployeeFromKmsDto
    {
        [JsonProperty("EmployeeCode")]
        public string EmployeeBadgeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [JsonProperty("JoinDate")]
        public DateTime JoinedDate { get; set; }
        [JsonProperty("ClientName")]
        public string CurrentTeam { get; set; }
    }
}
