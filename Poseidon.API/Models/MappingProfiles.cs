using AutoMapper;

namespace Poseidon.API.Models
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // BidList
            CreateMap<BidListInputModel, BidList>()
                .ForMember(x => x.Id, act => act.Ignore());
            
            CreateMap<BidList, BidListViewModel>();
            
            // CurvePoint
            CreateMap<CurvePointInputModel, CurvePoint>()
                .ForMember(x => x.Id, act => act.Ignore());

            CreateMap<CurvePoint, CurvePointViewModel>();
            
            // Rating
            CreateMap<RatingInputModel, Rating>()
                .ForMember(x => x.Id, act => act.Ignore());

            CreateMap<Rating, RatingViewModel>();
        }
    }
}