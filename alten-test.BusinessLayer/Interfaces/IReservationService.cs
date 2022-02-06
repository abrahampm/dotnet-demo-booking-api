using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using alten_test.Core.Dto;
using alten_test.Core.Interfaces;
using alten_test.Core.Models.Authentication;


namespace alten_test.BusinessLayer.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationDto> Create(ReservationDtoInput reservation, ApplicationUser user, IList<string> roles);

        Task<ReservationDto> FindById(int id, ApplicationUser user, IList<string> roles);

        Task<ReservationDto> Update(ReservationDto reservation, ApplicationUser user, IList<string> roles);

        Task Delete(int id, ApplicationUser user, IList<string> roles);

        Task<PaginationResultDto<ReservationDto>> List(IPaginationInfo pageInfo, ApplicationUser user, IList<string> roles);
        
        Task<List<RoomDto>> GetAvailability(DateTime startDate, DateTime endDate);

        bool ReservationExists(int id);
    }
}