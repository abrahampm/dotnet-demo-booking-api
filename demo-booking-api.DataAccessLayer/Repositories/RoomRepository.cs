using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using alten_test.Core.Models;
using alten_test.Core.Interfaces;
using alten_test.Core.Utilities;
using alten_test.DataAccessLayer.Context;
using alten_test.DataAccessLayer.Interfaces;
using alten_test.DataAccessLayer.Extensions;


namespace alten_test.DataAccessLayer.Repositories
{
    public class RoomRepository: Repository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Room>> GetAvailableWithStoredProcedure(DateTime startDate, DateTime endDate)
        {
            return await _entities.FromSqlInterpolated($"SELECT * FROM public.GetAvailableRooms(CAST({startDate} AS DATE),CAST({endDate} AS DATE))").ToListAsync();
        }
    }
}