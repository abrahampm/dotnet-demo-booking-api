using System.Linq;
using Microsoft.EntityFrameworkCore;

using alten_test.Core.Dto;

namespace alten_test.DataAccessLayer.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            var take = pageSize;
            var skip = (pageNumber - 1) * pageSize;
            var total = source.Count();
            
            if (take > total)
            {
                take = total;
                skip = 0;
            }

            if (skip > total)
            {
                skip = total;
                take = 0;
            }

            if (skip + take > total)
            {
                take = total - skip;
            }
            
            return source.Skip( skip ).Take( take );
        }
        
        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string sortPropertyName, bool sortAscending)
        {
            if (sortAscending)
            {
                return source.OrderBy(t => EF.Property<string>(t, sortPropertyName));
            }
            else
            {
                return source.OrderByDescending(t => EF.Property<string>(t, sortPropertyName));
            }
        } 
        
        public static IQueryable<T> FilterBy<T>(this IQueryable<T> source, string filterPropertyName, string filterTerm)
        {
            return source.Where(t => EF.Property<string>(t, filterPropertyName).Contains(filterTerm));
        } 
    }
}