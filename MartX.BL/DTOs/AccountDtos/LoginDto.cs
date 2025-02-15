using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartX.BL.DTOs.AccountDtos;

public class LoginDto
{
    public string? UsernameOrEmail { get; set; }
    public string? Password { get; set; }
    public bool IsPersistant { get; set; }
}
