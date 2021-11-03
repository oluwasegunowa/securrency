//using Identity.Application.Handlers;
//using Identity.Application.Response;
//using MediatR;
//using Moq;
//using Securrency.Domain.Response;
//using Securrency.Identity.Application.Command;
//using SecurrencyTDS.IdentityService.Infrastructure.Persistence.Repository;
//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace Identity.Application.Test
//{
//    public class LoginCommandTest
//    {

//        private Mock<IMediator> _mediator;
//        public Mock<IUnitOfWork> _mockUnitOfWork;

//        public LoginCommandTest()
//        {
//             _mediator = new Mock<IMediator>();
//             _mockUnitOfWork = new Mock<IUnitOfWork>();



//            _mediator.Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<GenericResponse<LoginResponse>>());


//        }
//        [Fact]
//        public async Task ConfirmLogin_Sucessful()
//        {
           


//            //Arange

//            LoginCommand command = new LoginCommand() { 
//               UserName="InvalidUser",
//               Password="InvalidPassord"
//            };
//            LoginHandler handler = new LoginHandler(_mockUnitOfWork.Object, _mediator.Object);

//            //Act
//            var _result = await handler.Handle(command, new System.Threading.CancellationToken());

//            //Asert
//            //Do the assertion

//            Assert.NotNull(_result);
//            Assert.True(_result?.IsSuccessful,"Invalid USer");

//        }
//    }
//}
