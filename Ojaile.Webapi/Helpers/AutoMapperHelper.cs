using AutoMapper;
using Ojaile.Core1.Model;
using Ojaile.Data.DBModel;

namespace Ojaile.Webapi.Helpers
{
    public class AutoMapperHelper: Profile
    {
        public AutoMapperHelper()
        {

            CreateMap<PropertyItem, PropertyItemViewModel>()
                .ForMember(dest =>
                dest.Lga, opt => opt.MapFrom(src => src.Lga))
                .ReverseMap();
            CreateMap<PropertyImage, PropertyImageViewModel>().ReverseMap();
        }
    }
}
