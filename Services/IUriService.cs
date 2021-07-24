using System;
using dotnet_webapi.Filter;

namespace dotnet_webapi.Services
{
 public interface IUriService
{
    public Uri GetPageUri(PaginationFilter filter, string route);
}
}