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

namespace Identity.Application.Test
{
    public class LoginCommandTest
    {

        private Mock<IMediator> _mediatorMock;
        public Mock<IUnitOfWork> _mockUnitOfWork;

        public LoginCommandTest()
        {

            _mediatorMock = new Mock<IMediator>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
         
            
            var dbSetMock = CreateDbSetMock(GetUsersList());
            var context = new Mock<ApplicationDbContext>();
            context.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);
            var repository = new Repository<User>(context.Object);
            _mockUnitOfWork.Setup(m => m.UserRepository).Returns(repository);          


            // _mediatorMock.Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<GenericResponse<LoginResponse>>());



            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTokenCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<string>());


        


        }
        //private Mock<IRepository<T>> CreateMock<T>() where T : class
        //{
        //    var mock = new Mock<IRepository<T>>();
        //    // do more common magic here
          
        //    return mock;
        //}


        [Fact]
        public async Task ConfirmLogin_WithValid_Credentials_Returns_Sucessful()
        {



            //Arange
           

            LoginCommand command = new LoginCommand()
            {
                UserName = "admin1",
                Password = "Pass1"
            };
            LoginHandler handler = new LoginHandler(_mockUnitOfWork.Object, _mediatorMock.Object);


            //Act
            var _result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Asert
            //Do the assertion

            Assert.NotNull(_result);
            Assert.True(_result?.IsSuccessful, "The login was successful");

        }


        [Fact]
        public async Task ConfirmLogin_WithInValid_Credentials_Returns_LoginFailed()
        {



            //Arange


            LoginCommand command = new LoginCommand()
            {
                UserName = "adminxx",
                Password = "Passxx"
            };
            LoginHandler handler = new LoginHandler(_mockUnitOfWork.Object, _mediatorMock.Object);


            //Act
            var _result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Asert
            //Do the assertion
            Assert.NotNull(_result);
            Assert.False(_result?.IsSuccessful, "The login was not successful");

        }


        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
            return dbSetMock;
        }

        private IEnumerable<User> GetUsersList()
        {
            var retList = new List<User>
        {
            new User {Id = 1,  UserName = "admin1",  Password = "Pass1".GetSHA512()},
            new User {Id = 2,  UserName = "admin2",  Password = "Pass2".GetSHA512()},
            new User {Id = 3, UserName = "admin3",  Password = "Pass3".GetSHA512()}
        };

            return retList;
        }
    }
}
