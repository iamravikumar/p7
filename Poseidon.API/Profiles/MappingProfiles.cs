using AutoMapper;
using Poseidon.API.Models;
using Poseidon.API.Models.ViewModels;

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