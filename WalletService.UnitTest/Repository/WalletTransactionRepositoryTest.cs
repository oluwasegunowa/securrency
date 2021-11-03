
using Microsoft.EntityFrameworkCore;
using Moq;
using SecurrencyTDS.WalletManager.Infrastructure.Persistence;
using SecurrencyTDS.WalletManager.Infrastructure.Persistence.Entities;
using SecurrencyTDS.WalletManager.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WalletTransactionService.Application.Test
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

            var _dbSetMock = CreateDbSetMock(GetWalletTransactionList());

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
            var transactionList = GetWalletTransactionList();
            var _dbSetMock = CreateDbSetMock(transactionList);
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
            var _dbSetMock = CreateDbSetMock(GetWalletTransactionList());
            var context = new Mock<ApplicationDbContext>();
            _context.Setup(x => x.Set<WalletTransaction>()).Returns(_dbSetMock.Object);
            var repository = new Repository<WalletTransaction>(_context.Object);


            // Act

            var result = await repository.Find(x => x.Sender == invalidWallet  || x.Receiver== invalidWallet);


            //Assert
            Assert.False(result.Any());
        }


        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var _dbSetMock = new Mock<DbSet<T>>();
            _dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            _dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            _dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            _dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
            return _dbSetMock;
        }

        private IEnumerable<WalletTransaction> GetWalletTransactionList()
        {
            var retList = new List<WalletTransaction>
        {
            new WalletTransaction {Id = 1,  Sender = "1111-1111-1111-1111", Receiver="3333-33333-3333-333-",  AssetCode ="XLM",   TxnAmount=100},
            new WalletTransaction {Id = 2,  Sender = "2222-2222-2222-222",  Receiver= "1111-1111-1111-1111",AssetCode ="XLM",   TxnAmount=100},
            new WalletTransaction {Id = 3, Sender = "3333-33333-3333-333-", Receiver="2222-2222-2222-222", AssetCode ="XLM",   TxnAmount=100},
             new WalletTransaction {Id = 4,  Sender = "1111-1111-1111-1111", Receiver="2222-2222-2222-222", AssetCode ="XLM",     TxnAmount=100},
            new WalletTransaction {Id = 5,  Sender = "2222-2222-2222-222",  Receiver="1111-1111-1111-1111", AssetCode ="XLM",  TxnAmount=100},
            new WalletTransaction {Id = 6, Sender = "3333-33333-3333-333-",  Receiver="2222-2222-2222-222", AssetCode ="XLM", TxnAmount=100},
             new WalletTransaction {Id = 7,  Sender = "1111-1111-1111-1111", Receiver="2222-2222-2222-222",AssetCode ="XLM",     TxnAmount=100},
            new WalletTransaction {Id = 8,  Sender = "2222-2222-2222-222",  Receiver="1111-1111-1111-1111", AssetCode ="XLM",  TxnAmount=100},
            new WalletTransaction {Id = 9, Sender = "3333-33333-3333-333-",  Receiver="1111-1111-1111-1111",AssetCode ="XLM",  TxnAmount=100},
             new WalletTransaction {Id = 10,  Sender = "1111-1111-1111-1111", Receiver="3333-33333-3333-333-",AssetCode ="XLM",     TxnAmount=100},
            new WalletTransaction {Id = 11,  Sender = "2222-2222-2222-222",  Receiver="3333-33333-3333-333-", AssetCode ="XLM",  TxnAmount=100},
            new WalletTransaction {Id = 12, Sender = "3333-33333-3333-333-", Receiver="1111-1111-1111-1111", AssetCode ="XLM",  TxnAmount=100}

        };

            return retList;
        }


    }
}
