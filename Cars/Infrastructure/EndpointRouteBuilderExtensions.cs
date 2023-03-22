namespace CarRentingSystem.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    public static class EndpointRouteBuilderExtensions
    {
        public static void MapDefaultAreaRoute(this IEndpointRouteBuilder endpoints)
            => endpoints.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=home}/{action=Index}/{id?}");
    }
}
