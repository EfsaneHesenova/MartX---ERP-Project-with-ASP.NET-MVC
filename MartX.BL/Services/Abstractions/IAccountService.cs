using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.BL.DTOs.AccountDtos;

namespace MartX.BL.Services.Abstractions;

public interface IAccountService
{
    Task RegisterAsync(RegisterDto registerDto);
    Task LoginAsync(LoginDto loginDto);
    Task LogoutAsync();

}
