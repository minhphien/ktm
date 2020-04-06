using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Entities.DTO;

namespace KMS.Product.Ktm.Entities.Profiles
{
    public class KudosProfile : Profile
    {
        /// <summary>
        /// auto mapper profile
        /// </summary>
        public KudosProfile()
        {
            // map kudo to kudo report dto
            CreateMap<Kudo, KudoReportDto>();
        }
    }
}
