//using Identity.Application.Handlers;
//using Identity.Application.Response;
//using MediatR;
//using Moq;
//using TDS.Domain.Response;
//using TDS.IdentityService.Application.Command;
//using TDS.IdentityService.Infrastructure.Persistence.Entities;
//using TDS.IdentityService.Infrastructure.Persistence.Repository;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace Identity.Application.Test
//{
//    public class LoginCommandTest
//    {

//        private Mock<IMediator> _mediatorMock;
//        public Mock<IUnitOfWork> _mockUnitOfWork;

//        public LoginCommandTest()
//        {
//            _mediatorMock = new Mock<IMediator>();
//            _mockUnitOfWork = new Mock<IUnitOfWork>();


//            _mediatorMock.Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<GenericResponse<LoginResponse>>());




//            var userRepositoryMock = new Mock<IRepository<User>>();
//            _mockUnitOfWork.Setup(m => m.UserRepository).Returns(userRepositoryMock.Object);


//        }
//        [Fact]
//        public async Task ConfirmLogin_Sucessful()
//        {


            
//            //Arange
//            CreateTokenCommandHandler createTokenCommandHandler = new CreateTokenCommandHandler(null);
//            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTokenCommand>(), It.IsAny<CancellationToken>()))
//                   .Returns(async (CreateTokenCommandHandler q, CancellationToken token) => await createTokenCommandHandler.Handle(new CreateTokenCommand { UserName = "segun" }, token));



//            LoginCommand command = new LoginCommand()
//            {
//                UserName = "InvalidUser",
//                Password = "InvalidPassord"
//            };
//            LoginHandler handler = new LoginHandler(_mockUnitOfWork.Object, _mediatorMock.Object);


//            //Act
//            var _result = await handler.Handle(command, new System.Threading.CancellationToken());

//            //Asert
//            //Do the assertion

//            Assert.NotNull(_result);
//            Assert.True(_result?.IsSuccessful, "Invalid USer");

//        }


    
//    ////[Fact]
//    ////public async Task CreateToken_Sucessful()
//    ////{



//    ////        //Arange

//    ////        string userName = "admin1";
//    ////        CreateTokenCommandHandler createTokenCommandHandler = new CreateTokenCommandHandler(null);

//    ////    _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTokenCommand>(), It.IsAny<CancellationToken>()))
//    ////           .Returns(async (CreateTokenCommandHandler q, CancellationToken token) => await createTokenCommandHandler.Handle(new  CreateTokenCommand { UserName = userName }, token));



//    ////    //Act
//    ////    var _result = await createTokenCommandHandler.Handle(command, new System.Threading.CancellationToken());

//    ////    //Asert
//    ////    //Do the assertion

//    ////    Assert.NotNull(_result);
//    ////    Assert.True(_result?.IsSuccessful, "Invalid USer");

//    ////}
//}
//}
