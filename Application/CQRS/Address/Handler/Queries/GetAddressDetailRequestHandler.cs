using AutoMapper;
using ECX.HR.Application.Contracts.Persistence;
using ECX.HR.Application.Contracts.Persistent;
using ECX.HR.Application.CQRS.Addresss.Request.Queries;

using ECX.HR.Application.DTOs.Addresss;
using ECX.HR.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.HR.Application.CQRS.Addresss.Handler.Queries
{
    public class GetAddressDetailRequestHandler : IRequestHandler<GetAddressDetailRequest, AddressDto>
    {
        private IAddressRepository _AddressRepository;
        private  IEmployeeRepository _employeeRepository;
        private IMapper _mapper;
        public GetAddressDetailRequestHandler(IAddressRepository AddressRepository,IEmployeeRepository EmployeeRepository ,IMapper mapper)
        {
           _AddressRepository = AddressRepository;
            _employeeRepository = EmployeeRepository;
            _mapper = mapper;
        }
        public async Task<AddressDto> Handle(GetAddressDetailRequest request, CancellationToken cancellationToken)
        {
            var address = await _AddressRepository.GetByEmpId(request.EmpId);

            //if (address == null || address.Status != 1)
            //    throw new NotFoundException(nameof(address), request.EmpId);

            return _mapper.Map<AddressDto>(address);
        }

    }
}
