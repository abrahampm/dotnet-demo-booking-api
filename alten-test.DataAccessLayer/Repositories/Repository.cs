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
    public class Repository<T> : IRepository<T> where T: BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public  IQueryable<T> GetAll() {
            return from e in _entities select e;
        }

        public async Task<List<T>> GetAllPaginated(IPaginationInfo pageInfo)
        {
            var ent = from e in _entities select e;

            if (pageInfo.HasFiltering())
            {
                ent = ent.FilterBy(pageInfo.FilterPropertyName, pageInfo.FilterTerm);
            }

            if (pageInfo.HasSorting())
            {
                ent = ent.SortBy(pageInfo.SortPropertyName, pageInfo.SortDirection == PageDirection.Ascending);
            }

            return await ent.Paginate(pageInfo.PageNumber, pageInfo.PageSize).ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Insert(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            entity.Id = 0;
            await _entities.AddAsync(entity);
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
        }

        public bool Exists(int id)
        {
            return _entities.Any(e => e.Id == id);
        }
    }
}