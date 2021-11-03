﻿using MediatR;
using Securrency.Domain.Response;
using SecurrencyTDS.WalletManager.Application.Command;
using SecurrencyTDS.WalletManager.Application.Response;
using SecurrencyTDS.WalletManager.Infrastructure.Integrations;
using SecurrencyTDS.WalletManager.Infrastructure.Persistence.Entities;
using SecurrencyTDS.WalletManager.Infrastructure.Persistence.Repository;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecurrencyTDS.WalletManager.Application.Handlers
{


    public class UploadWalletAddressesHandler : IRequestHandler<UploadWalletAddressesCommand, GenericResponse<UploadWalletResponse>>
    {

        private readonly IWalletTransactionDiscoveryService _walletTransactionDiscoveryService;

        private readonly IUnitOfWork _unitOfWork;
        public UploadWalletAddressesHandler(IWalletTransactionDiscoveryService walletTransactionDiscoveryService, IUnitOfWork unitOfWork)
        {
            _walletTransactionDiscoveryService = walletTransactionDiscoveryService;
            _unitOfWork = unitOfWork;
        }
        public async Task<GenericResponse<UploadWalletResponse>> Handle(UploadWalletAddressesCommand request, CancellationToken cancellationToken)
        {


            if(request.WalletAddressModels==null || request.WalletAddressModels.Count == 0)
            {
                return new GenericResponse<UploadWalletResponse>() { IsSuccessful=false, Message="Wallet address data is empty. You must upload atleast one address." };
            }


            foreach (var wallet in request.WalletAddressModels)
            {
                var existingWallet = (await _unitOfWork.WalletRepository.Find(f => f.Address == wallet.Address)).FirstOrDefault();
                if (existingWallet != null)
                {
                    continue;
                 //   return new GenericResponse<UploadWalletResponse>() { IsSuccessful = false, Message = $"A wallet with the same address: '{existingWallet.Address} 'already exist. Please check the data and try again." };
                }

                Wallet newwaller = new Wallet()
                {
                    DateCreated = DateTime.Now, 
                    Address = wallet.Address,
                    IsActive = true,
                     
                };
                await _unitOfWork.WalletRepository.AddAsync(newwaller);
            }

            
           await  _unitOfWork.Complete();


            return new GenericResponse<UploadWalletResponse>()
            {
                IsSuccessful = true,
                Message = "The list has been uploaded successfully",
                
            };
        }
    }
}
