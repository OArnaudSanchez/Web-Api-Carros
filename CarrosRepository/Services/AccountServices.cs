using CarrosData.Context;
using CarrosData.Models;
using CarrosServices.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarrosServices.Services
{
    public class AccountServices : IAccountUserRepository
    {
        private AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountServices(AppDbContext appDbContext, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<bool> Create(ApplicationUser user,UserInfo modelUser)
        {
            IdentityResult result = null;
            try
            {
               result = await _userManager.CreateAsync(user, modelUser.Password);
            }
            catch (Exception)
            {

                return false;
            }

            return result.Succeeded;
        }

        public async Task<bool> Login(UserInfo user)
        {
            SignInResult result = null;
            try
            {
                result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, isPersistent: false,
                    lockoutOnFailure: false);
            }
            catch (Exception)
            {

                return false;
            }

            return result.Succeeded;
        }

    }
}
