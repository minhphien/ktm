using System;
using System.Collections.Generic;
using System.Text;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Entities.DTO
{
    /// <summary>
    /// DTO kudo for report
    /// </summary>
    public class KudoSumReportDto
    {
        // count kudo by employee
        public int CountNums { get; set; }

        // sender id
        public string FilterName { get; set; }

        // sender team name
        public string TeamName { get; set; }

    }
}
