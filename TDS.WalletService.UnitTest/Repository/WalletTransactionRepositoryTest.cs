
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
    public class WalletTransactionRepositoryTest
    {
        public Mock<ApplicationDbContext>  _context;
        public Mock<DbSet<WalletTransaction>> _dbSetMock;

        public WalletTransactionRepositoryTest()
        {

             _context = new Mock<ApplicationDbContext>();
             _dbSetMock = new Mock<DbSet<WalletTransaction>>();
        }



        [Fact]
        public void Get_WalletTransactionObjectPassed_ProperMethodCalled()
        {
            // Arrange                      

            _context.Setup(x => x.Set<WalletTransaction>()).Returns(_dbSetMock.Object);
            _dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(new WalletTransaction());

            // Act
            var repository = new Repository<WalletTransaction>(_context.Object);
            repository.Get(1);

            // Assert
            _context.Verify(x => x.Set<WalletTransaction>());
            _dbSetMock.Verify(x => x.Find(It.IsAny<int>()));
        }


        [Fact]
        public void GetAll_WalletTransactionObjectPassed_ReturnsAll()
        {
            // Arrange

            var _dbSetMock = TestUtility.CreateDbSetMock(MockDataUtilty.GetWalletTransactionList());

            var context = new Mock<ApplicationDbContext>();
            _context.Setup(x => x.Set<WalletTransaction>()).Returns(_dbSetMock.Object);

            // Act
            var repository = new Repository<WalletTransaction>(_context.Object);
            var result = repository.GetAll();

            // Assert
            Assert.Equal(12, result.Count());
        }




        [Fact]
        public async Task Find_ValidAddresssWithTransactions_ReturnWalletTransactions()
        {

            // Arrange

            var validWallet = "1111-1111-1111-1111";
            var transactionList = MockDataUtilty.GetWalletTransactionList();
            var _dbSetMock = TestUtility.CreateDbSetMock(transactionList);
            var context = new Mock<ApplicationDbContext>();
            _context.Setup(x => x.Set<WalletTransaction>()).Returns(_dbSetMock.Object);
            var repository = new Repository<WalletTransaction>(_context.Object);

            //Count list using linq in preparation for asertion

            var totalCount = transactionList.Where(x => x.Sender == validWallet || x.Receiver == validWallet).Count();

            // Act
            var result = await repository.Find(x => x.Sender == validWallet || x.Receiver == validWallet);

            //Assert
            Assert.True(result.Any());
            Assert.Equal(totalCount, result.Count());
        }

        [Fact]
        public async Task Find_NonExistentWalletId_ReturnEmptyTransactions()
        {
            // Arrange

            var invalidWallet = "6666-66666-6666-6666";
            var _dbSetMock = TestUtility.CreateDbSetMock(MockDataUtilty.GetWalletTransactionList());
            var context = new Mock<ApplicationDbContext>();
            _context.Setup(x => x.Set<WalletTransaction>()).Returns(_dbSetMock.Object);
            var repository = new Repository<WalletTransaction>(_context.Object);


            // Act

            var result = await repository.Find(x => x.Sender == invalidWallet  || x.Receiver== invalidWallet);


            //Assert
            Assert.False(result.Any());
        }



       

    }
}
