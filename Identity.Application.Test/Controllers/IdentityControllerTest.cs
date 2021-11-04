using Identity.Application.Response;
using IdentityService.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Securrency.Domain.Response;
using Securrency.Identity.Application.Command;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Identity.Application.Test.Controllers
{
    public class IdentityControllerTest
    {


        public IdentityControllerTest()
        {

        }
        [Fact]
        public async Task LoginFailed_Index_ShouldReturnBadRequestAsync()
        {
            //Arrange
            var _mediatorMock = new Mock<IMediator>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(GetLoginInvalidResponseFakeData());
            var logger = Mock.Of<ILogger<IdentityController>>();

            var controller = new IdentityController(logger, _mediatorMock.Object);

            //Act
            var result = await controller.Login(new LoginCommand() { UserName = "admindd1", Password = "password1234" });

            //Assert
            var badObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnValue = Assert.IsType<BaseResponse>(badObjectResult.Value);
            Assert.NotEmpty(returnValue.Message);
        }



        [Fact]
        public async Task LoginSuccessful_Index_ShouldReturnAcccessTokenAsync()
        {
            //Arrange
            var _mediatorMock = new Mock<IMediator>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(GetLoginResponseFakeData());
            var logger = Mock.Of<ILogger<IdentityController>>();

            var controller = new IdentityController(logger, _mediatorMock.Object);
            
            //Act
            var result = await controller.Login(new LoginCommand() { UserName = "admin1", Password = "password1234" });

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<LoginResponse>(okResult.Value);           
            Assert.NotEmpty(returnValue.AccessToken);
        }


        private GenericResponse<LoginResponse> GetLoginResponseFakeData()
        {
            return new GenericResponse<LoginResponse>() { IsSuccessful = true, Message = "Login was successful", ResponseModel = new LoginResponse() { AccessToken = "XXXXXX" } };
        }

        private GenericResponse<LoginResponse> GetLoginInvalidResponseFakeData()
        {
            return new GenericResponse<LoginResponse>() { IsSuccessful = false, Message = "Login Failed", ResponseModel = null};
        }

    }
}
