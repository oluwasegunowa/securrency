using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securrency.Identity.Application.Command
{
   public  class CreateTokenCommand: IRequest<string>
    {

        public string UserName { get; set; }
    }
}
