using AutoMapper;
using KMS.Product.Ktm.Dto;
using KMS.Product.Ktm.Entities.Models;
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
            CreateMap<KmsEmployeeDto, Employee>()
                .ForMember(dest => dest.FirstMidName, option => option.MapFrom(src => $"{src.FirstName} {src.MiddleName}"));
            CreateMap<KmsTeamDto, Team>();
        }
    }
}
