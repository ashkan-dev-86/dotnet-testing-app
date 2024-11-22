using Carter;
using DomainBusinesses.Repositories.Users;
using DotNetTestingApp.Entities.Requests;

namespace DotnetTestingApp.Api.Endpoints
{
    public class UserEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var mainEndpoint = app.MapGroup("/users")
                .AllowAnonymous();

            mainEndpoint.MapPost("/RegisterUser", RegisterUser);
        }

        public static async Task<IResult> RegisterUser(RegisterUser user, UserRegistrarService registrar)
        {
            try
            {
                var request = new UserRegistrarService.Request(user.Email, user.FirstName, user.LastName, user.Password);
                var value = await registrar.Handle(request);
                return TypedResults.Ok(value);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
    }
}
