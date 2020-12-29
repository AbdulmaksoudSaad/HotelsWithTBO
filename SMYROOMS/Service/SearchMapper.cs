using SMYROOMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Service
{
   public class SearchMapper
    {
        public static SearchInputData MapInputData(CBSearchData value)
        {
            SearchInputData data = new SearchInputData();
            data.checkin = value.dateFrom;
            data.checkout = value.dateTo;
            data.currency = value.Currency;
            data.Nationality = value.Nationality;
            data.hotels.Add("160");
            data.hotels.Add("180");
            ////
            
            var occupancies = new List<Occupancy>();
            for (int i = 0; i < value.rooms; i++)
            {
                //   ClsRoomRequest room = new ClsRoomRequest();
                List<int> child = new List<int>();
                var agesdata = new List<Pax>();
                var occupancy = new Occupancy();
                var adult = Convert.ToInt32(value.sAdults.Split(',')[i]);
                for (int j = 0; j < adult; j++)
                {
                    agesdata.Add(new Pax { age = 30 });
                }
                if (value.sChilds != "" && value.sChilds != null)
                {
                    var childern = Convert.ToInt16(value.sChilds.Split(',')[i]);
                }
                string[] arrAges;
                if (value.sAges != "" && value.sAges != null && value.sAges != "0-0")
                {

                    arrAges = value.sAges.Split(',')[i].Split('-');
                    foreach (string age in arrAges)
                    {
                        if (age != "" && age != "0")
                        {
                            agesdata.Add(new Pax { age = Convert.ToInt32(age) });
                            // child.Add(Convert.ToInt32(age));
                        }

                    }
                }
                occupancy.paxes.AddRange(agesdata);
                occupancies.Add(occupancy);
            }
            data.occupancies = occupancies;
                 
                //
                return data;
        }
    }
}
