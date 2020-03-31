using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.Models
{
    public class TeamFromKmsResponse
    {
        public IEnumerable<TeamFromKmsDto> KmsTeamDtos { get; set; }
    }
}
