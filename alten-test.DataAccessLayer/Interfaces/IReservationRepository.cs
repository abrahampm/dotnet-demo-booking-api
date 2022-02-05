using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using alten_test.Core.Models;
using alten_test.Core.Interfaces;

namespace alten_test.DataAccessLayer.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        new IQueryable<Reservation> GetAll();
        new Task<Reservation> GetById(int id);
    }
}