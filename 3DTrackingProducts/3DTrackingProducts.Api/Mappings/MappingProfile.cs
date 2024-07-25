using System;
using _3DTrackingProducts.Api.Dtos;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Models.Auth;
using _3DTrackingProducts.Api.Resources;
using AutoMapper;

namespace _3DTrackingProducts.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserSignUpResource, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));

            CreateMap<LogResource, Log>()
                .ForMember(l => l.IPAddress, opt => opt.MapFrom(lg => lg.IPAddress));

            CreateMap<TagResource, Tag>()
                .ForMember(t => t.EPC, opt => opt.MapFrom(mr => mr.EPC));

            CreateMap<TagPositionResource, TagPosition>()
                .ForMember(tp => tp.TagEPC, opt => opt.MapFrom(mr => mr.TagEPC))
                .ForMember(tp => tp.PairAntennaId, opt => opt.MapFrom(mr => mr.ParAntennaId));

            CreateMap<CategoryResource, Category>()
                .ForMember(c => c.Name, opt => opt.MapFrom(cr => cr.Name));

            CreateMap<ControlTagResource, ControlTag>()
                .ForMember(p => p.PositionX, opt => opt.MapFrom(pr => pr.PositionX));

            CreateMap<Tag3DPosition, Position3DDto>()
                .ForMember(c => c.TagX, opt => opt.MapFrom(pr => pr.RelativePosX));

            CreateMap<RoomResource, Room>()
                .ForMember(r => r.Name, opt => opt.MapFrom(mr => mr.Name));

            CreateMap<PairAntennaResource, PairAntenna>()
                .ForMember(pa => pa.antenna01IP, opt => opt.MapFrom(par => par.antenna01IP));

            CreateMap<PairAntennaDetectionResource, PairAntenna>()
                .ForMember(pa => pa.antenna01IP, opt => opt.MapFrom(par => par.antenna01IP));
        }
    }
}

