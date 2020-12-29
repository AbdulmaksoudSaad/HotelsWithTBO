using Hotels.Common.Models;
using Hotels.DAL;
using Hotels.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.BLL
{
    public class GetPayData : IPayLink
    {
        public PayLinkRequest GetPayLinkRequest(string BN, string Sid,string ip,string pos,string NotTok)
        {
            PayLinkDB payLinkDB = new PayLinkDB();
         return payLinkDB.GetPayLinkRequestDA(BN, Sid,ip,pos,NotTok);
        }
    }
}
