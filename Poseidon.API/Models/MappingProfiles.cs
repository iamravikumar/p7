﻿using AutoMapper;

namespace Poseidon.API.Models
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // BidList
            CreateMap<BidListInputModel, BidList>()
                .ForMember(entity => entity.Id, action => action.Ignore());
            
            CreateMap<BidList, BidListViewModel>();
            
            // CurvePoint
            CreateMap<CurvePointInputModel, CurvePoint>()
                .ForMember(entity => entity.Id, action => action.Ignore());

            CreateMap<CurvePoint, CurvePointViewModel>();
            
            // Rating
            CreateMap<RatingInputModel, Rating>()
                .ForMember(entity => entity.Id, action => action.Ignore());

            CreateMap<Rating, RatingViewModel>();
            
            // RuleName
            CreateMap<RuleNameInputModel, RuleName>()
                .ForMember(entity => entity.Id, action => action.Ignore());

            CreateMap<RuleName, RuleNameViewModel>();
        }
    }
}