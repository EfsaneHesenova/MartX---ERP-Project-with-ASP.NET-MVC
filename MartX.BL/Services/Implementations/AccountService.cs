using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MartX.BL.DTOs.AccountDtos;
using MartX.BL.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace MartX.BL.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountService(IMapper mapper, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _mapper = mapper;
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }

   
    public async Task LoginAsync(LoginDto loginDto)
    {
        IdentityUser? user = await _userManager.FindByEmailAsync(loginDto.UsernameOrEmail);
        if (user is null)
        {
            user = await _userManager.FindByNameAsync(loginDto.UsernameOrEmail);
            if (user is null)
            {
                throw new Exception("Invalid login");
            }
        }
        var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.IsPersistant, false);
        if (!result.Succeeded)
        {
            throw new Exception("Invalid login");
        }
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task RegisterAsync(RegisterDto registerDto)
    {
        IdentityUser user = _mapper.Map<IdentityUser>(registerDto);
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            throw new Exception("Invalid login");
        }

        await _userManager.AddToRoleAsync(user, "User");
    }
}
