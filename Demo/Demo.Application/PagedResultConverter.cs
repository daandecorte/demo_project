using AutoMapper;
using System.Collections.Generic;

namespace Demo.Application
{
    public class PagedResultConverter<TSource, TDestination> : ITypeConverter<PagedResult<TSource>, PagedResult<TDestination>>
    {
        public PagedResult<TDestination> Convert(PagedResult<TSource> source, PagedResult<TDestination> destination, ResolutionContext context)
        {
            return new PagedResult<TDestination>
            {
                Data = context.Mapper.Map<List<TDestination>>(source.Data),
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                TotalNumberOfPages = source.TotalNumberOfPages,
                TotalRecordCount = source.TotalRecordCount,
                FilteredRecordCount = source.FilteredRecordCount
            };
        }
    }
}