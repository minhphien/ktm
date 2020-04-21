using System;
using System.Collections.Generic;
using System.Text;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Entities.Common;

namespace KMS.Product.Ktm.Entities.DTO
{
    /// <summary>
    /// user data with kudos
    /// </summary>
    public class UserDataDto
    {
        //user info
        public KmsLoginResponse UserInfo { get; set; }

        //list kudo sent
        public IEnumerable<Kudo> KudoSends { get; set; }

        //list kudo received
        public IEnumerable<Kudo> KudoReceives { get; set; }
    }
}
