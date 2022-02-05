using System.Threading.Tasks;
using alten_test.Core.Models;
using alten_test.DataAccessLayer.Context;
using alten_test.DataAccessLayer.Interfaces;

namespace alten_test.DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;
        private IReservationRepository _reservations;
        private IRepository<Contact> _contacts;
        private IRoomRepository _rooms;

        public UnitOfWork(IDatabaseContextFactory databaseContextFactory)
        {
            _databaseContext = databaseContextFactory.GetContext();
        }

        public IReservationRepository Reservations => _reservations ??= new ReservationRepository(_databaseContext);
        
        public IRepository<Contact> Contacts => _contacts ??= new Repository<Contact>(_databaseContext);

        public IRoomRepository Rooms => _rooms ??= new RoomRepository(_databaseContext);

        public async Task<int> Save()
        {
            return await _databaseContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _databaseContext.Dispose();
        }
        
    }
}