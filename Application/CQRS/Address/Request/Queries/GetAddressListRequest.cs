
using ECX.HR.Application.DTOs.Addresss;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.HR.Application.CQRS.Addresss.Request.Queries
{
    public class GetAddressListRequest :IRequest<List<AddressDto>>
    {
       
    }
}
