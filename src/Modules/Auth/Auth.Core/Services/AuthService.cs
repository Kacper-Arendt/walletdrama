using Auth.Core.Dtos;
using Auth.Core.Entities;
using Auth.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Auth.Core.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private IHttpContextAccessor _httpContextAccessor;

    public AuthService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<RegisterUserResponseDto> RegisterUser(RegisterUserDto registerUserDto)
    {
        var user = User.CreateNormalUser(registerUserDto.Email);
        var result = await _userManager.CreateAsync(user, registerUserDto.Password);

        if (!result.Succeeded)
        {
            throw new UserCreationFailedException(result.Errors.ToList());
        }

        return new RegisterUserResponseDto(user.Id);
    }

    public async Task<CurrentUserResponseDto?> GetCurrentUser()
    {
        var name =  _httpContextAccessor.HttpContext?.User.Identity?.Name;
        var user = await _userManager.FindByEmailAsync(name);
        var roles = await _userManager.GetRolesAsync(user);

        return name == null ? null : new CurrentUserResponseDto(name, user.Id, roles);
    }
}