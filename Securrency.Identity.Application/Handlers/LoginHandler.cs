using Identity.Application.Response;
using MediatR;
using Securrency.Domain.Response;
using Securrency.Identity.Application.Command;
using SecurrencyTDS.Domain.Extensions;
using SecurrencyTDS.IdentityService.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, GenericResponse< LoginResponse>>
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMediator _mediator;
        public LoginHandler(
            IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<GenericResponse<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            
            var retUser = (await _unitOfWork.UserRepository.Find(f => f.UserName == request.UserName && f.Password == request.Password.GetSHA512())).FirstOrDefault();

            if (retUser == null)
            {
                return new GenericResponse<LoginResponse>() { IsSuccessful = false, Message = "Either Username of password is not correct." };
            }

            //User does not exist
            return new GenericResponse<LoginResponse>() {  IsSuccessful=true, Message="Login is successful", ResponseModel=new LoginResponse() { AccessToken = await _mediator.Send(new CreateTokenCommand() { UserName = request.UserName }) } };

        }
    }
}
