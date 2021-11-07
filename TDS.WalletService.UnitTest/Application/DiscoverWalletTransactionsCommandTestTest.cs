
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TDS.Domain.Extensions;
using TDS.WalletService.Infrastructure.Persistence.Repository;
using TDS.WalletService.Application.Command;
using TDS.WalletService.Application.Handlers;
using TDS.WalletService.Infrastructure.Integrations;
using TDS.Domain.Models;
using TDS.WalletService.Infrastructure.Persistence;
using TDS.WalletService.Infrastructure.Persistence.Entities;
using System;
using TDS.WalletService.UnitTest;

namespace Identity.Application.Test
{
    public class DiscoverWalletTransactionsCommandTestTest
    {

        private Mock<IWalletTransactionDiscoveryService> _walletTransactionServiceMock;
        public Mock<IUnitOfWork> _mockUnitOfWork;

        public DiscoverWalletTransactionsCommandTestTest()
        {

            _walletTransactionServiceMock = new Mock<IWalletTransactionDiscoveryService>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
          
            //Mock service
            _walletTransactionServiceMock.Setup(m =>m.GetLatestTransactions(It.IsAny<string>(), It.IsAny<string>())).
                ReturnsAsync(MockDataUtilty.GetStellarWalletTransactionMockedData().ToList());




            //Mock WalletTransaction repository
            var dbSetMock = TestUtility.CreateDbSetMock(MockDataUtilty.GetWalletTransactionList());
            var context = new Mock<ApplicationDbContext>();
            context.Setup(x => x.Set<WalletTransaction>()).Returns(dbSetMock.Object);
            var repository = new Repository<WalletTransaction>(context.Object);
            _mockUnitOfWork.Setup(m => m.WalletTransactionRepository).Returns(repository);


            //Mock Wallet Repository
            var dbWalletSetMock = TestUtility.CreateDbSetMock(MockDataUtilty.GetWalletList());
            context.Setup(x => x.Set<Wallet>()).Returns(dbWalletSetMock.Object);
            var walletRepository = new Repository<Wallet>(context.Object);
            _mockUnitOfWork.Setup(m => m.WalletRepository).Returns(walletRepository);

        }



        [Fact]
        public async Task ScanWallletForTransactions_Returns_Stats()
        {



            //Arange


            DiscoverWalletTransactionsCommand command = new DiscoverWalletTransactionsCommand()
            {
               
            };
            DiscoverWalletTransactionsHandler handler = new DiscoverWalletTransactionsHandler(_walletTransactionServiceMock.Object, _mockUnitOfWork.Object);


            //Act
            var _result = await handler.Handle(command, new CancellationToken());

            //Asert
            //Do the assertion

            Assert.NotNull(_result);
            Assert.True(_result.NoOfWalletScanned>0);
        }




      
    }
}
