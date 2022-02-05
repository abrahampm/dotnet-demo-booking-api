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
        public RoomRepository(DatabaseContext context) : base(context) { }

        public async Task<List<Room>> GetAvailableWithStoredProcedure(DateTime startDate, DateTime endDate)
        {
            return await _entities.FromSqlInterpolated($"CALL `GetAvailableRooms`({startDate},{endDate})").ToListAsync();
        }
    }
}