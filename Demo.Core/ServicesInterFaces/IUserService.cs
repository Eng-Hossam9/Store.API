using Demo.Core.DTO.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.ServicesInterFaces
{
    public interface IUserService
    {
      Task<UserDTO> LogInAsync(LoginDTO model);
      Task<UserDTO> RegisterAsync(RegisterDTO model);
    }
}
