using AutoMapper;
using DineRate.DTO;
using DineRate.Models;

namespace DineRate.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Restaurant
            CreateMap<Restaurant, RestaurantDTO>()
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src =>
                    src.Reviews.SelectMany(r => r.Reactions).Count(rr => rr.IsLike)))
                .ForMember(dest => dest.DislikesCount, opt => opt.MapFrom(src =>
                    src.Reviews.SelectMany(r => r.Reactions).Count(rr => !rr.IsLike)))
                .ReverseMap();

            CreateMap<CreateRestaurantDTO, Restaurant>().ReverseMap();

            CreateMap<UpdateRestaurantDTO, Restaurant>().ReverseMap();


            //User
            CreateMap<User, AuthResponseDTO>()
                .ForMember(dest => dest.Token, opt => opt.Ignore()) // assigning Token manually
                .ReverseMap();

            CreateMap<RegisterDTO, User>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();

            //Review
            CreateMap<CreateReviewDTO, Review>()
                .ForMember(dest => dest.RestaurantId, opt => opt.MapFrom(src => src.RestaurantId))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));

            CreateMap<Review, ReviewDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))  // Assuming Review has a User object
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))  // Map UserId
                .ForMember(dest => dest.RestaurantId, opt => opt.MapFrom(src => src.RestaurantId))
                .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Restaurant.Name)) // Map Restaurant Name to ReviewDTO
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd HH:mm"))); // Format CreatedAt
        }
    }
}
