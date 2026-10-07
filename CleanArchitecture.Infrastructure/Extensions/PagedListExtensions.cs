using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Extensions;

public static class PagedListExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, PaginationParams paginationParams)
    {
        // Evitar página menor a 1
        if (paginationParams.PageNumber < 1)
        {
            throw new BusinessException("El número de página debe ser mayor o igual a 1");
        }

        // Evitar tamaño de página inválido
        if (paginationParams.PageSize < 1)
        {
            throw new BusinessException("El tamaño de página debe ser mayor o igual a 1");
        }

        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)paginationParams.PageSize);

        // Si no existen registros
        if (totalPages == 0)
        {
            return new PagedList<T>(
                new List<T>(),
                totalCount,
                1,
                paginationParams.PageSize
            );
        }

        if (paginationParams.PageNumber > totalPages)
        {
            throw new BusinessException($"La página solicitada no existe. La última página disponible es {totalPages}");
        }

        var items = await query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();

        return new PagedList<T>(items, totalCount, paginationParams.PageNumber, paginationParams.PageSize);
    }
}