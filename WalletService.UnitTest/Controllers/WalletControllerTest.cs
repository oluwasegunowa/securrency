﻿
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Securrency.Domain.Response;
using Securrency.Wallet.Controllers;
using SecurrencyTDS.WalletManager.Application.Command;
using SecurrencyTDS.WalletManager.Application.Requests;
using SecurrencyTDS.WalletManager.Application.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace WalletService.Application.Test.Controllers
{
    public class WalletControllerTest
    {
      
        public WalletControllerTest()
        {

        }
        [Fact]
        public async Task UploadFailed_ShouldReturnBadRequestAsync()
        {
            //Arrange
            var _mediatorMock = new Mock<IMediator>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<UploadWalletAddressesCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(GetInvalidResponseFakeData());
            var logger = Mock.Of<ILogger<WalletController>>();

            var controller = new WalletController(logger, _mediatorMock.Object);

            //Act
            var result = await controller.UploadWallet(new UploadWalletAddressesCommand() { WalletAddressModels =AddressListFakeData});

            //Assert
            var badObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnValue = Assert.IsType<BaseResponse>(badObjectResult.Value);
            Assert.NotEmpty(returnValue.Message);
        }



        [Fact]
        public async Task AddressUploadSuccessful_ShouldReturnOkResultAsync()
        {
            //Arrange
            var _mediatorMock = new Mock<IMediator>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<UploadWalletAddressesCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(GeResponseFakeData());
            var logger = Mock.Of<ILogger<WalletController>>();

            var controller = new WalletController(logger, _mediatorMock.Object);

            //Act
            var result = await controller.UploadWallet(new UploadWalletAddressesCommand() { WalletAddressModels = AddressListFakeData });

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<UploadWalletResponse>(okResult.Value);           
            Assert.True(returnValue.SuccessfulUpload>0);
        }


        private List<WalletAddressModel> AddressListFakeData {

            get {
                List<WalletAddressModel> fakeList = new List<WalletAddressModel>();
                fakeList.Add(new WalletAddressModel {  Address="32823832303023"});
                fakeList.Add(new WalletAddressModel { Address = "3333333333333" });
                fakeList.Add(new WalletAddressModel { Address = "4444444444444444" });
                fakeList.Add(new WalletAddressModel { Address = "55555555555555" });
                fakeList.Add(new WalletAddressModel { Address = "66666666666666" });
                fakeList.Add(new WalletAddressModel { Address = "777777777777777" });
                return fakeList;
            }
            
        
        }


        private GenericResponse<UploadWalletResponse> GeResponseFakeData()
        {
            return new GenericResponse<UploadWalletResponse>()
            {
                IsSuccessful = true,
                Message = "Wallet addresses uploaded.",
                ResponseModel = new UploadWalletResponse
                {

                    UploadEntriesCount = 4,
                    DuplicateEntriesCount = 1
                }
            };
        }

        private GenericResponse<UploadWalletResponse> GetInvalidResponseFakeData()
        {
            return new GenericResponse<UploadWalletResponse>() { IsSuccessful = false, Message = "Upload fail with errors", ResponseModel = null};
        }

    }
}
