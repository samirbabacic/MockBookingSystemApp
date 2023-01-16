using MockBookingSystem.Application.Objects.User;
using MockBookingSystem.Objects.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.ServiceLayer.Contracts
{
    public interface IAccountService
    {
        TokenOutput Register(UserModel userModel);
        TokenOutput Login(UserModel userModel);
    }
}
