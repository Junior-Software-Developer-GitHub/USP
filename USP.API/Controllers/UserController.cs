using Microsoft.AspNetCore.Mvc;
using USP.API.Services;
using USP.Application.Users.Commands;
using USP.Application.Users.Queries;

namespace USP.API.Controllers;

public class UserController(IUserService userService, IProductService productService) : ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult> Edit(EditUserCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpGet]
    public async Task<ActionResult> Test()
    {
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> GetAllUsers()
    {
       
        return Ok(await Mediator.Send(new GetAllUsersQuery()));
    }
}