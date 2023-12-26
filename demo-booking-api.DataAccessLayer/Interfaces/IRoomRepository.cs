using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using alten_test.Core.Models;
using alten_test.Core.Interfaces;

namespace alten_test.DataAccessLayer.Interfaces
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<List<Room>> GetAvailableWithStoredProcedure(DateTime startDate, DateTime endDate);
    }
}