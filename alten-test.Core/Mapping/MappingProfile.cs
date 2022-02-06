using AutoMapper;
using alten_test.Core.Dto;
using alten_test.Core.Models;
using alten_test.Core.Models.Authentication;
using alten_test.Core.Utilities;

namespace alten_test.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<ReservationDtoInput, ReservationDto>().ReverseMap();
            CreateMap<ReservationDtoInput, Reservation>();
            
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<RoomDtoInput, RoomDto>().ReverseMap();
            CreateMap<RoomDtoInput, Room>();
            
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<PaginationInfo,PaginationInfoDto>();
        }
    }
}