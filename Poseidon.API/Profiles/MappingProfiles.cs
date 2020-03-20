using AutoMapper;
using Poseidon.API.Models;

namespace Poseidon.API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BidListInputModel, BidList>();
            CreateMap<BidList, BidListViewModel>();
        }
    }
}