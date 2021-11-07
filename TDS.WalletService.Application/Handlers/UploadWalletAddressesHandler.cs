using MediatR;
using TDS.Domain.Response;
using TDS.WalletService.Application.Command;
using TDS.WalletService.Application.Response;
using TDS.WalletService.Infrastructure.Integrations;
using TDS.WalletService.Infrastructure.Persistence.Entities;
using TDS.WalletService.Infrastructure.Persistence.Repository;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TDS.WalletService.Application.Handlers
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

            var uploadresponse = new UploadWalletResponse()
            {
                UploadEntriesCount = request.WalletAddressModels.Count
            };
            foreach (var wallet in request.WalletAddressModels)
            {
                var existingWallet = (await _unitOfWork.WalletRepository.Find(f => f.Address == wallet.Address)).FirstOrDefault();
                if (existingWallet != null)
                {
                    uploadresponse.DuplicateEntriesCount++;
                    continue;
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
                 ResponseModel= uploadresponse,
                Message = "The list has been uploaded successfully",
                
            };
        }
    }
}
