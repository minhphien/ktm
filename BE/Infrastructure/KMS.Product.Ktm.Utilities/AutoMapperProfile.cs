using AutoMapper;
using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Utilities.AutoMapper
{
    /// <summary>
    /// Create mapping between DTOs and models
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeFromKmsDto, Employee>()
                .ForMember(dest => dest.FirstMidName, option => option.MapFrom(src => $"{src.FirstName} {src.MiddleName}"));
            CreateMap<TeamFromKmsDto, Team>();
        }
    }
}
