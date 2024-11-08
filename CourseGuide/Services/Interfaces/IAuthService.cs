using CourseGuide.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseGuide.Services.Interfaces
{
    public interface IAuthService
    {
        Task<HttpResponseMessage> Register(RegisterViewModel user);
        Task<HttpResponseMessage> Login(LoginViewModel user);
        Task<HttpResponseMessage> Logout();
    }

}
