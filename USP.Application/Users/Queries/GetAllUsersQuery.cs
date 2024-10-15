using MediatR;
using USP.Application.Common.Dto;
using USP.Application.Common.Interfaces;
using USP.Application.Common.Mappers;

namespace USP.Application.Users.Queries;

public record GetAllUsersQuery(): IRequest<List<UserDetailsDto>>;

public class GetAllUsersQueryHandler(IUserService userService) : IRequestHandler<GetAllUsersQuery, List<UserDetailsDto>>
{
    public async Task<List<UserDetailsDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await userService.GetAllUsersAsync(cancellationToken);
        
        var returnListDto =  result.ToUserDtoListAsync();
        
        return returnListDto;
       
    }
}