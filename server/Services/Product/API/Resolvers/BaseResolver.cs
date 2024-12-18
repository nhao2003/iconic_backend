using API.RequestHelpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;

namespace API.Resolvers
{
    public class BaseResolver
    {
        protected async Task<Pagination<T>> CreatePagedResult<T>(
            IGenericRepository<T> repo,
            ISpecification<T> spec,
            int pageIndex,
            int pageSize) where T : BaseEntity
        {
            var items = await repo.ListAsync(spec);
            var count = await repo.CountAsync(spec);

            return new Pagination<T>(pageIndex, pageSize, count, items);
        }

        protected async Task<Pagination<TDto>> CreatePagedResult<T, TDto>(
            IGenericRepository<T> repo,
            ISpecification<T> spec,
            int pageIndex,
            int pageSize,
            Func<T, TDto> toDto) where T : BaseEntity
        {
            var items = await repo.ListAsync(spec);
            var count = await repo.CountAsync(spec);

            var dtoItems = items.Select(toDto).ToList();

            return new Pagination<TDto>(pageIndex, pageSize, count, dtoItems);
        }

        protected async Task<Pagination<TDto>> CreatePagedResult<T, TDto>(
            IGenericRepository<T> repo,
            ISpecification<T> spec,
            int pageIndex,
            int pageSize,
            IMapper _mapper) where T : BaseEntity
        {
            var items = await repo.ListAsync(spec);
            var count = await repo.CountAsync(spec);

            var dtoItems = _mapper.Map<IReadOnlyList<TDto>>(items);

            return new Pagination<TDto>(pageIndex, pageSize, count, dtoItems);
        }

        protected bool IssExists<T>(int id, IGenericRepository<T> repo) where T : BaseEntity
        {
            return repo.Exists(id);
        }
    }
}

