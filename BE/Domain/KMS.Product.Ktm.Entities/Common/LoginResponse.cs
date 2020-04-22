using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.Common
{
    /// <summary>
    /// responce class
    /// </summary>
    public class KmsLoginResponse
    {
        // login token
        public string Token { get; set; }

        // short name
        public string ShortName { get; set; }

        // employee Code
        public string EmployeeCode { get; set; }

        // email
        public string Email { get; set; }
    }
}
