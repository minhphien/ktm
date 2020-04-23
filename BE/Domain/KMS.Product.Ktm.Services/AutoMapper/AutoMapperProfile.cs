using AutoMapper;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Dto.KmsEmployee;
using KMS.Product.Ktm.Dto.KmsTeam;

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
                .ForMember(dest => dest.FirstMidName, option => option.MapFrom(src => $"{src.FirstName} {src.MiddleName}"))
                .AfterMap((from, to) => to.UserName = from.Email.Split('@')[0]);
            CreateMap<KmsTeamDto, Team>();
        }
    }
}
