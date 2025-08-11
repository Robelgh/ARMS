using Domain.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class user : BaseDomainEntity
    {
        Guid appId {  get; set; }
        string userName {  get; set; }
    }
}
