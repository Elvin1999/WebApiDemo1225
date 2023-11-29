﻿using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text;

namespace WebApiDemo1225.MiddleWares
{
    public class Show404Requirement : IAuthorizationRequirement { }
    public class SampleAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();

        public async Task HandleAsync(
            RequestDelegate next,
            HttpContext context,
            AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader == null || authHeader.Trim() == "")
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            //basic elvin:12345
            if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring(6).Trim();
                string credentialString = "";
                try
                {
                    credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                }
                catch (Exception)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    return;
                }

                var credentials = credentialString.Split(':');

                var username = credentials[0];
                var password = credentials[1];

                if (username == "eynal123" && password == "eynal123")// for testing purpose
                {
                    var claims = new[]
                    {
                        new Claim("username",username),
                        new Claim(ClaimTypes.Role,"Admin")
                    };

                    var identity = new ClaimsIdentity(claims, "Basic");

                    context.User = new ClaimsPrincipal(identity);

                    context.Response.StatusCode = StatusCodes.Status200OK;
                    await next(context);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}
