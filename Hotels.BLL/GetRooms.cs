using Hotels.Common;
using Hotels.Common.Models;
using Hotels.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.BLL
{
  public class GetRoom
    {
        public static HotelSearchRoom GetRoomsByHotelIDAndProvide(string Sid, string pid, string Hid)
        {
            try
            {
               
                // call data acess layer 
                var Rooms = GetRooms.GetRoomsByHotelIDAndProvide(Sid, pid, Hid);
                if (Rooms != null)
                {
                    return Rooms;
                }

                return null;
            }
            catch (Exception ex )
            {
                throw ex;
            }

        }
        public static RequiredBookingData GetRoomsData(string sid, string hotel, string Pid, string rooms)
        {
            try
            {  // call data acess layer 
                var Rooms = GetRooms.GetRoomsData(sid,hotel,Pid,rooms);
                if (Rooms != null)
                {
                    return Rooms;
                }

                return null;
            }
            catch (Exception ex )
            {
                throw ex;
            }

        }

        //////// Hotel Rooms
        //////public static List<RoomResult> GetAvailableRoom(string SessionId, int ResIndex, string HotelCode, string SID)
        //////{
        //////    var Result = RoomAvailabiltyService.Availabilty(SessionId, ResIndex, HotelCode, SID);
        //////    //map tbo rooms rsp to general res 
        //////    List<RoomResult> rooms = RoomMapper.MapTboRoomRspTogenrl(Result);
        //////    // apply business rules on rooms
        //////    CurrencyManager currencyManager = new CurrencyManager();
        //////    //double ProviderExcahngeRate = currencyManager.GetCurrencyConversion(searchOutputs[0].MinHotelPrice.Currency, BaseCur, searchData.sID);
        //////    // double ExcahngeRate = currencyManager.GetCurrencyConversion(BaseCur, searchData.Currency, searchData.sID);

        //////    //save rooms in DB 
        //////    HotelManager manager = new HotelManager();
        //////    manager.SaveTBORooms(rooms, SID, HotelCode);
        //////    return rooms;
        //////}
    }
}
