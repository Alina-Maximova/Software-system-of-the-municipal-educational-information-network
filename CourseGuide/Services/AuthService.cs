using CourseGuide.Helpers;
using CourseGuide.Models;
using CourseGuide.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Azure;

namespace CourseGuide.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/AppUser";

        public AuthService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        // Переносим логику регистрации в контроллер
        public async Task<HttpResponseMessage> Register(RegisterViewModel user)
        {
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var pathBase = BasePath + "/Register";
            var response = await _client.PostAsync(pathBase, content);

           

            return response;
        }

        // Переносим логику логина в контроллер
        public async Task<HttpResponseMessage> Login(LoginViewModel user)
        {
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var pathBase = BasePath + "/Login";
            var response = await _client.PostAsync(pathBase, content);
            Console.WriteLine(response);
            return response;
        }

        public async Task<HttpResponseMessage> Logout()
        {
            var pathBase = BasePath + "/Login";

            var response = await _client.GetAsync(pathBase);

            return response;
        }
    }
}



