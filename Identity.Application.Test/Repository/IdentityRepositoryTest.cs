using Microsoft.EntityFrameworkCore;
using Moq;
using SecurrencyTDS.Domain.Extensions;
using SecurrencyTDS.IdentityService.Infrastructure.Persistence;
using SecurrencyTDS.IdentityService.Infrastructure.Persistence.Entities;
using SecurrencyTDS.IdentityService.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Identity.Application.Test
{
    public class IdentityRepositoryTest
    {
       
        public IdentityRepositoryTest()
        {
            //var moviesMock = CreateDbSetMock(GetFakeListOfMovies());
            //var mockDbContext = new Mock<MovieDbContext>();
            //mockDbContext.Setup(x => x.Movies).Returns(moviesMock.Object);
            //_sut = new MovieRepository(mockDbContext.Object);
        }





        [Fact]
        public void Get_UserObjectPassed_ProperMethodCalled()
        {
            // Arrange


            var context = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<User>>();

            context.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(new User());

            // Act
            var repository = new Repository<User>(context.Object);
            repository.Get(1);

            // Assert
            context.Verify(x => x.Set<User>());
            dbSetMock.Verify(x => x.Find(It.IsAny<int>()));
        }


        [Fact]
        public void GetAll_UserObjectPassed_ReturnsAll()
        {
            // Arrange

            var dbSetMock = CreateDbSetMock(GetUserListOfMovies());

            var context = new Mock<ApplicationDbContext>();
            context.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            // Act
            var repository = new Repository<User>(context.Object);
            var result = repository.GetAll();

            // Assert
            Assert.Equal(3, result.Count());
        }




        [Fact]
        public async Task Find_ValidUserObjectPassed_ReturnUser()
        {


            var dbSetMock = CreateDbSetMock(GetUserListOfMovies());
            var context = new Mock<ApplicationDbContext>();
            context.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            var repository = new Repository<User>(context.Object);

            var result = await repository.Find(x => x.UserName== "admin1" && x.Password== "Pass1".GetSHA512());
            
            Assert.Single(result.ToList());
        }

        [Fact]
        public async Task Find_InvalidUserObjectPassed_ReturnNoUser()
        {


            var dbSetMock = CreateDbSetMock(GetUserListOfMovies());
            var context = new Mock<ApplicationDbContext>();
            context.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            var repository = new Repository<User>(context.Object);

            var result = await repository.Find(x => x.UserName == "admin4" && x.Password == "Pass1".GetSHA512());

            Assert.False(result.Any());
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

        private IEnumerable<User> GetUserListOfMovies()
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
