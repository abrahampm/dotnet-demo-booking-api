using System.Threading.Tasks;
using alten_test.Core.Models;
using alten_test.DataAccessLayer.Context;
using alten_test.DataAccessLayer.Interfaces;

namespace alten_test.DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private IReservationRepository _reservations;
        private IRoomRepository _rooms;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IReservationRepository Reservations => _reservations ??= new ReservationRepository(_applicationDbContext);

        public IRoomRepository Rooms => _rooms ??= new RoomRepository(_applicationDbContext);

        public async Task<int> Save()
        {
            return await _applicationDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
        
    }
}