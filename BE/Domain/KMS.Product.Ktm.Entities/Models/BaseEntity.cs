using System;
using System.ComponentModel.DataAnnotations;

namespace KMS.Product.Ktm.Entities.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
