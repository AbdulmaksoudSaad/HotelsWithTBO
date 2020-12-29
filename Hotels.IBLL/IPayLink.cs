using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IBLL
{
   public interface IPayLink
    {
        PayLinkRequest GetPayLinkRequest(string BN, string Sid ,string ip ,string pos, string NotTok);

    }
}
