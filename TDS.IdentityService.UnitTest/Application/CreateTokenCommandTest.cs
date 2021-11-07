using Identity.Application.Handlers;
using MediatR;
using Moq;
using TDS.Domain.Response;
using TDS.IdentityService.Application.Command;
using TDS.IdentityService.Infrastructure.Persistence.Entities;
using TDS.IdentityService.Infrastructure.Persistence.Repository;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using TDS.Identity.Application.Response;
using System.Collections.Generic;
using TDS.IdentityService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TDS.Domain.Extensions;
using Microsoft.Extensions.Options;
using TDS.Domain.Authorization;

namespace Identity.Application.Test
{
    public class CreateTokenCommandTest
    {

        private Mock<IOptions<JWTSettings>> _jwtSettingsMock;
        public Mock<IUnitOfWork> _mockUnitOfWork;

        public CreateTokenCommandTest()
        {

            _jwtSettingsMock = new Mock<IOptions<JWTSettings>>();
            _jwtSettingsMock.Setup(m => m.Value).Returns(GetMockedJWTSettings()); 
        }

     

        [Fact]
        public async Task GenerateToken_ForUser_Returns_Token()
        {



            //Arange


            CreateTokenCommand command = new CreateTokenCommand()
            {
                 UserName="admin1"
            };
            CreateTokenCommandHandler handler = new CreateTokenCommandHandler(_jwtSettingsMock.Object);


            //Act
            var _result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Asert
            //Do the assertion
            Assert.NotEmpty(_result);

        }


        [Fact]
        public async Task GenerateToken_ForEmptyUser_Returns_Empty()
        {



            //Arange
            CreateTokenCommand command = new CreateTokenCommand()
            {
                UserName = ""
            };
            CreateTokenCommandHandler handler = new CreateTokenCommandHandler(_jwtSettingsMock.Object);


            //Act
            var _result = await handler.Handle(command, new System.Threading.CancellationToken());


            //Asert
            //Do the assertion
            Assert.Empty(_result);

        }


        private JWTSettings GetMockedJWTSettings()
        {

            return new JWTSettings()
            {
                Audience = "https://mockedsecurrency.com",
                DateToleranceMinutes = 1,
                Issuer = "https://mockedsecurrency.com",
                SecurityKey = "ThisKeyIsVeryImportantForAuthenticationMocksecurrency.ThatIsIt?",
                TimeOutSeconds = 60,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifeTime = true,
                ValidateSigningKey = true
            };
        }

    }
}
