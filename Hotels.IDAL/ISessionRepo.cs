using Hotels.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.IDAL
{
    public interface ISessionRepo
    {
        void CreateSession(string sID);

        bool ChechSessionStatus(SearchData searchData);
        HotelSearchResponse  GetDataBySession(SearchData searchData);


    }
}
