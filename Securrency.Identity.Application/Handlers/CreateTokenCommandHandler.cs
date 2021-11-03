
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Securrency.Identity.Application.Command;
using SecurrencyTDS.Domain.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Handlers
{
    public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, string>
    {


        private readonly IOptions<JWTSettings> _jwtSettings;

        public CreateTokenCommandHandler(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings;

        }
        public async Task<string> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {


                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.SecurityKey));

                var claims = new Claim[] { new Claim(ClaimTypes.NameIdentifier, $"{request.UserName.ToString()}")};
                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Value.Issuer,
                    audience: _jwtSettings.Value.Audience,
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires:   DateTime.Now.AddMinutes(_jwtSettings.Value.TimeOutSeconds),
                    signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
                );

                string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return await Task.FromResult(jwtToken);
            }



        
    }
}
