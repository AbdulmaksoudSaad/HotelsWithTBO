﻿using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IDAL
{
  public  interface IPaymentData
    {
        PayLinkRequest GetPayLinkRequestDA(string BN ,string Sid,string ip,string pos, string NotTok);
        string GetDataForGatewayDA(string BN, string Sid );

    }
}
