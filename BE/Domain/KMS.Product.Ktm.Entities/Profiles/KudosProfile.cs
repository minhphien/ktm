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
            CreateMap<Kudo, KudoDetailDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => src.Modified))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.KudoDetail.Content))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.KudoDetail.KudoType.TypeName))
                .ForMember(dest => dest.SenderBadgeId, opt => opt.MapFrom(src => src.Sender.EmployeeBadgeId))
                .ForMember(dest => dest.SenderFirstMidName, opt => opt.MapFrom(src => src.Sender.FirstMidName))
                .ForMember(dest => dest.SenderLastName, opt => opt.MapFrom(src => src.Sender.LastName))
                .ForMember(dest => dest.ReceiverBadgeId, opt => opt.MapFrom(src => src.Receiver.EmployeeBadgeId))
                .ForMember(dest => dest.ReceiverFirstMidName, opt => opt.MapFrom(src => src.Receiver.FirstMidName))
                .ForMember(dest => dest.ReceiverLastName, opt => opt.MapFrom(src => src.Receiver.LastName));
        }
    }
}
