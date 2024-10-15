using MongoDB.Entities;
using USP.Application.Common.Interfaces;
using USP.Domain.Entities;

namespace USP.Infrastructure.Services;

public class UserService : IUserService
{
    public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
       return await DB.Find<User>().ExecuteAsync(cancellationToken);
    }
}