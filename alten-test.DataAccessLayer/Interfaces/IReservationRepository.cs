using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using alten_test.Core.Models;
using alten_test.Core.Interfaces;
using alten_test.Core.Models.Authentication;

namespace alten_test.DataAccessLayer.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        new IQueryable<Reservation> GetAll();

        new Task<Reservation> GetById(int id);

        Task<List<Reservation>> GetByUserPaginated(IPaginationInfo pageInfo, ApplicationUser user);

        Task<int> GetTotal();
        
        Task<int> GetTotalByUser(ApplicationUser user);
    }
}