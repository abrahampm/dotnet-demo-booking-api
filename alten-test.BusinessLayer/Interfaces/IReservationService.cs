using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using alten_test.Core.Dto;
using alten_test.Core.Interfaces;


namespace alten_test.BusinessLayer.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationDto> Create(ReservationDtoInput reservation);

        Task<ReservationDto> FindById(int id);

        Task<ReservationDto> Update(ReservationDto reservation);

        Task Delete(int id);

        Task<PaginationResultDto<ReservationDto>> List(IPaginationInfo pageInfo);
        
        Task<List<RoomDto>> GetAvailability(DateTime startDate, DateTime endDate);

        bool ReservationExists(int id);
    }
}