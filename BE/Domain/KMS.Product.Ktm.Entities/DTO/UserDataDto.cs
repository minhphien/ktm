using System;
using System.Collections.Generic;

namespace KMS.Product.Ktm.Entities.DTO
{
    /// <summary>
    /// user data with kudos
    /// </summary>
    public class UserDataDto
    {
        //list kudo sent
        public IEnumerable<KudoDetailDto> KudoSends { get; set; }

        //list kudo received
        public IEnumerable<KudoDetailDto> KudoReceives { get; set; }
    }
}
