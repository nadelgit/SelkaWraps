using AutoMapper;
using SelkaWraps.Controllers;
using SelkaWraps.Data;
using SelkaWraps.Models.Listings;

namespace SelkaWraps.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Listing, ListingsReadOnlyVM>();
        }
    }
}
