using AutoMapper;
using Carpooling.Data.Models;
using Carpooling.Services.DTOs;
using Carpooling.Web.Models;
using Carpooling.Web.Models.APIModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Carpooling.Web.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
              this.CreateMap<User, UserPresentDTO>().ReverseMap();
            this.CreateMap<User, Role>();
            this.CreateMap<Feedback, User>();
            this.CreateMap<User, UserCreateDTO>().ReverseMap();
            this.CreateMap<Travel, TravelPresentDTO>()
                .ForMember(dto => dto.TravelTags, conf => conf.MapFrom(ol => ol.TravelTags.Select(y => y.Tag).ToList()))
                .ReverseMap();
            this.CreateMap<Travel, TravelCreateDTO>()
                .ForMember(dto => dto.TravelTags, conf => conf.MapFrom(ol => ol.TravelTags.Select(y => y.Tag).ToList()))
                .ReverseMap();
            this.CreateMap<Feedback, FeedbackPresentDTO>();
            this.CreateMap<Feedback, FeedbackCreateDTO>().ReverseMap();
            this.CreateMap<City, CityCreateDTO>();
            this.CreateMap<Feedback, CityPresentDTO>();
            this.CreateMap<TravelRequestModel, TravelCreateDTO>();
            this.CreateMap<TravelPresentDTO, TravelResponseModel>();
            this.CreateMap<CityCreateDTO, CityResponseModel>();
            this.CreateMap<CityResponseModel, CityPresentDTO>();
            this.CreateMap<CityResponseModel, CityCreateDTO>();
            this.CreateMap<CityResponseModel, CityCreateDTO>();
            this.CreateMap<UserPresentDTO, UserResponseModel>();
            this.CreateMap<UserRequestModel, UserCreateDTO>();
            this.CreateMap<TravelDetailsViewModel, TravelTag>().ReverseMap();
            this.CreateMap<TravelPresentDTO, TravelTag>().ReverseMap();
            this.CreateMap<TravelCreateViewModel, TravelTag>().ReverseMap();
            this.CreateMap<FeedbackResponseModel, FeedbackPresentDTO>();
            this.CreateMap<FeedbackCreateDTO, FeedbackRequestModel>().ReverseMap();
            this.CreateMap<TravelPresentDTO, TravelDetailsViewModel>();
            this.CreateMap<TravelCreateDTO, TravelUpdateViewModel>();
            this.CreateMap<TravelCreateDTO, TravelCreateViewModel>().ReverseMap();
            this.CreateMap<TravelViewModel, TravelPresentDTO>().ReverseMap();
            this.CreateMap<UserViewModel, UserPresentDTO>(); //.ReverseMap();
            this.CreateMap<UserProfileViewModel, UserPresentDTO>().ReverseMap();
            this.CreateMap<UserPresentDTO, TravelDetailsViewModel>().ReverseMap();
            this.CreateMap<UserUpdateViewModel, UserPresentDTO>().ReverseMap();
            this.CreateMap<UserCreateDTO, UserUpdateViewModel>().ReverseMap();
            this.CreateMap<UserHomeViewModel, UserPresentDTO>().ReverseMap();
            this.CreateMap<UserCreateDTO, RegisterViewModel>();
            this.CreateMap<UserPresentDTO, FeedbackSearchViewModel>()
                .ForMember(x => x.UserId, y => y.MapFrom(src => src.Id))
                .ReverseMap();
            this.CreateMap<FeedbackViewModel, FeedbackPresentDTO>() .ReverseMap();
            this.CreateMap<FeedbackViewModel, FeedbackSearchViewModel>() .ReverseMap();
            this.CreateMap<FeedbackPresentDTO, FeedbackSearchViewModel>() .ReverseMap();
            this.CreateMap<FeedbackGivenViewModel, FeedbackPresentDTO>().ReverseMap();
            this.CreateMap<FeedbackCreateDTO, FeedbackCreateViewModel>().ReverseMap();
            this.CreateMap<IEnumerable<UserProfileViewModel>, ParticipantsViewModel>()
                .ForMember(x => x.Passengers, y => y.MapFrom(src => src));
            this.CreateMap<UserProfileViewModel, ParticipantsViewModel>();
                //.ForMember(x => x.Driver, y => y.MapFrom(src => src)); ;
            this.CreateMap<IEnumerable<FeedbackViewModel>, ParticipantsViewModel>()
                .ForMember(x => x.Feedbacks, y => y.MapFrom(src => src));

        }
    }
}
