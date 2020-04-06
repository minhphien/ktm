using AutoMapper;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.EntitiesServices.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Services.AutoMapper
{
    /// <summary>
    /// Create mapping between DTOs and domain entities
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<KmsEmployeeDTO, Employee>()
                .ForMember(dest => dest.FirstMidName, option => option.MapFrom(src => $"{src.FirstName} {src.MiddleName}"));
            CreateMap<KmsTeamDTO, Team>();
        }
    }
}
