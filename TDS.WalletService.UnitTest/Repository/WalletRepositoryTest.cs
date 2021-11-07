
using Microsoft.EntityFrameworkCore;
using Moq;
using TDS.WalletService.Infrastructure.Persistence;
using TDS.WalletService.Infrastructure.Persistence.Entities;
using TDS.WalletService.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TDS.WalletService.UnitTest.Repository
{
    public class WalletRepositoryTest
    {
        public Mock<ApplicationDbContext>  _context;
        public Mock<DbSet<Wallet>> _dbSetMock;

        public WalletRepositoryTest()
        {

             _context = new Mock<ApplicationDbContext>();
             _dbSetMock = new Mock<DbSet<Wallet>>();
        }



        [Fact]
        public void Get_WalletObjectPassed_ProperMethodCalled()
        {
            // Arrange
                      

            _context.Setup(x => x.Set<Wallet>()).Returns(_dbSetMock.Object);
            _dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(new Wallet());

            // Act
            var repository = new Repository<Wallet>(_context.Object);
            repository.Get(1);

            // Assert
            _context.Verify(x => x.Set<Wallet>());
            _dbSetMock.Verify(x => x.Find(It.IsAny<int>()));
        }


        [Fact]
        public void GetAll_WalletObjectPassed_ReturnsAll()
        {
            // Arrange

            var _dbSetMock = TestUtility.CreateDbSetMock(MockDataUtilty.GetWalletList());

            var context = new Mock<ApplicationDbContext>();
            _context.Setup(x => x.Set<Wallet>()).Returns(_dbSetMock.Object);

            // Act
            var repository = new Repository<Wallet>(_context.Object);
            var result = repository.GetAll();

            // Assert
            Assert.Equal(3, result.Count());
        }




        [Fact]
        public async Task Find_ValidAddresss_ReturnWallet()
        {


            var _dbSetMock = TestUtility.CreateDbSetMock(MockDataUtilty.GetWalletList());
            var context = new Mock<ApplicationDbContext>();
            _context.Setup(x => x.Set<Wallet>()).Returns(_dbSetMock.Object);

            var repository = new Repository<Wallet>(_context.Object);

            var result = await repository.Find(x => x.Address== "1111-1111-1111-1111");
            
            Assert.Single(result.ToList());
        }

        [Fact]
        public async Task Find_InvalidAddress_ReturnEmpty()
        {


            var _dbSetMock = TestUtility.CreateDbSetMock(MockDataUtilty.GetWalletList());
            var context = new Mock<ApplicationDbContext>();
            _context.Setup(x => x.Set<Wallet>()).Returns(_dbSetMock.Object);

            var repository = new Repository<Wallet>(_context.Object);

            var result = await repository.Find(x => x.Address == "6666-66666-6666-6666" );

            Assert.False(result.Any());
        }


        

    }
}
