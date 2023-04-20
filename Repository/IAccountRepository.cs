using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using practices.Models;

namespace practices.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> Signup(SignupModel signupModel);
        Task<string> Login(SigninModel signinModel);

    }
}