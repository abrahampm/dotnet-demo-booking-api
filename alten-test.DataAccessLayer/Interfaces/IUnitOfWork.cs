using System.Threading.Tasks;
using alten_test.Core.Models;

namespace alten_test.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        IReservationRepository Reservations { get; }

        IRoomRepository Rooms { get; }

        Task<int> Save();

        void Dispose();
    }
}