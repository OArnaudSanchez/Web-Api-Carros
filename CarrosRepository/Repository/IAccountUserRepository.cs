using CarrosData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarrosServices.Repository
{
    public interface IAccountUserRepository
    {
        Task<bool> Create(ApplicationUser user,UserInfo modelUser);
        Task<bool> Login(UserInfo user);
        
    }
}
