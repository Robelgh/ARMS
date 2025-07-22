using AutoMapper;
using ECX.HR.Application.Contracts.Persistence;
using ECX.HR.Application.Contracts.Persistent;
using ECX.HR.Application.CQRS.Addresss.Request.Command;
using ECX.HR.Application.DTOs.Address.Validator;
using ECX.HR.Application.Exceptions;
using ECX.HR.Application.Response;
using ECX.HR.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.HR.Application.CQRS.Addresss.Handler.Command
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, BaseCommandResponse>
    {
       
        BaseCommandResponse response;
        private IAddressRepository _AddressRepository;
        private IMapper _mapper;

        public UpdateAddressCommandHandler(IAddressRepository AddressRepository, IMapper mapper)
        {
            _AddressRepository = AddressRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            response=new BaseCommandResponse();
            var validator = new AddressDtoValidator();
            var validationResult = await validator.ValidateAsync(request.AddressDto);
            if (validationResult.IsValid == false)
            {
                var error = "";
                for (int i = 0; i < validationResult.Errors.Count; i++) {
                    error = error + validationResult.Errors[i];
                    if(i != validationResult.Errors.Count - 1){
                        error = error + ", ";
                }
                }
                response.Success = false;
                response.Message = error;
                response.Status = "400";
            }
            else
            {

                request.AddressDto.UpdatedDate = DateTime.Now;
                var address = await _AddressRepository.GetById(request.AddressDto.Id);



                var add = _mapper.Map(request.AddressDto, address);
                try
                {
                    await _AddressRepository.Update(add);
                    response.Success = true;
                    response.Message = "Successfully Updated";
                    response.Status = "200";
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                    response.Status = "404";
                }
            }
            return response;
        }
    }
}

