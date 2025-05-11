using AutoMapper;
using DineRate.DTO;
using DineRate.Models;

namespace DineRate.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Restaurant Mappings
            CreateMap<Restaurant, RestaurantDTO>()
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ReverseMap();

            CreateMap<CreateRestaurantDTO, Restaurant>().ReverseMap();
            CreateMap<UpdateRestaurantDTO, Restaurant>().ReverseMap();

            // User Mappings
            CreateMap<User, AuthResponseDTO>()
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterDTO, User>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

            // Review Mappings
            CreateMap<CreateReviewDTO, Review>();

            CreateMap<Review, ReviewDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Restaurant.Name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.Reactions.Count(r => r.IsLike)))
                .ForMember(dest => dest.DislikesCount, opt => opt.MapFrom(src => src.Reactions.Count(r => !r.IsLike)));
        }
    }
}
