using AutoMapper;
using Poseidon.Shared.InputModels;

namespace Poseidon.API.Models
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // BidList
            CreateMap<BidListInputModel, BidList>()
                .ForMember(entity => entity.Id, action => action.Ignore());
            
            CreateMap<BidList, BidListInputModel>();
            
            // CurvePoint
            CreateMap<CurvePointInputModel, CurvePoint>()
                .ForMember(entity => entity.Id, action => action.Ignore());

            CreateMap<CurvePoint, CurvePointInputModel>();
            
            // Rating
            CreateMap<RatingInputModel, Rating>()
                .ForMember(entity => entity.Id, action => action.Ignore());

            CreateMap<Rating, RatingInputModel>();
            
            // RuleName
            CreateMap<RuleNameInputModel, RuleName>()
                .ForMember(entity => entity.Id, action => action.Ignore());

            CreateMap<RuleName, RuleNameInputModel>();
            
            // Trade
            CreateMap<TradeInputModel, Trade>()
                .ForMember(entity => entity.Id, action => action.Ignore());

            CreateMap<Trade, TradeInputModel>();
            
            // User
            CreateMap<UserInputModel, User>()
                .ForMember(entity => entity.Id, action => action.Ignore());

            CreateMap<User, UserInputModel>();
        }
    }
}