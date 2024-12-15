using API.DTOs;
using API.RequestHelpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repo,
        ISpecification<T> spec, int pageIndex, int pageSize) where T : BaseEntity
    {
        var items = await repo.ListAsync(spec);
        var count = await repo.CountAsync(spec);

        var pagination = new Pagination<T>(pageIndex, pageSize, count, items);

        return APISuccessResponse(pagination, "Data retrieved successfully");
    }

    protected async Task<ActionResult> CreatePagedResult<T, TDto>(IGenericRepository<T> repo,
        ISpecification<T> spec, int pageIndex, int pageSize, Func<T, TDto> toDto) where T
            : BaseEntity, IDtoConvertible
    {
        var items = await repo.ListAsync(spec);
        var count = await repo.CountAsync(spec);

        var dtoItems = items.Select(toDto).ToList();

        var pagination = new Pagination<TDto>(pageIndex, pageSize, count, dtoItems);

        return APISuccessResponse(pagination, "Data retrieved successfully");
    }

    protected async Task<ActionResult> CreatePagedResult<T, TDto>(
        IGenericRepository<T> repo,
        ISpecification<T> spec,
        int pageIndex,
        int pageSize,
        IMapper _mapper
        ) where T : BaseEntity
    {
        var items = await repo.ListAsync(spec);
        var count = await repo.CountAsync(spec);

        var dtoItems = _mapper.Map<IReadOnlyList<TDto>>(items);

        var pagination = new Pagination<TDto>(pageIndex, pageSize, count, dtoItems);

        return APISuccessResponse(pagination, "Data retrieved successfully");
    }

    protected ActionResult APISuccessResponse<T>(T data, string message = "Success")
    {
        return Ok(new APISucessResponse()
        {
            StatusCode = HttpStatusCode.OK,
            Message = message,
            Data = data
        });
    }

    protected ActionResult APIErrorResponse(Guid id, HttpStatusCode statusCode, string message, List<string> errors)
    {
        return StatusCode((int)statusCode, new APIErrorResponse
        {
            StatusCode = statusCode,
            Message = message,
            Errors = errors
        });
    }
}
