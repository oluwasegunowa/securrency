
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
using TDS.WalletService.Application.Requests;

namespace Identity.Application.Test
{
    public class UploadWalletAddressesCommandTest
    {

        private Mock<IWalletTransactionDiscoveryService> _walletTransactionServiceMock;
        public Mock<IUnitOfWork> _mockUnitOfWork;

        public UploadWalletAddressesCommandTest()
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
        public async Task AddressListNull_Returns_Failure()
        {



            //Arange

            //NULL
            


            UploadWalletAddressesCommand command = new UploadWalletAddressesCommand()
            {
                WalletAddressModels =null
            };
            UploadWalletAddressesHandler handler = new UploadWalletAddressesHandler(_walletTransactionServiceMock.Object, _mockUnitOfWork.Object);


            //Act
            var _result = await handler.Handle(command, new CancellationToken());

            //Asert
            //Do the assertion

            Assert.NotNull(_result);
            Assert.False(_result.IsSuccessful); //It has to be false 
        }



        [Fact]
        public async Task AddressListEmpty_Returns_Failure()
        {



            //Arange

            //NULL



            UploadWalletAddressesCommand command = new UploadWalletAddressesCommand()
            {
                WalletAddressModels = new List<WalletAddressModel>()
            };
            UploadWalletAddressesHandler handler = new UploadWalletAddressesHandler(_walletTransactionServiceMock.Object, _mockUnitOfWork.Object);


            //Act
            var _result = await handler.Handle(command, new CancellationToken());

            //Asert
            //Do the assertion

            Assert.NotNull(_result);
            Assert.False(_result.IsSuccessful); //It has to be false 
        }



        //[Fact]
        //public async Task NewAddressList_Returns_Successful()
        //{



        //    //Arange

        //    //NULL



        //    UploadWalletAddressesCommand command = new UploadWalletAddressesCommand()
        //    {
        //        WalletAddressModels = new List<WalletAddressModel>()
        //        {
        //            new WalletAddressModel(){ Address="2222-2222-2222-222" },
        //             new WalletAddressModel(){ Address="6666-66666-6666-6666" },
        //              new WalletAddressModel(){ Address="1111-1111-1111-111" }

        //        }
        //    };
        //    UploadWalletAddressesHandler handler = new UploadWalletAddressesHandler(_walletTransactionServiceMock.Object, _mockUnitOfWork.Object);


        //    //Act
        //    var _result = await handler.Handle(command, new CancellationToken());

        //    //Asert
        //    //Do the assertion

        //    Assert.NotNull(_result);
        //    Assert.True(_result.IsSuccessful);
        //    Assert.NotNull(_result.ResponseModel);
        //    Assert.True(_result.ResponseModel.UploadEntriesCount>0);
        //}



    }
}
