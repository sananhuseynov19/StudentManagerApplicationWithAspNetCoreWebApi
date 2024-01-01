using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentManagerApplicationWithWebApi.Data.Entities;
using StudentManagerApplicationWithWebApi.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentManagerApplicationWithWebApi.Middlewares
{
    public class AuthMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
      

        public AuthMiddleware(RequestDelegate next,IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if(token != null)
            {
                AttachToStudent(context, token);
            }
            await _next(context);

        }

        public void AttachToStudent(HttpContext context,string token)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWtToken:Jwtkey"]);

            tokenhandler.ValidateToken(token, new TokenValidationParameters
            {
                
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer=false,
                ValidateAudience=false, 
                ValidateIssuerSigningKey=true,
                ClockSkew=TimeSpan.Zero

            },out SecurityToken validatedToken);

            var jwttoken = (JwtSecurityToken)validatedToken;

            var identity = new ClaimsIdentity(jwttoken.Claims);
            var principal = new ClaimsPrincipal(identity);
            context.User = principal;
        }
    }

}
