using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using alten_test.Core.Dto;
using alten_test.Core.Interfaces;
using alten_test.Core.Models.Authentication;
using alten_test.Core.Utilities;


namespace alten_test.BusinessLayer.Interfaces
{
    public interface IReservationService
    {
        Task<ServiceResult> Create(ReservationDtoInput reservation, ApplicationUser user, IList<string> roles);

        Task<ServiceResult> FindById(int id, ApplicationUser user, IList<string> roles);

        Task<ServiceResult> Update(ReservationDto reservation, ApplicationUser user, IList<string> roles);

        Task<ServiceResult> Delete(int id, ApplicationUser user, IList<string> roles);

        Task<ServiceResult> List(IPaginationInfo pageInfo, ApplicationUser user, IList<string> roles);
        
        Task<ServiceResult> GetAvailability(DateTime startDate, DateTime endDate);

        bool ReservationExists(int id);
    }
}