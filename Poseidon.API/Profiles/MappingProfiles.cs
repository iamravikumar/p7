using AutoMapper;
using Poseidon.API.Models;

namespace Poseidon.API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BidListInputModel, BidList>()
                .ForMember(x => x.Id, act => act.Ignore());
            CreateMap<BidList, BidListViewModel>();
        }
    }
}