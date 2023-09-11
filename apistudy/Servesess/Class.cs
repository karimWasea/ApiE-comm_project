// Services/PaginationService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Ecommerce_Api.Models.Entityies;

using Microsoft.EntityFrameworkCore;
namespace apistudy.Servesess
{
    public class PaginationService<TEntity>
    {
        private readonly IQueryable<TEntity> _queryable;

        public PaginationService(IQueryable<TEntity> queryable)
        {
            _queryable = queryable;
        }

        public async Task<PaginatedResult<TEntity>> GetPaginatedResultsAsync(int page, int pageSize)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            var totalItems = await _queryable.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var items = await _queryable
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<TEntity>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = items
            };
        }
    }

   
}