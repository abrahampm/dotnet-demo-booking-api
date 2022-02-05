using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using alten_test.Core.Interfaces;


namespace alten_test.DataAccessLayer.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        Task<List<T>> GetAllPaginated(IPaginationInfo pageInfo);

        Task<T> GetById(int id);

        Task Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        bool Exists(int id);
    }
}