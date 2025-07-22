using AutoMapper;
using ECX.HR.Application.CQRS.Addresss.Request.Command;
using ECX.HR.Application.Exceptions;
using ECX.HR.Application.Contracts.Persistent;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECX.HR.Application.Contracts.Persistence;
using ECX.HR.Application.Response;

namespace ECX.HR.Application.CQRS.Addresss.Handler.Command
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand,BaseCommandResponse>
    {
        BaseCommandResponse response;
        private IAddressRepository _AddressRepository;
        private IMapper _mapper;
        public DeleteAddressCommandHandler(IAddressRepository AddressRepository, IMapper mapper)
        {
            _AddressRepository = AddressRepository;
            _mapper = mapper;
        }


        public async Task<BaseCommandResponse> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            response = new BaseCommandResponse();
            var address = await _AddressRepository.GetById(request.Id);
            address.Status = 0;
            try
            {
                await _AddressRepository.Update(address);
                response.Success = true;
                response.Message = "Successfully Deleted";
                response.Status = "200";
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;
                response.Status = "400";
            }

            return response;
        }
  
    }
}
