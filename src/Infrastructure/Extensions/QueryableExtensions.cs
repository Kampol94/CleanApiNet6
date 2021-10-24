using CleanApi.Application.Specifications;
using CleanApi.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CleanApi.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec)
        where T : BaseEntity
        {
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(query, (current, include) => current.Include(include));
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));
            if (spec.Criteria == null)
                return secondaryResult;
            else
                return secondaryResult.Where(spec.Criteria);
        }
    }
}