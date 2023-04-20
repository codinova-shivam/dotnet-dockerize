using System.Collections.Generic;
using System.Threading.Tasks;
using practices.Models;
using practices.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;


namespace practices.Repository
{
    public class AccountRepository : IAccountRepository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;




        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        public async Task<IdentityResult> Signup(SignupModel signupModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = signupModel.FirstName,
                LastName = signupModel.LastName,
                Email = signupModel.Email,
                UserName = signupModel.Email,
            };

            return await _userManager.CreateAsync(user, signupModel.Password);
        }

        public async Task<string> Login(SigninModel signinModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signinModel.Email, signinModel.Password, false, false);

            if(!result.Succeeded){
                return null;
            }
            var authClaims = new Claim[]{
                new Claim(ClaimTypes.Name, signinModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var JWToken = new JwtSecurityToken(
                issuer:  _configuration["Jwt:ValidIssuer"], 
                audience:  _configuration["Jwt:ValidAudience"], 
                claims: authClaims, 
                notBefore: new DateTimeOffset(DateTime.Now).DateTime, 
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes( _configuration["Jwt:Secret"])), 
                SecurityAlgorithms.HmacSha256Signature)
                );
            
            return new JwtSecurityTokenHandler().WriteToken(JWToken);
        }
    }
}