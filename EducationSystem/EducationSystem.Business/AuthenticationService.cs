using EducationSystem.Core.Authentication;
using EducationSystem.Core.Enums;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EducationSystem.Business
{
    public class AuthenticationService : IAuthenticationService
    {
        private IUserRepository _repo;
        public AuthenticationService(IUserRepository userRepository)
        {
            _repo = userRepository;
        }
        public UserDto GetAuthentificatedUser(string login)
        {
            return _repo.CheckUser(login);
        }
        public string GenerateToken(UserDto user)
        {
            var identity = GetIdentity(user);
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                user.Id
            };
            return response.ToString();
        }
        private ClaimsIdentity GetIdentity(UserDto user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
                };
            foreach(Role role in user.Roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, FriendlyNames.GetFriendlyRoleName(role)));
            }
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
