using Hangfire.Dashboard;

namespace dotnet_webapi.Services
{
    public class CustomAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;//we can make a condition to just retrive if a certain User/Role is Logged 
        }
    }
}