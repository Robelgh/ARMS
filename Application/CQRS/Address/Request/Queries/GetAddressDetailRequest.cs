
using ECX.HR.Application.DTOs.Addresss;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.HR.Application.CQRS.Addresss.Request.Queries
{
    public class GetAddressDetailRequest :IRequest<AddressDto>
    {
      public Guid Id { get; set; }
        public Guid EmpId { get; set; }
        public int status { get; set; }
    }
}
