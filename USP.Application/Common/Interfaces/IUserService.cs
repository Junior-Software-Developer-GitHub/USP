using USP.Domain.Entities;

namespace USP.Application.Common.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken);
}