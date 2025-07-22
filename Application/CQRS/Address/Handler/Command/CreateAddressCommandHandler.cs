using AutoMapper;
using ECX.HR.Application.Contracts.Persistence;
using ECX.HR.Application.Contracts.Persistent;
using ECX.HR.Application.CQRS.Addresss.Request.Command;
using ECX.HR.Application.CQRS.Departments.Request.Command;
using ECX.HR.Application.DTOs.Address.Validator;


using ECX.HR.Application.Exceptions;

using ECX.HR.Application.Response;
using ECX.HR.Domain;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ECX.HR.Application.CQRS.Address.Handler.Command
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, BaseCommandResponse>
    {
        BaseCommandResponse response;
        private IAddressRepository _AddressRepository;
        private IMapper _mapper;
        public CreateAddressCommandHandler(IAddressRepository AddressRepository, IMapper mapper)
        {
            _AddressRepository = AddressRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            response = new BaseCommandResponse();
            var validator =new AddressDtoValidator();
            var validationResult =await validator.ValidateAsync(request.AdressDetailDto);

            if (validationResult.IsValid == false)
            {
                var error = "";
                for (int i = 0; i < validationResult.Errors.Count; i++)
                {
                    error = error + validationResult.Errors[i];
                    if (i != validationResult.Errors.Count - 1)
                    {
                        error = error + ", ";
                    }
                }
                response.Success = false;
                response.Message = error;
                response.Status = "404";
            }
            else {
                try
                {
                    var Address = _mapper.Map<Domain.Address>(request.AdressDetailDto);
                    Address.Id = Guid.NewGuid();
                    var add = Address.Id;
                    var data = await _AddressRepository.Add(Address);
                    var emp = await _AddressRepository.GetById(Address.Id);
                    if (emp != null)
                    {
                        response.Success = true;
                        response.Message = "Successfully Created";
                        response.Status = "200";
                        response.Id = (Guid)Address.Id;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Error";
                        response.Status = "404";
                    }
                 
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
