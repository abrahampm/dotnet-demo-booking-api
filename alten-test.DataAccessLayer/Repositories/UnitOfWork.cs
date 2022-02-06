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
        private IRepository<Contact> _contacts;
        private IRoomRepository _rooms;

        public UnitOfWork(IDatabaseContextFactory databaseContextFactory)
        {
            _applicationDbContext = databaseContextFactory.GetContext();
        }

        public IReservationRepository Reservations => _reservations ??= new ReservationRepository(_applicationDbContext);
        
        public IRepository<Contact> Contacts => _contacts ??= new Repository<Contact>(_applicationDbContext);

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