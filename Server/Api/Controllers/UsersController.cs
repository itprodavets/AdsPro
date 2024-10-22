using System.Security.Claims;
using Api.Domain;
using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
[Authorize]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet("all")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync()
    {
        try
        {
            var result = await mediator.Send(new ListUsersQuery());

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Произошла ошибка при обработке вашего запроса. {ex.Message}");
        }
    }

    [HttpPost("update")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateRequest request)
    {
        try
        {
            var result = await mediator.Send(
                new UserUpdateCommand { Username = request.Username, IsActive = request.IsActive }
            );

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Произошла ошибка при обработке вашего запроса. {ex.Message}");
        }
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMeAsync()
    {
        try
        {
            var username = User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
            return Ok(username);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Произошла ошибка при обработке вашего запроса. {ex.Message}");
        }
    }
}