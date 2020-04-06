using KMS.Product.Ktm.EntitiesServices.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.EntitiesServices.Responses
{
    public class KmsTeamResponse
    {
        public IEnumerable<KmsTeamDTO> KmsTeamDTOs { get; set; }
    }
}
