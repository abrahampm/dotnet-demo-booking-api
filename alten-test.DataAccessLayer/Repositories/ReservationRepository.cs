using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using alten_test.Core.Models;
using alten_test.Core.Interfaces;
using alten_test.DataAccessLayer.Context;
using alten_test.DataAccessLayer.Interfaces;
using alten_test.DataAccessLayer.Extensions;


namespace alten_test.DataAccessLayer.Repositories
{
    public class ReservationRepository: Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext context) : base(context) { }


        public new IQueryable<Reservation> GetAll()
        {
            return (from e in _entities select e).Include("Contact").Include("Room");
        }

        public new async Task<Reservation> GetById(int id)
        {
            return await _entities.Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}