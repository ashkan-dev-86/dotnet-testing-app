using DotnetTestingApp.Entities;
using DotnetTestingApp.Entities.DB;
using DotNetTestingApp.Entities;
using FluentEmail.Core;
using Microsoft.Data.Sqlite;
using Services.PasswordHasher;

namespace DomainBusinesses.Repositories.Users
{
    public sealed class UserRegistrarService(
        DatabaseContext context,
        IPasswordHasherService passwordHasher,
        IFluentEmail fluentEmail
    )
    {
        public sealed record Request(string Email, string? FirstName, string LastName, string Password);

        public async Task<User> Handle(Request request)
        {
            //if (await context.Users.Exists(request.Email))
            //{
            //    throw new Exception("User already exists");
            //}

            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = passwordHasher.HashPassword(request.Password)
            };

            context.Users.Add(user);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (SqliteException ex)
                when (ex.ErrorCode == ErrorCodes.CONSTRAINT_UNIQUE_VIOLATION)
            {
                throw new Exception(UserErrorMessages.EMAIL_ALREADY_EXISTS, ex);
            }

            // Email Verification
            await fluentEmail
                .To(user.Email)
                .Subject("Email Verification for .NET Testing App")
                .Body("Please verify your email")
                .SendAsync();

            return user;
        }
    }
}
